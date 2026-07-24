using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblMatchTicketDetailsResponse
    {
        internal XblMatchTicketDetailsResponse(Interop.XblMatchTicketDetailsResponse interopHandle)
        {
            this.MatchStatus = interopHandle.matchStatus;
            this.EstimatedWaitTime = interopHandle.estimatedWaitTime;
            this.PreserveSession = interopHandle.preserveSession;
            this.TicketSession = new XblMultiplayerSessionReference(interopHandle.ticketSession);
            this.TargetSession = new XblMultiplayerSessionReference(interopHandle.targetSession);
            this.TicketAttributes = interopHandle.ticketAttributes.GetString();
        }

        public XblTicketStatus MatchStatus { get; }
        public Int64 EstimatedWaitTime { get; }
        public XblPreserveSessionMode PreserveSession { get; }
        public XblMultiplayerSessionReference TicketSession { get; }
        public XblMultiplayerSessionReference TargetSession { get; }
        public string TicketAttributes { get; }
    }
}
