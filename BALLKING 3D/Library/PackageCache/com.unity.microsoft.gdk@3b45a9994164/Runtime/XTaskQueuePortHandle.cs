using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime
{
    public class XTaskQueuePortHandle : EquatableHandle
    {
        public XTaskQueuePortHandle(IntPtr handle) :
            base(IntPtr.Zero, true, handle)
        {
        }

        protected override bool ReleaseHandle()
        {
            SetHandle(IntPtr.Zero);
            return true;
        }

        public override bool IsInvalid
        {
            get { return this.Handle == IntPtr.Zero; }
        }
    }
}
