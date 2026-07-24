using System;
using System.Collections.Generic;
using UnityEditor;

namespace Unity.Microsoft.GDK.Editor
{
    /// <summary>
    /// Post processes imported GDK assemblies to ensure they are set to be compatible with the
    /// Unity Editor ONLY by default.
    ///
    /// The GDK assemblies need to be available for Playmode but we do not want them included
    /// in target builds automatically by the editor.
    /// </summary>
    /// <remarks>
    /// This class inherits from AssetPostprocessor to handle post-processing of imported assets,
    /// specifically focusing on assemblies in the pending list. We expect GDK assemblies in the
    /// known "magic" plugin folder.
    /// </remarks>
    internal class GdkAssemblyAssetProcessor : AssetPostprocessor
    {
        private static readonly List<string> s_PendingAssemblies = new List<string>();

        internal static void ClearPendingAssemblies()
        {
            s_PendingAssemblies.Clear();
        }

        internal static void AddPendingAssemblyName(string assemblyName)
        {
#if UNITY_EDITOR_WIN
            // We expect the GDK assemblies at a pre-defined location
            s_PendingAssemblies.Add($"Assets/{GdkPlugins.k_PluginsLocation}/{assemblyName}");
#endif
        }

        /// <summary>
        /// Processes all assets after import, focusing on assemblies listed as pending.
        /// </summary>
        /// <param name="importedAssets">Array of paths to imported assets.</param>
        /// <param name="deletedAssets">Array of paths to deleted assets. Not used.</param>
        /// <param name="movedAssets">Array of paths to moved assets. Not used.</param>
        /// <param name="movedFromAssetPaths">Array of old paths for moved assets. Not used.</param>
        /// <remarks>
        /// This method is on the look-out for recently imported GDK assemblies which have been added
        /// to the pending assemblies list. When found the assembly will be configured to be available for
        /// the Editor ONLY by default. This makes them available for Playmode whilst avoiding the
        /// assembly from being automatically deployed to unsupported built targets.
        /// </remarks>
        static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            foreach (var str in importedAssets)
            {
                if (s_PendingAssemblies.Contains(str))
                {
                    var importer = AssetImporter.GetAtPath(str) as PluginImporter;
                    if (importer != null)
                    {
                        importer.SetCompatibleWithAnyPlatform(false);

                        importer.SetCompatibleWithEditor(true);
                        importer.SetPlatformData("Editor", "OS", "Windows");

                        importer.SetCompatibleWithPlatform(BuildTarget.StandaloneWindows, false);
                        importer.SetCompatibleWithPlatform(BuildTarget.StandaloneWindows64, false);

                        importer.SaveAndReimport();

                        s_PendingAssemblies.Remove(str);
                    }
                }
            }
        }
    }
}