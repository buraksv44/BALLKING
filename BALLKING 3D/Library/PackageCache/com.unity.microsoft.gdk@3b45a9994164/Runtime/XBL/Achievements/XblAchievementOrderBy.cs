using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblAchievementOrderBy : UInt32
    {
        /// <summary>Default order does not guarantee sort order.</summary>
        DefaultOrder,

        /// <summary>Sort by title id.</summary>
        TitleId,

        /// <summary>Sort by achievement unlock time.</summary>
        UnlockTime
    }
}
