using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblPresenceMediaIdType : UInt32
    {
        /// <summary>Unknown media Id.</summary>
        Unknown,

        /// <summary>Bing media Id.</summary>
        Bing,

        /// <summary>MediaProvider media Id.</summary>
        MediaProvider
    }
}
