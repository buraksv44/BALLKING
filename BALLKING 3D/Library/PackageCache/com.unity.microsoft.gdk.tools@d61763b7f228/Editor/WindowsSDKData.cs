using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEditor.Build;

//#define USE_MICROSOFT_WIN32 --> (For some reason this isn't available, maybe mscorelib needs to be a dependency?)
#if USE_MICROSOFT_WIN32
using Microsoft.Win32;
#endif

namespace Unity.Microsoft.GDK.Tools.Editor
{
    internal class WindowsSDKData
    {
        /*
         * The discovery methods used here are based on the content of the file 'winsdk.bat' installed and used by
         * Visual Studio 2019 (\Microsoft Visual Studio\2019\Professional\Common7\Tools\vsdevcmd\core\winsdk.bat)
         */

        private static string installationFolder = null;
        private static string productVersion = null;
        private static string binPath = null;

        private static string primaryLocation = @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SDKs\Windows\v10.0";
        private static string alternateLocation = @"HKEY_CURRENT_USER\Software\Wow6432Node\Microsoft\Microsoft SDKs\Windows\v10.0";

#if !USE_MICROSOFT_WIN32
        // P/Invoke method to access the Windows Registry (as Microsoft.Win32 does not seem to be available)
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        private static extern int RegGetValue(
            IntPtr hKey, string subKey, string valueName, int flags, out uint type, System.Text.StringBuilder data, ref uint dataSize);

        // Constants for registry access
        private const int HKEY_CURRENT_USER = unchecked((int)0x80000001);
        private const int HKEY_LOCAL_MACHINE = unchecked((int)0x80000002);

        private const int ERROR_SUCCESS = 0;
#endif

        public static string GetInstallationFolder()
        {
            if (installationFolder == null)
            {
                installationFolder = GetRegistryKeyString("InstallationFolder", primaryLocation, alternateLocation);
            }

            return installationFolder;
        }

        public static string GetVersion()
        {
            if (productVersion == null)
            {
                var includePath = Path.Combine(GetInstallationFolder(), "include");
                var directoryList = Directory.GetDirectories(includePath);

                productVersion = directoryList.Select(fn => new DirectoryInfo(fn))
                    .Where(d => Version.TryParse(d.Name, out _))
                    .OrderByDescending(d => d.Name)
                    .FirstOrDefault()?.Name;
            }

            return productVersion;
        }

        public static string GetBinPath()
        {
            if (binPath == null)
            {
                if (string.IsNullOrEmpty(GetInstallationFolder()) || string.IsNullOrEmpty(GetVersion()))
                    return null;

                string architecture = GetSystemArchitecture();
                binPath = Path.Combine(GetInstallationFolder(), "bin", GetVersion(), architecture);
            }

            return binPath;
        }

        private static string GetSystemArchitecture()
        {
            switch (RuntimeInformation.ProcessArchitecture)
            {
                case Architecture.X86:
                    return "x86";
                case Architecture.X64:
                    return "x64";
                case Architecture.Arm:
                    return "arm";
                case Architecture.Arm64:
                    return "arm64";
                default:
                    throw new PlatformNotSupportedException($"Unsupported architecture: {RuntimeInformation.ProcessArchitecture}");
            }
        }

        private static string GetValue(string keyName, string valueName, object defaultValue)
        {
#if USE_MICROSOFT_WIN32
            return (string)Microsoft.Win32.Registry.GetValue(keyName, valueName, defaultValue);
#else
            // Read the value from the registry
            uint dataType;
            uint dataSize = 1024;

            StringBuilder data = new StringBuilder((int)dataSize);

            IntPtr hKey = IntPtr.Zero;
            if (keyName.StartsWith(@"HKEY_LOCAL_MACHINE\"))
            {
                hKey = new IntPtr(HKEY_LOCAL_MACHINE);
                keyName = keyName.Replace(@"HKEY_LOCAL_MACHINE\", "");
            }
            else if (keyName.StartsWith(@"HKEY_CURRENT_USER\"))
            {
                hKey = new IntPtr(HKEY_CURRENT_USER);
                keyName = keyName.Replace(@"HKEY_CURRENT_USER\", "");
            }
            else
            {
                // This ain't expected!
                return defaultValue != null ? defaultValue.ToString() : null;
            }

            int result = RegGetValue(hKey, keyName, valueName, 0x00000002 /*RRF_RT_REG_SZ*/, out dataType, data, ref dataSize);

            if (result == ERROR_SUCCESS)
            {
                return data.ToString();
            }

            // Return the defaultValue if RegGetValue fails.
            return defaultValue != null ? defaultValue.ToString() : null;
#endif
        }

        public static string GetRegistryKeyString(string key, string primaryLocation, string alternateLocation = @"")
        {
            string val = GetValue(primaryLocation, key, null);
            if (val == null)
            {
                val = GetValue(alternateLocation, key, null);
            }
            return val;
        }
    }
}