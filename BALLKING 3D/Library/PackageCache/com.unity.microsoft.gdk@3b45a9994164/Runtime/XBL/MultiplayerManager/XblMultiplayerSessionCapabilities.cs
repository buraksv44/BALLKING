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
    public class XblMultiplayerSessionCapabilities
    {
        internal XblMultiplayerSessionCapabilities(Interop.XblMultiplayerSessionCapabilities interopStruct)
        {
            this.Connectivity = interopStruct.Connectivity.Value;
            this.Team = interopStruct.Team.Value;
            this.Arbitration = interopStruct.Arbitration.Value;
            this.SuppressPresenceActivityCheck = interopStruct.SuppressPresenceActivityCheck.Value;
            this.Gameplay = interopStruct.Gameplay.Value;
            this.Large = interopStruct.Large.Value;
            this.ConnectionRequiredForActiveMembers = interopStruct.ConnectionRequiredForActiveMembers.Value;
            this.UserAuthorizationStyle = interopStruct.UserAuthorizationStyle.Value;
            this.Crossplay = interopStruct.Crossplay.Value;
            this.Searchable = interopStruct.Searchable.Value;
            this.HasOwners = interopStruct.HasOwners.Value;
        }

        public bool Connectivity { get; }
        public bool Team { get; }
        public bool Arbitration { get; }
        public bool SuppressPresenceActivityCheck { get; }
        public bool Gameplay { get; }
        public bool Large { get; }
        public bool ConnectionRequiredForActiveMembers { get; }
        public bool UserAuthorizationStyle { get; }
        public bool Crossplay { get; }
        public bool Searchable { get; }
        public bool HasOwners { get; }
    }
}
