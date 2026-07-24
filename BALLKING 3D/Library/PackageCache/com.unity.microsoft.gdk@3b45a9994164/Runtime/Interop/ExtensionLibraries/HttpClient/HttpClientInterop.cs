using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef void
    //(CALLBACK* HCWebSocketMessageFunction) (
    //     _In_ HCWebsocketHandle websocket,
    //     _In_z_ const char* incomingBodyString,
    //     _In_ void* functionContext
    //    );
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void HCWebSocketMessageFunction(IntPtr websocket, IntPtr incomingBodyString, IntPtr functionContext);

    //typedef void
    //(CALLBACK* HCWebSocketBinaryMessageFunction) (
    //     _In_ HCWebsocketHandle websocket,
    //     _In_reads_bytes_(payloadSize) const uint8_t* payloadBytes,
    //     _In_ uint32_t payloadSize,
    //     _In_ void* functionContext
    //    );
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void HCWebSocketBinaryMessageFunction(IntPtr websocket, IntPtr payloadBytes, UInt32 payloadSize, IntPtr functionContext);

    //typedef void
    //(CALLBACK* HCWebSocketCloseEventFunction) (
    //     _In_ HCWebsocketHandle websocket,
    //     _In_ HCWebSocketCloseStatus closeStatus,
    //     _In_ void* functionContext
    //    );
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void HCWebSocketCloseEventFunction(IntPtr websocket, HCWebSocketCloseStatus closeStatus, IntPtr functionContext);

    //typedef void
    //(STDAPIVCALLTYPE* HCWebSocketRoutedHandler) (
    //     _In_ HCWebsocketHandle websocket,
    //     _In_ bool receiving,
    //     _In_opt_z_ const char* message,
    //     _In_opt_ const uint8_t* payloadBytes,
    //     _In_ size_t payloadSize,
    //     _In_opt_ void* context
    //    );
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void HCWebSocketRoutedHandler(IntPtr websocket, NativeBool receiving, [Optional] IntPtr message, [Optional] IntPtr payloadBytes, SizeT payloadSize, IntPtr conext);

    // Interop definitions for functions defined in httpClient.h
    partial class XGRInterop
    {
        public static IXgrHttpClient NativeXgrHttpClient;
        
        internal interface IXgrHttpClient
        {
            int HCInitialize(IntPtr args);
            int HCCleanupAsync(XAsyncBlockPtr asyncBlock);
        
            int HCWebSocketCreate(out IntPtr websocket, [Optional] HCWebSocketMessageFunction messageFunc,[Optional] HCWebSocketBinaryMessageFunction binaryMessageFunc, [Optional] HCWebSocketCloseEventFunction closeFunc, IntPtr functionContext);
            int HCWebSocketSetProxyUri(IntPtr websocket, byte[] proxyUri);
            int HCWebSocketSetHeader(IntPtr websocket, byte[] headerName, byte[] headerValue);
            int HCWebSocketGetEventFunctions(IntPtr websocket,[Optional] out HCWebSocketMessageFunction messageFunc, [Optional] out HCWebSocketBinaryMessageFunction binaryMessageFunc,[Optional] out HCWebSocketCloseEventFunction closeFunc, out IntPtr functionContext);
        
            int HCWebSocketConnectAsync(byte[] uri, byte[] subProtocol, IntPtr websocket, XAsyncBlockPtr asyncBlock);
            int HCGetWebSocketConnectResult(XAsyncBlockPtr asyncBlock, [In] ref WebSocketCompletionResult result);
        
            int HCWebSocketSendMessageAsync(IntPtr websocket, byte[] message, XAsyncBlockPtr asyncBlock);
            int HCWebSocketSendBinaryMessageAsync(IntPtr websocket, byte[] data, uint payloadSize, XAsyncBlockPtr asyncBlock);
            int HCGetWebSocketSendMessageResult(XAsyncBlockPtr asyncBlock, [In] ref WebSocketCompletionResult result);
        
            int HCWebSocketDisconnect(IntPtr websocket);
            IntPtr HCWebSocketDuplicateHandle(IntPtr websocket);
            int HCWebSocketCloseHandle(IntPtr websocket);
        
            int HCAddWebSocketRoutedHandler(HCWebSocketRoutedHandler handler, IntPtr context);
            void HCRemoveWebSocketRoutedHandler(int handlerId);
        }
        
        
        public static Int32 HCInitialize(IntPtr args) =>
            NativeXgrHttpClient.HCInitialize(args);
        
        public static Int32 HCCleanupAsync(XAsyncBlockPtr asyncBlock) => 
            NativeXgrHttpClient.HCCleanupAsync(asyncBlock);
        
        public static Int32 HCWebSocketCreate(out IntPtr websocket, [Optional] HCWebSocketMessageFunction messageFunc, [Optional] HCWebSocketBinaryMessageFunction binaryMessageFunc, [Optional] HCWebSocketCloseEventFunction closeFunc, IntPtr functionContext) => 
            NativeXgrHttpClient.HCWebSocketCreate(out websocket, messageFunc, binaryMessageFunc, closeFunc, functionContext);
        
        public static Int32 HCWebSocketSetProxyUri(IntPtr websocket, Byte[] proxyUri) => 
            NativeXgrHttpClient.HCWebSocketSetProxyUri(websocket, proxyUri);
        
        public static Int32 HCWebSocketSetHeader(IntPtr websocket, Byte[] headerName, Byte[] headerValue) => 
            NativeXgrHttpClient.HCWebSocketSetHeader(websocket, headerName, headerValue);
        
        public static Int32 HCWebSocketGetEventFunctions(IntPtr websocket, [Optional] out HCWebSocketMessageFunction messageFunc, [Optional] out HCWebSocketBinaryMessageFunction binaryMessageFunc, [Optional] out HCWebSocketCloseEventFunction closeFunc, out IntPtr functionContext) => 
            NativeXgrHttpClient.HCWebSocketGetEventFunctions(websocket, out messageFunc, out binaryMessageFunc, out closeFunc, out functionContext);
        
        public static Int32 HCWebSocketConnectAsync(Byte[] uri, Byte[] subProtocol, IntPtr websocket, XAsyncBlockPtr asyncBlock) => 
            NativeXgrHttpClient.HCWebSocketConnectAsync(uri, subProtocol, websocket, asyncBlock);
        
        public static Int32 HCGetWebSocketConnectResult(XAsyncBlockPtr asyncBlock, [In] ref WebSocketCompletionResult result) => 
            NativeXgrHttpClient.HCGetWebSocketConnectResult(asyncBlock, ref result);
        
        public static Int32 HCWebSocketSendMessageAsync(IntPtr websocket, Byte[]  message, XAsyncBlockPtr asyncBlock) => 
            NativeXgrHttpClient.HCWebSocketSendMessageAsync(websocket, message, asyncBlock);
        
        public static Int32 HCWebSocketSendBinaryMessageAsync(IntPtr websocket, Byte[] data, UInt32 payloadSize, XAsyncBlockPtr asyncBlock) => 
            NativeXgrHttpClient.HCWebSocketSendBinaryMessageAsync(websocket, data, payloadSize, asyncBlock);
        
        public static Int32 HCGetWebSocketSendMessageResult(XAsyncBlockPtr asyncBlock, [In] ref WebSocketCompletionResult result) => 
            NativeXgrHttpClient.HCGetWebSocketSendMessageResult(asyncBlock, ref result);
        
        public static Int32 HCWebSocketDisconnect(IntPtr websocket) => 
            NativeXgrHttpClient.HCWebSocketDisconnect(websocket);
        
        public static IntPtr HCWebSocketDuplicateHandle(IntPtr websocket) => 
            NativeXgrHttpClient.HCWebSocketDuplicateHandle(websocket);
        
        public static Int32 HCWebSocketCloseHandle(IntPtr websocket) =>
            NativeXgrHttpClient.HCWebSocketCloseHandle(websocket);
        
        public static Int32 HCAddWebSocketRoutedHandler(HCWebSocketRoutedHandler handler, IntPtr conext) =>
            NativeXgrHttpClient.HCAddWebSocketRoutedHandler(handler, conext);
        
        public static void HCRemoveWebSocketRoutedHandler(Int32 handlerId) => 
            NativeXgrHttpClient.HCRemoveWebSocketRoutedHandler(handlerId);
    }
}
