using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    partial class XGRInterop
    {
        /// <summary>
        /// Handle native DllImport for `libHttpClient.dll` provided by GDK editions >= 260400
        /// </summary>
        internal class XgrHttpClient2 : IXgrHttpClient
        {
#if (!UNITY_STANDALONE_WIN && ENABLE_IL2CPP)
            private const string k_dllName = "__Internal";
#else
            private const string k_dllName = "libHttpClient";
#endif
            
            public int HCInitialize(IntPtr args) => HCInitialize_Native(args);
            public int HCCleanupAsync(XAsyncBlockPtr asyncBlock) => HCCleanupAsync_Native(asyncBlock);
            public int HCWebSocketCreate(out IntPtr websocket, [Optional] HCWebSocketMessageFunction messageFunc, [Optional] HCWebSocketBinaryMessageFunction binaryMessageFunc, [Optional] HCWebSocketCloseEventFunction closeFunc, IntPtr functionContext) => HCWebSocketCreate_Native(out websocket, messageFunc, binaryMessageFunc, closeFunc, functionContext);
            public int HCWebSocketSetProxyUri(IntPtr websocket, byte[] proxyUri) => HCWebSocketSetProxyUri_Native(websocket, proxyUri);
            public int HCWebSocketSetHeader(IntPtr websocket, byte[] headerName, byte[] headerValue) => HCWebSocketSetHeader_Native(websocket, headerName, headerValue);
            public int HCWebSocketGetEventFunctions(IntPtr websocket, [Optional] out HCWebSocketMessageFunction messageFunc, [Optional] out HCWebSocketBinaryMessageFunction binaryMessageFunc, [Optional] out HCWebSocketCloseEventFunction closeFunc, out IntPtr functionContext) => HCWebSocketGetEventFunctions_Native(websocket, out messageFunc, out binaryMessageFunc, out closeFunc, out functionContext);
            public int HCWebSocketConnectAsync(byte[] uri, byte[] subProtocol, IntPtr websocket, XAsyncBlockPtr asyncBlock) => HCWebSocketConnectAsync_Native(uri, subProtocol, websocket, asyncBlock);
            public int HCGetWebSocketConnectResult(XAsyncBlockPtr asyncBlock,[In] ref WebSocketCompletionResult result) => HCGetWebSocketConnectResult_Native(asyncBlock, ref result);
            public int HCWebSocketSendMessageAsync(IntPtr websocket, byte[] message, XAsyncBlockPtr asyncBlock) => HCWebSocketSendMessageAsync_Native(websocket, message, asyncBlock);
            public int HCWebSocketSendBinaryMessageAsync(IntPtr websocket, byte[] data, uint payloadSize, XAsyncBlockPtr asyncBlock) => HCWebSocketSendBinaryMessageAsync_Native(websocket, data, payloadSize, asyncBlock);
            public int HCGetWebSocketSendMessageResult(XAsyncBlockPtr asyncBlock, [In] ref WebSocketCompletionResult result) => HCGetWebSocketSendMessageResult_Native(asyncBlock, ref result);
            public int HCWebSocketDisconnect(IntPtr websocket) => HCWebSocketDisconnect_Native(websocket);
            public IntPtr HCWebSocketDuplicateHandle(IntPtr websocket) => HCWebSocketDuplicateHandle_Native(websocket);
            public int HCWebSocketCloseHandle(IntPtr websocket) => HCWebSocketCloseHandle_Native(websocket);
            public int HCAddWebSocketRoutedHandler(HCWebSocketRoutedHandler handler, IntPtr context) => HCAddWebSocketRoutedHandler_Native(handler, context);
            public void HCRemoveWebSocketRoutedHandler(int handlerId) => HCRemoveWebSocketRoutedHandler_Native(handlerId);
            
            // STDAPI HCInitialize(_In_opt_ HCInitArgs* args) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCInitialize", CallingConvention = CallingConvention.StdCall)]
            private static extern int HCInitialize_Native(IntPtr args);
            
            // STDAPI HCCleanupAsync(XAsyncBlock* async) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCCleanupAsync", CallingConvention = CallingConvention.StdCall)]
            private static extern int HCCleanupAsync_Native(XAsyncBlockPtr asyncBlock);
            
            //STDAPI HCWebSocketCreate(
            //    _Out_ HCWebsocketHandle* websocket,
            //    _In_opt_ HCWebSocketMessageFunction messageFunc,
            //    _In_opt_ HCWebSocketBinaryMessageFunction binaryMessageFunc,
            //    _In_opt_ HCWebSocketCloseEventFunction closeFunc,
            //    _In_opt_ void* functionContext
            //    ) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCWebSocketCreate", CallingConvention = CallingConvention.StdCall)]
            private static extern int HCWebSocketCreate_Native(out IntPtr websocket, [Optional] HCWebSocketMessageFunction messageFunc,[Optional] HCWebSocketBinaryMessageFunction binaryMessageFunc, [Optional] HCWebSocketCloseEventFunction closeFunc, IntPtr functionContext);
            
            //STDAPI HCWebSocketSetProxyUri(
            //    _In_ HCWebsocketHandle websocket,
            //    _In_z_ const char* proxyUri
            //    ) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCWebSocketSetProxyUri", CallingConvention = CallingConvention.StdCall)]
            private static extern int HCWebSocketSetProxyUri_Native(IntPtr websocket, byte[] proxyUri);
            
            //STDAPI HCWebSocketSetHeader(
            //    _In_ HCWebsocketHandle websocket,
            //    _In_z_ const char* headerName,
            //    _In_z_ const char* headerValue
            //    ) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCWebSocketSetHeader", CallingConvention = CallingConvention.StdCall)]
            private static extern int HCWebSocketSetHeader_Native(IntPtr websocket, byte[] headerName, byte[] headerValue);
            
            //STDAPI HCWebSocketGetEventFunctions(
            //    _In_ HCWebsocketHandle websocket,
            //    _Out_opt_ HCWebSocketMessageFunction* messageFunc,
            //    _Out_opt_ HCWebSocketBinaryMessageFunction* binaryMessageFunc,
            //    _Out_opt_ HCWebSocketCloseEventFunction* closeFunc,
            //    _Out_ void** functionContext
            //    ) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCWebSocketGetEventFunctions", CallingConvention = CallingConvention.StdCall)]
            private static extern int HCWebSocketGetEventFunctions_Native(IntPtr websocket, [Optional] out HCWebSocketMessageFunction messageFunc, [Optional] out HCWebSocketBinaryMessageFunction binaryMessageFunc, [Optional] out HCWebSocketCloseEventFunction closeFunc, out IntPtr functionContext);
            
            //STDAPI HCWebSocketConnectAsync(
            //    _In_z_ const char* uri,
            //    _In_z_ const char* subProtocol,
            //    _In_ HCWebsocketHandle websocket,
            //    _Inout_ XAsyncBlock* asyncBlock
            //    ) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCWebSocketConnectAsync", CallingConvention = CallingConvention.StdCall)]
            private static extern int HCWebSocketConnectAsync_Native(byte[] uri, byte[] subProtocol, IntPtr websocket, XAsyncBlockPtr asyncBlock);
            
            //STDAPI HCGetWebSocketConnectResult(
            //    _Inout_ XAsyncBlock* asyncBlock,
            //    _In_ WebSocketCompletionResult* result
            //    ) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCGetWebSocketConnectResult", CallingConvention = CallingConvention.StdCall)]
            private static extern int HCGetWebSocketConnectResult_Native(XAsyncBlockPtr asyncBlock, [In] ref WebSocketCompletionResult result);
            
            //STDAPI HCWebSocketSendMessageAsync(
            //    _In_ HCWebsocketHandle websocket,
            //    _In_z_ const char* message,
            //    _Inout_ XAsyncBlock* asyncBlock
            //    ) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCWebSocketSendMessageAsync", CallingConvention = CallingConvention.StdCall)]
            private static extern int HCWebSocketSendMessageAsync_Native(IntPtr websocket, byte[] message, XAsyncBlockPtr asyncBlock);
            
            //STDAPI HCWebSocketSendBinaryMessageAsync(
            //    _In_ HCWebsocketHandle websocket,
            //    _In_reads_bytes_(payloadSize) const uint8_t* payloadBytes,
            //    _In_ uint32_t payloadSize,
            //    _Inout_ XAsyncBlock* asyncBlock
            //    ) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCWebSocketSendBinaryMessageAsync", CallingConvention = CallingConvention.StdCall)]
            private static extern int HCWebSocketSendBinaryMessageAsync_Native(IntPtr websocket, byte[] data, uint payloadSize, XAsyncBlockPtr asyncBlock);
            
            //STDAPI HCGetWebSocketSendMessageResult(
            //    _Inout_ XAsyncBlock* asyncBlock,
            //    _In_ WebSocketCompletionResult* result
            //    ) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCGetWebSocketSendMessageResult", CallingConvention = CallingConvention.StdCall)]
            private static extern int HCGetWebSocketSendMessageResult_Native(XAsyncBlockPtr asyncBlock, [In] ref WebSocketCompletionResult result);
            
            //STDAPI HCWebSocketDisconnect(
            //    _In_ HCWebsocketHandle websocket
            //    ) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCWebSocketDisconnect", CallingConvention = CallingConvention.StdCall)]
            private static extern int HCWebSocketDisconnect_Native(IntPtr websocket);
            
            //STDAPI_(HCWebsocketHandle) HCWebSocketDuplicateHandle(
            //    _In_ HCWebsocketHandle websocket
            //    ) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCWebSocketDuplicateHandle", CallingConvention = CallingConvention.StdCall)]
            private static extern IntPtr HCWebSocketDuplicateHandle_Native(IntPtr websocket);
            
            //STDAPI HCWebSocketCloseHandle(
            //    _In_ HCWebsocketHandle websocket
            //    ) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCWebSocketCloseHandle", CallingConvention = CallingConvention.StdCall)]
            private static extern int HCWebSocketCloseHandle_Native(IntPtr websocket);
            
            //STDAPI_(int32_t) HCAddWebSocketRoutedHandler(
            //    _In_ HCWebSocketRoutedHandler handler,
            //    _In_opt_ void* context
            //    ) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCAddWebSocketRoutedHandler", CallingConvention = CallingConvention.StdCall)]
            private static extern int HCAddWebSocketRoutedHandler_Native(HCWebSocketRoutedHandler handler, IntPtr context);
            
            //STDAPI_(void) HCRemoveWebSocketRoutedHandler(
            //    _In_ int32_t handlerId
            //    ) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCRemoveWebSocketRoutedHandler", CallingConvention = CallingConvention.StdCall)]
            private static extern void HCRemoveWebSocketRoutedHandler_Native(int handlerId);
        }
    }
}