// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.CSharp;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    partial class SDK
    {
        public static Int32 XGameEventWrite(XUserHandle user,
            string serviceConfigId,
            string playSessionId,
            string eventName,
            string dimensionsJson,
            string measurementsJson)
        {
            IntPtr userHandle = ( user != null) ? user.Handle : IntPtr.Zero;

            return NativeMethods.XGameEventWrite(userHandle,
                serviceConfigId,
                playSessionId,
                eventName,
                dimensionsJson,
                measurementsJson);
        }
    }
}