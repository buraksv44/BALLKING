using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblLeaderboardStatType : UInt32
    {
        /// <summary>Unsigned 64 bit integer.</summary>
        Uint64,

        /// <summary>Boolean.</summary>
        Boolean,

        /// <summary>Double.</summary>
        Double,

        /// <summary>String.</summary>
        String,

        /// <summary>Unknown.</summary>
        Other
    }
}
