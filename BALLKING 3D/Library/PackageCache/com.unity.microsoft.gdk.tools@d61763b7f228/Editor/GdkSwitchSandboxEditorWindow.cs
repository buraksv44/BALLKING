// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;


namespace Unity.Microsoft.GDK.Tools.Editor
{
    internal class GdkSwitchSandboxEditorWindow : EditorWindow
    {
        private int selectedSandboxIndex;
        private string[] sandboxOptions;
        private string currentSandboxId;
        private List<string> availableSandboxes;
        private bool initialized;
        private string operationsSummary;

        private const string RETAIL_SANDBOX_ID = "RETAIL";

        private GameConfigAsset m_gameConfigAsset;

        private void Initialize()
        {
            //Vector2 scrollPos = Vector2.zero;
            //Vector2 scenesScrollPos = Vector2.zero;

            EditorStyles.textArea.wordWrap = true;

            initialized = true;
        }

        private void OnEnable()
        {
            RefreshSwitchSandboxDropdownOptions();
        }

        private void OnFocus()
        {
            RefreshSwitchSandboxDropdownOptions();
        }

        private void OnGUI()
        {
            if (!initialized)
            {
                Initialize();
            }

            EditorGUILayout.LabelField("Sandbox Selection", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField("Choose a development sandbox below and click 'Switch " +
                "Sandbox'. To return to normal mode, select RETAIL and 'Switch Sandbox' again.",
                EditorStyles.wordWrappedLabel);
            EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);

            currentSandboxId = XblPCSandbox.XblPCSandbox.GetSandbox();

            GUI.enabled = false;
            GUIContent currentSandboxContent = new GUIContent("Current Sandbox", "The currently active Sandbox on the system.");
            EditorGUILayout.TextField(currentSandboxContent, currentSandboxId);
            GUI.enabled = true;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.EndVertical();
            if (sandboxOptions != null && sandboxOptions.Length > 0)
            {
                GUIContent availableSandboxContent = new GUIContent("Available Sandbox", "The list of Sandboxes specified within the project's Microsoft Game Config file.");
                selectedSandboxIndex = EditorGUILayout.Popup(availableSandboxContent, selectedSandboxIndex, sandboxOptions);
                if (GUILayout.Button("Switch Sandbox"))
                {
                    try
                    {
                        string sandboxId = sandboxOptions[selectedSandboxIndex];
                        if(currentSandboxId != sandboxId)
                        {
                            SetSandbox(sandboxId);                       
                        }
                    }
                    catch (Exception ex)
                    {
                        UnityEngine.Debug.Log(ex.Message);
                    }
                }
            }
            EditorGUILayout.BeginVertical();
            EditorGUILayout.EndHorizontal();

            bool isGameConfigEditorButtonEnabled = true;
            var platformGdkSettings = GdkSettingsProvider.GetActiveGdkSettingsForBuildTarget(EditorUserBuildSettings.activeBuildTarget);
            if (platformGdkSettings == null)
            {
                // Handle no GDK Settings
                EditorGUILayout.HelpBox("Could not retrieve sandboxes from the Microsoft Game Config.\nActive GDK Settings not configured. Create and activate GDK Settings with a configured Microsoft Game Config.", MessageType.Warning);
                isGameConfigEditorButtonEnabled = false;
            }
            else if(GameConfigAsset.LoadAsXmlGameConfig(AssetDatabase.GetAssetPath(platformGdkSettings.gameConfigAsset)) == null) 
            {
                // Handle no MGC file
                EditorGUILayout.HelpBox("Could not retrieve sandboxes from the Microsoft Game Config.\nMicrosoft Game Config not found. Create and select a Microsoft Game Config within the Microsoft GDK Settings.", MessageType.Warning);
                isGameConfigEditorButtonEnabled = false;
            }
            else if (availableSandboxes == null || availableSandboxes.Count == 0 || (availableSandboxes.Count == 1 && availableSandboxes[0].Equals(RETAIL_SANDBOX_ID)))
            {
                // Handle empty MGC file
                EditorGUILayout.HelpBox("Could not retrieve sandboxes from the Microsoft Game Config.\nHave you used the \"Associate with the Microsoft Store\" option in the Microsoft Game Config editor?", MessageType.Warning);
            }
            GUI.enabled = isGameConfigEditorButtonEnabled;
            if (GUILayout.Button("Open MGC with Game Config Editor"))
            {
                string assetPath = AssetDatabase.GetAssetPath(m_gameConfigAsset);
                GameConfigHelper.OpenWithGameConfigEditor(assetPath);
            }
            GUI.enabled = true;

            EditorGUILayout.EndVertical();

            GdkEditorHelpers.SectionSeperator();

            // Microsoft Apps section
            EditorGUILayout.LabelField("Microsoft Applications", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.HelpBox("If you are experiencing issues with the Microsoft Store and/or Xbox Live detecting the correct Sandbox, you can restart the apps in the current Sandbox with the button below.", MessageType.Info);
            if (GUILayout.Button("Start Microsoft Applications"))
            {
                try
                {
                    XblPCSandbox.XblPCSandbox.RestartApps();
                    operationsSummary = "Successfully launched system apps in the current sandbox mode.";
                }
                catch (Exception ex)
                {
                    operationsSummary = "Failed to launch system apps in the current sandbox mode.\n\n" + ex.Message;
                    UnityEngine.Debug.Log(ex.Message);
                }
            }

            EditorGUILayout.EndVertical();
        }

        private void RefreshSwitchSandboxDropdownOptions()
        {
            currentSandboxId = XblPCSandbox.XblPCSandbox.GetSandbox();
            availableSandboxes = GetAvailableSandboxes();
            sandboxOptions = availableSandboxes.ToArray();

            selectedSandboxIndex = 0;
            if (availableSandboxes.IndexOf(currentSandboxId) != -1)
            {
                selectedSandboxIndex = availableSandboxes.IndexOf(currentSandboxId);
            }
        }

        private List<string> GetAvailableSandboxes()
        {
            List<string> sandboxes = new List<string>();
            sandboxes.Add(RETAIL_SANDBOX_ID);

            GdkSettingsAsset.PlatformGdkSettings platformGdkSettings =
                GdkSettingsProvider.GetActiveGdkSettingsForBuildTarget(EditorUserBuildSettings.activeBuildTarget);

            if (platformGdkSettings == null)
            {
                // With no active GDK settings, only RETAIL sandbox is available (A warning is shown in sandbox switcher window)
                return sandboxes;
            }

            m_gameConfigAsset = platformGdkSettings.gameConfigAsset;
            
            string gameConfigAssetPath = AssetDatabase.GetAssetPath(m_gameConfigAsset);
            Game xmlGameConfig = GameConfigAsset.LoadAsXmlGameConfig(gameConfigAssetPath);
            if(xmlGameConfig != null)
            {
                string sandboxesRawValue = "";

                if (xmlGameConfig.ExtendedAttributeList != null)
                {
                    // The query breaks if the list length is 0 ...
                    if (xmlGameConfig.ExtendedAttributeList.ExtendedAttribute.Length > 0)
                    {
                        var sandboxesExtendedAttributes = 
                            (from extendedAttribute in xmlGameConfig.ExtendedAttributeList.ExtendedAttribute
                             where extendedAttribute.Name == "SandboxIds"
                             select extendedAttribute);

                        if(sandboxesExtendedAttributes.Count() > 0)
                        {
                            sandboxesRawValue = sandboxesExtendedAttributes.First().Value ?? "";
                        }
                    }
                }

                string[] sandboxesAsStringArray = sandboxesRawValue.Split(',');
                sandboxes.AddRange(sandboxesAsStringArray);

                selectedSandboxIndex = sandboxes.IndexOf(currentSandboxId);
            }

            return sandboxes;
        }

        private bool SetSandbox(string sandboxId)
        {
            string arguments = sandboxId + " /noApps";
            string gdkPath = Unity.Microsoft.GDK.Tools.Editor.GdkUtility.GdkToolsPath;
            string workingDirectory = gdkPath;

            var processStartInfo = new ProcessStartInfo(Path.Combine(gdkPath, "XBLPCSandbox.exe"));
            processStartInfo.Arguments = arguments;
            processStartInfo.Verb = "runas";
            processStartInfo.WorkingDirectory = workingDirectory;
            processStartInfo.CreateNoWindow = true;

            try
            {
                var process = Process.Start(processStartInfo);
                process.WaitForExit();
                process.Close();

                currentSandboxId = XblPCSandbox.XblPCSandbox.GetSandbox();
                if (!sandboxId.Equals(currentSandboxId))
                {
                    throw new Exception("Requested sandbox != current sandbox.\nRequested sandbox - " + sandboxId + "\nCurrent sandbox - " + currentSandboxId);
                }

                RefreshSwitchSandboxDropdownOptions();

                operationsSummary = "Successfully set the sandbox to " + currentSandboxId;
            }
            catch (Exception e)
            {
                operationsSummary = "Failed to set the sandbox to " + sandboxId + "\n\n" + e.Message;
                GdkEditorHelpers.LogError(e.Message);
                return false;
            }

            return true;
        }
    }
}