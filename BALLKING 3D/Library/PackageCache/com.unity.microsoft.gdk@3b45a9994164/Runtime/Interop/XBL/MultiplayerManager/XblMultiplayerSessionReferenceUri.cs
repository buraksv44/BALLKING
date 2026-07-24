using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblMultiplayerSessionReferenceUri
    //{
    //    _Null_terminated_ char value[XBL_MULTIPLAYER_SESSION_REFERENCE_URI_MAX_LENGTH];
    //}
    //XblMultiplayerSessionReferenceUri;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblMultiplayerSessionReferenceUri
    {
        private unsafe fixed Byte value[XblInterop.XBL_MULTIPLAYER_SESSION_REFERENCE_URI_MAX_LENGTH]; // char value[XBL_MULTIPLAYER_SESSION_REFERENCE_URI_MAX_LENGTH]

        internal string GetValue() { unsafe { fixed (Byte* ptr = this.value) { return Converters.BytePointerToString(ptr, XblInterop.XBL_MULTIPLAYER_SESSION_REFERENCE_URI_MAX_LENGTH); } } }

        internal XblMultiplayerSessionReferenceUri(Unity.XGamingRuntime.XblMultiplayerSessionReferenceUri publicObject)
        {
            unsafe
            {
                fixed (Byte* ptr = this.value)
                {
                    Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.Value, ptr, XblInterop.XBL_MULTIPLAYER_SESSION_REFERENCE_URI_MAX_LENGTH);
                }
            }
        }
    }
}