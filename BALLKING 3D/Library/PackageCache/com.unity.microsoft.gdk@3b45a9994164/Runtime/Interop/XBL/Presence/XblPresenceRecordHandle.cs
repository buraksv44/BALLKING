using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblPresenceRecord* XblPresenceRecordHandle;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblPresenceRecordHandle
    {
        internal readonly IntPtr intPtr;
    }
}
