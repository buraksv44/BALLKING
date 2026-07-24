using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblTitleStorageETagMatchCondition
    {
        /// <summary>
        /// There is no match condition.
        /// </summary>
        NotUsed,

        /// <summary>
        /// Perform the request if the Etag value specified matches the service value.
        /// </summary>
        IfMatch,

        /// <summary>
        /// Perform the request if the Etag value specified does not match the service value.
        /// </summary>
        IfNotMatch
    }
}
