using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    // enum class XErrorOptions : uint32_t
    // {
    //     None                     = 0x00,
    //     OutputDebugStringOnError = 0x01,
    //     DebugBreakOnError        = 0x02,
    //     FailFastOnError          = 0x04,
    // };
    public enum XErrorOptions : UInt32
    {
        None = 0x00,
        OutputDebugStringOnError = 0x01,
        DebugBreakOnError = 0x02,
        FailFastOnError = 0x04,
    };

    public delegate bool XErrorCallback(Int32 hr, string msg, IntPtr context);

    partial class SDK
    {
        static private CallbackWrapper<Interop.XErrorCallback> errorCallbackWrapper;

        [AOT.MonoPInvokeCallback(typeof(Interop.XErrorCallback))]
        private static bool OnErrorCallback(Int32 hr, string msg, IntPtr context)
        {
            GCHandle handle = GCHandle.FromIntPtr(context);
            var wrapper = handle.Target as CallbackWrapper<Interop.XErrorCallback>;
            return wrapper.Callback(hr, msg, wrapper.Context);
        }

        public static void XErrorSetCallback(XErrorCallback callback, IntPtr context)
        {
            Interop.XErrorCallback callbackInterop = (Int32 hr, string msg, IntPtr context) =>
            {
                return callback(hr, msg, context);
            };

            if (errorCallbackWrapper != null)
            {
                errorCallbackWrapper.Dispose();
                errorCallbackWrapper = null;
            }

            if (callback != null)
            {
                errorCallbackWrapper = new CallbackWrapper<Interop.XErrorCallback>(callbackInterop, context, OnErrorCallback);
            }

            NativeMethods.XErrorSetCallback((errorCallbackWrapper != null) ? errorCallbackWrapper.StaticCallback : null,
                (errorCallbackWrapper != null) ? errorCallbackWrapper.CallbackContext : IntPtr.Zero);

        }

        public static void XErrorSetOptions(XErrorOptions optionsDebuggerPresent, XErrorOptions optionsDebuggerNotPresent)
        {
            NativeMethods.XErrorSetOptions(optionsDebuggerPresent, optionsDebuggerNotPresent);
        }
    }
}