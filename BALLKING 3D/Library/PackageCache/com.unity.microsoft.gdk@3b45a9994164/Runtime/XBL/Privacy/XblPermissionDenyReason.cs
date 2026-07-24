using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblPermissionDenyReason : uint32_t
    //{
    //    Unknown = 0,
    //    NotAllowed = 2,
    //    MissingPrivilege = 3,
    //    PrivilegeRestrictsTarget = 4,
    //    BlockListRestrictsTarget = 5,
    //    MuteListRestrictsTarget = 7,
    //    PrivacySettingsRestrictsTarget = 9
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblPermissionDenyReason : UInt32
    {
        Unknown = 0,
        NotAllowed = 2,
        MissingPrivilege = 3,
        PrivilegeRestrictsTarget = 4,
        BlockListRestrictsTarget = 5,
        MuteListRestrictsTarget = 7,
        PrivacySettingsRestrictsTarget = 9,
    }
}
