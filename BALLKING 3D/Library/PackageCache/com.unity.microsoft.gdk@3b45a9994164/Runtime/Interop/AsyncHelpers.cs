using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    public class AsyncHelpers
    {
        public static XAsyncBlock WrapAsyncBlock(XTaskQueueHandle queue, XAsyncCompletionRoutine callback)
        {
            return new XAsyncBlock(queue, callback, IntPtr.Zero);
        }

        internal static void CleanupAsyncBlock(XAsyncBlock block)
        {
            block.Dispose();
        }
    }
}
