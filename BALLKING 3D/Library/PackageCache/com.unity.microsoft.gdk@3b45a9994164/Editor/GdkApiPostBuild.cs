using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Unity.Microsoft.GDK.Editor
{
#if UNITY_EDITOR_WIN
    /// <summary>
    /// Handles post-build operations for the GDK API package.
    /// </summary>
    internal class GdkApiPostBuild : IPostprocessBuildWithReport
    {
        public int callbackOrder => 25;

        public void OnPostprocessBuild(BuildReport report)
        {
            // Current implementation treats the GdkEdition.asset as a temporary asset
            // created just for inclusion in the build and then removed.
            GdkEditionAssetGenerator.RemoveGdkEditionAsset();
        }
    }
#endif // UNITY_EDITOR_WIN
}