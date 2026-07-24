using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblRealTimeActivitySubscription* XblRealTimeActivitySubscriptionHandle;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblRealTimeActivitySubscriptionHandle
    {
        private readonly IntPtr intPtr;
    }
}
