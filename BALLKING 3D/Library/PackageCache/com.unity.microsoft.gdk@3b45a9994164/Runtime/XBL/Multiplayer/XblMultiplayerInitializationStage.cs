using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblMultiplayerInitializationStage : uint32_t
    //{
    //    Unknown,
    //    None,
    //    Joining,
    //    Measuring,
    //    Evaluating,
    //    Failed
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblMultiplayerInitializationStage : UInt32
    {
        Unknown,
        None,
        Joining,
        Measuring,
        Evaluating,
        Failed
    }
}
