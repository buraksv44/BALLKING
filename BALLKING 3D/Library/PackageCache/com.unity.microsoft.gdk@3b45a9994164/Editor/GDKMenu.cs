using UnityEditor;
using UnityEngine;

namespace Unity.Microsoft.GDK.Editor
{
    internal static class GDKMenu
    {
        [MenuItem("GDK/Documentation/Developer Portal (GDK)")]
        private static void OpenDeveloperPortal()
        {
            Application.OpenURL("https://aka.ms/gamedevdocs");
        }
    }
}