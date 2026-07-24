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
    public class XblMultiplayerActivityDetails
    {
        internal XblMultiplayerActivityDetails(Interop.XblMultiplayerActivityDetails interopHandle)
        {
            this.SessionReference = new XblMultiplayerSessionReference(interopHandle.SessionReference);
            this.HandleId = interopHandle.GetHandleId();
            this.TitleId = interopHandle.TitleId;
            this.Visibility = interopHandle.Visibility;
            this.JoinRestriction = interopHandle.JoinRestriction;
            this.Closed = interopHandle.Closed;
            this.OwnerXuid = interopHandle.OwnerXuid;
            this.MaxMembersCount = interopHandle.MaxMembersCount;
            this.MembersCount = interopHandle.MembersCount;
            this.CustomSessionPropertiesJson = interopHandle.CustomSessionPropertiesJson.GetString();
        }

        public XblMultiplayerSessionReference SessionReference { get; }
        public string HandleId { get; }
        public UInt32 TitleId { get; }
        public XblMultiplayerSessionVisibility Visibility { get; }
        public XblMultiplayerSessionRestriction JoinRestriction { get; }
        public bool Closed { get; }
        public UInt64 OwnerXuid { get; }
        public UInt32 MaxMembersCount { get; }
        public UInt32 MembersCount { get; }
        public string CustomSessionPropertiesJson { get; }
    }
}
