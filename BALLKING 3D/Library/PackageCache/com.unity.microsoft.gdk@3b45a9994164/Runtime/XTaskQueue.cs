using System;
using System.Runtime.InteropServices;
using System.Threading;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    //enum class XTaskQueueDispatchMode : uint32_t
    //{
    //    Manual,
    //    ThreadPool,
    //    SerializedThreadPool,
    //    Immediate
    //};
    public enum XTaskQueueDispatchMode : UInt32
    {
        Manual = 0,
        ThreadPool,
        SerializedThreadPool,
        Immediate
    }

    //enum class XTaskQueuePort : uint32_t
    //{
    //    Work,
    //    Completion
    //};
    public enum XTaskQueuePort : UInt32
    {
        Work,
        Completion
    };

    public delegate void XTaskQueueCallback(IntPtr context, bool canceled);

    public delegate void XTaskQueueMonitorCallback(IntPtr context, IntPtr queue, XTaskQueuePort port);

    public delegate void XTaskQueueTerminatedCallback(IntPtr context);

    public class XTaskQueueWaiterCallbackHandle
    {
        internal XTaskQueueWaiterCallbackHandle(XTaskQueueHandle queue, Interop.XTaskQueueCallback callback,
            IntPtr context)
        {
            interop = new Interop.XTaskQueueWaiterCallbackHandle(queue, callback, context);
        }

        internal Interop.XTaskQueueWaiterCallbackHandle interop;

        public bool IsValid { get { return interop.IsValid; } }

        public UInt64 Token
        {
            get => interop.Token;
            set => interop.Token = value;
        }

        public void Unregister(XTaskQueueHandle queue)
        {
            interop.Unregister(queue);
        }

        public void Unregister()
        {
            interop.Unregister();
        }

        public void Dispose()
        {
            interop.Dispose();
        }
    }

    public class XTaskQueueMonitorCallbackHandle
    {
        internal Interop.XTaskQueueMonitorCallbackHandle interop;


        internal XTaskQueueMonitorCallbackHandle(XTaskQueueHandle queue, Interop.XTaskQueueMonitorCallback callback,
            IntPtr context)
        {
            interop = new Interop.XTaskQueueMonitorCallbackHandle(queue, callback, context);
        }

        public bool IsValid { get { return interop.IsValid; } }

        public UInt64 Token
        {
            get => interop.Token;
            set => interop.Token = value;
        }

        public void Unregister(XTaskQueueHandle queue)
        {
            interop.Unregister(queue);
        }

        public void Unregister()
        {
            interop.Unregister();
        }

        public void Dispose()
        {
            interop.Dispose();
        }
    }

    public class XTaskQueueCallbackHandle
    {
        internal Interop.XTaskQueueCallbackHandle interop;


        internal XTaskQueueCallbackHandle(Interop.XTaskQueueCallback callback,
            IntPtr context)
        {
            interop = new Interop.XTaskQueueCallbackHandle(callback, context);
        }

        public bool IsValid { get { return interop.IsValid; } }

        public UInt64 Token
        {
            get => interop.Token;
            set => interop.Token = value;
        }

        public void Dispose()
        {
            interop.Dispose();
        }
    }

    public class XTaskQueueTerminateCallbackHandle
    {
        internal Interop.XTaskQueueTerminateCallbackHandle interop;


        internal XTaskQueueTerminateCallbackHandle(Interop.XTaskQueueTerminatedCallback callback,
            IntPtr context)
        {
            interop = new Interop.XTaskQueueTerminateCallbackHandle(callback, context);
        }

        public bool IsValid { get { return interop.IsValid; } }

        public UInt64 Token
        {
            get => interop.Token;
            set => interop.Token = value;
        }

        public void Dispose()
        {
            interop.Dispose();
        }
    }

    public partial class SDK
    {
        public static void XTaskQueueCloseHandle(XTaskQueueHandle queue)
        {
            queue.Close();
        }

        public static Int32 XTaskQueueCreate(XTaskQueueDispatchMode workDispatchMode,
            XTaskQueueDispatchMode completionDispatchMode,
            out XTaskQueueHandle handle)
        {
            handle = null;
            IntPtr interopHandle;
            Int32 hr = NativeMethods.XTaskQueueCreate(workDispatchMode, completionDispatchMode, out interopHandle);
            if (HR.SUCCEEDED(hr))
            {
                handle = new XTaskQueueHandle(interopHandle);
            }

            return hr;
        }

        public static int XTaskQueueCreateComposite(XTaskQueuePortHandle workPort,
            XTaskQueuePortHandle completionPort,
            out XTaskQueueHandle queue)
        {
            queue = null;
            IntPtr interopHandle;
            Int32 hr = NativeMethods.XTaskQueueCreateComposite(workPort.Handle, completionPort.Handle, out interopHandle);
            if (HR.SUCCEEDED(hr))
            {
                queue = new XTaskQueueHandle(interopHandle);
            }

            return hr;
        }

        public static bool XTaskQueueDispatch(XTaskQueueHandle queue,
            XTaskQueuePort port,
            UInt32 timeoutInMs)
        {
            return NativeMethods.XTaskQueueDispatch(queue.Handle, port, timeoutInMs);
        }

        public static int XTaskQueueDuplicateHandle(XTaskQueueHandle queue,
            out XTaskQueueHandle duplicatedHandle)
        {
            duplicatedHandle = null;
            IntPtr handle;
            int hr = NativeMethods.XTaskQueueDuplicateHandle(queue.Handle, out handle);
            if (HR.SUCCEEDED(hr))
            {
                duplicatedHandle = new XTaskQueueHandle(handle);
            }

            return hr;
        }

        public static bool XTaskQueueGetCurrentProcessTaskQueue(out XTaskQueueHandle queue)
        {
            IntPtr handle;
            bool hasQueue = NativeMethods.XTaskQueueGetCurrentProcessTaskQueue(out handle);
            queue = hasQueue ? new XTaskQueueHandle(handle) : null;
            return hasQueue;
        }

        public static int XTaskQueueGetPort(XTaskQueueHandle queue,
            XTaskQueuePort port,
            out XTaskQueuePortHandle portHandle)
        {
            portHandle = null;
            IntPtr handle;
            int hr = NativeMethods.XTaskQueueGetPort(queue.Handle, port, out handle);
            if (HR.SUCCEEDED(hr))
            {
                portHandle = new XTaskQueuePortHandle(handle);
            }

            return hr;
        }

        public static int XTaskQueueRegisterMonitor(XTaskQueueHandle queue,
            IntPtr callbackContext,
            XTaskQueueMonitorCallback callback,
            out XTaskQueueMonitorCallbackHandle tokenHandle)
        {
            Interop.XTaskQueueMonitorCallback interopCallback = (IntPtr context, IntPtr queuePtr, XTaskQueuePort port) =>
            {
                callback(context, queuePtr, port);
            };

            tokenHandle = new XTaskQueueMonitorCallbackHandle(queue, interopCallback, callbackContext);

            UInt64 token;
            int hr = NativeMethods.XTaskQueueRegisterMonitor(queue.Handle,
                tokenHandle.interop.CallbackContext,
                tokenHandle.interop.StaticCallback,
                out token);
            if (HR.SUCCEEDED(hr))
            {
                tokenHandle.Token = token;
            }
            else
            {
                tokenHandle.Dispose();
                tokenHandle = null;
            }

            return hr;
        }

        public static int XTaskQueueRegisterWaiter(XTaskQueueHandle queue,
            XTaskQueuePort port,
            WaitHandle waitHandle,
            IntPtr callbackContext,
            XTaskQueueCallback callback,
            out XTaskQueueWaiterCallbackHandle tokenHandle)
        {
            Interop.XTaskQueueCallback interopCallback = (IntPtr context, bool canceled) =>
            {
                callback(context, canceled);
            };

            tokenHandle = new XTaskQueueWaiterCallbackHandle(queue, interopCallback, callbackContext);

            UInt64 token;
            int hr = NativeMethods.XTaskQueueRegisterWaiter(queue.Handle,
                port,
                waitHandle.SafeWaitHandle,
                tokenHandle.interop.CallbackContext,
                tokenHandle.interop.StaticCallback,
                out token);
            if (HR.SUCCEEDED(hr))
            {
                tokenHandle.Token = token;
            }
            else
            {
                tokenHandle.Dispose();
                tokenHandle = null;
            }

            return hr;
        }

        public static void XTaskQueueSetCurrentProcessTaskQueue(XTaskQueueHandle queue)
        {
            NativeMethods.XTaskQueueSetCurrentProcessTaskQueue(queue.Handle);
        }

        public static int XTaskQueueSubmitCallback(XTaskQueueHandle queue,
            XTaskQueuePort port,
            IntPtr callbackContext,
            XTaskQueueCallback callback,
            out XTaskQueueCallbackHandle callbackHandle)
        {
            Interop.XTaskQueueCallback interopCallback = (IntPtr context, bool canceled) =>
            {
                callback(context, canceled);
            };

            callbackHandle = new XTaskQueueCallbackHandle(interopCallback, callbackContext);

            int hr = NativeMethods.XTaskQueueSubmitCallback(queue.Handle,
                port,
                callbackHandle.interop.CallbackContext,
                callbackHandle.interop.StaticCallback);
            if (HR.FAILED(hr))
            {
                callbackHandle.Dispose();
                callbackHandle = null;
            }

            return hr;
        }

        public static int XTaskQueueSubmitDelayedCallback(XTaskQueueHandle queue,
            XTaskQueuePort port,
            UInt32 delayMs,
            IntPtr callbackContext,
            XTaskQueueCallback callback,
            out XTaskQueueCallbackHandle callbackHandle)
        {
            Interop.XTaskQueueCallback interopCallback = (IntPtr context, bool canceled) =>
            {
                callback(context, canceled);
            };

            callbackHandle = new XTaskQueueCallbackHandle(interopCallback, callbackContext);
            int hr = NativeMethods.XTaskQueueSubmitDelayedCallback(queue.Handle,
                port,
                delayMs,
                callbackHandle.interop.CallbackContext,
                callbackHandle.interop.StaticCallback);
            if (HR.FAILED(hr))
            {
                callbackHandle.Dispose();
                callbackHandle = null;
            }

            return hr;
        }

        public static int XTaskQueueTerminate(XTaskQueueHandle queue,
            bool wait,
            IntPtr callbackContext,
            XTaskQueueTerminatedCallback callback,
            out XTaskQueueTerminateCallbackHandle callbackHandle)
        {
            Interop.XTaskQueueTerminatedCallback interopCallback = (IntPtr context) =>
            {
                callback(context);
            };

            callbackHandle = new XTaskQueueTerminateCallbackHandle(interopCallback, callbackContext);
            int hr = NativeMethods.XTaskQueueTerminate(queue.Handle,
                wait,
                callbackHandle.interop.CallbackContext,
                callbackHandle.interop.StaticCallback);
            if (HR.FAILED(hr))
            {
                callbackHandle.Dispose();
                callbackHandle = null;
            }

            return hr;
        }

        public static int XTaskQueueTerminate(XTaskQueueHandle queue,
            bool wait,
            IntPtr callbackContext)
        {
            return NativeMethods.XTaskQueueTerminate(queue.Handle,
                wait,
                callbackContext,
                null);
        }

        public static void XTaskQueueUnregisterMonitor(XTaskQueueHandle queue, XTaskQueueMonitorCallbackHandle token)
        {
            token.Unregister(queue);
        }

        public static void XTaskQueueUnregisterWaiter(XTaskQueueHandle queue, XTaskQueueWaiterCallbackHandle token)
        {
            token.Unregister(queue);
        }
    }

}
