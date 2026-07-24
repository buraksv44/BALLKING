using System;
using System.IO;
using System.Linq;
using Unity.Microsoft.GDK.Discovery;
using UnityEditor;

#if UNITY_6000_5_OR_NEWER
using UnityEngine.Assemblies;
#endif

namespace Unity.Microsoft.GDK.Tools.Editor
{
    public static class GdkUtility
    {
        private const string k_MicrosoftGdkApiPackage = "Unity.Microsoft.GDK.Editor";
        private static bool? s_IsMicrosoftGdkApiPackageInstalled;

        private static string _gdkToolsPackageRootPath;
        private static string _gdkToolsPath;

        internal static string GdkToolsPackageRootPath
        {
            get
            {
                if (!File.Exists(_gdkToolsPackageRootPath))
                {
                    _gdkToolsPackageRootPath = Path.GetFullPath(@"Packages/com.unity.microsoft.gdk.tools");
                }

                return _gdkToolsPackageRootPath;
            }
        }

        private static readonly GdkData? LatestGdk;
        static GdkUtility()
        {
            var installedGdks = GdkEnumerator.DiscoveredGdks.OrderByDescending(data => data.Edition).ToArray();
            LatestGdk = installedGdks.FirstOrDefault();
        }

        internal static string GdkToolsPath
        {
            get
            {
                var latestGdkPath = LatestGdk?.Path;
                if (!File.Exists(_gdkToolsPath) && !string.IsNullOrEmpty(latestGdkPath))
                {
                    _gdkToolsPath = Path.Combine(latestGdkPath, "bin");
                }

                return _gdkToolsPath;
            }
        }

        public static bool IsMicrosoftGdkApiPackageInstalled()
        {
            if (s_IsMicrosoftGdkApiPackageInstalled.HasValue)
            {
                return s_IsMicrosoftGdkApiPackageInstalled.Value;
            }

#if UNITY_6000_5_OR_NEWER
            var assemblies = CurrentAssemblies.GetLoadedAssemblies();
#else
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
#endif
            s_IsMicrosoftGdkApiPackageInstalled = assemblies
                .Any(assembly => assembly.GetName().Name.Equals(k_MicrosoftGdkApiPackage));

            return s_IsMicrosoftGdkApiPackageInstalled.Value;
        }
    }
}
