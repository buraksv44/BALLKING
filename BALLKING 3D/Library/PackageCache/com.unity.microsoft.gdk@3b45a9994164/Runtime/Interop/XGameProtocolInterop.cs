// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    internal class XGameProtocolActivationToken : XRegistrationToken<Interop.XGameProtocolActivationCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XGameProtocolActivationCallback))]
        static void OnProtocolActivation(IntPtr context, string protocolUri)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XGameProtocolActivationCallback>;
            wrapper.Callback(wrapper.Context, protocolUri);
        }

        public XGameProtocolActivationToken(Interop.XGameProtocolActivationCallback callback, IntPtr context) :
            base(callback, context, OnProtocolActivation)
        { }

        public bool Unregister(bool wait)
        {
            bool result = true;
            if (this.Token != 0)
            {
                result = NativeMethods.XGameProtocolUnregisterForActivation(this.Token, wait);
                this.Token = 0;
            }

            return result;
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister(wait: true);
        }
    }

    // typedef void CALLBACK XGameProtocolActivationCallback(
    //     _In_opt_ void* context,
    //     _In_ const char* protocolUri);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void XGameProtocolActivationCallback(IntPtr context, [MarshalAs(UnmanagedType.LPStr)] string protocolUri);

    partial class NativeMethods
    {
        // STDAPI XGameProtocolRegisterForActivation(
        //     _In_opt_ XTaskQueueHandle queue,
        //     _In_opt_ void* context,
        //     _In_ XGameProtocolActivationCallback * callback,
        //     _Out_ XTaskQueueRegistrationToken* token) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameProtocolRegisterForActivation(IntPtr queue,
            IntPtr context,
            XGameProtocolActivationCallback callback,
            out UInt64 token);

        // STDAPI_(bool) XGameProtocolUnregisterForActivation(
        //     _In_ XTaskQueueRegistrationToken token,
        //     _In_ bool wait) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XGameProtocolUnregisterForActivation(UInt64 token, [MarshalAs(UnmanagedType.I1)] bool wait);
    }
}
