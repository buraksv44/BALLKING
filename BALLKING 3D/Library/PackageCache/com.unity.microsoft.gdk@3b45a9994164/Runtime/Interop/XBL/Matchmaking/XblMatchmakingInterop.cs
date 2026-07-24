using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    internal partial class XblInterop
    {
        //STDAPI XblMatchmakingCreateMatchTicketAsync(
        //    _In_ XblContextHandle xboxLiveContext,
        //    _In_ XblMultiplayerSessionReference ticketSessionReference,
        //    _In_ const char* matchmakingServiceConfigurationId,
        //    _In_ const char* hopperName,
        //    _In_ const uint64_t ticketTimeout,
        //    _In_ XblPreserveSessionMode preserveSession,
        //    _In_ const char* ticketAttributesJson,
        //    _In_ XAsyncBlock* asyncBlock
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMatchmakingCreateMatchTicketAsync(
            IntPtr xboxLiveContext,
            XblMultiplayerSessionReference ticketSessionReference,
            Byte[] matchmakingServiceConfigurationId,
            Byte[] hopperName,
            UInt64 ticketTimeout,
            XblPreserveSessionMode preserveSession,
            Byte[] ticketAttributesJson,
            XAsyncBlockPtr asyncBlock
            );

        //STDAPI XblMatchmakingCreateMatchTicketResult(
        //    _In_ XAsyncBlock* asyncBlock,
        //    _Out_ XblCreateMatchTicketResponse* resultPtr
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMatchmakingCreateMatchTicketResult(
            XAsyncBlockPtr async,
            out XblCreateMatchTicketResponse resultPtr
            );

        //STDAPI XblMatchmakingDeleteMatchTicketAsync(
        //    _In_ XblContextHandle xboxLiveContext,
        //    _In_ const char* serviceConfigurationId,
        //    _In_ const char* hopperName,
        //    _In_ const char* ticketId,
        //    _In_ XAsyncBlock* asyncBlock
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMatchmakingDeleteMatchTicketAsync(
            IntPtr xboxLiveContext,
            Byte[] serviceConfigurationId,
            Byte[] hopperName,
            Byte[] ticketId,
            XAsyncBlockPtr asyncBlock
            );

        //STDAPI XblMatchmakingGetMatchTicketDetailsAsync(
        //    _In_ XblContextHandle xboxLiveContext,
        //    _In_ const char* serviceConfigurationId,
        //    _In_ const char* hopperName,
        //    _In_ const char* ticketId,
        //    _In_ XAsyncBlock* asyncBlock
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMatchmakingGetMatchTicketDetailsAsync(
            IntPtr xboxLiveContext,
            Byte[] serviceConfigurationId,
            Byte[] hopperName,
            Byte[] ticketId,
            XAsyncBlockPtr asyncBlock
            );

        //STDAPI XblMatchmakingGetMatchTicketDetailsResultSize(
        //    _In_ XAsyncBlock* asyncBlock,
        //    _Out_ size_t* resultSizeInBytes
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMatchmakingGetMatchTicketDetailsResultSize(
            XAsyncBlockPtr asyncBlock,
            out SizeT resultSizeInBytes
            );

        //STDAPI XblMatchmakingGetMatchTicketDetailsResult(
        //    _In_ XAsyncBlock* asyncBlock,
        //    _In_ size_t bufferSize,
        //    _Out_writes_bytes_to_(bufferSize, * bufferUsed) void* buffer,
        //    _Outptr_ XblMatchTicketDetailsResponse** ptrToBuffer,
        //    _Out_opt_ size_t* bufferUsed
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMatchmakingGetMatchTicketDetailsResult(
            XAsyncBlockPtr asyncBlock,
            SizeT bufferSize,
            IntPtr buffer,
            out IntPtr ptrToBuffer,
            out SizeT bufferUsed
            );

        //STDAPI XblMatchmakingGetHopperStatisticsAsync(
        //    _In_ XblContextHandle xboxLiveContext,
        //    _In_ const char* serviceConfigurationId,
        //    _In_ const char* hopperName,
        //    _In_ XAsyncBlock* asyncBlock
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMatchmakingGetHopperStatisticsAsync(
            IntPtr xboxLiveContext,
            Byte[] serviceConfigurationId,
            Byte[] hopperName,
            XAsyncBlockPtr asyncBlock
            );

        //STDAPI XblMatchmakingGetHopperStatisticsResultSize(
        //    _In_ XAsyncBlock* asyncBlock,
        //    _Out_ size_t* resultSizeInBytes
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMatchmakingGetHopperStatisticsResultSize(
            XAsyncBlockPtr asyncBlock,
            out SizeT resultSizeInBytes
            );

        //STDAPI XblMatchmakingGetHopperStatisticsResult(
        //    _In_ XAsyncBlock* asyncBlock,
        //    _In_ size_t bufferSize,
        //    _Out_writes_bytes_to_(bufferSize, * bufferUsed) void* buffer,
        //    _Outptr_ XblHopperStatisticsResponse** ptrToBuffer,
        //    _Out_opt_ size_t* bufferUsed
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMatchmakingGetHopperStatisticsResult(
            XAsyncBlockPtr asyncBlock,
            SizeT bufferSize,
            IntPtr buffer,
            out IntPtr ptrToBuffer,
            out SizeT bufferUsed
            );
    }
}
