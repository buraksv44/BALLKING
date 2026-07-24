using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblLeaderboardColumn
    {
        internal XblLeaderboardColumn(Interop.XblLeaderboardColumn interopColumn)
        {
            this.StatName = interopColumn.statName.GetString();
            this.StatType = interopColumn.statType;
        }

        public string StatName { get; private set; }
        public XblLeaderboardStatType StatType { get; private set; }
    }
}
