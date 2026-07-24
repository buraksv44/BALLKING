using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblLeaderboardResult
    {
        internal XblLeaderboardResult(Interop.XblLeaderboardResult interopResult)
        {
            this.TotalRowCount = interopResult.totalRowCount;
            this.Columns = interopResult.GetColumns(c => new XblLeaderboardColumn(c));
            this.Rows = interopResult.GetRows(r => new XblLeaderboardRow(r));
            this.HasNext = interopResult.hasNext.Value;
            this.NextQuery = new XblLeaderboardQuery(interopResult.nextQuery);
        }

        public UInt32 TotalRowCount { get; private set; }
        public XblLeaderboardColumn[] Columns { get; private set; }
        public XblLeaderboardRow[] Rows { get; private set; }
        public bool HasNext { get; private set; }
        public XblLeaderboardQuery NextQuery { get; private set; }
    }
}
