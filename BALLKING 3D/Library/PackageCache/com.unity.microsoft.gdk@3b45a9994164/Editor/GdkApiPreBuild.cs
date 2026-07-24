using System;
using System.Reflection;
using Unity.Microsoft.GDK.Tools.Editor;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

#if UNITY_GAMECORE
using UnityEditor.GameCore;
#endif

using UnityEngine;

namespace Unity.Microsoft.GDK.Editor
{
#if UNITY_EDITOR_WIN
    /// <summary>
    /// Handles pre-build operations for the GDK API package.
    /// </summary>
    internal class GdkApiPreBuild : IPreprocessBuildWithReport
    {
        public int callbackOrder => 25;

        private static bool s_HasGetRequiredGdkEdition;
        private static MethodInfo s_GetRequiredGdkEditionMethod;

        public void OnPreprocessBuild(BuildReport report)
        {
            GenerateGdkEditionAsset(report.summary.platform);
        }

        /// <summary>
        /// The GDK API needs to know at runtime which interface version is being used. We do this with a
        /// automatically generated `GdkEdition.Asset`. This asset passes the edition number to the runtime
        /// allowing for runtime handling of API compatability requirements.
        ///
        /// The GdkEdition.asset is created pre-build and later removed during post-build as it is only
        /// required by the runtime and should not be committed to source control.
        /// </summary>
        /// <param name="platform">Current build platform</param>
        private void GenerateGdkEditionAsset(BuildTarget platform)
        {
            switch (platform)
            {
                case BuildTarget.StandaloneWindows64:
                {
                    var edition = GdkPlugins.GetTrackedEditionNumber();
                    GdkEditionAssetGenerator.GenerateGdkEditionAsset(edition);
                    break;
                }

                case BuildTarget.GameCoreXboxOne:
                case BuildTarget.GameCoreXboxSeries:
                {
                    // Try to query the editor through reflection, only for newer editors
                    if (TryGenerateGameCoreEditionFromEditorApi(platform))
                    {
                        return;
                    }

                    // For older editors we need a different approach
                    if (TryGenerateGameCoreEditionFromEditorVersion())
                    {
                        return;
                    }

                    GdkApiDebug.LogWarning("Unable to determine GDK edition.");
                    break;
                }
            }
        }

        /// <summary>
        /// Using reflection try to get the method info for the public `GameCoreUtils.GetRequiredGdkEdition` method
        /// caching the result.
        /// </summary>
        private static void CacheMethodInfo()
        {
        #if UNITY_GAMECORE
            if (!s_HasGetRequiredGdkEdition)
            {
                var gameCoreUtilsType = typeof(GameCoreUtils);
                s_GetRequiredGdkEditionMethod = gameCoreUtilsType.GetMethod(
                    "GetRequiredGdkEdition",
                    BindingFlags.Public | BindingFlags.Static);

                s_HasGetRequiredGdkEdition = s_GetRequiredGdkEditionMethod != null;
            }
        #endif
        }

        /// <summary>
        /// Trys to generate a GdkEdition.asset by asking the Unity Editor for the required GDK edition
        /// number. This is only available in newer versions of the editor for some specific platforms.
        /// </summary>
        /// <param name="platform">Target platform</param>
        /// <returns>true on success, otherwise false</returns>
        private bool TryGenerateGameCoreEditionFromEditorApi(BuildTarget platform)
        {
            try
            {
                CacheMethodInfo();

                if (s_HasGetRequiredGdkEdition)
                {
                    var edition = (int)s_GetRequiredGdkEditionMethod.Invoke(null, new object[] { platform });
                    GdkEditionAssetGenerator.GenerateGdkEditionAsset(edition);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                GdkApiDebug.LogWarning($"Failed to get GDK edition from editor: {ex.Message}");

                return false;
            }
        }

        /// <summary>
        /// Trys to generate a GdkEdition.asset with an edition number based on the Unity editor version.
        /// This is a fallback only solution use when the editor does not support the required public API.
        /// </summary>
        /// <returns>true on success, otherwise false</returns>
        private bool TryGenerateGameCoreEditionFromEditorVersion()
        {
            try
            {
                var mapper = new GdkEditionMapper();
                var editorVersion = Application.unityVersion;
                var edition = mapper.GetGdkEditionForUnityVersion(editorVersion);
                GdkEditionAssetGenerator.GenerateGdkEditionAsset(edition);

                return true;
            }
            catch (Exception ex)
            {
                GdkApiDebug.LogWarning($"Failed to generate GDK edition from Unity version: {ex.Message}");

                return false;
            }
        }
    }
#endif // UNITY_EDITOR_WIN
}