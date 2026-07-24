using System;
using UnityEditor;
using UnityEngine;

namespace Unity.Microsoft.GDK.Editor
{
#if UNITY_EDITOR_WIN
    internal static class GdkPlaymodeStateListener
    {
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.EnteredPlayMode:
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    GdkEditionAssetGenerator.RemoveGdkEditionAsset();
                    break;
                case PlayModeStateChange.EnteredEditMode:
                    break;
                case PlayModeStateChange.ExitingEditMode:
                    var edition = GdkPlugins.GetTrackedEditionNumber();
                    GdkEditionAssetGenerator.GenerateGdkEditionAsset(edition);
                    break;
                default:
                    GdkApiDebug.Log("Unity Editor OnPlayModeStateChanged: UNKNOWN STATE!");
                    break;
            }
        }
    }
#endif // UNITY_EDITOR_WIN
}