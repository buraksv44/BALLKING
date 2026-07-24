using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    /// <summary>Enumeration values that specify the order we display the results in.</summary>
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblAchievementsManagerSortOrder : UInt32
    {
        /// <summary>Unsorted sort order will skip the sort operation.</summary>
        Unsorted = 0,

        /// <summary>Elements in the response are in ascending order of the field specified by XblAchievementsManagerSortValue.</summary>
        Ascending = 1,

        /// <summary>Elements in the response are in descending order of the field specified by XblAchievementsManagerSortValue.</summary>
        Descending = 2
    };
}
