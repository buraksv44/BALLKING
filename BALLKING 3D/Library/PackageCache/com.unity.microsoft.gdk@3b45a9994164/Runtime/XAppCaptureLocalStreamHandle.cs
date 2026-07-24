using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public class XAppCaptureLocalStreamHandle : EquatableHandle
    {
        public XAppCaptureLocalStreamHandle(IntPtr handle) :
            base(IntPtr.Zero, true, handle)
        {
        }

        public int CloseResult { get; private set; }

        protected override bool ReleaseHandle()
        {
            this.CloseResult = NativeMethods.XAppCaptureCloseLocalStream(this.Handle);
            SetHandle(IntPtr.Zero);

            return HR.SUCCEEDED(this.CloseResult);
        }

        public override bool IsInvalid
        {
            get { return this.Handle == IntPtr.Zero; }
        }
    }
}
