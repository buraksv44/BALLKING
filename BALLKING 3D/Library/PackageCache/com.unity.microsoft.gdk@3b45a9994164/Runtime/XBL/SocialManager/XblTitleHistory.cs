using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblTitleHistory
    {
        internal XblTitleHistory(Interop.XblTitleHistory interopTitleHistory)
        {
            this.HasUserPlayed = interopTitleHistory.hasUserPlayed;
            this.LastTimeUserPlayed = interopTitleHistory.lastTimeUserPlayed.DateTime;
        }

        public bool HasUserPlayed { get; private set; }
        public DateTime LastTimeUserPlayed { get; private set; }
    }
}
