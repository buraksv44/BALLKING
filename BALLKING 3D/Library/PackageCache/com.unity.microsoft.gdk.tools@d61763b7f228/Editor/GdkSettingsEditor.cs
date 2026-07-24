using UnityEditor;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    /// <summary>
    /// Project-wide GDK Settings
    /// </summary>
    /// <remarks>
    /// Project-wide configuration settings relating to Microsoft GDK development are collected and stored in this
    /// class. Only one settings object can active at any time. It can be accessed through the Project Settings window.
    /// </remarks>
    [CustomEditor(typeof(GdkSettingsAsset))]
    internal class GdkSettingsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            GdkSettingsAsset activeGdkSettings = GdkSettingsProvider.LoadSettings();

            if (activeGdkSettings == target)
            {
                EditorGUILayout.HelpBox(UIContent.k_InfoSelectedGdkSettingsIsActive, MessageType.Info);

                GUILayout.Space(10);

                if (GUILayout.Button(UIContent.k_GenerateGdkPlatformSettingsCsButton, GUILayout.Height(30)))
                    GdkSettingsProvider.GenerateGdkSettingsCs();
            }
            else
            {
                string currentlyActiveAssetsPath = null;
                if (activeGdkSettings != null)
                    currentlyActiveAssetsPath = AssetDatabase.GetAssetPath(activeGdkSettings);

                if (!string.IsNullOrEmpty(currentlyActiveAssetsPath))
                    currentlyActiveAssetsPath = $"The currently active GDK Settings asset is '{currentlyActiveAssetsPath}'. ";

                EditorGUILayout.HelpBox($"This GDK Settings asset is not active. {currentlyActiveAssetsPath??""}Select \"Activate GDK Settings\" below to make '{target.name}' active.", MessageType.Warning);
                GUILayout.Space(10);

                if (GUILayout.Button(UIContent.k_MakeGdkSettingsActive, EditorStyles.miniButton))
                {
                    if (EditorUtility.DisplayDialog(
                            "Make GDK Settings Active",
                            "Would you like to make these GDK Settings active:\n\n" + target.name +
                            "\n\n WARNING: A domain reload or editor restart (to clear GDK runtimes) may be required.",
                            "Yes",
                            "No") == true)
                    {
                        GdkSettingsProvider.ActivateGdkSettings((GdkSettingsAsset)target);
                        GdkSettingsProvider.ForceReload();
                    }
                }
            }

            GUILayout.Space(10);
            if (GUILayout.Button(UIContent.k_OpenGdkSettings, GUILayout.Height(30)))
                GdkSettingsProvider.Open();
        }
    }
}