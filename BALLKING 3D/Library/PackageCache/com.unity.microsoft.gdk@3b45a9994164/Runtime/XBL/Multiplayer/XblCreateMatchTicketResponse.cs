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
    public class XblCreateMatchTicketResponse
    {
        internal XblCreateMatchTicketResponse(Interop.XblCreateMatchTicketResponse interopHandle)
        {
            this.MatchTicketId = Converters.ByteArrayToString(interopHandle.matchTicketId);
            this.EstimatedWaitTime = interopHandle.estimatedWaitTime;
        }

        public string MatchTicketId { get; }

        public Int64 EstimatedWaitTime { get; }
    }
}
