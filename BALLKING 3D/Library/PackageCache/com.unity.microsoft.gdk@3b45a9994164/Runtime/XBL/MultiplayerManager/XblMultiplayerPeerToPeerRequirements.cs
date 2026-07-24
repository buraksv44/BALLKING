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
    public class XblMultiplayerPeerToPeerRequirements
    {
        internal XblMultiplayerPeerToPeerRequirements(Interop.XblMultiplayerPeerToPeerRequirements interopStruct)
        {
            this.LatencyMaximum = interopStruct.LatencyMaximum;
            this.BandwidthMinimumInKbps = interopStruct.BandwidthMinimumInKbps;
        }

        public UInt64 LatencyMaximum { get; }
        public UInt64 BandwidthMinimumInKbps { get; }
    }
}
