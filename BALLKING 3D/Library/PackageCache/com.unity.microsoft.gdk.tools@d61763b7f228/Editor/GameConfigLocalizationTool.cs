using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

using XmlGameConfig = Unity.Microsoft.GDK.Game;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    internal class GameConfigLocalizationTool
    {
        private const string k_defaultPublisherDisplayName = @"ms-resource:PublisherDisplayName";
        private const string k_defaultDisplayName = @"ms-resource:PackageTitle";
        private const string k_defaultDescription = @"ms-resource:PackageDescription";
        private const string k_ProjectMetadataPath = "Packages/com.unity.microsoft.gdk.tools/ProjectMetadata";
        private const string k_Square44x44Logo = "Square44x44Logo.png";
        private const string k_Square150x150Logo = "Square150x150Logo.png";
        private const string k_Square480x480Logo = "Square480x480Logo.png";
        private const string k_StoreLogo = "StoreLogo.png";
        private const string k_SplashScreenImage = "SplashScreenImage.png";

        private static readonly string[] kContentImages = { k_Square44x44Logo, k_Square480x480Logo, k_StoreLogo };
        private static readonly string[] kApplicationImages = { k_Square44x44Logo, k_Square150x150Logo, k_Square480x480Logo, k_StoreLogo, k_SplashScreenImage };

        private GameConfigAsset m_GameConfigAsset;
        private GameConfigImporter m_GameConfigAssetImporter;
        private XmlGameConfig m_XmlGameConfig;
        private Dictionary<string, Tuple<Func<string>, Action<string>>> m_shellVisualsPropertyMap;
        private string m_GameConfigAssetPath;

        // We copy over different images if we are working with a content package verses a normal project.
        private bool m_IsContentPackage;

        internal GameConfigLocalizationTool(GameConfigAsset gameConfigAsset, bool isContentPackage = false)
        {
            m_GameConfigAsset = gameConfigAsset;
            m_GameConfigAssetPath = AssetDatabase.GetAssetPath(gameConfigAsset);
            m_GameConfigAssetImporter = AssetImporter.GetAtPath(m_GameConfigAssetPath) as GameConfigImporter;
            m_XmlGameConfig = GameConfigAsset.LoadAsXmlGameConfig(m_GameConfigAssetPath);
            m_IsContentPackage = isContentPackage;
            m_shellVisualsPropertyMap = null;
            InitializeShellVisualMGCResources();
        }

        private void InitializeShellVisualMGCResources(bool createIfNull = false)
        {
            if (m_XmlGameConfig.ShellVisuals is null)
            {
                if (createIfNull)
                {
                    CT_ShellVisuals newShellVisuals = new CT_ShellVisuals();
                    m_XmlGameConfig.ShellVisuals = newShellVisuals;
                    m_XmlGameConfig.ShellVisuals.Square44x44Logo = string.Empty;
                    m_XmlGameConfig.ShellVisuals.Square150x150Logo = string.Empty;
                    m_XmlGameConfig.ShellVisuals.Square480x480Logo = string.Empty;
                    m_XmlGameConfig.ShellVisuals.StoreLogo = string.Empty;
                    m_XmlGameConfig.ShellVisuals.SplashScreenImage = string.Empty;
                }
                else
                {
                    // No ShellVisuals in the XML, so we won't create it except if user ask for it.
                    return;
                }
            }

            if (m_shellVisualsPropertyMap != null)
            {
                return;
            }

            m_shellVisualsPropertyMap = new Dictionary<string, Tuple<Func<string>, Action<string>>>()
            {
                {
                    k_Square44x44Logo, new Tuple<Func<string>, Action<string>>(
                        () => m_XmlGameConfig.ShellVisuals.Square44x44Logo,
                        (value) => m_XmlGameConfig.ShellVisuals.Square44x44Logo = value)
                },
                {
                    k_Square150x150Logo, new Tuple<Func<string>, Action<string>>(
                        () => m_XmlGameConfig.ShellVisuals.Square150x150Logo,
                        (value) => m_XmlGameConfig.ShellVisuals.Square150x150Logo = value)
                },
                {
                    k_Square480x480Logo, new Tuple<Func<string>, Action<string>>(
                        () => m_XmlGameConfig.ShellVisuals.Square480x480Logo,
                        (value) => m_XmlGameConfig.ShellVisuals.Square480x480Logo = value)
                },
                {
                    k_StoreLogo, new Tuple<Func<string>, Action<string>>(
                        () => m_XmlGameConfig.ShellVisuals.StoreLogo,
                        (value) => m_XmlGameConfig.ShellVisuals.StoreLogo = value)
                },
                {
                    k_SplashScreenImage, new Tuple<Func<string>, Action<string>>(
                        () => m_XmlGameConfig.ShellVisuals.SplashScreenImage,
                        (value) => m_XmlGameConfig.ShellVisuals.SplashScreenImage = value)
                }
            };
        }

        public string GetShellVisualsResourcePath()
        {
            if (m_GameConfigAssetImporter == null ||
                String.IsNullOrEmpty(m_GameConfigAssetImporter.ShellVisualsResourcePath))
            {
                return NormalizePath(Path.GetDirectoryName(m_GameConfigAssetPath));
            }

            return NormalizePath(Path.Combine(Path.GetDirectoryName(Application.dataPath), m_GameConfigAssetImporter.ShellVisualsResourcePath));
        }

        public string GetResourcesPriPath()
        {
            if (m_GameConfigAssetImporter == null ||
                String.IsNullOrEmpty(m_GameConfigAssetImporter.BuiltResourcesPriPath))
            {
                return NormalizePath(Path.Combine(Path.GetDirectoryName(m_GameConfigAssetPath), "Resources.pri"));
            }

            return NormalizePath(Path.Combine(Path.GetDirectoryName(Application.dataPath), m_GameConfigAssetImporter.BuiltResourcesPriPath, "Resources.pri"));
        }

        public string GetPriConfigPath()
        {
            var shellVisualsResourcePath = GetShellVisualsResourcePath();
            return NormalizePath(Path.Combine(shellVisualsResourcePath, "Priconfig.xml"));
        }

        public bool ShellVisualsResourcesPathContainsPriConfig()
        {
            return File.Exists(GetPriConfigPath());
        }

        private delegate void SelectedLanguageCodeBlock(String langTag, GameConfigAsset gameConfigAsset);

        private void ForEach_SelectedLanguage(SelectedLanguageCodeBlock codeBlock)
        {
            foreach (GameConfigLanguages lang in Enum.GetValues(typeof(GameConfigLanguages)))
            {
                if (m_GameConfigAsset.IsLanguagePairSet(lang))
                {
                    string langTag = Enum.GetName(typeof(GameConfigLanguages), lang).Replace("_", "-");
                    codeBlock(langTag, m_GameConfigAsset);
                }
            }
        }

        public void InitializeResources()
        {
            var shellVisualsResourcePath = GetShellVisualsResourcePath();

            System.IO.Directory.CreateDirectory(shellVisualsResourcePath);

            // Has en-US been configured this is generally a default fall back
            bool enUSSetting = m_GameConfigAsset.IsLanguagePairSet(GameConfigLanguages.en_US);
            if (enUSSetting == false)
            {
                if (EditorUtility.DisplayDialog(
                        "Building .MGC Shell Visuals and Localized Resources",
                        "The 'en-US' language pair has not been configured.\n" +
                        "'en-US' is normally configured as the default language, if this is intentional then " +
                        "the 'priconfig.xml' will need to be changed manually.\n" +
                        "Would you like to add 'en-US' now to avoid a broken configuration?",
                        "Yes",
                        "No") == true)
                {
                    m_GameConfigAsset.SetLanguagePair(GameConfigLanguages.en_US, true);
                    m_GameConfigAsset.SaveXmlGameConfig(m_GameConfigAssetPath);
                    m_XmlGameConfig = GameConfigAsset.LoadAsXmlGameConfig(m_GameConfigAssetPath);
                }
            }

            // Based on player language settings copy over the starter Resources.resw for each language
            //  as this may be a re-run we do not overwrite pre-existing files.
            string resourcesReswTemplate = File.ReadAllText($"{k_ProjectMetadataPath}/Resources.resw");

            // The default resources go in the root (don't overwrite a pre-existing file).
            if (!File.Exists($"{shellVisualsResourcePath}/Resources.resw"))
            {
                string fixedupResourceResw = resourcesReswTemplate.Replace("{{LANG_PAIR}}", "en-US");
                File.WriteAllText($"{shellVisualsResourcePath}/Resources.resw", fixedupResourceResw);
            }

            // The config used by makepri to build this project.
            CreatePriConfigFile(GetPriConfigPath());

            ImportDefaultShellVisualsResources(shellVisualsResourcePath);

            List<string> newLanguagePath = null;

            // Go over each selected language and create a Resources.resw file for it if needed.
            // If the language is new, we will ask user if they want to copy default resources to the lang folder.
            ForEach_SelectedLanguage((langTag, settings) =>
            {
                string lang_path = NormalizePath(Path.Combine(shellVisualsResourcePath, langTag));

                // MakePri does not seem to like this. When dumping the generated resources this causes something
                //  called a 'subtree'... basically the languages don't work as expected when deployed :(
                //string destPath = lang_path + $"/Resources.{langTag}.resw";
                string destPath = lang_path + $"/Resources.resw";

                string fixedupResourceResw = resourcesReswTemplate.Replace("{{LANG_PAIR}}", langTag);

                bool exists = System.IO.Directory.Exists(lang_path);

                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(lang_path);

                    // If a new language support is added, we are going to ask user if they want
                    // to import placeholder resources.
                    if (newLanguagePath == null)
                    {
                        newLanguagePath = new List<string>();
                    }
                    newLanguagePath.Add(lang_path);
                }

                // Don't overwrite a pre-existing file
                if (!File.Exists(destPath))
                {
                    File.WriteAllText(destPath, fixedupResourceResw);
                }
            });

            if (newLanguagePath != null)
            {
                if (EditorUtility.DisplayDialog($"Shell Visuals resources: New language(s) support detected",
                    $"Import default images for new language pairs?",
                    "Yes", "No"))
                {
                    foreach (string lang_path in newLanguagePath)
                    {
                        ImportLanguagePairShellVisualsResources(shellVisualsResourcePath, lang_path);
                    }
                }
            }

            // The res files rely on files created/copied above.
            CreateResFiles(shellVisualsResourcePath);

            GameConfigAsset.SaveXmlGameConfig(m_GameConfigAssetPath, m_XmlGameConfig);

            AssetDatabase.Refresh();
        }

        public int BuildResources(out string outMessage)
        {
            var shellVisualsResourcePath = GetShellVisualsResourcePath();
            var builtResourcesPriPath = GetResourcesPriPath();
            var priConfigPath = GetPriConfigPath();

            string gameConfigAssetPath = AssetDatabase.GetAssetPath(m_GameConfigAsset);
            string manifestPath = NormalizePath(gameConfigAssetPath);
            bool manifestExists = false; // File.Exists(manifestPath); -- for now we will always assume we don't have access to the manifest file

            int exitCode = 0;

            if (Directory.Exists(shellVisualsResourcePath))
            {
                string commandLine = "new /o /pr \"" + shellVisualsResourcePath + "\" /cf \"" + priConfigPath + "\" /of \"" + builtResourcesPriPath + "\"";

                outMessage = ExecuteMakePriProcess(
                    out exitCode,
                    commandLine + (manifestExists ? (" /mn \"" + manifestPath) + "\"" : " /in MicrosoftGame.Config"),
                    shellVisualsResourcePath,
                    LogOpts.kNoOutput | LogOpts.kNoCommand);

                if (exitCode == 0)
                {
                    outMessage = "";
                    Debug.Log("makepri.exe completed successfully");
                }
            }
            else
            {
                outMessage = $"Failed to build Resources.pri. Resources path: {shellVisualsResourcePath} does not exist. Have the resources been generated?\n";
                exitCode = -1;
            }

            return exitCode;
        }

        public bool ValidateLocalizationResources(out string validationErrors)
        {
            bool success = true;
            // If no Shell Visual defined in the XML, we ignore the check.
            if (m_shellVisualsPropertyMap == null)
            {
                validationErrors = "Warning: Shell Visuals resources not configured.";
                return true;
            }

            StringBuilder stringBuilder = new StringBuilder();
            string shellVisualsResourcePath = GetShellVisualsResourcePath();
            string resourcesPriPath = GetResourcesPriPath();
            string priConfigPath = GetPriConfigPath();

            if (!Directory.Exists(shellVisualsResourcePath))
            {
                stringBuilder.AppendLine($"Error: Shell Visuals Resource not found at path \"<i>{shellVisualsResourcePath}</i>\".");
                success = false;
            }

            if (!File.Exists(priConfigPath))
            {
                stringBuilder.AppendLine($"Error: Pri Config file not found at path \"<i>{priConfigPath}</i>\".");
                success = false;
            }

            if (!File.Exists(resourcesPriPath))
            {
                var priPath = resourcesPriPath.Replace(Path.GetDirectoryName(Application.dataPath) + Path.DirectorySeparatorChar, "");
                stringBuilder.AppendLine($"Warning: Resources.pri file not found at path \"<i>{priPath}</i>\". " +
                    $"It can be generated at build time using GDK Settings' Shell Visuals Mode <b>'Run Make Pri And Copy'</b> " +
                    $"or by using the <b>'Build Shell Visual Resources (Resources.pri)'</b> button on the Game Config Asset.");
            }

            foreach (var elementRef in m_shellVisualsPropertyMap)
            {
                if (elementRef.Value.Item1() == null)
                {
                    continue;
                }

                string imageResourcePath = Path.Combine(shellVisualsResourcePath, elementRef.Value.Item1());
                if (!File.Exists(imageResourcePath))
                {
                    var elementPath = imageResourcePath.Replace(Path.GetDirectoryName(Application.dataPath) + Path.DirectorySeparatorChar, "");
                    stringBuilder.AppendLine($"Error: Shell Visual '{elementRef.Key.Replace(".png", "")}' not found at path \"<i>{elementPath}</i>\". " +
                        $"You can generate a default resource by using the <b>'Update Shell Visual Resources'</b> button on the Game Config Asset.");
                    success = false;
                }
            }

            validationErrors = stringBuilder.ToString();
            return success;
        }

        private void ImportDefaultShellVisualsResources(string parentPath)
        {
            InitializeShellVisualMGCResources(true);

            string assetPath = parentPath;

            string[] images = m_IsContentPackage ? kContentImages : kApplicationImages;

            foreach (string image in images)
            {
                // We dont want to create new images if user already provides them.
                if (m_shellVisualsPropertyMap.TryGetValue(image, out var elementRef) &&
                    !string.IsNullOrEmpty(elementRef.Item1()) && 
                    File.Exists(Path.Combine(assetPath, elementRef.Item1())))
                {
                    continue;
                }

                // Originally we were creating the image and then asking the user if they wanted to update the Game.Config.
                // But both image generation and updating the Game.Config should be done together.
                // This way, if user don't want to generate the image, we don't need to update the Game.Config and user can
                // fix the MGC first before trying to update the resources again.
                bool updateGameConfig = EditorUtility.DisplayDialog(
                    $"Invalid ShellVisuals element '{image.Replace(".png", "")}'.",
                    $"Do you want to generate a default '{image}' and overwrite the element in the Microsoft Game Configuration?", "Yes", "No");

                if (!updateGameConfig)
                {
                    continue;
                }

                string iimage = $"{k_ProjectMetadataPath}/{image}";

                // Copy the default image to the same location
                string oimage = Path.Combine(assetPath, image);

                // Only regenerate if the file doesn't already exist in this location
                if (!File.Exists(oimage))
                    File.Copy(iimage, oimage);

                if (elementRef.Item1() != image)
                {
                    elementRef.Item2(image);
                }
            }

            string message = $"Do you want to use the default localized resource string and overwrite the element in the Microsoft Game Configuration?";

            // Update config shell visuals to use localized resource strings
            if (string.IsNullOrEmpty(m_XmlGameConfig.ShellVisuals.DefaultDisplayName)
                && EditorUtility.DisplayDialog($"Shell Visuals \"DefaultDisplayName\" element is empty.",
                        message, "Yes", "No"))
            {
                m_XmlGameConfig.ShellVisuals.DefaultDisplayName = k_defaultDisplayName;
            }
            if (string.IsNullOrEmpty(m_XmlGameConfig.ShellVisuals.Description)
                && EditorUtility.DisplayDialog($"Shell Visuals \"Description\" element is empty.",
                    message, "Yes", "No"))
            {
                m_XmlGameConfig.ShellVisuals.Description = k_defaultDescription;
            }
            if (string.IsNullOrEmpty(m_XmlGameConfig.ShellVisuals.PublisherDisplayName)
                && EditorUtility.DisplayDialog($"Shell Visuals \"PublisherDisplayName\" element is empty.",
                    message, "Yes", "No"))
            {
                m_XmlGameConfig.ShellVisuals.PublisherDisplayName = k_defaultPublisherDisplayName;
            }
        }

        private void ImportLanguagePairShellVisualsResources(string shellVisualPath, string langPath)
        {
            InitializeShellVisualMGCResources(true);

            string assetPath = langPath;

            string[] images = m_IsContentPackage ? kContentImages : kApplicationImages;

            foreach (string image in images)
            {
                string iimage, opath;

                // Try to use default shell visual, otherwise draw from package metadata
                if (m_shellVisualsPropertyMap.TryGetValue(image, out var elementRef)                                                                                                                                             
                   && !string.IsNullOrEmpty(elementRef.Item1()))
                {
                    iimage = Path.Combine(shellVisualPath, elementRef.Item1());
                    opath = Path.Combine(langPath, elementRef.Item1());
                }
                else
                {
                    iimage = Path.Combine(k_ProjectMetadataPath, image);
                    opath = Path.Combine(langPath, image);
                }

                // Only regenerate if the file doesn't already exist in this location
                if (!File.Exists(opath))
                    File.Copy(iimage, opath);
            }
        }

        private void CreateResFiles(string apath)
        {
            // Layout.resfiles - reflects the final layout in the deployable folder
            //  recreate this every time initialize is called based on player settings - languages
            string layout = Path.Combine(apath, "Layout.resfiles");

            if (File.Exists(layout))
            {
                File.Delete(layout);
            }

            //string root = GameConfigLocalization.localizationImagesFolder + "\\";
            string root = "";

            using (StreamWriter sw = new StreamWriter(layout))
            {
                ForEach_SelectedLanguage((langTag, settings) =>
                {
                    foreach (string image in Directory.GetFiles(Path.Combine(apath, langTag), "*.png"))
                    {
                        string imageName = Path.GetFileName(image);
                        sw.WriteLine(root + langTag + "\\" + imageName);
                    }
                });

                foreach (string image in Directory.GetFiles(apath, "*.png"))
                {
                    string imageName = Path.GetFileName(image);
                    sw.WriteLine(root + imageName);
                }
            }

            // Pri.resfiles - a zero byte file that does not apply to localizing DLC, but is a required input to makepri.exe
            string prires = Path.Combine(apath, "Pri.resfiles");
            if (!File.Exists(prires))
            {
                using (StreamWriter sw = new StreamWriter(prires)) { }
            }

            // Resources.resfiles - Always recreate this, this tells the system which languages should be included.
            string resres = Path.Combine(apath, "Resources.resfiles");
            if (File.Exists(resres))
            {
                File.Delete(resres);
            }

            using (StreamWriter sw = new StreamWriter(resres))
            {
                ForEach_SelectedLanguage((langTag, settings) =>
                {
                    // This seems to stop the resources building correctly :(
                    //sw.WriteLine(langTag + $"\\Resources.{langTag}.resw");
                    sw.WriteLine(langTag + $"\\Resources.resw");
                });

                sw.WriteLine("Resources.resw");
            }
        }

        private void CreatePriConfigFile(string path)
        {
            if (File.Exists(path))
                return;

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                sw.WriteLine("<resources>");
                sw.WriteLine("  <index root=\"\\\" startIndexAt=\"layout.resfiles\">");
                sw.WriteLine("    <default>");
                sw.WriteLine("      <qualifier name=\"language\" value=\"en-US\" />");
                sw.WriteLine("    </default>");
                sw.WriteLine("    <indexer-config type=\"RESFILES\" qualifierDelimiter=\".\" />");
                sw.WriteLine("  </index>");
                sw.WriteLine("  <index root=\"\\\" startIndexAt=\"resources.resfiles\">");
                sw.WriteLine("    <default>");
                sw.WriteLine("      <qualifier name=\"language\" value=\"en-US\" />");
                sw.WriteLine("    </default>");
                sw.WriteLine("    <indexer-config type=\"RESW\" convertDotsToSlashes=\"true\" />");
                sw.WriteLine("    <indexer-config type=\"RESJSON\" />");
                sw.WriteLine("    <indexer-config type=\"RESFILES\" qualifierDelimiter=\".\" />");
                sw.WriteLine("  </index>");
                sw.WriteLine("  <index root=\"\\\" startIndexAt=\"pri.resfiles\">");
                sw.WriteLine("    <default>");
                sw.WriteLine("      <qualifier name=\"language\" value=\"en-US\" />");
                sw.WriteLine("    </default>");
                sw.WriteLine("    <indexer-config type=\"PRI\" />");
                sw.WriteLine("    <indexer-config type=\"RESFILES\" qualifierDelimiter=\".\" />");
                sw.WriteLine("  </index>");
                sw.WriteLine("</resources>");
            }
        }

        private string NormalizePath(string path)
        {
            if (Path.DirectorySeparatorChar == '\\')
                return path.Replace('/', Path.DirectorySeparatorChar);

            return path.Replace('\\', Path.DirectorySeparatorChar);
        }

        private string ExecuteMakePriProcess(
            out int exitCode,
            string args = "",
            string workingDir = "",
            LogOpts logOpts = LogOpts.kLogToFileAndConsole,
            bool forceAdministrator = false)
        {
            string winSdkPath = WindowsSDKData.GetBinPath();
            if (string.IsNullOrEmpty(winSdkPath))
            {
                exitCode = -1;
                return "Failed: Could not locate 'MakePri.exe'. Is the Windows SDK installed?";
            }

            string makePriCommand = Path.Combine(winSdkPath, "MakePri.exe");
            if (!File.Exists(makePriCommand))
            {
                exitCode = -1;
                return "Failed: Could not locate 'MakePri.exe'. Is the Windows SDK installed?";
            }

            ExecuteSystemProcess esp = new ExecuteSystemProcess(
                makePriCommand,
                args,
                workingDir,
                logOpts,
                forceAdministrator,
                0);

            exitCode = esp.exitCode;

            // Construct a more user friendly message. Note program.cs ProcessOutputStreamReader() adds a null terminator
            // after every character in esp.output for some reason. Possibly only related to calling makepri.exe.
            var message = "Working Directory: " + workingDir + "\nCommand Line: " + makePriCommand + " " + args + "\n\n" + esp.output.Replace("\0", "");

            return message;
        }
    }
}