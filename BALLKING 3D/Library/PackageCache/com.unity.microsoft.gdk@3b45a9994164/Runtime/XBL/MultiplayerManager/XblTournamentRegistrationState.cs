using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblTournamentRegistrationState : uint32_t
    //{
    //    Unknown,
    //    Pending,
    //    Withdrawn,
    //    Rejected,
    //    Registered,
    //    Completed
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblTournamentRegistrationState : UInt32
    {
        Unknown = 0,
        Pending = 1,
        Withdrawn = 2,
        Rejected = 3,
        Registered = 4,
        Completed = 5,
    }
}
