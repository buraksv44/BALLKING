using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Unity.XGamingRuntime.Interop
{
    internal struct XGameStreamingClientId
    {
        internal UInt64 value;
    };

    internal struct D3D12XBOX_FRAME_PIPELINE_TOKEN
    {
        internal UInt64 value;
    }

    // struct XGameStreamingTouchControlsStateValue
    // {
    //     XGameStreamingTouchControlsStateValueKind valueKind;
    //     union
    //     {
    //         bool booleanValue;
    //         int64_t integerValue;
    //         double doubleValue;
    //         const char* stringValue;
    //     };
    // };
    [StructLayout(LayoutKind.Explicit)]
    internal struct XGameStreamingTouchControlsStateValue
    {
        [FieldOffset(0)]
        internal XGameStreamingTouchControlsStateValueKind valueKind;

        [FieldOffset(8)]
        internal IntPtr stringValue;

        [FieldOffset(8)]
        internal double doubleValue;

        [FieldOffset(8)]
        internal bool boolValue;

        [FieldOffset(8)]
        internal UInt32 integerValue;
    }

    // struct XGameStreamingTouchControlsStateOperation
    // {
    //     XGameStreamingTouchControlsStateOperationKind operationKind;
    //     const char* path;
    //     XGameStreamingTouchControlsStateValue value;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XGameStreamingTouchControlsStateOperation
    {
        internal XGameStreamingTouchControlsStateOperationKind operationKind;
        [MarshalAs(UnmanagedType.LPStr)] internal string path;
        internal XGameStreamingTouchControlsStateValue value;
    }

    // struct RECT
    // {
    //     LONG    left;
    //     LONG    top;
    //     LONG    right;
    //     LONG    bottom;
    // }
    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        internal UInt32 left;
        internal UInt32 top;
        internal UInt32 right;
        internal UInt32 bottom;
    }

    // struct XGameStreamingDisplayDetails
    // {
    //     uint32_t preferredWidth;
    //     uint32_t preferredHeight;
    //     RECT safeArea;
    //     uint32_t maxPixels;
    //     uint32_t maxWidth;
    //     uint32_t maxHeight;
    //     XGameStreamingVideoFlags flags;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XGameStreamingDisplayDetails
    {
        internal UInt32 preferredWidth;
        internal UInt32 preferredHeight;
        internal RECT safeArea;
        internal UInt32 maxPixels;
        internal UInt32 maxWidth;
        internal UInt32 maxHeight;
        internal XGameStreamingVideoFlags flags;
    };

    internal class XGameStreamingConnectionStateChangedToken : XRegistrationToken<Interop.XGameStreamingConnectionStateChangedCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XGameStreamingConnectionStateChangedCallback))]
        static void OnConnectionStateChanged(IntPtr context, Interop.XGameStreamingClientId client, XGameStreamingConnectionState state)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XGameStreamingConnectionStateChangedCallback>;
            wrapper.Callback(wrapper.Context, client, state);
        }

        public XGameStreamingConnectionStateChangedToken(Interop.XGameStreamingConnectionStateChangedCallback callback, IntPtr context) :
            base(callback, context, OnConnectionStateChanged)
        { }

        public bool Unregister(bool wait)
        {
            bool result = true;
            if (this.Token != 0)
            {
                result = NativeMethods.XGameStreamingUnregisterConnectionStateChanged(this.Token, wait);
                this.Token = 0;
            }
            return result;
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister(wait: true);
        }
    }

    internal class XGameStreamingRegisterClientPropertiesChangedToken : XRegistrationToken<Interop.XGameStreamingClientPropertiesChangedCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XGameStreamingClientPropertiesChangedCallback))]
        static void OnClientPropertiesChanged(IntPtr context,
            Interop.XGameStreamingClientId client,
            UInt32 updatedPropertiesCount,
            XGameStreamingClientProperty[] updatedProperties)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XGameStreamingClientPropertiesChangedCallback>;
            wrapper.Callback(wrapper.Context, client, updatedPropertiesCount, updatedProperties);
        }

        private Interop.XGameStreamingClientId clientId;

        public XGameStreamingRegisterClientPropertiesChangedToken(Interop.XGameStreamingClientId clientId,
            Interop.XGameStreamingClientPropertiesChangedCallback callback,
            IntPtr context) : base(callback, context, OnClientPropertiesChanged)
        {
            this.clientId = clientId;
        }

        public bool Unregister(Interop.XGameStreamingClientId clientId, bool wait)
        {
            bool result = true;
            if (this.Token != 0)
            {
                result = NativeMethods.XGameStreamingUnregisterClientPropertiesChanged(clientId, this.Token, wait);
                this.Token = 0;
            }
            return result;
        }

        public bool Unregister(bool wait)
        {
            return Unregister(this.clientId, wait);
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister(wait: true);
        }
    }

    // typedef void CALLBACK XGameStreamingClientPropertiesChangedCallback(
    //     _In_ void* context,
    //     _In_ XGameStreamingClientId client,
    //     _In_ uint32_t updatedPropertiesCount,
    //     _In_reads_(updatedPropertiesCount) XGameStreamingClientProperty* updatedProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void XGameStreamingClientPropertiesChangedCallback(IntPtr context,
        XGameStreamingClientId client,
        UInt32 updatedPropertiesCount,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] XGameStreamingClientProperty[] updatedProperties);

    // typedef void CALLBACK XGameStreamingConnectionStateChangedCallback(
    //     _In_opt_ void* context,
    //     _In_ XGameStreamingClientId client,
    //     _In_ XGameStreamingConnectionState state);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void XGameStreamingConnectionStateChangedCallback(IntPtr context, XGameStreamingClientId client, XGameStreamingConnectionState state);


    partial class NativeMethods
    {
        // STDAPI XGameStreamingInitialize() noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingInitialize();

        // STDAPI_(void) XGameStreamingUninitialize() noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XGameStreamingUninitialize();

        // STDAPI_(bool) XGameStreamingIsStreaming() noexcept;
        [return: MarshalAs(UnmanagedType.I1)]
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool XGameStreamingIsStreaming();

        // STDAPI_(uint32_t) XGameStreamingGetClientCount() noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern UInt32 XGameStreamingGetClientCount();

        // STDAPI XGameStreamingGetClients(
        //     _In_ uint32_t clientCount,
        //     _Out_writes_to_(clientCount, *clientsUsed) XGameStreamingClientId* clients,
        //     _Out_ uint32_t* clientsUsed) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingGetClients(UInt32 clientCount,
            [Out, In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] XGameStreamingClientId[] clients,
            out UInt32 clientUsed);

        // STDAPI_(XGameStreamingConnectionState) XGameStreamingGetConnectionState(
        //     _In_ XGameStreamingClientId client
        // ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern XGameStreamingConnectionState XGameStreamingGetConnectionState(XGameStreamingClientId client);

        // STDAPI XGameStreamingRegisterConnectionStateChanged(
        //     _In_opt_ XTaskQueueHandle queue,
        //     _In_opt_ void* context,
        //     _In_ XGameStreamingConnectionStateChangedCallback* callback,
        //     _Out_ XTaskQueueRegistrationToken* token
        // ) noexcept;
        // [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingRegisterConnectionStateChanged(IntPtr queue, IntPtr context, XGameStreamingConnectionStateChangedCallback callback, out UInt64 token);

        // STDAPI_(bool) XGameStreamingUnregisterConnectionStateChanged(
        //     _In_ XTaskQueueRegistrationToken token,
        //     bool wait) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XGameStreamingUnregisterConnectionStateChanged(UInt64 token, [MarshalAs(UnmanagedType.I1)] bool wait);


        // STDAPI_(void) XGameStreamingHideTouchControls() noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XGameStreamingHideTouchControls();

        // STDAPI_(void) XGameStreamingHideTouchControlsOnClient(_In_ XGameStreamingClientId client) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XGameStreamingHideTouchControlsOnClient(XGameStreamingClientId client);

        // STDAPI_(void) XGameStreamingShowTouchControlLayout(_In_opt_z_ const char* layout) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XGameStreamingShowTouchControlLayout([MarshalAs(UnmanagedType.LPStr)] string layout);

        // STDAPI_(void) XGameStreamingShowTouchControlLayoutOnClient(
        //     _In_ XGameStreamingClientId client,
        //     _In_opt_z_ const char* layout) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XGameStreamingShowTouchControlLayoutOnClient(XGameStreamingClientId client, [MarshalAs(UnmanagedType.LPStr)] string layout);

        // STDAPI XGameStreamingRegisterClientPropertiesChanged(
        //     _In_ XGameStreamingClientId client,
        //     _In_opt_ XTaskQueueHandle queue,
        //     _In_opt_ void* context,
        //     _In_ XGameStreamingClientPropertiesChangedCallback* callback,
        //     _Out_ XTaskQueueRegistrationToken* token) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingRegisterClientPropertiesChanged(XGameStreamingClientId client, IntPtr queue,
            IntPtr context, XGameStreamingClientPropertiesChangedCallback callback, out UInt64 token);

        // STDAPI_(bool) XGameStreamingUnregisterClientPropertiesChanged(
        //     _In_ XGameStreamingClientId client,
        //     _In_ XTaskQueueRegistrationToken token,
        //     _In_ bool wait) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XGameStreamingUnregisterClientPropertiesChanged(XGameStreamingClientId client, UInt64 token, [MarshalAs(UnmanagedType.I1)] bool wait);

        // STDAPI XGameStreamingGetStreamPhysicalDimensions(
        //     _In_ XGameStreamingClientId client,
        //     _Out_ uint32_t* horizontalMm,
        //     _Out_ uint32_t* verticalMm) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingGetStreamPhysicalDimensions(XGameStreamingClientId client, out UInt32 horizontalMm, out UInt32 verticalMm);

        // STDAPI XGameStreamingGetStreamAddedLatency(
        //     _In_ XGameStreamingClientId client,
        //     _Out_ uint32_t* averageInputLatencyUs,
        //     _Out_ uint32_t* averageOutputLatencyUs,
        //     _Out_ uint32_t* standardDeviationUs) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingGetStreamAddedLatency(XGameStreamingClientId client, out UInt32 averageInputLatencyUs,
            out UInt32 averageOutputLatencyUs, out UInt32 standardDeviationUs);

        // STDAPI_(size_t) XGameStreamingGetServerLocationNameSize() noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern UInt64 XGameStreamingGetServerLocationNameSize();

        // STDAPI XGameStreamingGetServerLocationName(_In_ size_t serverLocationNameSize, _Out_writes_z_(serverLocationNameSize) char* serverLocationName) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingGetServerLocationName(UInt64 serverLocationNameSize, StringBuilder serverLocationName);

        // STDAPI XGameStreamingIsTouchInputEnabled(
        //     _In_ XGameStreamingClientId client,
        //     _Out_ bool* touchInputEnabled) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingIsTouchInputEnabled(XGameStreamingClientId client, [MarshalAs(UnmanagedType.I1)] out bool touchInputEnabled);

        // STDAPI XGameStreamingGetLastFrameDisplayed(
        //     _In_ XGameStreamingClientId client,
        //     _Out_ D3D12XBOX_FRAME_PIPELINE_TOKEN* framePipelineToken) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingGetLastFrameDisplayed(XGameStreamingClientId client, out UInt64 framePipelineToken);

        // STDAPI XGameStreamingGetAssociatedFrame(_In_ IGameInputReading* gamepadReading, _Out_ D3D12XBOX_FRAME_PIPELINE_TOKEN* framePipelineToken) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingGetAssociatedFrame(IntPtr gamepadReading, out UInt64 framePipelineToken);

        // STDAPI XGameStreamingGetGamepadPhysicality(
        //     _In_ IGameInputReading* gamepadReading,
        //     _Out_ XGameStreamingGamepadPhysicality* gamepadPhysicality) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingGetGamepadPhysicality(IntPtr gamepadReading, out XGameStreamingGamepadPhysicality gamepadPhysicality);

        // STDAPI XGameStreamingUpdateTouchControlsState(
        //     _In_ size_t operationCount,
        //     _In_reads_opt_(operationCount) const XGameStreamingTouchControlsStateOperation* operations) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingUpdateTouchControlsState(UInt64 operationCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] XGameStreamingTouchControlsStateOperation[] operations);

        // STDAPI XGameStreamingUpdateTouchControlsStateOnClient(
        //     _In_ XGameStreamingClientId client,
        //     _In_ size_t operationCount,
        //     _In_reads_opt_(operationCount) const XGameStreamingTouchControlsStateOperation* operations) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingUpdateTouchControlsStateOnClient(XGameStreamingClientId client, UInt64 operationCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] XGameStreamingTouchControlsStateOperation[] operations);

        // STDAPI XGameStreamingShowTouchControlsWithStateUpdate(
        //     _In_opt_z_ const char* layout,
        //     _In_ size_t operationCount,
        //     _In_reads_opt_(operationCount) const XGameStreamingTouchControlsStateOperation* operations) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingShowTouchControlsWithStateUpdate([MarshalAs(UnmanagedType.LPStr)] string layout,
            UInt64 operatoinCount, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] XGameStreamingTouchControlsStateOperation[] operations);

        // STDAPI XGameStreamingShowTouchControlsWithStateUpdateOnClient(
        //     _In_ XGameStreamingClientId client,
        //     _In_opt_z_ const char* layout,
        //     _In_ size_t operationCount,
        //     _In_reads_opt_(operationCount) const XGameStreamingTouchControlsStateOperation* operations) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingShowTouchControlsWithStateUpdateOnClient(XGameStreamingClientId client, [MarshalAs(UnmanagedType.LPStr)] string layout,
            UInt64 operationCount, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] XGameStreamingTouchControlsStateOperation[] operations);


        // STDAPI_(size_t) XGameStreamingGetTouchBundleVersionNameSize(_In_ XGameStreamingClientId client) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern UInt64 XGameStreamingGetTouchBundleVersionNameSize(XGameStreamingClientId client);

        // STDAPI XGameStreamingGetTouchBundleVersion(
        //     _In_ XGameStreamingClientId client,
        //     _Out_opt_ XVersion* version,
        //     _In_ size_t versionNameSize,
        //     _Out_writes_opt_z_(versionNameSize) char* versionName ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingGetTouchBundleVersion(XGameStreamingClientId client, out XVersion version,
            UInt64 versionNameSize, StringBuilder versionName);


        // STDAPI XGameStreamingGetClientIPAddress(
        //     _In_ XGameStreamingClientId client,
        //     _In_ size_t ipAddressSize,
        //     _Out_writes_z_(ipAddressSize) char* ipAddress) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingGetClientIPAddress(XGameStreamingClientId client, UInt64 ipAddressSize,
            StringBuilder ipAddress);

        // STDAPI XGameStreamingGetSessionId(
        //     _In_ XGameStreamingClientId client,
        //     _In_ size_t sessionIdSize,
        //     _Out_writes_bytes_to_(sessionIdSize, *sessionIdUsed) char* sessionId,
        //     _Out_opt_ size_t* sessionIdUsed) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingGetSessionId(XGameStreamingClientId client, UInt64 sessionIdSize,
            StringBuilder sessionId, out UInt64 sessionIdUsed);

        // STDAPI XGameStreamingGetDisplayDetails(
        //     _In_ XGameStreamingClientId client,
        //     _In_ uint32_t maxSupportedPixels,
        //     _In_ float widestSupportedAspectRatio,
        //     _In_ float tallestSupportedAspectRatio,
        //     _Out_ XGameStreamingDisplayDetails* displayDetails) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingGetDisplayDetails(XGameStreamingClientId clientId, UInt32 maxSupportedPixels, float widestSupportedAspectRatio,
            float tallestSupportedAspectRatio, out XGameStreamingDisplayDetails displayDetails);

        // STDAPI XGameStreamingSetResolution(_In_ uint32_t width, _In_ uint32_t height) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameStreamingSetResolution(UInt32 width, UInt32 height);
    }
}
