using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblGuid
    //{
    //    _Null_terminated_ char value[XBL_GUID_LENGTH];
    //} XblGuid;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblGuid
    {
        private unsafe fixed Byte value[XblInterop.XBL_GUID_LENGTH]; // char value[XBL_GUID_LENGTH]

        internal string GetValue() { unsafe { fixed (Byte* ptr = this.value) { return Converters.BytePointerToString(ptr, XblInterop.XBL_GUID_LENGTH); } } }

        internal XblGuid(Unity.XGamingRuntime.XblGuid publicObject)
        {
            unsafe
            {
                fixed (Byte* ptr = this.value)
                {
                    Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.Value, ptr, XblInterop.XBL_GUID_LENGTH);
                }
            }
        }
    }
}