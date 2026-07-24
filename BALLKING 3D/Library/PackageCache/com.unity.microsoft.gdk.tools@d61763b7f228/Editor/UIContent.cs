using UnityEditor;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    internal static class UIContent
    {
        // GdkSettingsEditor
        public static readonly GUIContent k_MakeGdkSettingsActive = new(
            "Activate GDK Settings",
            null,
            "This GDK Settings asset is not active. Click 'Activate GDK Settings' to activate the " +
            "selected GDK Settings asset.");

        public static readonly string k_InfoSelectedGdkSettingsIsActive =
            "The selected GDK Settings asset is currently active.";

        public static readonly string k_InvalidGameConfigAssetMessage =
            "Not a valid Microsoft Game Config asset." +
            " Check logs and ensure correct xml format.";

        public static readonly GUIContent k_WarningNeedGdkSettings = EditorGUIUtility.TrTextContent(
            "GDK Settings support development with the Unity Microsoft GDK package and are stored as an asset." +
            " An asset of type 'GdkSettings' can not be found. Either use the asset creation menu or the button" +
            " below to create a default GDK Settings asset.");

        public static readonly GUIContent k_WarningMultipleGdkSettings = EditorGUIUtility.TrTextContent(
            "This project contains multiple GDK Settings assets, since GDK Settings are global" +
            " only one can be active.");

        public static readonly GUIContent k_WarningGdkNotSupported = EditorGUIUtility.TrTextContent(
            "Microsoft GDK Developments is not supported on this platform");

        public static readonly GUIContent k_WarningPlatformGdkMissing = EditorGUIUtility.TrTextContent(
            "The settings for this platform are not present in the active GDK Settings asset. Click " +
            "'Add Target GDK Settings' to add the default settings for the selected platform.");

        public static readonly GUIContent k_CreateGdkSettings = new(
            "Create GDK Settings Asset",
            null,
            "When selected a 'GDK Settings Asset' will be created in the root of the project asset folder.");

        public static readonly GUIContent k_ActiveGdkSettings = new(
            "Active GDK Settings",
            null,
            "Select the 'GDK Settings' asset that contains the settings that should be applied at build time.");

        // GdkSettingsProvider
        public static readonly GUIContent k_AddTargetSettings = new(
            "Add Target GDK Settings",
            null,
            "Update the active GDK Settings asset to include the selected target platform settings.");

        public static readonly GUIContent k_BuildForMsGdk = new(
            "Build for Microsoft GDK",
            null,
            "Enables additional build steps to support the Microsoft GDK API and adds the MICROSOFT_GDK_SUPPORT " +
            "pre-processor to the player scripting defines list.");

        public static readonly GUIContent k_MicrosoftGdkVersion = new(
            "Microsoft GDK Version",
            null,
            $"Select the GDK edition to use for this project.");

        public static readonly GUIContent k_ExecuteMakePri = new(
        "Run 'MakePri.exe' on build",
        null,
        "Executes the MakePri.exe command line tool to build Resources.pri during the project build. " +
        "Requires resources and MGC language-locale pairs to be correctly configured.");

        public static readonly GUIContent k_ShellVisualsHandlingMode = new(
            "Shell Visuals Mode",
            null,
            "'Disabled' - Will not run 'MakePri.exe' or attempt to deploy resources, " +
            "'Make And Copy' - Will run the 'MakePri.exe' command tool and on success copy the shell visuals " +
            "resources and Resources.pri files to the build location, 'Copy Only' - will skip running " +
            "'MakePri.exe' and go straight to copying the shell visuals and Resources.pri.");

        public static readonly GUIContent k_DefineMsGdkSupport = new(
        "Define MICROSOFT_GDK_SUPPORT",
        null,
        "Defines MICROSOFT_GDK_SUPPORT pre-processor for the build to isolate GDK specific code.");

        public static readonly GUIContent k_CreateMsStorePackage = new(
            "Create Microsoft Store Package",
            null,
            "The build process attempts to package a successful build.");

        public static readonly GUIContent k_SideloadMsStorePackage = new(
            "Sideloadable Package",
            null,
            "Generates a test signed package. The package will be automatically installed into the local Microsoft Store app when using 'Build and Run'.");

        public static readonly GUIContent k_GenerateGdkPlatformSettingsCs = new(
            "Regenerate On Build",
            null,
            "At build time automatically regenerates the GDK settings script file (.cs) in the same location and named after the currently active GDK Settings asset. " +
            "Any pre-existing file will be replaced.");

        public static readonly GUIContent k_GdkPlatformSettingsNamespace = new(
            "Use Namespace",
            null,
            "An optional namespace to use when generating the GDK Settings script file.");

        public static readonly GUIContent k_MicrosoftGameConfig = new(
            "Microsoft Game Config",
            null,
            "The MGC being used for this platform.");

        public static readonly GUIContent k_GameConfigEditorButton = new(
            "Open MGC with Game Config Editor",
            null,
            "When selected the MGC will be opened externally with the Microsoft Game Config Editor.");

        public static readonly GUIContent k_GenerateShellVisualsResourcesButton = new(
            "Generate Shell Visuals Resources",
            null,
            "Generate placeholder 'Shell Visuals' resources as required by Microsoft Configuration File and" +
            " 'MakePri.exe', as well as for building a 'Resources.pri' file (generated content will depend on" +
            " currently configured language pairs).");

        public static readonly GUIContent k_UpdateShellVisualsResourcesButton = new(
            "Update Shell Visuals Resources",
            null,
            "Updates Shell Visuals resources to reflect any language-locale pair resource changes. The " +
            "pre-existing resources remain unchanged.");

        public static readonly GUIContent k_BuildShellVisualsResourcesPriButton = new(
            "Build Shell Visuals Resources (Resources.Pri)",
            null,
            "Runs MakePri.exe command line tool with the PriConfig and associated resources. The " +
            "successfully built Resources.pri is copied to the location specified by Built Resources Pri Path.");

        // GameConfigEditor
        public static readonly GUIContent[] k_GameConfigEditorTabTitles = {
            new GUIContent("Log"),
            new GUIContent("Preview", "Microsoft Game Config XML preview.") };

        // Shared
        public static readonly GUIContent k_OpenGdkSettings = new(
            "Open Project Settings - Microsoft GDK",
            null,
            "Open the Project Settings window at the Microsoft GDK section.");

        public static readonly GUIContent k_GenerateGdkPlatformSettingsCsButton = new(
            "Generate GDK Settings Script Now",
            null,
            "Generates the GDK Settings script file (.cs) in the same location and named after the currently active GDK Settings asset.");

        public static readonly string k_WarningMsStorePackage =
            "The Microsoft Store package requires Shell Visuals to be created (or makepkg will fail). " +
            $"We recommend to enable '{UIContent.k_ShellVisualsHandlingMode.text}' unless you are an advanced user.";
    }
}