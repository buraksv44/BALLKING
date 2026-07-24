using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblPresenceUserState : UInt32
    {
        /// <summary>The state is unknown.</summary>
        Unknown,

        /// <summary>User is signed in to Xbox LIVE and active in a title.</summary>
        Online,

        /// <summary>User is signed-in to Xbox LIVE, but inactive in all titles.</summary>
        Away,

        /// <summary>User is not signed in to Xbox LIVE.</summary>
        Offline
    }
}
