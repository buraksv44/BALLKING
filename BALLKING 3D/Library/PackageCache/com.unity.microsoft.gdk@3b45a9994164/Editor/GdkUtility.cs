using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Unity.Microsoft.GDK.Discovery;

using UnityEditor;
using UnityEngine;

namespace Unity.Microsoft.GDK.Editor
{
    public static class GdkUtility
    {
        static IEnumerable<GdkAssemblyInfo> CreateAssemblyList()
        {
            return GdkAssemblyRules.Resolve(s_PathCache.GdkEditionNum, s_PathCache.GrdkInstallPath);
        }

        private static readonly GdkData s_LatestGdk;
        private static GdkAssemblyPathCache s_PathCache;

        internal enum ImportResult
        {
            SuccessNoChange,
            SuccessInformUser,
            EditionNotFound,
            EditorRestartNeeded
        }

        static GdkUtility()
        {
            var installedGdks = GdkEnumerator.DiscoveredGdks.OrderByDescending(data => data.Edition).ToArray();

            s_LatestGdk  = installedGdks.FirstOrDefault();
            s_PathCache = new GdkAssemblyPathCache(s_LatestGdk) ;
        }

        public static string GdkEdition
        {
            get { return s_PathCache.GdkEdition; }
        }

        /// <summary>
        /// Import GDK DLLs from the lastest local edition (overwrites any DLLs with the same names).
        /// </summary>
        public static void PullGdkDlls() => ImportGdkDlls(s_LatestGdk);

        /// <summary>
        /// Imports the required GDK DLLs as specified by GdkData.
        /// </summary>
        /// <param name="gdkData"></param>
        /// <param name="report"></param>
        internal static ImportResult ImportGdkDlls(GdkData gdkData, StringBuilder report=null)
        {
#if UNITY_EDITOR_WIN
            s_PathCache = new GdkAssemblyPathCache(gdkData);

            if (string.IsNullOrEmpty(GdkEdition))
            {
                GdkApiDebug.LogWarning($"The Microsoft GDK Edition not found. The Microsoft GDK SDK is likely not installed on this machine.");
                return ImportResult.EditionNotFound;
            }

            bool logReport = false;
            if (report == null)
            {
                report = new StringBuilder();
                logReport = true;
            }

            report.AppendFormat($"Configuring Microsoft GDK edition: {s_PathCache.GdkEdition}\n\n");

            var assemblies = CreateAssemblyList();
            var pluginDllPath = Path.Combine(Application.dataPath, GdkPlugins.k_PluginsLocation);
            var processor = new GdkAssemblyImporter(pluginDllPath, report);

            var success = processor.ImportAssemblies(assemblies, out bool informUser);
            if (success == false)
            {
                report.AppendLine($"\nUnity editor RESTART needed. Please RESTART the editor when safe to do so.");

                if (logReport)
                {
                    Debug.LogWarning(report.ToString());
                }

                return ImportResult.EditorRestartNeeded;
            }

            AssetDatabase.Refresh();

            if (logReport)
            {
                GdkApiDebug.Log(report.ToString());
            }

            return informUser ? ImportResult.SuccessInformUser : ImportResult.SuccessNoChange;
#else // UNITY_EDITOR_WIN
            return ImportResult.SuccessNoChange;
#endif // UNITY_EDITOR_WIN
        }
    }
}