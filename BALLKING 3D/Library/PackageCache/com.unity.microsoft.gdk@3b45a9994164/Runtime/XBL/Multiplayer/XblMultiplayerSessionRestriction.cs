using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblMultiplayerSessionRestriction : uint32_t
    //{
    //    Unknown,
    //    None,
    //    Local,
    //    Followed
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblMultiplayerSessionRestriction : UInt32
    {
        Unknown = 0,
        None = 1,
        Local = 2,
        Followed = 3,
    }
}
