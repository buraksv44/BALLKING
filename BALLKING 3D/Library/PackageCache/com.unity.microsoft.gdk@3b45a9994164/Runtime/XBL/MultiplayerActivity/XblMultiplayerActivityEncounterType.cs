using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblMultiplayerActivityEncounterType : uint32_t
    //{
    //    Default = 0,
    //    Teammate = 1,
    //    Opponent = 2
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblMultiplayerActivityEncounterType
    {
        Default = 0,
        Teammate = 1,
        Opponent = 2
    }
}
