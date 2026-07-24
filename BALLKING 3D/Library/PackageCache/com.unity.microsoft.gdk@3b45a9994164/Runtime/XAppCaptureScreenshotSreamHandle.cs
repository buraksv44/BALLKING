using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public class XAppCaptureScreenshotStreamHandle : EquatableHandle
    {
        public XAppCaptureScreenshotStreamHandle(IntPtr handle) :
            base(IntPtr.Zero, true, handle)
        {
        }

        protected override bool ReleaseHandle()
        {
            this.CloseResult = NativeMethods.XAppCaptureCloseScreenshotStream(this.Handle);
            SetHandle(IntPtr.Zero);

            return HR.SUCCEEDED(this.CloseResult);
        }

        public int CloseResult { get; private set; }

        internal static Int32 WrapAndReturnHResult(Int32 hresult, IntPtr interopHandle, out XAppCaptureScreenshotStreamHandle handle)
        {
            if (Interop.HR.SUCCEEDED(hresult))
            {
                handle = new XAppCaptureScreenshotStreamHandle(interopHandle);
            }
            else
            {
                handle = default(XAppCaptureScreenshotStreamHandle);
            }
            return hresult;
        }

        public override bool IsInvalid
        {
            get { return this.Handle == IntPtr.Zero; }
        }
    }
}
