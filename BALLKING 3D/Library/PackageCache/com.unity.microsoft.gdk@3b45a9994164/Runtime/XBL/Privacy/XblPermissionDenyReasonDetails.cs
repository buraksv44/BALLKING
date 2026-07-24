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
    public class XblPermissionDenyReasonDetails
    {
        internal XblPermissionDenyReasonDetails(Interop.XblPermissionDenyReasonDetails interopStruct)
        {
            this.Reason = interopStruct.reason;
            this.RestrictedPrivilege = interopStruct.restrictedPrivilege;
            this.RestrictedPrivacySetting = interopStruct.restrictedPrivacySetting;
        }

        public XblPermissionDenyReason Reason { get; private set; }
        public XblPrivilege RestrictedPrivilege { get; private set; }
        public XblPrivacySetting RestrictedPrivacySetting { get; private set; }
    }
}
