using System;
using System.IO;
using System.Linq;
using System.Text;
using Unity.Microsoft.GDK.Discovery;
using UnityEditor;
using UnityEngine;

namespace Unity.Microsoft.GDK.Editor
{
#if UNITY_EDITOR_WIN
    /// <summary>
    /// Manages GDK plugin initialization, file watching, and DLL importing for the Unity Editor.
    /// This class automatically handles GDK edition tracking and updates.
    /// </summary>
    [InitializeOnLoad]
    internal static class GdkPlugins
    {
        internal const string k_PluginsLocation = @"Plugins/GDK";
        internal const string k_TrackerFileName = @"GdkEdition.json";

        private static FileWatcherService s_FileWatcher;
        private static GdkEditionTracker s_EditionTracker;

        private static readonly object s_Lock = new();
        private static bool s_IsInitialized;

        static GdkPlugins()
        {
            AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
            EditorApplication.quitting += OnEditorQuitting;

            // Use EditorApplication.update to delay the initialization slightly so as to avoid importing
            // during Editor initialization.
            // NOTE: `EditorApplication.delayCall` is not safe to use during `[InitializeOnLoad]` or domain reloads.
            EditorApplication.update += WaitForInitialization;
        }

        private static void WaitForInitialization()
        {
            if (!EditorApplication.isUpdating && !EditorApplication.isCompiling)
            {
                EditorApplication.update -= WaitForInitialization;

                if (!s_IsInitialized)
                {
                    InitializeGdkTracking();
                }
            }
        }

        /// <summary>
        /// Cleans up all resources used by the GDK plugin system.
        /// </summary>
        /// <remarks>
        /// This method is thread-safe and can be called multiple times safely.
        /// </remarks>
        private static void CleanupResources()
        {
            lock (s_Lock)
            {
                CleanupFileWatcher();

                s_EditionTracker = null;
                s_IsInitialized = false;
            }
        }

        /// <summary>
        /// Attempts to reinitialize the GDK tracking system. This can be used to recover from errors
        /// or to force a refresh of the GDK configuration.
        /// </summary>
        /// <remarks>
        /// This method will clean up existing resources before attempting reinitialization.
        /// It's safe to call this method multiple times.
        /// </remarks>
        internal static void RetryInitialization()
        {
            if (s_IsInitialized)
            {
                CleanupResources();
            }

            InitializeGdkTracking();
        }

        private static bool TryInitializeEditionTracker()
        {
            if (s_EditionTracker == null)
            {
                s_EditionTracker = new GdkEditionTracker(Path.Combine(Application.dataPath, k_PluginsLocation, k_TrackerFileName));
            }

            if (!s_EditionTracker.TrackerFileExists())
            {
                var installedGdks = GdkEnumerator.DiscoveredGdks
                    .OrderByDescending(data => data.Edition)
                    .ToArray();

                if (installedGdks.Length == 0)
                {
                    //GdkApiDebug.LogWarning($"No 'Microsoft GDK' installations found. Recommended edition: {GdkDiscovery.EditionNumberToString(GdkEditions.Recommended)}");
                    return false;
                }

                var latestGdk = installedGdks[0];
                s_EditionTracker.WriteGdkEdition(latestGdk.Edition);
            }

            return true;
        }

        private static void HandleEditionUpdate()
        {
            lock (s_Lock)
            {
                if (TryInitializeEditionTracker())
                {
                    ImportEdition(FetchEditionNumber());
                }
            }
        }

        private static void InitializeGdkTracking()
        {
            try
            {
                lock (s_Lock)
                {
                    if (TryInitializeEditionTracker())
                    {

                        ImportEdition(FetchEditionNumber());
                        InitializeTrackerFileWatcher();

                        s_IsInitialized = true;
                    }
                }
            }
            catch (Exception ex)
            {
                GdkApiDebug.LogError($"Unable to create the GDK tracking file: {ex.Message}\n{ex.StackTrace}");
                CleanupFileWatcher();
            }
        }

        /// <summary>
        /// Initializes the file system watcher for tracking GDK configuration changes.
        /// </summary>
        /// <remarks>
        /// The watcher monitors the GDK plugins directory for changes to JSON configuration files.
        /// </remarks>
        private static void InitializeTrackerFileWatcher()
        {
            try
            {
                var watchPath = Path.Combine(Application.dataPath, k_PluginsLocation);

                if (s_FileWatcher != null)
                {
                    s_FileWatcher.Dispose();
                    s_FileWatcher = null;
                }

                s_FileWatcher = new FileWatcherService(watchPath, k_TrackerFileName);
                s_FileWatcher.RegisterCallbacks(
                    onFileChanged: OnFileChanged,
                    onFileDeleted: OnFileDeleted
                );
            }
            catch (Exception ex)
            {
                GdkApiDebug.Log($"Error initializing GDK tracker file watcher: {ex.Message}, you may need to restart Unity.");

                s_FileWatcher?.Dispose();
                s_FileWatcher = null;
            }
        }

        /// <summary>
        /// Cleans up the file watcher resources.
        /// </summary>
        private static void CleanupFileWatcher()
        {
            if (s_FileWatcher != null)
            {
                s_FileWatcher.Dispose();
                s_FileWatcher = null;
            }
        }

        private static void OnBeforeAssemblyReload()
        {
            CleanupResources();
        }

        private static void OnEditorQuitting()
        {
            CleanupResources();
        }

        private static void OnFileChanged(string path)
        {
            // Dispatch to main thread to handle Unity API calls safely
            EditorApplication.delayCall += HandleEditionUpdate;
        }

        private static void OnFileDeleted(string path)
        {
            // Dispatch to main thread to handle Unity API calls safely
            EditorApplication.delayCall += HandleEditionUpdate;
        }

        /// <summary>
        /// Validates and processes the GDK edition number.
        /// </summary>
        /// <returns>
        /// The validated edition number, or -1 if the edition is invalid or auto-import is disabled.
        /// </returns>
        private static int FetchEditionNumber()
        {
            int editionNumber;

            try
            {
                editionNumber = s_EditionTracker.ReadGdkEdition();
            }
            catch (Exception ex)
            {
                GdkApiDebug.LogError($"Failed to read Microsoft GDK tracker file: {ex.Message}");
                return -1;
            }

            if (editionNumber < 0)
            {
                return -1; // Auto-import disabled by tracker
            }

            switch (GdkEditions.ValidateEdition(editionNumber))
            {
                case GdkEditions.Compatability.TooOld:
                    GdkApiDebug.Log(
                        $"The tracked Microsoft GDK edition can not be used, {GdkVersion.Format(editionNumber)}, " + "" +
                        $"minimum allowed edition, {GdkVersion.Format(GdkEditions.Minimum)}.");
                    return -1; // Auto-import disabled

                case GdkEditions.Compatability.OK:
                    return editionNumber;

                case GdkEditions.Compatability.TooNew:
                    GdkApiDebug.LogWarning(
                        $"The tracked Microsoft GDK edition, {GdkVersion.Format(editionNumber)}, " +
                        $"is newer than recommended edition, {GdkVersion.Format(GdkEditions.Recommended)}. " +
                        $"Compatibility issues may occur.");
                    return editionNumber;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Attempts to locate and import DLLs for the specified GDK edition.
        /// </summary>
        /// <param name="editionNumber">The GDK edition number to import.</param>
        /// <returns>True if the import was successful, false otherwise.</returns>
        private static bool ImportEdition(int editionNumber)
        {
            if (editionNumber < 0)
            {
                GdkApiDebug.Log($"Can not read edition number from the GdkEdition.json tracking file, JSON format may be invalid - automatic Microsoft GDK DLL imports are disabled until resolved.");
                return true;
            }

            var gdkData = GdkEnumerator.DiscoveredGdks.FirstOrDefault(gdk => gdk.Edition == editionNumber);
            if (gdkData.Path == null)
            {
                GdkApiDebug.LogWarning($"The tracked Microsoft GDK edition, {GdkVersion.Format(editionNumber)}, " +
                                       $"is not installed on this machine.");
                return false;
            }

            try
            {
                var report = new StringBuilder();

                var importResult = GdkUtility.ImportGdkDlls(gdkData, report);
                if (importResult != GdkUtility.ImportResult.SuccessNoChange)
                {
                    report.AppendLine();

                    if (importResult == GdkUtility.ImportResult.EditorRestartNeeded)
                    {
                        GdkApiDebug.LogError(report.ToString());
                        GdkApiDebug.LogError($"The Microsoft GDK DLLs are locked. Please RESTART Unity when safe to do so.");

                       string dialogMessage =
                            $"The Microsoft DLLs in this project are locked.\n\nTo complete the Microsoft GDK edition selection " +
                            $"the Unity Editor must be restarted. Would you like to RESTART now.\n\n" +
                            $"(Selecting 'No' or cancelling a save will require you to restart later when it is safe to do so)";

                       bool shouldRestart = EditorUtility.DisplayDialog($"Microsoft GDK Edition Selection", dialogMessage, "Yes", "No");
                       if (shouldRestart)
                       {
                            // Save any unsaved assets before restarting
                            AssetDatabase.SaveAssets();

                            // Get current project path (removing "/Assets" from the end)
                            string projectPath = Application.dataPath.Substring(0, Application.dataPath.Length - 7);

                            // Restart the Unity Editor by reopening the current project
                            EditorApplication.OpenProject(projectPath);
                       }
                    }
                    else
                    {
                        report.AppendLine($"Successfully imported Microsoft GDK DLLs for edition: " +
                                          $"{GdkVersion.Format(editionNumber)}");

                        GdkApiDebug.Log(report.ToString());
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                GdkApiDebug.LogError($"Failed to import Microsoft GDK DLLs: {ex.Message}");
                return false;
            }
        }

        public static int GetTrackedEditionNumber()
        {
            if (TryInitializeEditionTracker())
            {
                return s_EditionTracker.ReadGdkEdition();
            }
            else
            {
                return -1;
            }
        }
    }
#endif
}