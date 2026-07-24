using System;
using System.IO;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace Unity.Microsoft.GDK.Editor
{
    internal sealed class FileWatcherService : IDisposable
    {
        private readonly object m_Lock = new object();
        private FileSystemWatcher m_Watcher;
        private volatile bool m_HasFileChanged;
        private string m_LastChangedFile;
        private FileSystemEventArgs m_LastEventArgs;
        private WatcherChangeTypes m_LastChangeType;

        private bool m_IsDisposed;

        private Action<string> m_OnFileChanged;
        private Action<string> m_OnFileCreated;
        private Action<string> m_OnFileDeleted;
        private Action<string, string> m_OnFileRenamed;

        /// <summary>
        /// Initializes a new instance of the FileWatcherService with specified directory path and file filter.
        /// </summary>
        /// <param name="directoryPath">The directory path to monitor for changes.</param>
        /// <param name="fileFilter">The file filter (e.g., "*.txt" or "*.*").</param>
        /// <exception cref="ArgumentNullException">Thrown when directoryPath or fileFilter is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when directory does not exist.</exception>
        public FileWatcherService(string directoryPath, string fileFilter)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                throw new ArgumentNullException(nameof(directoryPath));
            }

            if (string.IsNullOrEmpty(fileFilter))
            {
                throw new ArgumentNullException(nameof(fileFilter));
            }

            if (!Directory.Exists(directoryPath))
            {
                throw new ArgumentException("Directory does not exist", nameof(directoryPath));
            }

            Initialize(directoryPath, fileFilter);
            RegisterEditorUpdate();
        }

        /// <summary>
        /// Initializes a new instance of the FileWatcherService that watches all files in the specified directory.
        /// </summary>
        /// <param name="directoryPath">The directory path to monitor for changes.</param>
        /// <exception cref="ArgumentNullException">Thrown when directoryPath is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when directory does not exist.</exception>
        public FileWatcherService(string directoryPath) : this(directoryPath, "*.*")
        {
        }

        /// <summary>
        /// Gets a value indicating whether the service is actively watching for file changes.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Thrown when the service has been disposed.</exception>
        public bool IsWatching
        {
            get
            {
                ThrowIfDisposed();
                return m_Watcher?.EnableRaisingEvents ?? false;
            }
        }

        /// <summary>
        /// Registers callback methods for file system events.
        /// </summary>
        /// <param name="onFileChanged">Callback when a file is modified.</param>
        /// <param name="onFileCreated">Callback when a file is created.</param>
        /// <param name="onFileDeleted">Callback when a file is deleted.</param>
        /// <param name="onFileRenamed">Callback when a file is renamed. Provides both old and new file paths.</param>
        /// <exception cref="ObjectDisposedException">Thrown when the service has been disposed.</exception>
        public void RegisterCallbacks(
            Action<string> onFileChanged = null,
            Action<string> onFileCreated = null,
            Action<string> onFileDeleted = null,
            Action<string, string> onFileRenamed = null)
        {
            ThrowIfDisposed();

            lock (m_Lock)
            {
                m_OnFileChanged = onFileChanged;
                m_OnFileCreated = onFileCreated;
                m_OnFileDeleted = onFileDeleted;
                m_OnFileRenamed = onFileRenamed;
            }
        }

        private void Initialize(string directoryPath, string fileFilter)
        {
            ThrowIfDisposed();

            try
            {
                m_Watcher = new FileSystemWatcher(directoryPath, fileFilter)
                {
                    Path = directoryPath,
                    Filter = fileFilter,
                    NotifyFilter = NotifyFilters.LastWrite
                                | NotifyFilters.LastAccess
                                | NotifyFilters.FileName
                                | NotifyFilters.DirectoryName
                };

                // Subscribe to events within a lock to ensure thread safety
                lock (m_Lock)
                {
                    m_Watcher.Changed += OnFileChanged;
                    m_Watcher.Created += OnFileChanged;
                    m_Watcher.Deleted += OnFileChanged;
                    m_Watcher.Renamed += OnFileRenamed;
                    m_Watcher.EnableRaisingEvents = true;
                }
            }
            catch (Exception ex)
            {
                GdkApiDebug.LogError($"Failed to initialize file watcher for {directoryPath}: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }

        private void RegisterEditorUpdate()
        {
            EditorApplication.update += OnEditorUpdate;
        }

        private void OnEditorUpdate()
        {
            if (!m_HasFileChanged)
            {
                 return;
            }

            FileSystemEventArgs eventArgs;
            string changedFile;
            WatcherChangeTypes changeType;

            lock (m_Lock)
            {
                eventArgs = m_LastEventArgs;
                changedFile = m_LastChangedFile;
                changeType = m_LastChangeType;
                m_HasFileChanged = false;
                m_LastEventArgs = null;
                m_LastChangedFile = null;
            }

            // Process events outside the lock
            switch (changeType)
            {
                case WatcherChangeTypes.Changed:
                    m_OnFileChanged?.Invoke(changedFile);
                    break;
                case WatcherChangeTypes.Created:
                    m_OnFileCreated?.Invoke(changedFile);
                    break;
                case WatcherChangeTypes.Deleted:
                    m_OnFileDeleted?.Invoke(changedFile);
                    break;
                case WatcherChangeTypes.Renamed:
                    if (eventArgs is RenamedEventArgs renamedArgs)
                    {
                        m_OnFileRenamed?.Invoke(renamedArgs.OldFullPath, renamedArgs.FullPath);
                    }
                    break;
            }
        }

        private void OnFileChanged(object sender, FileSystemEventArgs args)
        {
            lock (m_Lock)
            {
                m_LastEventArgs = args;
                m_LastChangedFile = args.FullPath;
                m_LastChangeType = args.ChangeType;
                m_HasFileChanged = true;
            }
        }

        private void OnFileRenamed(object sender, RenamedEventArgs args)
        {
            lock (m_Lock)
            {
                m_LastEventArgs = args;
                m_LastChangedFile = args.FullPath;
                m_LastChangeType = args.ChangeType;
                m_HasFileChanged = true;
            }
        }

        private void ThrowIfDisposed()
        {
            if (m_IsDisposed)
            {
                throw new ObjectDisposedException(nameof(FileWatcherService));
            }
        }

        /// <summary>
        /// Disposes the FileWatcherService, releasing all resources and unregistering callbacks.
        /// </summary>
        public void Dispose()
        {
            if (m_IsDisposed)
            {
                return;
            }

            m_IsDisposed = true;

            EditorApplication.update -= OnEditorUpdate;

            lock (m_Lock)
            {
                m_OnFileChanged = null;
                m_OnFileCreated = null;
                m_OnFileDeleted = null;
                m_OnFileRenamed = null;

                if (m_Watcher != null)
                {
                    m_Watcher.EnableRaisingEvents = false;

                    m_Watcher.Changed -= OnFileChanged;
                    m_Watcher.Created -= OnFileChanged;
                    m_Watcher.Deleted -= OnFileChanged;
                    m_Watcher.Renamed -= OnFileRenamed;

                    m_Watcher.Dispose();
                    m_Watcher = null;
                }

                m_LastEventArgs = null;
                m_LastChangedFile = null;
            }
        }
    }
}