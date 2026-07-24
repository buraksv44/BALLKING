// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //struct XNetworkingConnectivityHint
    //{
    //    XNetworkingConnectivityLevelHint connectivityLevel;
    //    XNetworkingConnectivityCostHint connectivityCost;
    //    uint32_t ianaInterfaceType;
    //    bool networkInitialized;
    //    bool approachingDataLimit;
    //    bool overDataLimit;
    //    bool roaming;
    //};
    [StructLayout(LayoutKind.Sequential)]
    internal struct XNetworkingConnectivityHint
    {
        internal XNetworkingConnectivityLevelHint connectivityLevel;
        internal XNetworkingConnectivityCostHint connectivityCost;
        internal UInt32 ianaInterfaceType;
        [MarshalAs(UnmanagedType.I1)]
        internal bool networkInitialized;
        [MarshalAs(UnmanagedType.I1)]
        internal bool approachingDataLimit;
        [MarshalAs(UnmanagedType.I1)]
        internal bool overDataLimit;
        [MarshalAs(UnmanagedType.I1)]
        internal bool roaming;
    };


    //struct XNetworkingThumbprint
    //{
    //    XNetworkingThumbprintType thumbprintType;
    //    size_t thumbprintBufferByteCount;
    //    _Field_size_(thumbprintBufferByteCount) uint8_t* thumbprintBuffer;
    //};
    [StructLayout(LayoutKind.Sequential)]
    internal struct XNetworkingThumbprint
    {
        internal XNetworkingThumbprintType thumbprintType;
        internal UInt64 thumbprintBufferByteCount;
        internal IntPtr thumbprintBuffer;
    };

    //struct XNetworkingSecurityInformation
    //{
    //    uint32_t enabledHttpSecurityProtocolFlags;
    //    size_t thumbprintCount;
    //    _Field_size_(thumbprintCount) XNetworkingThumbprint* thumbprints;
    //};
    [StructLayout(LayoutKind.Sequential)]
    internal struct XNetworkingSecurityInformation
    {
        internal UInt32 enabledHttpSecurityProtocolFlags;
        internal UInt64 thumbprintCount;
        internal IntPtr thumbprints;
    };

    //struct XNetworkingTcpQueuedReceivedBufferUsageStatistics
    //{
    //    uint64_t numBytesCurrentlyQueued;
    //    uint64_t peakNumBytesEverQueued;
    //    uint64_t totalNumBytesQueued;
    //    uint64_t numBytesDroppedForExceedingConfiguredMax;
    //    uint64_t numBytesDroppedDueToAnyFailure;
    //};
    [StructLayout(LayoutKind.Sequential)]
    internal struct XNetworkingTcpQueuedReceivedBufferUsageStatistics
    {
        internal UInt64 numBytesCurrentlyQueued;
        internal UInt64 peakNumBytesEverQueued;
        internal UInt64 totalNumBytesQueued;
        internal UInt64 numBytesDroppedForExceedingConfiguredMax;
        internal UInt64 numBytesDroppedDueToAnyFailure;
    };

    //union XNetworkingStatisticsBuffer
    //{
    //    XNetworkingTcpQueuedReceivedBufferUsageStatistics tcpQueuedReceiveBufferUsage;
    //};
    [StructLayout(LayoutKind.Sequential)]
    internal struct XNetworkingStatisticsBuffer
    {
        internal XNetworkingTcpQueuedReceivedBufferUsageStatistics tcpQueuedReceiveBufferUsage;
    }

    // Callback token classes.
    internal class XNetworkingRegisterConnectivityHintChangedCallbackToken : XRegistrationToken<Interop.XNetworkingConnectivityHintChangedCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XNetworkingConnectivityHintChangedCallback))]
        static void OnConnectivityHintChanged(IntPtr context, XNetworkingConnectivityHint connectivityHint)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XNetworkingConnectivityHintChangedCallback>;
            wrapper.Callback(wrapper.Context, connectivityHint);
        }

        public XNetworkingRegisterConnectivityHintChangedCallbackToken(Interop.XNetworkingConnectivityHintChangedCallback callback,
            IntPtr context)
            : base(callback, context, OnConnectivityHintChanged)
        { }

        public bool Unregister(bool wait)
        {
            bool result = true;
            if (this.Token != 0)
            {
                result = NativeMethods.XNetworkingUnregisterConnectivityHintChanged(this.Token, wait);
                this.Token = 0;
            }
            return result;
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister(wait: true);
        }
    }

    internal class XNetworkingPreferredLocalUdpMultiplayerPortChangedCallbackToken : XRegistrationToken<Interop.XNetworkingPreferredLocalUdpMultiplayerPortChangedCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XNetworkingPreferredLocalUdpMultiplayerPortChangedCallback))]
        static void OnPreferredLocalUdpMultiplayerPortChanged(IntPtr context, UInt16 preferredLocalUdpMultiplayerPort)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XNetworkingPreferredLocalUdpMultiplayerPortChangedCallback>;
            wrapper.Callback(wrapper.Context, preferredLocalUdpMultiplayerPort);
        }

        public XNetworkingPreferredLocalUdpMultiplayerPortChangedCallbackToken(Interop.XNetworkingPreferredLocalUdpMultiplayerPortChangedCallback callback,
            IntPtr context) :
            base(callback, context, OnPreferredLocalUdpMultiplayerPortChanged)
        { }

        public bool Unregister(bool wait)
        {
            bool result = true;
            if (this.Token != 0)
            {
                result = NativeMethods.XNetworkingUnregisterPreferredLocalUdpMultiplayerPortChanged(this.Token, wait);
                this.Token = 0;
            }
            return result;
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister(wait: true);
        }
    }

    // typedef void XNetworkingConnectivityHintChangedCallback(_In_opt_ void* context, _In_ const XNetworkingConnectivityHint* connectivityHint);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void XNetworkingConnectivityHintChangedCallback(IntPtr context, XNetworkingConnectivityHint connectivityHint);

    //typedef void XNetworkingPreferredLocalUdpMultiplayerPortChangedCallback(_In_opt_ void* context, _In_ uint16_t );
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void XNetworkingPreferredLocalUdpMultiplayerPortChangedCallback(IntPtr context, UInt16 preferredLocalUdpMultiplayerPort);

    partial class NativeMethods
    {
        //STDAPI XNetworkingGetConnectivityHint(
        //    _Out_ XNetworkingConnectivityHint* connectivityHint) noexcept
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XNetworkingGetConnectivityHint(
            out XNetworkingConnectivityHint connectivityHint);

        //STDAPI XNetworkingQueryConfigurationSetting(
        //    _In_ XNetworkingConfigurationSetting configurationSetting,
        //    _Out_ uint64_t* value) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XNetworkingQueryConfigurationSetting(
            XNetworkingConfigurationSetting configurationSetting,
            out UInt64 value);

        //STDAPI XNetworkingQueryPreferredLocalUdpMultiplayerPort(
        //    _Out_ uint16_t* preferredLocalUdpMultiplayerPort) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XNetworkingQueryPreferredLocalUdpMultiplayerPort(
            out UInt16 value);

        //STDAPI XNetworkingQueryPreferredLocalUdpMultiplayerPortAsync(
        //    _Inout_ XAsyncBlock* asyncBlock) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XNetworkingQueryPreferredLocalUdpMultiplayerPortAsync(
            IntPtr async);

        //STDAPI XNetworkingQueryPreferredLocalUdpMultiplayerPortAsyncResult(
        //    _Inout_ XAsyncBlock* asyncBlock,
        //    _Out_ uint16_t* preferredLocalUdpMultiplayerPort) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XNetworkingQueryPreferredLocalUdpMultiplayerPortAsyncResult(
            IntPtr async,
            out UInt16 preferredLocalUdpMultiplayerPort);

        //STDAPI XNetworkingQuerySecurityInformationForUrlAsync(
        //    _In_z_ const char* url,
        //    _Inout_ XAsyncBlock* asyncBlock) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XNetworkingQuerySecurityInformationForUrlAsync(
            [MarshalAs(UnmanagedType.LPStr)] string url,
            IntPtr async);

        //STDAPI XNetworkingQuerySecurityInformationForUrlAsyncResultSize(
        //    _Inout_ XAsyncBlock* asyncBlock,
        //    _Out_ size_t* securityInformationBufferByteCount) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XNetworkingQuerySecurityInformationForUrlAsyncResultSize(
            IntPtr async,
            out UInt64 securityInformationBufferByteCount);

        //STDAPI XNetworkingQuerySecurityInformationForUrlAsyncResult(
        //    _Inout_ XAsyncBlock* asyncBlock,
        //    _In_ size_t securityInformationBufferByteCount,
        //    _Out_opt_ size_t* securityInformationBufferByteCountUsed,
        //    _Out_writes_bytes_to_(securityInformationBufferByteCount, * securityInformationBufferByteCountUsed) uint8_t* securityInformationBuffer,
        //    _Outptr_ XNetworkingSecurityInformation** securityInformation) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XNetworkingQuerySecurityInformationForUrlAsyncResult(
            IntPtr async,
            UInt64 securityInformationBufferByteCount,
            out UInt64 securityInformationBufferByteCountUsed,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] securityInformationBuffer,
            out IntPtr securityInformation);

        //STDAPI XNetworkingQuerySecurityInformationForUrlUtf16Async(
        //    _In_z_ const wchar_t* url,
        //    _Inout_ XAsyncBlock* asyncBlock) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XNetworkingQuerySecurityInformationForUrlUtf16Async(
            [MarshalAs(UnmanagedType.LPWStr)] string url,
            IntPtr async);

        //STDAPI XNetworkingQuerySecurityInformationForUrlUtf16AsyncResultSize(
        //    _Inout_ XAsyncBlock* asyncBlock,
        //    _Out_ size_t* securityInformationBufferByteCount) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XNetworkingQuerySecurityInformationForUrlUtf16AsyncResultSize(
            IntPtr async,
            out UInt64 securityInformationBufferByteCount);

        //STDAPI XNetworkingQuerySecurityInformationForUrlUtf16AsyncResult(
        //    _Inout_ XAsyncBlock* asyncBlock,
        //    _In_ size_t securityInformationBufferByteCount,
        //    _Out_opt_ size_t* securityInformationBufferByteCountUsed,
        //    _Out_writes_bytes_to_(securityInformationBufferByteCount, * securityInformationBufferByteCountUsed) uint8_t* securityInformationBuffer,
        //    _Outptr_ XNetworkingSecurityInformation** securityInformation) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XNetworkingQuerySecurityInformationForUrlUtf16AsyncResult(
            IntPtr async,
            UInt64 securityInformationBufferByteCount,
            out UInt64 securityInformationBufferByteCountUsed,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] securityInformationBuffer,
            out IntPtr securityInformation);

        //STDAPI XNetworkingQueryStatistics(
        //    _In_ XNetworkingStatisticsType statisticsType,
        //    _Out_ XNetworkingStatisticsBuffer* statisticsBuffer
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XNetworkingQueryStatistics(
            XNetworkingStatisticsType statisticsType,
            out XNetworkingStatisticsBuffer statisticsBuffer);

        //STDAPI XNetworkingRegisterConnectivityHintChanged(
        //    _In_opt_ XTaskQueueHandle queue,
        //    _In_opt_ void* context,
        //    _In_ XNetworkingConnectivityHintChangedCallback* callback,
        //    _Out_ XTaskQueueRegistrationToken* token
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XNetworkingRegisterConnectivityHintChanged(
            IntPtr taskQueueHandle,
            IntPtr context,
            XNetworkingConnectivityHintChangedCallback callback,
            out UInt64 registrationToken);

        //STDAPI_(bool) XNetworkingUnregisterConnectivityHintChanged(
        //    _In_ XTaskQueueRegistrationToken token,
        //    _In_ bool wait
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XNetworkingUnregisterConnectivityHintChanged(
            UInt64 token,
            [MarshalAs(UnmanagedType.I1)]
            bool wait);

        //STDAPI XNetworkingRegisterPreferredLocalUdpMultiplayerPortChanged(
        //    _In_opt_ XTaskQueueHandle queue,
        //    _In_opt_ void* context,
        //    _In_ XNetworkingPreferredLocalUdpMultiplayerPortChangedCallback* callback,
        //    _Out_ XTaskQueueRegistrationToken* token) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XNetworkingRegisterPreferredLocalUdpMultiplayerPortChanged(
            IntPtr taskQueueHandle,
            IntPtr context,
            XNetworkingPreferredLocalUdpMultiplayerPortChangedCallback callback,
            out UInt64 registrationToken);

        //STDAPI_(bool) XNetworkingUnregisterPreferredLocalUdpMultiplayerPortChanged(
        //    _In_ XTaskQueueRegistrationToken token,
        //    _In_ bool wait
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XNetworkingUnregisterPreferredLocalUdpMultiplayerPortChanged(
            UInt64 token,
            [MarshalAs(UnmanagedType.I1)]
            bool wait);

        //STDAPI XNetworkingVerifyServerCertificate(
        //    _In_ void* requestHandle,
        //    _In_ const XNetworkingSecurityInformation* securityInformation) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XNetworkingVerifyServerCertificate(
            IntPtr requestHandle,
            IntPtr securityInformation);
    }
}
