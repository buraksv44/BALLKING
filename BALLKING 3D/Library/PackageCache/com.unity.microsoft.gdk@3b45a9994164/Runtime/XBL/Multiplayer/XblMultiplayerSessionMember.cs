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
    public class XblMultiplayerSessionMember
    {
        internal XblMultiplayerSessionMember(Interop.XblMultiplayerSessionMember interopHandle)
        {
            this.InteropHandle = interopHandle;

            this.MemberId = interopHandle.MemberId;
            this.TeamId = interopHandle.TeamId.GetString();
            this.InitialTeam = interopHandle.InitialTeam.GetString();
            this.ArbitrationStatus = interopHandle.ArbitrationStatus;
            this.Xuid = interopHandle.Xuid;
            this.CustomConstantsJson = interopHandle.CustomConstantsJson.GetString();
            this.SecureDeviceBaseAddress64 = interopHandle.SecureDeviceBaseAddress64.GetString();
            this.Roles = interopHandle.GetRoles(r => new XblMultiplayerSessionMemberRole(r));
            this.CustomPropertiesJson = interopHandle.CustomPropertiesJson.GetString();
            this.Gamertag = interopHandle.GetGamertag();
            this.Status = interopHandle.Status;
            this.IsTurnAvailable = interopHandle.IsTurnAvailable;
            this.IsCurrentUser = interopHandle.IsCurrentUser;
            this.InitializeRequested = interopHandle.InitializeRequested;
            this.MatchmakingResultServerMeasurementsJson = interopHandle.MatchmakingResultServerMeasurementsJson.GetString();
            this.ServerMeasurementsJson = interopHandle.ServerMeasurementsJson.GetString();
            this.MembersInGroupIds = interopHandle.GetMembersInGroupIds(x => x);
            this.QosMeasurementsJson = interopHandle.QosMeasurementsJson.GetString();
            this.DeviceToken = new XblDeviceToken(interopHandle.DeviceToken);
            this.Nat = interopHandle.Nat;
            this.ActiveTitleId = interopHandle.ActiveTitleId;
            this.InitializationEpisode = interopHandle.InitializationEpisode;
            this.JoinTime = interopHandle.JoinTime.DateTime;
            this.InitializationFailureCause = interopHandle.InitializationFailureCause;
            this.Groups = interopHandle.GetGroups();
            this.Encounters = interopHandle.GetEncounters();
            this.TournamentTeamSessionReference = new XblMultiplayerSessionReference(interopHandle.TournamentTeamSessionReference);
        }

        public UInt32 MemberId { get; }
        public string TeamId { get; }
        public string InitialTeam { get; }
        public XblTournamentArbitrationStatus ArbitrationStatus { get; }
        public UInt64 Xuid { get; }
        public string CustomConstantsJson { get; }
        public string SecureDeviceBaseAddress64 { get; }
        public XblMultiplayerSessionMemberRole[] Roles { get; }
        public string CustomPropertiesJson { get; }
        public string Gamertag { get; }
        public XblMultiplayerSessionMemberStatus Status { get; }
        public bool IsTurnAvailable { get; }
        public bool IsCurrentUser { get; }
        public bool InitializeRequested { get; }
        public string MatchmakingResultServerMeasurementsJson { get; }
        public string ServerMeasurementsJson { get; }
        public UInt32[] MembersInGroupIds { get; }
        public string QosMeasurementsJson { get; }
        public XblDeviceToken DeviceToken { get; }
        public XblNetworkAddressTranslationSetting Nat { get; }
        public  UInt32 ActiveTitleId { get; }
        public  UInt32 InitializationEpisode { get; }
        public DateTime JoinTime { get; }
        public XblMultiplayerMeasurementFailure InitializationFailureCause { get; }
        public string[] Groups { get; }
        public string[] Encounters { get; }
        public XblMultiplayerSessionReference TournamentTeamSessionReference { get; }

        internal Interop.XblMultiplayerSessionMember InteropHandle { get; }
    }
}
