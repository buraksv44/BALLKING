// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    // enum class XPackageChunkSelectorType : uint32_t
    // {
    //     Language,
    //     Tag,
    //     Chunk,
    //     Feature
    // };
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XPackageChunkSelectorType : UInt32
    {
        Language,
        Tag,
        Chunk,
        Feature
    };

    // enum class XPackageChunkAvailability : uint32_t
    // {
    //     Ready,
    //     Pending,
    //     Installable,
    //     Unavailable
    // };
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XPackageChunkAvailability : UInt32
    {
        Ready,
        Pending,
        Installable,
        Unavailable
    };

    // enum class XPackageKind : uint32_t
    // {
    //     Game,
    //     Content
    // };
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XPackageKind : UInt32
    {
        Game,
        Content
    };

    // enum class XPackageEnumerationScope : uint32_t
    // {
    //     ThisOnly,
    //     ThisAndRelated,
    //     ThisPublisher
    // };
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XPackageEnumerationScope : UInt32
    {
        ThisOnly,
        ThisAndRelated,
        ThisPublisher
    }


#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XPackageInstallationMonitorHandle : EquatableHandle
    {
        public XPackageInstallationMonitorHandle(IntPtr handle) :
            base(IntPtr.Zero, true, handle)
        {
        }

        protected override bool ReleaseHandle()
        {
            NativeMethods.XPackageCloseInstallationMonitorHandle(this.Handle);
            SetHandle(IntPtr.Zero);
            return true;
        }

        public override bool IsInvalid
        {
            get { return this.Handle == IntPtr.Zero; }
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XPackageMountHandle : EquatableHandle
    {
        public XPackageMountHandle(IntPtr handle) :
            base(IntPtr.Zero, true, handle)
        {
        }

        protected override bool ReleaseHandle()
        {
            NativeMethods.XPackageCloseMountHandle(this.Handle);
            SetHandle(IntPtr.Zero);
            return true;
        }

        public override bool IsInvalid
        {
            get { return this.Handle == IntPtr.Zero; }
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XPackageChunkSelector
    {
        internal XPackageChunkSelector(Interop.XPackageChunkSelectorInterop interop)
        {
            _languageOrTagOrFeature = Marshal.PtrToStringAnsi(interop.languageOrTagOrFeature);
            this.interop = interop;
        }

        public XPackageChunkSelector()
        {
            this.interop = new Interop.XPackageChunkSelectorInterop();
        }

        internal Interop.XPackageChunkSelectorInterop interop;
        internal string _languageOrTagOrFeature;

        public XPackageChunkSelectorType Type
        {
            get => this.interop.type;
            set => this.interop.type = value;
        }

        public string LanguageTagOrFeature
        {
            get => this._languageOrTagOrFeature;
            set => this._languageOrTagOrFeature = value;
        }
        public UInt32 ChunkId
        {
            get => this.interop.chunkId;
            set => this.interop.chunkId = value;
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XPackageInstallationProgress
    {
        internal XPackageInstallationProgress(Interop.XPackageInstallationProgress interop)
        {
            this.interop = interop;
        }

        public XPackageInstallationProgress()
        {
            this.interop = new Interop.XPackageInstallationProgress();
        }

        internal Interop.XPackageInstallationProgress interop;

        public UInt64 TotalBytes
        {
            get => this.interop.totalBytes;
            set => this.interop.totalBytes = value;
        }

        public UInt64 InstalledBytes
        {
            get => this.interop.installedBytes;
            set => this.interop.installedBytes = value;
        }

        public UInt64 LaunchBytes
        {
            get => this.interop.launchBytes;
            set => this.interop.launchBytes = value;
        }

        public bool Launchable
        {
            get => this.interop.launchable;
            set => this.interop.launchable = value;
        }

        public bool Completed
        {
            get => this.interop.completed;
            set => this.interop.completed = value;
        }
    };

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XPackageDetails
    {
        internal XPackageDetails(Interop.XPackageDetails interop)
        {
            this.interop = interop;
            this._XVersion = new XVersion(interop.version);
        }

        public XPackageDetails()
        {
            this.interop = new Interop.XPackageDetails();
            _XVersion = new XVersion();
        }

        internal Interop.XPackageDetails interop;
        internal XVersion _XVersion;

        public string PackageIdentifier
        {
            get => this.interop.packageIdentifier;
            set => this.interop.packageIdentifier = value;
        }

        public XVersion Version
        {
            get => this._XVersion;
            set => this._XVersion = value;
        }

        public XPackageKind Kind
        {
            get => this.interop.kind;
            set => this.interop.kind = value;
        }

        public string DisplayName
        {
            get => this.interop.displayName;
            set => this.interop.displayName = value;
        }

        public string Description
        {
            get => this.interop.description;
            set => this.interop.description = value;
        }

        public string Publisher
        {
            get => this.interop.publisher;
            set => this.interop.publisher = value;
        }

        public string StoreId
        {
            get => this.interop.storeId;
            set => this.interop.storeId = value;
        }

        public bool Installing
        {
            get => this.interop.installing;
            set => this.interop.installing = value;
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XPackageFeature
    {
        internal XPackageFeature(Interop.XPackageFeature interop)
        {
            this._interop = interop;
            this._storeIds = InteropHelpers.MarshalStringArrayAnsi(interop.storeIds, interop.storeIdCount);
        }

        public XPackageFeature()
        {
            this._interop = new Interop.XPackageFeature();
        }

        internal Interop.XPackageFeature _interop;

        private string[] _storeIds;

        public string Id
        {
            get => this._interop.id;
            set => this._interop.id = value;
        }

        public string DisplayName
        {
            get => this._interop.displayName;
            set => this._interop.displayName = value;
        }

        public string Tags
        {
            get => this._interop.tags;
            set => this._interop.tags = value;
        }

        public bool Hidden
        {
            get => this._interop.hidden;
            set => this._interop.hidden = value;
        }

        public string[] StoreIds
        {
            get => this._storeIds;
            set => this._storeIds = value;
        }
    };

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XPackageWriteStats
    {
        internal XPackageWriteStats(Interop.XPackageWriteStats interop)
        {
            this.interop = interop;
        }

        internal Interop.XPackageWriteStats interop;

        public XPackageWriteStats()
        {
            this.interop = new Interop.XPackageWriteStats();
        }

        public UInt64 Interval
        {
            get => this.interop.interval;
            set => this.interop.interval = value;
        }

        public UInt64 Budget
        {
            get => this.interop.budget;
            set => this.interop.budget = value;
        }

        public UInt64 Elapsed
        {
            get => this.interop.elapsed;
            set => this.interop.elapsed = value;
        }

        public UInt64 BytesWritten
        {
            get => this.interop.bytesWritten;
            set => this.interop.bytesWritten = value;
        }
    };

    public class XPackageRegisterPackageInstalledToken
    {
        internal Interop.XPackageRegisterPackageInstalledToken interop { get; private set; }

        internal XPackageRegisterPackageInstalledToken(Interop.XPackageInstalledCallback callback, IntPtr context)
        {
            interop = new Interop.XPackageRegisterPackageInstalledToken(callback, context);
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

        public void Dispose()
        {
            interop.Dispose();
        }
    };

    public class XPackageRegisterInstallationProgressChangedToken
    {
        internal Interop.XPackageRegisterInstallationProgressChangedToken interop { get; private set; }

        internal XPackageRegisterInstallationProgressChangedToken(XPackageInstallationMonitorHandle handle, Interop.XPackageInstallationProgressCallback callback, IntPtr context)
        {
            interop = new Interop.XPackageRegisterInstallationProgressChangedToken(handle, callback, context);
        }

        public UInt64 Token
        {
            get => interop.Token;
            set => interop.Token = value;
        }

        public bool IsValid { get { return interop.IsValid; } }

        public bool Unregister(XPackageInstallationMonitorHandle installationMonitor, bool wait)
        {
            return interop.Unregister(installationMonitor, wait);
        }

        public void Dispose()
        {
            interop.Dispose();
        }
    };

    public delegate bool XPackageEnumerationCallback(IntPtr context, XPackageDetails details);

    public delegate bool XPackageChunkAvailabilityCallback(IntPtr context, XPackageChunkSelector selector, XPackageChunkAvailability availability);

    public delegate bool XPackageFeatureEnumerationCallback(IntPtr context, XPackageFeature feature);

    public delegate void XPackageInstalledCallback(IntPtr context, XPackageDetails details);

    public delegate void XPackageInstallationProgressCallback(IntPtr context, XPackageInstallationMonitorHandle monitor);

    partial class SDK
    {
        public static readonly int XPACKAGE_IDENTIFIER_MAX_LENGTH = 33;
        public static readonly int LOCALE_NAME_MAX_LENGTH = 85;

        private static Int32 ProcessChunkSelector(XPackageChunkSelector[] selectors, ref XPackageChunkSelectorInterop[] nativeSelectors, out List<IntPtr> stringsToFree)
        {
            stringsToFree = new List<IntPtr>();

            for (int i = 0; i < selectors.Length; i++)
            {
                XPackageChunkSelector selector = selectors[i];
                nativeSelectors[i].type = selector.Type;
                switch (selector.Type)
                {
                    case XPackageChunkSelectorType.Chunk:
                        nativeSelectors[i].chunkId = selector.ChunkId;
                        break;
                    case XPackageChunkSelectorType.Language:
                    case XPackageChunkSelectorType.Tag:
                    case XPackageChunkSelectorType.Feature:
                        nativeSelectors[i].languageOrTagOrFeature = InteropHelpers.MarshalStringUtf8(selector.LanguageTagOrFeature);
                        stringsToFree.Add(nativeSelectors[i].languageOrTagOrFeature);
                        break;
                    default:
                        return HR.E_INVALIDARG;
                }
            }

            return HR.S_OK;
        }

        public static Int32 XPackageCreateInstallationMonitor(
            string packageIdentifier,
            XPackageChunkSelector[] selectors,
            UInt32 minimumUpdateIntervalMs,
            XTaskQueueHandle queue,
            out XPackageInstallationMonitorHandle installationMonitor)
        {
            Int32 hr = HR.S_OK;
            installationMonitor = null;

            IntPtr interopQueue = queue != null ? queue.Handle : IntPtr.Zero;

            if(selectors != null)
            {
                var nativeSelectors = new XPackageChunkSelectorInterop[selectors.Length];
                List<IntPtr> stringsToFree = new List<IntPtr>();

                try
                {
                    hr = ProcessChunkSelector(selectors, ref nativeSelectors, out stringsToFree);

                    if (Interop.HR.SUCCEEDED(hr))
                    {
                        IntPtr interopHandle;
                        hr = NativeMethods.XPackageCreateInstallationMonitor(
                            packageIdentifier,
                            (uint)nativeSelectors.Length,
                            nativeSelectors,
                            minimumUpdateIntervalMs,
                            interopQueue,
                            out interopHandle);

                        if (Interop.HR.SUCCEEDED(hr))
                        {
                            installationMonitor = new XPackageInstallationMonitorHandle(interopHandle);
                        }
                    }
                }
                finally
                {
                    foreach (IntPtr strPtr in stringsToFree)
                    {
                        Marshal.FreeCoTaskMem(strPtr);
                    }
                }
            }
            else
            {
                IntPtr interopHandle;
                hr = NativeMethods.XPackageCreateInstallationMonitor(
                    packageIdentifier,
                    0,
                    null,
                    minimumUpdateIntervalMs,
                    interopQueue,
                    out interopHandle);

                if (Interop.HR.SUCCEEDED(hr))
                {
                    installationMonitor = new XPackageInstallationMonitorHandle(interopHandle);
                }
            }

            return hr;
        }

        public static void XPackageCloseInstallationMonitorHandle(XPackageInstallationMonitorHandle installationMonitor)
        {
            installationMonitor.Close();
        }

        public static Int32 XPackageGetCurrentProcessPackageIdentifier(out string buffer)
        {
            buffer = null;

            StringBuilder bufferSb = new StringBuilder(XPACKAGE_IDENTIFIER_MAX_LENGTH);
            int hr = NativeMethods.XPackageGetCurrentProcessPackageIdentifier((ulong)XPACKAGE_IDENTIFIER_MAX_LENGTH, bufferSb);

            if (HR.SUCCEEDED(hr))
            {
                buffer = bufferSb.ToString();
            }

            return hr;
        }

        public static bool XPackageIsPackagedProcess()
        {
            return NativeMethods.XPackageIsPackagedProcess();
        }

        public static void XPackageGetInstallationProgress(XPackageInstallationMonitorHandle installationMonitor,
            out XPackageInstallationProgress progress)
        {
            Interop.XPackageInstallationProgress interopProgress;

            NativeMethods.XPackageGetInstallationProgress(installationMonitor.Handle, out interopProgress);

            progress = new XPackageInstallationProgress(interopProgress);
        }

        public static void XPackageUpdateInstallationMonitor(XPackageInstallationMonitorHandle installationMonitor)
        {
            NativeMethods.XPackageUpdateInstallationMonitor(installationMonitor.Handle);
        }

        public static Int32 XPackageGetUserLocale(out string locale)
        {
            locale = null;

            StringBuilder localeSb = new StringBuilder(LOCALE_NAME_MAX_LENGTH);
            int hr = NativeMethods.XPackageGetUserLocale((ulong)LOCALE_NAME_MAX_LENGTH, localeSb);

            if (HR.SUCCEEDED(hr))
            {
                locale = localeSb.ToString();
            }

            return hr;
        }

        public static Int32 XPackageFindChunkAvailability(string packageIdentifier,
            XPackageChunkSelector[] selectors,
            out XPackageChunkAvailability availability)
        {
            availability = default(XPackageChunkAvailability);

            Int32 hr = HR.S_OK;

            if(selectors != null)
            {
                List<IntPtr> stringsToFree = new List<IntPtr>();
                try
                {
                    var nativeSelectors = new XPackageChunkSelectorInterop[selectors.Length];
                    hr = ProcessChunkSelector(selectors, ref nativeSelectors, out stringsToFree);

                    if (Interop.HR.SUCCEEDED(hr))
                    {
                        hr = NativeMethods.XPackageFindChunkAvailability(packageIdentifier, (uint)nativeSelectors.Length, nativeSelectors, out availability);
                    }
                }
                finally
                {
                    foreach (IntPtr strPtr in stringsToFree)
                    {
                        Marshal.FreeCoTaskMem(strPtr);
                    }
                }
            }
            else
            {
                hr = NativeMethods.XPackageFindChunkAvailability(packageIdentifier, 0, null, out availability);
            }

            return hr;
        }

        public static Int32 XPackageChangeChunkInstallOrder(string packageIdentifier,
            XPackageChunkSelector[] selectors)
        {
            Int32 hr = HR.S_OK;
            List<IntPtr> stringsToFree = new List<IntPtr>();

            try
            {
                var nativeSelectors = new XPackageChunkSelectorInterop[selectors.Length];
                hr = ProcessChunkSelector(selectors, ref nativeSelectors, out stringsToFree);

                if (Interop.HR.SUCCEEDED(hr))
                {
                    hr = NativeMethods.XPackageChangeChunkInstallOrder(packageIdentifier, (uint)nativeSelectors.Length, nativeSelectors);
                }
            }
            finally
            {
                foreach (IntPtr strPtr in stringsToFree)
                {
                    Marshal.FreeCoTaskMem(strPtr);
                }
            }

            return hr;
        }

        public static Int32 XPackageInstallChunks(string packageIdentifier,
            XPackageChunkSelector[] selectors,
            UInt32 minimumUpdateIntervalMs,
            bool suppressUserConfirmation,
            XTaskQueueHandle queue,
            out XPackageInstallationMonitorHandle installationMonitor)
        {
            List<IntPtr> stringsToFree = new List<IntPtr>();
            Int32 hr = HR.S_OK;
            try
            {
                installationMonitor = null;

                var nativeSelectors = new XPackageChunkSelectorInterop[selectors.Length];
                hr = ProcessChunkSelector(selectors, ref nativeSelectors, out stringsToFree);

                if (Interop.HR.SUCCEEDED(hr))
                {
                    IntPtr interopHandle;
                    IntPtr interopQueue = queue != null ? queue.Handle : IntPtr.Zero;
                    hr = NativeMethods.XPackageInstallChunks(packageIdentifier, (uint)nativeSelectors.Length, nativeSelectors, minimumUpdateIntervalMs, suppressUserConfirmation, interopQueue, out interopHandle);

                    if (Interop.HR.SUCCEEDED(hr))
                    {
                        installationMonitor = new XPackageInstallationMonitorHandle(interopHandle);
                    }
                }
            }
            finally
            {
                foreach (IntPtr strPtr in stringsToFree)
                {
                    Marshal.FreeCoTaskMem(strPtr);
                }
            }

            return hr;
        }

        public static Int32 XPackageInstallChunksAsync(string packageIdentifier,
            XPackageChunkSelector[] selectors,
            UInt32 minimumUpdateIntervalMs,
            bool suppressUserConfirmation,
            XAsyncBlock async)
        {
            var nativeSelectors = new XPackageChunkSelectorInterop[selectors.Length];

            Int32 hr = HR.S_OK;
            List<IntPtr> stringsToFree = new List<IntPtr>();
            try
            {
                hr = ProcessChunkSelector(selectors, ref nativeSelectors, out stringsToFree);

                if (Interop.HR.SUCCEEDED(hr))
                {
                    hr = NativeMethods.XPackageInstallChunksAsync(packageIdentifier, (uint)nativeSelectors.Length, nativeSelectors, minimumUpdateIntervalMs, suppressUserConfirmation, async.InteropPtr);
                }
            }
            finally
            {
                foreach (IntPtr strPtr in stringsToFree)
                {
                    Marshal.FreeCoTaskMem(strPtr);
                }
            }

            return hr;
        }

        public static Int32 XPackageInstallChunksResult(XAsyncBlock asyncBlock, out XPackageInstallationMonitorHandle installationMonitor)
        {
            installationMonitor = null;

            IntPtr interopHandle;
            Int32 hr = NativeMethods.XPackageInstallChunksResult(asyncBlock.InteropPtr, out interopHandle);

            if (Interop.HR.SUCCEEDED(hr))
            {
                installationMonitor = new XPackageInstallationMonitorHandle(interopHandle);
            }

            return hr;
        }

        public static Int32 XPackageEstimateDownloadSize(string packageIdentifier,
            XPackageChunkSelector[] selectors,
            out UInt64 downloadSize,
            out bool shouldPresentUserConfirmation)
        {
            downloadSize = 0;
            shouldPresentUserConfirmation = false;

            Int32 hr = HR.S_OK;
            List<IntPtr> stringsToFree = new List<IntPtr>();
            try
            {
                var nativeSelectors = new XPackageChunkSelectorInterop[selectors.Length];
                hr = ProcessChunkSelector(selectors, ref nativeSelectors, out stringsToFree);

                if (Interop.HR.SUCCEEDED(hr))
                {
                    hr = NativeMethods.XPackageEstimateDownloadSize(packageIdentifier, (uint)nativeSelectors.Length, nativeSelectors, out downloadSize, out shouldPresentUserConfirmation);
                }
            }
            finally
            {
                foreach (IntPtr strPtr in stringsToFree)
                {
                    Marshal.FreeCoTaskMem(strPtr);
                }
            }

            return hr;
        }

        public static Int32 XPackageUninstallChunks(string packageIdentifier, XPackageChunkSelector[] selectors)
        {

            var nativeSelectors = new XPackageChunkSelectorInterop[selectors.Length];

            Int32 hr = HR.S_OK;
            List<IntPtr> stringsToFree = new List<IntPtr>();
            try
            {
                hr = ProcessChunkSelector(selectors, ref nativeSelectors, out stringsToFree);

                if (Interop.HR.SUCCEEDED(hr))
                {
                    hr = NativeMethods.XPackageUninstallChunks(packageIdentifier, (uint)nativeSelectors.Length, nativeSelectors);
                }
            }
            finally
            {
                foreach (IntPtr strPtr in stringsToFree)
                {
                    Marshal.FreeCoTaskMem(strPtr);
                }
            }

            return hr;
        }

        public static void XPackageCloseMountHandle(XPackageMountHandle mount)
        {
            mount.Close();
        }

        [AOT.MonoPInvokeCallback(typeof(Interop.XPackageChunkAvailabilityCallback))]
        private static bool OnPackageChunkAvailability(IntPtr context, Interop.XPackageChunkSelectorInterop selector, XPackageChunkAvailability availability)
        {
            GCHandle handle = GCHandle.FromIntPtr(context);
            var wrapper = handle.Target as CallbackWrapper<Interop.XPackageChunkAvailabilityCallback>;
            return wrapper.Callback(wrapper.Context, selector, availability);
        }

        public static Int32 XPackageEnumerateChunkAvailability(string packageIdentifier,
            XPackageChunkSelectorType type,
            IntPtr context,
            XPackageChunkAvailabilityCallback callback)
        {
            Interop.XPackageChunkAvailabilityCallback interopCallback = (_context, _selector, _availability) =>
            {
                return callback(_context, new XPackageChunkSelector(_selector), _availability);
            };

            using (var wrapper = new CallbackWrapper<Interop.XPackageChunkAvailabilityCallback>(interopCallback, context, OnPackageChunkAvailability))
            {
                return NativeMethods.XPackageEnumerateChunkAvailability(packageIdentifier, type, wrapper.CallbackContext, wrapper.StaticCallback);
            }
        }

        [AOT.MonoPInvokeCallback(typeof(Interop.XPackageFeatureEnumerationCallbackInterop))]
        static bool OnXPackageFeature(IntPtr context, Interop.XPackageFeature feature)
        {
            GCHandle handle = GCHandle.FromIntPtr(context);
            var wrapper = handle.Target as CallbackWrapper<Interop.XPackageFeatureEnumerationCallbackInterop>;
            return wrapper.Callback(wrapper.Context, feature);
        }

        public static Int32 XPackageEnumerateFeatures(string packageIdentifier, IntPtr context, XPackageFeatureEnumerationCallback callback)
        {
            Interop.XPackageFeatureEnumerationCallbackInterop localCallback = (_context, _featureInterop) =>
            {
                XPackageFeature feature = new XPackageFeature(_featureInterop);
                return callback(_context, feature);
            };

            using (var wrapper = new CallbackWrapper<Interop.XPackageFeatureEnumerationCallbackInterop>(localCallback, context, OnXPackageFeature))
            {
                return NativeMethods.XPackageEnumerateFeatures(packageIdentifier, wrapper.CallbackContext, wrapper.StaticCallback);
            }
        }

        [AOT.MonoPInvokeCallback(typeof(Interop.XPackageEnumerationCallback))]
        static bool OnPackageEnumeration(IntPtr context, Interop.XPackageDetails details)
        {
            GCHandle handle = GCHandle.FromIntPtr(context);
            var wrapper = handle.Target as CallbackWrapper<Interop.XPackageEnumerationCallback>;
            return wrapper.Callback(wrapper.Context, details);
        }

        public static Int32 XPackageEnumeratePackages(XPackageKind kind,
            XPackageEnumerationScope scope,
            IntPtr context,
            XPackageEnumerationCallback callback)
        {
            Interop.XPackageEnumerationCallback interopCallback = (_context, _details) =>
            {
                XPackageDetails details = new XPackageDetails(_details);
                return callback(_context, details);
            };
            using (var wrapper = new CallbackWrapper<Interop.XPackageEnumerationCallback>(interopCallback, context, OnPackageEnumeration))
            {
                return NativeMethods.XPackageEnumeratePackages(kind, scope, wrapper.CallbackContext, wrapper.StaticCallback);
            }
        }

        public static Int32 XPackageGetMountPathSize(XPackageMountHandle mount, out UInt64 pathSize)
        {
            return NativeMethods.XPackageGetMountPathSize(mount.Handle, out pathSize);
        }

        public static Int32 XPackageGetMountPath(XPackageMountHandle mount, UInt64 pathSize, out string path)
        {
            path = null;

            StringBuilder pathSb = new StringBuilder((int)pathSize);
            int hr = NativeMethods.XPackageGetMountPath(mount.Handle, pathSize, pathSb);
            if (HR.SUCCEEDED(hr))
            {
                path = pathSb.ToString();
            }

            return hr;
        }

        public static Int32 XPackageGetWriteStats(out XPackageWriteStats writeStats)
        {
            writeStats = default;
            Interop.XPackageWriteStats interopWriteStats = default;

            Int32 hr = NativeMethods.XPackageGetWriteStats(out interopWriteStats);

            if (HR.SUCCEEDED(hr))
            {
                writeStats = new XPackageWriteStats(interopWriteStats);
            }

            return hr;
        }

        public static Int32 XPackageMountWithUiAsync(string packageIdentifier,
            XAsyncBlock asyncBlock)
        {
            return NativeMethods.XPackageMountWithUiAsync(packageIdentifier, asyncBlock.InteropPtr);
        }

        public static Int32 XPackageMountWithUiResult(XAsyncBlock async,
            out XPackageMountHandle mount)
        {
            mount = null;

            IntPtr mountHandle;
            int hr = NativeMethods.XPackageMountWithUiResult(async.InteropPtr, out mountHandle);

            if (Interop.HR.SUCCEEDED(hr))
            {
                mount = new XPackageMountHandle(mountHandle);
            }

            return hr;
        }

        public static Int32 XPackageRegisterInstallationProgressChanged(XPackageInstallationMonitorHandle installationMonitorHandle,
            IntPtr context,
            XPackageInstallationProgressCallback callback,
            out XPackageRegisterInstallationProgressChangedToken token)
        {
            Interop.XPackageInstallationProgressCallback interopCallback = (_context, _monitor) =>
            {
                callback(_context, installationMonitorHandle);
            };

            token = new XPackageRegisterInstallationProgressChangedToken(installationMonitorHandle, interopCallback, context);

            UInt64 tokenValue;
            int hr = NativeMethods.XPackageRegisterInstallationProgressChanged(installationMonitorHandle.Handle,
                token.interop.CallbackContext,
                token.interop.StaticCallback,
                out tokenValue);
            if (HR.SUCCEEDED(hr))
            {
                token.Token = tokenValue;
            }
            else
            {
                token.Dispose();
                token = null;
            }

            return hr;
        }

        public static Int32 XPackageRegisterPackageInstalled(XTaskQueueHandle queue,
            IntPtr context,
            XPackageInstalledCallback callback,
            out XPackageRegisterPackageInstalledToken token)
        {
            Interop.XPackageInstalledCallback interopCallback = (_context, _details) =>
            {
                callback(_context, new XPackageDetails(_details));
            };

            token = new XPackageRegisterPackageInstalledToken(interopCallback, context);

            UInt64 tokenValue;
            int hr = NativeMethods.XPackageRegisterPackageInstalled(queue.Handle,
                token.interop.CallbackContext,
                token.interop.StaticCallback,
                out tokenValue);
            if (HR.SUCCEEDED(hr))
            {
                token.Token = tokenValue;
            }
            else
            {
                token.Dispose();
                token = null;
            }

            return hr;
        }

        public static Int32 XPackageUninstallUWPInstance(string packageName)
        {
            return NativeMethods.XPackageUninstallUWPInstance(packageName);
        }

        public static bool XPackageUninstallPackage(string packageIdentifier)
        {
            return NativeMethods.XPackageUninstallPackage(packageIdentifier);
        }

        public static bool XPackageUnregisterInstallationProgressChanged(XPackageInstallationMonitorHandle installationMonitor,
            XPackageRegisterInstallationProgressChangedToken token,
            bool wait)
        {
            return token.Unregister(installationMonitor, wait);
        }

        public static bool XPackageUnregisterInstallationProgressChanged(XPackageInstallationMonitorHandle installationMonitor,
            XPackageRegisterInstallationProgressChangedToken token)
        {
            return XPackageUnregisterInstallationProgressChanged(installationMonitor, token, true);
        }

        public static bool XPackageUnregisterPackageInstalled(XPackageRegisterPackageInstalledToken token)
        {
            return XPackageUnregisterPackageInstalled(token, true);
        }

        public static bool XPackageUnregisterPackageInstalled(XPackageRegisterPackageInstalledToken token,
            bool wait)
        {
            return token.Unregister(wait);
        }
    }
}
