// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Unity.XGamingRuntime.Interop
{
    internal struct XPersistentLocalStorageSpaceInfo
    {
        // Bytes that are available to be written to PLS.
        internal UInt64 availableFreeBytes;

        // Bytes left in PLS allocation. May require prompting user to free up
        // space to make these bytes available.
        internal UInt64 totalFreeBytes;

        // Bytes already used in PLS.
        internal UInt64 usedBytes;

        // Maximum total bytes that can be stored in PLS.
        internal UInt64 totalBytes;
    }

    partial class NativeMethods
    {
        //STDAPI XPersistentLocalStorageGetPathSize(
        //    _Out_ size_t* pathSize
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPersistentLocalStorageGetPathSize(out UInt64 pathSize);

        //STDAPI XPersistentLocalStorageGetPath(
        //    _In_ size_t pathSize,
        //    _Out_writes_to_(pathSize, *pathUsed) char* path,
        //    _Out_opt_ size_t* pathUsed
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPersistentLocalStorageGetPath(UInt64 pathSize,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] byte[] path,
            out UInt64 pathUsed);

        //STDAPI XPersistentLocalStorageGetSpaceInfo(
        //    _Out_ XPersistentLocalStorageSpaceInfo* spaceInfo
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPersistentLocalStorageGetSpaceInfo(out XPersistentLocalStorageSpaceInfo spaceInfo);

        //STDAPI XPersistentLocalStoragePromptUserForSpaceAsync(
        //    _In_ uint64_t requestedBytes,
        //    _Inout_ XAsyncBlock* asyncBlock
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPersistentLocalStoragePromptUserForSpaceAsync(UInt64 requestedBytes,
            IntPtr asyncBlock);

        //STDAPI XPersistentLocalStoragePromptUserForSpaceResult(
        //    _Inout_ XAsyncBlock* asyncBlock
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XPersistentLocalStoragePromptUserForSpaceResult(IntPtr asyncBlock);
    }
}
