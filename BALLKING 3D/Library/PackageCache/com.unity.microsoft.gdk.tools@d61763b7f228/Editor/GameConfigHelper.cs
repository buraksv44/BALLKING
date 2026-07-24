using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = System.Object;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    internal static class GameConfigHelper
    {
        private const string m_GdkRootMenu = "Assets/GDK/";

        public static void OpenWithGameConfigEditor(string assetPath)
        {
            if (!string.IsNullOrEmpty(assetPath))
            {
                string configEditorPath = Path.Combine(Unity.Microsoft.GDK.Tools.Editor.GdkUtility.GdkToolsPath, "GameConfigEditor.exe");
                if (!File.Exists(configEditorPath))
                {
                    EditorUtility.DisplayDialog("GDK tools not found", "Ensure the GDK is installed on this PC.", "Close");
                    return;
                }

                string fullAssetPath = Path.GetFullPath(assetPath);
                if (!File.Exists(fullAssetPath))
                {
                    EditorUtility.DisplayDialog("MicrosoftGame.config not found", "No MicrosoftGame.config file was found. Please re-import this plugin.", "Close");
                    return;
                }

                using (Process configEditorProcess = new Process())
                {
                    configEditorProcess.StartInfo.FileName = configEditorPath;
                    configEditorProcess.StartInfo.Arguments = $"\"{fullAssetPath}\" GameEngine";

                    configEditorProcess.Start();
                }
            }
        }

        [MenuItem(m_GdkRootMenu + "Open Project Settings - Microsoft GDK")]
        public static void OpenMicrosoftGdkProjectSettings()
        {
            GdkSettingsProvider.Open();
        }

        [MenuItem(m_GdkRootMenu + "Make GDK Settings Active")]
        public static void ActivateGdkSettings()
        {
            string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            Object asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Object));

            if (asset != null)
            {
                if (asset is GdkSettingsAsset settings)
                {
                    GdkSettingsProvider.ActivateGdkSettings(settings);
                    GdkSettingsProvider.ForceReload();

                    // Refresh the inspector window (it might be viewing the previous or newly selected GDK Settings asset)
                    GdkEditorHelpers.ForceInspectorWindowRefresh();
                }
            }
        }

        [MenuItem(m_GdkRootMenu + "Make GDK Settings Active", true)]
        public static bool ValidateGdkSettings()
        {
            // Check if the selected asset has the desired file extension
            string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            Object asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Object));

            if (asset != null)
            {
                if (asset is GdkSettingsAsset)
                {
                    return true;
                }
            }

            return false;
        }

        // enhance the context menu when selecting a *.mgc file
        [MenuItem(m_GdkRootMenu + "Open with Game Config Editor")]
        public static void OpenWithGameConfigEditor()
        {
            string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            OpenWithGameConfigEditor(assetPath);
        }

        [MenuItem(m_GdkRootMenu + "Open with Game Config Editor", true)]
        [MenuItem(m_GdkRootMenu + "Use for build", true)]
        public static bool ValidateEditGameConfig()
        {
            // Check if the selected asset has the desired file extension
            string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            return assetPath.EndsWith("." + GameConfigAsset.k_FileExtension);
        }

        // default action when double clicking a *.gsc file
        [OnOpenAsset]
#if UNITY_6000_5_OR_NEWER
        public static bool OnOpenAsset(EntityId entityId)
        {
            var asset = EditorUtility.EntityIdToObject(entityId);
            var assetPath = AssetDatabase.GetAssetPath(asset);
#else
        public static bool OpenAsset(int instanceID, int line)
        {
            string assetPath = AssetDatabase.GetAssetPath(instanceID);
            Object asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Object));
#endif

            if (asset != null)
            {
                if (asset is GdkSettingsAsset settings)
                {
                    // Would the user like to make this 'GDK Settings' asset active?
                    if (EditorUtility.DisplayDialog(
                            "Make GDK Settings Active",
                            "Would you like to make these GDK Settings active:\n\n" + assetPath +
                            "\n\n WARNING: A domain reload or editor restart (to clear GDK runtimes) may be required.",
                            "Yes",
                            "No") == true)
                    {
                        GdkSettingsProvider.ActivateGdkSettings(settings);
                        GdkSettingsProvider.ForceReload();

                        // Refresh the inspector window (it might be viewing the previous or newly selected GDK Settings asset)
                        GdkEditorHelpers.ForceInspectorWindowRefresh();
                    }
                }
                else if (asset is GameConfigAsset)
                {
                    if (assetPath.EndsWith("." + GameConfigAsset.k_FileExtension))
                    {
                        OpenWithGameConfigEditor(assetPath);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}