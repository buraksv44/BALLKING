// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Unity.XGamingRuntime.Interop
{
    //struct XPackageChunkSelector
    //{
    //    XPackageChunkSelectorType type;
    //    union
    //    {
    //        _Field_z_ const char* language;
    //        _Field_z_ const char* tag;
    //        _Field_z_ const char* feature;
    //        uint32_t chunkId;
    //    };
    //};
    [StructLayout(LayoutKind.Explicit)]
    internal struct XPackageChunkSelectorInterop
    {
        [FieldOffset(0)]
        internal XPackageChunkSelectorType type;

        // While we would prefer to use string here, c# does not allow overlapping
        // a string with a UInt32, so we must marshal strings manually
        [FieldOffset(8)]
        internal IntPtr languageOrTagOrFeature;

        [FieldOffset(8)]
        internal UInt32 chunkId;
    }

    // struct XPackageInstallationProgress
    // {
    //     uint64_t totalBytes;
    //     uint64_t installedBytes;
    //     uint64_t launchBytes;
    //     bool launchable;
    //     bool completed;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XPackageInstallationProgress
    {
        internal UInt64 totalBytes;
        internal UInt64 installedBytes;
        internal UInt64 launchBytes;
        [MarshalAs(UnmanagedType.I1)] internal bool launchable;
        [MarshalAs(UnmanagedType.I1)] internal bool completed;
    };

    // struct XPackageDetails
    // {
    //     _Field_z_ const char* packageIdentifier;
    //     XVersion version;
    //     XPackageKind kind;
    //     _Field_z_ const char* displayName;
    //     _Field_z_ const char* description;
    //     _Field_z_ const char* publisher;
    //     _Field_z_ const char* storeId;
    //     bool installing;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XPackageDetails
    {
        [MarshalAs(UnmanagedType.LPStr)] internal string packageIdentifier;
        internal XVersion version;
        internal XPackageKind kind;
        [MarshalAs(UnmanagedType.LPStr)] internal string displayName;
        [MarshalAs(UnmanagedType.LPStr)] internal string description;
        [MarshalAs(UnmanagedType.LPStr)] internal string publisher;
        [MarshalAs(UnmanagedType.LPStr)] internal string storeId;
        [MarshalAs(UnmanagedType.I1)] internal bool installing;
    }

    // struct XPackageFeature
    // {
    //     _Field_z_ const char* id;
    //     _Field_z_ const char* displayName;
    //     _Field_z_ const char* tags;
    //     bool hidden;
    //     uint32_t storeIdCount;
    //     _Field_z_ const char** storeIds;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XPackageFeature
    {
        [MarshalAs(UnmanagedType.LPStr)] internal string id;
        [MarshalAs(UnmanagedType.LPStr)] internal string displayName;
        [MarshalAs(UnmanagedType.LPStr)] internal string tags;
        [MarshalAs(UnmanagedType.I1)] internal bool hidden;
        internal UInt32 storeIdCount;
        internal IntPtr storeIds;
    };

    // struct XPackageWriteStats
    // {
    //     uint64_t interval;
    //     uint64_t budget;
    //     uint64_t elapsed;
    //     uint64_t bytesWritten;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XPackageWriteStats
    {
        internal UInt64 interval;
        internal UInt64 budget;
        internal UInt64 elapsed;
        internal UInt64 bytesWritten;
    };

    internal class XPackageRegisterPackageInstalledToken : XRegistrationToken<Interop.XPackageInstalledCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XPackageInstalledCallback))]
        static void OnPackageInstalled(IntPtr context, Interop.XPackageDetails details)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XPackageInstalledCallback>;
            wrapper.Callback(wrapper.Context, details);
        }

        public XPackageRegisterPackageInstalledToken(Interop.XPackageInstalledCallback callback, IntPtr context) :
            base(callback, context, OnPackageInstalled)
        { }

        public bool Unregister(bool wait)
        {
            bool result = true;
            if (this.Token != 0)
            {
                result = NativeMethods.XPackageUnregisterPackageInstalled(this.Token, wait);
                this.Token = 0;
            }

            return result;
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister(true);
        }
    }

    internal class XPackageRegisterInstallationProgressChangedToken : XRegistrationToken<Interop.XPackageInstallationProgressCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XPackageInstallationProgressCallback))]
        static void OnInstallationProgress(IntPtr context, IntPtr monitor)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XPackageInstallationProgressCallback>;
            wrapper.Callback(wrapper.Context, monitor);
        }

        XPackageInstallationMonitorHandle installationProgressChanged;

        public XPackageRegisterInstallationProgressChangedToken(XPackageInstallationMonitorHandle installationProgressChanged,
            Interop.XPackageInstallationProgressCallback callback,
            IntPtr context) : base(callback, context, OnInstallationProgress)
        {
            this.installationProgressChanged = installationProgressChanged;
        }

        public bool Unregister(XPackageInstallationMonitorHandle installationProgressChanged, bool wait)
        {
            bool result = true;
            if (this.Token != 0)
            {
                result = NativeMethods.XPackageUnregisterInstallationProgressChanged(installationProgressChanged.Handle, this.Token, wait);
                this.Token = 0;
            }

            return result;
        }

        public bool Unregister(bool wait)
        {
            return Unregister(this.installationProgressChanged, true);
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister(wait: true);
        }
    }

    // typedef bool CALLBACK XPackageEnumerationCallback(_In_ void* context, _In_ const XPackageDetails* details);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    internal delegate bool XPackageEnumerationCallback(IntPtr context, XPackageDetails details);

    // typedef bool CALLBACK XPackageChunkAvailabilityCallback(_In_ void* context, _In_ const XPackageChunkSelector* selector, _In_ XPackageChunkAvailability availability);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    internal delegate bool XPackageChunkAvailabilityCallback(IntPtr context, XPackageChunkSelectorInterop selector, XPackageChunkAvailability availability);

    // typedef bool CALLBACK XPackageFeatureEnumerationCallback(_In_ void* context, _In_ const XPackageFeature* feature);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    internal delegate bool XPackageFeatureEnumerationCallbackInterop(IntPtr context, Interop.XPackageFeature feature);

    // typedef void CALLBACK XPackageInstalledCallback(_In_ void* context, _In_ const XPackageDetails* details);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void XPackageInstalledCallback(IntPtr context, XPackageDetails details);

    // typedef void CALLBACK XPackageInstallationProgressCallback(_In_ void* context, _In_ XPackageInstallationMonitorHandle monitor);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void XPackageInstallationProgressCallback(IntPtr context, IntPtr monitor);

    partial class NativeMethods
    {
        //STDAPI XPackageCreateInstallationMonitor(
        //    _In_z_ const char* packageIdentifier,
        //    _In_ uint32_t selectorCount,
        //    _In_reads_opt_(selectorCount) XPackageChunkSelector* selectors,
        //    _In_ uint32_t minimumUpdateIntervalMs, // 0 == no update
        //    _In_opt_ XTaskQueueHandle queue,
        //    _Out_ XPackageInstallationMonitorHandle* installationMonitor) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageCreateInstallationMonitor(
            [MarshalAs(UnmanagedType.LPStr)] string packageIdentifier,
            UInt32 selectorCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] XPackageChunkSelectorInterop[] selectors,
            UInt32 minimumUpdateIntervalMs,
            IntPtr queue,
            out IntPtr installationMonitor);

        //STDAPI_(void) XPackageCloseInstallationMonitorHandle(
        //    _In_ XPackageInstallationMonitorHandle installationMonitor
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XPackageCloseInstallationMonitorHandle(IntPtr installationMonitor);

        // STDAPI XPackageGetCurrentProcessPackageIdentifier(_In_ size_t bufferSize, _Out_writes_(bufferSize) char* buffer) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageGetCurrentProcessPackageIdentifier(UInt64 bufferSize, StringBuilder buffer);

        // STDAPI_(bool) XPackageIsPackagedProcess() noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XPackageIsPackagedProcess();

        // STDAPI_(void) XPackageGetInstallationProgress(_In_ XPackageInstallationMonitorHandle installationMonitor, _Out_ XPackageInstallationProgress* progress) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XPackageGetInstallationProgress(IntPtr installationMonitor, out XPackageInstallationProgress progress);

        // STDAPI_(bool) XPackageUpdateInstallationMonitor(_In_ XPackageInstallationMonitorHandle installationMonitor) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XPackageUpdateInstallationMonitor(IntPtr installationMonitor);

        // STDAPI XPackageGetUserLocale(_In_ size_t localeSize, _Out_writes_(localeSize) char* locale) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageGetUserLocale(UInt64 localeSize, StringBuilder locale);

        // STDAPI XPackageFindChunkAvailability(_In_z_ const char* packageIdentifier,
        //     _In_ uint32_t selectorCount,
        //     _In_reads_opt_(selectorCount) XPackageChunkSelector* selectors,
        //     _Out_ XPackageChunkAvailability* availability) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageFindChunkAvailability([MarshalAs(UnmanagedType.LPStr)] string packageIdentifier,
            UInt32 selectorCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] XPackageChunkSelectorInterop[] selectors,
            out XPackageChunkAvailability availability);

        // STDAPI XPackageChangeChunkInstallOrder(_In_z_ const char* packageIdentifier,
        //     _In_ uint32_t selectorCount,
        //     _In_reads_(selectorCount) XPackageChunkSelector* selectors) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageChangeChunkInstallOrder([MarshalAs(UnmanagedType.LPStr)] string packageIdentifier,
            UInt32 selectorCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] XPackageChunkSelectorInterop[] selectors);

        // STDAPI XPackageInstallChunks(_In_z_ const char* packageIdentifier,
        //     _In_ uint32_t selectorCount,
        //     _In_reads_(selectorCount) XPackageChunkSelector* selectors,
        //     _In_ uint32_t minimumUpdateIntervalMs,
        //     _In_ bool suppressUserConfirmation,
        //     _In_opt_ XTaskQueueHandle queue,
        //     _Out_ XPackageInstallationMonitorHandle* installationMonitor) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageInstallChunks([MarshalAs(UnmanagedType.LPStr)] string packageIdentifier,
            UInt32 selectorCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] XPackageChunkSelectorInterop[] selectors,
            UInt32 minimumUpdateIntervalMs,
            [MarshalAs(UnmanagedType.I1)] bool suppressUserConfirmation,
            IntPtr queue,
            out IntPtr installationMonitor);

        // STDAPI XPackageInstallChunksAsync(_In_z_ const char* packageIdentifier,
        //     _In_ uint32_t selectorCount,
        //     _In_reads_(selectorCount) XPackageChunkSelector* selectors,
        //     _In_ uint32_t minimumUpdateIntervalMs,
        //     _In_ bool suppressUserConfirmation,
        //     _Inout_ XAsyncBlock* asyncBlock) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageInstallChunksAsync([MarshalAs(UnmanagedType.LPStr)] string packageIdentifier,
            UInt32 selectorCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] XPackageChunkSelectorInterop[] selectors,
            UInt32 minimumUpdateIntervalMs,
            [MarshalAs(UnmanagedType.I1)] bool suppressUserConfirmation,
            IntPtr async);


        // STDAPI XPackageInstallChunksResult(_Inout_ XAsyncBlock* asyncBlock,
        //     _Out_ XPackageInstallationMonitorHandle* installationMonitor) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageInstallChunksResult(IntPtr asyncBlock, out IntPtr installationMonitor);

        // STDAPI XPackageEstimateDownloadSize(_In_z_ const char* packageIdentifier,
        //     _In_ uint32_t selectorCount,
        //     _In_reads_(selectorCount) XPackageChunkSelector* selectors,
        //     _Out_ uint64_t* downloadSize,
        //     _Out_opt_ bool* shouldPresentUserConfirmation) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageEstimateDownloadSize([MarshalAs(UnmanagedType.LPStr)] string packageIdentifier,
            UInt32 selectorCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] XPackageChunkSelectorInterop[] selectors,
            out UInt64 downloadSize,
            [MarshalAs(UnmanagedType.I1)] out bool shouldPresentUserConfirmation);


        // STDAPI XPackageUninstallChunks(_In_z_ const char* packageIdentifier,
        //     _In_ uint32_t selectorCount,
        //     _In_reads_(selectorCount) XPackageChunkSelector* selectors) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageUninstallChunks([MarshalAs(UnmanagedType.LPStr)] string packageIdentifier,
            UInt32 selectorCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] XPackageChunkSelectorInterop[] selectors);

        //  STDAPI_(void) XPackageCloseMountHandle(
        //      _In_ XPackageMountHandle mount) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XPackageCloseMountHandle(IntPtr mount);

        // STDAPI XPackageEnumerateChunkAvailability(
        //     _In_z_ const char* packageIdentifier,
        //     _In_ XPackageChunkSelectorType type,
        //     _In_ void* context,
        //     _In_ XPackageChunkAvailabilityCallback* callback) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageEnumerateChunkAvailability([MarshalAs(UnmanagedType.LPStr)] string packageIdentifier,
            XPackageChunkSelectorType type,
            IntPtr context,
            XPackageChunkAvailabilityCallback callback);

        // STDAPI XPackageEnumerateFeatures(
        //     _In_z_ const char* packageIdentifier,
        //     _In_opt_ void* context,
        //     _In_ XPackageFeatureEnumerationCallback* callback) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageEnumerateFeatures([MarshalAs(UnmanagedType.LPStr)] string packageIdentifier,
            IntPtr context,
            XPackageFeatureEnumerationCallbackInterop callback);

        // STDAPI XPackageEnumeratePackages(
        //     _In_ XPackageKind kind,
        //     _In_ XPackageEnumerationScope scope,
        //     _In_opt_ void* context,
        //     _In_ XPackageEnumerationCallback* callback) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageEnumeratePackages(
            XPackageKind kind,
            XPackageEnumerationScope scope,
            IntPtr context,
            XPackageEnumerationCallback callback);

        // STDAPI XPackageGetMountPathSize(
        //     _In_ XPackageMountHandle mount,
        //     _Out_ size_t* pathSize) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageGetMountPathSize(IntPtr mount, out UInt64 pathSize);

        // STDAPI XPackageGetMountPath(
        // _In_ XPackageMountHandle mount,
        // _In_ size_t pathSize,
        // _Out_writes_(pathSize) char* path) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageGetMountPath(IntPtr mount, UInt64 pathSize, StringBuilder path);

        // STDAPI XPackageGetWriteStats(_Out_ XPackageWriteStats* writeStats) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageGetWriteStats(out XPackageWriteStats writeStats);

        // STDAPI XPackageMountWithUiAsync(
        //     _In_z_ const char* packageIdentifier,
        //     _Inout_ XAsyncBlock* async) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageMountWithUiAsync([MarshalAs(UnmanagedType.LPStr)] string packageIdentifier, IntPtr async);

        // STDAPI XPackageMountWithUiResult(
        //     _Inout_ XAsyncBlock* async,
        //     _Out_ XPackageMountHandle* mount) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageMountWithUiResult(IntPtr async, out IntPtr mount);

        // STDAPI XPackageRegisterInstallationProgressChanged(
        //     _In_ XPackageInstallationMonitorHandle installationMonitor,
        //     _In_opt_ void* context,
        //     _In_ XPackageInstallationProgressCallback* callback,
        //     _Out_ XTaskQueueRegistrationToken* token) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageRegisterInstallationProgressChanged(
            IntPtr installationMonitor,
            IntPtr context,
            XPackageInstallationProgressCallback callback,
            out UInt64 token);

        // STDAPI XPackageRegisterPackageInstalled(
        //     _In_ XTaskQueueHandle queue,
        //     _In_opt_ void* context,
        //     _In_ XPackageInstalledCallback* callback,
        //     _Out_ XTaskQueueRegistrationToken* token) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageRegisterPackageInstalled(IntPtr queue, IntPtr context, XPackageInstalledCallback callback, out UInt64 token);

        // STDAPI XPackageUninstallUWPInstance(
        //     _In_z_ const char* packageName) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPackageUninstallUWPInstance([MarshalAs(UnmanagedType.LPStr)] string packageName);

        // STDAPI_(bool) XPackageUninstallPackage(_In_z_ const char* packageIdentifier) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XPackageUninstallPackage([MarshalAs(UnmanagedType.LPStr)] string packageIdentifier);

        // STDAPI_(bool) XPackageUnregisterInstallationProgressChanged(
        //     _In_ XPackageInstallationMonitorHandle installationMonitor,
        //     _In_ XTaskQueueRegistrationToken token,
        //     _In_ bool wait) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XPackageUnregisterInstallationProgressChanged(IntPtr installationMonitor,
            UInt64 token,
            [MarshalAs(UnmanagedType.I1)] bool wait);

        // STDAPI_(bool) XPackageUnregisterPackageInstalled(
        //     _In_ XTaskQueueRegistrationToken token,
        //     _In_ bool wait) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XPackageUnregisterPackageInstalled(UInt64 token, [MarshalAs(UnmanagedType.I1)] bool wait);
    }
}
