using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    // typedef struct WebSocketCompletionResult
    // {
    //     HCWebsocketHandle websocket;
    //     HRESULT errorCode;
    //     uint32_t platformErrorCode;
    // }
    // WebSocketCompletionResult;
    [StructLayout(LayoutKind.Sequential)]
    internal struct WebSocketCompletionResult
    {
        internal readonly IntPtr websocket;
        internal readonly Int32 errorCode;
        internal readonly UInt32 platformErrorCode;
    }
}
