using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    // typedef void (CALLBACK HCTraceCallback) (_In_z_ const char* areaName, _In_ HCTraceLevel level, _In_ uint64_t threadId, _In_ uint64_t timestamp, _In_z_ const char* message);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void HCTraceCallback(
        Byte[] areaName,
        HCTraceLevel level,
        UInt64 threadId,
        UInt64 timestamp,
        Byte[] message
        );

    internal partial class XblInterop
    {
        public static IXblHttpClient NativeXblHttpClient;
        
        internal interface IXblHttpClient
        {
            int HCSettingsSetTraceLevel(HCTraceLevel traceLevel);
            int HCSettingsGetTraceLevel(out HCTraceLevel traceLevel);
            void HCTraceSetClientCallback(HCTraceCallback callback);
            void HCTraceSetTraceToDebugger(bool traceToDebugger);
        }
        
        
        internal static Int32 HCSettingsSetTraceLevel(HCTraceLevel traceLevel) => 
            NativeXblHttpClient.HCSettingsSetTraceLevel(traceLevel);
        
        internal static Int32 HCSettingsGetTraceLevel(out HCTraceLevel traceLevel) => 
            NativeXblHttpClient.HCSettingsGetTraceLevel(out traceLevel);
        
        internal static void HCTraceSetClientCallback(HCTraceCallback callback) => 
            NativeXblHttpClient.HCTraceSetClientCallback(callback);
        
        internal static void HCTraceSetTraceToDebugger(bool traceToDebugger) => 
            NativeXblHttpClient.HCTraceSetTraceToDebugger(traceToDebugger);
    }
}
