using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblPresenceBroadcastProvider : UInt32
    {
        /// <summary>Unknown streaming provider.</summary>
        Unknown,

        /// <summary>Streaming using Twitch.</summary>
        Twitch
    }
}
