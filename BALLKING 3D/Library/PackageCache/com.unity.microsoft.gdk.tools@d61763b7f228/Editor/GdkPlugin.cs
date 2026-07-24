// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using UnityEditor;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    [InitializeOnLoad]
    internal static class GdkPlugin
    {
        [MenuItem("GDK/PC/Switch sandbox", false, 100)]
        private static void SwitchToTestSandbox()
        {
            GdkSwitchSandboxEditorWindow window = EditorWindow.GetWindow<GdkSwitchSandboxEditorWindow>();

            window.minSize = new Vector2(400, 200);
            window.maxSize = new Vector2(600, 400);

            GUIContent titleContent = new GUIContent("GDK - Switch PC Sandbox");

            window.titleContent = titleContent;
            window.Show();
        }

        // The GDK PC sub-menu item should always be active regardless of what the current active build target is (this was previously set to disable it which was confusing for users)
        [MenuItem("GDK/PC/Switch sandbox", true)]
        private static bool ValidateGdkPCSubMenu()
        {
            return true;
        }
    }
}