// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    partial class NativeMethods
    {
        // STDAPI XGameEventWrite(_In_ XUserHandle user,
        //     _In_z_ const char *serviceConfigId,
        //     _In_z_ const char *playSessionId,
        //     _In_z_ const char* eventName,
        //     _In_opt_z_ const char* dimensionsJson,
        //     _In_opt_z_ const char* measurementsJson) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameEventWrite(IntPtr user,
            [MarshalAs(UnmanagedType.LPStr)] string serviceConfigId,
            [MarshalAs(UnmanagedType.LPStr)] string playSessionId,
            [MarshalAs(UnmanagedType.LPStr)] string eventName,
            [MarshalAs(UnmanagedType.LPStr)] string dimensionsJson,
            [MarshalAs(UnmanagedType.LPStr)] string measurementsJson);
    }
}