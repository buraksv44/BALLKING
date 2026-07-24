using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    [InitializeOnLoad]
    internal class MakePkgStableKeyConfig : EditorWindow
    {
        private const string SESSION_STATE_KEY = "GDK_PC_StableKeyFilePath";
        private const string ENVIRONMENT_VARIABLE = "UNITY_GDK_MAKEPKG_EKBFILE_PC";

        static MakePkgStableKeyConfig()
        {
            EditorApplication.quitting += OnEditorQuitting;
        }

        private static void OnEditorQuitting()
        {
            SessionState.SetString(SESSION_STATE_KEY, "");
        }

        private void OnEnable()
        {
            Initialize();
        }

        private static void Initialize()
        {
            if (!SessionState.GetBool("GDK_StableKeyInitialized", false))
            {
                SessionState.SetString(SESSION_STATE_KEY, Environment.GetEnvironmentVariable(ENVIRONMENT_VARIABLE) ?? "");
                SessionState.SetBool("GDK_StableKeyInitialized", true);
            }
        }

        public static string PackageStableKey
        {
            get
            {
                Initialize();
                return SessionState.GetString(SESSION_STATE_KEY, "");
            }
            private set => SessionState.SetString(SESSION_STATE_KEY, value);
        }

        [MenuItem("GDK/PC/Package Stable-Key Config", false, 200)]
        private static void ShowWindow()
        {
            MakePkgStableKeyConfig window = GetWindow<MakePkgStableKeyConfig>();

            window.titleContent = new GUIContent("GDK Package Stable-Key Config");
            window.minSize = new Vector2(400, 425);
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            GUIStyle boldStyle = new GUIStyle(EditorStyles.label);
            boldStyle.fontStyle = FontStyle.Bold;

            EditorGUILayout.LabelField("Windows Standalone", boldStyle);

            // Informational section
            EditorGUILayout.LabelField("Select or create a Escrowed Key Blob (LEKB) file. When selected the Microsoft Store package post-build step will automatically use the Stable Key (/lk) encryption mode when calling the MakePkg tool.", EditorStyles.wordWrappedLabel);
            GUILayout.Space(5);
            EditorGUILayout.LabelField($"Changes here will override the '{ENVIRONMENT_VARIABLE}' environment variable value if configured and will persist only for the lifetime of the current Editor session.", EditorStyles.wordWrappedLabel);
            GUILayout.Space(20);
            EditorGUILayout.HelpBox("The contents of the LEKB file are only lightly obfuscated. It is your responsibility to ensure the security of your content packages by securing access to the LEKB file. Anyone possessing the LEKB file and the content package file output by MakePkg can decrypt its contents. Store it in a secure location with need-to-know access. Ideally, only official build machines/accounts should have access to it.", MessageType.Warning);
            GUILayout.Space(20);

            // File selection section
            EditorGUILayout.LabelField("LEKB File:");
            EditorGUILayout.BeginHorizontal();
            var newPath = EditorGUILayout.TextField(PackageStableKey);
            if (newPath != PackageStableKey)
            {
                PackageStableKey = newPath;
                Repaint();
            }

            if (GUILayout.Button("...", GUILayout.Width(30)))
            {
                var path = EditorUtility.SaveFilePanel("LEKB File", "", "SECRET_KEY", "lekb");
                if (!string.IsNullOrEmpty(path))
                {
                    PackageStableKey = path;
                    Repaint();
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Clear"))
            {
                PackageStableKey = "";
                Repaint();
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();

            // Optional key generation
            GUILayout.Space(20);

            EditorGUILayout.LabelField("By selecting 'Generate Key', the LEKB File above will be overwrite. Any pre-existing key data will be lost.", EditorStyles.wordWrappedLabel);

            GUILayout.Space(5);

            if (GUILayout.Button("Generate Key"))
            {
                GenerateNewKeyFile();
            }

            GUILayout.EndVertical();
        }

        private void GenerateNewKeyFile()
        {
            if (string.IsNullOrEmpty(PackageStableKey))
            {
                EditorUtility.DisplayDialog("Error", "Escrowed Key Blob (LEKB) file not set.", "OK");
                return;
            }

            if (File.Exists(PackageStableKey))
            {
                bool proceed = EditorUtility.DisplayDialog("Warning",
                    "LEKB file already exists. Do you want to overwrite the file?",
                    "Yes", "No");
                if (!proceed)
                {
                    return;
                }
            }

            var exitCode = 0;
            var outMessage = WindowsPlatformBuilder.ExecuteMakePkgProcess(
                out exitCode,
                $"genkey /ekb {PackageStableKey}",
                "",
                LogOpts.kNoOutput | LogOpts.kNoCommand);

            if (exitCode != 0)
            {
                throw new BuildFailedException($"FAILED - 'makepkg.exe genkey ...' (error code: {exitCode}) - See log below:\n\n{outMessage}");
            }

            Debug.Log($"'makepkg.exe genkey' completed without errors.\n\n{outMessage}");
            EditorUtility.DisplayDialog("Success", $"Escrowed Key Blob (LEKB) file written to: {PackageStableKey}", "OK");
        }
    }
}