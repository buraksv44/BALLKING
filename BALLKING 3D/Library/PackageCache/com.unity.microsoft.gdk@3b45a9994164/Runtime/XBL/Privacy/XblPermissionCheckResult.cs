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
    public class XblPermissionCheckResult
    {
        internal XblPermissionCheckResult(Interop.XblPermissionCheckResult interopStruct)
        {
            this.IsAllowed = interopStruct.isAllowed.Value;
            this.TargetXuid = interopStruct.targetXuid;
            this.TargetUserType = interopStruct.targetUserType;
            this.PermissionRequested = interopStruct.permissionRequested;
            this.Reasons = interopStruct.GetReasons(x => new XblPermissionDenyReasonDetails(x));
        }

        public bool IsAllowed { get; private set; }
        public UInt64 TargetXuid { get; private set; }
        public XblAnonymousUserType TargetUserType { get; private set; }
        public XblPermission PermissionRequested { get; private set; }
        public XblPermissionDenyReasonDetails[] Reasons { get; private set; }
    }
}
