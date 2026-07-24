// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    // enum class XAppCaptureScreenshotFormatFlag : uint16_t
    // {
    //     SDR = 1,
    //     HDR = 2
    // };
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XAppCaptureScreenshotFormatFlag : UInt16
    {
        SDR = 1,
        HDR = 2
    };

    // enum class XAppCaptureMetadataPriority : uint8_t
    // {
    //     Informational = 0,
    //     Important,
    // };
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XAppCaptureMetadataPriority : byte
    {
        Informational = 0,
        Important,
    };

    // enum class XAppCaptureVideoEncoding : uint8_t
    // {
    //     H264 = 0,
    //     HEVC = 1
    // };
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XAppCaptureVideoEncoding : byte
    {
        H264 = 0,
        HEVC = 1
    };

    // enum class XAppCaptureVideoColorFormat : uint8_t
    // {
    //     SDR = 0,
    //     HDR = 1
    // };
    public enum XAppCaptureVideoColorFormat : byte
    {
        SDR = 0,
        HDR = 1
    };

    [Obsolete("class XAppScreenshotLocalId will be removed in future releases.", false)]
    [System.Serializable]
    public class XAppScreenshotLocalId
    {
        public byte[] Value {get;}

        public XAppScreenshotLocalId( byte[] value )
        {
            Value = value;
        }
    }

    public class XIsAppBroadcastingChangedRegistrationToken
    {
        internal Interop.XIsAppBroadcastingChangedRegistrationToken interop { get; private set; }

        internal XIsAppBroadcastingChangedRegistrationToken(Interop.XAppBroadcastMonitorCallback callback, IntPtr context)
        {
            interop = new Interop.XIsAppBroadcastingChangedRegistrationToken(callback, context);
        }

        public UInt64 Token
        {
            get => interop.Token;
            set => interop.Token = value;
        }

        public bool IsValid { get { return interop.IsValid; } }

        public bool Unregister(bool wait)
        {
            return interop.Unregister(wait);
        }
    };

    public class XMetadataPurgedToken
    {
        internal Interop.XMetadataPurgedToken interop { get; private set; }

        internal XMetadataPurgedToken(Interop.XAppCaptureMetadataPurgedCallback callback, IntPtr context)
        {
            interop = new Interop.XMetadataPurgedToken(callback, context);
        }

        public bool IsValid { get { return interop.IsValid; } }

        public UInt64 Token
        {
            get => interop.Token;
            set => interop.Token = value;
        }

        public bool Unregister(bool wait)
        {
            return interop.Unregister(wait);
        }
    }

    public class XAppCaptureLocalResult
    {
        internal XAppCaptureLocalResult(Interop.XAppCaptureLocalResult interop)
        {
            _clipHandle = new XAppCaptureLocalStreamHandle(interop.clipHandle);
            _clipStartTimestamp = new SYSTEMTIME(interop.clipStartTimestamp);
            this.interop = interop;
        }

        internal XAppCaptureLocalStreamHandle _clipHandle;
        internal SYSTEMTIME _clipStartTimestamp;
        internal Interop.XAppCaptureLocalResult interop;

        public XAppCaptureLocalStreamHandle ClipHandle
        {
            get => _clipHandle;
            set => _clipHandle = value;
        }

        public ulong FileSizeInBytes
        {
            get => interop.fileSizeInBytes;
            set => interop.fileSizeInBytes = value;
        }

        public SYSTEMTIME ClipStartTimestamp
        {
            get => _clipStartTimestamp;
            set => _clipStartTimestamp = value;
        }

        public UInt64 DurationInMilliseconds
        {
            get => interop.durationInMilliseconds;
            set => interop.durationInMilliseconds = value;
        }

        public UInt32 Width
        {
            get => interop.width;
            set => interop.width = value;
        }

        public UInt32 Height
        {
            get => interop.height;
            set => interop.height = value;
        }

        public XAppCaptureVideoEncoding Encoding
        {
            get => interop.encoding;
            set => interop.encoding = value;
        }

        public XAppCaptureVideoColorFormat ColorFormat
        {
            get => interop.colorFormat;
            set => interop.colorFormat = value;
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XAppCaptureDiagnosticScreenshotResult
    {
        public XAppCaptureScreenshotFile[] Files { get; set; }

        internal XAppCaptureDiagnosticScreenshotResult(Interop.XAppCaptureDiagnosticScreenshotResult interop)
        {
            var results = new List<XAppCaptureScreenshotFile>();

            for (int i = 0; i < interop.fileCount; i++)
            {
                XAppCaptureScreenshotFile screenshotFile = new XAppCaptureScreenshotFile(interop.files[i]);

                results.Add(screenshotFile);
            }

            Files = results.ToArray();
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XAppBroadcastStatus
    {
        internal XAppBroadcastStatus(Interop.XAppBroadcastStatus interop)
        {
            this.interop = interop;
        }

        public XAppBroadcastStatus()
        {
            this.interop = new Interop.XAppBroadcastStatus();
        }

        internal Interop.XAppBroadcastStatus interop;

        public bool CanStartBroadcast
        {
            get => interop.canStartBroadcast;
            set => interop.canStartBroadcast = value;
        }

        public bool IsAnyAppBroadcasting
        {
            get => interop.isAnyAppBroadcasting;
            set => interop.isAnyAppBroadcasting = value;
        }

        public bool IsCaptureResourceUnavailable
        {
            get => interop.isCaptureResourceUnavailable;
            set => interop.isCaptureResourceUnavailable = value;
        }

        public bool IsGameStreamInProgress
        {
            get => interop.isGameStreamInProgress;
            set => interop.isGameStreamInProgress = value;
        }

        public bool IsGpuConstrained
        {
            get => interop.isGpuConstrained;
            set => interop.isGpuConstrained = value;
        }

        public bool IsAppInactive
        {
            get => interop.isAppInactive;
            set => interop.isAppInactive = value;
        }

        public bool IsBlockedForApp
        {
            get => interop.isBlockedForApp;
            set => interop.isBlockedForApp = value;
        }

        public bool IsDisabledByUser
        {
            get => interop.isDisabledByUser;
            set => interop.isDisabledByUser = value;
        }

        public bool IsDisabledBySystem
        {
            get => interop.isDisabledBySystem;
            set => interop.isDisabledBySystem = value;
        }

        [Obsolete("Please use CanStartBroadcast instead, (UnityUpgradable) -> CanStartBroadcast", true)]
        public bool canStartBroadcast
        {
            get => interop.canStartBroadcast;
            set => interop.canStartBroadcast = value;
        }

        [Obsolete("Please use IsAnyAppBroadcasting instead, (UnityUpgradable) -> IsAnyAppBroadcasting", true)]
        public bool isAnyAppBroadcasting
        {
            get => interop.isAnyAppBroadcasting;
            set => interop.isAnyAppBroadcasting = value;
        }

        [Obsolete("Please use IsCaptureResourceUnavailable instead, (UnityUpgradable) -> IsCaptureResourceUnavailable", true)]
        public bool isCaptureResourceUnavailable
        {
            get => interop.isCaptureResourceUnavailable;
            set => interop.isCaptureResourceUnavailable = value;
        }

        [Obsolete("Please use IsGameStreamInProgress instead, (UnityUpgradable) -> IsGameStreamInProgress", true)]
        public bool isGameStreamInProgress
        {
            get => interop.isGameStreamInProgress;
            set => interop.isGameStreamInProgress = value;
        }

        [Obsolete("Please use IsGpuConstrained instead, (UnityUpgradable) -> IsGpuConstrained", true)]
        public bool isGpuConstrained
        {
            get => interop.isGpuConstrained;
            set => interop.isGpuConstrained = value;
        }

        [Obsolete("Please use IsAppInactive instead, (UnityUpgradable) -> IsAppInactive", true)]
        public bool isAppInactive
        {
            get => interop.isAppInactive;
            set => interop.isAppInactive = value;
        }

        [Obsolete("Please use IsBlockedForApp instead, (UnityUpgradable) -> IsBlockedForApp", true)]
        public bool isBlockedForApp
        {
            get => interop.isBlockedForApp;
            set => interop.isBlockedForApp = value;
        }

        [Obsolete("Please use IsDisabledByUser instead, (UnityUpgradable) -> IsDisabledByUser", true)]
        public bool isDisabledByUser
        {
            get => interop.isDisabledByUser;
            set => interop.isDisabledByUser = value;
        }

        [Obsolete("Please use IsDisabledBySystem instead, (UnityUpgradable) -> IsDisabledBySystem", true)]
        public bool isDisabledBySystem
        {
            get => interop.isDisabledBySystem;
            set => interop.isDisabledBySystem = value;
        }
    };

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XAppCaptureTakeScreenshotResult
    {
        internal XAppCaptureTakeScreenshotResult(Interop.XAppCaptureTakeScreenshotResult interop)
        {
            data = interop;
        }

        public XAppCaptureTakeScreenshotResult()
        {
            data = new Interop.XAppCaptureTakeScreenshotResult();
        }

        internal Interop.XAppCaptureTakeScreenshotResult data;

        public string LocalId
        {
            get => data.localId;
            set => data.localId = value;
        }
        public XAppCaptureScreenshotFormatFlag AvailableScreenshotFormats
        {
            get => data.availableScreenshotFormats;
            set => data.availableScreenshotFormats = value;
        }

        [Obsolete("XAppScreenshotLocalId will be removed in future releases. Use XAppCaptureTakeScreenshotResult.localId",false)]
        public XAppScreenshotLocalId Id
        {
            get => new XAppScreenshotLocalId(System.Text.Encoding.UTF8.GetBytes(data.localId));
        }
    };

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XAppCaptureRecordClipResult
    {
        internal XAppCaptureRecordClipResult(Interop.XAppCaptureRecordClipResult interop)
        {
            this.interop = interop;
        }

        public XAppCaptureRecordClipResult()
        {
            interop = new Interop.XAppCaptureRecordClipResult();
        }

        internal Interop.XAppCaptureRecordClipResult interop;

        public string Path
        {
            get => interop.path;
            set => interop.path = value;
        }

        public long FileSize
        {
            get => interop.fileSize;
            set => interop.fileSize = value;
        }

        public long StartTime
        {
            get => interop.startTime;
            set => interop.startTime = value;
        }

        public UInt32 DurationInMs
        {
            get => interop.durationInMs;
            set => interop.durationInMs = value;
        }

        public UInt32 Width
        {
            get => interop.width;
            set => interop.width = value;
        }

        public UInt32 Height
        {
            get => interop.height;
            set => interop.height = value;
        }

        public XAppCaptureVideoEncoding Encoding
        {
            get => interop.encoding;
            set => interop.encoding = value;
        }

        public UInt32 StartTimePreciseOffsetHns
        {
            get => interop.startTimePreciseOffsetHns;
            set => interop.startTimePreciseOffsetHns = value;
        }

        [Obsolete("Please use Path instead, (UnityUpgradable) -> Path", true)]
        public string path
        {
            get => interop.path;
            set => interop.path = value;
        }

        [Obsolete("Please use FileSize instead, (UnityUpgradable) -> FileSize", true)]
        public long fileSize
        {
            get => interop.fileSize;
            set => interop.fileSize = value;
        }

        [Obsolete("Please use StartTime instead, (UnityUpgradable) -> StartTime", true)]
        public long startTime
        {
            get => interop.startTime;
            set => interop.startTime = value;
        }

        [Obsolete("Please use DurationInMs instead, (UnityUpgradable) -> DurationInMs", true)]
        public UInt32 durationInMs
        {
            get => interop.durationInMs;
            set => interop.durationInMs = value;
        }

        [Obsolete("Please use Width instead, (UnityUpgradable) -> Width", true)]
        public UInt32 width
        {
            get => interop.width;
            set => interop.width = value;
        }

        [Obsolete("Please use Height instead, (UnityUpgradable) -> Height", true)]
        public UInt32 height
        {
            get => interop.height;
            set => interop.height = value;
        }

        [Obsolete("Please use Encoding instead, (UnityUpgradable) -> Encoding", true)]
        public XAppCaptureVideoEncoding encoding
        {
            get => interop.encoding;
            set => interop.encoding = value;
        }

        [Obsolete("Please use StartTimePreciseOffsetHns instead, (UnityUpgradable) -> StartTimePreciseOffsetHns", true)]
        public UInt32 startTimePreciseOffsetHns
        {
            get => interop.startTimePreciseOffsetHns;
            set => interop.startTimePreciseOffsetHns = value;
        }
    };

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XAppCaptureScreenshotFile
    {
        internal XAppCaptureScreenshotFile(Interop.XAppCaptureScreenshotFile interop)
        {
            this.interop = interop;
        }

        internal XAppCaptureScreenshotFile()
        {
            this.interop = new Interop.XAppCaptureScreenshotFile();
        }

        internal Interop.XAppCaptureScreenshotFile interop;

        public string Path
        {
            get => interop.path;
            set => interop.path = value;
        }

        public long FileSize
        {
            get => interop.fileSize;
            set => interop.fileSize = value;
        }

        public UInt32 Width
        {
            get => interop.width;
            set => interop.width = value;
        }

        public UInt32 Height
        {
            get => interop.height;
            set => interop.height = value;
        }

        [Obsolete("Please use Path instead, (UnityUpgradable) -> Path", true)]
        public string path
        {
            get => interop.path;
            set => interop.path = value;
        }

        [Obsolete("Please use FileSize instead, (UnityUpgradable) -> FileSize", true)]
        public long fileSize
        {
            get => interop.fileSize;
            set => interop.fileSize = value;
        }

        [Obsolete("Please use Width instead, (UnityUpgradable) -> Width", true)]
        public UInt32 width
        {
            get => interop.width;
            set => interop.width = value;
        }

        [Obsolete("Please use Height instead, (UnityUpgradable) -> Height", true)]
        public UInt32 height
        {
            get => interop.height;
            set => interop.height = value;
        }
    };

    public class SYSTEMTIME
    {
        internal SYSTEMTIME(Interop.SYSTEMTIME interop)
        {
            this.interop = interop;
        }

        public SYSTEMTIME()
        {
            interop = new Interop.SYSTEMTIME();
        }

        internal Interop.SYSTEMTIME interop;

        public ushort WYear
        {
            get => interop.wYear;
            set => interop.wYear = value;
        }

        public ushort WMonth
        {
            get => interop.wMonth;
            set => interop.wMonth = value;
        }

        public ushort WDayOfWeek
        {
            get => interop.wDayOfWeek;
            set => interop.wDayOfWeek = value;
        }

        public ushort WDay
        {
            get => interop.wDay;
            set => interop.wDay = value;
        }

        public ushort WHour
        {
            get => interop.wHour;
            set => interop.wHour = value;
        }

        public ushort WMinute
        {
            get => interop.wMinute;
            set => interop.wMinute = value;
        }

        public ushort WSecond
        {
            get => interop.wSecond;
            set => interop.wSecond = value;
        }

        public ushort WMilliseconds
        {
            get => interop.wMilliseconds;
            set => interop.wMilliseconds = value;
        }
    }

    public class XAppCaptureVideoCaptureSettings
    {
        internal XAppCaptureVideoCaptureSettings(Interop.XAppCaptureVideoCaptureSettings interop)
        {
            this.interop = interop;
        }

        public XAppCaptureVideoCaptureSettings()
        {
            this.interop = new Interop.XAppCaptureVideoCaptureSettings();
        }

        internal Interop.XAppCaptureVideoCaptureSettings interop;

        public UInt32 Width
        {
            get => interop.width;
            set => interop.width = value;
        }

        public UInt32 Height
        {
            get => interop.height;
            set => interop.height = value;
        }

        public UInt64 MaxRecordTimespanDurationInMs
        {
            get => interop.maxRecordTimespanDurationInMs;
            set => interop.maxRecordTimespanDurationInMs = value;
        }

        public XAppCaptureVideoEncoding Encoding
        {
            get => interop.encoding;
            set => interop.encoding = value;
        }

        public XAppCaptureVideoColorFormat ColorFormat
        {
            get => interop.colorFormat;
            set => interop.colorFormat = value;
        }

        public bool IsCaptureByGamesAllowed
        {
            get => interop.isCaptureByGamesAllowed;
            set => interop.isCaptureByGamesAllowed = value;
        }
    }

    public delegate void XAppBroadcastMonitorCallback(IntPtr context);

    public delegate void XAppCaptureMetadataPurgedCallback(IntPtr context);

    partial class SDK
    {
        public static Int32 XAppBroadcastGetStatus(XUserHandle requestingUser, out XAppBroadcastStatus appBroadcastStatus)
        {
            appBroadcastStatus = default;

            IntPtr userHandle = (requestingUser != null) ? requestingUser.Handle : IntPtr.Zero;

            Interop.XAppBroadcastStatus appBroadcastStatusInterop = default;
            Int32 hr = NativeMethods.XAppBroadcastGetStatus(userHandle, out appBroadcastStatusInterop);
            appBroadcastStatus = new XAppBroadcastStatus(appBroadcastStatusInterop);

            return hr;
        }

        public static bool XAppBroadcastIsAppBroadcasting()
        {
            return NativeMethods.XAppBroadcastIsAppBroadcasting();
        }

        public static Int32 XAppBroadcastShowUI(XUserHandle requestingUser)
        {
            IntPtr userHandle = (requestingUser != null) ? requestingUser.Handle : IntPtr.Zero;

            return NativeMethods.XAppBroadcastShowUI(userHandle);
        }

        public static Int32 XAppBroadcastRegisterIsAppBroadcastingChanged(XTaskQueueHandle queue,
            IntPtr context,
            XAppBroadcastMonitorCallback appBroadcastMonitorCallback,
            out XIsAppBroadcastingChangedRegistrationToken token)
        {
            Interop.XAppBroadcastMonitorCallback interopCallback = (IntPtr context) =>
            {
                appBroadcastMonitorCallback(context);
            };

            token = new XIsAppBroadcastingChangedRegistrationToken(interopCallback, context);

            IntPtr interopQueue = queue != null ? queue.Handle : IntPtr.Zero;
            UInt64 tokenValue;
            int hr = NativeMethods.XAppBroadcastRegisterIsAppBroadcastingChanged(interopQueue,
                token.interop.CallbackContext,
                token.interop.StaticCallback,
                out tokenValue);
            if (HR.SUCCEEDED(hr))
            {
                token.interop.Token = tokenValue;
            }
            else
            {
                token.interop.Dispose();
                token = null;
            }

            return hr;
        }

        public static Int32 XAppBroadcastRegisterIsAppBroadcastingChanged(XTaskQueueHandle queue,
            XAppBroadcastMonitorCallback appBroadcastMonitorCallback,
            out XIsAppBroadcastingChangedRegistrationToken token)
        {
            return XAppBroadcastRegisterIsAppBroadcastingChanged(queue, IntPtr.Zero, appBroadcastMonitorCallback, out token);
        }

        public static int XAppCaptureCloseLocalStream(XAppCaptureLocalStreamHandle handle)
        {
            handle.Close();
            return handle.CloseResult;
        }

        [Obsolete("Please use XAppCaptureCloseScreenshotStream(XAppCaptureScreenshotStreamHandle) instead.", false)]
        public static Int32 XAppCaptureCloseScreenshotStream(IntPtr handle)
        {
            return NativeMethods.XAppCaptureCloseScreenshotStream(handle);
        }

        public static Int32 XAppCaptureCloseScreenshotStream(XAppCaptureScreenshotStreamHandle handle)
        {
            handle.Close();
            return handle.CloseResult;
        }

        public static Int32 XAppCaptureEnableRecord()
        {
            return NativeMethods.XAppCaptureEnableRecord();
        }

        public static Int32 XAppCaptureDisableRecord()
        {
            return NativeMethods.XAppCaptureDisableRecord();
        }

        public static bool XAppBroadcastUnregisterIsAppBroadcastingChanged(XIsAppBroadcastingChangedRegistrationToken token, bool wait)
        {
            return token.Unregister(wait);
        }

        public static Int32 XAppCaptureMetadataAddStringEvent(string name,
            string value,
            XAppCaptureMetadataPriority priority)
        {
            return NativeMethods.XAppCaptureMetadataAddStringEvent(name, value, priority);
        }

        public static Int32 XAppCaptureMetadataAddInt32Event(string name,
            Int32 value,
            XAppCaptureMetadataPriority priority)
        {
            return NativeMethods.XAppCaptureMetadataAddInt32Event(name, value, priority);
        }

        public static Int32 XAppCaptureMetadataAddDoubleEvent(string name,
            double value,
            XAppCaptureMetadataPriority priority)
        {
            return NativeMethods.XAppCaptureMetadataAddDoubleEvent(name, value, priority);
        }

        public static Int32 XAppCaptureMetadataStartStringState(string name,
            string value,
            XAppCaptureMetadataPriority priority)
        {
            return NativeMethods.XAppCaptureMetadataStartStringState(name, value, priority);
        }

        public static Int32 XAppCaptureMetadataStartInt32State(string name,
            Int32 value,
            XAppCaptureMetadataPriority priority)
        {
            return NativeMethods.XAppCaptureMetadataStartInt32State(name, value, priority);
        }

        public static Int32 XAppCaptureMetadataStartDoubleState(string name,
            double value,
            XAppCaptureMetadataPriority priority)
        {
            return NativeMethods.XAppCaptureMetadataStartDoubleState(name, value, priority);
        }

        public static Int32 XAppCaptureMetadataStopState(string name)
        {
            return NativeMethods.XAppCaptureMetadataStopState(name);
        }

        public static Int32 XAppCaptureMetadataStopAllStates()
        {
            return NativeMethods.XAppCaptureMetadataStopAllStates();
        }

        public static Int32 XAppCaptureMetadataRemainingStorageBytesAvailable(out UInt64 value)
        {
            return NativeMethods.XAppCaptureMetadataRemainingStorageBytesAvailable(out value);
        }

        [Obsolete("Use XAppCaptureOpenScreenshotStream(string localId, XAppCaptureScreenshotFormatFlag, out XAppCaptureScreenshotStreamHandle, out UInt64)", false)]
        public static Int32 XAppCaptureOpenScreenshotStream(XAppScreenshotLocalId id,
            XAppCaptureScreenshotFormatFlag screenshotFormat,
            out IntPtr handle,
            out ulong totalBytes)
        {
            string localId = System.Text.Encoding.UTF8.GetString(id.Value);
            return NativeMethods.XAppCaptureOpenScreenshotStream(localId, screenshotFormat, out handle, out totalBytes);
        }

        public static Int32 XAppCaptureOpenScreenshotStream(string localId,
            XAppCaptureScreenshotFormatFlag screenshotFormat,
            out XAppCaptureScreenshotStreamHandle handle,
            out UInt64 totalBytes)
        {
            IntPtr interopHandle;
            Int32 hr = NativeMethods.XAppCaptureOpenScreenshotStream(localId, screenshotFormat, out interopHandle, out totalBytes);
            if (HR.SUCCEEDED(hr) && interopHandle == IntPtr.Zero)
            {
                handle = null;
                return hr;
            }
            return XAppCaptureScreenshotStreamHandle.WrapAndReturnHResult(hr, interopHandle, out handle);
        }

        [Obsolete("Use XAppCaptureReadScreenshotStream(XAppCaptureScreenshotStreamHandle, UInt64, UInt32, byte[], out UInt32)", false)]
        public static Int32 XAppCaptureReadScreenshotStream(IntPtr handle,
            UInt64 startPosition,
            UInt32 totalBytes,
            byte[] data,
            out Int32 bytesWritten)
        {
            // Created for back compatability and will be removed in future
            UInt32 uint32BytesWritten;
            Int32 returnVal = NativeMethods.XAppCaptureReadScreenshotStream(handle, startPosition, totalBytes, data, out uint32BytesWritten);
            bytesWritten = (Int32)uint32BytesWritten;
            return returnVal;
        }

        public static Int32 XAppCaptureReadScreenshotStream(XAppCaptureScreenshotStreamHandle handle,
            UInt64 startPosition,
            UInt32 bytesToRead,
            byte[] buffer,
            out UInt32 bytesWritten)
        {
            return NativeMethods.XAppCaptureReadScreenshotStream(handle.Handle, startPosition, bytesToRead, buffer, out bytesWritten);
        }

        public static Int32 XAppCaptureRecordDiagnosticClip(long startTime,
            UInt32 durationInMs,
            string filenamePrefix,
            out XAppCaptureRecordClipResult result)
        {
            result = default(XAppCaptureRecordClipResult);
            Interop.XAppCaptureRecordClipResult resultInterop = default;

            Int32 hr = NativeMethods.XAppCaptureRecordDiagnosticClip(startTime, durationInMs, filenamePrefix, out resultInterop);

            if(HR.SUCCEEDED(hr))
            {
                result = new XAppCaptureRecordClipResult(resultInterop);
            }

            return hr;
        }

        public static Int32 XAppCaptureTakeDiagnosticScreenshot(bool gamescreenOnly,
            XAppCaptureScreenshotFormatFlag captureFlags,
            string filenamePrefix,
            out XAppCaptureDiagnosticScreenshotResult result)
        {
            result = default(XAppCaptureDiagnosticScreenshotResult);
            Interop.XAppCaptureDiagnosticScreenshotResult resultInterop = default;

            Int32 hr = NativeMethods.XAppCaptureTakeDiagnosticScreenshot(gamescreenOnly, captureFlags, filenamePrefix, out resultInterop);

            if (HR.SUCCEEDED(hr))
            {
                result = new XAppCaptureDiagnosticScreenshotResult(resultInterop);
            }

            return hr;
        }

        public static Int32 XAppCaptureTakeScreenshot(XUserHandle requestingUser,
            out XAppCaptureTakeScreenshotResult result)
        {
            result = default(XAppCaptureTakeScreenshotResult);
            Interop.XAppCaptureTakeScreenshotResult resultInterop = default;

            IntPtr userHandle = (requestingUser != null) ? requestingUser.Handle : IntPtr.Zero;

            Int32 hr = NativeMethods.XAppCaptureTakeScreenshot(userHandle, out resultInterop);
            if(HR.SUCCEEDED(hr))
            {
                result = new XAppCaptureTakeScreenshotResult(resultInterop);
            }

            return hr;
        }

        public static Int32 XAppCaptureRegisterMetadataPurged(XTaskQueueHandle queue, IntPtr context, XAppCaptureMetadataPurgedCallback callback,
            out XMetadataPurgedToken token)
        {
            Interop.XAppCaptureMetadataPurgedCallback interopCallback = (IntPtr context) =>
            {
                callback(context);
            };

            token = new XMetadataPurgedToken(interopCallback, context);

            UInt64 tokenValue;
            int hr = NativeMethods.XAppCaptureRegisterMetadataPurged( (queue != null) ? queue.Handle : IntPtr.Zero,
                token.interop.CallbackContext,
                token.interop.StaticCallback,
                out tokenValue);
            if (HR.SUCCEEDED(hr))
            {
                token.Token = tokenValue;
            }
            else
            {
                token.interop.Dispose();
                token = null;
            }

            return hr;
        }

        public static Int32 XAppCaptureRegisterMetadataPurged(XTaskQueueHandle queue,
            XAppCaptureMetadataPurgedCallback callback,
            out XMetadataPurgedToken token)
        {
            return XAppCaptureRegisterMetadataPurged(queue, IntPtr.Zero, callback, out token);
        }

        public static bool XAppCaptureUnRegisterMetadataPurged(XMetadataPurgedToken token, bool wait)
        {
            return token.Unregister(wait);
        }

        public static Int32 XAppCaptureReadLocalStream(XAppCaptureLocalStreamHandle handle, long startPosition, UInt32 bytesToRead,
            ref byte[] buffer, out UInt32 bytesWritten)
        {
            return NativeMethods.XAppCaptureReadLocalStream(handle.Handle, startPosition, bytesToRead, buffer, out bytesWritten);
        }

        public static Int32 XAppCaptureRecordTimespan(SYSTEMTIME startTimestamp, UInt64 durationInMilliseconds, out XAppCaptureLocalResult result)
        {
            result = default(XAppCaptureLocalResult);

            Int32 hr = HR.S_OK;

            Interop.XAppCaptureLocalResult interop;

            GCHandle startTimestampHandle = GCHandle.Alloc(startTimestamp.interop, GCHandleType.Pinned);

            try
            {
                hr = NativeMethods.XAppCaptureRecordTimespan(startTimestampHandle.AddrOfPinnedObject(), durationInMilliseconds, out interop);

                if (HR.SUCCEEDED(hr))
                {
                    result = new XAppCaptureLocalResult(interop);
                }
            }
            catch(Exception e)
            {
                hr = e.HResult;
            }
            finally
            {
                startTimestampHandle.Free();
            }

            return hr;
        }

        public static Int32 XAppCaptureRecordTimespan(UInt64 durationInMilliseconds, out XAppCaptureLocalResult result)
        {
            result = default(XAppCaptureLocalResult);

            Interop.XAppCaptureLocalResult interop;

            Int32 hr = NativeMethods.XAppCaptureRecordTimespan(IntPtr.Zero, durationInMilliseconds, out interop);
            if (HR.SUCCEEDED(hr))
            {
                result = new XAppCaptureLocalResult(interop);
            }

            return hr;
        }

        public static Int32 XAppCaptureGetVideoCaptureSettings(out XAppCaptureVideoCaptureSettings userCaptureSettings)
        {
            Interop.XAppCaptureVideoCaptureSettings interopUserCaptureSettings = default;

            userCaptureSettings = default;

            Int32 hr = NativeMethods.XAppCaptureGetVideoCaptureSettings(out interopUserCaptureSettings);

            if(HR.SUCCEEDED(hr))
            {
                userCaptureSettings = new XAppCaptureVideoCaptureSettings(interopUserCaptureSettings);
            }

            return hr;
        }
    }
}
