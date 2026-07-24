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
    public enum XblMultiplayerSessionWriteMode
    {
        /// <summary> Create a new multiplayer session. Fails if the session already exists. </summary>
        CreateNew,

        /// <summary> Either update or create a new session. Doesn't care whether the session exists. </summary>
        UpdateOrCreateNew,

        /// <summary> Updates an existing multiplayer session. Fails if the session doesn't exist. </summary>
        UpdateExisting,

        /// <summary> Updates an existing multiplayer session. Fails with HTTP_E_STATUS_PRECOND_FAILED (HTTP status 412) if eTag on local session doesn't match eTag on server. Fails if the session does not exist. </summary>
        SynchronizedUpdate,
    }
}
