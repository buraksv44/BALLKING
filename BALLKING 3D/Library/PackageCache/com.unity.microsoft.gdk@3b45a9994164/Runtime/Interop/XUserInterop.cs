// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Unity.XGamingRuntime.Interop
{
    internal struct XUserLocalId
    {
        public UInt64 value { get; set; }
    }

    /*
    #define APP_LOCAL_DEVICE_ID_SIZE 32
    typedef struct APP_LOCAL_DEVICE_ID
    {
        BYTE value[APP_LOCAL_DEVICE_ID_SIZE];
    } APP_LOCAL_DEVICE_ID;
    */
    internal struct APP_LOCAL_DEVICE_ID
    {
        public const int APP_LOCAL_DEVICE_ID_SIZE = 32;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = APP_LOCAL_DEVICE_ID_SIZE)] public byte[] value;
    }

    //typedef void CALLBACK XUserChangeEventCallback(
    //    _In_opt_ void* context,
    //    _In_ XUserLocalId userLocalId,
    //    _In_ XUserChangeEvent event
    //    );
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void XUserChangeEventCallback(
        IntPtr context,
        XUserLocalId userLocalId,
        XUserChangeEvent changeEvent);

    //typedef void CALLBACK XUserDefaultAudioEndpointUtf16ChangedCallback(
    //    _In_opt_ void* context,
    //    XUserLocalId user,
    //    XUserDefaultAudioEndpointKind defaultAudioEndpointKind,
    //    _In_opt_z_ const wchar_t* endpointIdUtf16
    //    );
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void XUserDefaultAudioEndpointUtf16ChangedCallback(
        IntPtr context,
        XUserLocalId user,
        XUserDefaultAudioEndpointKind defaultAudioEndpointKind,
        [MarshalAs(UnmanagedType.LPWStr)] string endpointIdUtf16);

    //struct XUserDeviceAssociationChange
    //{
    //    APP_LOCAL_DEVICE_ID deviceId;
    //    XUserLocalId oldUser;
    //    XUserLocalId newUser;
    //};
    internal struct XUserDeviceAssociationChange
    {
        internal APP_LOCAL_DEVICE_ID deviceId;
        internal XUserLocalId oldUser;
        internal XUserLocalId newUser;
    }

    //typedef void CALLBACK XUserDeviceAssociationChangedCallback(
    //    _In_opt_ void* context,
    //    _In_ const XUserDeviceAssociationChange* change
    //    );
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void XUserDeviceAssociationChangedCallback(
        IntPtr context,
        ref XUserDeviceAssociationChange change);

    //struct XUserGetTokenAndSignatureData
    //{
    //    size_t tokenSize;
    //    size_t signatureSize;
    //    _Field_size_bytes_opt_(tokenSize) _Null_terminated_ const char* token;
    //    _Field_size_bytes_opt_(signatureSize) _Null_terminated_ const char* signature;
    //};
    internal struct XUserGetTokenAndSignatureData
    {
        internal UInt64 tokenSize;
        internal UInt64 signatureSize;
        [MarshalAs(UnmanagedType.LPStr)] internal string token;
        [MarshalAs(UnmanagedType.LPStr)] internal string signature;
    }

    //struct XUserGetTokenAndSignatureHttpHeader
    //{
    //    _Field_z_ const char* name;
    //    _Field_z_ const char* value;
    //};
    internal struct XUserGetTokenAndSignatureHttpHeader
    {
        [MarshalAs(UnmanagedType.LPStr)] internal string name;
        [MarshalAs(UnmanagedType.LPStr)] internal string value;
    }

    //struct XUserGetTokenAndSignatureUtf16Data
    //{
    //    size_t tokenCount;
    //    size_t signatureCount;
    //    _Field_size_opt_(tokenCount) _Null_terminated_ const wchar_t* token;
    //    _Field_size_opt_(signatureCount) _Null_terminated_ const wchar_t* signature;
    //};
    internal struct XUserGetTokenAndSignatureUtf16Data
    {
        internal UInt64 tokenSize;
        internal UInt64 signatureSize;
        [MarshalAs(UnmanagedType.LPWStr)] internal string token;
        [MarshalAs(UnmanagedType.LPWStr)] internal string signature;
    }

    //struct XUserGetTokenAndSignatureUtf16HttpHeader
    //{
    //    _Field_z_ const wchar_t* name;
    //    _Field_z_ const wchar_t* value;
    //};
    internal struct XUserGetTokenAndSignatureUtf16HttpHeader
    {
        [MarshalAs(UnmanagedType.LPWStr)] internal string name;
        [MarshalAs(UnmanagedType.LPWStr)] internal string value;
    }

    internal class XUserChangeRegistrationToken : XRegistrationToken<Interop.XUserChangeEventCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XUserChangeEventCallback))]
        static void OnChangeEvent(IntPtr context, XUserLocalId userLocalId, XUserChangeEvent changeEvent)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XUserChangeEventCallback>;
            wrapper.Callback(wrapper.Context, userLocalId, changeEvent);
        }

        public XUserChangeRegistrationToken(Interop.XUserChangeEventCallback callback, IntPtr context) :
            base(callback, context, OnChangeEvent)
        {
        }

        public bool Unregister(bool wait)
        {
            bool result = true;
            if (this.Token != 0)
            {
                result = NativeMethods.XUserUnregisterForChangeEvent(this.Token, wait);
                this.Token = 0;
            }
            return result;
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister(wait: true);
        }
    }

    internal class XUserDefaultAudioEndpointUtf16RegistrationToken : XRegistrationToken<Interop.XUserDefaultAudioEndpointUtf16ChangedCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XUserDefaultAudioEndpointUtf16ChangedCallback))]
        static void OnDefaultAudioEndpointUtf16Changed(
            IntPtr context,
            XUserLocalId user,
            XUserDefaultAudioEndpointKind defaultAudioEndpointKind,
            string endpointIdUtf16)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XUserDefaultAudioEndpointUtf16ChangedCallback>;
            wrapper.Callback(wrapper.Context, user, defaultAudioEndpointKind, endpointIdUtf16);
        }

        public XUserDefaultAudioEndpointUtf16RegistrationToken(Interop.XUserDefaultAudioEndpointUtf16ChangedCallback callback, IntPtr context) :
            base(callback, context, OnDefaultAudioEndpointUtf16Changed)
        { }

        public bool Unregister(bool wait)
        {
            bool result = true;
            if (this.Token != 0)
            {
                result = NativeMethods.XUserUnregisterForDefaultAudioEndpointUtf16Changed(this.Token, wait);
                this.Token = 0;
            }
            return result;
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister(wait: true);
        }
    }

    internal class XUserDeviceAssociationChangedRegistrationToken : XRegistrationToken<Interop.XUserDeviceAssociationChangedCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XUserDeviceAssociationChangedCallback))]
        static void DeviceAssociationChanged(IntPtr context, ref Interop.XUserDeviceAssociationChange change)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XUserDeviceAssociationChangedCallback>;
            wrapper.Callback(wrapper.Context, ref change);
        }

        public XUserDeviceAssociationChangedRegistrationToken(Interop.XUserDeviceAssociationChangedCallback callback, IntPtr context) :
            base(callback, context, DeviceAssociationChanged)
        {
        }

        public bool Unregister(bool wait)
        {
            bool result = true;
            if (this.Token != 0)
            {
                result = NativeMethods.XUserUnregisterForDeviceAssociationChanged(this.Token, wait);
                this.Token = 0;
            }
            return result;
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister(wait: true);
        }
    }

    partial class NativeMethods
    {
        //STDAPI XUserAddAsync(
        //    _In_ XUserAddOptions options,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserAddAsync(XUserAddOptions options,
            IntPtr async);

        //STDAPI XUserAddResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XUserHandle* newUser
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserAddResult(IntPtr async,
            out IntPtr newUser);

        //STDAPI XUserAddByIdWithUiAsync (
        //    _In_ uint64_t userId,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserAddByIdWithUiAsync(UInt64 userId,
            IntPtr async);

        //STDAPI XUserAddByIdWithUiResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XUserHandle* newUser
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserAddByIdWithUiResult(IntPtr async,
            out IntPtr newUser);

        //STDAPI XUserCheckPrivilege(
        //    _In_ XUserHandle user,
        //    _In_ XUserPrivilegeOptions options,
        //    _In_ XUserPrivilege privilege,
        //    _Out_ bool* hasPrivilege,
        //    _Out_opt_ XUserPrivilegeDenyReason* reason
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserCheckPrivilege(IntPtr user,
            XUserPrivilegeOptions options,
            XUserPrivilege privilege,
            [MarshalAs(UnmanagedType.I1)] out bool hasPrivilege,
            out XUserPrivilegeDenyReason reason);

        // STDAPI_(void) XUserCloseHandle(
        //     _In_ XUserHandle user
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XUserCloseHandle(IntPtr user);

        //STDAPI_(void) XUserCloseSignOutDeferralHandle(
        //    _In_ XUserSignOutDeferralHandle deferral
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XUserCloseSignOutDeferralHandle(IntPtr deferral);

        //    STDAPI_(int32_t) XUserCompare(
        //    _In_opt_ XUserHandle user1,
        //    _In_opt_ XUserHandle user2
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserCompare(IntPtr user1,
            IntPtr user2);

        //STDAPI XUserDuplicateHandle(
        //    _In_ XUserHandle handle,
        //    _Out_ XUserHandle* duplicatedHandle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserDuplicateHandle(IntPtr handle,
            out IntPtr duplicatedHandle);

        //STDAPI XUserFindControllerForUserWithUiAsync(
        //    _In_ XUserHandle user,
        //    _In_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserFindControllerForUserWithUiAsync(IntPtr user,
            IntPtr async);

        //STDAPI XUserFindControllerForUserWithUiResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ APP_LOCAL_DEVICE_ID* deviceId
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserFindControllerForUserWithUiResult(IntPtr async,
            out APP_LOCAL_DEVICE_ID deviceId);

        //STDAPI XUserFindForDevice(
        //    _In_ const APP_LOCAL_DEVICE_ID* deviceId,
        //    _Out_ XUserHandle* handle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserFindForDevice(ref APP_LOCAL_DEVICE_ID deviceId,
            out IntPtr handle);

        // STDAPI XUserFindUserById(
        //     _In_ uint64_t userId,
        //     _Out_ XUserHandle* handle
        //     ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserFindUserById(UInt64 userId,
            out IntPtr handle);

        //STDAPI XUserFindUserByLocalId(
        //    _In_ XUserLocalId userLocalId,
        //    _Out_ XUserHandle* handle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserFindUserByLocalId(XUserLocalId userLocalId,
            out IntPtr handle);

        //STDAPI XUserGetAgeGroup(
        //    _In_ XUserHandle user,
        //    _Out_ XUserAgeGroup* ageGroup
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserGetAgeGroup(IntPtr user,
            out XUserAgeGroup ageGroup);

        //STDAPI XUserGetDefaultAudioEndpointUtf16(
        //    XUserLocalId user,
        //    XUserDefaultAudioEndpointKind defaultAudioEndpointKind,
        //    size_t endpointIdUtf16Count,
        //    _Out_writes_to_(endpointIdUtf16Count, *endpointIdUtf16Used) wchar_t* endpointIdUtf16,
        //    _Out_opt_ size_t* endpointIdUtf16Used
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        internal static extern Int32 XUserGetDefaultAudioEndpointUtf16(XUserLocalId user,
            XUserDefaultAudioEndpointKind defaultAudioEndpointKind,
            UInt64 endpointIdUtf16Count,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] char[] endpointIdUtf16,
            out UInt64 endpointIdUtf16Used);

        //STDAPI XUserGetGamerPictureAsync(
        //    _In_ XUserHandle user,
        //    _In_ XUserGamerPictureSize pictureSize,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserGetGamerPictureAsync(IntPtr user,
            XUserGamerPictureSize pictureSize,
            IntPtr async);

        //STDAPI XUserGetGamerPictureResultSize(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ size_t* bufferSize
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserGetGamerPictureResultSize(IntPtr async,
            out UInt64 bufferSize);

        //STDAPI XUserGetGamerPictureResult(
        //    _Inout_ XAsyncBlock* async,
        //    _In_ size_t bufferSize,
        //    _Out_writes_bytes_to_(bufferSize, *bufferUsed) void* buffer,
        //    _Out_opt_ size_t* bufferUsed
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserGetGamerPictureResult(IntPtr async,
            UInt64 bufferSize,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] buffer,
            out UInt64 bufferUsed);

        //STDAPI XUserGetGamertag(
        //    _In_ XUserHandle user,
        //    _In_ XUserGamertagComponent gamertagComponent,
        //    _In_ size_t gamertagSize,
        //    _Out_writes_bytes_to_(gamertagSize, *gamertagUsed) char* gamertag,
        //    _Out_opt_ size_t* gamertagUsed
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserGetGamertag(IntPtr user,
            XUserGamertagComponent gamertagComponent,
            UInt64 gamertagSize,
            StringBuilder gamertag,
            out UInt64 gamertagUsed);

        //STDAPI XUserGetId(
        //    _In_ XUserHandle user,
        //    _Out_ uint64_t* userId
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserGetId(IntPtr user,
            out UInt64 userId);

        //STDAPI XUserGetIsGuest(
        //    _In_ XUserHandle user,
        //    _Out_ bool* isGuest
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserGetIsGuest(IntPtr user,
            [MarshalAs(UnmanagedType.I1)] out bool isGuest);

        //STDAPI XUserGetLocalId(
        //    _In_ XUserHandle user,
        //    _Out_ XUserLocalId* userLocalId
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserGetLocalId(IntPtr user,
            out XUserLocalId userLocalId);

        //STDAPI XUserGetMaxUsers(
        //    _Out_ uint32_t* maxUsers
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserGetMaxUsers(out UInt32 maxUsers);

        //STDAPI XUserGetSignOutDeferral(
        //    _Out_ XUserSignOutDeferralHandle* deferral
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserGetSignOutDeferral(out IntPtr deferral);

        //STDAPI XUserGetState(
        //    _In_ XUserHandle user,
        //    _Out_ XUserState* state
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserGetState(IntPtr user,
            out XUserState state);

        //STDAPI XUserGetTokenAndSignatureAsync(
        //    _In_ XUserHandle user,
        //    _In_ XUserGetTokenAndSignatureOptions options,
        //    _In_z_ const char* method,
        //    _In_z_ const char* url,
        //    _In_ size_t headerCount,
        //    _In_reads_opt_(headerCount) const XUserGetTokenAndSignatureHttpHeader* headers,
        //    _In_ size_t bodySize,
        //    _In_reads_bytes_opt_(bodySize) const void* bodyBuffer,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserGetTokenAndSignatureAsync(IntPtr user,
            XUserGetTokenAndSignatureOptions options,
            [MarshalAs(UnmanagedType.LPStr)] string method,
            [MarshalAs(UnmanagedType.LPStr)] string url,
            UInt64 headerCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] XUserGetTokenAndSignatureHttpHeader[] headers,
            UInt64 bodySize,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 6)] byte[] bodyBuffer,
            IntPtr async);

        //STDAPI XUserGetTokenAndSignatureResultSize(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ size_t* bufferSize
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserGetTokenAndSignatureResultSize(IntPtr async,
            out UInt64 bufferSize);

        //STDAPI XUserGetTokenAndSignatureResult(
        //    _Inout_ XAsyncBlock* async,
        //    _In_ size_t bufferSize,
        //    _Out_writes_bytes_to_(bufferSize, *bufferUsed) void* buffer,
        //    _Outptr_ XUserGetTokenAndSignatureData** ptrToBuffer,
        //    _Out_opt_ size_t* bufferUsed
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserGetTokenAndSignatureResult(IntPtr async,
            UInt64 bufferSize,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] buffer,
            out IntPtr ptrToBuffer,
            out UInt64 bufferUsed);

        //STDAPI XUserGetTokenAndSignatureUtf16Async(
        //    _In_ XUserHandle user,
        //    _In_ XUserGetTokenAndSignatureOptions options,
        //    _In_z_ const wchar_t* method,
        //    _In_z_ const wchar_t* url,
        //    _In_ size_t headerCount,
        //    _In_reads_opt_(headerCount) const XUserGetTokenAndSignatureUtf16HttpHeader* headers,
        //    _In_ size_t bodySize,
        //    _In_reads_bytes_opt_(bodySize) const void* bodyBuffer,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserGetTokenAndSignatureUtf16Async(IntPtr user,
            XUserGetTokenAndSignatureOptions options,
            [MarshalAs(UnmanagedType.LPWStr)] string method,
            [MarshalAs(UnmanagedType.LPWStr)] string url,
            UInt64 headerCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] XUserGetTokenAndSignatureUtf16HttpHeader[] headers,
            UInt64 bodySize,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 6)] byte[] bodyBuffer,
            IntPtr async);

        //STDAPI XUserGetTokenAndSignatureUtf16ResultSize(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ size_t* bufferSize
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserGetTokenAndSignatureUtf16ResultSize(IntPtr async,
            out UInt64 bufferSize);

        //STDAPI XUserGetTokenAndSignatureUtf16Result(
        //    _Inout_ XAsyncBlock* async,
        //    _In_ size_t bufferSize,
        //    _Out_writes_bytes_to_(bufferSize, *bufferUsed) void* buffer,
        //    _Outptr_ XUserGetTokenAndSignatureUtf16Data** ptrToBuffer,
        //    _Out_opt_ size_t* bufferUsed
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserGetTokenAndSignatureUtf16Result(IntPtr async,
            UInt64 bufferSize,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] buffer,
            out IntPtr ptrToBuffer,
            out UInt64 bufferUsed);

        //STDAPI_(bool) XUserIsStoreUser(
        //    _In_ XUserHandle user
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XUserIsStoreUser(IntPtr user);

        //STDAPI XUserRegisterForChangeEvent(
        //    _In_opt_ XTaskQueueHandle queue,
        //    _In_opt_ void* context,
        //    _In_ XUserChangeEventCallback* callback,
        //    _Out_ XTaskQueueRegistrationToken* token
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserRegisterForChangeEvent(IntPtr queue,
            IntPtr context,
            XUserChangeEventCallback callback,
            out UInt64 token);

        //STDAPI XUserRegisterForDefaultAudioEndpointUtf16Changed(
        //    _In_opt_ XTaskQueueHandle queue,
        //    _In_opt_ void* context,
        //    _In_ XUserDefaultAudioEndpointUtf16ChangedCallback* callback,
        //    _Out_ XTaskQueueRegistrationToken* token
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserRegisterForDefaultAudioEndpointUtf16Changed(IntPtr queue,
            IntPtr context,
            XUserDefaultAudioEndpointUtf16ChangedCallback callback,
            out UInt64 token);

        //STDAPI XUserRegisterForDeviceAssociationChanged(
        //    _In_opt_ XTaskQueueHandle queue,
        //    _In_opt_ void* context,
        //    _In_ XUserDeviceAssociationChangedCallback* callback,
        //    _Out_ XTaskQueueRegistrationToken* token
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserRegisterForDeviceAssociationChanged(IntPtr queue,
            IntPtr context,
            XUserDeviceAssociationChangedCallback callback,
            out UInt64 token);

        //STDAPI XUserResolveIssueWithUiAsync(
        //    _In_ XUserHandle user,
        //    _In_opt_z_ const char* url,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserResolveIssueWithUiAsync(IntPtr user,
            [MarshalAs(UnmanagedType.LPStr)] string url,
            IntPtr async);

        //STDAPI XUserResolveIssueWithUiResult(
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserResolveIssueWithUiResult(IntPtr async);

        //STDAPI XUserResolveIssueWithUiUtf16Async(
        //    _In_ XUserHandle user,
        //    _In_opt_z_ const wchar_t* url,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserResolveIssueWithUiUtf16Async(IntPtr user,
            [MarshalAs(UnmanagedType.LPWStr)] string url,
            IntPtr async);

        //STDAPI XUserResolveIssueWithUiUtf16Result(
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserResolveIssueWithUiUtf16Result(IntPtr async);

        //STDAPI XUserResolvePrivilegeWithUiAsync(
        //    _In_ XUserHandle user,
        //    _In_ XUserPrivilegeOptions options,
        //    _In_ XUserPrivilege privilege,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserResolvePrivilegeWithUiAsync(IntPtr user,
            XUserPrivilegeOptions options,
            XUserPrivilege privilege,
            IntPtr async);

        //STDAPI XUserResolvePrivilegeWithUiResult(
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XUserResolvePrivilegeWithUiResult(IntPtr async);

        //STDAPI_(bool) XUserUnregisterForChangeEvent(
        //    _In_ XTaskQueueRegistrationToken token,
        //    _In_ bool wait
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XUserUnregisterForChangeEvent(UInt64 token,
            [MarshalAs(UnmanagedType.I1)] bool wait);

        //STDAPI_(bool) XUserUnregisterForDefaultAudioEndpointUtf16Changed(
        //    XTaskQueueRegistrationToken token,
        //    bool wait
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XUserUnregisterForDefaultAudioEndpointUtf16Changed(UInt64 token,
            [MarshalAs(UnmanagedType.I1)] bool wait);

        //STDAPI_(bool) XUserUnregisterForDeviceAssociationChanged(
        //    XTaskQueueRegistrationToken token,
        //    bool wait
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XUserUnregisterForDeviceAssociationChanged(UInt64 token,
            [MarshalAs(UnmanagedType.I1)] bool wait);
    }
}
