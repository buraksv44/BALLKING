// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;
using System.Text;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XSystemDeviceType : uint32_t {
    //    Unknown              = 0x00,
    //    Pc                   = 0x01,
    //    XboxOne              = 0x02,
    //    XboxOneS             = 0x03,
    //    XboxOneX             = 0x04,
    //    XboxOneXDevkit       = 0x05,
    //    XboxScarlettLockhart = 0x06,
    //    XboxScarlettAnaconda = 0x07,
    //    XboxScarlettDevkit   = 0x08
    //};
    public enum XSystemDeviceType : UInt32
    {
        Unknown = 0x00,
        Pc = 0x01,
        XboxOne = 0x02,
        XboxOneS = 0x03,
        XboxOneX = 0x04,
        XboxOneXDevkit = 0x05,
        XboxScarlettLockhart = 0x06,
        XboxScarlettAnaconda = 0x07,
        XboxScarlettDevkit = 0x08
    }

    //enum class XSystemHandleType : uint32_t
    //{
    //    AppCaptureScreenshotStream = 0x00,
    //    DisplayTimeoutDeferral     = 0x01,
    //    GameSaveContainer          = 0x02,
    //    GameSaveProvider           = 0x03,
    //    GameSaveUpdate             = 0x04,
    //    PackageInstallationMonitor = 0x05,
    //    PackageMount               = 0x06,
    //    SpeechSynthesizer          = 0x07,
    //    SpeechSynthesizerStream    = 0x08,
    //    StoreContext               = 0x09,
    //    StoreLicense               = 0x0a,
    //    StoreProductQuery          = 0x0b,
    //    TaskQueue                  = 0x0c,
    //    User                       = 0x0d,
    //    UserSignOutDeferral        = 0x0e,
    //    GameUiTextEntry            = 0x0f,
    //};
    public enum XSystemHandleType : UInt32
    {
        AppCaptureScreenshotStream = 0x00,
        DisplayTimeoutDeferral = 0x01,
        GameSaveContainer = 0x02,
        GameSaveProvider = 0x03,
        GameSaveUpdate = 0x04,
        PackageInstallationMonitor = 0x05,
        PackageMount = 0x06,
        SpeechSynthesizer = 0x07,
        SpeechSynthesizerStream = 0x08,
        StoreContext = 0x09,
        StoreLicense = 0x0a,
        StoreProductQuery = 0x0b,
        TaskQueue = 0x0c,
        User = 0x0d,
        UserSignOutDeferral = 0x0e,
        GameUiTextEntry = 0x0f,
    }

    //enum class XSystemHandleCallbackReason : uint32_t
    //{
    //    Created   = 0x00,
    //    Destroyed = 0x01,
    //};
    public enum XSystemHandleCallbackReason : UInt32
    {
        Created = 0x00,
        Destroyed = 0x01,
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XVersion
    {
        internal XVersion(Interop.XVersion interop)
        {
            this.interop = interop;
        }

        public XVersion()
        {
            this.interop = new Interop.XVersion();
        }

        internal Interop.XVersion interop;

        public UInt16 Major
        {
            get => this.interop.major;
            set => this.interop.major = value;
        }

        public UInt16 Minor
        {
            get => this.interop.minor;
            set => this.interop.minor = value;
        }

        public UInt16 Build
        {
            get => this.interop.build;
            set => this.interop.build = value;
        }

        public UInt16 Revision
        {
            get => this.interop.revision;
            set => this.interop.revision = value;
        }

        public UInt64 Value
        {
            get => this.interop.Value;
            set => this.interop.Value = value;
        }
    }

    [MovedFrom("Unity.GameCore.Interop")]
    public class XSystemAnalyticsInfo
    {
        internal XSystemAnalyticsInfo(Interop.XSystemAnalyticsInfo interop)
        {
            this.interop = interop;
            _osVersion = new XVersion(this.interop.osVersion);
            _hostingOsVersion = new XVersion(this.interop.hostingOsVersion);
        }

        public XSystemAnalyticsInfo()
        {
            this.interop = new Interop.XSystemAnalyticsInfo();
            _osVersion = new XVersion();
            _hostingOsVersion = new XVersion();
        }

        internal Interop.XSystemAnalyticsInfo interop;
        private XVersion _osVersion;
        private XVersion _hostingOsVersion;

        public XVersion OsVersion
        {
            get => this._osVersion;
            set => this._osVersion = value;
        }

        public XVersion HostingOsVersion
        {
            get => this._hostingOsVersion;
            set => this._hostingOsVersion = value;
        }

        public string Family
        {
            get => this.interop.family;
            set => this.interop.family = value;
        }

        public string Form
        {
            get => this.interop.form;
            set => this.interop.form = value;
        }
    }

    public class XSystemRuntimeInfo
    {
        internal XSystemRuntimeInfo(Interop.XSystemRuntimeInfo interop)
        {
            _runtimeVersion = new XVersion(interop.runtimeVersion);
            _availableVersion = new XVersion(interop.availableVersion);
        }

        public XSystemRuntimeInfo()
        {
            _runtimeVersion = new XVersion();
            _availableVersion = new XVersion();
        }

        internal XVersion _runtimeVersion;
        internal XVersion _availableVersion;

        public XVersion RuntimeVersion
        {
            get => this._runtimeVersion;
            set => this._runtimeVersion = value;
        }

        public XVersion AvailableVersion
        {
            get => this._availableVersion;
            set => this._availableVersion = value;
        }
    }

    public delegate void XSystemHandleCallback(IntPtr handle,
        XSystemHandleType type,
        XSystemHandleCallbackReason reason,
        IntPtr context);

    partial class SDK
    {
        public static readonly UInt64 XSystemAppSpecificDeviceIdBytes = 45;
        public static readonly UInt64 XSystemConsoleIdBytes = 39;
        public static readonly UInt64 XSystemXboxLiveSandboxIdMaxBytes = 16;

        public static XSystemAnalyticsInfo XSystemGetAnalyticsInfo()
        {
            Interop.XSystemAnalyticsInfo interopInfo = default;
            NativeMethods.XSystemGetAnalyticsInfo(out interopInfo);
            return new XSystemAnalyticsInfo(interopInfo);
        }

        public static int XSystemGetAppSpecificDeviceId(out string appSpecificDeviceId)
        {
            appSpecificDeviceId = null;

            UInt64 appSpecificDeviceIdUsed;
            var sb = new StringBuilder((int)XSystemAppSpecificDeviceIdBytes);
            int hr = NativeMethods.XSystemGetAppSpecificDeviceId((ulong)sb.Capacity, sb, out appSpecificDeviceIdUsed);
            if (HR.SUCCEEDED(hr))
            {
                appSpecificDeviceId = sb.ToString();
            }

            return hr;
        }

        public static int XSystemGetConsoleId(out string consoleId)
        {
            consoleId = null;

            UInt64 consoleIdUsed;
            var sb = new StringBuilder((int)XSystemConsoleIdBytes);
            int hr = NativeMethods.XSystemGetConsoleId((ulong)sb.Capacity, sb, out consoleIdUsed);
            if (HR.SUCCEEDED(hr))
            {
                consoleId = sb.ToString();
            }

            return hr;
        }

        public static XSystemDeviceType XSystemGetDeviceType()
        {
            return NativeMethods.XSystemGetDeviceType();
        }

        public static int XSystemGetXboxLiveSandboxId(out string sandboxId)
        {
            sandboxId = null;

            UInt64 sandboxIdUsed;
            var sb = new StringBuilder((int)XSystemXboxLiveSandboxIdMaxBytes);
            int hr = NativeMethods.XSystemGetXboxLiveSandboxId((ulong)sb.Capacity, sb, out sandboxIdUsed);
            if (HR.SUCCEEDED(hr))
            {
                sandboxId = sb.ToString();
            }

            return hr;
        }

        public static XSystemRuntimeInfo XSystemGetRuntimeInfo()
        {
            return new XSystemRuntimeInfo(NativeMethods.XSystemGetRuntimeInfo());
        }

        public static bool XSystemIsHandleValid(IntPtr handle)
        {
            return NativeMethods.XSystemIsHandleValid(handle);
        }

        public static int XSystemHandleTrack(XSystemHandleCallback callback,
            IntPtr context,
            out XSystemHandleCallbackHandle handle)
        {
            Interop.XSystemHandleCallback interopCallback = (IntPtr handlePtr, XSystemHandleType type, XSystemHandleCallbackReason reason, IntPtr callbackContext) =>
            {
                callback(handlePtr, type, reason, callbackContext);
            };

            handle = new XSystemHandleCallbackHandle(interopCallback, context);
            int hr = NativeMethods.XSystemHandleTrack(handle.interop.StaticCallback, handle.interop.CallbackContext);

            if (HR.FAILED(hr))
            {
                handle.Dispose();
                handle = null;
            }

            return hr;
        }
    }

    public class XSystemHandleCallbackHandle
    {
        internal Interop.XSystemHandleCallbackHandle interop;


        internal XSystemHandleCallbackHandle(Interop.XSystemHandleCallback callback,
            IntPtr context)
        {
            interop = new Interop.XSystemHandleCallbackHandle(callback, context);
        }

        public bool IsValid { get { return interop.IsValid; } }

        public UInt64 Token
        {
            get => interop.Token;
            set => interop.Token = value;
        }

        public void Unregister()
        {
            interop.Unregister();
        }

        public void Dispose()
        {
            interop.Dispose();
        }
    }
}
