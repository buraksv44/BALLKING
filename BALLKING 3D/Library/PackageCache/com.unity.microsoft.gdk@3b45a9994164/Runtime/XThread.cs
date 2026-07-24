// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    partial class SDK
    {
        public static void XThreadAssertNotTimeSensitive()
        {
            NativeMethods.XThreadAssertNotTimeSensitive();
        }

        public static bool XThreadIsTimeSensitive()
        {
            return NativeMethods.XThreadIsTimeSensitive();
        }

        public static int XThreadSetTimeSensitive(bool isTimeSensitiveThread)
        {
            return NativeMethods.XThreadSetTimeSensitive(isTimeSensitiveThread);
        }
    }
}
