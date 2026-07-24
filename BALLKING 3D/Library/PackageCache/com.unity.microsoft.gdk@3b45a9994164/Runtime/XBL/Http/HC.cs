using System;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime

{
    public delegate void HCTraceCallback(Byte[] areaName, HCTraceLevel level, UInt64 threadId, UInt64 timestamp, Byte[] message);

    public partial class SDK
    {
        public partial class XBL
        {
            public static Int32 HCSettingsSetTraceLevel(
                HCTraceLevel traceLevel
                )
            {
                ResolveXblHttpClientDllImport();
                IsHttpClientApiSupported();
                return XblInterop.HCSettingsSetTraceLevel(traceLevel);
            }

            public static Int32 HCSettingsGetTraceLevel(
                out HCTraceLevel traceLevel
                )
            {
                ResolveXblHttpClientDllImport();
                IsHttpClientApiSupported();
                return XblInterop.HCSettingsGetTraceLevel(out traceLevel);
            }
            
            public static void HCTraceSetTraceToDebugger(
                bool traceToDebugger
                )
            {
                ResolveXblHttpClientDllImport();
                IsHttpClientApiSupported();
                XblInterop.HCTraceSetTraceToDebugger(traceToDebugger);
            }
            
            private static void ResolveXblHttpClientDllImport()
            {
                if (XblInterop.NativeXblHttpClient == null)
                {
                    if (GetGdkEdition() < 260400)
                    {
                        XblInterop.NativeXblHttpClient = new XblInterop.XblHttpClient(); 
                        return;
                    }
            
                    XblInterop.NativeXblHttpClient = new XblInterop.XblHttpClient2();
                }
            }
        }
    }
}
