using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblHopperStatisticsResponse
    //{
    //    _Field_z_ char* hopperName;
    //    int64_t estimatedWaitTime;
    //    uint32_t playersWaitingToMatch;
    //}
    //XblHopperStatisticsResponse;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblHopperStatisticsResponse
    {
        internal UTF8StringPtr hopperName;
        internal Int64 estimatedWaitTime;
        internal UInt32 playersWaitingToMatch;
    }
}
