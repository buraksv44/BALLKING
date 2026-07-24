using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblAchievementTimeWindow
    {
        internal XblAchievementTimeWindow(Interop.XblAchievementTimeWindow interopTimeWindow)
        {
            this.StartDate = interopTimeWindow.startDate.DateTime;
            this.EndDate = interopTimeWindow.endDate.DateTime;
        }

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
    }
}
