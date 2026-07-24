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
    public class XblMultiplayerSessionHandle : EquatableHandle
    {
        internal XblMultiplayerSessionHandle(Interop.XblMultiplayerSessionHandle interopHandle) :
            base(IntPtr.Zero, true, interopHandle.intPtr)
        {
        }

        public override bool IsInvalid => this.Handle == IntPtr.Zero;

        internal static Int32 WrapInteropHandleAndReturnHResult(Int32 hresult, Interop.XblMultiplayerSessionHandle interopHandle, out XblMultiplayerSessionHandle sessionHandle)
        {
            if (HR.SUCCEEDED(hresult))
            {
                sessionHandle = new XblMultiplayerSessionHandle(interopHandle);
            }
            else
            {
                sessionHandle = default(XblMultiplayerSessionHandle);
            }
            return hresult;
        }

        protected override bool ReleaseHandle()
        {
            XblInterop.XblMultiplayerSessionCloseHandle(this.Handle);
            SetHandle(IntPtr.Zero);
            return true;
        }
    }
}
