#if UNITY_EDITOR

using UnityEditor;

#endif

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools
{
    /// <summary>
    /// Project-wide GDK Settings
    /// </summary>
    /// <remarks>
    /// Project-wide configuration settings relating to Microsoft GDK development are collected and stored in this
    /// class. Only one settings object can active at any time. It can be accessed through the Project Settings window.
    /// </remarks>
    [CreateAssetMenu(fileName = "GDKSettings", menuName = "GDK/GDK Settings", order = 1)]
    public class GdkSettingsAsset : ScriptableObject
    {
        public enum ShellVisualsMode
        {
            // Does not run the 'MakePri.exe' command line too and will not attempt to copy resources
            Disabled = 0,

            // Runs the 'MakePri.exe' command line tool and attempts to copy the resources on success
            RunMakePriAndCopy = 1,

            // Will attempt to copy pre-built resources into the ongoing build but will not run 'MakePri.exe'
            CopyOnly = 2
        }

        [System.Serializable]
        public class PlatformGdkSettings
        {
            public RuntimePlatform runtimePlatform;

            public bool targetMicrosoftGdk = true;
            public GameConfigAsset gameConfigAsset = null;
            public ShellVisualsMode shellVisualsMode = ShellVisualsMode.Disabled;

            [Obsolete("Please do not use, will be removed in future. The MICROSOFT_GDK_SUPPORT define is now present when targetMicrosoftGdk is true", false)]
            public bool createScriptingDefine = true;

            public bool createPackage = false;
            public bool sideloadPackage = false;
        }

        public bool createGdkPlatformSettingsCs = true;
        public bool createGdkPlatformSettingsDefine = true;
        public string gdkPlatformSettingsCsNamespace = "";

        public List<PlatformGdkSettings> settingsList;

        public GdkSettingsAsset()
        {
#if UNITY_EDITOR
            AddDefaultSettingsPlatformIfNeeded(RuntimePlatform.WindowsPlayer);
#endif
        }

        public bool SettingsExistForRuntimePlatform(RuntimePlatform platform)
        {
            return SettingsForRuntimePlatform(platform, out _);
        }

        public bool SettingsForRuntimePlatform(RuntimePlatform platform, out PlatformGdkSettings platformSettings)
        {
            if (settingsList != null)
            {
                foreach (PlatformGdkSettings setting in settingsList)
                {
                    if (setting.runtimePlatform == platform)
                    {
                        platformSettings = setting;
                        return true;
                    }
                }
            }

            platformSettings = null;
            return false;
        }

#if UNITY_EDITOR
        public void AddDefaultSettingsPlatformIfNeeded(RuntimePlatform platform)
        {
            if (!SettingsExistForRuntimePlatform(platform))
            {
                PlatformGdkSettings defaultPlatformSettings = new PlatformGdkSettings
                {
                    runtimePlatform = platform,
                    targetMicrosoftGdk = true,
                    gameConfigAsset = null,
                    shellVisualsMode = ShellVisualsMode.Disabled,
                    createPackage = false,
                    sideloadPackage = false
                };

                if (settingsList == null)
                    settingsList = new List<PlatformGdkSettings>();

                settingsList.Add(defaultPlatformSettings);
            }
        }
#endif
        public IEnumerable<PlatformGdkSettings> GetPlatformSettingsList() => settingsList.AsReadOnly();
    }
}