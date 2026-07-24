using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblSocialRelationshipFilter : uint32_t
    //{
    //    All,
    //    Favorite,
    //    LegacyXboxLiveFriends
    //};

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblSocialRelationshipFilter : UInt32
    {
        All = 0,
        Favorite,
        LegacyXboxLiveFriends
    }
}
