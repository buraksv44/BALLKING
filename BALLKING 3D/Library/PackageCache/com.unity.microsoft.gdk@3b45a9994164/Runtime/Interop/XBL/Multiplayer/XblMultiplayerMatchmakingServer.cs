using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblMultiplayerMatchmakingServer
    //{
    //    XblMatchmakingStatus Status;
    //    const char* StatusDetails;
    //    uint32_t TypicalWaitInSeconds;
    //    XblMultiplayerSessionReference TargetSessionRef;
    //} XblMultiplayerMatchmakingServer;

    [StructLayout(LayoutKind.Sequential)]
    internal struct XblMultiplayerMatchmakingServer
    {
        internal XblMatchmakingStatus Status;
        internal UTF8StringPtr StatusDetails;
        internal UInt32 TypicalWaitInSeconds;
        internal XblMultiplayerSessionReference TargetSessionRef;

    }
}
