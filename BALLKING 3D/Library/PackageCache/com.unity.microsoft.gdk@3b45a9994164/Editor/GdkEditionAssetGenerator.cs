using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Unity.XGamingRuntime;

#if UNITY_GAMECORE
using UnityEditor.GameCore;
#endif

namespace Unity.Microsoft.GDK.Editor
{
    /// <summary>
    /// Utility class for generating and managing GDK Edition asset files.
    /// This class handles the creation and removal of GDK Edition assets in the project.
    /// </summary>
    internal static class GdkEditionAssetGenerator
    {
        private static class Paths
        {
            private const string k_ResourcesFolderName = "GDKEditionAutoGen";
            private const string k_AssetName = "GDKEdition.asset";

            public static readonly string ResourcesPath = $"Resources/{k_ResourcesFolderName}";
            public static readonly string AssetPath = $"Assets/{ResourcesPath}/{k_AssetName}";
            public static readonly string FolderPath = $"Assets/{ResourcesPath}";
        }

        /// <summary>
        /// Generates a new GDK Edition asset from a string edition number.
        /// Creates necessary folders if they don't exist and saves the asset to the project.
        /// </summary>
        /// <param name="edition">The edition number as a string.</param>
        /// <exception cref="FormatException">Thrown when the edition string cannot be parsed to an integer.</exception>
        public static void GenerateGdkEditionAsset(string edition)
        {
            GenerateGdkEditionAsset(Int32.Parse(edition));
        }

        /// <summary>
        /// Generates a new GDK Edition asset with the specified edition number.
        /// Creates necessary folders if they don't exist and saves the asset to the project.
        /// Refreshes the Asset Database after creation.
        /// </summary>
        /// <param name="edition">The edition number to use for the GDK Edition asset.</param>
        public static void GenerateGdkEditionAsset(Int32 edition)
        {
            var gdkEditionAsset = GdkEditionAsset.CreateInstance(edition);

            var path = Path.Combine(Application.dataPath, Paths.ResourcesPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            AssetDatabase.CreateAsset(gdkEditionAsset, Paths.AssetPath);
            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Removes the GDK Edition asset and its containing folder from the project if they exist.
        /// Refreshes the Asset Database after removal.
        /// </summary>
        public static void RemoveGdkEditionAsset()
        {
            if (File.Exists(Paths.AssetPath))
            {
                AssetDatabase.DeleteAsset(Paths.AssetPath);
            }

            if (Directory.Exists(Paths.FolderPath))
            {
                AssetDatabase.DeleteAsset(Paths.FolderPath);
            }

            AssetDatabase.Refresh();
        }
    }
}