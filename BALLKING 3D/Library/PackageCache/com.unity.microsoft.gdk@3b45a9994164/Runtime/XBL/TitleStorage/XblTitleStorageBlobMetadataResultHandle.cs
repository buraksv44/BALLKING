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
    public class XblTitleStorageBlobMetadataResultHandle : EquatableHandle
    {
        internal XblTitleStorageBlobMetadataResultHandle(Interop.XblTitleStorageBlobMetadataResultHandle interopHandle) :
            base(IntPtr.Zero, true, interopHandle.intPtr)
        {
        }

        public override bool IsInvalid => this.handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            XblInterop.XblTitleStorageBlobMetadataResultCloseHandle(this.Handle);
            SetHandle(this.handle);
            return true;
        }
    }
}
