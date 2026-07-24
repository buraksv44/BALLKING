using System;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class HCWebsocketHandle : EquatableHandle
    {
        internal HCWebsocketHandle(IntPtr handle) : 
            base(IntPtr.Zero, true, handle)
        {
        }

        internal HCWebsocketHandle(IntPtr handle, bool ownsHandle) :
            base(IntPtr.Zero, ownsHandle, handle)
        {

        }
        internal static Int32 WrapAndReturnHResult(Int32 hresult, IntPtr interopHandle, out HCWebsocketHandle handle, GCHandle callbackHandle)
        {
            if (HR.SUCCEEDED(hresult))
            {
                handle = new HCWebsocketHandle(interopHandle);
                handle.cbHandle = callbackHandle;
            }
            else
            {
                if ( callbackHandle != null && callbackHandle.IsAllocated )
                    callbackHandle.Free();

                handle = default(HCWebsocketHandle);
            }
            return hresult;
        }

        public override bool Equals(object obj) => obj is HCWebsocketHandle userHandleObj && this.Handle == userHandleObj.Handle;
        public override int GetHashCode() => this.Handle.GetHashCode();

        protected override bool ReleaseHandle()
        {
            XGRInterop.HCWebSocketCloseHandle(this.Handle);
            SetHandle(IntPtr.Zero);
            return true;
        }

        public static bool operator ==(HCWebsocketHandle handle1, HCWebsocketHandle handle2) =>
            object.ReferenceEquals(handle1, null) ? object.ReferenceEquals(handle2, null) : handle1.Equals(handle2);
        public static bool operator !=(HCWebsocketHandle handle1, HCWebsocketHandle handle2) => !(handle1 == handle2);

        public HCWebSocketMessageFunction MessageFunction { get { return messageCallback; } }
        public HCWebSocketBinaryMessageFunction BinaryMessageFunction { get { return binaryMessageCallback; } }
        public HCWebSocketCloseEventFunction CloseFunction { get { return closeCallback; } }

        public override bool IsInvalid
        {
            get { return this.Handle == IntPtr.Zero; }
        }

        internal Interop.HCWebSocketMessageFunction messageFunc;
        internal Interop.HCWebSocketBinaryMessageFunction binaryMessageFunc;
        internal Interop.HCWebSocketCloseEventFunction closeFunc;

        internal HCWebSocketMessageFunction messageCallback;
        internal HCWebSocketBinaryMessageFunction binaryMessageCallback;
        internal HCWebSocketCloseEventFunction closeCallback;

        internal GCHandle cbHandle;
    }
}
