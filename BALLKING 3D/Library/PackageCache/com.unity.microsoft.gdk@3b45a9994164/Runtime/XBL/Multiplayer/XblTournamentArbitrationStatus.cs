using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblTournamentArbitrationStatus : uint32_t
    //{
    //    Waiting,
    //    InProgress,
    //    Complete,
    //    Playing,
    //    Incomplete,
    //    Joining
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblTournamentArbitrationStatus : UInt32
    {
        Waiting = 0,
        InProgress = 1,
        Complete = 2,
        Playing = 3,
        Incomplete = 4,
        Joining = 5,
    }
}
