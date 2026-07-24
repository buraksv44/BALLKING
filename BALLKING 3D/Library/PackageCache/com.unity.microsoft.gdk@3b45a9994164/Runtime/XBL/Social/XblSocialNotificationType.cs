using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime

{
    //enum class XblSocialNotificationType : uint32_t
    //{
    //    Unknown,
    //    Added,
    //    Changed,
    //    Removed
    //};

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblSocialNotificationType : UInt32
    {
        Unknown = 0,
        Added,
        Changed,
        Removed
    }
}
