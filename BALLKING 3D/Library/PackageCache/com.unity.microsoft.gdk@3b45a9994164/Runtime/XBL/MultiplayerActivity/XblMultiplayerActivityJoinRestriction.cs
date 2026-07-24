using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblMultiplayerActivityJoinRestriction : uint32_t
    //{
    //    Public = 0,
    //    InviteOnly = 1,
    //    Followed = 2
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblMultiplayerActivityJoinRestriction : UInt32
    {
        Public = 0,
        InviteOnly = 1,
        Followed = 2
    }
}
