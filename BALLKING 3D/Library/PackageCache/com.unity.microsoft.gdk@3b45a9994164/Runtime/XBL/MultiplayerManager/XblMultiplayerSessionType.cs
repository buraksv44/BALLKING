using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblMultiplayerSessionType : uint32_t
    //{
    //    Unknown,
    //    LobbySession,
    //    GameSession,
    //    MatchSession
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblMultiplayerSessionType : UInt32
    {
        Unknown = 0,
        LobbySession = 1,
        GameSession = 2,
        MatchSession = 3,
    }
}
