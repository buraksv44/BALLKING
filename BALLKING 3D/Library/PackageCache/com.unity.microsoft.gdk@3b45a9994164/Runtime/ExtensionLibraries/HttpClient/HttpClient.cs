using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public delegate void HCCleanupHandler();
    public delegate void HCWebSocketMessageFunction(HCWebsocketHandle websocket, string incomingBodyString);
    public delegate void HCWebSocketBinaryMessageFunction(HCWebsocketHandle websocket, byte[] payloadBytes);
    public delegate void HCWebSocketCloseEventFunction(HCWebsocketHandle websocket, HCWebSocketCloseStatus closeStatus);

    public delegate void HCSocketCompletionResultFunction(HCWebsocketHandle websocket, Int32 errorCode, UInt32 platformErrorCode);
    public delegate void HCWebSocketRoutedHandler(HCWebsocketHandle websocket, bool receiving, string message, byte[] payloadBytes);

    public partial class SDK
    {
        #region Callbacks
        [MonoPInvokeCallback]
        private unsafe static void HCWebSocketMessageCallback(IntPtr websocket, IntPtr incomingBodyString, IntPtr functionContext)
        {
            GCHandle cbHandle = GCHandle.FromIntPtr(functionContext);
            var webSocketHandle = cbHandle.Target as HCWebsocketHandle;
            
            string incomingMessage = null;
            if (incomingBodyString != IntPtr.Zero)
            {
                int len = 0;
                while (Marshal.ReadByte(incomingBodyString, len) != 0) ++len;
                incomingMessage = System.Text.Encoding.UTF8.GetString((byte*)incomingBodyString, len);
            }
            webSocketHandle.messageCallback?.Invoke(new HCWebsocketHandle(websocket, false), incomingMessage);
        }

        [MonoPInvokeCallback]
        private static void HCWebSocketBinaryMessageCallback(IntPtr websocket, IntPtr payloadBytes, UInt32 countOfBlobs, IntPtr functionContext)
        {
            GCHandle cbHandle = GCHandle.FromIntPtr(functionContext);
            var webSocketHandle = cbHandle.Target as HCWebsocketHandle;

            byte[] buffer = null;
            if (payloadBytes != IntPtr.Zero && countOfBlobs != 0)
            {
                buffer = new byte[countOfBlobs];
                Marshal.Copy(payloadBytes, buffer, 0, buffer.Length);
            }
            webSocketHandle.binaryMessageCallback?.Invoke(new HCWebsocketHandle(websocket, false), buffer);
        }

        [MonoPInvokeCallback]
        private static void HCWebSocketCloseCallback(IntPtr websocket, HCWebSocketCloseStatus closeStatus, IntPtr functionContext)
        {
            GCHandle cbHandle = GCHandle.FromIntPtr(functionContext);
            var webSocketHandle = cbHandle.Target as HCWebsocketHandle;
            
            webSocketHandle.closeCallback?.Invoke(new HCWebsocketHandle(websocket, false), closeStatus);

            if (cbHandle.IsAllocated)
            {
                cbHandle.Free();
            }
        }

        [MonoPInvokeCallback]
        private unsafe static void HCWebSocketRoutedCallback(IntPtr websocket, NativeBool receiving, IntPtr message, IntPtr payloadBytes, SizeT payloadSize, IntPtr conext)
        {
            Interop.HCWebSocketMessageFunction messageFunc;
            Interop.HCWebSocketBinaryMessageFunction binaryMessageFunc;
            Interop.HCWebSocketCloseEventFunction closeFunc;
            IntPtr functionContext;

            XGRInterop.HCWebSocketGetEventFunctions(websocket, out messageFunc, out binaryMessageFunc, out closeFunc, out functionContext);
            GCHandle cbHandle = GCHandle.FromIntPtr(functionContext);
            var webSocketHandle = cbHandle.Target as HCWebsocketHandle;

            // convert the message
            string incomingMessage = null;
            if (message != IntPtr.Zero)
            {
                int len = 0;
                while (Marshal.ReadByte(message, len) != 0) ++len;
                incomingMessage = System.Text.Encoding.UTF8.GetString((byte*)message, len);
            }
            // convert the payload
            byte[] buffer = null;
            if (payloadBytes != IntPtr.Zero && !payloadSize.IsZero)
            {
                buffer = new byte[payloadSize.ToInt32()];
                Marshal.Copy(payloadBytes, buffer, 0, buffer.Length);
            }
            routedCallback?.Invoke(new HCWebsocketHandle(websocket, false), receiving.Value, incomingMessage, buffer);
        }
        #endregion

        public static Int32 HCInitialize()
        {
            ResolveXgrHttpClientDllImport();
            IsHttpClientApiSupported();
            return XGRInterop.HCInitialize(IntPtr.Zero);
        }

        public static Int32 HCCleanupAsync(
            HCCleanupHandler completionRoutine
            )
        {
            ResolveXgrHttpClientDllImport();
            IsHttpClientApiSupported();
            XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
            {
                completionRoutine?.Invoke();
            });

            Int32 hr = XGRInterop.HCCleanupAsync(asyncBlock);
            return hr;
        }

        public static Int32 HCWebSocketCreate(
            out HCWebsocketHandle websocket,
            HCWebSocketMessageFunction messageFunc,
            HCWebSocketBinaryMessageFunction binaryMessageFunc,
            HCWebSocketCloseEventFunction closeFunc
            )
        {
            ResolveXgrHttpClientDllImport();
            IsHttpClientApiSupported();
            IntPtr interopHandle;

            var webSocketHandle = new HCWebsocketHandle(default(IntPtr))
            {
                messageFunc = HCWebSocketMessageCallback,
                binaryMessageFunc = HCWebSocketBinaryMessageCallback,
                closeFunc = HCWebSocketCloseCallback,
                messageCallback = messageFunc,
                binaryMessageCallback = binaryMessageFunc,
                closeCallback = closeFunc
            };
            GCHandle cbHandle = GCHandle.Alloc(webSocketHandle);
            webSocketHandle.cbHandle = cbHandle;

            Int32 hr = XGRInterop.HCWebSocketCreate(out interopHandle, webSocketHandle.messageFunc, webSocketHandle.binaryMessageFunc, webSocketHandle.closeFunc, GCHandle.ToIntPtr(cbHandle));
            if (hr == HR.S_OK && interopHandle == IntPtr.Zero)
            {
                cbHandle.Free();
                websocket = null;
                return hr;
            }
  
            return HCWebsocketHandle.WrapAndReturnHResult(hr, interopHandle, out websocket, cbHandle);
        }

        public static Int32 HCWebSocketSetProxyUri(
            HCWebsocketHandle websocket,
            Byte[] proxyUri
            )
        {
            ResolveXgrHttpClientDllImport();
            IsHttpClientApiSupported();
            Int32 hr = XGRInterop.HCWebSocketSetProxyUri(websocket.Handle, proxyUri);
            return hr;
        }

        public static Int32 HCWebSocketSetHeader(
            HCWebsocketHandle websocket,
            string headerName,
            string headerValue
            )
        {
            ResolveXgrHttpClientDllImport();
            IsHttpClientApiSupported();
            Byte[] convertedHeaderName = Converters.StringToNullTerminatedUTF8ByteArray(headerName);
            Byte[] convertedHeaderValue = Converters.StringToNullTerminatedUTF8ByteArray(headerValue);
            Int32 hr = XGRInterop.HCWebSocketSetHeader(websocket.Handle, convertedHeaderName, convertedHeaderValue);
            return hr;
        }

        public static Int32 HCWebSocketConnectAsync(
            string uri,
            string subProtocol,
            HCWebsocketHandle websocket,
            HCSocketCompletionResultFunction completionRoutine
            )
        {
            ResolveXgrHttpClientDllImport();
            IsHttpClientApiSupported();
            XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
            {
                Interop.WebSocketCompletionResult result = new WebSocketCompletionResult();
                Int32 hresult = XGRInterop.HCGetWebSocketConnectResult(block, ref result);
                if (hresult == HR.S_OK)
                {
                    completionRoutine(websocket, result.errorCode, result.platformErrorCode);
                }
                else
                {
                    // error happened when we tried to read the result, pass the error to the user code
                    completionRoutine(null, hresult, 0);
                }
            });

            Byte[] convertedUri = Converters.StringToNullTerminatedUTF8ByteArray(uri);
            Byte[] convertedSubProtocol = Converters.StringToNullTerminatedUTF8ByteArray(subProtocol);
            Int32 hr = XGRInterop.HCWebSocketConnectAsync(convertedUri, convertedSubProtocol, websocket.Handle, asyncBlock);
            return hr;
        }

        public static Int32 HCWebSocketSendMessageAsync(
            HCWebsocketHandle websocket,
            string message,
            HCSocketCompletionResultFunction completionRoutine
            )
        {
            ResolveXgrHttpClientDllImport();
            IsHttpClientApiSupported();
            XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
            {
                Interop.WebSocketCompletionResult result = new WebSocketCompletionResult();
                Int32 hresult = XGRInterop.HCGetWebSocketSendMessageResult(block, ref result);
                if (hresult == HR.S_OK)
                {
                    completionRoutine(websocket, result.errorCode, result.platformErrorCode);
                }
                else
                {
                    // error happened when we tried to read the result, pass the error to the user code
                    completionRoutine(null, hresult, 0);
                }
            });

            Byte[] convertedMessage = Converters.StringToNullTerminatedUTF8ByteArray(message);

            Int32 hr = XGRInterop.HCWebSocketSendMessageAsync(websocket.Handle, convertedMessage, asyncBlock);

            return hr;
        }

        public static Int32 HCWebSocketSendBinaryMessageAsync(
            HCWebsocketHandle websocket,
            Byte[] data,
            UInt32 payloadSize,
            HCSocketCompletionResultFunction completionRoutine
            )
        {
            ResolveXgrHttpClientDllImport();
            IsHttpClientApiSupported();
            XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
            {
                Interop.WebSocketCompletionResult result = new WebSocketCompletionResult();
                Int32 hresult = XGRInterop.HCGetWebSocketSendMessageResult(block, ref result);
                if (hresult == HR.S_OK)
                {
                    completionRoutine(websocket, result.errorCode, result.platformErrorCode);
                }
                else
                {
                    // error happened when we tried to read the result, pass the error to the user code
                    completionRoutine(null, hresult, 0);
                }
            });

            Int32 hr = XGRInterop.HCWebSocketSendBinaryMessageAsync(websocket.Handle, data, payloadSize, asyncBlock);
            return hr;
        }

        public static Int32 HCWebSocketDisconnect(
            HCWebsocketHandle websocket
            )
        {
            ResolveXgrHttpClientDllImport();
            IsHttpClientApiSupported();
            Int32 hr = XGRInterop.HCWebSocketDisconnect(websocket.Handle);
            return hr;
        }

        public static Int32 HCWebSocketCloseHandle(
            HCWebsocketHandle websocket
            )
        {
            ResolveXgrHttpClientDllImport();
            IsHttpClientApiSupported();
            return XGRInterop.HCWebSocketCloseHandle(websocket.Handle);

        }

        [Obsolete("This method is deprecated and will be removed. Use the HCWebSocketRouted event instead.", false)]
        public static Int32 HCAddWebSocketRoutedHandler(HCWebSocketRoutedHandler handler)
        {
            if (routedCallback == null)
            {
                ResolveXgrHttpClientDllImport();
                hcRoutedHandlerId = XGRInterop.HCAddWebSocketRoutedHandler(HCWebSocketRoutedCallback, IntPtr.Zero);
            }
                
            routedCallback += handler;
            return hcRoutedHandlerId;
        }

        [Obsolete("This method is deprecated and will be removed. Use the HCWebSocketRouted event instead.", false)]
        public static void HCRemoveWebSocketRoutedHandler(Int32 handlerId)
        {
            ResolveXgrHttpClientDllImport();
            XGRInterop.HCRemoveWebSocketRoutedHandler(handlerId);
        }

        private static HCWebSocketRoutedHandler routedCallback;
        private static Int32 hcRoutedHandlerId;

        public static event HCWebSocketRoutedHandler HCWebSocketRouted
        {
            add
            {
                if (routedCallback == null)
                {
                    ResolveXgrHttpClientDllImport();
                    hcRoutedHandlerId = XGRInterop.HCAddWebSocketRoutedHandler(HCWebSocketRoutedCallback, IntPtr.Zero);
                }
                routedCallback += value;
            }
            remove
            {
                routedCallback -= value;
                if (routedCallback == null)
                {
                    ResolveXgrHttpClientDllImport();
                    XGRInterop.HCRemoveWebSocketRoutedHandler(hcRoutedHandlerId);
                    hcRoutedHandlerId = 0;
                }
            }
        }

        private static void IsHttpClientApiSupported()
        {
#if (UNITY_STANDALONE_WIN)
            if (GetGdkEdition() < 240600)
            {
                throw new NotImplementedException(
                    "HTTP Client API requires GDK edition 240600 or newer. " +
                    $"Current edition: {GetGdkEdition()}");
            }
#endif
        }

        private static void ResolveXgrHttpClientDllImport()
        {
            if (XGRInterop.NativeXgrHttpClient == null)
            {
                if (GetGdkEdition() < 260400)
                {
                    XGRInterop.NativeXgrHttpClient = new XGRInterop.XgrHttpClient();
                    return;
                }
            
                XGRInterop.NativeXgrHttpClient = new XGRInterop.XgrHttpClient2();
            }
        }
    }
}