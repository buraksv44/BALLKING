using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Unity.XGamingRuntime.Interop
{
    internal class XTaskQueueWaiterCallbackHandle : XRegistrationToken<Interop.XTaskQueueCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XTaskQueueCallback))]
        static void OnWaiter(IntPtr context, bool canceled)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XTaskQueueCallback>;
            wrapper.Callback(wrapper.Context, canceled);
        }

        XTaskQueueHandle queue;

        public XTaskQueueWaiterCallbackHandle(XTaskQueueHandle queue, Interop.XTaskQueueCallback callback, IntPtr context) :
            base(callback, context, OnWaiter)
        {
            this.queue = queue;
        }

        public void Unregister(XTaskQueueHandle queue)
        {
            if (this.Token != 0)
            {
                NativeMethods.XTaskQueueUnregisterWaiter(queue.Handle, this.Token);
                this.Token = 0;
            }
        }

        public void Unregister()
        {
            Unregister(this.queue);
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister();
        }
    }

    internal class XTaskQueueMonitorCallbackHandle : XRegistrationToken<Interop.XTaskQueueMonitorCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XTaskQueueMonitorCallback))]
        static void OnMonitor(IntPtr context, IntPtr queue, XTaskQueuePort port)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XTaskQueueMonitorCallback>;
            wrapper.Callback(wrapper.Context, queue, port);
        }

        XTaskQueueHandle queue;

        public XTaskQueueMonitorCallbackHandle(XTaskQueueHandle queue, Interop.XTaskQueueMonitorCallback callback, IntPtr context) :
            base(callback, context, OnMonitor)
        {
            this.queue = queue;
        }

        public void Unregister(XTaskQueueHandle queue)
        {
            if (this.Token != 0)
            {
                NativeMethods.XTaskQueueUnregisterMonitor(queue.Handle, this.Token);
                this.Token = 0;
            }
        }

        public void Unregister()
        {
            Unregister(this.queue);
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister();
        }
    }

    internal class XTaskQueueCallbackHandle : XRegistrationToken<Interop.XTaskQueueCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XTaskQueueCallback))]
        static void OnCallback(IntPtr context, bool canceled)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XTaskQueueCallback>;
            wrapper.Callback(wrapper.Context, canceled);
        }

        public XTaskQueueCallbackHandle(Interop.XTaskQueueCallback callback, IntPtr context) :
            base(callback, context, OnCallback)
        {
        }

        protected override void DisposeInternal(bool disposing)
        {
        }
    }

    internal class XTaskQueueTerminateCallbackHandle : XRegistrationToken<Interop.XTaskQueueTerminatedCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XTaskQueueTerminatedCallback))]
        static void OnTerminate(IntPtr context)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XTaskQueueTerminatedCallback>;
            wrapper.Callback(wrapper.Context);
        }

        public XTaskQueueTerminateCallbackHandle(Interop.XTaskQueueTerminatedCallback callback, IntPtr context) :
            base(callback, context, OnTerminate)
        { }

        protected override void DisposeInternal(bool disposing)
        {
        }
    }

    //typedef void CALLBACK XTaskQueueCallback(_In_opt_ void* context, _In_ bool canceled);
    internal delegate void XTaskQueueCallback(IntPtr context,
        [MarshalAs(UnmanagedType.I1)] bool canceled);

    //typedef void CALLBACK XTaskQueueMonitorCallback(_In_opt_ void* context, _In_ XTaskQueueHandle queue, _In_ XTaskQueuePort port);
    internal delegate void XTaskQueueMonitorCallback(IntPtr context, IntPtr queue, XTaskQueuePort port);

    //typedef void CALLBACK XTaskQueueTerminatedCallback(_In_opt_ void* context);
    internal delegate void XTaskQueueTerminatedCallback(IntPtr context);

    partial class NativeMethods
    {
        //STDAPI_(void) XTaskQueueCloseHandle(
        //    _In_ XTaskQueueHandle queue
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XTaskQueueCloseHandle(IntPtr queue);

        //STDAPI XTaskQueueCreate(
        //    _In_ XTaskQueueDispatchMode workDispatchMode,
        //    _In_ XTaskQueueDispatchMode completionDispatchMode,
        //    _Out_ XTaskQueueHandle* queue
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XTaskQueueCreate(
            XTaskQueueDispatchMode workDispatchMode,
            XTaskQueueDispatchMode completionDispatchMode,
            out IntPtr queue);

        //STDAPI XTaskQueueCreateComposite(
        //    _In_ XTaskQueuePortHandle workPort,
        //    _In_ XTaskQueuePortHandle completionPort,
        //    _Out_ XTaskQueueHandle* queue
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XTaskQueueCreateComposite(IntPtr workPort,
            IntPtr completionPort,
            out IntPtr queue);

        //STDAPI_(bool) XTaskQueueDispatch(
        //    _In_ XTaskQueueHandle queue,
        //    _In_ XTaskQueuePort port,
        //    _In_ uint32_t timeoutInMs
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XTaskQueueDispatch(IntPtr queue,
            XTaskQueuePort port,
            UInt32 timeoutInMs);

        //STDAPI XTaskQueueDuplicateHandle(
        //    _In_ XTaskQueueHandle queueHandle,
        //    _Out_ XTaskQueueHandle* duplicatedHandle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XTaskQueueDuplicateHandle(IntPtr queueHandle,
            out IntPtr duplicatedHandle);

        //STDAPI_(bool) XTaskQueueGetCurrentProcessTaskQueue(_Out_ XTaskQueueHandle* queue) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XTaskQueueGetCurrentProcessTaskQueue(out IntPtr queue);

        //STDAPI XTaskQueueGetPort(
        //    _In_ XTaskQueueHandle queue,
        //    _In_ XTaskQueuePort port,
        //    _Out_ XTaskQueuePortHandle* portHandle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XTaskQueueGetPort(IntPtr queue,
            XTaskQueuePort port,
            out IntPtr portHandle);

        //STDAPI XTaskQueueRegisterMonitor(
        //    _In_ XTaskQueueHandle queue,
        //    _In_opt_ void* callbackContext,
        //    _In_ XTaskQueueMonitorCallback* callback,
        //    _Out_ XTaskQueueRegistrationToken* token
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XTaskQueueRegisterMonitor(IntPtr queue,
            IntPtr callbackContext,
            XTaskQueueMonitorCallback callback,
            out UInt64 token);

        //STDAPI XTaskQueueRegisterWaiter(
        //    _In_ XTaskQueueHandle queue,
        //    _In_ XTaskQueuePort port,
        //    _In_ HANDLE waitHandle,
        //    _In_opt_ void* callbackContext,
        //    _In_ XTaskQueueCallback* callback,
        //    _Out_ XTaskQueueRegistrationToken* token
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XTaskQueueRegisterWaiter(IntPtr queue,
            XTaskQueuePort port,
            SafeWaitHandle waitHandle,
            IntPtr callbackContext,
            XTaskQueueCallback callback,
            out UInt64 token);

        //STDAPI_(void) XTaskQueueSetCurrentProcessTaskQueue(
        //    _In_ XTaskQueueHandle queue
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XTaskQueueSetCurrentProcessTaskQueue(IntPtr queue);

        //STDAPI XTaskQueueSubmitCallback(
        //    _In_ XTaskQueueHandle queue,
        //    _In_ XTaskQueuePort port,
        //    _In_opt_ void* callbackContext,
        //    _In_ XTaskQueueCallback* callback
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XTaskQueueSubmitCallback(IntPtr queue,
            XTaskQueuePort port,
            IntPtr callbackContext,
            XTaskQueueCallback callback);

        //STDAPI XTaskQueueSubmitDelayedCallback(
        //    _In_ XTaskQueueHandle queue,
        //    _In_ XTaskQueuePort port,
        //    _In_ uint32_t delayMs,
        //    _In_opt_ void* callbackContext,
        //    _In_ XTaskQueueCallback* callback
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XTaskQueueSubmitDelayedCallback(IntPtr queue,
            XTaskQueuePort port,
            UInt32 delayMs,
            IntPtr callbackContext,
            XTaskQueueCallback callback);

        //STDAPI XTaskQueueTerminate(
        //    _In_ XTaskQueueHandle queue,
        //    _In_ bool wait,
        //    _In_opt_ void* callbackContext,
        //    _In_opt_ XTaskQueueTerminatedCallback* callback
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XTaskQueueTerminate(IntPtr queue,
            [MarshalAs(UnmanagedType.I1)] bool wait,
            IntPtr callbackContext,
            XTaskQueueTerminatedCallback callback);

        //STDAPI_(void) XTaskQueueUnregisterMonitor(
        //    _In_ XTaskQueueHandle queue,
        //    _In_ XTaskQueueRegistrationToken token
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XTaskQueueUnregisterMonitor(IntPtr queue,
            UInt64 token);

        //STDAPI_(void) XTaskQueueUnregisterWaiter(
        //    _In_ XTaskQueueHandle queue,
        //    _In_ XTaskQueueRegistrationToken token
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XTaskQueueUnregisterWaiter(IntPtr queue,
            UInt64 token);
    }
}
