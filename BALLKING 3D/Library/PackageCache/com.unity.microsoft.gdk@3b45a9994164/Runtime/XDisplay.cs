using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    // enum class XDisplayHdrModeResult : uint32_t
    // {
    //     Unknown = 0,
    //     Enabled = 1,
    //     Disabled = 2
    // };
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XDisplayHdrModeResult : UInt32
    {
        Unknown = 0,
        Enabled = 1,
        Disabled = 2
    }

    // enum class XDisplayHdrModePreference : uint32_t
    // {
    //     PreferHdr = 0,
    //     PreferRefreshRate = 1
    // };
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XDisplayHdrModePreference : UInt32
    {
        PreferHdr = 0,
        PreferRefreshRate = 1
    };

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XDisplayHdrModeInfo
    {
        internal XDisplayHdrModeInfo(Interop.XDisplayHdrModeInfo interop)
        {
            this.interop = interop;
        }

        public XDisplayHdrModeInfo()
        {
            this.interop = new Interop.XDisplayHdrModeInfo();
        }

        internal Interop.XDisplayHdrModeInfo interop;

        public float MinToneMapLuminance
        {
            get => interop.minToneMapLuminance;
            set => interop.minToneMapLuminance = value;
        }

        public float MaxToneMapLuminance
        {
            get => interop.maxToneMapLuminance;
            set => interop.maxToneMapLuminance = value;
        }

        public float MaxFullFrameToneMapLuminance
        {
            get => interop.maxFullFrameToneMapLuminance;
            set => interop.maxFullFrameToneMapLuminance = value;
        }

        [Obsolete("Please use MinToneMapLuminance instead, (UnityUpgradable) -> MinToneMapLuminance", true)]
        public float minToneMapLuminance
        {
            get => interop.minToneMapLuminance;
            set => interop.minToneMapLuminance = value;
        }

        [Obsolete("Please use MaxToneMapLuminance instead, (UnityUpgradable) -> MaxToneMapLuminance", true)]
        public float maxToneMapLuminance
        {
            get => interop.maxToneMapLuminance;
            set => interop.maxToneMapLuminance = value;
        }

        [Obsolete("Please use MaxFullFrameToneMapLuminance instead, (UnityUpgradable) -> MaxFullFrameToneMapLuminance", true)]
        public float maxFullFrameToneMapLuminance
        {
            get => interop.maxFullFrameToneMapLuminance;
            set => interop.maxFullFrameToneMapLuminance = value;
        }
    };

    partial class SDK
    {
        public static XDisplayHdrModeResult XDisplayTryEnableHdrMode(XDisplayHdrModePreference displayModePreference, out XDisplayHdrModeInfo displayHdrModeInfo)
        {
            Interop.XDisplayHdrModeInfo displayHdrModeInfoInterop = default;

            XDisplayHdrModeResult result = NativeMethods.XDisplayTryEnableHdrMode(displayModePreference, out displayHdrModeInfoInterop);

            displayHdrModeInfo = new XDisplayHdrModeInfo(displayHdrModeInfoInterop);

            return result;
        }

        public static Int32 XDisplayAcquireTimeoutDeferral(out XDisplayTimeoutDeferralHandle handle)
        {
            handle = default(XDisplayTimeoutDeferralHandle);

            IntPtr interopHandle;
            Int32 hr = NativeMethods.XDisplayAcquireTimeoutDeferral(out interopHandle);

            if(HR.SUCCEEDED(hr))
            {
                handle = new XDisplayTimeoutDeferralHandle(interopHandle);
            }

            return hr;
        }

        public static void XDisplayCloseTimeoutDeferralHandle(XDisplayTimeoutDeferralHandle handle)
        {
            handle.Close();
        }
    }
}
