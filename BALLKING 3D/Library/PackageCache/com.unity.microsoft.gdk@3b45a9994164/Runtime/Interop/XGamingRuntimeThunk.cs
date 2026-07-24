// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    public static class XGamingRuntimeInterop
    {
        #if (!UNITY_STANDALONE_WIN && ENABLE_IL2CPP)
            public const string ThunkDllName = "__Internal";
        #else
            public const string ThunkDllName = "XGameRuntime.Thunks";
        #endif
    }
}
