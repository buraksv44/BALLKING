// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;
using System.Text;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XUserAddOptions: uint32_t
    //{
    //    None                     = 0x00,
    //    AddDefaultUserSilently   = 0x01,
    //    AllowGuests              = 0x02,
    //    AddDefaultUserAllowingUI = 0x04,
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XUserAddOptions : UInt32
    {
        None = 0x00,
        AddDefaultUserSilently = 0x01,
        AllowGuests = 0x02,
        AddDefaultUserAllowingUI = 0x04,
    }

    //enum class XUserAgeGroup: uint32_t
    //{
    //    Unknown = 0,
    //    Child   = 1,
    //    Teen    = 2,
    //    Adult   = 3,
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XUserAgeGroup : UInt32
    {
        Unknown = 0,
        Child = 1,
        Teen = 2,
        Adult = 3,
    }

    //enum class XUserChangeEvent: uint32_t
    //{
    //    SignedInAgain   = 0,
    //    SigningOut      = 1,
    //    SignedOut       = 2,
    //    Gamertag        = 3,
    //    GamerPicture    = 4,
    //    Privileges      = 5,
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XUserChangeEvent : UInt32
    {
        SignedInAgain = 0,
        SigningOut = 1,
        SignedOut = 2,
        Gamertag = 3,
        GamerPicture = 4,
        Privileges = 5,
    }

    //enum class XUserDefaultAudioEndpointKind : uint32_t
    //{
    //    CommunicationRender = 0,
    //    CommunicationCapture = 1
    //};
    public enum XUserDefaultAudioEndpointKind : UInt32
    {
        CommunicationRender = 0,
        CommunicationCapture = 1
    }

    //enum class XUserGamerPictureSize: uint32_t
    //{
    //    Small           = 0, // 64x64
    //    Medium          = 1, // 208x208
    //    Large           = 2, // 424x424
    //    ExtraLarge      = 3, // 1080x1080
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XUserGamerPictureSize : UInt32
    {
        Small = 0, // 64x64
        Medium = 1, // 208x208
        Large = 2, // 424x424
        ExtraLarge = 3, // 1080x1080
    };

    //enum class XUserGamertagComponent: uint32_t
    //{
    //    Classic         = 0, // Clasic Gamertag
    //    Modern          = 1, // Modern Gamertag without Suffix
    //    ModernSuffix    = 2, // Modern Gamertag Suffix if present (otherwise empty)
    //    UniqueModern    = 3, // Combined Modern Gamertag with Suffix
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XUserGamertagComponent : UInt32
    {
        Classic = 0, // Clasic Gamertag
        Modern = 1, // Modern Gamertag without Suffix
        ModernSuffix = 2, // Modern Gamertag Suffix if present (otherwise empty)
        UniqueModern = 3, // Combined Modern Gamertag with Suffix
    };

    //enum class XUserGetTokenAndSignatureOptions: uint32_t
    //{
    //    None            = 0x00,
    //    ForceRefresh    = 0x01,
    //    AllUsers        = 0x02,
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XUserGetTokenAndSignatureOptions : UInt32
    {
        None = 0x00,
        ForceRefresh = 0x01,
        AllUsers = 0x02,
    };

    //enum class XUserPrivilege: uint32_t
    //{
    //    CrossPlay              = 185, // The user can play with people outside of Xbox Live
    //    Clubs                  = 188, // Create/join/participate in Clubs
    //    Sessions               = 189, // Create/join non interactive multiplayer sessions
    //    Broadcast              = 190, // Broadcast live gameplay
    //    ManageProfilePrivacy   = 196, // Change settings to show real name
    //    GameDvr                = 198, // Upload GameDVR
    //    MultiplayerParties     = 203, // Join parties
    //    CloudManageSession     = 207, // Allocate cloud compute resources for their session
    //    CloudJoinSession       = 208, // Join cloud compute sessions
    //    CloudSavedGames        = 209, // Save games on the cloud
    //    SocialNetworkSharing   = 220, // Share progress to social networks
    //    UserGeneratedContent   = 247, // Access user generated content in game
    //    Communications         = 252, // Use real time voice and text communication
    //    Multiplayer            = 254, // Join multiplayer sessions
    //    AddFriends             = 255, // Add friends / people to follow
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XUserPrivilege : UInt32
    {
        CrossPlay = 185, // The user can play with people outside of Xbox Live
        Clubs = 188, // Create/join/participate in Clubs
        Sessions = 189, // Create/join non interactive multiplayer sessions
        Broadcast = 190, // Broadcast live gameplay
        ManageProfilePrivacy = 196, // Change settings to show real name
        GameDvr = 198, // Upload GameDVR
        MultiplayerParties = 203, // Join parties
        CloudManageSession = 207, // Allocate cloud compute resources for their session
        CloudJoinSession = 208, // Join cloud compute sessions
        CloudSavedGames = 209, // Save games on the cloud
        SocialNetworkSharing = 220, // Share progress to social networks
        UserGeneratedContent = 247, // Access user generated content in game
        Communications = 252, // Use real time voice and text communication
        Multiplayer = 254, // Join multiplayer sessions
        AddFriends = 255, // Add friends / people to follow
    }

    //enum class XUserPrivilegeOptions: uint32_t
    //{
    //    None            = 0x00,
    //    AllUsers        = 0x01,
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XUserPrivilegeOptions : UInt32
    {
        None = 0x00,
        AllUsers = 0x01,
    }

    //enum class XUserPrivilegeDenyReason: uint32_t
    //{
    //    None                = 0,
    //    PurchaseRequired    = 1,
    //    Restricted          = 2,
    //    Banned              = 3,
    //    Unknown             = 0xFFFFFFFF
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XUserPrivilegeDenyReason : UInt32
    {
        None = 0,
        PurchaseRequired = 1,
        Restricted = 2,
        Banned = 3,
        Unknown = 0xFFFFFFFF
    }

    //enum class XUserState: uint32_t
    //{
    //    SignedIn    = 0,
    //    SigningOut  = 1,
    //    SignedOut   = 2,
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XUserState : UInt32
    {
        SignedIn = 0,
        SigningOut = 1,
        SignedOut = 2,
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XUserLocalId
    {
        internal XUserLocalId(Interop.XUserLocalId interop)
        {
            this.interop = interop;
        }

        public XUserLocalId()
        {
            this.interop = new Interop.XUserLocalId();
        }

        internal Interop.XUserLocalId interop;

        public UInt64 Value
        {
            get => this.interop.value;
            set => this.interop.value = value;
        }

        [Obsolete("Please use Value instead, (UnityUpgradable) -> Value", true)]
        public UInt64 value
        {
            get => this.interop.value;
            set => this.interop.value = value;
        }
    }

    public class APP_LOCAL_DEVICE_ID
    {
        internal APP_LOCAL_DEVICE_ID(Interop.APP_LOCAL_DEVICE_ID interop)
        {
            this.interop = interop;
        }

        public APP_LOCAL_DEVICE_ID()
        {
            this.interop = new Interop.APP_LOCAL_DEVICE_ID();
        }

        internal Interop.APP_LOCAL_DEVICE_ID interop;

        public byte[] Value
        {
            get => interop.value;
            set => interop.value = value;
        }
    }

    public class XUserDeviceAssociationChange
    {
        internal XUserDeviceAssociationChange(Interop.XUserDeviceAssociationChange interop)
        {
            _deviceId = new APP_LOCAL_DEVICE_ID(interop.deviceId);
            _oldUser = new XUserLocalId(interop.oldUser);
            _newUser = new XUserLocalId(interop.newUser);
        }

        internal APP_LOCAL_DEVICE_ID _deviceId;
        internal XUserLocalId _oldUser;
        internal XUserLocalId _newUser;

        public XUserDeviceAssociationChange()
        {
            this._oldUser = new XUserLocalId();
            this._newUser = new XUserLocalId();
            this._deviceId = new APP_LOCAL_DEVICE_ID();
        }

        public APP_LOCAL_DEVICE_ID DeviceId
        {
            get => _deviceId;
            set => _deviceId = value;
        }

        public XUserLocalId OldUser
        {
            get => _oldUser;
            set => _oldUser = value;
        }

        public XUserLocalId NewUser
        {
            get => _newUser;
            set => _newUser = value;
        }
    }

    public class XUserGetTokenAndSignatureData
    {
        internal XUserGetTokenAndSignatureData(Interop.XUserGetTokenAndSignatureData interop)
        {
            this.interop = interop;
        }

        public XUserGetTokenAndSignatureData()
        {
            this.interop = new Interop.XUserGetTokenAndSignatureData();
        }

        internal Interop.XUserGetTokenAndSignatureData interop;

        public UInt64 TokenSize
        {
            get => interop.tokenSize;
            set => interop.tokenSize = value;
        }

        public UInt64 SignatureSize
        {
            get => interop.signatureSize;
            set => interop.signatureSize = value;
        }

        public string Token
        {
            get => interop.token;
            set => interop.token = value;
        }

        public string Signature
        {
            get => interop.signature;
            set => interop.signature = value;
        }
    }

    public class XUserGetTokenAndSignatureHttpHeader
    {
        internal XUserGetTokenAndSignatureHttpHeader(Interop.XUserGetTokenAndSignatureHttpHeader interop)
        {
            this.interop = interop;
        }

        internal Interop.XUserGetTokenAndSignatureHttpHeader interop;

        public XUserGetTokenAndSignatureHttpHeader()
        {
            this.interop = new Interop.XUserGetTokenAndSignatureHttpHeader();
        }

        public string Name
        {
            get => interop.name;
            set => interop.name = value;
        }

        public string Value
        {
            get => interop.value;
            set => interop.value = value;
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XUserGetTokenAndSignatureUtf16Data
    {
        internal XUserGetTokenAndSignatureUtf16Data(Interop.XUserGetTokenAndSignatureUtf16Data interop)
        {
            this.interop = interop;
        }

        public XUserGetTokenAndSignatureUtf16Data()
        {
            this.interop = new Interop.XUserGetTokenAndSignatureUtf16Data();
        }

        internal Interop.XUserGetTokenAndSignatureUtf16Data interop;

        public UInt64 TokenSize
        {
            get => interop.tokenSize;
            set => interop.tokenSize = value;
        }

        public UInt64 SignatureSize
        {
            get => interop.signatureSize;
            set => interop.signatureSize = value;
        }

        public string Token
        {
            get => interop.token;
            set => interop.token = value;
        }

        public string Signature
        {
            get => interop.signature;
            set => interop.signature = value;
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XUserGetTokenAndSignatureUtf16HttpHeader
    {
        internal XUserGetTokenAndSignatureUtf16HttpHeader(Interop.XUserGetTokenAndSignatureUtf16HttpHeader interop)
        {
            this.interop = interop;
        }

        internal Interop.XUserGetTokenAndSignatureUtf16HttpHeader interop;

        public XUserGetTokenAndSignatureUtf16HttpHeader()
        {
            this.interop = new Interop.XUserGetTokenAndSignatureUtf16HttpHeader();
        }

        public string Name
        {
            get => interop.name;
            set => interop.name = value;
        }

        public string Value
        {
            get => interop.value;
            set => interop.value = value;
        }
    }

    public delegate void XUserChangeEventCallback(
        IntPtr context,
        XUserLocalId userLocalId,
        XUserChangeEvent changeEvent);

    public delegate void XUserDefaultAudioEndpointUtf16ChangedCallback(
        IntPtr context,
        XUserLocalId user,
        XUserDefaultAudioEndpointKind defaultAudioEndpointKind,
        string endpointIdUtf16);

    public delegate void XUserDeviceAssociationChangedCallback(
        IntPtr context,
        ref XUserDeviceAssociationChange change);

    public partial class SDK
    {
        //const size_t XUserAudioEndpointMaxUtf16Count = 56;
        public static readonly UInt64 XUserAudioEndpointMaxUtf16Count = 56;

        public static int XUserAddAsync(XUserAddOptions options, XAsyncBlock async)
        {
            return NativeMethods.XUserAddAsync(options, async.InteropPtr);
        }

        public static int XUserAddResult(XAsyncBlock async, out XUserHandle newUser)
        {
            IntPtr handle;
            int hr = NativeMethods.XUserAddResult(async.InteropPtr, out handle);
            return XUserHandle.WrapAndReturnHResult(hr, handle, out newUser);
        }

        public static int XUserAddByIdWithUiAsync(UInt64 userId, XAsyncBlock async)
        {
            return NativeMethods.XUserAddByIdWithUiAsync(userId, async.InteropPtr);
        }

        public static int XUserAddByIdWithUiResult(XAsyncBlock async, out XUserHandle newUser)
        {
            IntPtr handle;
            int hr = NativeMethods.XUserAddByIdWithUiResult(async.InteropPtr, out handle);
            return XUserHandle.WrapAndReturnHResult(hr, handle, out newUser);
        }

        public static int XUserCheckPrivilege(XUserHandle user,
            XUserPrivilegeOptions options,
            XUserPrivilege privilege,
            out bool hasPrivilege,
            out XUserPrivilegeDenyReason reason)
        {
            IntPtr userHandle = (user != null) ? user.Handle : IntPtr.Zero;
            return NativeMethods.XUserCheckPrivilege(userHandle, options, privilege, out hasPrivilege, out reason);
        }

        public static void XUserCloseHandle(XUserHandle user)
        {
            if(user == null)
            {
                return;
            }

            user.Close();
        }

        public static Int32 XUserCloseSignOutDeferralHandle(XUserSignOutDeferralHandle deferral)
        {
            deferral.Close();

            return HR.S_OK;
        }

        public static int XUserCompare(XUserHandle user1, XUserHandle user2)
        {
            IntPtr userHandle1 = (user1 != null) ? user1.Handle : IntPtr.Zero;
            IntPtr userHandle2 = (user2 != null) ? user2.Handle : IntPtr.Zero;

            return NativeMethods.XUserCompare(userHandle1, userHandle2);
        }

        public static int XUserCompare(XUserHandle user1, XUserHandle user2, out Int32 comparisonResult)
        {
            comparisonResult = XUserCompare(user1, user2);

            return HR.S_OK;
        }

        public static int XUserDuplicateHandle(XUserHandle handle, out XUserHandle duplicatedHandle)
        {
            IntPtr dupHandle;
            IntPtr userHandle = (handle != null) ? handle.Handle : IntPtr.Zero;

            int hr = NativeMethods.XUserDuplicateHandle(userHandle, out dupHandle);
            return XUserHandle.WrapAndReturnHResult(hr, dupHandle, out duplicatedHandle);
        }

        public static int XUserFindControllerForUserWithUiAsync(XUserHandle user, XAsyncBlock async)
        {
            IntPtr userHandle = (user != null) ? user.Handle : IntPtr.Zero;

            return NativeMethods.XUserFindControllerForUserWithUiAsync(userHandle, async.InteropPtr);
        }

        public static int XUserFindControllerForUserWithUiResult(XAsyncBlock async, out APP_LOCAL_DEVICE_ID deviceId)
        {
            deviceId = default;
            Interop.APP_LOCAL_DEVICE_ID interopDevice = default;

            int hr = NativeMethods.XUserFindControllerForUserWithUiResult(async.InteropPtr, out interopDevice);
            if(HR.SUCCEEDED(hr))
            {
                deviceId = new APP_LOCAL_DEVICE_ID(interopDevice);
            }

            return hr;
        }

        public static int XUserFindForDevice(APP_LOCAL_DEVICE_ID deviceId, out XUserHandle handle)
        {
            IntPtr nativeHandle;
            int hr = NativeMethods.XUserFindForDevice(ref deviceId.interop, out nativeHandle);
            return XUserHandle.WrapAndReturnHResult(hr, nativeHandle, out handle);
        }

        public static int XUserFindUserById(UInt64 userId, out XUserHandle handle)
        {
            IntPtr interopHandle;
            Int32 hr = NativeMethods.XUserFindUserById(userId, out interopHandle);
            return XUserHandle.WrapAndReturnHResult(hr, interopHandle, out handle);
        }

        public static int XUserFindUserByLocalId(XUserLocalId userLocalId, out XUserHandle handle)
        {
            IntPtr interopHandle;
            Int32 hr = NativeMethods.XUserFindUserByLocalId(userLocalId.interop, out interopHandle);
            return XUserHandle.WrapAndReturnHResult(hr, interopHandle, out handle);
        }

        public static int XUserGetAgeGroup(XUserHandle user, out XUserAgeGroup ageGroup)
        {
            IntPtr userHandle = (user != null) ? user.Handle : IntPtr.Zero;

            return NativeMethods.XUserGetAgeGroup(userHandle, out ageGroup);
        }

        public static int XUserGetDefaultAudioEndpointUtf16(XUserLocalId user,
            XUserDefaultAudioEndpointKind defaultAudioEndpointKind,
            UInt64 endpointIdUtf16Count,
            char[] endpointIdUtf16,
            out UInt64 endpointIdUtf16Used)
        {
            return NativeMethods.XUserGetDefaultAudioEndpointUtf16(user.interop, defaultAudioEndpointKind, endpointIdUtf16Count, endpointIdUtf16, out endpointIdUtf16Used);
        }

        public static int XUserGetGamerPictureAsync(XUserHandle user,
            XUserGamerPictureSize pictureSize,
            XAsyncBlock async)
        {
            IntPtr userHandle = (user != null) ? user.Handle : IntPtr.Zero;

            return NativeMethods.XUserGetGamerPictureAsync(userHandle, pictureSize, async.InteropPtr);
        }

        public static int XUserGetGamerPictureResultSize(XAsyncBlock async,
            out UInt64 bufferSize)
        {
            return NativeMethods.XUserGetGamerPictureResultSize(async.InteropPtr, out bufferSize);
        }

        public static int XUserGetGamerPictureResult(XAsyncBlock async,
            byte[] buffer,
            out UInt64 bufferUsed)
        {
            return NativeMethods.XUserGetGamerPictureResult(async.InteropPtr, (ulong)buffer.Length, buffer, out bufferUsed);
        }

        public static int XUserGetGamertag(XUserHandle user,
            XUserGamertagComponent gamertagComponent,
            StringBuilder gamertag,
            out UInt64 gamertagUsed)
        {
            IntPtr userHandle = (user != null) ? user.Handle : IntPtr.Zero;

            return NativeMethods.XUserGetGamertag(userHandle, gamertagComponent, (ulong)gamertag.Capacity, gamertag, out gamertagUsed);
        }

        public static int XUserGetId(XUserHandle user,
            out UInt64 userId)
        {
            IntPtr userHandle = (user != null) ? user.Handle : IntPtr.Zero;

            return NativeMethods.XUserGetId(userHandle, out userId);
        }

        public static int XUserGetIsGuest(XUserHandle user,
            out bool isGuest)
        {
            IntPtr userHandle = (user != null) ? user.Handle : IntPtr.Zero;

            return NativeMethods.XUserGetIsGuest(userHandle, out isGuest);
        }

        public static int XUserGetLocalId(XUserHandle user,
            out XUserLocalId userLocalId)
        {
            userLocalId = default;
            Interop.XUserLocalId iteropUserLocalId = default;

            IntPtr userHandle = (user != null) ? user.Handle : IntPtr.Zero;

            int hr = NativeMethods.XUserGetLocalId(userHandle, out iteropUserLocalId);

            if(HR.SUCCEEDED(hr))
            {
                userLocalId = new XUserLocalId(iteropUserLocalId);
            }

            return hr;
        }

        public static int XUserGetMaxUsers(out UInt32 maxUsers)
        {
            return NativeMethods.XUserGetMaxUsers(out maxUsers);
        }

        public static int XUserGetSignOutDeferral(out XUserSignOutDeferralHandle deferral)
        {
            deferral = null;

            IntPtr handle;
            Int32 hr = NativeMethods.XUserGetSignOutDeferral(out handle);
            if (HR.SUCCEEDED(hr))
            {
                deferral = new XUserSignOutDeferralHandle(handle);
            }
            return hr;
        }

        public static int XUserGetState(XUserHandle user, out XUserState state)
        {
            IntPtr userHandle = (user != null) ? user.Handle : IntPtr.Zero;

            return NativeMethods.XUserGetState(userHandle, out state);
        }

        public static int XUserGetTokenAndSignatureAsync(XUserHandle user,
            XUserGetTokenAndSignatureOptions options,
            string method,
            string url,
            XUserGetTokenAndSignatureHttpHeader[] headers,
            byte[] bodyBuffer,
            XAsyncBlock async)
        {
            Interop.XUserGetTokenAndSignatureHttpHeader[] interopHeaders = null;

            if(headers != null)
            {
                interopHeaders = new Interop.XUserGetTokenAndSignatureHttpHeader[headers.Length];

                for (int i = 0; i < headers.Length; i++)
                {
                    interopHeaders[i] = headers[i].interop;
                }
            }

            IntPtr userHandle = (user != null) ? user.Handle : IntPtr.Zero;
            ulong headerCount = (interopHeaders != null) ? (ulong)interopHeaders.Length : 0;
            ulong bodySize = (bodyBuffer != null) ? (ulong)bodyBuffer.Length : 0;

            return NativeMethods.XUserGetTokenAndSignatureAsync(userHandle,
                options,
                method,
                url,
                headerCount,
                interopHeaders,
                bodySize,
                bodyBuffer,
                async.InteropPtr);
        }

        public static int XUserGetTokenAndSignatureResultSize(XAsyncBlock async,
            out UInt64 bufferSize)
        {
            return NativeMethods.XUserGetTokenAndSignatureResultSize(async.InteropPtr, out bufferSize);
        }

        public static int XUserGetTokenAndSignatureResult(XAsyncBlock async,
            byte[] buffer,
            out XUserGetTokenAndSignatureData result)
        {
            IntPtr ptrToBuffer;
            UInt64 bufferUsed;

            result = default;

            Interop.XUserGetTokenAndSignatureData interopResult = default;
            int hr = NativeMethods.XUserGetTokenAndSignatureResult(async.InteropPtr,
                (ulong)buffer.Length,
                buffer,
                out ptrToBuffer,
                out bufferUsed);
            if (HR.SUCCEEDED(hr))
            {
                interopResult = (Interop.XUserGetTokenAndSignatureData)Marshal.PtrToStructure(ptrToBuffer, typeof(Interop.XUserGetTokenAndSignatureData));

                result = new XUserGetTokenAndSignatureData(interopResult);
            }

            return hr;
        }

        public static int XUserGetTokenAndSignatureUtf16Async(XUserHandle user,
            XUserGetTokenAndSignatureOptions options,
            string method,
            string url,
            XUserGetTokenAndSignatureUtf16HttpHeader[] headers,
            byte[] bodyBuffer,
            XAsyncBlock async)
        {
            Interop.XUserGetTokenAndSignatureUtf16HttpHeader[] interopHeaders = null;

            if(headers != null)
            {
                interopHeaders = new Interop.XUserGetTokenAndSignatureUtf16HttpHeader[headers.Length];

                for(int i = 0; i < headers.Length; i++)
                {
                    interopHeaders[i] = headers[i].interop;
                }
            }

            IntPtr userHandle = (user != null) ? user.Handle : IntPtr.Zero;
            ulong headerCount = (interopHeaders != null) ? (ulong)interopHeaders.Length : 0;
            ulong bodySize = (bodyBuffer != null) ? (ulong)bodyBuffer.Length : 0;

            return NativeMethods.XUserGetTokenAndSignatureUtf16Async(userHandle,
                options,
                method,
                url,
                headerCount,
                interopHeaders,
                bodySize,
                bodyBuffer,
                async.InteropPtr);
        }

        public static int XUserGetTokenAndSignatureUtf16ResultSize(XAsyncBlock async,
            out UInt64 bufferSize)
        {
            return NativeMethods.XUserGetTokenAndSignatureUtf16ResultSize(async.InteropPtr, out bufferSize);
        }

        public static int XUserGetTokenAndSignatureUtf16Result(XAsyncBlock async,
            byte[] buffer,
            out XUserGetTokenAndSignatureUtf16Data result)
        {
            result = default;
            IntPtr ptrToBuffer;
            UInt64 bufferUsed;
            Interop.XUserGetTokenAndSignatureUtf16Data resultInterop = default;
            int hr = NativeMethods.XUserGetTokenAndSignatureUtf16Result(async.InteropPtr,
                (ulong)buffer.Length,
                buffer,
                out ptrToBuffer,
                out bufferUsed);
            if (HR.SUCCEEDED(hr))
            {
                resultInterop = (Interop.XUserGetTokenAndSignatureUtf16Data)Marshal.PtrToStructure(ptrToBuffer, typeof(Interop.XUserGetTokenAndSignatureUtf16Data));
                result = new XUserGetTokenAndSignatureUtf16Data(resultInterop);
            }

            return hr;
        }

        public static bool XUserIsStoreUser(XUserHandle user)
        {
            IntPtr userHandle = (user != null) ? user.Handle : IntPtr.Zero;

            return NativeMethods.XUserIsStoreUser(userHandle);
        }

        public static int XUserRegisterForChangeEvent(XTaskQueueHandle queue,
            IntPtr context,
            XUserChangeEventCallback callback,
            out XUserChangeRegistrationToken token)
        {
            Interop.XUserChangeEventCallback interopCallback = (IntPtr callbackContext, Interop.XUserLocalId userLocalId, XUserChangeEvent changeEvent) =>
            {
                callback(callbackContext, new XUserLocalId(userLocalId), changeEvent);
            };

            token = new XUserChangeRegistrationToken(interopCallback, context);

            UInt64 tokenValue;
            IntPtr interopQueue = queue != null ? queue.Handle : IntPtr.Zero;
            int hr = NativeMethods.XUserRegisterForChangeEvent(interopQueue,
                token.interop.CallbackContext,
                token.interop.StaticCallback,
                out tokenValue);
            if (HR.SUCCEEDED(hr))
            {
                token.Token = tokenValue;
            }
            else
            {
                token.Dispose();
                token = null;
            }

            return hr;
        }

        public static int XUserRegisterForDefaultAudioEndpointUtf16Changed(XTaskQueueHandle queue,
            IntPtr context,
            XUserDefaultAudioEndpointUtf16ChangedCallback callback,
            out XUserDefaultAudioEndpointUtf16RegistrationToken token)
        {
            Interop.XUserDefaultAudioEndpointUtf16ChangedCallback interopCallback = (IntPtr callbackContext,
                Interop.XUserLocalId user,
                XUserDefaultAudioEndpointKind defaultAudioEndpointKind,
                string endpointIdUtf16) =>
            {
                callback(callbackContext, new XUserLocalId(user), defaultAudioEndpointKind, endpointIdUtf16);
            };

            token = new XUserDefaultAudioEndpointUtf16RegistrationToken(interopCallback, context);

            UInt64 tokenValue;
            IntPtr interopQueue = queue != null ? queue.Handle : IntPtr.Zero;
            int hr = NativeMethods.XUserRegisterForDefaultAudioEndpointUtf16Changed(interopQueue,
                token.interop.CallbackContext,
                token.interop.StaticCallback,
                out tokenValue);
            if (HR.SUCCEEDED(hr))
            {
                token.Token = tokenValue;
            }
            else
            {
                token.Dispose();
                token = null;
            }

            return hr;
        }

        public static int XUserRegisterForDeviceAssociationChanged(XTaskQueueHandle queue,
            IntPtr context,
            XUserDeviceAssociationChangedCallback callback,
            out XUserDeviceAssociationChangedRegistrationToken token)
        {
            Interop.XUserDeviceAssociationChangedCallback interopCallback = (IntPtr context, ref Interop.XUserDeviceAssociationChange change) =>
            {
                XUserDeviceAssociationChange changeStruct = new XUserDeviceAssociationChange(change);
                callback(context, ref changeStruct);
            };

            token = new XUserDeviceAssociationChangedRegistrationToken(interopCallback, context);
            UInt64 tokenValue;
            IntPtr interopQueue = queue != null ? queue.Handle : IntPtr.Zero;
            int hr = NativeMethods.XUserRegisterForDeviceAssociationChanged(interopQueue,
                token.interop.CallbackContext,
                token.interop.StaticCallback,
                out tokenValue);
            if (HR.SUCCEEDED(hr))
            {
                token.Token = tokenValue;
            }
            else
            {
                token.Dispose();
                token = null;
            }

            return hr;
        }

        public static int XUserResolveIssueWithUiAsync(XUserHandle user,
            string url,
            XAsyncBlock async)
        {
            IntPtr userHandle = (user != null) ? user.Handle : IntPtr.Zero;

            return NativeMethods.XUserResolveIssueWithUiAsync(userHandle, url, async.InteropPtr);
        }

        public static int XUserResolveIssueWithUiResult(XAsyncBlock async)
        {
            return NativeMethods.XUserResolveIssueWithUiResult(async.InteropPtr);
        }

        public static int XUserResolveIssueWithUiUtf16Async(XUserHandle user,
            string url,
            XAsyncBlock async)
        {
            IntPtr userHandle = (user != null) ? user.Handle : IntPtr.Zero;

            return NativeMethods.XUserResolveIssueWithUiUtf16Async(userHandle, url, async.InteropPtr);
        }

        public static int XUserResolveIssueWithUiUtf16Result(XAsyncBlock async)
        {
            return NativeMethods.XUserResolveIssueWithUiUtf16Result(async.InteropPtr);
        }

        public static int XUserResolvePrivilegeWithUiAsync(XUserHandle user,
            XUserPrivilegeOptions options,
            XUserPrivilege privilege,
            XAsyncBlock async)
        {
            IntPtr userHandle = (user != null) ? user.Handle : IntPtr.Zero;

            return NativeMethods.XUserResolvePrivilegeWithUiAsync(userHandle, options, privilege, async.InteropPtr);
        }

        public static int XUserResolvePrivilegeWithUiResult(XAsyncBlock async)
        {
            return NativeMethods.XUserResolvePrivilegeWithUiResult(async.InteropPtr);
        }

        public static bool XUserUnregisterForChangeEvent(XUserChangeRegistrationToken token,
            bool wait)
        {
            return token.Unregister(wait);
        }

        public static bool XUserUnregisterForChangeEvent(XUserChangeRegistrationToken token)
        {
            return XUserUnregisterForChangeEvent(token, true);
        }

        public static bool XUserUnregisterForDefaultAudioEndpointUtf16Changed(XUserDefaultAudioEndpointUtf16RegistrationToken token,
            bool wait)
        {
            return token.Unregister(wait);
        }

        public static bool XUserUnregisterForDeviceAssociationChanged(XUserDeviceAssociationChangedRegistrationToken token,
            bool wait)
        {
            return token.Unregister(wait);
        }
    }

    public class XUserChangeRegistrationToken
    {
        internal Interop.XUserChangeRegistrationToken interop;


        internal XUserChangeRegistrationToken(Interop.XUserChangeEventCallback callback,
            IntPtr context)
        {
            interop = new Interop.XUserChangeRegistrationToken(callback, context);
        }

        public bool IsValid { get { return interop.IsValid; } }

        public UInt64 Token
        {
            get => interop.Token;
            set => interop.Token = value;
        }

        public bool Unregister(bool wait)
        {
            return interop.Unregister(wait);
        }

        public void Dispose()
        {
            interop.Dispose();
        }
    }

    public class XUserDefaultAudioEndpointUtf16RegistrationToken
    {
        internal Interop.XUserDefaultAudioEndpointUtf16RegistrationToken interop;


        internal XUserDefaultAudioEndpointUtf16RegistrationToken(Interop.XUserDefaultAudioEndpointUtf16ChangedCallback callback,
            IntPtr context)
        {
            interop = new Interop.XUserDefaultAudioEndpointUtf16RegistrationToken(callback, context);
        }

        public bool IsValid { get { return interop.IsValid; } }

        public UInt64 Token
        {
            get => interop.Token;
            set => interop.Token = value;
        }

        public bool Unregister(bool wait)
        {
            return interop.Unregister(wait);
        }

        public void Dispose()
        {
            interop.Dispose();
        }
    }

    public class XUserDeviceAssociationChangedRegistrationToken
    {
        internal Interop.XUserDeviceAssociationChangedRegistrationToken interop;


        internal XUserDeviceAssociationChangedRegistrationToken(Interop.XUserDeviceAssociationChangedCallback callback,
            IntPtr context)
        {
            interop = new Interop.XUserDeviceAssociationChangedRegistrationToken(callback, context);
        }

        public bool IsValid { get { return interop.IsValid; } }

        public UInt64 Token
        {
            get => interop.Token;
            set => interop.Token = value;
        }

        public bool Unregister(bool wait)
        {
            return interop.Unregister(wait);
        }

        public void Dispose()
        {
            interop.Dispose();
        }
    }
}
