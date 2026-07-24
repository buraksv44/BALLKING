using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblMultiplayerSessionStatus : uint32_t
    //{
    //    Unknown,
    //    Active,
    //    Inactive,
    //    Reserved
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblMultiplayerSessionStatus : UInt32
    {
        Unknown,
        Active,
        Inactive,
        Reserved
    }
}
