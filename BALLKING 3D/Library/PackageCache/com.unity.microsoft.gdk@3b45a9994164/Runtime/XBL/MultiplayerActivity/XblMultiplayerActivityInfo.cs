using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblMultiplayerActivityInfo
    {
        public XblMultiplayerActivityInfo()
        {
        }

        internal XblMultiplayerActivityInfo( Interop.XblMultiplayerActivityInfo interopStruct )
        {
            Xuid = interopStruct.xuid;
            ConnectionString = interopStruct.connectionString.GetString();
            JoinRestriction = interopStruct.joinRestriction;
            MaxPlayers = interopStruct.maxPlayers.ToUInt32();
            CurrentPlayers = interopStruct.currentPlayers.ToUInt32();
            GroupId = interopStruct.groupId.GetString();
            Platform = interopStruct.platform;
        }

        public UInt64 Xuid { get; set; }
        public string ConnectionString { get; set; }
        public XblMultiplayerActivityJoinRestriction JoinRestriction { get; set; }
        public UInt32 MaxPlayers { get; set; }
        public UInt32 CurrentPlayers { get; set; }
        public string GroupId { get; set; }
        public XblMultiplayerActivityPlatform Platform { get; set; }
    }
}
