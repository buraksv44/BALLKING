using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblMultiplayerPeerToHostRequirements
    {
        internal XblMultiplayerPeerToHostRequirements(Interop.XblMultiplayerPeerToHostRequirements interopStruct)
        {
            this.LatencyMaximum = interopStruct.LatencyMaximum;
            this.BandwidthDownMinimumInKbps = interopStruct.BandwidthDownMinimumInKbps;
            this.BandwidthUpMinimumInKbps = interopStruct.BandwidthUpMinimumInKbps;
            this.HostSelectionMetric = interopStruct.HostSelectionMetric;
        }

        public UInt64 LatencyMaximum { get; }
        public UInt64 BandwidthDownMinimumInKbps { get; }
        public UInt64 BandwidthUpMinimumInKbps { get; }
        public XblMultiplayerMetrics HostSelectionMetric { get; }
    }
}
