// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    partial class NativeMethods
    {
        // STDAPI XGameRuntimeInitialize();
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameRuntimeInitialize();

        //STDAPI XGameRuntimeInitializeWithOptions(_In_ const XGameRuntimeOptions* options);
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameRuntimeInitializeWithOptions(XGameRuntimeOptions options);

        // STDAPI_(void) XGameRuntimeUninitialize();
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XGameRuntimeUninitialize();
    }
}
