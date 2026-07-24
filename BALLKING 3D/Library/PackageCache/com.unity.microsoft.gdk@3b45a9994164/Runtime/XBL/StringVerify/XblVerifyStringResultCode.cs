using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblVerifyStringResultCode : UInt32
    {
        /// <summary>No issues were found with the string.</summary>
        Success,

        /// <summary>The string contains offensive content.</summary>
        Offensive,

        /// <summary>The string is too long to verify.</summary>
        TooLong,

        /// <summary>An unknown error was encountered during string verification.</summary>
        UnknownError
    }
}
