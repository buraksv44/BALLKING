// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    partial class NativeMethods
    {
        //STDAPI_(void) XThreadAssertNotTimeSensitive() noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XThreadAssertNotTimeSensitive();

        //STDAPI_(bool) XThreadIsTimeSensitive() noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XThreadIsTimeSensitive();

        //STDAPI XThreadSetTimeSensitive(_In_ bool isTimeSensitiveThread) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern int XThreadSetTimeSensitive([MarshalAs(UnmanagedType.I1)] bool isTimeSensitiveThread);
    }
}
