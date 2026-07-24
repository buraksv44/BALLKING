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
    public class XblMultiplayerManagerMember
    {
        internal XblMultiplayerManagerMember(Interop.XblMultiplayerManagerMember interopStruct)
        {
            this.MemberId = interopStruct.MemberId;
            this.TeamId = interopStruct.TeamId.GetString();
            this.InitialTeam = interopStruct.InitialTeam.GetString();
            this.Xuid = interopStruct.Xuid;
            this.DebugGamertag = interopStruct.DebugGamertag.GetString();
            this.IsLocal = interopStruct.IsLocal.Value;
            this.IsInLobby = interopStruct.IsInLobby.Value;
            this.IsInGame = interopStruct.IsInGame.Value;
            this.Status = interopStruct.Status;
            this.ConnectionAddress = interopStruct.ConnectionAddress.GetString();
            this.PropertiesJson = interopStruct.PropertiesJson.GetString();
            this.DeviceToken = interopStruct.DeviceToken.GetString();
        }

        public UInt32 MemberId { get; }
        public string TeamId { get; }
        public string InitialTeam { get; }
        public UInt64 Xuid { get; }
        public string DebugGamertag { get; }
        public bool IsLocal { get; }
        public bool IsInLobby { get; }
        public bool IsInGame { get; }
        public XblMultiplayerSessionMemberStatus Status { get; }
        public string ConnectionAddress { get; }
        public string PropertiesJson { get; }
        public string DeviceToken { get; }
    }
}
