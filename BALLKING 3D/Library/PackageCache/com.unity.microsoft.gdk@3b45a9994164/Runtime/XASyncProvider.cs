using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    // enum class XAsyncOp : uint32_t
    // {
    //     Begin,
    //     DoWork,
    //     GetResult,
    //     Cancel,
    //     Cleanup
    // };
    public enum XAsyncOp : UInt32
    {
        Begin,
        DoWork,
        GetResult,
        Cancel,
        Cleanup
    }

    public class XAsyncProviderData
    {
        internal XAsyncProviderData(Interop.XAsyncProviderData interop, XAsyncBlock block)
        {
            _async = block;
            this.interop = interop;
        }

        internal Interop.XAsyncProviderData interop;
        internal XAsyncBlock _async;

        public XAsyncBlock Async
        {
            get => _async;
            set => _async = value;
        }

        public UInt64 BufferSize
        {
            get => interop.bufferSize;
            set => interop.bufferSize = value;
        }

        public IntPtr Buffer
        {
            get => interop.buffer;
            set => interop.buffer = value;
        }

        public IntPtr Context
        {
            get => interop.context;
            set => interop.context = value;
        }
    }

    public delegate Int32 XAsyncProvider(XAsyncOp op, XAsyncProviderData data);

    partial class SDK
    {
        [AOT.MonoPInvokeCallback(typeof(XAsyncProviderInterop))]
        private static Int32 OnAsyncProvider(XAsyncOp op, Interop.XAsyncProviderData data)
        {
            GCHandle handle = GCHandle.FromIntPtr(data.context);
            var wrapper = handle.Target as CallbackWrapper<XAsyncProviderInterop>;
            return wrapper.Callback(op, data);
        }

        public static Int32 XAsyncBegin(XAsyncBlock asyncBlock,
            IntPtr context,
            IntPtr identity,
            string identityName,
            XAsyncProvider provider)
        {
            XAsyncProviderInterop localCallback = (XAsyncOp _op, Interop.XAsyncProviderData _dataInterop) =>
            {
                XAsyncProviderData data = new XAsyncProviderData(_dataInterop, asyncBlock);
                return provider(_op, data);
            };

            using (var wrapper = new CallbackWrapper<XAsyncProviderInterop>(localCallback, context, OnAsyncProvider))
            {
                return NativeMethods.XAsyncBegin(asyncBlock.InteropPtr,
                wrapper.CallbackContext,
                identity,
                identityName,
                wrapper.StaticCallback);
            }
        }

        public static Int32 XAsyncSchedule(XAsyncBlock asyncBlock, UInt32 delayInMs)
        {
            return NativeMethods.XAsyncSchedule(asyncBlock.InteropPtr, delayInMs);
        }

        public static void XAsyncComplete(XAsyncBlock asyncBlock,
            UInt32 result,
            UInt64 requiredBufferSize)
        {
            NativeMethods.XAsyncComplete(asyncBlock.InteropPtr,
                result,
                requiredBufferSize);
        }

        public static int XAsyncGetResult(XAsyncBlock asyncBlock,
            IntPtr identity,
            byte[] buffer,
            out UInt64 bufferUsed)
        {
            return NativeMethods.XAsyncGetResult(asyncBlock.InteropPtr,
                identity,
                (UInt32)buffer.Length,
                buffer,
                out bufferUsed);
        }
    }
}
