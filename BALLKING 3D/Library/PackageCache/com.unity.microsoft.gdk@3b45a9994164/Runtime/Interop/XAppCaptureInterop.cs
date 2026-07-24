using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    // struct XAppBroadcastStatus
    // {
    //     bool canStartBroadcast;
    //     bool isAnyAppBroadcasting;
    //     bool isCaptureResourceUnavailable;
    //     bool isGameStreamInProgress;
    //     bool isGpuConstrained;
    //     bool isAppInactive;
    //     bool isBlockedForApp;
    //     bool isDisabledByUser;
    //     bool isDisabledBySystem;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XAppBroadcastStatus
    {
        [MarshalAs(UnmanagedType.I1)] internal bool canStartBroadcast;
        [MarshalAs(UnmanagedType.I1)] internal bool isAnyAppBroadcasting;
        [MarshalAs(UnmanagedType.I1)] internal bool isCaptureResourceUnavailable;
        [MarshalAs(UnmanagedType.I1)] internal bool isGameStreamInProgress;
        [MarshalAs(UnmanagedType.I1)] internal bool isGpuConstrained;
        [MarshalAs(UnmanagedType.I1)] internal bool isAppInactive;
        [MarshalAs(UnmanagedType.I1)] internal bool isBlockedForApp;
        [MarshalAs(UnmanagedType.I1)] internal bool isDisabledByUser;
        [MarshalAs(UnmanagedType.I1)] internal bool isDisabledBySystem;
    };

    // struct XAppCaptureTakeScreenshotResult
    // {
    //     _Field_z_ char localId[APPCAPTURE_MAX_LOCALID_LENGTH];
    //     XAppCaptureScreenshotFormatFlag availableScreenshotFormats;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XAppCaptureTakeScreenshotResult
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NativeMethods.APPCAPTURE_MAX_LOCALID_LENGTH)] internal string localId;
        internal XAppCaptureScreenshotFormatFlag availableScreenshotFormats;
    };

    // struct XAppCaptureRecordClipResult
    // {
    //     _Field_z_ char path[MAX_PATH];
    //     size_t fileSize;
    //     time_t startTime;
    //     uint32_t durationInMs;
    //     uint32_t width;
    //     uint32_t height;
    //     XAppCaptureVideoEncoding encoding;
    //     uint32_t startTimePreciseOffsetHns;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XAppCaptureRecordClipResult
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NativeMethods.MAX_PATH)] internal string path;
        internal long fileSize;
        internal long startTime;
        internal UInt32 durationInMs;
        internal UInt32 width;
        internal UInt32 height;
        internal XAppCaptureVideoEncoding encoding;
        internal UInt32 startTimePreciseOffsetHns;
    };

    // struct XAppCaptureScreenshotFile
    // {
    //     _Field_z_ char path[MAX_PATH];
    //     size_t fileSize;
    //     uint32_t width;
    //     uint32_t height;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XAppCaptureScreenshotFile
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NativeMethods.MAX_PATH)] internal string path;
        internal long fileSize;
        internal UInt32 width;
        internal UInt32 height;
    };

    // struct XAppCaptureDiagnosticScreenshotResult
    // {
    //     size_t fileCount;
    //     XAppCaptureScreenshotFile files[APPCAPTURE_MAX_CAPTURE_FILES];
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XAppCaptureDiagnosticScreenshotResult
    {
        internal long fileCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = NativeMethods.APPCAPTURE_MAX_CAPTURE_FILES)] internal XAppCaptureScreenshotFile[] files;
    };

    // typedef struct _SYSTEMTIME {
    // WORD wYear;
    // WORD wMonth;
    // WORD wDayOfWeek;
    // WORD wDay;
    // WORD wHour;
    // WORD wMinute;
    // WORD wSecond;
    // WORD wMilliseconds;
    // } SYSTEMTIME, *PSYSTEMTIME, *LPSYSTEMTIME;
    [StructLayout(LayoutKind.Sequential)]
    internal struct SYSTEMTIME
    {
        internal ushort wYear;
        internal ushort wMonth;
        internal ushort wDayOfWeek;
        internal ushort wDay;
        internal ushort wHour;
        internal ushort wMinute;
        internal ushort wSecond;
        internal ushort wMilliseconds;
    }

    // struct XAppCaptureLocalResult
    // {
    //     XAppCaptureLocalStreamHandle clipHandle;
    //     size_t fileSizeInBytes;
    //     SYSTEMTIME clipStartTimestamp;
    //     uint64_t durationInMilliseconds;
    //     uint32_t width;
    //     uint32_t height;
    //     XAppCaptureVideoEncoding encoding;
    //     XAppCaptureVideoColorFormat colorFormat;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XAppCaptureLocalResult
    {
        internal IntPtr clipHandle;
        internal ulong fileSizeInBytes;
        internal SYSTEMTIME clipStartTimestamp;
        internal UInt64 durationInMilliseconds;
        internal UInt32 width;
        internal UInt32 height;
        internal XAppCaptureVideoEncoding encoding;
        internal XAppCaptureVideoColorFormat colorFormat;
    }

    // struct XAppCaptureVideoCaptureSettings
    // {
    //     uint32_t width;
    //     uint32_t height;
    //     uint64_t maxRecordTimespanDurationInMs;
    //     XAppCaptureVideoEncoding encoding;
    //     XAppCaptureVideoColorFormat colorFormat;
    //     bool isCaptureByGamesAllowed;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XAppCaptureVideoCaptureSettings
    {
        internal UInt32 width;
        internal UInt32 height;
        internal UInt64 maxRecordTimespanDurationInMs;
        internal XAppCaptureVideoEncoding encoding;
        internal XAppCaptureVideoColorFormat colorFormat;
        [MarshalAs(UnmanagedType.I1)] internal bool isCaptureByGamesAllowed;
    }

    internal class XIsAppBroadcastingChangedRegistrationToken : XRegistrationToken<Interop.XAppBroadcastMonitorCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XAppBroadcastMonitorCallback))]
        static void OnIsAppBroadcastingChanged(IntPtr context)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XAppBroadcastMonitorCallback>;
            wrapper.Callback(wrapper.Context);
        }

        public XIsAppBroadcastingChangedRegistrationToken(Interop.XAppBroadcastMonitorCallback callback, IntPtr context) :
            base(callback, context, OnIsAppBroadcastingChanged)
        {
        }

        public bool Unregister(bool wait)
        {
            bool result = true;
            if (this.Token != 0)
            {
                result = NativeMethods.XAppBroadcastUnregisterIsAppBroadcastingChanged(this.Token, wait);
                this.Token = 0;
            }
            return result;
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister(wait: true);
        }
    }

    internal class XMetadataPurgedToken : XRegistrationToken<Interop.XAppCaptureMetadataPurgedCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XAppCaptureMetadataPurgedCallback))]
        static void OnXMetadataPurged(IntPtr context)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XAppCaptureMetadataPurgedCallback>;
            wrapper.Callback(wrapper.Context);
        }

        public XMetadataPurgedToken(Interop.XAppCaptureMetadataPurgedCallback callback, IntPtr context) :
            base(callback, context, OnXMetadataPurged)
        {
        }

        public bool Unregister(bool wait)
        {
            bool result = true;
            if (this.Token != 0)
            {
                result = NativeMethods.XAppCaptureUnRegisterMetadataPurged(this.Token, wait);
                this.Token = 0;
            }

            return result;
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister(wait: true);
        }
    }

    // typedef void CALLBACK XAppBroadcastMonitorCallback(
    //          _In_ void* context
    // );
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void XAppBroadcastMonitorCallback(IntPtr context);

    // typedef void CALLBACK XAppCaptureMetadataPurgedCallback(_In_ void* context);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void XAppCaptureMetadataPurgedCallback(IntPtr context);

    partial class NativeMethods
    {
        internal const int MAX_PATH = 260;
        internal const int APPCAPTURE_MAX_CAPTURE_FILES = 10;
        internal const int APPCAPTURE_MAX_LOCALID_LENGTH = 250;

        // STDAPI XAppBroadcastGetStatus(
        //    _In_ XUserHandle requestingUser,
        //    _Out_ XAppBroadcastStatus* appBroadcastStatus) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppBroadcastGetStatus(IntPtr requestingUser,
            out XAppBroadcastStatus appBroadcastStatus);

        // STDAPI_(bool) XAppBroadcastIsAppBroadcasting() noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XAppBroadcastIsAppBroadcasting();

        // STDAPI XAppBroadcastShowUI(_In_ XUserHandle requestingUser) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppBroadcastShowUI(IntPtr requestingUser);

        // STDAPI XAppBroadcastRegisterIsAppBroadcastingChanged(
        //   _In_opt_ XTaskQueueHandle queue,
        //   _In_opt_ void* context,
        //   _In_ XAppBroadcastMonitorCallback* appBroadcastMonitorCallback,
        //   _Out_ XTaskQueueRegistrationToken* token) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppBroadcastRegisterIsAppBroadcastingChanged(IntPtr queue,
            IntPtr context,
            XAppBroadcastMonitorCallback appBroadcastMonitorCallback,
            out UInt64 token);

        // STDAPI XAppCaptureCloseScreenshotStream(
        //   _In_ XAppCaptureScreenshotStreamHandle handle
        //   ) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureCloseScreenshotStream(IntPtr handle);

        // STDAPI XAppCaptureEnableRecord() noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureEnableRecord();

        // STDAPI XAppCaptureDisableRecord() noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureDisableRecord();

        // STDAPI_(bool) XAppBroadcastUnregisterIsAppBroadcastingChanged(
        //   _In_ XTaskQueueRegistrationToken token,
        //   _In_ bool wait) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XAppBroadcastUnregisterIsAppBroadcastingChanged(UInt64 token, [MarshalAs(UnmanagedType.I1)] bool wait);

        // STDAPI XAppCaptureMetadataAddStringEvent(
        //   _In_z_ const char* name,
        //   _In_z_ const char* value,
        //   _In_ XAppCaptureMetadataPriority priority) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureMetadataAddStringEvent([MarshalAs(UnmanagedType.LPStr)] string name,
            [MarshalAs(UnmanagedType.LPStr)] string value,
            XAppCaptureMetadataPriority priority);

        // STDAPI XAppCaptureMetadataAddInt32Event(
        //   _In_z_ const char* name,
        //   _In_ int32_t value,
        //   _In_ XAppCaptureMetadataPriority priority) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureMetadataAddInt32Event([MarshalAs(UnmanagedType.LPStr)] string name,
            Int32 value,
            XAppCaptureMetadataPriority priority);

        // STDAPI XAppCaptureMetadataAddDoubleEvent(
        //   _In_z_ const char* name,
        //   _In_ double value,
        //   _In_ XAppCaptureMetadataPriority priority) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureMetadataAddDoubleEvent([MarshalAs(UnmanagedType.LPStr)] string name,
            double value,
            XAppCaptureMetadataPriority priority);

        // STDAPI XAppCaptureMetadataStartStringState(
        //   _In_z_ const char* name,
        //   _In_z_ const char* value,
        //   _In_ XAppCaptureMetadataPriority priority) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureMetadataStartStringState([MarshalAs(UnmanagedType.LPStr)] string name,
            [MarshalAs(UnmanagedType.LPStr)] string value,
            XAppCaptureMetadataPriority priority);

        // STDAPI XAppCaptureMetadataStartInt32State(
        //   _In_z_ const char* name,
        //   _In_ int32_t value,
        //   _In_ XAppCaptureMetadataPriority priority) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureMetadataStartInt32State([MarshalAs(UnmanagedType.LPStr)] string name,
            Int32 value,
            XAppCaptureMetadataPriority priority);

        // STDAPI XAppCaptureMetadataStartDoubleState(
        //   _In_z_ const char* name,
        //   _In_ double value,
        //   _In_ XAppCaptureMetadataPriority priority) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureMetadataStartDoubleState([MarshalAs(UnmanagedType.LPStr)] string name,
            double value,
            XAppCaptureMetadataPriority priority);

        // STDAPI XAppCaptureMetadataStopState(_In_z_ const char* name) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureMetadataStopState([MarshalAs(UnmanagedType.LPStr)] string name);

        // STDAPI XAppCaptureMetadataStopAllStates() noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureMetadataStopAllStates();

        //STDAPI XAppCaptureMetadataRemainingStorageBytesAvailable(_Out_ uint64_t* value) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureMetadataRemainingStorageBytesAvailable(out UInt64 value);

        // STDAPI XAppCaptureOpenScreenshotStream(
        //   _In_ const char* localId,
        //   _In_ XAppCaptureScreenshotFormatFlag screenshotFormat,
        //   _Out_ XAppCaptureScreenshotStreamHandle* handle,
        //   _Out_opt_ uint64_t* totalBytes
        //   ) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureOpenScreenshotStream([MarshalAs(UnmanagedType.LPStr)] string localId,
            XAppCaptureScreenshotFormatFlag screenshotFormat,
            out IntPtr handle,
            out UInt64 totalBytes);

        // STDAPI XAppCaptureReadScreenshotStream(
        //   _In_ XAppCaptureScreenshotStreamHandle handle,
        //   _In_ uint64_t startPosition,
        //   _In_ uint32_t bytesToRead,
        //   _Out_writes_to_(bytesToRead, *bytesWritten) uint8_t* buffer,
        //   _Out_ uint32_t* bytesWritten
        // ) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureReadScreenshotStream(IntPtr handle,
            UInt64 startPosition,
            UInt32 bytesToRead,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer,
            out UInt32 bytesWritten);

        // STDAPI XAppCaptureRecordDiagnosticClip(
        //   _In_ time_t startTime,
        //   _In_ uint32_t durationInMs,
        //   _In_opt_ const char* filenamePrefix,
        //   _Out_ XAppCaptureRecordClipResult* result
        // ) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureRecordDiagnosticClip(long startTime,
            UInt32 durationInMs,
            [MarshalAs(UnmanagedType.LPStr)] string filenamePrefix,
            out XAppCaptureRecordClipResult result);

        // STDAPI XAppCaptureTakeDiagnosticScreenshot(
        //   _In_ bool gamescreenOnly,
        //   _In_ XAppCaptureScreenshotFormatFlag captureFlags,
        //   _In_opt_ const char* filenamePrefix,
        //   _Out_ XAppCaptureDiagnosticScreenshotResult* result
        //   ) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureTakeDiagnosticScreenshot([MarshalAs(UnmanagedType.I1)] bool gamescreenOnly,
            XAppCaptureScreenshotFormatFlag captureFlags,
            [MarshalAs(UnmanagedType.LPStr)] string filenamePrefix,
            out Interop.XAppCaptureDiagnosticScreenshotResult result);

        // STDAPI XAppCaptureTakeScreenshot(
        //   _In_ XUserHandle requestingUser,
        //   _Out_ XAppCaptureTakeScreenshotResult* result
        //   ) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureTakeScreenshot(IntPtr requestingUser,
            out XAppCaptureTakeScreenshotResult result);

        // STDAPI XAppCaptureRegisterMetadataPurged(
        // _In_opt_ XTaskQueueHandle queue,
        // _In_ void* context,
        // _In_ XAppCaptureMetadataPurgedCallback* callback,
        // _Out_ XTaskQueueRegistrationToken* token) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureRegisterMetadataPurged(IntPtr queue,
            IntPtr context, XAppCaptureMetadataPurgedCallback callback,
            out UInt64 token);

        // STDAPI_(bool) XAppCaptureUnRegisterMetadataPurged(_In_ XTaskQueueRegistrationToken token,
        //      _In_ bool wait) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XAppCaptureUnRegisterMetadataPurged(UInt64 token,
            [MarshalAs(UnmanagedType.I1)] bool wait);

        // STDAPI XAppCaptureReadLocalStream(
        //   _In_ XAppCaptureLocalStreamHandle handle,
        //   _In_ size_t startPosition,
        //   _In_ uint32_t bytesToRead,
        //   _Out_writes_to_(bytesToRead, *bytesWritten) uint8_t* buffer,
        //   _Out_ uint32_t* bytesWritten) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureReadLocalStream(IntPtr handle, long startPosition, UInt32 bytesToRead,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer, out UInt32 bytesWritten);

        // STDAPI XAppCaptureCloseLocalStream(_In_ XAppCaptureLocalStreamHandle handle ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureCloseLocalStream(IntPtr handle);

        // STDAPI XAppCaptureRecordTimespan(
        //   _In_opt_ const SYSTEMTIME* startTimestamp,
        //   _In_ uint64_t durationInMilliseconds,
        //   _Out_ XAppCaptureLocalResult* result) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureRecordTimespan(IntPtr startTimestamp, UInt64 durationInMilliseconds, out Interop.XAppCaptureLocalResult result);

        // STDAPI XAppCaptureGetVideoCaptureSettings(
        //   _Out_ XAppCaptureVideoCaptureSettings* userCaptureSettings) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAppCaptureGetVideoCaptureSettings(out XAppCaptureVideoCaptureSettings userCaptureSettings);
    }
}
