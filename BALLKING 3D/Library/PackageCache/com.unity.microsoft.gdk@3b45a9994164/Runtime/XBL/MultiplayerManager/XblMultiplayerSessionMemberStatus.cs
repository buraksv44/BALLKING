using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblMultiplayerSessionMemberStatus : uint32_t
    //{
    //    Reserved,
    //    Inactive,
    //    Ready,
    //    Active
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblMultiplayerSessionMemberStatus : UInt32
    {
        Reserved = 0,
        Inactive = 1,
        Ready = 2,
        Active = 3,
    }
}
