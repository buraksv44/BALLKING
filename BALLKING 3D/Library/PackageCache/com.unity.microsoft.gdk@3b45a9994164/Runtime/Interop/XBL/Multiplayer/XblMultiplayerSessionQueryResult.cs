using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblMultiplayerSessionQueryResult
    //{
    //    time_t StartTime;
    //    XblMultiplayerSessionReference SessionReference;
    //    XblMultiplayerSessionStatus Status;
    //    XblMultiplayerSessionVisibility Visibility;
    //    bool IsMyTurn;
    //    uint64_t Xuid;
    //    uint32_t AcceptedMemberCount;
    //    XblMultiplayerSessionRestriction JoinRestriction;
    //}
    //XblMultiplayerSessionQueryResult;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblMultiplayerSessionQueryResult
    {
        internal TimeT StartTime;
        internal XblMultiplayerSessionReference SessionReference;
        internal XblMultiplayerSessionStatus Status;
        internal XblMultiplayerSessionVisibility Visibility;
        [MarshalAs(UnmanagedType.U1)]
        internal bool IsMyTurn;
        internal UInt64 Xuid;
        internal UInt32 AcceptedMemberCount;
        internal XblMultiplayerSessionRestriction JoinRestriction;
    }
}
