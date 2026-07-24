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
    // enum class XGameRuntimeFeature : uint32_t
    // {
    //     XAccessibility               = 0,
    //     XAppCapture                  = 1,
    //     XAsync                       = 2,
    //     XAsyncProvider               = 3,
    //     XDisplay                     = 4,
    //     XGame                        = 5,
    //     XGameInvite                  = 6,
    //     XGameSave                    = 7,
    //     XGameUI                      = 8,
    //     XLauncher                    = 9,
    //     XNetworking                  = 10,
    //     XPackage                     = 11,
    //     XPersistentLocalStorage      = 12,
    //     XSpeechSynthesizer           = 13,
    //     XStore                       = 14,
    //     XSystem                      = 15,
    //     XTaskQueue                   = 16,
    //     XThread                      = 17,
    //     XUser                        = 18,
    //     XError                       = 19,
    //     XGameEvent                   = 20,
    //     XGameStreaming               = 21,
    // };
    public enum XGameRuntimeFeature : UInt32
    {
        XAccessibility               = 0,
        XAppCapture                  = 1,
        XAsync                       = 2,
        XAsyncProvider               = 3,
        XDisplay                     = 4,
        XGame                        = 5,
        XGameInvite                  = 6,
        XGameSave                    = 7,
        XGameUI                      = 8,
        XLauncher                    = 9,
        XNetworking                  = 10,
        XPackage                     = 11,
        XPersistentLocalStorage      = 12,
        XSpeechSynthesizer           = 13,
        XStore                       = 14,
        XSystem                      = 15,
        XTaskQueue                   = 16,
        XThread                      = 17,
        XUser                        = 18,
        XError                       = 19,
        XGameEvent                   = 20,
        XGameStreaming               = 21,
    };

    partial class SDK
    {
        public static bool XGameRuntimeIsFeatureAvailable(XGameRuntimeFeature feature)
        {
            return NativeMethods.XGameRuntimeIsFeatureAvailable(feature);
        }
    }
}