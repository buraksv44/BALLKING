using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    internal partial class XblInterop
    {
        //STDAPI XblTitleStorageBlobMetadataResultGetItems(
        //    _In_ XblTitleStorageBlobMetadataResultHandle resultHandle,
        //    _Out_ const XblTitleStorageBlobMetadata** items,
        //    _Out_ size_t* itemsCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblTitleStorageBlobMetadataResultGetItems(
            [In] IntPtr resultHandle,
            out IntPtr items,
            out SizeT itemsCount
            );

        //STDAPI XblTitleStorageBlobMetadataResultHasNext(
        //    _In_ XblTitleStorageBlobMetadataResultHandle resultHandle,
        //    _Out_ bool* hasNext
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblTitleStorageBlobMetadataResultHasNext(
            [In] IntPtr resultHandle,
            [MarshalAs(UnmanagedType.U1)] out bool hasNext
            );

        //STDAPI XblTitleStorageBlobMetadataResultGetNextAsync(
        //    _In_ XblTitleStorageBlobMetadataResultHandle resultHandle,
        //    _In_ uint32_t maxItems,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblTitleStorageBlobMetadataResultGetNextAsync(
            [In] IntPtr resultHandle,
            UInt32 maxItems,
            XAsyncBlockPtr async
            );

        //STDAPI XblTitleStorageBlobMetadataResultGetNextResult(
        //    _In_ XAsyncBlock* async,
        //    _Out_ XblTitleStorageBlobMetadataResultHandle* result
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblTitleStorageBlobMetadataResultGetNextResult(
            XAsyncBlockPtr async,
            out XblTitleStorageBlobMetadataResultHandle result
            );

        //STDAPI XblTitleStorageBlobMetadataResultDuplicateHandle(
        //    _In_ XblTitleStorageBlobMetadataResultHandle handle,
        //    _Out_ XblTitleStorageBlobMetadataResultHandle* duplicatedHandle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblTitleStorageBlobMetadataResultDuplicateHandle(
            [In] IntPtr handle,
            out XblTitleStorageBlobMetadataResultHandle duplicatedHandle
            );

        //STDAPI_(void) XblTitleStorageBlobMetadataResultCloseHandle(
        //    _In_ XblTitleStorageBlobMetadataResultHandle handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblTitleStorageBlobMetadataResultCloseHandle(
            [In] IntPtr handle
            );

        //STDAPI XblTitleStorageGetQuotaAsync(
        //    _In_ XblContextHandle xboxLiveContext,
        //    _In_z_ const char* serviceConfigurationId,
        //    _In_ XblTitleStorageType storageType,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblTitleStorageGetQuotaAsync(
            IntPtr xboxLiveContext,
            Byte[] serviceConfigurationId,
            XblTitleStorageType storageType,
            XAsyncBlockPtr async
            );

        //STDAPI XblTitleStorageGetQuotaResult(
        //    _In_ XAsyncBlock* async,
        //    _Out_ size_t* usedBytes,
        //    _Out_ size_t* quotaBytes
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblTitleStorageGetQuotaResult(
            XAsyncBlockPtr async,
            out SizeT usedBytes,
            out SizeT quotaBytes
            );

        //STDAPI XblTitleStorageGetBlobMetadataAsync(
        //    _In_ XblContextHandle xboxLiveContext,
        //    _In_z_ const char* serviceConfigurationId,
        //    _In_ XblTitleStorageType storageType,
        //    _In_z_ const char* blobPath,
        //    _In_ uint64_t xboxUserId,
        //    _In_ uint32_t skipItems,
        //    _In_ uint32_t maxItems,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblTitleStorageGetBlobMetadataAsync(
            IntPtr xboxLiveContext,
            Byte[] serviceConfigurationId,
            XblTitleStorageType storageType,
            Byte[] blobPath,
            UInt64 xboxUserId,
            UInt32 skipItems,
            UInt32 maxItems,
            XAsyncBlockPtr async
            );

        //STDAPI XblTitleStorageGetBlobMetadataResult(
        //    _In_ XAsyncBlock* async,
        //    _Out_ XblTitleStorageBlobMetadataResultHandle* result
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblTitleStorageGetBlobMetadataResult(
            XAsyncBlockPtr async,
            out XblTitleStorageBlobMetadataResultHandle result
            );

        //STDAPI XblTitleStorageDeleteBlobAsync(
        //    _In_ XblContextHandle xboxLiveContext,
        //    _In_ XblTitleStorageBlobMetadata blobMetadata,
        //    _In_ bool deleteOnlyIfEtagMatches,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblTitleStorageDeleteBlobAsync(
            IntPtr xboxLiveContext,
            XblTitleStorageBlobMetadata blobMetadata,
            [MarshalAs(UnmanagedType.U1)] bool deleteOnlyIfEtagMatches,
            XAsyncBlockPtr async
            );

        //STDAPI XblTitleStorageDownloadBlobAsync(
        //    _In_ XblContextHandle xboxLiveContext,
        //    _In_ XblTitleStorageBlobMetadata blobMetadata,
        //    _Out_writes_(blobBufferCount) uint8_t* blobBuffer,
        //    _In_ size_t blobBufferCount,
        //    _In_ XblTitleStorageETagMatchCondition etagMatchCondition,
        //    _In_opt_z_ const char* selectQuery,
        //    _In_ size_t preferredDownloadBlockSize,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblTitleStorageDownloadBlobAsync(
            IntPtr xboxLiveContext,
            XblTitleStorageBlobMetadata blobMetadata,
            IntPtr blobBuffer,
            SizeT blobBufferCount,
            XblTitleStorageETagMatchCondition etagMatchCondition,
            Byte[] selectQuery,
            SizeT preferredDownloadBlockSize,
            XAsyncBlockPtr async
            );

        //STDAPI XblTitleStorageDownloadBlobResult(
        //    _In_ XAsyncBlock* async,
        //    _Out_ XblTitleStorageBlobMetadata* blobMetadata
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblTitleStorageDownloadBlobResult(
            XAsyncBlockPtr async,
            out XblTitleStorageBlobMetadata blobMetadata
            );

        //STDAPI XblTitleStorageUploadBlobAsync(
        //    _In_ XblContextHandle xboxLiveContext,
        //    _In_ XblTitleStorageBlobMetadata blobMetadata,
        //    _In_ const uint8_t* blobBuffer,
        //    _In_ size_t blobBufferCount,
        //    _In_ XblTitleStorageETagMatchCondition etagMatchCondition,
        //    _In_ size_t preferredUploadBlockSize,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblTitleStorageUploadBlobAsync(
            IntPtr xboxLiveContext,
            XblTitleStorageBlobMetadata blobMetadata,
            IntPtr blobBuffer,
            SizeT blobBufferCount,
            XblTitleStorageETagMatchCondition etagMatchCondition,
            SizeT preferredUploadBlockSize,
            XAsyncBlockPtr async
            );

        //STDAPI XblTitleStorageUploadBlobResult(
        //    _In_ XAsyncBlock* async,
        //    _Out_ XblTitleStorageBlobMetadata* blobMetadata
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblTitleStorageUploadBlobResult(
            XAsyncBlockPtr async,
            out XblTitleStorageBlobMetadata blobMetadata
            );
    }
}
