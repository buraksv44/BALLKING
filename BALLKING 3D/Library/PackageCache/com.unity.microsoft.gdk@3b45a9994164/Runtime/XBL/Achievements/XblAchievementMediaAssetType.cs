using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblAchievementMediaAssetType : UInt32
    {
        /// <summary>The media asset type is unknown.</summary>
        Unknown,

        /// <summary>An icon media asset.</summary>
        Icon,

        /// <summary>An art media asset.</summary>
        Art
    }
}
