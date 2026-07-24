// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Unity.XGamingRuntime.Interop
{
    //struct XVersion
    //{
    //    union
    //    {
    //        struct
    //        {
    //            uint16_t major;
    //            uint16_t minor;
    //            uint16_t build;
    //            uint16_t revision;
    //        };
    //        uint64_t Value;
    //    };
    //};
    [StructLayout(LayoutKind.Explicit)]
    internal struct XVersion
    {
        [FieldOffset(0)] internal UInt16 major;
        [FieldOffset(2)] internal UInt16 minor;
        [FieldOffset(4)] internal UInt16 build;
        [FieldOffset(6)] internal UInt16 revision;
        [FieldOffset(0)] internal UInt64 Value;
    }

    //struct XSystemAnalyticsInfo
    //{
    //    XVersion osVersion;
    //    XVersion hostingOsVersion;
    //    _Field_z_ char family[64];
    //    _Field_z_ char form[64];
    //};
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct XSystemAnalyticsInfo
    {
        internal XVersion osVersion;
        internal XVersion hostingOsVersion;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)] internal string family;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)] internal string form;
    }

    //struct XSystemRuntimeInfo
    //{
    //    XVersion runtimeVersion;
    //    XVersion availableVersion;
    //};
    internal struct XSystemRuntimeInfo
    {
        internal XVersion runtimeVersion;
        internal XVersion availableVersion;
    }

    //typedef void (CALLBACK *XSystemHandleCallback)(
    //    _In_ XSystemHandle handle,
    //    _In_ XSystemHandleType type,
    //    _In_ XSystemHandleCallbackReason reason,
    //    _In_ void* context
    //    );
    internal delegate void XSystemHandleCallback(IntPtr handle,
        XSystemHandleType type,
        XSystemHandleCallbackReason reason,
        IntPtr context);

    internal class XSystemHandleCallbackHandle : XRegistrationToken<Interop.XSystemHandleCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XSystemHandleCallback))]
        static void OnHandle(IntPtr handle,
            XSystemHandleType type,
            XSystemHandleCallbackReason reason,
            IntPtr context)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XSystemHandleCallback>;
            wrapper.Callback(handle, type, reason, wrapper.Context);
        }

        public XSystemHandleCallbackHandle(Interop.XSystemHandleCallback callback, IntPtr context) :
            base(callback, context, OnHandle)
        { }

        public void Unregister()
        {
            NativeMethods.XSystemHandleTrack(null, IntPtr.Zero);
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister();
        }
    }

    partial class NativeMethods
    {
        //STDAPI_(XSystemAnalyticsInfo) XSystemGetAnalyticsInfo() noexcept;
        // Note: ABI rules translate the return value to a hidden first out pointer parameter
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XSystemGetAnalyticsInfo(out XSystemAnalyticsInfo info);

        //STDAPI XSystemGetAppSpecificDeviceId(
        //    _In_ size_t appSpecificDeviceIdSize,
        //    _Out_writes_bytes_to_(appSpecificDeviceIdSize, *appSpecificDeviceIdUsed) char* appSpecificDeviceId,
        //    _Out_opt_ size_t* appSpecificDeviceIdUsed
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSystemGetAppSpecificDeviceId(UInt64 appSpecificDeviceIdSize,
            StringBuilder appSpecificDeviceId,
            out UInt64 appSpecificDeviceIdUsed);

        //STDAPI XSystemGetConsoleId(
        //    _In_ size_t consoleIdSize,
        //    _Out_writes_bytes_to_(consoleIdSize, *consoleIdUsed) char* consoleId,
        //    _Out_opt_ size_t* consoleIdUsed
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSystemGetConsoleId(UInt64 consoleIdSize,
            StringBuilder consoleId,
            out UInt64 consoleIdUsed);

        //STDAPI_(XSystemDeviceType) XSystemGetDeviceType() noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern XSystemDeviceType XSystemGetDeviceType();

        //STDAPI XSystemGetXboxLiveSandboxId(
        //    _In_ size_t sandboxIdSize,
        //    _Out_writes_bytes_to_(sandboxIdSize, *sandboxIdUsed) char* sandboxId,
        //    _Out_opt_ size_t* sandboxIdUsed
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSystemGetXboxLiveSandboxId(UInt64 sandboxIdSize,
            StringBuilder sandboxId,
            out UInt64 sandboxIdUsed);

        //STDAPI_(XSystemRuntimeInfo) XSystemGetRuntimeInfo() noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern XSystemRuntimeInfo XSystemGetRuntimeInfo();

        //STDAPI_(bool) XSystemIsHandleValid(
        //    _In_ XSystemHandle handle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XSystemIsHandleValid(IntPtr handle);

        //STDAPI XSystemHandleTrack(
        //    _In_ XSystemHandleCallback callback,
        //    _In_opt_ void* context
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSystemHandleTrack(XSystemHandleCallback callback, IntPtr context);
    }
}
