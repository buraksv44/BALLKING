using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;

namespace Unity.Microsoft.GDK.Discovery
{
    /// <summary>
    /// Provides methods to retrieve GDK (Game Development Kit) data from the Windows registry.
    /// </summary>
    public static class GdkRegistryData
    {
        /// <summary>
        /// Retrieves GDK data from the Windows registry.
        /// </summary>
        /// <returns>An IEnumerable of GdkData objects containing information about installed GDK editions.</returns>
        public static IEnumerable<GdkData> GetGDKData()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                yield break;

            using var registryKey = Registry.LocalMachine.OpenSubKey($@"{GetLocalMachineSoftwareRegPath()}\Microsoft\GDK");
            if (registryKey == null)
                yield break;

            foreach (var individualGDKEntryName in registryKey.GetSubKeyNames())
            {
                var individualGDKEntry = registryKey.OpenSubKey(individualGDKEntryName);

                var isGDKEdition = int.TryParse(individualGDKEntryName, NumberStyles.Any, CultureInfo.InvariantCulture, out var edition);
                if (!isGDKEdition)
                    continue;

                var grdkSubKey = individualGDKEntry?.OpenSubKey("GRDK");
                var xboxDevSubkey = individualGDKEntry?.OpenSubKey("XboxDev");
                var pcDevSubkey = individualGDKEntry?.OpenSubKey("PCDev");
                if (grdkSubKey?.GetValue("EditionVersionFriendlyName") == null
                    && xboxDevSubkey?.GetValue("EditionVersionFriendlyName") == null
                    && pcDevSubkey?.GetValue("EditionVersionFriendlyName") == null)
                    continue;

                var componentInstalledRootsKey = grdkSubKey.OpenSubKey("Installed Roots");
                var gdkRoot = (string)(componentInstalledRootsKey?.GetValue("GDKInstallPath") ?? "");
                if (string.IsNullOrEmpty(gdkRoot) || (!Directory.Exists(gdkRoot)))
                    continue;

                var componentPaths = new Dictionary<string, string>();
                foreach (var gdkComponentName in individualGDKEntry.GetSubKeyNames())
                {
                    var componentSubKey = individualGDKEntry.OpenSubKey(gdkComponentName);
                    var installPath = (string)(componentSubKey?.GetValue("InstallPath") ?? "");

                    if (string.IsNullOrEmpty(installPath) || (!Directory.Exists(installPath)))
                        continue;

                    componentPaths.Add(gdkComponentName, installPath);
                }

                yield return new GdkData{Edition = edition, Path = gdkRoot, ComponentPaths = componentPaths};
            }
        }


        private static string GetLocalMachineSoftwareRegPath()
        {
            return Environment.Is64BitOperatingSystem ? @"SOFTWARE\WOW6432Node" : @"SOFTWARE";
        }
    }
}
