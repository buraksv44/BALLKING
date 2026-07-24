// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.CSharp;
using Unity.XGamingRuntime.Interop;
using UnityEngine;

namespace Unity.XGamingRuntime
{
    //enum class XGameRuntimeGameConfigSource : uint32_t
    //{
    //    Default,
    //    Inline,
    //    File
    //};
    public enum XGameRuntimeGameConfigSource : UInt32
    {
        Default = 0,
        Inline = 1,
        File = 2
    }

    //struct XGameRuntimeOptions
    //{
    //    XGameRuntimeGameConfigSource gameConfigSource;
    //    const char* gameConfig;
    //};
    [StructLayout(LayoutKind.Sequential)]
    public struct XGameRuntimeOptions
    {
        public XGameRuntimeGameConfigSource gameConfigSource;
        [MarshalAs(UnmanagedType.LPStr)]
        public string gameConfig;
    }

    partial class SDK
    {
        public static Int32 XGameRuntimeInitialize()
        {
#if UNITY_EDITOR
            try
            {
                // Play mode is only available in GDKs versions later than April 2025
                // early return if using ealier version
                if (GetGdkEdition() < 250400)
                {
                    string editorExePath = AppDomain.CurrentDomain.BaseDirectory;
                    string configFile = Path.Combine(editorExePath, "MicrosoftGame.Config");
                    if (!File.Exists(configFile))
                    {
                        Debug.LogWarning("Microsoft Game Config not found. See package com.unity.microsoft.gdk.tools documentation for play mode support.");
                    }

                    return NativeMethods.XGameRuntimeInitialize();
                }

                //  If any handle was close during the GC collect it will cause an implicit load of GRTS
                //  in that cause we uninitialize GRTS again. If it was already uninitialize it will ignore the call.
                SDK.XGameRuntimeUninitialize();

                var gameConfigPath =  Path.GetFullPath(PlayerPrefs.GetString("PlaymodeMgcPath", ""));

                var options = new XGameRuntimeOptions()
                {
                    gameConfigSource = XGameRuntimeGameConfigSource.File,
                    gameConfig = gameConfigPath
                };

                // Version: 10.0.26100.2279
                XVersion referenceVersion = new XVersion()
                {
                    Major = 10,
                    Minor = 0,
                    Build = 26100,
                    Revision = 2279
                };

                XSystemRuntimeInfo grtsVersion = SDK.XSystemGetRuntimeInfo();

                if (XVersionLessThan(referenceVersion, grtsVersion.RuntimeVersion))
                {
                    Debug.LogWarning("Your current Gaming Runtime Service version is not compatible with the Play Mode feature. Please update to the latest version from the Xbox Store to continue.");
                    return NativeMethods.XGameRuntimeInitialize();
                }

                Int32 hr = NativeMethods.XGameRuntimeInitializeWithOptions(options);

                // GRTS occasionally encounters this error, but it usually succeeds on the next attempt. So we try again
                if(hr == unchecked((Int32)0x8000FFFF))
                {
                    hr = NativeMethods.XGameRuntimeInitializeWithOptions(options);
                }

                if(hr == HR.E_GAMERUNTIME_OPTIONS_MISMATCH)
                {
                    Debug.LogWarning("XGameRuntimeInitialize failed to initialize in Play in Editor Mode due to the inability to unload previous Gaming runtime services " +
                        "components with different configurations. Please restart the editor to ensure proper initialization.");
                }

                return hr;
            }
            catch (EntryPointNotFoundException /*e*/)
            {
                // If the entry point for `XGameRuntimeInitializeWithOptions()` is missing,
                // fallback to the original behaviour calling `XGameRuntimeInitialize()`.
                // XGameRuntimeInitializeWithOptions() is only available in GDK Editions >= 241000

                return NativeMethods.XGameRuntimeInitialize();
            }
#else
            return NativeMethods.XGameRuntimeInitialize();
#endif
        }

        private static Int32 XGameRuntimeInitializeWithOptions(XGameRuntimeOptions options)
        {
            return NativeMethods.XGameRuntimeInitializeWithOptions(options);
        }

        public static void XGameRuntimeUninitialize()
        {
#if UNITY_EDITOR
            // Force GC to execute any finalizers from any missing handle that wasn't close
            GC.Collect();
            GC.WaitForPendingFinalizers();
#endif
            NativeMethods.XGameRuntimeUninitialize();
        }

        private static bool XVersionLessThan(XVersion version1, XVersion version2)
        {
            if (version1.Value == version2.Value)
            {
                return false;
            }

            if (version1.Major < version2.Major)
            {
                return false;
            }
            else if (version1.Minor < version2.Minor)
            {
                return false;
            }
            else if (version1.Build < version2.Build)
            {
                return false;
            }
            else if (version1.Revision < version2.Revision)
            {
                return false;
            }

            return true;
        }
    }
}