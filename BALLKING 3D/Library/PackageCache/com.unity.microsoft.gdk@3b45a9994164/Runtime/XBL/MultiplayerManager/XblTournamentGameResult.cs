using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblTournamentGameResult : uint32_t
    //{
    //    NoContest,
    //    Win,
    //    Loss,
    //    Draw,
    //    Rank,
    //    NoShow,
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblTournamentGameResult : UInt32
    {
        NoContest = 0,
        Win = 1,
        Loss = 2,
        Draw = 3,
        Rank = 4,
        NoShow = 5,
    }
}
