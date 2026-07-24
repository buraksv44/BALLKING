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
    public class XblMultiplayerSearchHandle : EquatableHandle
    {
        internal XblMultiplayerSearchHandle(Interop.XblMultiplayerSearchHandle interopHandle) :
            base(IntPtr.Zero, true, interopHandle.Ptr)
        {
        }

        public override bool IsInvalid => this.Handle == IntPtr.Zero;

        internal static Int32 WrapInteropHandleAndReturnHResult(Int32 hresult, Interop.XblMultiplayerSearchHandle interopHandle, out XblMultiplayerSearchHandle userHandle)
        {
            if (HR.SUCCEEDED(hresult))
            {
                userHandle = new XblMultiplayerSearchHandle(interopHandle);
            }
            else
            {
                userHandle = default(XblMultiplayerSearchHandle);
            }
            return hresult;
        }

        protected override bool ReleaseHandle()
        {
            XblInterop.XblMultiplayerSearchHandleCloseHandle(this.Handle);
            SetHandle(IntPtr.Zero);
            return true;
        }
    }
}
