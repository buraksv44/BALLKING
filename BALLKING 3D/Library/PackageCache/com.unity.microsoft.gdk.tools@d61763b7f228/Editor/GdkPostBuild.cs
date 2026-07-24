using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    /// <summary>
    /// Handles post-build processing for GDK platforms.
    /// </summary>
    internal class GdkPostBuild : IPostprocessBuildWithReport
    {
        public int callbackOrder => 50;

        /// <summary>
        /// Called after the build process completes to perform GDK-specific post-processing.
        /// </summary>
        /// <param name="report">The build report containing build information.</param>
        public void OnPostprocessBuild(BuildReport report)
        {
            if (!GDKPlatformManager.SupportsBuildTarget(report.summary.platform))
            {
                return;
            }

            GdkSettingsProvider.LoadSettings();
            var platformGdkSettings =
                GdkSettingsProvider.GetActiveGdkSettingsForBuildTarget(report.summary.platform);

            if (platformGdkSettings?.targetMicrosoftGdk != true)
            {
                return;
            }

            GDKPlatformManager.ProcessPostBuild(report, platformGdkSettings);
        }
    }
}