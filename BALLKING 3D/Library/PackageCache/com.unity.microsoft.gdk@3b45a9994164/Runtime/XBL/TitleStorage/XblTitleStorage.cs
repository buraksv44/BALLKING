using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public delegate void XblTitleStorageBlobMetadataResultGetNextCompleted(Int32 hresult, XblTitleStorageBlobMetadataResultHandle result);
    public delegate void XblTitleStorageGetQuotaCompleted(Int32 hresult, UInt64 usedBytes, UInt64 quotaBytes);
    public delegate void XblTitleStorageGetBlobMetadataCompleted(Int32 hresult, XblTitleStorageBlobMetadataResultHandle result);
    public delegate void XblTitleStorageDeleteBlobCompleted(Int32 hresult);
    public delegate void XblTitleStorageDownloadBlobCompleted(Int32 hresult, XblTitleStorageBlobMetadata result, Byte[] blobBuffer);
    public delegate void XblTitleStorageUploadBlobCompleted(Int32 hresult, XblTitleStorageBlobMetadata result);

    public partial class SDK
    {
        public partial class XBL
        {
            public static Int32 XblTitleStorageBlobMetadataResultGetItems(
                XblTitleStorageBlobMetadataResultHandle resultHandle,
                out XblTitleStorageBlobMetadata[] items
                )
            {
                if (resultHandle == null)
                {
                    items = default(XblTitleStorageBlobMetadata[]);
                    return HR.E_INVALIDARG;
                }

                Int32 hresult = XblInterop.XblTitleStorageBlobMetadataResultGetItems(
                    resultHandle.Handle,
                    out IntPtr itemsIntPtr,
                    out SizeT itemsCount);

                if (HR.FAILED(hresult))
                {
                    items = default(XblTitleStorageBlobMetadata[]);
                    return hresult;
                }

                items = Converters.PtrToClassArray<XblTitleStorageBlobMetadata, Interop.XblTitleStorageBlobMetadata>(
                    itemsIntPtr,
                    itemsCount,
                    i => new XblTitleStorageBlobMetadata(i));

                return hresult;
            }

            public static Int32 XblTitleStorageBlobMetadataResultHasNext(
                XblTitleStorageBlobMetadataResultHandle result,
                out bool hasNext
                )
            {
                hasNext = false;
                if (result == null)
                {
                    return HR.E_INVALIDARG;
                }

                Int32 hr = XblInterop.XblTitleStorageBlobMetadataResultHasNext(result.Handle, out hasNext);

                return hr;
            }

            public static void XblTitleStorageBlobMetadataResultGetNextAsync(
                XblTitleStorageBlobMetadataResultHandle result,
                UInt32 maxItems,
                XblTitleStorageBlobMetadataResultGetNextCompleted completionRoutine
                )
            {
                if (result == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblTitleStorageBlobMetadataResultHandle));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Int32 hr = XblInterop.XblTitleStorageBlobMetadataResultGetNextResult(block, out Interop.XblTitleStorageBlobMetadataResultHandle interopHandle);

                    if (hr == HR.S_OK)
                        completionRoutine(hr, new XblTitleStorageBlobMetadataResultHandle(interopHandle));
                    else
                        completionRoutine(hr, default(XblTitleStorageBlobMetadataResultHandle));
                });

                Int32 hresult = XblInterop.XblTitleStorageBlobMetadataResultGetNextAsync(
                    result.Handle,
                    maxItems,
                    asyncBlock);

                if (HR.FAILED(hresult))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hresult, default(XblTitleStorageBlobMetadataResultHandle));
                }
            }

            public static Int32 XblTitleStorageBlobMetadataResultDuplicateHandle(XblTitleStorageBlobMetadataResultHandle handle, out XblTitleStorageBlobMetadataResultHandle duplicatedHandle)
            {
                if (handle == null)
                {
                    duplicatedHandle = default(XblTitleStorageBlobMetadataResultHandle);
                    return HR.E_INVALIDARG;
                }

                Interop.XblTitleStorageBlobMetadataResultHandle duplicatedInteropHandle;
                Int32 hresult = XblInterop.XblTitleStorageBlobMetadataResultDuplicateHandle(handle.Handle, out duplicatedInteropHandle);
                if (HR.SUCCEEDED(hresult))
                {
                    duplicatedHandle = new XblTitleStorageBlobMetadataResultHandle(duplicatedInteropHandle);
                }
                else
                {
                    duplicatedHandle = default(XblTitleStorageBlobMetadataResultHandle);
                }
                return hresult;
            }

            public static void XblTitleStorageBlobMetadataResultCloseHandle(XblTitleStorageBlobMetadataResultHandle handle)
            {
                if (handle == null)
                {
                    return;
                }

               handle.Close();
            }

            public static void XblTitleStorageGetQuotaAsync(
                XblContextHandle xboxLiveContext,
                string serviceConfigurationId,
                XblTitleStorageType storageType,
                XblTitleStorageGetQuotaCompleted completionRoutine
                )
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG, 0, 0);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    SizeT usedBytes;
                    SizeT quotaBytes;
                    Int32 hr = XblInterop.XblTitleStorageGetQuotaResult(block, out usedBytes, out quotaBytes);

                    if (hr == HR.S_OK)
                        completionRoutine(hr, usedBytes.ToUInt64(), quotaBytes.ToUInt64());
                    else
                        completionRoutine(hr, 0, 0);
                });

                Int32 hresult = XblInterop.XblTitleStorageGetQuotaAsync(
                    xboxLiveContext.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(serviceConfigurationId),
                    storageType,
                    asyncBlock);

                if (HR.FAILED(hresult))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hresult, 0, 0);
                }
            }

            public static void XblTitleStorageGetBlobMetadataAsync(
                XblContextHandle xboxLiveContext,
                string serviceConfigurationId,
                XblTitleStorageType storageType,
                string blobPath,
                UInt64 xboxUserId,
                UInt32 skipItems,
                UInt32 maxItems,
                XblTitleStorageGetBlobMetadataCompleted completionRoutine
                )
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblTitleStorageBlobMetadataResultHandle));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Int32 hr = XblInterop.XblTitleStorageGetBlobMetadataResult(block, out Interop.XblTitleStorageBlobMetadataResultHandle interopHandle);

                    if (hr == HR.S_OK)
                        completionRoutine(hr, new XblTitleStorageBlobMetadataResultHandle(interopHandle));
                    else
                        completionRoutine(hr, default(XblTitleStorageBlobMetadataResultHandle));
                });

                Int32 hresult = XblInterop.XblTitleStorageGetBlobMetadataAsync(
                    xboxLiveContext.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(serviceConfigurationId),
                    storageType,
                    Converters.StringToNullTerminatedUTF8ByteArray(blobPath),
                    xboxUserId,
                    skipItems,
                    maxItems,
                    asyncBlock);

                if (HR.FAILED(hresult))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hresult, default(XblTitleStorageBlobMetadataResultHandle));
                }
            }

            public static void XblTitleStorageDeleteBlobAsync(
                XblContextHandle xboxLiveContext,
                XblTitleStorageBlobMetadata blobMetadata,
                bool deleteOnlyIfEtagMatches,
                XblTitleStorageDeleteBlobCompleted completionRoutine
                )
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    completionRoutine(NativeMethods.XAsyncGetStatus(block.InteropPtr, wait: false));
                });

                Int32 hresult = XblInterop.XblTitleStorageDeleteBlobAsync(
                    xboxLiveContext.Handle,
                    blobMetadata.interop,
                    deleteOnlyIfEtagMatches,
                    asyncBlock);

                if (HR.FAILED(hresult))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hresult);
                }
            }

            public static void XblTitleStorageDownloadBlobAsync(
                XblContextHandle xboxLiveContext,
                XblTitleStorageBlobMetadata blobMetadata,
                XblTitleStorageETagMatchCondition etagMatchCondition,
                string selectQuery,
                UInt64 preferredDownloadBlockSize,
                XblTitleStorageDownloadBlobCompleted completionRoutine
                )
            {
                if (xboxLiveContext == null || blobMetadata.Length == 0)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblTitleStorageBlobMetadata), default(Byte[]));
                }

                int blobBufferCount = (Int32)blobMetadata.Length * Marshal.SizeOf(new Byte());
                IntPtr blobBuffer = Marshal.AllocHGlobal(blobBufferCount);

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Int32 hr = XblInterop.XblTitleStorageDownloadBlobResult(block, out Interop.XblTitleStorageBlobMetadata interopHandle);

                    if (hr == HR.S_OK)
                    {
                        byte[] buffer = new byte[blobBufferCount];
                        Marshal.Copy(blobBuffer, buffer, 0, buffer.Length);

                        completionRoutine(hr, new XblTitleStorageBlobMetadata(interopHandle), buffer);
                    }
                    else
                        completionRoutine(hr, default(XblTitleStorageBlobMetadata), default(Byte[]));

                    Marshal.FreeHGlobal(blobBuffer);
                });

                Int32 hresult = XblInterop.XblTitleStorageDownloadBlobAsync(
                    xboxLiveContext.Handle,
                    blobMetadata.interop,
                    blobBuffer,
                    new SizeT(blobBufferCount),
                    etagMatchCondition,
                    Converters.StringToNullTerminatedUTF8ByteArray(selectQuery),
                    new SizeT(preferredDownloadBlockSize),
                    asyncBlock);

                if (HR.FAILED(hresult))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hresult, default(XblTitleStorageBlobMetadata), default(Byte[]));
                }
            }

            public static void XblTitleStorageUploadBlobAsync(
                XblContextHandle xboxLiveContext,
                XblTitleStorageBlobMetadata blobMetadata,
                Byte[] blobBuffer,
                XblTitleStorageETagMatchCondition etagMatchCondition,
                UInt64 preferredDownloadBlockSize,
                XblTitleStorageUploadBlobCompleted completionRoutine
                )
            {
                if (xboxLiveContext == null || blobBuffer == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblTitleStorageBlobMetadata));
                }

                int blobBufferCount = (Int32)blobBuffer.Length * Marshal.SizeOf(new Byte());
                IntPtr blobBufferPtr = Marshal.AllocHGlobal(blobBufferCount);
                Marshal.Copy(blobBuffer, 0, blobBufferPtr, blobBufferCount);

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Int32 hr = XblInterop.XblTitleStorageUploadBlobResult(block, out Interop.XblTitleStorageBlobMetadata interopHandle);

                    if (hr == HR.S_OK)
                    {
                        completionRoutine(hr, new XblTitleStorageBlobMetadata(interopHandle));
                    }
                    else
                        completionRoutine(hr, default(XblTitleStorageBlobMetadata));

                    Marshal.FreeHGlobal(blobBufferPtr);
                });

                Int32 hresult = XblInterop.XblTitleStorageUploadBlobAsync(
                    xboxLiveContext.Handle,
                    blobMetadata.interop,
                    blobBufferPtr,
                    new SizeT(blobBufferCount),
                    etagMatchCondition,
                    new SizeT(preferredDownloadBlockSize),
                    asyncBlock);

                if (HR.FAILED(hresult))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hresult, default(XblTitleStorageBlobMetadata));
                }
            }
        }
    }
}
