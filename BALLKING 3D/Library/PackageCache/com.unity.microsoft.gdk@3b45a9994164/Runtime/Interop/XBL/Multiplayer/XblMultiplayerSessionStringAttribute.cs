using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblMultiplayerSessionNumberAttribute
    //{
    //    char name[XBL_MULTIPLAYER_SEARCH_HANDLE_MAX_FIELD_LENGTH];
    //    double value;
    //}
    //XblMultiplayerSessionNumberAttribute;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblMultiplayerSessionStringAttribute
    {
        private unsafe fixed Byte name[XblInterop.XBL_MULTIPLAYER_SEARCH_HANDLE_MAX_FIELD_LENGTH];
        private unsafe fixed Byte value[XblInterop.XBL_MULTIPLAYER_SEARCH_HANDLE_MAX_FIELD_LENGTH];

        internal string GetName() { unsafe { fixed (Byte* ptr = this.name) { return Converters.BytePointerToString(ptr, XblInterop.XBL_MULTIPLAYER_SEARCH_HANDLE_MAX_FIELD_LENGTH); } } }
        internal string GetValue() { unsafe { fixed (Byte* ptr = this.value) { return Converters.BytePointerToString(ptr, XblInterop.XBL_MULTIPLAYER_SEARCH_HANDLE_MAX_FIELD_LENGTH); } } }

        internal XblMultiplayerSessionStringAttribute(Unity.XGamingRuntime.XblMultiplayerSessionStringAttribute publicObject)
        {
            unsafe
            {
                fixed (Byte* ptrName = this.name)
                {
                    Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.Name, ptrName, XblInterop.XBL_MULTIPLAYER_SEARCH_HANDLE_MAX_FIELD_LENGTH);
                }

                fixed (Byte* ptrValue = this.value)
                {
                    Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.Value, ptrValue, XblInterop.XBL_MULTIPLAYER_SEARCH_HANDLE_MAX_FIELD_LENGTH);
                }
            }
        }
    }
}