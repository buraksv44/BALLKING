using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblSocialUserGroupType : UInt32
    {
        /// <summary>Social user group based off of filters</summary>
        FilterType,

        /// <summary>Social user group based off of list of users</summary>
        UserListType
    }
}
