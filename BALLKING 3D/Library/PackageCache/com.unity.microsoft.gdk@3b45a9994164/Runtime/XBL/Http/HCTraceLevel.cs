using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class HCTraceLevel : uint32_t
    //{
    //    Off = HC_PRIVATE_TRACE_LEVEL_OFF,
    //    Error = HC_PRIVATE_TRACE_LEVEL_ERROR,
    //    Warning = HC_PRIVATE_TRACE_LEVEL_WARNING,
    //    Important = HC_PRIVATE_TRACE_LEVEL_IMPORTANT,
    //    Information = HC_PRIVATE_TRACE_LEVEL_INFORMATION,
    //    Verbose = HC_PRIVATE_TRACE_LEVEL_VERBOSE,
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum HCTraceLevel
    {
        Off = 0,
        Error = 1,
        Warning = 2,
        Important = 3,
        Information = 4,
        Verbose = 5,
    }
}
