using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblMultiplayerSessionHandleId
    //{
    //    _Null_terminated_ char value[XBL_GUID_LENGTH];
    //}
    //XblMultiplayerSessionHandleId;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblMultiplayerSessionHandleId
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = XblInterop.XBL_GUID_LENGTH)]
        internal Byte[] value;

        internal string GetValue() { unsafe { fixed (Byte* ptr = this.value) { return Converters.BytePointerToString(ptr, XblInterop.XBL_GUID_LENGTH); } } }
    }
}
