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
    public class XblMultiplayerEventArgsHandle : EquatableHandle
    {
        // XblMultiplayerEventArgsHandle doesn't have a close API to call at release time, ownership set to false
        // to prevent autoreleases
        internal XblMultiplayerEventArgsHandle(Interop.XblMultiplayerEventArgsHandle interopHandle) :
            base(IntPtr.Zero, false, interopHandle.handle)
        {
        }


        internal static Int32 WrapInteropHandleAndReturnHResult(Int32 hresult, Interop.XblMultiplayerEventArgsHandle interopHandle, out XblMultiplayerEventArgsHandle handle)
        {
            if (HR.SUCCEEDED(hresult))
            {
                handle = new XblMultiplayerEventArgsHandle(interopHandle);
            }
            else
            {
                handle = default(XblMultiplayerEventArgsHandle);
            }
            return hresult;
        }

        protected override bool ReleaseHandle()
        {
            SetHandle(IntPtr.Zero);
            return true;
        }


        public override bool IsInvalid => this.Handle == IntPtr.Zero;
    }
}
