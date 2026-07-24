using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblTitleStorageBlobType
    {
        /// <summary>
        /// Unknown blob type.
        /// </summary>
        Unknown,

        /// <summary>
        /// Binary blob type.
        /// </summary>
        Binary,

        /// <summary>
        /// JSON blob type.
        /// </summary>
        Json,

        /// <summary>
        /// Config blob type.
        /// </summary>
        Config
    }
}
