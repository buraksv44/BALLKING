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
    public class XblAchievementsResultHandle : EquatableHandle
    {
        internal XblAchievementsResultHandle(Interop.XblAchievementsResultHandle interopHandle) :
            base(IntPtr.Zero, true, interopHandle.handle)
        {
        }

        public override bool IsInvalid => this.Handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            XblInterop.XblAchievementsResultCloseHandle(this.Handle);
            SetHandle(IntPtr.Zero);
            return true;
        }
    }
}
