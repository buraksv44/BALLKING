using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblMultiplayerSessionInitializationInfo
    //{
    //    XblMultiplayerInitializationStage Stage;
    //    time_t StageStartTime;
    //    uint32_t Episode;
    //}
    //XblMultiplayerSessionInitializationInfo;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblMultiplayerSessionInitializationInfo
    {
        internal XblMultiplayerInitializationStage Stage;
        internal TimeT StageStartTime;
        internal UInt32 Episode;
    }
}
