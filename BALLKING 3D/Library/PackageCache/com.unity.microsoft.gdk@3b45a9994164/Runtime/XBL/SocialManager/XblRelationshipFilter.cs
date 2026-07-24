using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblRelationshipFilter : UInt32
    {
        /// <summary>Unknown</summary>
        Unknown,

        /// <summary>Friends of the user (user is following)</summary>
        Friends,

        /// <summary>Favorites of the user</summary>
        Favorite
    }
}
