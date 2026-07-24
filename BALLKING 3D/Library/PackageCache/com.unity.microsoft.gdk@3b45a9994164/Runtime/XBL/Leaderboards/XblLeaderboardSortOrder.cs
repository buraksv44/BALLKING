using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblLeaderboardSortOrder : UInt32
    {
        /// <summary>
        /// Sorting the leaderboard highest to lowest.
        /// </summary>
        Descending,

        /// <summary>
        /// Sorting the leaderboard lowest to highest.
        /// </summary>
        Ascending
    }
}
