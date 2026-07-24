using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblAchievementProgressState : UInt32
    {
        /// <summary>Achievement progress is unknown.</summary>
        Unknown,

        /// <summary>Achievement has been earned.</summary>
        Achieved,

        /// <summary>Achievement progress has not been started.</summary>
        NotStarted,

        /// <summary>Achievement progress has started.</summary>
        InProgress
    }
}
