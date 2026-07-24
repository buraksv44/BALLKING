// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public delegate void XGameProtocolActivationCallback(IntPtr context, string protocolUri);

    public class XGameProtocolActivationToken
    {
        internal Interop.XGameProtocolActivationToken interop { get; private set; }

        internal XGameProtocolActivationToken(Interop.XGameProtocolActivationCallback callback, IntPtr context)
        {
            interop = new Interop.XGameProtocolActivationToken(callback, context);
        }

        public UInt64 Token
        {
            get => interop.Token;
            set => interop.Token = value;
        }

        public bool IsValid { get { return interop.IsValid; } }

        public bool Unregister(bool wait)
        {
            return interop.Unregister(wait);
        }
    };

    partial class SDK
    {
        public static Int32 XGameProtocolRegisterForActivation(XTaskQueueHandle queue,
            IntPtr context,
            XGameProtocolActivationCallback callback,
            out XGameProtocolActivationToken token)
        {
            Interop.XGameProtocolActivationCallback callbackInterop = (IntPtr context, string protocolUri) =>
            {
                callback(context, protocolUri);
            };

            token = new XGameProtocolActivationToken(callbackInterop, context);

            UInt64 tokenValue;
            IntPtr interopQueue = (queue != null) ? queue.Handle : IntPtr.Zero;
            int hr = NativeMethods.XGameProtocolRegisterForActivation(interopQueue,
                token.interop.CallbackContext,
                token.interop.StaticCallback,
                out tokenValue);
            if (HR.SUCCEEDED(hr))
            {
                token.Token = tokenValue;
            }
            else
            {
                token.interop.Dispose();
                token = null;
            }

            return hr;
        }

        public static bool XGameProtocolUnregisterForActivation(XGameProtocolActivationToken token,
            bool wait)
        {
            return token.Unregister(wait);
        }
    }
}
