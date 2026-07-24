using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblTitleManagedStatType : uint32_t
    //{
    //    Number,
    //    String
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblTitleManagedStatType : UInt32
    {
        Number = 0,
        String = 1,
    }
}
