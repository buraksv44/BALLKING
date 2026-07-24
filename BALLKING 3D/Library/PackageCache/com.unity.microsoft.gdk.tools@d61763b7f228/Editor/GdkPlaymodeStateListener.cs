using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Unity.Microsoft.GDK.Tools.Editor
{
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
                    //Debug.Log("Unity Editor OnPlayModeStateChanged: EnteredPlayMode");
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    //Debug.Log("Unity Editor OnPlayModeStateChanged: ExitingPlayMode");
                    break;
                case PlayModeStateChange.EnteredEditMode:
                    //Debug.Log("Unity Editor OnPlayModeStateChanged: EnteredEditMode");
                    PlayerPrefs.DeleteKey("PlaymodeMgcPath");
                    break;
                case PlayModeStateChange.ExitingEditMode:
                    //Debug.Log("Unity Editor OnPlayModeStateChanged: ExitingEditMode");

                    // When transitioning from 'EditMode -> Playmode'
                    // Make the path to the 'active' microsoft game configuration available for discovery.
                    // We will remove this on returning back to EditMode.

                    var activeGameConfig = GdkSettings.GetActiveGameConfig(EditorUserBuildSettings.activeBuildTarget);
                    if (activeGameConfig != null)
                    {
                        var mgcAssetPath = AssetDatabase.GetAssetPath(activeGameConfig);

                        PlayerPrefs.SetString("PlaymodeMgcPath", mgcAssetPath);
                        PlayerPrefs.Save();
                    }
                    break;
                default:
                    Debug.Log("Unity Editor OnPlayModeStateChanged: UNKNOWN STATE!");
                    break;
            }
        }
    }
}