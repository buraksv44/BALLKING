using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblHttpCallResponseBodyType : uint32_t
    //{
    //    String,
    //    Vector
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblHttpCallResponseBodyType : UInt32
    {
        String = 0,
        Vector = 1,
    }
}
