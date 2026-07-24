using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblAnonymousUserType : uint32_t
    //{
    //    Unknown = 0,
    //    CrossNetworkUser,
    //    CrossNetworkFriend
    //};

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblAnonymousUserType : UInt32
    {
        Unknown = 0,
        CrossNetworkUser,
        CrossNetworkFriend
    }
}
