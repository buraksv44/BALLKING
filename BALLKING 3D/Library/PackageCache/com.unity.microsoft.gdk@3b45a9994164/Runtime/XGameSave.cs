// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XGameSaveBlobInfo
    {
        internal XGameSaveBlobInfo(Interop.XGameSaveBlobInfo interop)
        {
            this.interop = interop;
        }

        public XGameSaveBlobInfo()
        {
            this.interop = new Interop.XGameSaveBlobInfo();
        }

        internal Interop.XGameSaveBlobInfo interop;

        public string Name
        {
            get => interop.name;
            set => interop.name = value;
        }

        public UInt32 Size
        {
            get => interop.size;
            set => interop.size = value;
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XGameSaveBlob
    {
        public XGameSaveBlobInfo Info { get; set; }
        public byte[] Data { get; set; }

        internal XGameSaveBlob(Interop.XGameSaveBlobInterop interop)
        {
            Info = new XGameSaveBlobInfo(interop.info);
            Data = InteropHelpers.MarshalArray<byte>(interop.data, interop.info.size);
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XGameSaveContainerInfo
    {
        internal XGameSaveContainerInfo(Interop.XGameSaveContainerInfo interop)
        {
            this.interop = interop;
        }

        public XGameSaveContainerInfo()
        {
            this.interop = new Interop.XGameSaveContainerInfo();
        }

        internal Interop.XGameSaveContainerInfo interop;

        public string Name
        {
            get => interop.name;
            set => interop.name = value;
        }

        public string DisplayName
        {
            get => interop.displayName;
            set => interop.displayName = value;
        }

        public UInt32 BlobCount
        {
            get => interop.blobCount;
            set => interop.blobCount = value;
        }

        public UInt64 TotalSize
        {
            get => interop.totalSize;
            set => interop.totalSize = value;
        }

        public Int64 LastModifiedTime
        {
            get => interop.lastModifiedTime;
            set => interop.lastModifiedTime = value;
        }

        public bool NeedsSync
        {
            get => interop.needsSync;
            set => interop.needsSync = value;
        }
    };

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XGameSaveProviderHandle : EquatableHandle
    {
        public XGameSaveProviderHandle(IntPtr handle) :
            base(IntPtr.Zero, true, handle)
        {
        }

        protected override bool ReleaseHandle()
        {
            NativeMethods.XGameSaveCloseProvider(this.Handle);
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
    public class XGameSaveContainerHandle : EquatableHandle
    {
        public XGameSaveContainerHandle(IntPtr handle) :
            base(IntPtr.Zero, true, handle)
        {
        }

        protected override bool ReleaseHandle()
        {
            NativeMethods.XGameSaveCloseContainer(this.Handle);
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
    public class XGameSaveUpdateHandle : EquatableHandle
    {
        public XGameSaveUpdateHandle(IntPtr handle) :
            base(IntPtr.Zero, true, handle)
        {
        }

        protected override bool ReleaseHandle()
        {
            NativeMethods.XGameSaveCloseUpdate(this.Handle);
            SetHandle(IntPtr.Zero);
            return true;
        }

        public override bool IsInvalid
        {
            get { return this.Handle == IntPtr.Zero; }
        }
    }

    public delegate bool XGameSaveBlobInfoCallback(XGameSaveBlobInfo info, IntPtr context);

    public delegate bool XGameSaveContainerInfoCallback(XGameSaveContainerInfo info, IntPtr context);

    partial class SDK
    {
        public static void XGameSaveCloseContainer(XGameSaveContainerHandle context)
        {
            context.Close();
        }

        public static void XGameSaveCloseProvider(XGameSaveProviderHandle provider)
        {
            provider.Close();
        }

        public static void XGameSaveCloseUpdate(XGameSaveUpdateHandle context)
        {
            context.Close();
        }

        public static Int32 XGameSaveInitializeProvider(XUserHandle requestingUser, string configurationId, bool syncOnDemand, out XGameSaveProviderHandle provider)
        {
            IntPtr providerPtr = default(IntPtr);
            provider = null;

            IntPtr userHandle = ( requestingUser != null) ? requestingUser.Handle : IntPtr.Zero;

            int hr = NativeMethods.XGameSaveInitializeProvider(userHandle, configurationId, syncOnDemand, out providerPtr);

            if (HR.SUCCEEDED(hr) || (hr == HR.E_GS_USER_CANCELED && providerPtr != default(IntPtr)))
            {
                provider = new XGameSaveProviderHandle(providerPtr);
            }

            return hr;
        }

        public static Int32 XGameSaveInitializeProviderAsync(XUserHandle requestingUser, string configurationId, bool syncOnDemand, XAsyncBlock asyncBlock)
        {
            IntPtr userHandle = ( requestingUser != null) ? requestingUser.Handle : IntPtr.Zero;

            return NativeMethods.XGameSaveInitializeProviderAsync(userHandle, configurationId, syncOnDemand, asyncBlock.InteropPtr);
        }

        public static Int32 XGameSaveInitializeProviderResult(XAsyncBlock asyncBlock, out XGameSaveProviderHandle provider)
        {
            IntPtr providerPtr = default(IntPtr);
            provider = null;
            int hr = NativeMethods.XGameSaveInitializeProviderResult(asyncBlock.InteropPtr, out providerPtr);

            if (HR.SUCCEEDED(hr) || (hr == HR.E_GS_USER_CANCELED && providerPtr != default(IntPtr)))
            {
                provider = new XGameSaveProviderHandle(providerPtr);
            }

            return hr;
        }

        public static Int32 XGameSaveGetRemainingQuota(XGameSaveProviderHandle provider, out Int64 remainingQuota)
        {
            return NativeMethods.XGameSaveGetRemainingQuota(provider.Handle, out remainingQuota);
        }

        public static Int32 XGameSaveGetRemainingQuotaAsync(XGameSaveProviderHandle provider, XAsyncBlock asyncBlock)
        {
            return NativeMethods.XGameSaveGetRemainingQuotaAsync(provider.Handle, asyncBlock.InteropPtr);
        }

        public static Int32 XGameSaveGetRemainingQuotaResult(XAsyncBlock async, out Int64 remainingQuota)
        {
            return NativeMethods.XGameSaveGetRemainingQuotaResult(async.InteropPtr, out remainingQuota);
        }

        public static Int32 XGameSaveDeleteContainer(XGameSaveProviderHandle provider, string containerName)
        {
            return NativeMethods.XGameSaveDeleteContainer(provider.Handle, containerName);
        }

        public static Int32 XGameSaveDeleteContainerAsync(XGameSaveProviderHandle provider, string containerName, XAsyncBlock async)
        {
            return NativeMethods.XGameSaveDeleteContainerAsync(provider.Handle, containerName, async.InteropPtr);
        }

        public static Int32 XGameSaveDeleteContainerResult(XAsyncBlock async)
        {
            return NativeMethods.XGameSaveDeleteContainerResult(async.InteropPtr);
        }

        [AOT.MonoPInvokeCallback(typeof(Interop.XGameSaveContainerInfoCallback))]
        private static bool OnContainerInfo(Interop.XGameSaveContainerInfo info, IntPtr context)
        {
            GCHandle handle = GCHandle.FromIntPtr(context);
            var wrapper = handle.Target as CallbackWrapper<Interop.XGameSaveContainerInfoCallback>;
            return wrapper.Callback(info, wrapper.Context);
        }

        public static Int32 XGameSaveGetContainerInfo(XGameSaveProviderHandle provider, string containerName, IntPtr context, XGameSaveContainerInfoCallback callback)
        {
            Interop.XGameSaveContainerInfoCallback interopCallback = (Interop.XGameSaveContainerInfo info, IntPtr callbackContext) =>
            {
                return callback(new XGameSaveContainerInfo(info), callbackContext);
            };

            using (var wrapper = new CallbackWrapper<Interop.XGameSaveContainerInfoCallback>(interopCallback, context, OnContainerInfo))
            {
                return NativeMethods.XGameSaveGetContainerInfo(provider.Handle, containerName, wrapper.CallbackContext, wrapper.StaticCallback);
            }
        }

        public static Int32 XGameSaveEnumerateContainerInfo(XGameSaveProviderHandle provider, IntPtr context, XGameSaveContainerInfoCallback callback)
        {
            Interop.XGameSaveContainerInfoCallback interopCallback = (Interop.XGameSaveContainerInfo info, IntPtr callbackContext) =>
            {
                return callback(new XGameSaveContainerInfo(info), callbackContext);
            };

            using (var wrapper = new CallbackWrapper<Interop.XGameSaveContainerInfoCallback>(interopCallback, context, OnContainerInfo))
            {
                return NativeMethods.XGameSaveEnumerateContainerInfo(provider.Handle, wrapper.CallbackContext, wrapper.StaticCallback);
            }
        }

        public static Int32 XGameSaveEnumerateContainerInfoByName(XGameSaveProviderHandle provider, string containerNamePrefix, IntPtr context, XGameSaveContainerInfoCallback callback)
        {
            Interop.XGameSaveContainerInfoCallback interopCallback = (Interop.XGameSaveContainerInfo info, IntPtr callbackContext) =>
            {
                return callback(new XGameSaveContainerInfo(info), callbackContext);
            };

            using (var wrapper = new CallbackWrapper<Interop.XGameSaveContainerInfoCallback>(interopCallback, context, OnContainerInfo))
            {
                return NativeMethods.XGameSaveEnumerateContainerInfoByName(provider.Handle, containerNamePrefix, wrapper.CallbackContext, wrapper.StaticCallback);
            }
        }

        public static Int32 XGameSaveReadBlobData(XGameSaveContainerHandle container,
            XGameSaveBlobInfo[] blobInfos,
            out XGameSaveBlob[] blobData)
        {
            blobData = null;

            int hr = HR.S_OK;

            string[] blobNames = blobInfos.Select(x => x.Name).ToArray();
            UInt32 blobCount = (UInt32)blobNames.Length;

            UInt64 sizeOfBlobs = (UInt64)blobInfos.Sum(x =>
                Marshal.SizeOf(typeof(Interop.XGameSaveBlobInterop)) +
                x.Name.Length + 1 + // +1 for null terminator
                Convert.ToInt32(x.Size));

            IntPtr blobDataInterop = Marshal.AllocHGlobal(Convert.ToInt32(sizeOfBlobs));

            try
            {
                hr = NativeMethods.XGameSaveReadBlobData(container.Handle, blobNames, ref blobCount, sizeOfBlobs, blobDataInterop);

                if (HR.SUCCEEDED(hr))
                {
                    blobData = InteropHelpers.MarshalArray<Interop.XGameSaveBlobInterop, XGameSaveBlob>(blobDataInterop,
                        blobCount,
                        (blobDataInterop) => new XGameSaveBlob(blobDataInterop));
                }
            }
            catch(Exception e)
            {
                hr = e.HResult;
            }
            finally
            {
                Marshal.FreeHGlobal(blobDataInterop);
            }

            return hr;
        }

        public static Int32 XGameSaveCreateContainer(XGameSaveProviderHandle provider, string containerName, out XGameSaveContainerHandle containerContext)
        {
            containerContext = null;
            IntPtr containerContextPtr;
            int hr = NativeMethods.XGameSaveCreateContainer(provider.Handle, containerName, out containerContextPtr);

            if (HR.SUCCEEDED(hr))
            {
                containerContext = new XGameSaveContainerHandle(containerContextPtr);
            }

            return hr;
        }

        public static Int32 XGameSaveReadBlobDataResult(XAsyncBlock async,
            UInt64 blobsSize,
            out XGameSaveBlob[] blobData)
        {
            blobData = null;

            int hr = HR.S_OK;
            IntPtr blobDataInterop = Marshal.AllocHGlobal(Convert.ToInt32(blobsSize));

            try
            {
                UInt32 countOfBlobs;
                hr = NativeMethods.XGameSaveReadBlobDataResult(async.InteropPtr, blobsSize, blobDataInterop, out countOfBlobs);

                if (HR.SUCCEEDED(hr))
                {
                    blobData = InteropHelpers.MarshalArray<XGameSaveBlobInterop, XGameSaveBlob>(blobDataInterop,
                        countOfBlobs,
                        (blobDataInterop) => new XGameSaveBlob(blobDataInterop));
                }
            }
            catch(Exception e)
            {
                hr = e.HResult;
            }
            finally
            {
                Marshal.FreeHGlobal(blobDataInterop);
            }

            return hr;
        }

        [AOT.MonoPInvokeCallback(typeof(Interop.XGameSaveBlobInfoCallback))]
        private static bool OnBlobInfo(Interop.XGameSaveBlobInfo info, IntPtr context)
        {
            GCHandle handle = GCHandle.FromIntPtr(context);
            var wrapper = handle.Target as CallbackWrapper<Interop.XGameSaveBlobInfoCallback>;
            return wrapper.Callback(info, wrapper.Context);
        }

        public static Int32 XGameSaveEnumerateBlobInfo(XGameSaveContainerHandle container, IntPtr context, XGameSaveBlobInfoCallback callback)
        {
            Interop.XGameSaveBlobInfoCallback interopCallback = (Interop.XGameSaveBlobInfo info, IntPtr callbackContext) =>
            {
                return callback(new XGameSaveBlobInfo(info), callbackContext);
            };

            using (var wrapper = new CallbackWrapper<Interop.XGameSaveBlobInfoCallback>(interopCallback, context, OnBlobInfo))
            {
                return NativeMethods.XGameSaveEnumerateBlobInfo(container.Handle, wrapper.CallbackContext, wrapper.StaticCallback);
            }
        }

        public static Int32 XGameSaveEnumerateBlobInfoByName(XGameSaveContainerHandle container, string blobNamePrefix, IntPtr context, XGameSaveBlobInfoCallback callback)
        {
            Interop.XGameSaveBlobInfoCallback interopCallback = (Interop.XGameSaveBlobInfo info, IntPtr callbackContext) =>
            {
                return callback(new XGameSaveBlobInfo(info), callbackContext);
            };

            using (var wrapper = new CallbackWrapper<Interop.XGameSaveBlobInfoCallback>(interopCallback, context, OnBlobInfo))
            {
                return NativeMethods.XGameSaveEnumerateBlobInfoByName(container.Handle, blobNamePrefix, wrapper.CallbackContext, wrapper.StaticCallback);
            }
        }

        public static Int32 XGameSaveReadBlobDataAsync(XGameSaveContainerHandle container, string[] blobNames, XAsyncBlock async)
        {
            return NativeMethods.XGameSaveReadBlobDataAsync(container.Handle, blobNames, (UInt32)blobNames.Length, async.InteropPtr);
        }

        public static Int32 XGameSaveCreateUpdate(XGameSaveContainerHandle container, string containerDisplayName, out XGameSaveUpdateHandle updateContext)
        {
            updateContext = null;
            IntPtr updateContextPtr;
            int hr = NativeMethods.XGameSaveCreateUpdate(container.Handle, containerDisplayName, out updateContextPtr);

            if (HR.SUCCEEDED(hr))
            {
                updateContext = new XGameSaveUpdateHandle(updateContextPtr);
            }

            return hr;
        }

        public static Int32 XGameSaveSubmitBlobWrite(XGameSaveUpdateHandle updateContext, string blobName, byte[] data)
        {
            return NativeMethods.XGameSaveSubmitBlobWrite(updateContext.Handle, blobName, data, (UInt32)data.Length);
        }

        public static Int32 XGameSaveSubmitBlobWrite(XGameSaveUpdateHandle updateContext, string blobName, byte[] data, UInt32 length)
        {
            return NativeMethods.XGameSaveSubmitBlobWrite(updateContext.Handle, blobName, data, length);
        }

        public static Int32 XGameSaveSubmitBlobDelete(XGameSaveUpdateHandle updateContext, string blobName)
        {
            return NativeMethods.XGameSaveSubmitBlobDelete(updateContext.Handle, blobName);
        }

        public static Int32 XGameSaveSubmitUpdate(XGameSaveUpdateHandle updateContext)
        {
            return NativeMethods.XGameSaveSubmitUpdate(updateContext.Handle);
        }

        public static Int32 XGameSaveSubmitUpdateAsync(XGameSaveUpdateHandle updateContext, XAsyncBlock async)
        {
            return NativeMethods.XGameSaveSubmitUpdateAsync(updateContext.Handle, async.InteropPtr);
        }

        public static Int32 XGameSaveSubmitUpdateResult(XAsyncBlock async)
        {
            return NativeMethods.XGameSaveSubmitUpdateResult(async.InteropPtr);
        }
    }
}
