using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblSocialManagerUserGroupHandle : EquatableHandle
    {
        internal XblSocialManagerUserGroupHandle(Interop.XblSocialManagerUserGroupHandle interopHandle) :
            base(IntPtr.Zero, true, interopHandle.Handle)
        {
        }

        internal static Int32 WrapAndReturnHResult(Int32 hresult, Interop.XblSocialManagerUserGroupHandle interopHandle, out XblSocialManagerUserGroupHandle handle)
        {
            if (Interop.HR.SUCCEEDED(hresult))
            {
                handle = new XblSocialManagerUserGroupHandle(interopHandle);
            }
            else
            {
                handle = default(XblSocialManagerUserGroupHandle);
            }
            return hresult;
        }

        public override bool IsInvalid => this.Handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            return true;
        }
    }
}
