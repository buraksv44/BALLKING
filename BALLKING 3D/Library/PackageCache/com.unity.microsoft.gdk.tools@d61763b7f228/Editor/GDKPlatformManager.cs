using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    /// <summary>
    /// Abstract base class representing a GDK platform with build and UI configuration capabilities.
    /// </summary>
    public abstract class GdkPlatform
    {
        /// <summary>
        /// Gets the display name for this platform, shown as "Settings for {DisplayName}" in the tabbed view.
        /// </summary>
        public abstract string DisplayName { get; }

        /// <summary>
        /// Gets the content displayed on the tab header (icon, text, tooltip).
        /// </summary>
        public abstract GUIContent TabTitle { get; }

        /// <summary>
        /// Gets the tab order for UI display. Lower values appear on the left, higher values on the right.
        /// </summary>
        public abstract uint TabOrder { get; }

        /// <summary>
        /// Gets the Unity BuildTarget associated with this platform.
        /// </summary>
        public abstract BuildTarget BuildTarget { get; }

        /// <summary>
        /// Gets the Unity RuntimePlatform associated with this platform.
        /// </summary>
        public abstract RuntimePlatform RuntimePlatform { get; }

        /// <summary>
        /// Gets the compiler conditional symbol for this platform.
        /// </summary>
        public abstract string PlatformCompilerConditional { get; }

        /// <summary>
        /// Determines whether this platform supports the specified build target.
        /// </summary>
        /// <param name="buildTarget">The build target to check.</param>
        /// <returns>True if the build target is supported; otherwise, false.</returns>
        public abstract bool SupportsBuildTarget(BuildTarget buildTarget);

        /// <summary>
        /// Determines whether this platform supports the specified build target group.
        /// </summary>
        /// <param name="buildTargetGroup">The build target group to check.</param>
        /// <returns>True if the build target group is supported; otherwise, false.</returns>
        public abstract bool SupportsBuildTargetGroup(BuildTargetGroup buildTargetGroup);

        /// <summary>
        /// Gets a value indicating whether this platform only supports GDK builds.
        /// </summary>
        public virtual bool PlatformOnlySupportsGdk { get; } = true;

        /// <summary>
        /// Gets or sets the selected GDK edition for this platform. -1 indicates no selection.
        /// </summary>
        public virtual int SelectedGdkEdition { get; set; } = -1;

        /// <summary>
        /// Gets or sets the platform builder responsible for build processing.
        /// </summary>
        public IBuilder PlatformBuilder;

        /// <summary>
        /// Interface for platform-specific build processing operations.
        /// </summary>
        public interface IBuilder
        {
            /// <summary>
            /// Called before the build process begins.
            /// </summary>
            /// <param name="report">The build report containing build information.</param>
            /// <param name="platformGdkSettings">The GDK settings for this platform.</param>
            void OnPreprocessBuild(BuildReport report, GdkSettingsAsset.PlatformGdkSettings platformGdkSettings);

            /// <summary>
            /// Called after the build process completes.
            /// </summary>
            /// <param name="report">The build report containing build information.</param>
            /// <param name="platformGdkSettings">The GDK settings for this platform.</param>
            void OnPostprocessBuild(BuildReport report, GdkSettingsAsset.PlatformGdkSettings platformGdkSettings);

            /// <summary>
            /// Gets the output path for localization files.
            /// </summary>
            /// <param name="report">The build report containing build information.</param>
            /// <returns>The localization output path.</returns>
            string GetLocalizationOutputPath(BuildReport report);

            /// <summary>
            /// Gets the output path for localization files.
            /// </summary>
            /// <returns>The localization output path.</returns>
            [Obsolete("Please use GetLocalizationOutputPath(BuildReport report) instead", false)]
            string GetLocalizationOutputPath(); // Obsolete, will be removed at later date.

            /// <summary>
            /// Gets the path to the staged resource PRI file.
            /// </summary>
            /// <returns>The staged resource PRI path.</returns>
            string GetStagedResourcePriPath();

            /// <summary>
            /// Sets the source and destination paths for staged shell visuals images.
            /// </summary>
            /// <param name="stagedShellVisualsImageSourceDestPaths">Array of source-destination path pairs.</param>
            void SetStagedShellVisualsImageSourceDestPaths((string, string)[] stagedShellVisualsImageSourceDestPaths);

            /// <summary>
            /// Gets the source and destination paths for staged shell visuals images.
            /// </summary>
            /// <returns>Array of source-destination path pairs.</returns>
            (string, string)[] GetStagedShellVisualsImageSourceDestPaths();
        }
    }

    /// <summary>
    /// Manages registration and operations for GDK platforms in the Unity Editor.
    /// </summary>
    public class GDKPlatformManager
    {
        private static List<GdkPlatform> s_registeredPlatforms = new List<GdkPlatform>();

        /// <summary>
        /// Registers a GDK platform with the manager. Platforms are automatically sorted by TabOrder.
        /// </summary>
        /// <param name="processor">The GDK platform to register.</param>
        /// <exception cref="ArgumentNullException">Thrown when processor is null.</exception>
        public static void RegisterGdkPlatform(GdkPlatform processor)
        {
            if (processor == null)
            {
                throw new ArgumentNullException(nameof(processor));
            }

            s_registeredPlatforms.Add(processor);
            s_registeredPlatforms.Sort((x,y) => x.TabOrder.CompareTo(y.TabOrder));
        }

        /// <summary>
        /// Unregisters a previously registered GDK platform from the manager.
        /// </summary>
        /// <param name="processor">The GDK platform to unregister.</param>
        public static void UnregisterGdkPlatform(GdkPlatform processor)
        {
            s_registeredPlatforms.Remove(processor);
        }

        /// <summary>
        /// Gets the number of currently registered platforms.
        /// </summary>
        /// <returns>The count of registered platforms.</returns>
        public static int RegisteredPlatformCount() => s_registeredPlatforms.Count;

        private static GdkPlatform FindPlatformByBuildTarget(BuildTarget buildTarget)
        {
            foreach (var platform in s_registeredPlatforms)
            {
                if (platform.SupportsBuildTarget(buildTarget))
                {
                    return platform;
                }
            }
            return null;
        }

        private static GdkPlatform FindPlatformByBuildTargetGroup(BuildTargetGroup buildTargetGroup)
        {
            foreach (var platform in s_registeredPlatforms)
            {
                if (platform.SupportsBuildTargetGroup(buildTargetGroup))
                {
                    return platform;
                }
            }
            return null;
        }

        /// <summary>
        /// Processes pre-build operations for the platform that supports the build target.
        /// </summary>
        /// <param name="report">The build report containing build information.</param>
        /// <param name="platformGdkSettings">The GDK settings for the platform.</param>
        public static void ProcessPreBuild(BuildReport report, GdkSettingsAsset.PlatformGdkSettings platformGdkSettings)
        {
            var platform = FindPlatformByBuildTarget(report.summary.platform);
            platform?.PlatformBuilder?.OnPreprocessBuild(report, platformGdkSettings);
        }

        /// <summary>
        /// Processes post-build operations for the platform that supports the build target.
        /// </summary>
        /// <param name="report">The build report containing build information.</param>
        /// <param name="platformGdkSettings">The GDK settings for the platform.</param>
        public static void ProcessPostBuild(BuildReport report, GdkSettingsAsset.PlatformGdkSettings platformGdkSettings)
        {
            var platform = FindPlatformByBuildTarget(report.summary.platform);
            platform?.PlatformBuilder?.OnPostprocessBuild(report, platformGdkSettings);
        }

        /// <summary>
        /// Determines whether any registered platform supports the specified build target.
        /// </summary>
        /// <param name="buildTarget">The build target to check.</param>
        /// <returns>True if any platform supports the build target; otherwise, false.</returns>
        public static bool SupportsBuildTarget(BuildTarget buildTarget)
        {
            return FindPlatformByBuildTarget(buildTarget) != null;
        }

        /// <summary>
        /// Determines whether any registered platform supports the specified build target group.
        /// </summary>
        /// <param name="buildTargetGroup">The build target group to check.</param>
        /// <returns>True if any platform supports the build target group; otherwise, false.</returns>
        public static bool SupportsBuildTargetGroup(BuildTargetGroup buildTargetGroup)
        {
            return FindPlatformByBuildTargetGroup(buildTargetGroup) != null;
        }

        /// <summary>
        /// Gets the display name of the platform that supports the specified build target group.
        /// </summary>
        /// <param name="buildTargetGroup">The build target group to query.</param>
        /// <returns>The display name of the supporting platform, or an empty string if none found.</returns>
        public static string GetBuildTargetGroupDisplayName(BuildTargetGroup buildTargetGroup)
        {
            var platform = FindPlatformByBuildTargetGroup(buildTargetGroup);
            return platform?.DisplayName ?? string.Empty;
        }

        /// <summary>
        /// Determines whether the platform supporting the build target group only supports GDK builds.
        /// </summary>
        /// <param name="buildTargetGroup">The build target group to check.</param>
        /// <returns>True if the platform only supports GDK; true by default if no platform found.</returns>
        public static bool BuildTargetGroupOnlySupportsGdk(BuildTargetGroup buildTargetGroup)
        {
            var platform = FindPlatformByBuildTargetGroup(buildTargetGroup);
            return platform?.PlatformOnlySupportsGdk ?? true;
        }

        /// <summary>
        /// Gets the selected GDK edition for the platform supporting the build target group.
        /// </summary>
        /// <param name="buildTargetGroup">The build target group to query.</param>
        /// <returns>The selected GDK edition, or -1 if no platform found or no edition selected.</returns>
        public static int BuildTargetGroupGetSelectedGdkEdition(BuildTargetGroup buildTargetGroup)
        {
            var platform = FindPlatformByBuildTargetGroup(buildTargetGroup);
            return platform?.SelectedGdkEdition ?? -1;
        }

        /// <summary>
        /// Sets the selected GDK edition for the platform supporting the build target group.
        /// </summary>
        /// <param name="buildTargetGroup">The build target group to update.</param>
        /// <param name="edition">The GDK edition to select.</param>
        public static void BuildTargetGroupSetSelectedGdkEdition(BuildTargetGroup buildTargetGroup, int edition)
        {
            var platform = FindPlatformByBuildTargetGroup(buildTargetGroup);
            if (platform != null)
            {
                platform.SelectedGdkEdition = edition;
            }
        }

        /// <summary>
        /// Gets the localization output path for the platform supporting the build target.
        /// </summary>
        /// <param name="report">The build report containing build information.</param>
        /// <returns>The localization output path, or an empty string if no platform found.</returns>
        public static string GetLocalizationOutputPath(BuildReport report)
        {
            var platform = FindPlatformByBuildTarget(report.summary.platform);
            return platform?.PlatformBuilder?.GetLocalizationOutputPath(report) ?? string.Empty;
        }

        /// <summary>
        /// Gets the localization output path for the platform supporting the build target.
        /// </summary>
        /// <param name="buildTarget">The build target to query.</param>
        /// <returns>The localization output path, or an empty string if no platform found.</returns>
        [Obsolete("Please use GetLocalizationOutputPath(BuildReport report) instead", false)]
        public static string GetLocalizationOutputPath(BuildTarget buildTarget)
        {
            var platform = FindPlatformByBuildTarget(buildTarget);
            return platform?.PlatformBuilder?.GetLocalizationOutputPath() ?? string.Empty;
        }

        /// <summary>
        /// Gets the staged resource PRI path for the platform supporting the build target.
        /// </summary>
        /// <param name="buildTarget">The build target to query.</param>
        /// <returns>The staged resource PRI path, or an empty string if no platform found.</returns>
        public static string GetStagedResourcePriPath(BuildTarget buildTarget)
        {
            var platform = FindPlatformByBuildTarget(buildTarget);
            return platform?.PlatformBuilder?.GetStagedResourcePriPath() ?? string.Empty;
        }

        /// <summary>
        /// Sets the staged shell visuals image source and destination paths for the platform supporting the build target.
        /// </summary>
        /// <param name="buildTarget">The build target to update.</param>
        /// <param name="stagedShellVisualsImageSourceDestPaths">Array of source-destination path pairs.</param>
        public static void SetStagedShellVisualsImageSourceDestPaths(BuildTarget buildTarget, (string, string)[] stagedShellVisualsImageSourceDestPaths)
        {
            var platform = FindPlatformByBuildTarget(buildTarget);
            platform?.PlatformBuilder?.SetStagedShellVisualsImageSourceDestPaths(stagedShellVisualsImageSourceDestPaths);
        }

        /// <summary>
        /// Gets the staged shell visuals image source and destination paths for the platform supporting the build target.
        /// </summary>
        /// <param name="buildTarget">The build target to query.</param>
        /// <returns>Array of source-destination path pairs, or null if no platform found.</returns>
        public static (string, string)[] GetStagedShellVisualsImageSourceDestPaths(BuildTarget buildTarget)
        {
            var platform = FindPlatformByBuildTarget(buildTarget);
            return platform?.PlatformBuilder?.GetStagedShellVisualsImageSourceDestPaths();
        }

        /// <summary>
        /// Converts a build target group to its corresponding runtime platform.
        /// </summary>
        /// <param name="targetGroup">The build target group to convert.</param>
        /// <returns>The corresponding runtime platform.</returns>
        /// <exception cref="NotSupportedException">Thrown when the build target group is not supported by any registered platform.</exception>
        public static RuntimePlatform BuildTargetGroupToRuntimePlatform(BuildTargetGroup targetGroup)
        {
            foreach (GdkPlatform platform in s_registeredPlatforms)
            {
                var platformBuildTargetGroup = BuildPipeline.GetBuildTargetGroup(platform.BuildTarget);
                if (platformBuildTargetGroup == targetGroup)
                {
                    return platform.RuntimePlatform;
                }
            }

            throw new NotSupportedException("Unsupported BuildTargetGroup.");
        }

        /// <summary>
        /// Gets the compiler conditional symbol for the specified runtime platform.
        /// </summary>
        /// <param name="runtimePlatform">The runtime platform to query.</param>
        /// <returns>The compiler conditional symbol, or an empty string if no platform found.</returns>
        public static string GetPlatformCompilerConditional(RuntimePlatform runtimePlatform)
        {
            foreach (GdkPlatform platform in s_registeredPlatforms)
            {
                if (platform.RuntimePlatform == runtimePlatform)
                {
                    return platform.PlatformCompilerConditional;
                }
            }

            return "";
        }

        /// <summary>
        /// Gets a read-only collection of all registered GDK platforms.
        /// </summary>
        /// <returns>A read-only enumerable of registered platforms.</returns>
        public static IEnumerable<GdkPlatform> GetRegisteredPlatforms() => s_registeredPlatforms.AsReadOnly();
    }
}