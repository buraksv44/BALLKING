using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;


namespace Unity.Microsoft.GDK.Tools.Editor
{
    internal class WindowsPlatformBuilder : GdkPlatform.IBuilder
    {
        private static readonly string kMetadataFolderName = "Metadata";
        private static readonly string kMakePkgBatFileName = "gdktool.bat";

        /// <summary>
        /// 1. Copy metadata files to the output directory.
        /// 2. Run makepkg.exe to generate the package.
        /// </summary>
        private static readonly string kPostBuildEventCommand =
            $"xcopy \"$(SolutionDir)build\\bin\\$(SolutionName)_Data\" \"$(OutDir)\\$(SolutionName)_Data\" /s /i /y " +
            $"& xcopy \"$(SolutionDir){kMetadataFolderName}\\*.*\" \"$(OutDir)\" /s /i /y ";

        private static readonly string kRunBatCommand =
            $"& \"$(SolutionDir){kMakePkgBatFileName}\" \"$(SolutionDir)\" \"$(OutDir)\" $(Configuration)";

        /// <summary>
        /// Batch file template that is:
        ///     1. Checks arguments passed by vcxproj macros.
        ///     2. Queries the registry to retrieve the Gdk tool path
        ///         - This is done so a machine A can generate a build and
        ///         a machine B with different setup can still run the bat file.
        ///         The registry is pointing toward the Microsoft GDK installation path,
        ///         so it doesn't matter where the GDK version is installed.
        ///         The path is completed afterward - adding the /bin.
        ///     3. Runs makepkg.exe commands:
        ///         1. genmap - Create the layout.xml directly in the content directory
        ///         2. pack - Create the package with provided arguments.
        ///     4. If sideloads option was set:
        ///         - The package is generated using test signing encryption.
        ///         - The package is sideloaded to the device using wdapp.exe install.
        ///
        /// Arg 0: extra arguments to pass to makepkg.exe.
        /// Arg 1: encryption switch.
        /// </summary>
        private static readonly string kBatFileTemplate = "@echo off" +
            "\r\nREM 1. Arguments checks" +
            "\r\nset solutionPath=%~1" +
            "\r\nIF \"%solutionPath%\"==\"\" (" +
            "\r\n   echo No solution path provided. Please run from Visual Studio Solution. Exiting..." +
            "\r\n   exit /b 1" +
            "\r\n)" +
            "\r\nIF NOT EXIST \"%solutionPath%\" (" +
            "\r\n   echo The solution path \"%solutionPath%\" does not exist. Exiting..." +
            "\r\n   exit /b 1\r\n)" +
            "\r\nset binariesPath=%~2" +
            "\r\nREM Ignore last '\\' character from directory path." +
            "\r\nset binariesPath=%binariesPath:~0,-1%" +
            "\r\nIF \"%binariesPath%\"==\"\" (" +
            "\r\n   echo No binary path provided. Please run from Visual Studio Solution. Exiting..." +
            "\r\n   exit /b 1" +
            "\r\n)" +
            "\r\nIF NOT EXIST \"%binariesPath%\" (" +
            "\r\n   echo The binary path \"%binariesPath%\" does not exist. Exiting..." +
            "\r\n   exit /b 1" +
            "\r\n)" +
            "\r\nset configuration=%~3" +
            "\r\nIF \"%configuration%\"==\"\" (" +
            "\r\n   echo No configuration provided. Please run from Visual Studio Solution. Exiting..." +
            "\r\n   exit /b 1" +
            "\r\n)" +
            "\r\nREM 2. Query the registry value to retrieve Gdk tool path." +
            // Retrieving the registry key from here allows the bat file to be use from another machine.
            "\r\nfor /f \"tokens=2*\" %%a in ('reg query \"HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\Microsoft\\GDK\" /v GRDKInstallPath ^| find \"GRDKInstallPath\"') do set \"gdkPath=%%b\"" +
            "\r\nset makePkgPath=\"%gdkPath%bin\\makepkg.exe\"" +
            "\r\nset wdappPath=\"%gdkPath%bin\\wdapp.exe\"" +
            "\r\nset layoutXml=%binariesPath%\\layout.xml" +
            "\r\nset msixvc=%solutionPath%MSIXVC\\%configuration%" +
            "\r\nIF NOT EXIST \"%msixvc%\" (" +
            "\r\n   mkdir \"%msixvc%\"" +
            "\r\n)" +
            "\r\nREM 3. Create layout.xml" +
            "\r\necho Running command: %makePkgPath% genmap /f \"%layoutXml%\" /d \"%binariesPath%\"" +
            "\r\ncall %makePkgPath% genmap /f \"%layoutXml%\" /d \"%binariesPath%\"" +
            "{2}" +
            "\r\nREM 4. Create package" +
            "\r\nset encryptionSwitch={1}" +
            "\r\necho Running command: %makePkgPath% pack /f \"%layoutXml%\" /d \"%binariesPath%\" /pd \"%msixvc%\" /pc /CorrelationId Unity %encryptionSwitch% {0}" +
            "\r\ncall %makePkgPath% pack /f \"%layoutXml%\" /d \"%binariesPath%\" /pd \"%msixvc%\" /pc /CorrelationId Unity %encryptionSwitch% {0}" +
            "\r\nIF NOT \"%encryptionSwitch%\"==\"/lt\" (" +
            "\r\n   exit /b 0" +
            "\r\n)" +
            "\r\nREM 5. Sideload package - First get the name of the .msixvc file" +
            "\r\nfor /f \"delims=\" %%F in ('dir /b \"%msixvc%\\*.msixvc\"') do set \"msixvcFile=%%F\"" +
            "\r\necho Running command: %wdappPath% install \"%msixvc%\\%msixvcFile%\"" +
            "\r\ncall %wdappPath% install \"%msixvc%\\%msixvcFile%\"";

        public void OnPreprocessBuild(BuildReport report, GdkSettingsAsset.PlatformGdkSettings platformGdkSettings)
        {
            // We only care if this platform is configured to target Microsoft GDK
            if (platformGdkSettings is null || platformGdkSettings.targetMicrosoftGdk == false)
                return;

            // Exit early when targeting unsupported build architectures
            if (report.summary.platform == BuildTarget.StandaloneWindows64 || report.summary.platform == BuildTarget.StandaloneWindows)
            {
#if UNITY_EDITOR_WIN
                const string unsupportedArchitectureMessage = "Build FAILED: Unsupported GDK build architecture detected ({0}). Only Windows Intel 64-bit is supported for GDK builds.\n\n";

#if UNITY_2023_3_OR_NEWER
                var activeBuildTarget = EditorUserBuildSettings.activeBuildTarget;
                var buildTargetGroup = BuildPipeline.GetBuildTargetGroup(activeBuildTarget).ToString();
                var architecture = EditorUserBuildSettings.GetPlatformSettings(buildTargetGroup, "Architecture").ToUpper();
                
                switch (architecture)
                {
                    case "ARM64":
                        throw new BuildFailedException(string.Format(unsupportedArchitectureMessage, "Windows ARM 64-bit"));
                    case "X86":
                        throw new BuildFailedException(string.Format(unsupportedArchitectureMessage, "Windows Intel 32-bit"));
                }
#elif UNITY_2021_3_OR_NEWER
                if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows)
                {
                    throw new BuildFailedException(string.Format(unsupportedArchitectureMessage, "Windows Intel 32-bit"));
                }
                else if(EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows64
                    && PlayerSettings.GetScriptingBackend(BuildTargetGroup.Standalone) == ScriptingImplementation.IL2CPP)
                {
                    Debug.LogWarning("Build WARNING: Detected GDK build with Intel 64-bit and IL2CPP backend. Only use this build on Intel 64-bit, as ARM 64-bit is not supported for GDK builds.\n\n");
                }
#endif
#endif
            }

            // Are we tracking the Microsoft GDK Edition for this project
            GdkEditionTracker tracker = new GdkEditionTracker();
            if (tracker.TrackerFileExists())
            {
                var edition = tracker.ReadGdkEdition();
                if (edition != -1 && !MicrosoftGdkData.IsEditionAvailable(edition))
                {
                    throw new BuildFailedException($"Build FAILED: The tracked Microsoft GDK edition is not installed.\nPlease install, {edition}, select another edition or disable GDK targeting (Project Settings > Microsoft GDK > Build for Microsoft GDK).");
                }

                Debug.Log($"Building for GDK edition: {edition}");
            }
            else
            {
                // Revert to the original behaviour when there is no tracker file
                // this might happen when the user is not using the Microsoft GDK API package or it has not been updated.
                if (!MicrosoftGdkData.IsInstalled())
                {
                    throw new BuildFailedException("Build FAILED: Targeting Microsoft GDK but the 'Microsoft GDK' SDK is not installed.\nPlease install the Microsoft GDK to proceed or disable GDK targeting (Project Settings > Microsoft GDK > Build for Microsoft GDK).\n\n");
                }
            }
        }

        public void OnPostprocessBuild(BuildReport report, GdkSettingsAsset.PlatformGdkSettings platformGdkSettings)
        {
            // We only care if this platform is configured to target Microsoft GDK
            if (platformGdkSettings is null || platformGdkSettings.targetMicrosoftGdk == false)
                return;

            // A GDK build requires a Microsoft Game Config to live alongside the built executable
            // we won't be able to complete the build if we can't locate one (missing or not configured)
            string mgcAssetPath = AssetDatabase.GetAssetPath(platformGdkSettings.gameConfigAsset);
            if (string.IsNullOrEmpty(mgcAssetPath))
            {
                Debug.LogError("Build FAILED: Targeting Microsoft GDK but a Microsoft Game Config (MGC) has not been configured for this platform (See GDK Settings).");
                throw new BuildFailedException("Build failure: " + report.summary.result);
            }

            CreateManifest(report, mgcAssetPath);

            var createSolution = IsCreateSolutionSet();

            DeployGdkAssemblies(report, createSolution);

            // Package the build (if requested)
            bool usingXboxPCToolbox = false;

#if UNITY_6000_5_OR_NEWER
            var deployTarget = EditorUserBuildSettings.windowsBuildAndRunDeployTarget;
            usingXboxPCToolbox =
                deployTarget == WindowsBuildAndRunDeployTarget.XboxPcToolbox;
#endif

            bool shouldCreatePackage =
                platformGdkSettings.createPackage && !usingXboxPCToolbox;

            bool shouldSideloadPackage =
                platformGdkSettings.sideloadPackage && shouldCreatePackage;

            // Package the build (if requested)
            if (shouldCreatePackage)
            {
                // If we are creating a solution, the layout.xml is going to be created on post-build event.
                if (!createSolution)
                {
                    CreateAndConfigureLayoutFile(report);
                    
                    // Raise the event, allowing subscribers to modify the layout.xml file.
                    var outputPath = Path.GetDirectoryName(report.summary.outputPath);
                    var mappingFilePath = Path.Combine(outputPath, "layout.xml");

                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(mappingFilePath);
                    
                    GdkToolsEvents.RaiseUpdatePackageMapping(xmlDoc, outputPath, report.summary.platform);
                    
                    xmlDoc.Save(mappingFilePath);
                }

                MakePackage(report, shouldSideloadPackage, platformGdkSettings.gameConfigAsset.ContentIdOverride);
            }
            // In case we don't create a package but are making a solution, we need to inject the post-build event command.
            else if (createSolution)
            {
                InjectPostBuildCommand(report.summary.outputPath, kPostBuildEventCommand);
            }

            // Should we attempt to install and run?
            if (shouldSideloadPackage && !createSolution)
            {
                string aumid = String.Empty;
                InstallPackage(report, out aumid);
                
                if ((report.summary.options & BuildOptions.AutoRunPlayer) != 0)
                {
                    // The user selected 'Build and Run', however since we are not allowed to change the BuildOptions
                    // here launching the package here will cause the app to launch twice as the Unity editor
                    // will still go on to launch the app.

                    //LaunchPackage(report, aumid);

                    // Remove the autorun flag to prevent the editor trying to autorun the built executable
                    // NO CAN DO :( - report.summary.options &= ~report.summary.options;
                }
            }
        }

        /// <summary>
        /// Deploys GDK assemblies to the build output directory.
        /// </summary>
        /// <param name="report">The BuildReport containing information about the current build process.</param>
        /// <remarks>
        /// This method copies all .dll files from the Assets/Plugins/GDK directory to the appropriate
        /// Plugins folder in the build output. making necessary GDK assemblies are available
        /// in the built application.
        ///
        /// NOTE: Works on the assumption that the required assemblies pre-exist under Assets/Plugins/GDK
        /// folder in the project and will take ALL .DLL in this location.
        /// </remarks>
        private void DeployGdkAssemblies(BuildReport report, bool createSolution)
        {
            var assembliesSourcePath = @"Assets/Plugins/GDK";
            if (!Directory.Exists(assembliesSourcePath)) return;
                
            var nameWithoutExtension = Path.GetFileNameWithoutExtension(report.summary.outputPath);
            var outputPath = Path.GetDirectoryName(report.summary.outputPath);

            var destinations = new[]
            {
                new { Path = $"{outputPath}/{nameWithoutExtension}_Data/Plugins/x86_64", Include = createSolution == false },
                new { Path = $"{outputPath}/build/bin/{nameWithoutExtension}_Data/Plugins/x86", Include = createSolution == true },
                new { Path = $"{outputPath}/build/bin/{nameWithoutExtension}_Data/Plugins/x86_64", Include = createSolution == true },
            };

            foreach (var destination in destinations)
            {
                if (destination.Include)
                {
                    // Ensure the destination folder exists
                    if (!Directory.Exists(destination.Path))
                    {
                        Directory.CreateDirectory(destination.Path);
                    }

                    // Perform the copy - get all DLL files in the source directory
                    var files = Directory.GetFiles(assembliesSourcePath, "*.dll");

                    // Copy each file in turn to the new destination
                    foreach (var file in files)
                    {
                        string fileName = Path.GetFileName(file);
                        string destFile = Path.Combine(destination.Path, fileName);
                        File.Copy(file, destFile, true);
                    }
                }
            }
        }

        private bool IsCreateSolutionSet()
        {
            BuildTarget activeBuildTarget = EditorUserBuildSettings.activeBuildTarget;
            var buildTargetGroup = BuildPipeline.GetBuildTargetGroup(activeBuildTarget).ToString();
            return EditorUserBuildSettings.GetPlatformSettings(buildTargetGroup, "CreateSolution").ToLower() == "true";
        }

        public string GetLocalizationOutputPath(BuildReport report)
        {
            string outputDir = Path.GetDirectoryName(report.summary.outputPath);

            if (outputDir == null)
            {
                outputDir = "";
            }

            if (IsCreateSolutionSet())
            {
                // If creating a solution, the manifest and localized content will be in a separated
                // folder for them to be copied on csproj post-build event.
                return Path.Combine(outputDir, kMetadataFolderName);
            }

            return outputDir;
        }

        /// OBSOLETE Use `string GetLocalizationOutputPath(BuildReport report)`
        ///
        /// REASON: The documentation for 'EditorUserBuildSettings.GetBuildLocation' is incorrect.
        ///   EditorUserBuildSettings.GetBuildLocation() should ONLY be used to track the build location set via
        ///   the path selection UI, it does not track BuildPlayerOptions.locationPathName
        [Obsolete("Please use GetLocalizationOutputPath(BuildReport report) instead", false)]
        public string GetLocalizationOutputPath()
        {
            string outputDir = Path.GetDirectoryName(EditorUserBuildSettings.GetBuildLocation(EditorUserBuildSettings.activeBuildTarget));

            if (IsCreateSolutionSet())
            {
                // If creating a solution, the manifest and localized content will be in a separated
                // folder for them to be copied on csproj post-build event.
                return Path.Combine(outputDir, kMetadataFolderName);
            }
            else
            {
                return outputDir;
            }
        }

        public string GetStagedResourcePriPath()
        {
            // not needed for this platform
            return "";
        }

        public void SetStagedShellVisualsImageSourceDestPaths((string, string)[] stagedShellVisualsImageSourceDestPaths)
        {
            // not needed for this platform
        }

        public (string, string)[] GetStagedShellVisualsImageSourceDestPaths()
        {
            // not needed for this platform
            return null;
        }

        private bool CreateManifest(BuildReport report, string mgcAssetPath)
        {
            Game xmlGameConfig = GameConfigAsset.LoadAsXmlGameConfig(mgcAssetPath);

            // We need to make sure that the 'Executable Name' is correctly configured in the MGC is correct!
            // TODO: Not sure in what situation you would have more than one executable
            CT_ExecutableList newExecutableList = new CT_ExecutableList();
            CT_ExecutableListExecutable newExecutable = new CT_ExecutableListExecutable();

            var executableName = Path.GetFileName(report.summary.outputPath);

            newExecutable.Name = $"{executableName}";
            newExecutable.Id = "Game";

            newExecutableList.Executable = new[] { newExecutable };

            xmlGameConfig.ExecutableList = newExecutableList;

            // Add "VC14" as a "KnownDependency" to avoid the "ILI_HasMissingDependency" submission validation error
            AddVc14DependencyToManifest(ref xmlGameConfig);

            // Save the updated MGC but don't touch the original asset (for now anyways)
            if (IsCreateSolutionSet())
            {
                // If creating a solution, the manifest will be in a separated
                // folder for them to be copied on vcxproj post-build event.
                GameConfigAsset.SaveXmlGameConfig(Path.Combine(
                    Path.GetDirectoryName(report.summary.outputPath), kMetadataFolderName, "MicrosoftGame.config"), xmlGameConfig);
            }
            else
            {
                GameConfigAsset.SaveXmlGameConfig(
                    Path.Combine(Path.GetDirectoryName(report.summary.outputPath), "MicrosoftGame.config"), xmlGameConfig);
            }

            return true;
        }

        private static void AddVc14DependencyToManifest(ref Game xmlGameConfig)
        {
            // Ensure DesktopRegistration exists
            if (xmlGameConfig.DesktopRegistration == null)
            {
                xmlGameConfig.DesktopRegistration = new CT_DesktopRegistration();
            }

            // Ensure DependencyList exists
            if (xmlGameConfig.DesktopRegistration.DependencyList == null)
            {
                xmlGameConfig.DesktopRegistration.DependencyList = new CT_DependencyList[] { new CT_DependencyList() };
            }

            // Check if VC14 already exists in any DependencyList
            foreach (var dependencyList in xmlGameConfig.DesktopRegistration.DependencyList)
            {
                // Ensure KnownDependency array exists
                if (dependencyList.KnownDependency == null)
                {
                    dependencyList.KnownDependency = Array.Empty<CT_DependencyListKnownDependency>();
                }

                // If VC14 already exists, no need to add
                if (dependencyList.KnownDependency.Any(kd => kd.Name == ST_KnownDependency.VC14))
                {
                    return;
                }
            }

            // Since VC14 doesn't exist, we add it to the first DependencyList
            var firstDependencyList = xmlGameConfig.DesktopRegistration.DependencyList[0];
            var knownDependencies = firstDependencyList.KnownDependency.ToList();
            knownDependencies.Add(new CT_DependencyListKnownDependency { Name = ST_KnownDependency.VC14 });
            firstDependencyList.KnownDependency = knownDependencies.ToArray();
        }

        private bool CreateAndConfigureLayoutFile(BuildReport report)
        {
            string outputPath = Path.GetDirectoryName(report.summary.outputPath);

            int exitCode = 0;
            string outMessage = ExecuteMakePkgProcess(
                out exitCode,
                string.Format("genmap /f \"{0}\\layout.xml\" /d \"{0}\"", outputPath),
                "",
                LogOpts.kNoOutput | LogOpts.kNoCommand);

            if (exitCode != 0)
                throw new BuildFailedException($"FAILED - 'makepkg.exe genmap ...' (error code: {exitCode}):\n\n{outMessage}");

            Debug.Log($"'makepkg.exe genmap' completed without errors.\n\n{outMessage}");

            string layoutPath = outputPath + @"\layout.xml";

            // makepkg genmap can produce invalid XML when paths contain '&' (e.g. "Fonts & Materials").
            // Sanitize bare '&' characters before parsing.
            string layoutXml = File.ReadAllText(layoutPath);
            layoutXml = System.Text.RegularExpressions.Regex.Replace(layoutXml, @"&(?!amp;|lt;|gt;|quot;|apos;|#)", "&amp;");
            var doc = XDocument.Parse(layoutXml);
            var ignoredNodes = (from node in doc.Descendants()
                                where node.Name == "FileGroup" &&
                                      (node.Attribute("SourcePath").Value.Contains("DontShipItWithYourGame") ||
                                       node.Attribute("SourcePath").Value.Contains("DoNotShip"))
                                select node).ToArray();

            for (int i = 0; i < ignoredNodes.Length; ++i)
            {
                ignoredNodes[i].Remove();
            }

            doc.Save(layoutPath);

            return true;
        }

        private string EncryptionMethod(bool sideloadPackage)
        {
            // Test Signing: for sideloading/testing supports PC
            if (sideloadPackage)
                return "/lt";

            // Stable-Key: when we have a user supplied key file
            if (!string.IsNullOrEmpty(MakePkgStableKeyConfig.PackageStableKey))
                return $"/lk {MakePkgStableKeyConfig.PackageStableKey}";

            // Random Key: otherwise!
            return "/l";
        }

        private bool MakePackage(BuildReport report, bool sideloadPackage, string contentIdOverride)
        {
            string outputPath = Path.GetDirectoryName(report.summary.outputPath);
            string outputPathMsixvc = Path.Combine(outputPath, "MSIXVC");

            if (!Directory.Exists(outputPathMsixvc))
                Directory.CreateDirectory(outputPathMsixvc);

            string makePkgExtraArgs = string.Empty;

            if (!string.IsNullOrEmpty(contentIdOverride))
                makePkgExtraArgs = " /contentid " + contentIdOverride;

            // Always pass /updatesubval when the installed GDK supports it (October 2025+).
            bool shouldUpdateSubVal = false;
            {
                int trackedEdition = new GdkEditionTracker().ReadGdkEdition();
                if (trackedEdition >= 251000) // GDK October 2025
                {
                    makePkgExtraArgs += " /updatesubval";
                    shouldUpdateSubVal = true;
                }
            }

            // Copy the existing SubmissionValidator.dll from the GDK \bin directory to the build
            // output so makepkg has a fallback if the machine is offline and cannot download an update.
            if (shouldUpdateSubVal)
            {
                string srcDll = Path.Combine(GdkUtility.GdkToolsPath, "SubmissionValidator.dll");
                string dstDll = Path.Combine(outputPath, "SubmissionValidator.dll");
                if (File.Exists(srcDll))
                    File.Copy(srcDll, dstDll, true);
                else
                    Debug.LogWarning($"Could not find SubmissionValidator.dll at '{srcDll}' to seed the build output directory.");
            }

            int exitCode = 0;
            string outMessage;

            // If create VSS is enabled, generate a .bat file to run gdk commands.
            if (IsCreateSolutionSet())
            {
                // Point /validationpath at the build output so makepkg downloads the updated
                // SubmissionValidator.dll there instead of the GDK \bin directory (which requires admin rights).
                string batExtraArgs = makePkgExtraArgs;
                string batPrePackCommands = "";
                if (shouldUpdateSubVal)
                {
                    batExtraArgs += " /validationpath \"%binariesPath%\"";
                    // Copy the existing DLL from the GDK so makepkg has a fallback when offline.
                    batPrePackCommands =
                        "\r\nREM Copy existing SubmissionValidator.dll to build output as offline fallback" +
                        "\r\nIF EXIST \"%gdkPath%bin\\SubmissionValidator.dll\" (" +
                        "\r\n   copy /Y \"%gdkPath%bin\\SubmissionValidator.dll\" \"%binariesPath%\\SubmissionValidator.dll\" >nul" +
                        "\r\n)";
                }

                outMessage = WriteMakePkgProcessToBatFile(
                    outputPath,
                    batExtraArgs,
                    sideloadPackage,
                    batPrePackCommands);

                if (exitCode != 0)
                    throw new BuildFailedException($"FAILED - {kMakePkgBatFileName} generation (error code: {exitCode}):\n\n{outMessage}");

                // Inject a post-build command to the generated vcxproj to copy metadata to the binaries and launch the bat file.
                InjectPostBuildCommand(report.summary.outputPath, kPostBuildEventCommand + kRunBatCommand);

                Debug.Log($"{kMakePkgBatFileName} generation and .vcxproj post-build event commands injection are successful.");
            }
            else
            {
                // Point /validationpath at the build output so makepkg downloads the updated
                // SubmissionValidator.dll there instead of the GDK \bin directory (which requires admin rights).
                string directExtraArgs = makePkgExtraArgs;
                if (shouldUpdateSubVal)
                    directExtraArgs += $" /validationpath \"{outputPath}\"";

                outMessage = ExecuteMakePkgProcess(
                    out exitCode,
                    string.Format(
                        "pack /f \"{0}\\layout.xml\" /d \"{0}\" /pd \"{1}\" /pc /CorrelationId Unity {2}" + directExtraArgs, outputPath, outputPathMsixvc, EncryptionMethod(sideloadPackage)),
                    "",
                    LogOpts.kNoOutput | LogOpts.kNoCommand);

                if (exitCode != 0)
                    throw new BuildFailedException($"FAILED - 'makepkg.exe pack ...' (error code: {exitCode}) - See log below:\n\n{outMessage}");

                Debug.Log($"'makepkg.exe pack' completed without errors.\n\n{outMessage}");
            }

            return true;
        }

        private static bool InstallPackage(BuildReport report, out string aumid)
        {
            string outputPath = Path.GetDirectoryName(report.summary.outputPath);
            string outputPathMsixvc = Path.Combine(outputPath, "MSIXVC");

            if (!Directory.Exists(outputPathMsixvc))
                throw new BuildFailedException($"FAILED - can not find package to install");

            string[] files = Directory.GetFiles(outputPathMsixvc, "*.msixvc");
            if (files.Length == 0)
                throw new BuildFailedException($"FAILED - '.msixvc' not found");

            string msixvcFilePath = files[0];
            if (files.Length > 1)
                Debug.Log($"FAILED - More than one '.msixvc' found. Installing this one: {files[0]}");

            // We always try to uninstall an old package before installing a new one to avoid
            // cases where an old install is not fully cleaned up.
            string pFN = Path.GetFileNameWithoutExtension(msixvcFilePath);

            int exitCode = 0;
            string outMessage = ExecuteWdappProcess(
                out exitCode,
                "list",
                "",
                LogOpts.kNoOutput | LogOpts.kNoCommand,
                true);

            if (exitCode != 0)
                throw new BuildFailedException($"FAILED - 'Wdapp.exe list' (error code: {exitCode}):\n\n{outMessage}");

            if (outMessage.Contains(pFN))
            {
                outMessage = ExecuteWdappProcess(
                    out exitCode,
                    $"uninstall {pFN}",
                    "",
                    LogOpts.kNoOutput | LogOpts.kNoCommand);

                if (exitCode != 0)
                    throw new BuildFailedException($"FAILED - 'Wdapp.exe uninstall' (error code: {exitCode}):\n\n{outMessage}");
            }

            outMessage = ExecuteWdappProcess(
                out exitCode,
                $"install \"{msixvcFilePath}\"",
                "",
                LogOpts.kNoOutput | LogOpts.kNoCommand);

            if (exitCode != 0)
                throw new BuildFailedException($"FAILED - 'Wdapp.exe install' (error code: {exitCode}):\n\n{outMessage}");

            Debug.Log($"Package installation successful - See log below:\n\n{outMessage}");

            string pattern = @"^(.*?)!Game";
            Match match = Regex.Match(outMessage, pattern, RegexOptions.Multiline);
            if (match.Success)
            {
                aumid = match.Groups[0].Value.Trim();
            }
            else
                throw new BuildFailedException($"FAILED - 'Wdapp.exe install', could not extract aumid (error code: {exitCode}):\n\n{outMessage}");

            return true;
        }

        private static bool LaunchPackage(BuildReport report, string aumid)
        {
            int exitCode = 0;
            string outMessage = ExecuteWdappProcess(
                out exitCode,
                $"launch {aumid}",
                "",
                LogOpts.kNoOutput | LogOpts.kNoCommand);

            if (exitCode != 0)
                throw new BuildFailedException($"FAILED - 'Wdapp.exe launch' (error code: {exitCode}):\n\n{outMessage}");

            return true;
        }

        internal static string ExecuteMakePkgProcess(
            out int exitCode,
            string args = "",
            string workingdir = "",
            LogOpts logOpts = LogOpts.kLogToFileAndConsole,
            bool forceAdministrator = false)
        {
            string gdkPath = Unity.Microsoft.GDK.Tools.Editor.GdkUtility.GdkToolsPath; // Expect something like: c:\Program Files (x86)\Microsoft GDK\bin
            string makePkgCommand = Path.Combine(gdkPath, "makepkg.exe");

            if (!File.Exists(makePkgCommand))
            {
                exitCode = -1;
                return "Failed: Could not locate 'makepkg.exe'. Is the Microsoft GDK installed?";
            }

            ExecuteSystemProcess esp = new ExecuteSystemProcess(
                makePkgCommand,
                args,
                workingdir,
                logOpts,
                forceAdministrator,
                0);

            exitCode = esp.exitCode;

            // Construct a more user friendly message. Note program.cs ProcessOutputStreamReader() adds a null terminator
            // after every character in esp.output for some reason. Possibly only related to calling makepri.exe.
            string message = "Working Directory: " + workingdir + "\nCommand Line: " + makePkgCommand + " " + args + "\n\n" + esp.output.Replace("\0", "");

            return message;
        }

        /// <summary>
        /// Writes a bat file to disk that will run makepkg.exe with the provided arguments.
        /// TO DO: sideloadPackage need to be turned into an enum for encryption mode.
        /// </summary>
        /// <param name="outputPath"></param>
        /// <param name="extraArgs"></param>
        /// <param name="sideloadPackage"></param>
        /// <returns></returns>
        private string WriteMakePkgProcessToBatFile(
            string outputPath,
            string extraArgs,
            bool sideloadPackage,
            string prePackCommands = "")
        {
            string gdkPath = GdkUtility.GdkToolsPath; // Expect something like: c:\Program Files (x86)\Microsoft GDK\bin
            string makePkgCommand = Path.Combine(gdkPath, "makepkg.exe");

            string batFilePath = Path.Combine(outputPath, kMakePkgBatFileName);

            string message = "";
            if (!File.Exists(makePkgCommand))
            {
                message = "Failed: Could not locate 'makepkg.exe'. Is the Microsoft GDK installed?";
            }
            else
            {
                // Creating the bat file.
                // TODO: Add all encryption switches (/l, /lt, /lk).
                // https://developer.microsoft.com/en-us/games/xbox/docs/gdk/title-packaging-streaming-install-testing#encryption
                string batContent = string.Format(kBatFileTemplate, extraArgs, EncryptionMethod(sideloadPackage), prePackCommands);

                // Write the .bat file.
                try
                {
                    File.WriteAllText(batFilePath, batContent);
                }
                catch (Exception ex)
                {
                    message += $"Failed to write to {kMakePkgBatFileName}. " + ex.Message;
                }
            }

            return message;
        }

        internal static string ExecuteWdappProcess(
            out int exitCode,
            string args = "",
            string workingdir = "",
            LogOpts logOpts = LogOpts.kLogToFileAndConsole,
            bool forceAdministrator = false)
        {
            string gdkPath = GdkUtility.GdkToolsPath; // Expect something like: c:\Program Files (x86)\Microsoft GDK\bin
            string makeWdappCommand = Path.Combine(gdkPath, "wdapp.exe");

            if (!File.Exists(makeWdappCommand))
            {
                exitCode = -1;
                return "Failed: Could not locate 'Wdapp.exe'. Is the Microsoft GDK installed?";
            }

            ExecuteSystemProcess esp = new ExecuteSystemProcess(
                makeWdappCommand,
                args,
                workingdir,
                logOpts,
                forceAdministrator,
                0);

            exitCode = esp.exitCode;

            // Construct a more user friendly message. Note program.cs ProcessOutputStreamReader() adds a null terminator
            // after every character in esp.output for some reason. Possibly only related to calling wdapp.exe.
            string message = "Working Directory: " + workingdir + "\nCommand Line: " + makeWdappCommand + " " + args + "\n\n" + esp.output.Replace("\0", "");

            return message;
        }

        private static void InjectPostBuildCommand(string ouputPath, string command)
        {
            string projectFilePath = Path.Combine(Path.GetDirectoryName(ouputPath),
                PlayerSettings.productName, PlayerSettings.productName) + ".vcxproj";
            if (!File.Exists(projectFilePath))
            {
                Debug.LogError(projectFilePath + " not found.");
                return;
            }

            var projectXml = new XmlDocument();
            projectXml.Load(projectFilePath);

            // Get the namespace from the xmlns attribute of the Project node.
            string projectNamespace = projectXml.DocumentElement.GetAttribute("xmlns");
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(projectXml.NameTable);
            nsmgr.AddNamespace("ns", projectNamespace);

            // Get the first ItemDefinitionGroups (affects all configurations).
            var defaultItemDefinition = projectXml.SelectSingleNode("//ns:ItemDefinitionGroup", nsmgr);
            if (defaultItemDefinition != null)
            {
                // Check if the makepkg post-build event node already exists.
                XmlNode postBuildEventNode = defaultItemDefinition.SelectSingleNode("PostBuildEvent[@makepkg]");
                if (postBuildEventNode == null)
                {
                    // Create the post-build event section with the 'makepkg' attribute.
                    postBuildEventNode = projectXml.CreateElement("PostBuildEvent");
                    XmlAttribute attribute = projectXml.CreateAttribute("makepkg");
                    postBuildEventNode.Attributes.SetNamedItem(attribute);
                    defaultItemDefinition.AppendChild(postBuildEventNode);

                    // Add the post-build event command
                    var commandNode = projectXml.CreateElement("Command");
                    commandNode.InnerText = command;
                    postBuildEventNode.AppendChild(commandNode);
                }
            }

            projectXml.Save(projectFilePath);
        }
    }
}