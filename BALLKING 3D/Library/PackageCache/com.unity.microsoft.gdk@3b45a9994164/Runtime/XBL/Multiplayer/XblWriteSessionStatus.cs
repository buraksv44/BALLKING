using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblWriteSessionStatus
    {
        /// <summary> Unknown write result. </summary>
        Unknown,

        /// <summary> HTTP Result 403- User does not have proper permission to write a session. </summary>
        AccessDenied,

        /// <summary> HTTP Result 201- Write created session successfully. </summary>
        Created,

        /// <summary> HTTP Result 409- Conflict occurred during write about session document. </summary>
        Conflict,

        /// <summary> HTTP Result 404- Session not found. </summary>
        HandleNotFound,

        /// <summary> HTTP Result 412- Session document is not the most recent. </summary>
        OutOfSync,

        /// <summary> HTTP Result 204- Session deleted successfully. </summary>
        SessionDeleted,

        /// <summary> HTTP Result 200- Session updated successfully. </summary>
        Updated
    }
}
