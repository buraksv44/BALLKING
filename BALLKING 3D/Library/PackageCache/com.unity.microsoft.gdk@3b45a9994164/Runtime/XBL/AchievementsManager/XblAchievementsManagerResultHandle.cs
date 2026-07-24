using System;
using System.Collections.Generic;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblAchievementsManagerResultHandle : EquatableHandle
    {
        internal XblAchievementsManagerResultHandle(Interop.XblAchievementsManagerResultHandle interopHandle) :
            base(IntPtr.Zero, true, interopHandle.Ptr)
        {
        }

        internal static Int32 WrapAndReturnHResult(Int32 hresult, Interop.XblAchievementsManagerResultHandle interopHandle, out XblAchievementsManagerResultHandle handle)
        {
            if (HR.SUCCEEDED(hresult))
            {
                handle = new XblAchievementsManagerResultHandle(interopHandle);
            }
            else
            {
                handle = default(XblAchievementsManagerResultHandle);
            }
            return hresult;
        }

        public override bool Equals(object obj) => obj is XblAchievementsManagerResultHandle userHandleObj && this.Handle == userHandleObj.Handle;
        public override int GetHashCode() => this.Handle.GetHashCode();

        public override bool IsInvalid => this.Handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            XblInterop.XblAchievementsManagerResultCloseHandle(this.Handle);
            SetHandle(IntPtr.Zero);
            return true;
        }

        public static bool operator ==(XblAchievementsManagerResultHandle handle1, XblAchievementsManagerResultHandle handle2) =>
            object.ReferenceEquals(handle1, null) ? object.ReferenceEquals(handle2, null) : handle1.Equals(handle2);
        public static bool operator !=(XblAchievementsManagerResultHandle handle1, XblAchievementsManagerResultHandle handle2) => !(handle1 == handle2);
    }
}
