using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblAchievementType : UInt32
    {
        /// <summary>The achievement type is unknown.</summary>
        Unknown,

        /// <summary>Gets all achievements regardless of type.</summary>
        All,

        /// <summary>A persistent achievement that may be unlocked at any time.
        /// Persistent achievements can give Gamerscore as a reward.</summary>
        Persistent,

        /// <summary>A challenge achievement that may only be unlocked within a certain time period.
        /// Challenge achievements can't give Gamerscore as a reward.</summary>
        Challenge
    }
}
