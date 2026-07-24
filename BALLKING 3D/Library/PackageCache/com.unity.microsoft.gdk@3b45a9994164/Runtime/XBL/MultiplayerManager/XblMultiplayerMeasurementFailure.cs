using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblMultiplayerMeasurementFailure : uint32_t
    //{
    //    Unknown,
    //    None,
    //    Timeout,
    //    Latency,
    //    BandwidthUp,
    //    BandwidthDown,
    //    Group,
    //    Network,
    //    Episode
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblMultiplayerMeasurementFailure : UInt32
    {
        Unknown = 0,
        None = 1,
        Timeout = 2,
        Latency = 3,
        BandwidthUp = 4,
        BandwidthDown = 5,
        Group = 6,
        Network = 7,
        Episode = 8,
    }
}
