using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    internal class GdkInstalledAppsEditorWindow : EditorWindow
    {
        // --- Data structures to match the JSON from "wdapp list /d /json" ---
        
        [Serializable]
        private class WdappListResult
        {
            public List<PackageData> Packages;
        }

        [Serializable]
        private class PackageData
        {
            public string PackageFullName;
            public string DisplayName;
            public string InstallPath;
            public string Logo;
            public string ContentId;
            public List<ApplicationData> Applications;
        }

        [Serializable]
        private class ApplicationData
        {
            public string Aumid;
        }

        // --- Flattened data structure for easy UI display ---

        private class DisplayAppData
        {
            public string Aumid;
            public string PackageFullName;
            public string DisplayName;
            public string InstallPath;
            public string LogoPath;
            public string ContentId;
            public Texture2D LogoTexture;
        }

        private List<DisplayAppData> m_InstalledApps;
        private Dictionary<string, Texture2D> m_LogoCache = new Dictionary<string, Texture2D>();
        private Vector2 m_ScrollPosition;
        private bool m_IsRefreshing;

        [MenuItem("GDK/PC/Sideloaded Packages", false, 101)]
        private static void ShowWindow()
        {
            var window = GetWindow<GdkInstalledAppsEditorWindow>();
            window.titleContent = new GUIContent("GDK Sideloaded Packages");
            window.minSize = new Vector2(600, 400);
            window.Show();
        }

        private void OnEnable()
        {
            RefreshInstalledAppsList();
        }

        private void OnDisable()
        {
            // Clean up loaded textures to prevent memory leaks when the window is closed.
            ClearTextureCache();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUILayout.Label("GDK Sideloaded Packages");
            GUILayout.FlexibleSpace();
            using (new EditorGUI.DisabledScope(m_IsRefreshing))
            {
                if (GUILayout.Button("Refresh", EditorStyles.toolbarButton))
                {
                    RefreshInstalledAppsList();
                }
            }
            EditorGUILayout.EndHorizontal();

            if (m_IsRefreshing)
            {
                EditorGUILayout.HelpBox("Refreshing list of installed applications...", MessageType.Info);
                return;
            }

            if (m_InstalledApps == null || m_InstalledApps.Count == 0)
            {
                EditorGUILayout.HelpBox(
                    "No GDK applications found, or failed to query the list. " +
                    "Ensure the GDK is installed and you are in the correct sandbox.", MessageType.Info);
                return;
            }

            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);

            foreach (var app in m_InstalledApps)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                // --- Title ---
                EditorGUILayout.LabelField(app.DisplayName, EditorStyles.boldLabel);
                EditorGUILayout.Space();

                // --- Main content area (Logo + Details) ---
                EditorGUILayout.BeginHorizontal();
                {
                    // Left Column: Logo
                    if (app.LogoTexture != null)
                    {
                        GUILayout.Label(app.LogoTexture, GUILayout.Width(80), GUILayout.Height(80));
                    }
                    else
                    {
                        GUILayout.Box(GUIContent.none, GUILayout.Width(80), GUILayout.Height(80));
                    }

                    // Right Column: Details
                    EditorGUILayout.BeginVertical();
                    {
                        DrawSelectableDetail("Install Path:", app.InstallPath);
                        DrawSelectableDetail("Content ID:", app.ContentId);
                        DrawSelectableDetail("Package Full Name:", app.PackageFullName);
                        DrawSelectableDetail("AUMID:", app.Aumid);
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();

                // --- Buttons Section ---
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();

                    bool isInstallPathValid = !string.IsNullOrEmpty(app.InstallPath) && Directory.Exists(app.InstallPath);
                    using (new EditorGUI.DisabledScope(!isInstallPathValid))
                    {
                        if (GUILayout.Button("Show in Explorer", GUILayout.Width(120)))
                        {
                            // This Unity API handily opens the folder in Explorer on Windows or Finder on macOS
                            EditorUtility.RevealInFinder(app.InstallPath);
                        }
                    }

                    if (GUILayout.Button("Launch", GUILayout.Width(100)))
                    {
                        ExecuteWdappCommand($"launch {app.Aumid}", $"Successfully launched {app.DisplayName}.");
                    }
                    if (GUILayout.Button("Terminate", GUILayout.Width(100)))
                    {
                        ExecuteWdappCommand($"terminate {app.PackageFullName}", $"Successfully terminated {app.DisplayName}.");
                    }
                    if (GUILayout.Button("Uninstall", GUILayout.Width(100)))
                    {
                        if (EditorUtility.DisplayDialog("Uninstall Application",
                            $"Are you sure you want to uninstall '{app.DisplayName}'?\n\n({app.PackageFullName})",
                            "Uninstall", "Cancel"))
                        {
                            if (ExecuteWdappCommand($"uninstall {app.PackageFullName}", $"Successfully uninstalled {app.DisplayName}."))
                            {
                                RefreshInstalledAppsList();
                            }
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }

            EditorGUILayout.EndScrollView();
        }


        // Add this new helper method to the GdkInstalledAppsEditorWindow class
        private void DrawSelectableDetail(string label, string value)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(120)); // Fixed width for alignment
            EditorGUILayout.SelectableLabel(value, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.EndHorizontal();
        }

        private void RefreshInstalledAppsList()
        {
            if (m_IsRefreshing)
            {
                return;
            }

            m_IsRefreshing = true;

            try
            {
                string gdkPath = GdkUtility.GdkToolsPath;
                if (string.IsNullOrEmpty(gdkPath))
                {
                    throw new Exception("Could not locate GDK tools path. Is the Microsoft GDK installed?");
                }

                string wdappCommand = Path.Combine(gdkPath, "wdapp.exe");
                if (!File.Exists(wdappCommand))
                {
                    throw new FileNotFoundException("Could not locate 'wdapp.exe'. Is the Microsoft GDK installed correctly?", wdappCommand);
                }

                var esp = new ExecuteSystemProcess(
                    wdappCommand,
                    "list /d /json",
                    string.Empty,
                    LogOpts.kProcessOutputOnly,
                    true,
                    0);

                int exitCode = esp.exitCode;
                string jsonOutput = esp.output;

                if (exitCode == 0 && !string.IsNullOrWhiteSpace(jsonOutput))
                {
                    var parsedResult = JsonUtility.FromJson<WdappListResult>(jsonOutput);
                    FlattenAndStoreAppList(parsedResult);
                }
                else
                {
                    Debug.LogError($"Failed to list GDK applications. 'wdapp.exe' exited with code {exitCode}:\n{jsonOutput}");
                    m_InstalledApps = new List<DisplayAppData>();
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"An exception occurred while refreshing the installed apps list: {e.Message}");
                m_InstalledApps = new List<DisplayAppData>();
            }
            finally
            {
                m_IsRefreshing = false;
                Repaint();
            }
        }

        private void FlattenAndStoreAppList(WdappListResult parsedResult)
        {
            ClearTextureCache();
            m_InstalledApps = new List<DisplayAppData>();
            if (parsedResult == null || parsedResult.Packages == null)
            {
                return;
            }

            foreach (var package in parsedResult.Packages)
            {
                if (package.Applications == null)
                {
                    continue;
                }

                foreach (var app in package.Applications)
                {
                    var appData = new DisplayAppData
                    {
                        DisplayName = package.DisplayName,
                        PackageFullName = package.PackageFullName,
                        Aumid = app.Aumid,
                        InstallPath = package.InstallPath,
                        LogoPath = package.Logo,
                        ContentId = package.ContentId
                    };

                    // Load and cache the logo texture from its absolute file path
                    if (!string.IsNullOrEmpty(appData.LogoPath) && File.Exists(appData.LogoPath))
                    {
                        if (!m_LogoCache.TryGetValue(appData.LogoPath, out appData.LogoTexture))
                        {
                            var texture = new Texture2D(2, 2); // Create a dummy texture
                            texture.LoadImage(File.ReadAllBytes(appData.LogoPath)); // Load the image data
                            appData.LogoTexture = texture;
                            m_LogoCache[appData.LogoPath] = texture;
                        }
                    }

                    m_InstalledApps.Add(appData);
                }
            }
        }

        private bool ExecuteWdappCommand(string arguments, string successMessage)
        {
            try
            {
                string gdkPath = GdkUtility.GdkToolsPath;
                if (string.IsNullOrEmpty(gdkPath))
                {
                    throw new Exception("Could not locate GDK tools path. Is the Microsoft GDK installed?");
                }

                string wdappCommandPath = Path.Combine(gdkPath, "wdapp.exe");
                if (!File.Exists(wdappCommandPath))
                {
                    throw new FileNotFoundException("Could not locate 'wdapp.exe'. Is the Microsoft GDK installed correctly?", wdappCommandPath);
                }

                var esp = new ExecuteSystemProcess(
                    wdappCommandPath,
                    arguments,
                    string.Empty,
                    LogOpts.kDefault,
                    true,
                    0);

                if (esp.exitCode == 0)
                {
                    Debug.Log(successMessage);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"An exception occurred while executing wdapp.exe: {e.Message}");
                return false;
            }
        }

        private void ClearTextureCache()
        {
            foreach (var texture in m_LogoCache.Values)
            {
                if (texture != null)
                {
                    DestroyImmediate(texture);
                }
            }
            m_LogoCache.Clear();
        }
    }
}
