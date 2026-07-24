using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblAchievementParticipationType : UInt32
    {
        /// <summary>The participation type is unknown.</summary>
        Unknown,

        /// <summary>An achievement that can be earned as an individual participant.</summary>
        Individual,

        /// <summary>An achievement that can be earned as a group participant.</summary>
        Group
    }
}

