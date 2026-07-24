using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Unity.XGamingRuntime.Interop
{
    // struct XGameUiWebAuthenticationResultData
    // {
    //     HRESULT responseStatus;
    //     size_t responseCompletionUriSize;
    //     _Field_size_opt_(responseCompletionUriSize) _Null_terminated_ const char* responseCompletionUri;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XGameUiWebAuthenticationResultData
    {
        internal UInt32 responseStatus;
        internal UInt64 responseCompletionUriSize;
        [MarshalAs(UnmanagedType.LPStr)] internal string responseCompletionUri;
    }

    // struct XGameUiTextEntryOptions
    // {
    //     XGameUiTextEntryInputScope inputScope;
    //     XGameUiTextEntryPositionHint positionHint;
    //     XGameUiTextEntryVisibilityFlags visibilityFlags;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XGameUiTextEntryOptions
    {
        internal XGameUiTextEntryInputScope inputScope;
        internal XGameUiTextEntryPositionHint positionHint;
        internal XGameUiTextEntryVisibilityFlags visibilityFlags;
    };

    // struct XGameUiTextEntryExtents
    // {
    //     float left;
    //     float top;
    //     float right;
    //     float bottom;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XGameUiTextEntryExtents
    {
        internal float left;
        internal float top;
        internal float right;
        internal float bottom;
    };

    partial class NativeMethods
    {
        // STDAPI XGameUiShowMessageDialogAsync(
        //     _In_ XAsyncBlock* async,
        //     _In_z_ const char* titleText,
        //     _In_z_ const char* contentText,
        //     _In_opt_z_ const char* firstButtonText,
        //     _In_opt_z_ const char* secondButtonText,
        //     _In_opt_z_ const char* thirdButtonText,
        //     _In_ XGameUiMessageDialogButton defaultButton,
        //     _In_ XGameUiMessageDialogButton cancelButton
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowMessageDialogAsync(IntPtr async,
            [MarshalAs(UnmanagedType.LPStr)] string titleText,
            [MarshalAs(UnmanagedType.LPStr)] string contextText,
            [MarshalAs(UnmanagedType.LPStr)] string firstButtonText,
            [MarshalAs(UnmanagedType.LPStr)] string secondButtonText,
            [MarshalAs(UnmanagedType.LPStr)] string thirdButtonText,
            XGameUiMessageDialogButton defaultButton,
            XGameUiMessageDialogButton cancelButton);

        // STDAPI XGameUiShowMessageDialogResult(
        //     _In_ XAsyncBlock* async,
        //     _Out_ XGameUiMessageDialogButton* resultButton
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowMessageDialogResult(IntPtr async,
            out XGameUiMessageDialogButton resultButton);

        // STDAPI XGameUiShowSendGameInviteAsync(
        //     _In_ XAsyncBlock* async,
        //     _In_ XUserHandle requestingUser,
        //     _In_z_ const char* sessionConfigurationId,
        //     _In_z_ const char* sessionTemplateName,
        //     _In_z_ const char* sessionId,
        //     _In_opt_z_ const char* invitationText,
        //     _In_opt_z_ const char* customActivationContext
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowSendGameInviteAsync(IntPtr async, IntPtr requestingUser,
            [MarshalAs(UnmanagedType.LPStr)] string sessionConfigurationId,
            [MarshalAs(UnmanagedType.LPStr)] string sessionTemplateName,
            [MarshalAs(UnmanagedType.LPStr)] string sessionId,
            [MarshalAs(UnmanagedType.LPStr)] string invitationText,
            [MarshalAs(UnmanagedType.LPStr)] string customActivationContext);


        // STDAPI XGameUiShowSendGameInviteResult(
        //     _In_ XAsyncBlock* async
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowSendGameInviteResult(IntPtr async);

        // STDAPI XGameUiShowMultiplayerActivityGameInviteAsync(
        //     _In_ XAsyncBlock* async,
        //     _In_ XUserHandle requestingUser
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowMultiplayerActivityGameInviteAsync(IntPtr async, IntPtr requestingUser);

        // STDAPI XGameUiShowMultiplayerActivityGameInviteResult(
        //     _In_ XAsyncBlock* async
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowMultiplayerActivityGameInviteResult(IntPtr async);

        // STDAPI XGameUiShowPlayerProfileCardAsync(
        //     _In_ XAsyncBlock* async,
        //     _In_ XUserHandle requestingUser,
        //     _In_ uint64_t targetPlayer
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowPlayerProfileCardAsync(IntPtr async,
            IntPtr requestingUser,
            UInt64 targetPlayer);

        // STDAPI XGameUiShowPlayerProfileCardResult(
        //     _In_ XAsyncBlock* async
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowPlayerProfileCardResult(IntPtr async);

        // STDAPI XGameUiShowAchievementsAsync(
        //     _In_ XAsyncBlock* async,
        //     _In_ XUserHandle requestingUser,
        //     _In_ uint32_t titleId
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowAchievementsAsync(IntPtr async,
            IntPtr requestingUser,
            UInt32 titleId);

        // STDAPI XGameUiShowAchievementsResult(
        //     _In_ XAsyncBlock * async
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowAchievementsResult(IntPtr async);

        // STDAPI XGameUiShowPlayerPickerAsync(
        //     _In_ XAsyncBlock* async,
        //     _In_ XUserHandle requestingUser,
        //     _In_z_ const char* promptText,
        //     _In_ uint32_t selectFromPlayersCount,
        //     _In_reads_(selectFromPlayersCount) const uint64_t* selectFromPlayers,
        //     _In_ uint32_t preSelectedPlayersCount,
        //     _In_reads_opt_(preSelectedPlayersCount) const uint64_t* preSelectedPlayers,
        //     _In_ uint32_t minSelectionCount,
        //     _In_ uint32_t maxSelectionCount
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowPlayerPickerAsync(IntPtr async,
            IntPtr requestingUser,
            [MarshalAs(UnmanagedType.LPStr)] string promptText,
            UInt32 selectFromPlayersCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] UInt64[] selectFromPlayers,
            UInt32 preSelectedPlayersCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] UInt64[] preSelectedPlayers,
            UInt32 minSelectionCount,
            UInt32 maxSelectionCount);

        // STDAPI XGameUiShowPlayerPickerResultCount(
        //     _In_ XAsyncBlock* async,
        //     _Out_ uint32_t* resultPlayersCount
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowPlayerPickerResultCount(IntPtr async, out UInt32 resultPlayersCount);

        // STDAPI XGameUiShowPlayerPickerResult(
        //     _In_ XAsyncBlock* async,
        //     _In_ uint32_t resultPlayersCount,
        //     _Out_writes_to_(resultPlayersCount, *resultPlayersUsed) uint64_t* resultPlayers,
        //     _Out_opt_ uint32_t* resultPlayersUsed
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowPlayerPickerResult(IntPtr async,
            UInt32 resultPlayersCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] UInt64[] resultPlayers,
            out UInt32 resultPlayerUsed);

        // STDAPI XGameUiShowErrorDialogAsync(
        //     _In_ XAsyncBlock* async,
        //     _In_ HRESULT errorCode,
        //     _In_opt_z_ const char* context
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowErrorDialogAsync(IntPtr async,
            Int32 errorCode,
            [MarshalAs(UnmanagedType.LPStr)] string context);

        // STDAPI XGameUiShowErrorDialogResult(
        //     _In_ XAsyncBlock* async
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowErrorDialogResult(IntPtr async);

        // STDAPI XGameUiSetNotificationPositionHint(
        //     _In_ XGameUiNotificationPositionHint position
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiSetNotificationPositionHint(XGameUiNotificationPositionHint position);

        // STDAPI XGameUiShowTextEntryAsync(
        //     _In_ XAsyncBlock* async,
        //     _In_opt_z_ const char* titleText,
        //     _In_opt_z_ const char* descriptionText,
        //     _In_opt_z_ const char* defaultText,
        //     _In_ XGameUiTextEntryInputScope inputScope,
        //     _In_ uint32_t maxTextLength
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowTextEntryAsync(IntPtr async,
            [MarshalAs(UnmanagedType.LPStr)] string titleText,
            [MarshalAs(UnmanagedType.LPStr)] string descriptionText,
            [MarshalAs(UnmanagedType.LPStr)] string defaultText,
            XGameUiTextEntryInputScope inputScope,
            UInt32 maxTextLength);

        // STDAPI XGameUiShowTextEntryResultSize(
        //     _In_ XAsyncBlock* async,
        //     _Out_ uint32_t* resultTextBufferSize
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowTextEntryResultSize(IntPtr async,
            out UInt32 resultTextBufferSize);

        // STDAPI XGameUiShowTextEntryResult(
        //     _In_ XAsyncBlock* async,
        //     _In_ uint32_t resultTextBufferSize,
        //     _Out_writes_to_(resultTextBufferSize, *resultTextBufferUsed) char* resultTextBuffer,
        //     _Out_opt_ uint32_t* resultTextBufferUsed
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowTextEntryResult(IntPtr async,
            UInt32 resultTextBufferSize,
            StringBuilder resultTextBuffer,
            out UInt32 resultTextBufferUsed);

        // STDAPI XGameUiShowWebAuthenticationAsync(
        //     _In_ XAsyncBlock* async,
        //     _In_ XUserHandle requestingUser,
        //     _In_z_ const char* requestUri,
        //     _In_z_ const char* completionUri
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowWebAuthenticationAsync(IntPtr async,
            IntPtr requestingUser,
            [MarshalAs(UnmanagedType.LPStr)] string requestUri,
            [MarshalAs(UnmanagedType.LPStr)] string completionUri);

        // STDAPI XGameUiShowWebAuthenticationWithOptionsAsync(
        //     _In_ XAsyncBlock* async,
        //     _In_ XUserHandle requestingUser,
        //     _In_z_ const char* requestUri,
        //     _In_z_ const char* completionUri,
        //     _In_ XGameUiWebAuthenticationOptions options
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowWebAuthenticationWithOptionsAsync(IntPtr async,
            IntPtr requestingUser,
            [MarshalAs(UnmanagedType.LPStr)] string requestUri,
            [MarshalAs(UnmanagedType.LPStr)] string completionUri,
            XGameUiWebAuthenticationOptions options);

        // STDAPI XGameUiShowWebAuthenticationResult(
        //     _Inout_ XAsyncBlock* asyncblock,
        //     _In_ size_t bufferSize,
        //     _Out_writes_bytes_to_(bufferSize, *bufferUsed) void* buffer,
        //     _Outptr_ XGameUiWebAuthenticationResultData** ptrToBuffer,
        //     _Out_opt_ size_t* bufferUsed) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowWebAuthenticationResult(IntPtr asyncblock,
            UInt64 bufferSize,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] buffer,
            out IntPtr ptrToBuffer,
            out UInt64 bufferUsed);

        // STDAPI XGameUiShowWebAuthenticationResultSize(
        //     _Inout_ XAsyncBlock* async,
        //     _Out_ size_t* bufferSize
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiShowWebAuthenticationResultSize(IntPtr async, out UInt64 bufferSize);

        // STDAPI XGameUiTextEntryOpen(
        //     _In_ const XGameUiTextEntryOptions* options,
        //     _In_ uint32_t maxLength,
        //     _In_opt_z_ const char* initialText,
        //     _In_ uint32_t cursorIndex,
        //     _Out_ XGameUiTextEntryHandle* handle
        // ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiTextEntryOpen(XGameUiTextEntryOptions options,
            UInt32 maxLength, [MarshalAs(UnmanagedType.LPStr)] string initialText,
            UInt32 cursorIndex,
            out IntPtr handle);

        // STDAPI_(void) XGameUiTextEntryClose(
        //     _In_ XGameUiTextEntryHandle handle
        // ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XGameUiTextEntryClose(IntPtr handle);

        // STDAPI XGameUiTextEntryGetExtents(
        //  _In_ XGameUiTextEntryHandle handle,
        //  _Out_ XGameUiTextEntryExtents* extents) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiTextEntryGetExtents(IntPtr handle, out XGameUiTextEntryExtents extents);

        // STDAPI XGameUiTextEntryGetState(
        //     _In_ XGameUiTextEntryHandle handle,
        //     _Out_ XGameUiTextEntryChangeTypeFlags* changeType,
        //     _Out_opt_ uint32_t* cursorIndex,
        //     _Out_opt_ uint32_t* imeClauseStartIndex,
        //     _Out_opt_ uint32_t* imeClauseEndIndex,
        //     _In_ uint32_t bufferSize,
        //     _Out_writes_z_(bufferSize) char* buffer
        // ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiTextEntryGetState(IntPtr handle,
            out XGameUiTextEntryChangeTypeFlags changeType,
            out UInt32 cursorIndex,
            out UInt32 imeClauseStartIndex,
            out UInt32 imeClauseEndIndex,
            UInt32 bufferSize,
            StringBuilder buffer);


        // STDAPI XGameUiTextEntryUpdatePositionHint(
        //     _In_ XGameUiTextEntryHandle handle,
        //     _In_ XGameUiTextEntryPositionHint positionHint
        // ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiTextEntryUpdatePositionHint(IntPtr handle, XGameUiTextEntryPositionHint positionHint);

        // STDAPI XGameUiTextEntryUpdateVisibility(
        //     _In_ XGameUiTextEntryHandle handle,
        //     _In_ XGameUiTextEntryVisibilityFlags visibilityFlags
        // ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameUiTextEntryUpdateVisibility(IntPtr handle, XGameUiTextEntryVisibilityFlags visibilityFlags);
    }
}
