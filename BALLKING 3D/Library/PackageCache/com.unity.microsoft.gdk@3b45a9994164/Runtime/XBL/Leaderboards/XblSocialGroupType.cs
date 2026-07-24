using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblSocialGroupType : UInt32
    {
        /// <summary>
        /// No social group.
        /// </summary>
        None,

        /// <summary>
        /// Social group for the people followed.
        /// </summary>
        People,

        /// <summary>
        /// Social group for the people tagged as favorites.
        /// </summary>
        Favorites
    }
}
