using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblTournamentRegistrationReason : uint32_t
    //{
    //    Unknown,
    //    RegistrationClosed,
    //    MemberAlreadyRegistered,
    //    TournamentFull,
    //    TeamEliminated,
    //    TournamentCompleted
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblTournamentRegistrationReason : UInt32
    {
        Unknown = 0,
        RegistrationClosed = 1,
        MemberAlreadyRegistered = 2,
        TournamentFull = 3,
        TeamEliminated = 4,
        TournamentCompleted = 5,
    }
}
