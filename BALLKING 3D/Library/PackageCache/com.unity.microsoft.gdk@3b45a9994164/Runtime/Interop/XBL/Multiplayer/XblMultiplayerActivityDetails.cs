using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblMultiplayerActivityDetails
    //{
    //    XblMultiplayerSessionReference SessionReference;
    //    char HandleId[XBL_GUID_LENGTH];
    //    uint32_t TitleId;
    //    XblMultiplayerSessionVisibility Visibility;
    //    XblMultiplayerSessionRestriction JoinRestriction;
    //    bool Closed;
    //    uint64_t OwnerXuid;
    //    uint32_t MaxMembersCount;
    //    uint32_t MembersCount;
    //    const char* CustomSessionPropertiesJson;
    //}
    //XblMultiplayerActivityDetails;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblMultiplayerActivityDetails
    {
        internal XblMultiplayerSessionReference SessionReference;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = XblInterop.XBL_GUID_LENGTH)]
        internal Byte[] HandleId;
        internal UInt32 TitleId;
        internal XblMultiplayerSessionVisibility Visibility;
        internal XblMultiplayerSessionRestriction JoinRestriction;
        [MarshalAs(UnmanagedType.U1)]
        internal bool Closed;
        internal UInt64 OwnerXuid;
        internal UInt32 MaxMembersCount;
        internal UInt32 MembersCount;
        internal UTF8StringPtr CustomSessionPropertiesJson;

        internal string GetHandleId() { unsafe { fixed (Byte* ptr = this.HandleId) { return Converters.BytePointerToString(ptr, XblInterop.XBL_GUID_LENGTH); } } }
    }
}
