using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    [CustomEditor(typeof(GameConfigImporter))]
    internal class GameConfigEditor : ScriptedImporterEditor
    {
        // We already provide everything needed in the inspector.
        public override bool showImportedObject => false;

        private static readonly string k_RefreshButtonLabel = "Refresh";
        private static readonly string k_ShellVisualString = "Shell Visuals";
        private static readonly List<GUIContent> k_Log = new List<GUIContent>();
        private static readonly HashSet<string> k_LogMessages = new HashSet<string>();
        private static bool s_HasAssetBeenReimported = false;

        private static Texture k_ErrorIcon;
        private static Texture k_InfoIcon;
        private static Texture k_WarningIcon;
        private static GUIStyle k_LogStyle;
        private static GUIStyle k_LogIconStyle;

        private static GameConfigAsset s_GameConfigAsset;
        private static MessageType s_WorstLogType = MessageType.None;
        private static int s_SelectedTab = 0;
        private static bool s_ShellVisualSectionFoldout = false;

        private SerializedProperty m_NameProperty;
        private SerializedProperty m_PublisherProperty;
        private SerializedProperty m_VersionProperty;
        private SerializedProperty m_TitleIdProperty;
        private SerializedProperty m_SCIDProperty;
        private SerializedProperty m_MSAAppIdProperty;
        private SerializedProperty m_StoreIdProperty;
        private SerializedProperty m_ContentIdOverrideProperty;
        private SerializedProperty m_EKBIDOverrideProperty;
        private SerializedProperty m_LanguagePairResourcesProperty;
        private SerializedProperty m_ShellVisualsResourcePathProperty;
        private SerializedProperty m_BuiltResourcesPriPathProperty;
        // private SerializedProperty m_resourcesDeploymentPathProperty;
        private SerializedProperty m_SandboxesProperty;

        private GUIContent m_ManifestContent;
        private Vector2 m_PreviewScrollPosition;
        private string m_ManifestPreview = string.Empty;

        private bool m_PendingShellVisualsInit = false;

        // Called every time the asset is selected and modified in the editor.
        public override void OnEnable()
        {
            base.OnEnable();
            var gameConfigAsset = GetAsset();

            if (gameConfigAsset == null)
            {
                // If the imported asset is invalid, the static object is set to null.
                s_GameConfigAsset = null;
                SetLog("Error: " + UIContent.k_InvalidGameConfigAssetMessage);
                return;
            }

            if (s_GameConfigAsset == null)
            {
                // When the static object is invalidated, it is defined as a null object but retains its previous instanceID.
                // As ScriptableObject comparison operator uses instanceID to compare objects,
                // the comparison will always return true in case a broken imported asset is fixed.
                // Resetting the static object to null here ensures that the comparison is performed
                // correctly the next time the asset is checked.
                s_GameConfigAsset = null;
            }

            if (s_GameConfigAsset != gameConfigAsset)
            {
                s_GameConfigAsset = gameConfigAsset;
                k_Log.Clear();
                s_WorstLogType = MessageType.None;
                s_HasAssetBeenReimported = false;
            }

            ValidateAndRefreshUtility();
            RefreshSerializedProperty();
        }

        // This is the only hook we have when asset is reimported.
        // However, this is also called (sometimes) after an Apply() call.
        public override void ReloadPreviewInstances()
        {
            base.ReloadPreviewInstances();

            // This is required to prevent overriding log from Apply() as Reload call is not predictable.
            if (s_HasAssetBeenReimported)
            {
                s_HasAssetBeenReimported = false;
                return;
            }

            ValidateAndRefreshUtility();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (s_GameConfigAsset == null)
            {
                GUI.enabled = false;
                base.OnInspectorGUI();
                GUI.enabled = true;
                EditorGUILayout.Space();
                DrawLogSection(false);
                return;
            }

            EditorGUILayout.Space();

            DrawElements();

            ApplyRevertGUI();

            EditorGUILayout.Space();

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                // Update log tab icon.
                UIContent.k_GameConfigEditorTabTitles[0].image = GetIconForMessageType(s_WorstLogType);
                s_SelectedTab = GUILayout.Toolbar(s_SelectedTab, UIContent.k_GameConfigEditorTabTitles);

                if (s_SelectedTab == 0)
                {
                    DrawLogSection(true);
                }
                else
                {
                    DrawPreviewSection();
                }
            }

            // Handle delayed Shell Visuals work until the UI has finished updating otherwise when we change the
            // asset on disk the scriptable object gets disposed halfway through UI drawing leading to problems
            if (m_PendingShellVisualsInit)
            {
                m_PendingShellVisualsInit = false;

                var gameConfigLocalizationTool = new GameConfigLocalizationTool(s_GameConfigAsset);
                gameConfigLocalizationTool.InitializeResources();
                ValidateAndRefreshUtility();
            }
        }

        private GameConfigAsset GetAsset()
        {
            return assetTarget as GameConfigAsset;
        }

        protected override void Apply()
        {
            base.Apply();

            var importer = target as GameConfigImporter;
            if (importer == null)
                return;

            // Validate and apply change at the same time.
            importer.ApplyEdits(GetAsset(), importer.assetPath, out var validationMessages);
            SetLog(validationMessages);
            RefreshManifestPreview();

            // Safeguard to prevent multiple validation calls.
            // This can happen when the asset is reloaded (unpredictable).
            s_HasAssetBeenReimported = true;
        }

        private void DrawElements()
        {
            EditorGUILayout.PropertyField(m_TitleIdProperty);
            EditorGUILayout.PropertyField(m_SCIDProperty);
            EditorGUILayout.PropertyField(m_MSAAppIdProperty);

            // [Header("Identity")]
            EditorGUILayout.PropertyField(m_NameProperty, new GUIContent("Name"));
            EditorGUILayout.PropertyField(m_PublisherProperty, new GUIContent("Publisher"));
            EditorGUILayout.PropertyField(m_VersionProperty, new GUIContent("Version"));

            // [Header("Store")]
            EditorGUILayout.PropertyField(m_StoreIdProperty);

            EditorGUILayout.Space();

            DrawShellVisuals();

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(m_SandboxesProperty, new GUIContent("Sandboxes"));

            // [Header("Development Only")]
            EditorGUILayout.PropertyField(m_ContentIdOverrideProperty);
            EditorGUILayout.PropertyField(m_EKBIDOverrideProperty);

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawShellVisuals()
        {
            s_ShellVisualSectionFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(s_ShellVisualSectionFoldout, k_ShellVisualString);

            if (!s_ShellVisualSectionFoldout)
            {
                EditorGUILayout.EndFoldoutHeaderGroup();
                return;
            }

            EditorGUILayout.PropertyField(m_LanguagePairResourcesProperty);
            EditorGUILayout.PropertyField(m_ShellVisualsResourcePathProperty);
            EditorGUILayout.PropertyField(m_BuiltResourcesPriPathProperty);
            // EditorGUILayout.PropertyField(m_resourcesDeploymentPathProperty);

            GUI.enabled = !HasModified();

            var gameConfigLocalizationTool = new GameConfigLocalizationTool(s_GameConfigAsset);

            bool priConfigExists = gameConfigLocalizationTool.ShellVisualsResourcesPathContainsPriConfig();
            if (GUILayout.Button(priConfigExists ? UIContent.k_UpdateShellVisualsResourcesButton : UIContent.k_GenerateShellVisualsResourcesButton, EditorStyles.miniButton))
            {
                m_PendingShellVisualsInit = true;
            }

            // We can only build resources when a Priconfig.xml exists
            using (new EditorGUI.DisabledScope(!priConfigExists))
            {
                if (GUILayout.Button(UIContent.k_BuildShellVisualsResourcesPriButton, EditorStyles.miniButton))
                {
                    if (s_GameConfigAsset.HasLanguagePairSet())
                    {
                        if (gameConfigLocalizationTool.BuildResources(out var outMessage) != 0
                            || !File.Exists(gameConfigLocalizationTool.GetResourcesPriPath()))
                        {
                            string errorMessage = "Failed to build Resources.pri.";
                            AddLogMessage(outMessage + "\nError: " + errorMessage);
                            Debug.LogError(errorMessage);
                        }

                        if (outMessage != null && outMessage.Length > 0)
                        {
                            Debug.Log(outMessage);
                        }

                        AssetDatabase.Refresh();
                        // We need to manually call the validation here as this asset as not been modified.
                        ValidateAndRefreshUtility();
                    }
                    else
                    {
                        string message = $"Unable to build Shell Visuals for '{s_GameConfigAsset.name}' as no language pairs have been set.";
                        AddLogMessage("Warning: " + message);
                        Debug.LogWarning(message);
                    }
                }
            }

            GUI.enabled = true;

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawLogSection(bool drawRefreshButton)
        {
            // Can't be initialized in static constructor because it uses EditorStyles.
            if (k_LogStyle == null)
            {
                k_LogStyle = new GUIStyle(EditorStyles.label)
                {
                    wordWrap = true,
                    richText = true,
                    margin = new RectOffset(6, 6, 6, 6),
                    stretchHeight = true,
                    stretchWidth = true,
                    imagePosition = ImagePosition.TextOnly
                };

                k_LogIconStyle = new GUIStyle(k_LogStyle)
                {
                    stretchHeight = true,
                    stretchWidth = false,
                    imagePosition = ImagePosition.ImageOnly
                };
            }

            bool hasAnyError = k_Log != null && k_Log.Count > 0;
            if (hasAnyError)
            {
                foreach (var log in k_Log)
                {
                    using (new EditorGUILayout.HorizontalScope(GUI.skin.box))
                    {
                        GUILayout.Label(log, k_LogIconStyle);
                        GUILayout.Label(log, k_LogStyle);
                    }
                }
            }

            if (!drawRefreshButton)
            {
                return;
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button(k_RefreshButtonLabel, GUILayout.ExpandWidth(false)))
                {
                    // Before U6, the asset is only reimported, and this safeguard will prevent the
                    // first refresh call from working.
                    s_HasAssetBeenReimported = false;
                    ValidateAndRefreshUtility();
                }
            }
        }

        private void DrawPreviewSection()
        {
            m_PreviewScrollPosition = EditorGUILayout.BeginScrollView(m_PreviewScrollPosition, GUILayout.MaxHeight(256));
            {
                Vector2 textSize = EditorStyles.textArea.CalcSize(m_ManifestContent);
                EditorGUILayout.SelectableLabel(m_ManifestPreview, EditorStyles.textArea,
                    GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true), GUILayout.MinWidth(textSize.x),
                    GUILayout.MinHeight(textSize.y + GUI.skin.horizontalScrollbar.fixedHeight));
            }
            EditorGUILayout.EndScrollView();
        }

        // Validate and refresh log and manifest preview.
        private void ValidateAndRefreshUtility()
        {
            // CONSOLE-304: U6 GameConfigAsset - Reimporting asset triggers 2 validation calls.
            // In Unity 6, changing an asset in the inspector triggers a reimport and editor refresh (OnEnable).
            // Before Unity 6, only the reimport is triggered.
            // We use the same mechanism that ReloadPreviewInstances to avoid this.
            if (s_HasAssetBeenReimported)
            {
                s_HasAssetBeenReimported = false;
                return;
            }

            MGCValidatorUtility.ValidateAll(s_GameConfigAsset, out var validationMessages);
            SetLog(validationMessages);
            RefreshManifestPreview();
        }

        private void AddLogMessage(string message)
        {
            if (k_LogMessages.Contains(message))
            {
                return;
            }

            k_LogMessages.Add(message);
            RegenerateLog();
        }

        private void AddLogMessage(IReadOnlyList<string> messages)
        {
            foreach (var message in messages)
            {
                k_LogMessages.Add(message);
            }

            RegenerateLog();
        }

        private void SetLog(string validationMessage)
        {
            var messages = validationMessage.Split('\n');
            k_LogMessages.Clear();

            foreach (var message in messages)
            {
                k_LogMessages.Add(message);
            }

            RegenerateLog();
        }

        private void RegenerateLog()
        {
            k_Log.Clear();
            s_WorstLogType = MessageType.None;

            foreach (var message in k_LogMessages)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    continue;
                }

                if (message.Contains("Error: "))
                {
                    k_Log.Insert(0, EditorGUIUtility.TrTextContent(message.Replace("Error: ", ""), "", "error"));
                    s_WorstLogType = MessageType.Error;
                    s_SelectedTab = 0;
                }
                else if (message.Contains("Warning: "))
                {
                    k_Log.Add(EditorGUIUtility.TrTextContent(message.Replace("Warning: ", ""), "", "warning"));
                    if (s_WorstLogType != MessageType.Error)
                        s_WorstLogType = MessageType.Warning;
                }
                else
                {
                    k_Log.Add(EditorGUIUtility.TrTextContent(message, "", "info"));
                    if (s_WorstLogType == MessageType.None)
                        s_WorstLogType = MessageType.Info;
                }
            }
        }

        private void RefreshManifestPreview()
        {
            var importer = target as GameConfigImporter;
            m_ManifestPreview = GameConfigAsset.GameConfigToString(GameConfigAsset.LoadAsXmlGameConfig(importer.assetPath));

            if (m_ManifestContent == null)
            {
                m_ManifestContent = new GUIContent(m_ManifestPreview);
            }
            else
            {
                m_ManifestContent.text = m_ManifestPreview;
            }
        }

        private void RefreshSerializedProperty()
        {
            m_NameProperty = serializedObject.FindProperty("m_IdentityName");
            m_PublisherProperty = serializedObject.FindProperty("m_IdentityPublisher");
            m_VersionProperty = serializedObject.FindProperty("m_IdentityVersion");
            m_TitleIdProperty = serializedObject.FindProperty("m_TitleId");
            m_SCIDProperty = serializedObject.FindProperty("m_SCID");
            m_MSAAppIdProperty = serializedObject.FindProperty("m_MSAAppId");
            m_StoreIdProperty = serializedObject.FindProperty("m_StoreId");
            m_ContentIdOverrideProperty = serializedObject.FindProperty("m_ContentIdOverride");
            m_EKBIDOverrideProperty = serializedObject.FindProperty("m_EKBIDOverride");
            m_LanguagePairResourcesProperty = serializedObject.FindProperty("m_LanguagePairResources");
            m_ShellVisualsResourcePathProperty = serializedObject.FindProperty("m_ShellVisualsResourcePath");
            m_BuiltResourcesPriPathProperty = serializedObject.FindProperty("m_BuiltResourcesPriPath");
            // m_resourcesDeploymentPath = serializedObject.FindProperty("m_ResourcesDeploymentPath");
            m_SandboxesProperty = serializedObject.FindProperty("m_Sandboxes");
        }

        private static Texture GetIconForMessageType(MessageType type)
        {
            switch (type)
            {
                case MessageType.Info:
                    if (k_InfoIcon == null)
                    {
                        k_InfoIcon = EditorGUIUtility.IconContent("info").image;
                    }
                    return k_InfoIcon;

                case MessageType.Warning:
                    if (k_WarningIcon == null)
                    {
                        k_WarningIcon = EditorGUIUtility.IconContent("warning").image;
                    }
                    return k_WarningIcon;

                case MessageType.Error:
                    if (k_ErrorIcon == null)
                    {
                        k_ErrorIcon = EditorGUIUtility.IconContent("error").image;
                    }
                    return k_ErrorIcon;

                default:
                    return null;
            }
        }
    }
}
