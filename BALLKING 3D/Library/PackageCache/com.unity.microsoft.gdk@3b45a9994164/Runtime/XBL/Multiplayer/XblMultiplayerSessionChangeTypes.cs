using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblMultiplayerSessionChangeTypes : uint32_t
    //{
    //    None = 0x0000,
    //    Everything = 0x0001,
    //    HostDeviceTokenChange = 0x0002,
    //    InitializationStateChange = 0x0004,
    //    MatchmakingStatusChange = 0x0008,
    //    MemberListChange = 0x0010,
    //    MemberStatusChange = 0x0020,
    //    SessionJoinabilityChange = 0x0040,
    //    CustomPropertyChange = 0x0080,
    //    MemberCustomPropertyChange = 0x0100,
    //    TournamentPropertyChange = 0x0200,
    //    ArbitrationPropertyChange = 0x0400
    //};
    [Flags]
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblMultiplayerSessionChangeTypes : UInt32
    {
        None = 0x0000,
        Everything = 0x0001,
        HostDeviceTokenChange = 0x0002,
        InitializationStateChange = 0x0004,
        MatchmakingStatusChange = 0x0008,
        MemberListChange = 0x0010,
        MemberStatusChange = 0x0020,
        SessionJoinabilityChange = 0x0040,
        CustomPropertyChange = 0x0080,
        MemberCustomPropertyChange = 0x0100,
        TournamentPropertyChange = 0x0200,
        ArbitrationPropertyChange = 0x0400
    }
}
