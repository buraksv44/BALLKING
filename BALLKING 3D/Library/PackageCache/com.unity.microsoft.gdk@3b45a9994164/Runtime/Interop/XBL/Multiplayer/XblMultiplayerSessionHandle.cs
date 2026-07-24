using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblMultiplayerSession* XblMultiplayerSessionHandle;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblMultiplayerSessionHandle
    {
        internal readonly IntPtr intPtr;
    }
}
