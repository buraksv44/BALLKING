using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;

#if UNITY_2018_4_OR_NEWER
using UnityEditor.Build.Reporting;
#endif

using Debug = UnityEngine.Debug;

namespace Unity.Microsoft.GDK.Tools.Editor
{

    internal static class GdkBuild
    {
        public static string BuildLocalizationIfNeeded(BuildReport report, BuildTarget buildTarget, GdkSettingsAsset.PlatformGdkSettings platformGdkSettings /*BuildPlayerOptions buildOptions*/)
        {
            // Early build and staging of localization resources
            GameConfigAsset gameConfigAsset = platformGdkSettings.gameConfigAsset;

            if (gameConfigAsset != null &&
                platformGdkSettings.shellVisualsMode != GdkSettingsAsset.ShellVisualsMode.Disabled)
            {
                string outMessage;
                bool shouldRunMakePriAndCopy = platformGdkSettings.shellVisualsMode == GdkSettingsAsset.ShellVisualsMode.RunMakePriAndCopy
                    && gameConfigAsset.HasLanguagePairSet();

                if (!MGCValidatorUtility.ValidateLocalizationResources(gameConfigAsset, out outMessage))
                {
                    EditorGUIUtility.PingObject(gameConfigAsset);
                    Selection.activeObject = gameConfigAsset;
                    Debug.LogError($"{gameConfigAsset.name}.mgc is invalid! See details below.\n\n{outMessage}", gameConfigAsset);
                    throw new BuildFailedException("Microsoft Game Config is invalid.");
                }
                else if (outMessage != string.Empty)
                {
                    if (shouldRunMakePriAndCopy)
                    {
                        // Remove the warning about the 'Resources.pri' as it is going to be generated.
                        outMessage = RemoveResourcePriMessageIfNeeded(outMessage);
                    }

                    // Checking again as the only message might have been removed.
                    if (string.IsNullOrEmpty(outMessage))
                    {
                        Debug.LogWarning($"{gameConfigAsset.name}.mgc localization resources validation returned warning(s). " +
                            $"See details below.\n\n{outMessage}", gameConfigAsset);
                    }
                }

                var gameConfigLocalizationTool = new GameConfigLocalizationTool(gameConfigAsset);

                // Run 'MakePri.exe' command line tool to build the 'Resources.Pri' file
                if (shouldRunMakePriAndCopy && (gameConfigLocalizationTool.BuildResources(out outMessage) != 0
                    || !File.Exists(gameConfigLocalizationTool.GetResourcesPriPath())))
                {
                    throw new BuildFailedException("Failed to build localization resources.\n" + outMessage);
                }

                if (!shouldRunMakePriAndCopy &&
                    platformGdkSettings.shellVisualsMode != GdkSettingsAsset.ShellVisualsMode.CopyOnly)
                {
                    return null;
                }

                string outputPath = GDKPlatformManager.GetLocalizationOutputPath(report);

                // Make sure the directory exists
                if (!Directory.Exists(outputPath))
                {
                    Directory.CreateDirectory(outputPath);
                }

                // Copy the generated PRI file alongside the MGC (that is where it is expected)
                try
                {
                    File.Copy(gameConfigLocalizationTool.GetResourcesPriPath(), Path.Combine(outputPath, "Resources.pri"), true);
                }
                catch (Exception /*e*/)
                {
                    throw new BuildFailedException("Failed to copy build localization resources.pri to build folder." +
                        "Please ensure the game config is correctly configured.\n\n" + outMessage);
                }

                // Transfer over the localization resources
                string[] extensionsToCopy = { ".png" };
                string localizationAssetRoot = gameConfigLocalizationTool.GetShellVisualsResourcePath();

                // This is not exactly what we wanted but need to push on so will leave as is so as not to leave
                // code in a broken state. Struggling to get  the layout/priconfig set up as required.

                GDKPlatformManager.SetStagedShellVisualsImageSourceDestPaths(
                    report.summary.platform,
                    GdkEditorHelpers.CopyFilesWithExtensions(localizationAssetRoot, outputPath, extensionsToCopy).ToArray());
            }

            return null;
        }

        private static string RemoveResourcePriMessageIfNeeded(string outMessage)
        {
            var messagesArray = outMessage
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(message => !message.Contains("Warning: Resources.pri file not found at path"))
                .ToList();

            return string.Join("\n", messagesArray);
        }
    }
}