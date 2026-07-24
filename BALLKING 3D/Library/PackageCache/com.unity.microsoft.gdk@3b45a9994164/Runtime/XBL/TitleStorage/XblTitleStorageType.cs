using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblTitleStorageType
    {
        /// <summary>
        /// Per-user data storage such as game state or game settings that can be only be accessed by Xbox consoles.
        /// User restrictions can be configured to public or owner only in the service configuration.
        /// </summary>
        TrustedPlatformStorage,

        /// <summary>
        /// Global data storage.  This storage type is only writable via title configuration sites or Xbox Live developer tools.
        /// Any platform may read from this storage type.  Data could be rosters, maps, challenges, art resources, etc.
        /// </summary>
        GlobalStorage,

        /// <summary>
        /// Per-user data storage such as game state or game settings that can be accessed by Xbox consoles, Windows 10, and mobile devices.
        /// User restrictions can be configured to public or owner only in the service configuration.
        /// </summary>
        Universal
    }
}
