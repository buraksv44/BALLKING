using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblMultiplayerEventArgsHandle
    {
        internal readonly IntPtr handle;
    }
}
