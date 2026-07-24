using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblInitArgs
    //{
    //    /// <summary>
    //    /// Queue used for XSAPI internal asynchronous work (telemetry, rta, etc.).
    //    /// This field if optional - if not provided, a threadpool based queue will be used.
    //    /// </summary>
    //    XTaskQueueHandle queue;

    //#if !(HC_PLATFORM == HC_PLATFORM_XDK || HC_PLATFORM == HC_PLATFORM_UWP)
    //    /// <summary>
    //    /// The service configuration Id for the app.
    //    /// </summary>
    //    _Field_z_ const char* scid;
    //#endif
    //}

    [StructLayout(LayoutKind.Sequential)]
    internal struct XblInitArgs
    {
        public XTaskQueueHandle queue;

        public IntPtr scid;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct XblInitArgsPtr
    {
        internal XblInitArgsPtr(IntPtr intPtr)
        {
            this.IntPtr = intPtr;
        }

        internal readonly IntPtr IntPtr;
    }
}