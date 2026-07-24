using System;
using UnityEditor;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    [InitializeOnLoad]
    internal class WindowsPlatform : GdkPlatform
    {
        public override string DisplayName => "Windows";
        public override GUIContent TabTitle => k_TabTitle;
        public override uint TabOrder => 100;
        public override BuildTarget BuildTarget => BuildTarget.StandaloneWindows;
        public override RuntimePlatform RuntimePlatform => RuntimePlatform.WindowsPlayer;
        public override string PlatformCompilerConditional => "UNITY_STANDALONE_WIN";

        private static readonly Texture2D k_Image = SelectTabImage();
        private static readonly GUIContent k_TabTitle = EditorGUIUtility.TrTextContent("Windows", "Windows settings", k_Image);

        public override bool PlatformOnlySupportsGdk { get; } = false;

        internal static Texture2D SelectTabImage()
        {
            if (EditorGUIUtility.isProSkin)
            {
                // Dark theme
                return AssetDatabase.LoadAssetAtPath<Texture2D>("Packages/com.unity.microsoft.gdk.tools/Editor/Icons/Dark/Windows.png");
            }

            //Light theme
            return AssetDatabase.LoadAssetAtPath<Texture2D>("Packages/com.unity.microsoft.gdk.tools/Editor/Icons/Light/Windows.png");
        }

        static WindowsPlatform()
        {
            var platform = new WindowsPlatform
            {
                PlatformBuilder = new WindowsPlatformBuilder()
            };

            GDKPlatformManager.RegisterGdkPlatform(platform);
        }

        public override bool SupportsBuildTarget(BuildTarget buildTarget)
        {
            if (buildTarget == BuildTarget.StandaloneWindows ||
                buildTarget == BuildTarget.StandaloneWindows64)
            {
                return true;
            }

            return false;
        }

        public override bool SupportsBuildTargetGroup(BuildTargetGroup buildTargetGroup)
        {
            if (buildTargetGroup == BuildPipeline.GetBuildTargetGroup(BuildTarget))
            {
                return true;
            }

            return false;
        }

        private const string k_GdkEditionTrackerPath = @"Plugins/GDK/GdkEdition.json";
        private GdkEditionTracker m_GdkEditionTracker;

        private GdkEditionTracker EditionTracker => m_GdkEditionTracker ??= new GdkEditionTracker();

        public override int SelectedGdkEdition
        {
            get => EditionTracker.ReadGdkEdition();
            set => EditionTracker.WriteGdkEdition(value);
        }
    }
}