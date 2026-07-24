using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblPreserveSessionMode : uint32_t
    //{
    //    Unknown,
    //    Always,
    //    Never
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblPreserveSessionMode
    {
        Unknown,
        Always,
        Never
    }
}
