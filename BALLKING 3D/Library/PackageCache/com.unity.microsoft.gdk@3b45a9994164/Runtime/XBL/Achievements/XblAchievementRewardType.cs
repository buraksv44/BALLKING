using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblAchievementRewardType : UInt32
    {
        /// <summary>The reward type is unknown.</summary>
        Unknown,

        /// <summary>A Gamerscore reward.</summary>
        Gamerscore,

        /// <summary>An in-app reward, defined and delivered by the title.</summary>
        InApp,

        /// <summary>A digital art reward.</summary>
        Art
    }
}

