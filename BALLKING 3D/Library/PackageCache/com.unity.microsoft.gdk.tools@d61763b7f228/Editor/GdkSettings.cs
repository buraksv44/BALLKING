using UnityEditor;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    /// <summary>
    /// Provides easy access to the common GDK Tools API.
    /// </summary>
    public static class GdkSettings
    {
        /// <summary>
        /// Sets the provided GdkSettingsAsset as the currently active settings to be use at build time.
        /// </summary>
        /// <param name="gdkSettings"> GdkSettingsAsset to set as active. </param>
        public static void Activate(GdkSettingsAsset gdkSettings)
        {
            GdkSettingsProvider.ActivateGdkSettings(gdkSettings);
        }

        /// <summary>
        /// Retrieves the PlatformGdkSettings, from the active GDK Settings, associated with the specified BuildTarget.
        /// </summary>
        /// <param name="buildTarget"> BuildTarget associated with the PlatformGdkSettings being retrieved. </param>
        /// <returns>
        /// On success PlatformGdkSettings containing the active settings for the given BuildTarget, otherwise null.
        /// Maybe null is the associated platform has not yet been configured.
        /// </returns>
        public static GdkSettingsAsset.PlatformGdkSettings GetActivePlatformSettings(BuildTarget buildTarget)
        {
            return GdkSettingsProvider.GetActiveGdkSettingsForBuildTarget(buildTarget);
        }

        /// <summary>
        /// Retrieves the Game Config Asset, from the active GDK Settings, associated with the specified BuildTarget.
        /// </summary>
        /// <param name="buildTarget"> BuildTarget associated with the Game Config Asset being retrieved. </param>
        /// <returns> On success the GameConfigAsset associated with the BuiltTarget, otherwise null. </returns>
        public static GameConfigAsset GetActiveGameConfig(BuildTarget buildTarget)
        {
            var targetGdkSettings = GdkSettingsProvider.GetActiveGdkSettingsForBuildTarget(buildTarget);
            if (targetGdkSettings == null || targetGdkSettings.gameConfigAsset == null)
                return null;

            return targetGdkSettings.gameConfigAsset;
        }
    }
}