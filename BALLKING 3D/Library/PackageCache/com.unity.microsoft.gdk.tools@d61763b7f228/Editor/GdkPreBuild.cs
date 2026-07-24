using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    /// <summary>
    /// Handles pre-build processing for GDK platforms.
    /// </summary>
    internal class PreBuildProcessor : IPreprocessBuildWithReport
    {
        public int callbackOrder => 50;

        /// <summary>
        /// Called before the build process begins to configure GDK-specific settings.
        /// </summary>
        /// <param name="report">The build report containing build information.</param>
        public void OnPreprocessBuild(BuildReport report)
        {
            if (!GDKPlatformManager.SupportsBuildTarget(report.summary.platform))
            {
                return;
            }

            var activeGdkSettings = GdkSettingsProvider.LoadSettings();
            var platformGdkSettings = GdkSettingsProvider.GetActiveGdkSettingsForBuildTarget(report.summary.platform);

            if (platformGdkSettings?.targetMicrosoftGdk != true)
            {
                ConfigureScriptingDefine(report, false);
                return;
            }

            ValidateGameConfig(platformGdkSettings);
            ValidateGameConfigElements(platformGdkSettings.gameConfigAsset);

            ConfigureScriptingDefine(report, true);

            if (activeGdkSettings.createGdkPlatformSettingsCs)
            {
                GdkSettingsProvider.GenerateGdkSettingsCs();
            }

            Debug.Log($"Starting GDK build process for target: {report.summary.platform}\n\n");

            GdkBuild.BuildLocalizationIfNeeded(report, report.summary.platform, platformGdkSettings);
            GDKPlatformManager.ProcessPreBuild(report, platformGdkSettings);
        }

        private static void ValidateGameConfig(GdkSettingsAsset.PlatformGdkSettings platformGdkSettings)
        {
            if (platformGdkSettings.gameConfigAsset == null)
            {
                throw new BuildFailedException("Build FAILED: Targeting Microsoft GDK but a Microsoft Game Config (MGC) has not been configured for this platform (See GDK Settings).\n\n");
            }
        }

        private static void ValidateGameConfigElements(GameConfigAsset gameConfigAsset)
        {
            if (!MGCValidatorUtility.ValidateElements(gameConfigAsset, out string validationMessage))
            {
                EditorGUIUtility.PingObject(gameConfigAsset);
                Selection.activeObject = gameConfigAsset;
                Debug.LogError($"{gameConfigAsset.name}.mgc is invalid! See details below.\n\n{validationMessage}", gameConfigAsset);
                throw new BuildFailedException("Microsoft Game Config is invalid.");
            }

            if (!string.IsNullOrEmpty(validationMessage))
            {
                Debug.LogWarning($"{gameConfigAsset.name}.mgc elements validation returned warning(s). " +
                                 $"See details below.\n\n{validationMessage}", gameConfigAsset);
            }
        }

        private bool ConfigureScriptingDefine(BuildReport report, bool enableDefine)
        {
            if (enableDefine)
            {
                GdkEditorHelpers.EnableScriptingDefine(GdkEditorHelpers.k_scriptingDefine, report.summary.platformGroup);
            }
            else
            {
                GdkEditorHelpers.DisableScriptingDefine(GdkEditorHelpers.k_scriptingDefine, report.summary.platformGroup);
            }

            return true;
        }
    }
}