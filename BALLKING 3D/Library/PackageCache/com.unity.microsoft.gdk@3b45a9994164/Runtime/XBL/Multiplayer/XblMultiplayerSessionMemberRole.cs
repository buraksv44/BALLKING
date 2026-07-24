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
    public class XblMultiplayerSessionMemberRole
    {
        internal XblMultiplayerSessionMemberRole(Interop.XblMultiplayerSessionMemberRole interopHandle)
        {
            this.InteropHandle = interopHandle;

            this.RoleTypeName = interopHandle.roleTypeName.GetString();
            this.RoleName = interopHandle.roleName.GetString();
        }

        public string RoleTypeName { get; }
        public string RoleName { get; }

        internal Interop.XblMultiplayerSessionMemberRole InteropHandle { get; }
    }
}
