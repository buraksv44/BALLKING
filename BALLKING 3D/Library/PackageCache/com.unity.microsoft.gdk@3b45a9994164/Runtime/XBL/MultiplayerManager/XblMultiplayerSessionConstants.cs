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
    public class XblMultiplayerSessionConstants
    {
        internal XblMultiplayerSessionConstants(Interop.XblMultiplayerSessionConstants interopStruct)
        {
            this.MaxMembersInSession = interopStruct.MaxMembersInSession;
            this.Visibility = interopStruct.Visibility;
            this.InitiatorXuids = interopStruct.GetInitiatorXuids(x => x);
            this.CustomJson = interopStruct.CustomJson.GetString();
            this.SessionCloudComputePackageConstantsJson = interopStruct.SessionCloudComputePackageConstantsJson.GetString();
            this.MemberReservedTimeout = interopStruct.MemberReservedTimeout;
            this.MemberInactiveTimeout = interopStruct.MemberInactiveTimeout;
            this.MemberReadyTimeout = interopStruct.MemberReadyTimeout;
            this.SessionEmptyTimeout = interopStruct.SessionEmptyTimeout;
            this.ArbitrationTimeout = interopStruct.ArbitrationTimeout;
            this.ForfeitTimeout = interopStruct.ForfeitTimeout;
            this.EnableMetricsLatency = interopStruct.EnableMetricsLatency.Value;
            this.EnableMetricsBandwidthDown = interopStruct.EnableMetricsBandwidthDown.Value;
            this.EnableMetricsBandwidthUp = interopStruct.EnableMetricsBandwidthUp.Value;
            this.EnableMetricsCustom = interopStruct.EnableMetricsCustom.Value;
            this.MemberInitialization = interopStruct.GetMemberInitialization(x => new XblMultiplayerMemberInitialization(x));
            this.PeerToPeerRequirements = new XblMultiplayerPeerToPeerRequirements(interopStruct.PeerToPeerRequirements);
            this.PeerToHostRequirements = new XblMultiplayerPeerToHostRequirements(interopStruct.PeerToHostRequirements);
            this.MeasurementServerAddressesJson = interopStruct.MeasurementServerAddressesJson.GetString();
            this.ClientMatchmakingCapable = interopStruct.ClientMatchmakingCapable.Value;
            this.SessionCapabilities = new XblMultiplayerSessionCapabilities(interopStruct.SessionCapabilities);
        }

        public UInt32 MaxMembersInSession { get; }
        public XblMultiplayerSessionVisibility Visibility { get; }
        public UInt64[] InitiatorXuids { get; }
        public string CustomJson { get; }
        public string SessionCloudComputePackageConstantsJson { get; }
        public UInt64 MemberReservedTimeout { get; }
        public UInt64 MemberInactiveTimeout { get; }
        public UInt64 MemberReadyTimeout { get; }
        public UInt64 SessionEmptyTimeout { get; }
        public UInt64 ArbitrationTimeout { get; }
        public UInt64 ForfeitTimeout { get; }
        public bool EnableMetricsLatency { get; }
        public bool EnableMetricsBandwidthDown { get; }
        public bool EnableMetricsBandwidthUp { get; }
        public bool EnableMetricsCustom { get; }
        public XblMultiplayerMemberInitialization MemberInitialization { get; }
        public XblMultiplayerPeerToPeerRequirements PeerToPeerRequirements { get; }
        public XblMultiplayerPeerToHostRequirements PeerToHostRequirements { get; }
        public string MeasurementServerAddressesJson { get; }
        public bool ClientMatchmakingCapable { get; }
        public XblMultiplayerSessionCapabilities SessionCapabilities { get; }
    }
}
