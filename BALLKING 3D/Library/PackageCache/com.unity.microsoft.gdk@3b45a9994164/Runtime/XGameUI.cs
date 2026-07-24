using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.CSharp;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    // enum class XGameUiMessageDialogButton : uint32_t
    // {
    //     First = 0,
    //     Second = 1,
    //     Third = 2
    // };
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XGameUiMessageDialogButton : UInt32
    {
        First = 0,
        Second = 1,
        Third = 2
    }

    // enum class XGameUiNotificationPositionHint : uint32_t
    // {
    //     BottomCenter = 0,
    //     BottomLeft = 1,
    //     BottomRight = 2,
    //     TopCenter = 3,
    //     TopLeft = 4,
    //     TopRight = 5
    // };
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XGameUiNotificationPositionHint : UInt32
    {
        BottomCenter = 0,
        BottomLeft = 1,
        BottomRight = 2,
        TopCenter = 3,
        TopLeft = 4,
        TopRight = 5
    }

    // enum class XGameUiTextEntryInputScope : uint32_t
    // {
    //     Default = 0,
    //     Url = 1,
    //     EmailSmtpAddress = 5,
    //     Number = 29,
    //     Password = 31,
    //     TelephoneNumber = 32,
    //     Alphanumeric = 40,
    //     Search = 50,
    //     ChatWithoutEmoji = 68,
    // };
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XGameUiTextEntryInputScope : UInt32
    {
        Default = 0,
        Url = 1,
        EmailSmtpAddress = 5,
        Number = 29,
        Password = 31,
        TelephoneNumber = 32,
        Alphanumeric = 40,
        Search = 50,
        ChatWithoutEmoji = 68,
    };

    // enum class XGameUiWebAuthenticationOptions : uint32_t
    // {
    //     None                     = 0x00,
    //     PreferFullscreen         = 0x01
    // };
    public enum XGameUiWebAuthenticationOptions : UInt32
    {
        None = 0x00,
        PreferFullscreen = 0x01
    };

    // enum class XGameUiTextEntryChangeTypeFlags : uint32_t
    // {
    //     None                = 0x0,
    //     TextChanged         = 0x1,
    //     Dismissed   = 0x2,
    // };
    public enum XGameUiTextEntryChangeTypeFlags : UInt32
    {
        None = 0x0,
        TextChanged = 0x1,
        Dismissed = 0x2,
    };

    // enum class XGameUiTextEntryVisibilityFlags : uint32_t
    // {
    //     Default = 0x0,
    //     OnlyShowCandidates = 0x1,
    // };
    public enum XGameUiTextEntryVisibilityFlags : UInt32
    {
        Default = 0x0,
        OnlyShowCandidates = 0x1,
    };

    // enum class XGameUiTextEntryPositionHint : uint32_t
    // {
    //     Bottom = 0,
    //     Top = 1,
    // };
    public enum XGameUiTextEntryPositionHint : UInt32
    {
        Bottom = 0,
        Top = 1,
    };

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XGameUiWebAuthenticationResultData
    {
        internal XGameUiWebAuthenticationResultData(Interop.XGameUiWebAuthenticationResultData interop)
        {
            this.data = interop;
        }

        public XGameUiWebAuthenticationResultData()
        {
            this.data = new Interop.XGameUiWebAuthenticationResultData();
        }

        internal Interop.XGameUiWebAuthenticationResultData data;

        public UInt32 ResponseStatus
        {
            get => this.data.responseStatus;
            set => this.data.responseStatus = value;
        }

        public UInt64 ResponseCompletionUriSize
        {
            get => this.data.responseCompletionUriSize;
            set => this.data.responseCompletionUriSize = value;
        }

        public string ResponseCompletionUri
        {
            get => this.data.responseCompletionUri;
            set => this.data.responseCompletionUri = value;
        }

        [Obsolete("Please use ResponseStatus instead, (UnityUpgradable) -> ResponseStatus", true)]
        public UInt32 responseStatus
        {
            get => this.data.responseStatus;
            set => this.data.responseStatus = value;
        }

        [Obsolete("Please use ResponseCompletionUriSize instead, (UnityUpgradable) -> ResponseCompletionUriSize", true)]
        public UInt64 responseCompletionUriSize
        {
            get => this.data.responseCompletionUriSize;
            set => this.data.responseCompletionUriSize = value;
        }

        [Obsolete("Please use ResponseCompletionUri instead, (UnityUpgradable) -> ResponseCompletionUri", true)]
        public string responseCompletionUri
        {
            get => this.data.responseCompletionUri;
            set => this.data.responseCompletionUri = value;
        }
    }

    public class XGameUiTextEntryOptions
    {
        internal XGameUiTextEntryOptions(Interop.XGameUiTextEntryOptions interop)
        {
            this.data = interop;
        }

        public XGameUiTextEntryOptions()
        {
            this.data = new Interop.XGameUiTextEntryOptions();
        }

        internal Interop.XGameUiTextEntryOptions data;

        public XGameUiTextEntryInputScope InputScope
        {
            get => this.data.inputScope;
            set => this.data.inputScope = value;
        }

        public XGameUiTextEntryPositionHint PositionHint
        {
            get => this.data.positionHint;
            set => this.data.positionHint = value;
        }

        public XGameUiTextEntryVisibilityFlags VisibilityFlags
        {
            get => this.data.visibilityFlags;
            set => this.data.visibilityFlags = value;
        }
    };


    public class XGameUiTextEntryExtents
    {
        internal XGameUiTextEntryExtents(Interop.XGameUiTextEntryExtents interop)
        {
            this.interop = interop;
        }

        public XGameUiTextEntryExtents()
        {
            this.interop = new Interop.XGameUiTextEntryExtents();
        }

        internal Interop.XGameUiTextEntryExtents interop;

        public float Left
        {
            get => this.interop.left;
            set => this.interop.left = value;
        }

        public float Top
        {
            get => this.interop.top;
            set => this.interop.top = value;
        }

        public float Right
        {
            get => this.interop.right;
            set => this.interop.right = value;
        }

        public float Bottom
        {
            get => this.interop.bottom;
            set => this.interop.bottom = value;
        }
    };

    public class XGameUiTextEntryHandle : EquatableHandle
    {
        public XGameUiTextEntryHandle(IntPtr handle) :
            base(IntPtr.Zero, true, handle)
        {
        }

        protected override bool ReleaseHandle()
        {
            NativeMethods.XGameUiTextEntryClose(this.Handle);
            SetHandle(IntPtr.Zero);
            return true;
        }

        public override bool IsInvalid
        {
            get { return this.Handle == IntPtr.Zero; }
        }
    }

    partial class SDK
    {
        public static Int32 XGameUiShowMessageDialogAsync(XAsyncBlock async, string titleText,
            string contextText, string firstButtonText,
            string secondButtonText, string thirdButtonText,
            XGameUiMessageDialogButton defaultButton, XGameUiMessageDialogButton cancelButton)
        {
            return NativeMethods.XGameUiShowMessageDialogAsync(async.InteropPtr, titleText, contextText, firstButtonText,
                secondButtonText, thirdButtonText, defaultButton, cancelButton);
        }

        public static Int32 XGameUiShowMessageDialogResult(XAsyncBlock async, out XGameUiMessageDialogButton resultButton)
        {
            return NativeMethods.XGameUiShowMessageDialogResult(async.InteropPtr, out resultButton);
        }

        public static Int32 XGameUiShowSendGameInviteAsync(XAsyncBlock async, XUserHandle requestingUser, string sessionConfigurationId,
            string sessionTemplateName, string sessionId,
            string invitationText, string customActivationContext)
        {
            IntPtr userHandle = (requestingUser != null) ? requestingUser.Handle : IntPtr.Zero;
            return NativeMethods.XGameUiShowSendGameInviteAsync(async.InteropPtr, userHandle, sessionConfigurationId,
                sessionTemplateName, sessionId, invitationText, customActivationContext);
        }

        public static Int32 XGameUiShowSendGameInviteResult(XAsyncBlock async)
        {
            return NativeMethods.XGameUiShowSendGameInviteResult(async.InteropPtr);
        }

        public static Int32 XGameUiShowMultiplayerActivityGameInviteAsync(XAsyncBlock async, XUserHandle requestingUser)
        {
            IntPtr userHandle = (requestingUser != null) ? requestingUser.Handle : IntPtr.Zero;

            return NativeMethods.XGameUiShowMultiplayerActivityGameInviteAsync(async.InteropPtr, userHandle);
        }

        public static Int32 XGameUiShowMultiplayerActivityGameInviteResult(XAsyncBlock async)
        {
            return NativeMethods.XGameUiShowMultiplayerActivityGameInviteResult(async.InteropPtr);
        }

        public static Int32 XGameUiShowPlayerProfileCardAsync(XAsyncBlock async, XUserHandle requestingUser, UInt64 targetPlayer)
        {
            IntPtr userHandle = (requestingUser != null) ? requestingUser.Handle : IntPtr.Zero;

            return NativeMethods.XGameUiShowPlayerProfileCardAsync(async.InteropPtr, userHandle, targetPlayer);
        }

        public static Int32 XGameUiShowPlayerProfileCardResult(XAsyncBlock async)
        {
            return NativeMethods.XGameUiShowPlayerProfileCardResult(async.InteropPtr);
        }

        public static Int32 XGameUiShowAchievementsAsync(XAsyncBlock async, XUserHandle requestingUser, UInt32 titleId)
        {
            IntPtr userHandle = (requestingUser != null) ? requestingUser.Handle : IntPtr.Zero;

            return NativeMethods.XGameUiShowAchievementsAsync(async.InteropPtr, userHandle, titleId);
        }

        public static Int32 XGameUiShowAchievementsResult(XAsyncBlock async)
        {
            return NativeMethods.XGameUiShowAchievementsResult(async.InteropPtr);
        }

        public static Int32 XGameUiShowPlayerPickerAsync(XAsyncBlock async,
            XUserHandle requestingUser,
            string promptText,
            UInt64[] selectFromPlayers,
            UInt64[] preSelectedPlayers,
            UInt32 minSelectionCount,
            UInt32 maxSelectionCount)
        {
            IntPtr userHandle = (requestingUser != null) ? requestingUser.Handle : IntPtr.Zero;

            UInt32 preSelectedPlayersLength = preSelectedPlayers != null ? (UInt32)preSelectedPlayers.Length : 0;

            return NativeMethods.XGameUiShowPlayerPickerAsync(async.InteropPtr,
                userHandle,
                promptText,
                (UInt32)selectFromPlayers.Length,
                selectFromPlayers,
                preSelectedPlayersLength,
                preSelectedPlayers,
                minSelectionCount,
                maxSelectionCount);
        }

        public static Int32 XGameUiShowWebAuthenticationResult(XAsyncBlock async,
            byte[] buffer,
            out XGameUiWebAuthenticationResultData result)
        {
            IntPtr ptrToBuffer;
            UInt64 bufferUsed;
            result = default(XGameUiWebAuthenticationResultData);
            int hr = NativeMethods.XGameUiShowWebAuthenticationResult(async.InteropPtr,
                (ulong)buffer.Length,
                buffer,
                out ptrToBuffer,
                out bufferUsed);
            if (HR.SUCCEEDED(hr))
            {
                result = new XGameUiWebAuthenticationResultData(
                    (Interop.XGameUiWebAuthenticationResultData)Marshal.PtrToStructure(ptrToBuffer, typeof(Interop.XGameUiWebAuthenticationResultData)));
            }

            return hr;
        }

        public static Int32 XGameUiShowPlayerPickerResultCount(XAsyncBlock async, out UInt32 resultPlayersCount)
        {
            return NativeMethods.XGameUiShowPlayerPickerResultCount(async.InteropPtr, out resultPlayersCount);
        }

        public static Int32 XGameUiShowPlayerPickerResult(XAsyncBlock async, UInt64[] resultPlayers, out UInt32 resultPlayerUsed)
        {
            return NativeMethods.XGameUiShowPlayerPickerResult(async.InteropPtr, (UInt32)resultPlayers.Length, resultPlayers, out resultPlayerUsed);
        }

        public static Int32 XGameUiShowErrorDialogAsync(XAsyncBlock async, Int32 errorCode, string context)
        {
            return NativeMethods.XGameUiShowErrorDialogAsync(async.InteropPtr, errorCode, context);
        }

        public static Int32 XGameUiShowErrorDialogResult(XAsyncBlock async)
        {
            return NativeMethods.XGameUiShowErrorDialogResult(async.InteropPtr);
        }

        public static Int32 XGameUiSetNotificationPositionHint(XGameUiNotificationPositionHint position)
        {
            return NativeMethods.XGameUiSetNotificationPositionHint(position);
        }

        public static Int32 XGameUiShowTextEntryAsync(XAsyncBlock async, string titleText,
            string descriptionText, string defaultText,
            XGameUiTextEntryInputScope inputScope, UInt32 maxTextLength)
        {
            return NativeMethods.XGameUiShowTextEntryAsync(async.InteropPtr, titleText, descriptionText, defaultText, inputScope, maxTextLength);
        }

        public static Int32 XGameUiShowTextEntryResultSize(XAsyncBlock async, out UInt32 resultTextBufferSize)
        {
            return NativeMethods.XGameUiShowTextEntryResultSize(async.InteropPtr, out resultTextBufferSize);
        }

        public static Int32 XGameUiShowTextEntryResult(XAsyncBlock async, UInt32 resultTextBufferSize,
            out string resultTextBuffer)
        {
            resultTextBuffer = null;

            UInt32 bufferUsed;
            StringBuilder bufferSb = new StringBuilder((int)resultTextBufferSize);
            int hr = NativeMethods.XGameUiShowTextEntryResult(async.InteropPtr, resultTextBufferSize, bufferSb, out bufferUsed);

            if (HR.SUCCEEDED(hr))
            {
                resultTextBuffer = bufferSb.ToString();
            }

            return hr;
        }

        public static Int32 XGameUiShowWebAuthenticationAsync(XAsyncBlock async, XUserHandle requestingUser,
            string requestUri, string completeUri)
        {
            IntPtr userHandle = (requestingUser != null) ? requestingUser.Handle : IntPtr.Zero;

            return NativeMethods.XGameUiShowWebAuthenticationAsync(async.InteropPtr, userHandle, requestUri, completeUri);
        }

        public static Int32 XGameUiShowWebAuthenticationWithOptionsAsync(XAsyncBlock async, XUserHandle requestingUser,
            string requestUri, string completionUri, XGameUiWebAuthenticationOptions options)
        {
            IntPtr userHandle = (requestingUser != null) ? requestingUser.Handle : IntPtr.Zero;

            return NativeMethods.XGameUiShowWebAuthenticationWithOptionsAsync(async.InteropPtr, userHandle, requestUri, completionUri, options);
        }

        public static Int32 XGameUiShowWebAuthenticationResultSize(XAsyncBlock async, out UInt64 bufferSize)
        {
            return NativeMethods.XGameUiShowWebAuthenticationResultSize(async.InteropPtr, out bufferSize);
        }

        public static void XGameUiTextEntryClose(XGameUiTextEntryHandle handle)
        {
            handle.Close();
        }

        public static Int32 XGameUiTextEntryGetExtents(XGameUiTextEntryHandle handle, out XGameUiTextEntryExtents extents)
        {
            extents = default(XGameUiTextEntryExtents);
            Interop.XGameUiTextEntryExtents interopExtents = default(Interop.XGameUiTextEntryExtents);

            Int32 hr = NativeMethods.XGameUiTextEntryGetExtents(handle.Handle, out interopExtents);

            if (HR.SUCCEEDED(hr))
            {
                extents = new XGameUiTextEntryExtents(interopExtents);
            }

            return hr;
        }

        public static Int32 XGameUiTextEntryOpen(XGameUiTextEntryOptions options, UInt32 maxLength, string initialText,
            UInt32 cursorIndex, out XGameUiTextEntryHandle handle)
        {
            handle = null;

            IntPtr handlePtr;
            int hr = NativeMethods.XGameUiTextEntryOpen(options.data, maxLength, initialText, cursorIndex, out handlePtr);

            if (Interop.HR.SUCCEEDED(hr))
            {
                handle = new XGameUiTextEntryHandle(handlePtr);
            }

            return hr;
        }

        public static Int32 XGameUiTextEntryGetState(XGameUiTextEntryHandle handle, out XGameUiTextEntryChangeTypeFlags changeType,
            out UInt32 cursorIndex, out UInt32 imeClauseStartIndex, out UInt32 imeClauseEndIndex, UInt32 bufferSize, out string buffer)
        {
            buffer = null;

            StringBuilder bufferSb = new StringBuilder((int)bufferSize);
            int hr = NativeMethods.XGameUiTextEntryGetState(handle.Handle, out changeType, out cursorIndex,
                out imeClauseStartIndex, out imeClauseEndIndex, bufferSize, bufferSb);

            if (HR.SUCCEEDED(hr))
            {
                buffer = bufferSb.ToString();
            }

            return hr;
        }

        public static Int32 XGameUiTextEntryUpdatePositionHint(XGameUiTextEntryHandle handle, XGameUiTextEntryPositionHint positionHint)
        {
            return NativeMethods.XGameUiTextEntryUpdatePositionHint(handle.Handle, positionHint);
        }

        public static Int32 XGameUiTextEntryUpdateVisibility(XGameUiTextEntryHandle handle, XGameUiTextEntryVisibilityFlags visibilityFlags)
        {
            return NativeMethods.XGameUiTextEntryUpdateVisibility(handle.Handle, visibilityFlags);
        }
    }
}
