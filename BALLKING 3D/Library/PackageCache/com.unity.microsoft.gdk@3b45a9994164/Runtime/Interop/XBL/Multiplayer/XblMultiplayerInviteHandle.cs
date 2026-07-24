using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblMultiplayerInviteHandle
    //{
    //    _Null_terminated_ char Data[XBL_GUID_LENGTH];
    //}
    //XblMultiplayerInviteHandle;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblMultiplayerInviteHandle
    {
        private unsafe fixed Byte Data[XblInterop.XBL_GUID_LENGTH]; // char Data[XBL_GUID_LENGTH]

        internal string GetData() { unsafe { fixed (Byte* ptr = this.Data) { return Converters.BytePointerToString(ptr, XblInterop.XBL_GUID_LENGTH); } } }
    }
}
