using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblCreateMatchTicketResponse
    //{
    //    _Field_z_ char matchTicketId[XBL_SCID_LENGTH];
    //    int64_t estimatedWaitTime;
    //}
    //XblCreateMatchTicketResponse;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblCreateMatchTicketResponse
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = XblInterop.XBL_SCID_LENGTH)]
        internal Byte[] matchTicketId;

        internal Int64 estimatedWaitTime;
    }
}
