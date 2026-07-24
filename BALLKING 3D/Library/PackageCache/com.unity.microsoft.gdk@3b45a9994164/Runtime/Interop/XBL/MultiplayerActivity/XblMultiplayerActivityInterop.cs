using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    internal partial class XblInterop
    {
        //STDAPI XblMultiplayerActivityUpdateRecentPlayers(
        //    _In_ XblContextHandle xblContext,
        //    _In_reads_(updatesCount) const XblMultiplayerActivityRecentPlayerUpdate* updates,
        //    _In_ size_t updatesCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerActivityUpdateRecentPlayers(
            IntPtr xboxLiveContext,
            [Optional] XblMultiplayerActivityRecentPlayerUpdate[] updates,
            SizeT updatesCount
            );

        //STDAPI XblMultiplayerActivityFlushRecentPlayersAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerActivityFlushRecentPlayersAsync(
            IntPtr xboxLiveContext,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerActivitySetActivityAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_ const XblMultiplayerActivityInfo* activityInfo,
        //    _In_ bool allowCrossPlatformJoin,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerActivitySetActivityAsync(
            IntPtr xboxLiveContext,
            [In] ref XblMultiplayerActivityInfo activityInfo,
            [MarshalAs(UnmanagedType.U1)] bool allowCrossPlatformJoin,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerActivityGetActivityAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_reads_(xuidsCount) const uint64_t* xuids,
        //    _In_ size_t xuidsCount,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerActivityGetActivityAsync(
            IntPtr xboxLiveContext,
            [In] UInt64[] xboxUserIdList,
            SizeT xboxUserIdListCount,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerActivityGetActivityResultSize(
        //    _In_ XAsyncBlock* async,
        //    _Out_ size_t* resultSizeInBytes
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerActivityGetActivityResultSize(
            XAsyncBlockPtr async,
            out SizeT resultSizeInBytes
        );

        //STDAPI XblMultiplayerActivityGetActivityResult(
        //    _In_ XAsyncBlock* async,
        //    _In_ size_t bufferSize,
        //    _Out_writes_bytes_to_(bufferSize, * bufferUsed) void* buffer,
        //    _Outptr_ XblMultiplayerActivityInfo** ptrToBufferResults,
        //    _Out_ size_t* resultCount,
        //    _Out_opt_ size_t* bufferUsed
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerActivityGetActivityResult(
            XAsyncBlockPtr async,
            SizeT bufferSize,
            IntPtr buffer,
            out IntPtr ptrToBufferResults,
            out SizeT resultCount,
            out SizeT bufferUsed
        );

        //STDAPI XblMultiplayerActivityDeleteActivityAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerActivityDeleteActivityAsync(
            IntPtr xboxLiveContext,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerActivitySendInvitesAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_ const uint64_t* xuids,
        //    _In_ size_t xuidsCount,
        //    _In_ bool allowCrossPlatformJoin,
        //    _In_opt_z_ const char* connectionString,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerActivitySendInvitesAsync(
            IntPtr xblContext,
            [In] UInt64[] xboxUserIdList,
            SizeT xboxUserIdListCount,
            [MarshalAs(UnmanagedType.U1)] bool allowCrossPlatformJoin,
            Byte[] connectionString,
            XAsyncBlockPtr async
        );
    }
}