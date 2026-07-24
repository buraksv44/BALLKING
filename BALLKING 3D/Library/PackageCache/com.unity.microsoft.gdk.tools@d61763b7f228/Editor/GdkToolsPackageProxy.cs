using System.IO;
using UnityEditor;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    /*
     * WARNING! This class provides support to the Unity Editor Platform Extensions through
     * the use of reflection. Changes here could result in loosing the ability to perform
     * a successful build.
     *
     * When changing ensure you review and test against GdkToolsPackageProxy.cs in relevant
     * platform extension.
     */
    public class GDKToolsPackageProxy
    {
        public static bool EnabledForBuildTarget(BuildTarget buildTarget)
        {
            GdkSettingsAsset.PlatformGdkSettings platformGdkSettings =
                GdkSettingsProvider.GetActiveGdkSettingsForBuildTarget(buildTarget);

            return platformGdkSettings is { targetMicrosoftGdk: true };
        }

        public static string GetSCID(BuildTarget buildTarget)
        {
            GdkSettingsAsset.PlatformGdkSettings platformGdkSettings =
                GdkSettingsProvider.GetActiveGdkSettingsForBuildTarget(buildTarget);

            if (platformGdkSettings.gameConfigAsset)
            {
                return platformGdkSettings.gameConfigAsset.SCID;
            }

            return "";
        }

        // The platform extension build Program wants the staged location of the Resource.pri file.
        public static string GetStagedResourcePriPath(BuildTarget buildTarget)
        {
            return GDKPlatformManager.GetStagedResourcePriPath(buildTarget);
        }

        // The platform extension build Program which wants the staged location of the Shell Visual image files.
        // Since this is called from PostProcess... the GDK Package has time to prepare during the PreProcess step.
        public static (string, string)[] GetStagedShellVisualsImageSourceDestPaths(BuildTarget buildTarget)
        {
            return GDKPlatformManager.GetStagedShellVisualsImageSourceDestPaths(buildTarget);
        }
    }
}