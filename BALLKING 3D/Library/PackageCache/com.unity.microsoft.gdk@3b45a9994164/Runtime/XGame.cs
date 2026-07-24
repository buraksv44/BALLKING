// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    partial class SDK
    {
        public static Int32 XGameGetXboxTitleId(out UInt32 titleId)
        {
            return NativeMethods.XGameGetXboxTitleId(out titleId);
        }

        public static void XLaunchNewGame(string exePath, string args, XUserHandle defaultUser)
        {
            IntPtr userHandle = (defaultUser != null) ? defaultUser.Handle : IntPtr.Zero;

            NativeMethods.XLaunchNewGame(exePath, args, userHandle);
        }

        public static Int32 XLaunchRestartOnCrash(string args, UInt32 reserved)
        {
            return NativeMethods.XLaunchRestartOnCrash(args, reserved);
        }
    }
}
