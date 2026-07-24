// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    partial class NativeMethods
    {
        // STDAPI_(bool) XGameRuntimeIsFeatureAvailable(XGameRuntimeFeature feature) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool XGameRuntimeIsFeatureAvailable(XGameRuntimeFeature feature);

    }
}