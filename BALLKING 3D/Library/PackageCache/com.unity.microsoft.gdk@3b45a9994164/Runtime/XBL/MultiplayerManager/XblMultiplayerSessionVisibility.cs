using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblMultiplayerSessionVisibility : uint32_t
    //{
    //    Unknown,
    //    Any,
    //    PrivateSession,
    //    Visible,
    //    Full,
    //    Open
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblMultiplayerSessionVisibility : UInt32
    {
        Unknown = 0,
        Any = 1,
        PrivateSession = 2,
        Visible = 3,
        Full = 4,
        Open = 5,
    }
}
