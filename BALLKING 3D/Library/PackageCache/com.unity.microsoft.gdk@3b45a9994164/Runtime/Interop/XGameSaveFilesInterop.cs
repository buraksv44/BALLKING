using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Unity.XGamingRuntime.Interop
{
    partial class NativeMethods
    {
        //STDAPI XGameSaveFilesGetFolderWithUiAsync(_In_ XUserHandle requestingUser,
        //  _In_z_ const char* configurationId,
        //  _In_ XAsyncBlock* async) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveFilesGetFolderWithUiAsync(IntPtr requestingUser,
            [MarshalAs(UnmanagedType.LPStr)] string configurationId,
            IntPtr async);

        // STDAPI XGameSaveFilesGetFolderWithUiResult(_In_ XAsyncBlock* async,
        //     _In_ size_t folderSize,
        //     _Out_writes_bytes_(folderSize) char* folderResult) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveFilesGetFolderWithUiResult(IntPtr async,
            UInt64 folderSize,
            StringBuilder folderResult);

        // STDAPI XGameSaveFilesGetRemainingQuota(_In_ XUserHandle userContext,
        //     _In_z_ const char* configurationId,
        //     _Out_ int64_t* remainingQuota) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameSaveFilesGetRemainingQuota(IntPtr userContext,
            [MarshalAs(UnmanagedType.LPStr)] string configurationId,
            out UInt64 remainingQuota);
    }
}
