using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct XAsyncBlockPtr
    {
        internal XAsyncBlockPtr(IntPtr intPtr)
        {
            this.IntPtr = intPtr;
        }

        internal readonly IntPtr IntPtr;

        public static implicit operator IntPtr(XAsyncBlockPtr ptr) => ptr.IntPtr;
        public static implicit operator XAsyncBlockPtr(XAsyncBlock block) => new XAsyncBlockPtr(block.InteropPtr);
    }
}
