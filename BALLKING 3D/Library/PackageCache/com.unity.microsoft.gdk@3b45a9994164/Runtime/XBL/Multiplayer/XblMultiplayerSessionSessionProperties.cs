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
    public class XblMultiplayerSessionProperties
    {
        internal XblMultiplayerSessionProperties(Interop.XblMultiplayerSessionProperties interopHandle)
        {
            this.InteropHandle = interopHandle;

            this.Keywords = interopHandle.GetKeywords();
            this.JoinRestriction = interopHandle.JoinRestriction;
            this.ReadRestriction = interopHandle.ReadRestriction;
            this.TurnCollection = interopHandle.GetTurnCollection(x => x);
            this.MatchmakingTargetSessionConstantsJson = interopHandle.MatchmakingTargetSessionConstantsJson.GetString();
            this.SessionCustomPropertiesJson = interopHandle.SessionCustomPropertiesJson.GetString();
            this.MatchmakingServerConnectionString = interopHandle.MatchmakingServerConnectionString.GetString();
            this.ServerConnectionStringCandidates = interopHandle.GetServerConnectionStringCandidates();
            this.SessionOwnerMemberIds = interopHandle.GetSessionOwnerMemberIds(x => x);
            this.HostDeviceToken = new XblDeviceToken(interopHandle.HostDeviceToken);
            this.Closed = interopHandle.Closed;
            this.Locked = interopHandle.Locked;
            this.AllocateCloudCompute = interopHandle.AllocateCloudCompute;
            this.MatchmakingResubmit = interopHandle.MatchmakingResubmit;
        }

        public string[] Keywords { get; }
        public XblMultiplayerSessionRestriction JoinRestriction { get; }
        public XblMultiplayerSessionRestriction ReadRestriction { get; }
        public UInt32[] TurnCollection { get; }
        public string MatchmakingTargetSessionConstantsJson { get; }
        public string SessionCustomPropertiesJson { get; }
        public string MatchmakingServerConnectionString { get; }
        public string[] ServerConnectionStringCandidates { get; }
        public UInt32[] SessionOwnerMemberIds { get; }
        public XblDeviceToken HostDeviceToken { get; }
        public bool Closed { get; }
        public bool Locked { get; }
        public bool AllocateCloudCompute { get; }
        public bool MatchmakingResubmit { get; }

        internal Interop.XblMultiplayerSessionProperties InteropHandle { get; }
    }
}
