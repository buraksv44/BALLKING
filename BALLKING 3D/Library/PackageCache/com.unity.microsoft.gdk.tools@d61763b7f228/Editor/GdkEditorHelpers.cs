// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

#if UNITY_2023_2_OR_NEWER
using UnityEditor.Build;
#endif

using Debug = UnityEngine.Debug;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    internal class GdkEditorHelpers : ScriptableObject
    {
        public const string k_scriptingDefine = "MICROSOFT_GDK_SUPPORT";

        internal static bool StartCmdProcessAsAdmin(string arguments)
        {
            var processStartInfo = new ProcessStartInfo("cmd.exe");
            processStartInfo.Arguments = arguments;
            processStartInfo.UseShellExecute = true;
            processStartInfo.RedirectStandardOutput = false;
            processStartInfo.RedirectStandardError = false;
            processStartInfo.Verb = "runas";

            try
            {
                var process = Process.Start(processStartInfo);
                process.WaitForExit();
                process.Close();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }

            return true;
        }

        internal static void LogError(string message)
        {
            UnityEngine.Debug.LogError(message);
        }

        internal static void SectionSeperator()
        {
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
        }

        public static bool ScriptingDefineExists(string customScriptingDefine, BuildTargetGroup targetGroup)
        {
            if (string.IsNullOrEmpty(customScriptingDefine))
                return false;

            customScriptingDefine = customScriptingDefine.ToUpper();

#if UNITY_2023_2_OR_NEWER
            string currentScriptingDefinesForGroup = PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(targetGroup)).ToUpper();
#else
            string currentScriptingDefinesForGroup = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup).ToUpper();
#endif

            if (!string.IsNullOrEmpty(currentScriptingDefinesForGroup) &&
                currentScriptingDefinesForGroup.Contains(customScriptingDefine))
            {
                return true;
            }

            return false;
        }

        public static void EnableScriptingDefine(string customScriptingDefine, BuildTargetGroup targetGroup)
        {
            if (ScriptingDefineExists(customScriptingDefine, targetGroup))
                return;

            customScriptingDefine = customScriptingDefine.ToUpper();

#if UNITY_2023_2_OR_NEWER
            string currentScriptingDefinesForGroup = PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(targetGroup));
#else
            string currentScriptingDefinesForGroup = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
#endif

            if (!string.IsNullOrEmpty(currentScriptingDefinesForGroup) &&
                currentScriptingDefinesForGroup[^1] != ';')
            {
                currentScriptingDefinesForGroup += ";";
            }

#if UNITY_2023_2_OR_NEWER
            PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(targetGroup), currentScriptingDefinesForGroup + customScriptingDefine + ";");
#else
            PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, currentScriptingDefinesForGroup + customScriptingDefine + ";");
#endif
        }

        public static void DisableScriptingDefine(string customScriptingDefine, BuildTargetGroup targetGroup)
        {
            if (!ScriptingDefineExists(customScriptingDefine, targetGroup))
                return;

            customScriptingDefine = customScriptingDefine.ToUpper();

#if UNITY_2023_2_OR_NEWER
            string currentScriptingDefinesForGroup = PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(targetGroup));
#else
            string currentScriptingDefinesForGroup = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
#endif

            string newScriptingDefineSymbolsForGroup = currentScriptingDefinesForGroup.Replace(customScriptingDefine + ";", "");
            newScriptingDefineSymbolsForGroup = newScriptingDefineSymbolsForGroup.Replace(customScriptingDefine, "");

#if UNITY_2023_2_OR_NEWER
            PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(targetGroup), newScriptingDefineSymbolsForGroup);
#else
            PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, newScriptingDefineSymbolsForGroup);
#endif
        }

        public static List<(string, string)> CopyFilesWithExtensions(string sourceDir, string destDir, string[] extensions)
        {
            List<(string, string)> sourceDestPathList = new List<(string, string)>();

            foreach (string sourceSubDirPath in Directory.GetDirectories(sourceDir))
            {
                string destSubDirPath = Path.Combine(destDir, sourceSubDirPath.Substring(sourceDir.Length + 1));
                var subDirPathList = CopyFilesWithExtensions(sourceSubDirPath, destSubDirPath, extensions);
                sourceDestPathList.AddRange(subDirPathList);
            }

            DirectoryInfo dirInfo = new DirectoryInfo(sourceDir);
            FileInfo[] files = dirInfo.GetFiles()
                .Where(file => extensions.Contains(file.Extension))
                .ToArray();

            // This will avoid creating empty directories at the destination
            if (files.Length == 0)
                return sourceDestPathList;

            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            foreach (FileInfo file in files)
            {
                string destFilePath = Path.Combine(destDir, file.Name);
                file.CopyTo(destFilePath, true);

                sourceDestPathList.Add((destFilePath, destFilePath.Substring("Temp//StagingArea".Length)));
            }

            return sourceDestPathList;
        }

        public static void ForceInspectorWindowRefresh()
        {
            System.Type inspectorWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.InspectorWindow");
            EditorWindow editorWindow = EditorWindow.GetWindow(inspectorWindowType);

            if (editorWindow != null)
            {
                editorWindow.Repaint();
            }
        }
    }
}
