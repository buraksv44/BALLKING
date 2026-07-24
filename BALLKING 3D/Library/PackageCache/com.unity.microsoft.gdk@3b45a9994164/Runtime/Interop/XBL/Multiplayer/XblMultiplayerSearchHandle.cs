using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblMultiplayerSearchHandleDetails* XblMultiplayerSearchHandle;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblMultiplayerSearchHandle
    {
        internal readonly IntPtr Ptr;
    }
}
