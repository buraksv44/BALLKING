using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public delegate void XGameInviteEventCallback(IntPtr context, string inviteUri);

    public class XGameInviteRegistrationToken
    {
        internal Interop.XGameInviteRegistrationToken interop { get; private set; }

        internal XGameInviteRegistrationToken(Interop.XGameInviteEventCallback callback, IntPtr context)
        {
            interop = new Interop.XGameInviteRegistrationToken(callback, context);
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
        public static Int32 XGameInviteRegisterForEvent(XTaskQueueHandle handle,
            XGameInviteEventCallback callback,
            IntPtr context,
            out XGameInviteRegistrationToken token)
        {
            Interop.XGameInviteEventCallback callbackInterop = (IntPtr context, string inviteUri) =>
            {
                callback(context, inviteUri);
            };

            token = new XGameInviteRegistrationToken(callbackInterop, context);

            UInt64 tokenValue;
            IntPtr interopQueue = (handle != null) ? handle.Handle : IntPtr.Zero;
            int hr = NativeMethods.XGameInviteRegisterForEvent(interopQueue,
                token.interop.CallbackContext,
                token.interop.StaticCallback,
                out tokenValue);
            if (HR.SUCCEEDED(hr) && tokenValue != 0)
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

        public static Int32 XGameInviteRegisterForEvent(XTaskQueueHandle handle,
            XGameInviteEventCallback callback,
            out XGameInviteRegistrationToken token)
        {
            return XGameInviteRegisterForEvent(handle, callback, IntPtr.Zero, out token);
        }

        public static bool XGameInviteUnregisterForEvent(XGameInviteRegistrationToken token, bool wait)
        {
            return token.Unregister(wait);
        }

        public static bool XGameInviteUnregisterForEvent(XGameInviteRegistrationToken token)
        {
            return XGameInviteUnregisterForEvent(token, true);
        }
    }
}
