using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblLeaderboardQueryType : uint32_t
    //{
    //    UserStatBacked = 0,
    //    TitleManagedStatBackedGlobal = 1,
    //    TitleManagedStatBackedSocial = 2,
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblLeaderboardQueryType : UInt32
    {
        /// <summary>
        /// A leaderboard based an event based user stat.
        /// </summary>
        UserStatBacked = 0,

        /// <summary>
        /// A global leaderboard backed by a title managed stat.
        /// </summary>
        TitleManagedStatBackedGlobal = 1,

        /// <summary>
        /// A social leaderboard backed by a title managed stat.
        /// </summary>
        TitleManagedStatBackedSocial = 2,
    }
}
