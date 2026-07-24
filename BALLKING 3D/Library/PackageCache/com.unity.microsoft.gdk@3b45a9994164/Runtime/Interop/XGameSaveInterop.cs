using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    // struct XGameSaveBlobInfo
    // {
    //     _Field_z_ const char* name;  // unique blob name (unique to container)
    //     uint32_t size;           // size of the saved data
    // };
    // Public by design to support size calculations for XGameSaveReadBlobDataAsync
    [StructLayout(LayoutKind.Sequential)]
    public struct XGameSaveBlobInfo
    {
        [MarshalAs(UnmanagedType.LPStr)] internal string name;
        internal UInt32 size;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct XGameSaveBlobInterop
    {
        internal XGameSaveBlobInfo info;
        internal IntPtr data;
    }

    // struct XGameSaveContainerInfo
    // {
    //     _Field_z_ const char*  name;           // unique container name
    //     _Field_z_ const char*  displayName;    // display name
    //     uint32_t blobCount;      // number of blobs in the container
    //     uint64_t totalSize;      // size of all the blobs in the container
    //     time_t lastModifiedTime; // last time this container was updated
    //     bool needsSync;          // sync status, if not synced any operation on this container may result in a network call (if using SyncOnDemand)
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XGameSaveContainerInfo
    {
        [MarshalAs(UnmanagedType.LPStr)] internal string name;
        [MarshalAs(UnmanagedType.LPStr)] internal string displayName;
        internal UInt32 blobCount;
        internal UInt64 totalSize;
        internal Int64 lastModifiedTime;
        [MarshalAs(UnmanagedType.I1)] internal bool needsSync;
    };

    //typedef bool CALLBACK XGameSaveBlobInfoCallback(_In_ const XGameSaveBlobInfo* info, _In_opt_ void* context);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    internal delegate bool XGameSaveBlobInfoCallback(XGameSaveBlobInfo info, IntPtr context);

    //typedef bool CALLBACK XGameSaveContainerInfoCallback(_In_ const XGameSaveContainerInfo* info, _In_opt_ void* context);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    internal delegate bool XGameSaveContainerInfoCallback(XGameSaveContainerInfo info, IntPtr context);

    partial class NativeMethods
    {
        // STDAPI XGameSaveInitializeProvider(_In_ XUserHandle requestingUser, _In_z_ const char* configurationId, _In_ bool syncOnDemand, _Outptr_result_nullonfailure_ XGameSaveProviderHandle* provider) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveInitializeProvider(IntPtr requestingUser, [MarshalAs(UnmanagedType.LPStr)] string configurationId, [MarshalAs(UnmanagedType.I1)] bool syncOnDemand, out IntPtr provider);

        // STDAPI XGameSaveInitializeProviderAsync(_In_ XUserHandle requestingUser, _In_z_ const char* configurationId, _In_ bool syncOnDemand, _In_ XAsyncBlock* async) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveInitializeProviderAsync(IntPtr requestingUser, [MarshalAs(UnmanagedType.LPStr)] string configurationId,
            [MarshalAs(UnmanagedType.I1)] bool syncOnDemand, IntPtr async);

        // STDAPI XGameSaveInitializeProviderResult(_In_ XAsyncBlock* async, _Outptr_result_nullonfailure_ XGameSaveProviderHandle* provider) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveInitializeProviderResult(IntPtr async, out IntPtr provider);

        // STDAPI_(void) XGameSaveCloseProvider(_In_ XGameSaveProviderHandle provider) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XGameSaveCloseProvider(IntPtr provider);

        // STDAPI XGameSaveGetRemainingQuota(_In_ XGameSaveProviderHandle provider, _Out_ int64_t* remainingQuota) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveGetRemainingQuota(IntPtr provider, out Int64 remainingQuota);

        // STDAPI XGameSaveGetRemainingQuotaAsync(_In_ XGameSaveProviderHandle provider, _In_ XAsyncBlock* async) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveGetRemainingQuotaAsync(IntPtr provider, IntPtr xAsyncBlockInterop);

        // STDAPI XGameSaveGetRemainingQuotaResult(_In_ XAsyncBlock* async, _Out_ int64_t* remainingQuota) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveGetRemainingQuotaResult(IntPtr async, out Int64 remainingQuota);

        // STDAPI XGameSaveDeleteContainer(_In_ XGameSaveProviderHandle provider, _In_z_ const char* containerName) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveDeleteContainer(IntPtr provider, [MarshalAs(UnmanagedType.LPStr)] string containerName);

        // STDAPI XGameSaveDeleteContainerAsync(_In_ XGameSaveProviderHandle provider, _In_z_ const char* containerName, _In_ XAsyncBlock* async) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveDeleteContainerAsync(IntPtr provider, [MarshalAs(UnmanagedType.LPStr)] string containerName, IntPtr async);

        // STDAPI XGameSaveDeleteContainerResult(_In_ XAsyncBlock* async) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveDeleteContainerResult(IntPtr async);

        // STDAPI XGameSaveGetContainerInfo(_In_ XGameSaveProviderHandle provider, _In_z_ const char* containerName, _In_opt_ void* context, _In_ XGameSaveContainerInfoCallback* callback) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveGetContainerInfo(IntPtr provider, [MarshalAs(UnmanagedType.LPStr)] string containerName, IntPtr context, XGameSaveContainerInfoCallback callback);

        //STDAPI XGameSaveEnumerateContainerInfo(_In_ XGameSaveProviderHandle provider, _In_opt_ void* context, _In_ XGameSaveContainerInfoCallback* callback) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveEnumerateContainerInfo(IntPtr provider, IntPtr context, XGameSaveContainerInfoCallback callback);

        // STDAPI XGameSaveEnumerateContainerInfoByName(_In_ XGameSaveProviderHandle provider, _In_opt_z_ const char* containerNamePrefix, _In_opt_ void* context, _In_ XGameSaveContainerInfoCallback* callback) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveEnumerateContainerInfoByName(IntPtr provider, [MarshalAs(UnmanagedType.LPStr)] string containerNamePrefix, IntPtr context, XGameSaveContainerInfoCallback callback);

        // STDAPI XGameSaveCreateContainer(_In_ XGameSaveProviderHandle provider, _In_z_ const char* containerName, _Outptr_result_nullonfailure_ XGameSaveContainerHandle* containerContext) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveCreateContainer(IntPtr provider, [MarshalAs(UnmanagedType.LPStr)] string containerName, out IntPtr containerContext);

        // STDAPI_(void) XGameSaveCloseContainer(_In_ XGameSaveContainerHandle context) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XGameSaveCloseContainer(IntPtr context);

        // STDAPI XGameSaveEnumerateBlobInfo(_In_ XGameSaveContainerHandle container, _In_opt_ void* context, _In_ XGameSaveBlobInfoCallback* callback) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveEnumerateBlobInfo(IntPtr container, IntPtr context, XGameSaveBlobInfoCallback callback);

        // STDAPI XGameSaveEnumerateBlobInfoByName(_In_ XGameSaveContainerHandle container, _In_opt_z_ const char* blobNamePrefix, _In_opt_ void* context, _In_ XGameSaveBlobInfoCallback* callback) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveEnumerateBlobInfoByName(IntPtr container, [MarshalAs(UnmanagedType.LPStr)] string blobNamePrefix, IntPtr context, XGameSaveBlobInfoCallback callback);

        // STDAPI XGameSaveReadBlobData(_In_ XGameSaveContainerHandle container,
        //     _In_opt_z_count_(*countOfBlobs) const char** blobNames,
        //     _Inout_ uint32_t* countOfBlobs,
        //     _In_ size_t blobsSize,
        //     _Out_writes_bytes_(blobsSize) XGameSaveBlob* blobData) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveReadBlobData(IntPtr container,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 2)] string[] blobNames,
            ref UInt32 countOfBlobs,
            UInt64 blobsSize,
            IntPtr blobData);

        // STDAPI XGameSaveReadBlobDataAsync(_In_ XGameSaveContainerHandle container, _In_opt_z_count_(countOfBlobs) const char** blobNames,
        //  _In_ uint32_t countOfBlobs,
        //  _In_ XAsyncBlock* async) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveReadBlobDataAsync(IntPtr container,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 2)] string[] blobNames,
            UInt32 countOfBlobs,
            IntPtr async);

        // STDAPI XGameSaveReadBlobDataResult(_In_ XAsyncBlock* async,
        //  _In_ size_t blobsSize,
        //  _Out_writes_bytes_(blobsSize) XGameSaveBlob* blobData,
        //  _Out_ uint32_t* countOfBlobs) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveReadBlobDataResult(IntPtr async,
            UInt64 blobsSize,
            IntPtr blobData,
            out UInt32 countOfBlobs);

        // STDAPI XGameSaveCreateUpdate(_In_ XGameSaveContainerHandle container,
        //  _In_z_ const char* containerDisplayName,
        //  _Outptr_result_nullonfailure_ XGameSaveUpdateHandle* updateContext) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveCreateUpdate(IntPtr container,
            [MarshalAs(UnmanagedType.LPStr)] string containerDisplayName,
            out IntPtr updateContext);

        // STDAPI_(void) XGameSaveCloseUpdate(_In_ XGameSaveUpdateHandle context) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveCloseUpdate(IntPtr context);

        // STDAPI XGameSaveSubmitBlobWrite(_In_ XGameSaveUpdateHandle updateContext, _In_z_ const char* blobName, _In_reads_bytes_(byteCount) const uint8_t* data, _In_ size_t byteCount) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveSubmitBlobWrite(IntPtr updateContext, [MarshalAs(UnmanagedType.LPStr)] string blobName,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] data, UInt64 byteCount);

        // STDAPI XGameSaveSubmitBlobDelete(_In_ XGameSaveUpdateHandle updateContext, _In_z_ const char* blobName) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveSubmitBlobDelete(IntPtr updateContext, [MarshalAs(UnmanagedType.LPStr)] string blobName);

        // STDAPI XGameSaveSubmitUpdate(_In_ XGameSaveUpdateHandle updateContext) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveSubmitUpdate(IntPtr updateContext);

        // STDAPI XGameSaveSubmitUpdateAsync(_In_ XGameSaveUpdateHandle updateContext, _In_ XAsyncBlock* async) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveSubmitUpdateAsync(IntPtr updateContext, IntPtr async);

        // STDAPI XGameSaveSubmitUpdateResult(_In_ XAsyncBlock* async) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveSubmitUpdateResult(IntPtr async);
    }
}
