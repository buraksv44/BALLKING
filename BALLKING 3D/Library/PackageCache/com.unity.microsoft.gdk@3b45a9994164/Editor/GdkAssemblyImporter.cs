using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace Unity.Microsoft.GDK.Editor
{
    internal sealed class GdkAssemblyImporter
    {
        private readonly StringBuilder m_Report;
        private bool m_InformUser;
        private bool m_RestartNeeded;

        private readonly string m_PluginDllPath;

        public GdkAssemblyImporter(string pluginDllPath, StringBuilder report)
        {
            m_Report = report;
            m_InformUser = false;
            m_RestartNeeded = false;

            m_PluginDllPath = pluginDllPath;
        }

        public bool ImportAssemblies(IEnumerable<GdkAssemblyInfo> assemblies, out bool informUser)
        {
            GdkAssemblyAssetProcessor.ClearPendingAssemblies();

            // Ensure the expected plugin directory exists
            if (!Directory.Exists(m_PluginDllPath))
            {
                Directory.CreateDirectory(m_PluginDllPath);
            }

            // Import or remove assemblies as needed
            foreach (var assembly in assemblies)
            {
                try
                {
                    ImportAssembly(assembly);
                }
                catch (Exception ex)
                {
                    var errorCode = System.Runtime.InteropServices.Marshal.GetHRForException(ex) & ((1 << 16) - 1);
                    if (errorCode == 32 || errorCode == 33) // 32: ERROR_SHARING_VIOLATION, 33: ERROR_LOCK_VIOLATION
                    {
                        m_RestartNeeded = true;
                    }

                    m_Report.AppendFormat("Error importing assembly {0}: {1}\n", assembly.SourcePath, ex.Message);
                    informUser = true;
                    return false;
                }
            }

            informUser = m_InformUser;
            return true;
        }

        public bool RestartNeeded
        {
            get { return m_RestartNeeded; }
            private set { m_RestartNeeded = value; }
        }

        private void ImportAssembly(GdkAssemblyInfo assembly)
        {
            var absPath = Path.Combine(m_PluginDllPath, assembly.Name);
            var relativePath = FileUtil.GetProjectRelativePath(absPath);

            if (!assembly.Include)
            {
                HandleExcludedAssembly(assembly, relativePath);
                return;
            }

            if (!File.Exists(assembly.SourcePath))
            {
                m_Report.AppendFormat(
                    "IMPORT : {0}, Source not found. Make sure the Microsoft GDK is correctly installed " +
                    "(expected source location: {1})\n",
                    assembly.Name,
                    assembly.SourcePath);

                m_InformUser = true;

                return;
            }

            if (File.Exists(relativePath) && !ShouldUpdateExistingAssembly(assembly.SourcePath, relativePath))
            {
                m_Report.AppendFormat("NO CHANGE: {0}, already in project.\n", assembly.Name);
                return;
            }

            ImportAssembly(assembly, relativePath);
        }

        private void HandleExcludedAssembly(GdkAssemblyInfo assembly, string relativePath)
        {
            if (!File.Exists(relativePath))
            {
                return;
            }

            if (AssetDatabase.DeleteAsset(relativePath))
            {
                m_Report.AppendFormat("REMOVE : {0}, Not required for active edition.\n", relativePath);
            }
            else
            {
                m_Report.AppendFormat("REMOVE : {0}, Not required for active edition but FAILED to remove.\n",
                    relativePath);
            }

            m_InformUser = true;
        }

        private bool ShouldUpdateExistingAssembly(string sourcePath, string destinationPath)
        {
            try
            {
                var sourceFileInfo = new FileInfo(sourcePath);
                var destFileInfo = new FileInfo(destinationPath);

                if (sourceFileInfo.Length != destFileInfo.Length)
                {
                    return true;
                }

                // Pre March 24 we use a thunks dll shipped with the package however GetVersionInfo()
                // seems unable to extract version details for the package .dll returning 'null'
                // this results in a version miss-match on this file always so can not be trusted
                if (sourcePath.IndexOf("com.unity.microsoft.gdk", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return false;
                }

                // ... in all other cases we will do a version check

                var sourceVersion = FileVersionInfo.GetVersionInfo(sourcePath);
                var destVersion = FileVersionInfo.GetVersionInfo(destinationPath);

                return sourceVersion.FileVersion != destVersion.FileVersion ||
                       sourceVersion.ProductVersion != destVersion.ProductVersion;
            }
            catch (Exception)
            {
                // If we can't compare versions, better safe than sorry
                return true;
            }
        }

        private void ImportAssembly(GdkAssemblyInfo assembly, string relativePath)
        {
            GdkAssemblyAssetProcessor.AddPendingAssemblyName(assembly.Name);

            File.Copy(assembly.SourcePath, relativePath, true);
            AssetDatabase.ImportAsset(relativePath, ImportAssetOptions.ForceUpdate);

            m_Report.AppendFormat("IMPORT: {0} => {1}\n", assembly.SourcePath, relativePath);
            // Removing to prevent APV failure: m_InformUser = true;
        }
    }
}