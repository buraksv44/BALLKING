// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    partial class NativeMethods
    {
        //STDAPI XLaunchUri(
        //    _In_opt_ XUserHandle requestingUser,
        //    _In_z_ const char* uri
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XLaunchUri(IntPtr requestingUser,
            [MarshalAs(UnmanagedType.LPStr)] string uri);
    }
}
