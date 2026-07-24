using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    internal class GdkSettingsProvider : SettingsProvider
    {
        // Default location for a new GDK Settings asset when no location has been specified
        private const string k_GdkSettingsDefaultAssetPath = "Assets/GDKSettings.asset";

        // This defines where in the Project Settings hierarchy the 'Microsoft GDK' settings will live
        private const string k_GdkSettingsOnProjectSettingsWindow = "Project/Microsoft GDK";

        // Used with EditorBuildSettings for tracking the currently active GDK Settings asset
        private const string k_EditorBuildSettingsConfigKey = "Unity.Microsoft.GDK.Editor.Settings";

        private static GdkSettingsProvider s_Instance;

        private List<GUIContent> m_CurrentSupportedPlatforms;
        private List<BuildTargetGroup> m_BuildTargets;
        private int m_SelectedPlatform = 0;

        private GdkEditionsSelector m_EditionSelector = new GdkEditionsSelector();

        public GdkSettingsProvider(string path, SettingsScope scope)
            : base(path, scope)
        {
            s_Instance = this;
        }

        public static void ActivateGdkSettings(GdkSettingsAsset gdkSettings)
        {
            if (gdkSettings != null)
            {
                EditorBuildSettings.AddConfigObject(
                    k_EditorBuildSettingsConfigKey,
                    gdkSettings,
                    true);
            }
            else
            {
                EditorBuildSettings.RemoveConfigObject(k_EditorBuildSettingsConfigKey);
            }

            SetScriptingDefinesForSettingsAsset(gdkSettings);
        }

        public static GdkSettingsAsset CreateGdkSettings(string path = null)
        {
            GdkSettingsAsset newGdkSettings = ScriptableObject.CreateInstance<GdkSettingsAsset>();

            RuntimePlatform platform = GDKPlatformManager.BuildTargetGroupToRuntimePlatform(BuildTargetGroup.Standalone);
            newGdkSettings.AddDefaultSettingsPlatformIfNeeded(platform);

            AssetDatabase.CreateAsset(newGdkSettings, string.IsNullOrEmpty(path) ? k_GdkSettingsDefaultAssetPath : path);
            AssetDatabase.SaveAssets();

            return newGdkSettings;
        }

        [SettingsProvider]
        public static SettingsProvider CreateGdkSettingsProvider()
        {
            return new GdkSettingsProvider(k_GdkSettingsOnProjectSettingsWindow, SettingsScope.Project);
        }

        public static void ForceReload()
        {
            if (s_Instance != null)
            {
                // Request repaint.
                SettingsService.NotifySettingsProviderChanged();
            }
        }

        public static bool GenerateGdkSettingsCs()
        {
            GdkSettingsAsset activeGdkSettings = LoadSettings();
            if (activeGdkSettings == null)
                return false;

            GdkPlatformSettingsGenerator.Options options = new GdkPlatformSettingsGenerator.Options();

            string assetPath = AssetDatabase.GetAssetPath(activeGdkSettings);

            options.includeDefine = activeGdkSettings.createGdkPlatformSettingsDefine;
            options.namespaceName = activeGdkSettings.gdkPlatformSettingsCsNamespace;
            options.sourceAssetPath = assetPath;

            string csFilePath = Path.Combine(Path.GetDirectoryName(assetPath),
                Path.GetFileNameWithoutExtension(assetPath) + ".cs");
            GdkPlatformSettingsGenerator.GenerateWrapperCode(csFilePath, activeGdkSettings, options);

            AssetDatabase.Refresh();

            return true;
        }

        public static GdkSettingsAsset.PlatformGdkSettings GetActiveGdkSettingsForBuildTarget(
            BuildTarget buildTarget)
        {
            GdkSettingsAsset activeGdkSettings = LoadSettings();
            if (activeGdkSettings != null)
            {
                BuildTargetGroup buildTargetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget);

                if (GDKPlatformManager.SupportsBuildTargetGroup(buildTargetGroup) == false)
                {
                    return null;
                }

                var platform = GDKPlatformManager.BuildTargetGroupToRuntimePlatform(buildTargetGroup);
                activeGdkSettings.SettingsForRuntimePlatform(platform, out var platformSetting);

                return platformSetting;
            }

            return null;
        }

        public static bool IsActiveGdkSettingsAvailable()
        {
            return LoadSettings() != null;
        }

        public static bool IsBuildTargetSupported(BuildTarget target)
        {
            return BuildPipeline.IsBuildTargetSupported(BuildPipeline.GetBuildTargetGroup(target), target);
        }

        public static bool IsGdkSettingsAssetAvailable()
        {
            List<GdkSettingsAsset> gdkSettingsList = FindGdkSettingsAssetsInProject();
            if (gdkSettingsList == null || gdkSettingsList.Count == 0)
                return false;

            return true;
        }

        public static bool IsTargetingActiveBuildTarget()
        {
            GdkSettingsAsset.PlatformGdkSettings platformGdkSettings =
                GetActiveGdkSettingsForBuildTarget(EditorUserBuildSettings.activeBuildTarget);

            return platformGdkSettings is { targetMicrosoftGdk: true };
        }

        public static bool IsTargetingBuildTarget(BuildTarget buildTarget)
        {
            GdkSettingsAsset.PlatformGdkSettings platformGdkSettings =
                GetActiveGdkSettingsForBuildTarget(buildTarget);

            return platformGdkSettings is { targetMicrosoftGdk: true };
        }

        public static GdkSettingsAsset LoadSettings()
        {
            EditorBuildSettings.TryGetConfigObject(k_EditorBuildSettingsConfigKey, out GdkSettingsAsset activeGdkSettings);
            return activeGdkSettings;
        }

        public static void Open()
        {
            SettingsService.OpenProjectSettings(k_GdkSettingsOnProjectSettingsWindow);
        }

        public bool LoadOrCreateSettings()
        {
            if (LoadSettings())
                return true;

            // Let's be nice, if we're about to create the only GDK Settings in the project
            //  activate them at the same time.
            bool makeActiveGdkSettings = false;
            List<GdkSettingsAsset> gdkSettingsList = FindGdkSettingsAssetsInProject();
            if (gdkSettingsList == null || gdkSettingsList.Count == 0)
                makeActiveGdkSettings = true;

            GdkSettingsAsset newGdkSettings = CreateGdkSettings(k_GdkSettingsDefaultAssetPath);

            if (makeActiveGdkSettings)
            {
                ActivateGdkSettings(newGdkSettings);
            }

            return true;
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            base.OnActivate(searchContext, rootElement);

            RefreshAvailableBuildTargets();

            //GdkSettingsProvider.onSettingsChange += OnSettingsChange;
            //Undo.undoRedoPerformed += OnUndoRedo;
        }

        public override void OnDeactivate()
        {
            base.OnDeactivate();

            //GdkSettingsProvider.onSettingsChange -= OnSettingsChange;
            //Undo.undoRedoPerformed -= OnUndoRedo;
        }

        private bool ConfirmDomainReloadDialog()
        {
            return EditorUtility.DisplayDialog("Warning: Domain Reload Required",
                "A domain reload will be required to proceed with this change. Are you sure you want to continue?",
                "Continue", "Cancel");
        }

        public override void OnGUI(string searchContext)
        {
            if (!TryGetActiveGDKSettings(out var activeGdkSettings))
            {
                return;
            }

            EditorGUILayout.Space();

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                // Draw toolbar
                m_SelectedPlatform = GUILayout.Toolbar(m_SelectedPlatform, m_CurrentSupportedPlatforms.ToArray());

                if (m_BuildTargets.Count==0)
                {
                    EditorGUILayout.HelpBox(
                        "Installed platforms do not support Microsoft GDK.",
                        MessageType.Info);

                    return;
                }

                BuildTargetGroup buildTargetGroup = m_BuildTargets[m_SelectedPlatform];
                if (!GDKPlatformManager.SupportsBuildTargetGroup(buildTargetGroup))
                {
                    EditorGUILayout.HelpBox(
                        UIContent.k_WarningGdkNotSupported.text,
                        MessageType.Info);

                    return;
                }

                // Do we have settings for the current platform?
                activeGdkSettings.SettingsForRuntimePlatform(
                    GDKPlatformManager.BuildTargetGroupToRuntimePlatform(buildTargetGroup),
                    out var targetGdkSettings);

                if (targetGdkSettings == null)
                {
                    EditorGUILayout.HelpBox(
                        UIContent.k_WarningPlatformGdkMissing.text,
                        MessageType.Info);

                    if (GUILayout.Button(UIContent.k_AddTargetSettings, GUILayout.Height(30)))
                    {
                        RuntimePlatform platform = GDKPlatformManager.BuildTargetGroupToRuntimePlatform(buildTargetGroup);
                        activeGdkSettings.AddDefaultSettingsPlatformIfNeeded(platform);
                        SetScriptingDefinesForSettingsAsset(activeGdkSettings);

                        EditorUtility.SetDirty(activeGdkSettings);
                        AssetDatabase.SaveAssets();
                    }

                    return;
                }

                var platformDisplayName = GDKPlatformManager.GetBuildTargetGroupDisplayName(buildTargetGroup);
                EditorGUILayout.LabelField(
                    string.Format(L10n.Tr("Settings for {0}"), platformDisplayName),
                    EditorStyles.boldLabel);

                EditorGUI.BeginChangeCheck();
                EditorGUIUtility.labelWidth = GUILayoutUtility.GetLastRect().width * 0.5f;

                GUI.enabled = GDKPlatformManager.BuildTargetGroupOnlySupportsGdk(buildTargetGroup) == false;

                bool targetMicrosoftGdk = EditorGUILayout.Toggle(UIContent.k_BuildForMsGdk, targetGdkSettings.targetMicrosoftGdk);
                if(targetMicrosoftGdk != targetGdkSettings.targetMicrosoftGdk)
                {
                    if(ConfirmDomainReloadDialog())
                    {
                        targetGdkSettings.targetMicrosoftGdk = targetMicrosoftGdk;

#pragma warning disable CS0618 // Type or member is obsolete
                        // Try to provide a reasonable value for now
                        targetGdkSettings.createScriptingDefine = targetMicrosoftGdk;
#pragma warning restore CS0618

                        SetScriptingDefinesForSettingsAsset(activeGdkSettings);
                    }
                }

                GUI.enabled = true;

                if (GDKPlatformManager.BuildTargetGroupOnlySupportsGdk(buildTargetGroup))
                {
                    if (targetGdkSettings.targetMicrosoftGdk == false)
                    {
                        EditorGUILayout.HelpBox(
                            $"The {platformDisplayName} platform no longer supports disabling the 'Build for Microsoft GDK' setting. " +
                            "The active GDK Settings Asset is from an older GDK Tools package version.\n\nPlease re-create to enable build for Microsoft GDK. ",
                            MessageType.Warning);
                    }
                }

                using (new EditorGUI.DisabledScope(!targetGdkSettings.targetMicrosoftGdk))
                {
                    m_EditionSelector.Draw(buildTargetGroup);

                    EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
                    DrawMGCSection(targetGdkSettings);

                    EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);

                    // Allows the user to select how the GDK package should handle the Shell Visuals resources
                    targetGdkSettings.shellVisualsMode =
                        (GdkSettingsAsset.ShellVisualsMode)EditorGUILayout.EnumPopup(UIContent.k_ShellVisualsHandlingMode, targetGdkSettings.shellVisualsMode);

                    EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);

                    if (buildTargetGroup == BuildTargetGroup.Standalone)
                    {
                        bool usingXboxPCToolbox = false;

#if UNITY_6000_5_OR_NEWER
                        var deployTarget = EditorUserBuildSettings.windowsBuildAndRunDeployTarget;
                        usingXboxPCToolbox =
                            deployTarget == WindowsBuildAndRunDeployTarget.XboxPcToolbox;
#endif

                        if (usingXboxPCToolbox)
                        {
                            using (new EditorGUI.DisabledScope(true))
                            {
                                targetGdkSettings.createPackage =
                                    EditorGUILayout.Toggle(UIContent.k_CreateMsStorePackage, targetGdkSettings.createPackage);

                                EditorGUI.indentLevel++;

                                targetGdkSettings.sideloadPackage =
                                    EditorGUILayout.Toggle(UIContent.k_SideloadMsStorePackage, targetGdkSettings.sideloadPackage);

                                EditorGUI.indentLevel--;
                            }

                            EditorGUILayout.HelpBox(
                                "Microsoft Store Packaging is disabled when deploying using Xbox PC Toolbox. Change the deploy target in Build Settings to enable it.",
                                MessageType.Info);
                        }
                        else
                        {
                            targetGdkSettings.createPackage =
                                EditorGUILayout.Toggle(UIContent.k_CreateMsStorePackage, targetGdkSettings.createPackage);

                            using (new EditorGUI.DisabledScope(!targetGdkSettings.createPackage))
                            {
                                EditorGUI.indentLevel++;

                                targetGdkSettings.sideloadPackage =
                                    EditorGUILayout.Toggle(UIContent.k_SideloadMsStorePackage, targetGdkSettings.sideloadPackage);

                                EditorGUI.indentLevel--;

                                if (targetGdkSettings.createPackage)
                                {
                                    EditorGUILayout.Space();

                                    var encryptionMethod = "a Random Key (/l), see documentation for Stable Key (/lk) configuration";

                                    if (!string.IsNullOrEmpty(MakePkgStableKeyConfig.PackageStableKey))
                                        encryptionMethod = "a Stable Key (/lk), keep your key secure";

                                    if (targetGdkSettings.sideloadPackage)
                                        encryptionMethod = "Test Signing (/lt), not suitable for submission";

                                    EditorGUILayout.HelpBox(
                                        $"The Microsoft Store package will be encrypted using {encryptionMethod}.",
                                        MessageType.Info);

                                    if (targetGdkSettings.shellVisualsMode == GdkSettingsAsset.ShellVisualsMode.Disabled)
                                    {
                                        EditorGUILayout.HelpBox(UIContent.k_WarningMsStorePackage, MessageType.Warning);
                                    }
                                }
                            }
                        }

                        EditorGUILayout.Space();
                    }
                }
            }

            DrawProjectGenerationSection(activeGdkSettings);

            if (EditorGUI.EndChangeCheck())
            {
                //SaveSettings();
                if (activeGdkSettings != null)
                {
                    EditorUtility.SetDirty(activeGdkSettings);
                    AssetDatabase.SaveAssets();
                }
            }

            EditorGUIUtility.labelWidth = 0;
        }

        private static void SetScriptingDefinesForSettingsAsset(GdkSettingsAsset settings)
        {
            foreach(var gdkPlatform in GDKPlatformManager.GetRegisteredPlatforms())
            {
                var buildTargetGroup = BuildPipeline.GetBuildTargetGroup(gdkPlatform.BuildTarget);
                if (BuildPipeline.IsBuildTargetSupported(buildTargetGroup, gdkPlatform.BuildTarget))
                {
                    GdkSettingsAsset.PlatformGdkSettings platformSettings;
                    RuntimePlatform runtimePlatform = GDKPlatformManager.BuildTargetGroupToRuntimePlatform(buildTargetGroup);
                    if(settings == null)
                    {
                        GdkEditorHelpers.DisableScriptingDefine(GdkEditorHelpers.k_scriptingDefine, buildTargetGroup);
                        continue;
                    }

                    bool enableDefine = false;
                    if (settings.SettingsForRuntimePlatform(runtimePlatform, out platformSettings))
                    {
                        enableDefine = platformSettings.targetMicrosoftGdk;
                    }

                    if (enableDefine == true)
                    {
                        GdkEditorHelpers.EnableScriptingDefine(GdkEditorHelpers.k_scriptingDefine, buildTargetGroup);
                    }
                    else
                    {
                        GdkEditorHelpers.DisableScriptingDefine(GdkEditorHelpers.k_scriptingDefine, buildTargetGroup);
                    }
                }
            }
        }

        private bool TryGetActiveGDKSettings(out GdkSettingsAsset activeGdkSettings)
        {
            activeGdkSettings = null;
            var gdkSettingsAssetList = FindGdkSettingsAssetsInProject();
            // Do we actually have any Gdk Settings assets in the project?
            if (gdkSettingsAssetList == null || gdkSettingsAssetList.Count < 1)
            {
                // Clean up the 'Editor Build Settings' then let the user know what is going on
                EditorBuildSettings.RemoveConfigObject(k_EditorBuildSettingsConfigKey);

                EditorGUILayout.HelpBox(UIContent.k_WarningNeedGdkSettings.text, MessageType.Info);

                if (GUILayout.Button(UIContent.k_CreateGdkSettings, GUILayout.Height(30)))
                    LoadOrCreateSettings();

                return false;
            }

            // Warn the user if the project contains more than one GDK Settings asset
            //  Only once can be active at any time!
            if (gdkSettingsAssetList.Count > 1)
            {
                EditorGUILayout.HelpBox(UIContent.k_WarningMultipleGdkSettings.text, MessageType.Info);
                EditorGUILayout.Space();
            }

            // Load the active settings and give the user a chance to switch the current active settings
            activeGdkSettings = LoadSettings();
            GdkSettingsAsset selectedGdkSettings = EditorGUILayout.ObjectField(UIContent.k_ActiveGdkSettings,
                activeGdkSettings, typeof(GdkSettingsAsset), false) as GdkSettingsAsset;

            if (selectedGdkSettings != activeGdkSettings)
            {
                ActivateGdkSettings(selectedGdkSettings);

                // Reload the active settings
                activeGdkSettings = LoadSettings();

                // Refresh the inspector window (it might be viewing the previous or newly selected GDK Settings asset)
                GdkEditorHelpers.ForceInspectorWindowRefresh();
            }

            // We can't go any further without active settings
            if (activeGdkSettings == null)
                return false;

            return true;
        }

        private static void DrawMGCSection(GdkSettingsAsset.PlatformGdkSettings targetGdkSettings)
        {
            targetGdkSettings.gameConfigAsset = EditorGUILayout.ObjectField(UIContent.k_MicrosoftGameConfig,
                                targetGdkSettings.gameConfigAsset, typeof(GameConfigAsset), false) as GameConfigAsset;

            if (targetGdkSettings.gameConfigAsset != null)
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUI.indentLevel++;

                    EditorGUILayout.TextField("Title ID", targetGdkSettings.gameConfigAsset.TitleId);
                    EditorGUILayout.TextField("SCID", targetGdkSettings.gameConfigAsset.SCID);

                    EditorGUI.indentLevel--;
                }

                using (new EditorGUI.DisabledScope(targetGdkSettings.gameConfigAsset == null))
                {
                    EditorGUILayout.BeginHorizontal(); // Start the horizontal group
                    GUILayout.FlexibleSpace();

                    string assetPath = AssetDatabase.GetAssetPath(targetGdkSettings.gameConfigAsset);

                    if (GUILayout.Button(UIContent.k_GameConfigEditorButton))
                        GameConfigHelper.OpenWithGameConfigEditor(assetPath);

                    EditorGUILayout.EndHorizontal(); // End the horizontal group
                }
            }
        }

        private static void DrawProjectGenerationSection(GdkSettingsAsset activeGdkSettings)
        {
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("GDK Settings Script", EditorStyles.boldLabel);

            activeGdkSettings.createGdkPlatformSettingsCs =
                EditorGUILayout.Toggle(UIContent.k_GenerateGdkPlatformSettingsCs, activeGdkSettings.createGdkPlatformSettingsCs);

            activeGdkSettings.gdkPlatformSettingsCsNamespace =
                EditorGUILayout.TextField(UIContent.k_GdkPlatformSettingsNamespace, activeGdkSettings.gdkPlatformSettingsCsNamespace);

            EditorGUILayout.Space();

            if (GUILayout.Button(UIContent.k_GenerateGdkPlatformSettingsCsButton, GUILayout.Height(30)))
                GenerateGdkSettingsCs();
        }

        public override void OnTitleBarGUI()
        {/*
            if (EditorGUILayout.DropdownButton(EditorGUIUtility.IconContent("_Popup"), FocusType.Passive, EditorStyles.label))
            {
                var menu = new GenericMenu();
                menu.AddDisabledItem(new GUIContent("Available Settings Assets:"));
                menu.AddSeparator("");
                for (var i = 0; i < m_AvailableSettingsAssetsOptions.Length; i++)
                    menu.AddItem(new GUIContent(m_AvailableSettingsAssetsOptions[i]), m_CurrentSelectedInputSettingsAsset == i, (path) => {
                        InputSystem.settings = AssetDatabase.LoadAssetAtPath<InputSettings>((string)path);
                    }, m_AvailableInputSettingsAssets[i]);
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("New Settings Asset…"), false, CreateNewSettingsAsset);
                menu.ShowAsContext();
                Event.current.Use();
            }
            */
        }

        private void RefreshAvailableBuildTargets()
        {
            if (m_CurrentSupportedPlatforms == null)
            {
                m_CurrentSupportedPlatforms = new List<GUIContent>();
            }
            if (m_BuildTargets == null)
            {
                m_BuildTargets = new List<BuildTargetGroup>();
            }

            m_CurrentSupportedPlatforms.Clear();
            m_BuildTargets.Clear();

            foreach (GdkPlatform platform in GDKPlatformManager.GetRegisteredPlatforms())
            {
                if (IsBuildTargetSupported(platform.BuildTarget))
                {
                    m_CurrentSupportedPlatforms.Add(platform.TabTitle);

                    var platformBuildTargetGroup = BuildPipeline.GetBuildTargetGroup(platform.BuildTarget);
                    m_BuildTargets.Add(platformBuildTargetGroup);
                }
            }
        }

        private static List<GdkSettingsAsset> FindGdkSettingsAssetsInProject()
        {
            string[] guids = AssetDatabase.FindAssets("t:GdkSettingsAsset");
            List<GdkSettingsAsset> gdkSettingsAssetList = new List<GdkSettingsAsset>();

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                GdkSettingsAsset scriptableObject = AssetDatabase.LoadAssetAtPath<GdkSettingsAsset>(assetPath);
                gdkSettingsAssetList.Add(scriptableObject);
            }

            return gdkSettingsAssetList;
        }
    }
}