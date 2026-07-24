using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    // struct XDisplayHdrModeInfo
    // {
    //     float minToneMapLuminance;
    //     float maxToneMapLuminance;
    //     float maxFullFrameToneMapLuminance;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XDisplayHdrModeInfo
    {
        internal float minToneMapLuminance;
        internal float maxToneMapLuminance;
        internal float maxFullFrameToneMapLuminance;
    };

    partial class NativeMethods
    {
        // STDAPI_(XDisplayHdrModeResult) XDisplayTryEnableHdrMode(
        //     _In_ XDisplayHdrModePreference displayModePreference,
        //     _Out_opt_ XDisplayHdrModeInfo* displayHdrModeInfo
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern XDisplayHdrModeResult XDisplayTryEnableHdrMode(XDisplayHdrModePreference displayModePreference, out XDisplayHdrModeInfo displayHdrModeInfo);

        // STDAPI XDisplayAcquireTimeoutDeferral(
        //     _Out_ XDisplayTimeoutDeferralHandle* handle
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XDisplayAcquireTimeoutDeferral(out IntPtr handle);

        // STDAPI_(void) XDisplayCloseTimeoutDeferralHandle(
        //     _In_ XDisplayTimeoutDeferralHandle handle
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XDisplayCloseTimeoutDeferralHandle(IntPtr handle);
    }
}
