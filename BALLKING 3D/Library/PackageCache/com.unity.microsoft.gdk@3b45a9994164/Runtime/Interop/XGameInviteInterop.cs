using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    internal class XGameInviteRegistrationToken : XRegistrationToken<Interop.XGameInviteEventCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XGameInviteEventCallback))]
        static void OnInvite(IntPtr context, string inviteUri)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XGameInviteEventCallback>;
            wrapper.Callback(wrapper.Context, inviteUri);
        }

        internal XGameInviteRegistrationToken(Interop.XGameInviteEventCallback callback, IntPtr context) :
            base(callback, context, OnInvite)
        { }

        public bool Unregister(bool wait)
        {
            bool result = true;
            if (this.Token != 0)
            {
                result = NativeMethods.XGameInviteUnregisterForEvent(this.Token, wait);
                this.Token = 0;
            }

            return result;
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister(wait: true);
        }
    }

    // typedef void CALLBACK XGameInviteEventCallback(
    //     _In_opt_ void* context,
    //     _In_ const char* inviteUri
    // );
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void XGameInviteEventCallback(IntPtr context,
        [MarshalAs(UnmanagedType.LPStr)] string inviteUri);

    partial class NativeMethods
    {
        // STDAPI XGameInviteRegisterForEvent(
        //     _In_opt_ XTaskQueueHandle queue,
        //     _In_opt_ void* context,
        //     _In_ XGameInviteEventCallback* callback,
        //     _Out_ XTaskQueueRegistrationToken* token
        //     );
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XGameInviteRegisterForEvent(
            IntPtr queue,
            IntPtr context,
            XGameInviteEventCallback callback,
            out UInt64 token);

        // STDAPI_(bool) XGameInviteUnregisterForEvent(
        //     _In_ XTaskQueueRegistrationToken token,
        //     _In_ bool wait
        //     );
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XGameInviteUnregisterForEvent(UInt64 token,
            [MarshalAs(UnmanagedType.I1)] bool wait);
    }
}
