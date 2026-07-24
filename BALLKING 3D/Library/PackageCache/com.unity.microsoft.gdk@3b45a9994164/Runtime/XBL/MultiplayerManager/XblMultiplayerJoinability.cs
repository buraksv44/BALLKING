using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblMultiplayerJoinability : uint32_t
    //{
    //    None,
    //    JoinableByFriends,
    //    InviteOnly,
    //    DisableWhileGameInProgress,
    //    Closed
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblMultiplayerJoinability : UInt32
    {
        None = 0,
        JoinableByFriends = 1,
        InviteOnly = 2,
        DisableWhileGameInProgress = 3,
        Closed = 4,
    }
}
