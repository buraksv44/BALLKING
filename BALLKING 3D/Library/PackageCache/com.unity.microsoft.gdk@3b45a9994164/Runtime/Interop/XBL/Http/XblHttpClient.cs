using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    internal partial class XblInterop
    {
        /// <summary>
        /// Handle native DllImport for `libHttpClient.GDK.dll` provided by GDK editions < 260000
        /// </summary>
        internal class XblHttpClient : IXblHttpClient
        {
#if (!UNITY_STANDALONE_WIN && ENABLE_IL2CPP)
            private const string k_dllName = "__Internal";
#else
            private const string k_dllName = "libHttpClient.GDK";
#endif           
            
            
            public int HCSettingsSetTraceLevel(HCTraceLevel traceLevel) => 
                HCSettingsSetTraceLevel_Native(traceLevel);
            
            public int HCSettingsGetTraceLevel(out HCTraceLevel traceLevel) => 
                HCSettingsGetTraceLevel_Native(out traceLevel);
            
            public void HCTraceSetClientCallback(HCTraceCallback callback) => 
                HCTraceSetClientCallback_Native(callback);
            
            public void HCTraceSetTraceToDebugger(bool traceToDebugger) => 
                HCTraceSetTraceToDebugger_Native(traceToDebugger);
            
            
            // STDAPI HCSettingsSetTraceLevel(_In_ HCTraceLevel traceLevel) noexcept;            
            [DllImport(k_dllName, EntryPoint = "HCSettingsSetTraceLevel", CallingConvention = CallingConvention.StdCall)]
            private static extern int HCSettingsSetTraceLevel_Native(HCTraceLevel traceLevel);
            
            // STDAPI HCSettingsGetTraceLevel(_Out_ HCTraceLevel* traceLevel) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCSettingsGetTraceLevel", CallingConvention = CallingConvention.StdCall)]
            private static extern int HCSettingsGetTraceLevel_Native(out HCTraceLevel traceLevel);
            
            // STDAPI_(void) HCTraceSetClientCallback(_In_opt_ HCTraceCallback* callback) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCTraceSetClientCallback", CallingConvention = CallingConvention.StdCall)]
            private static extern void HCTraceSetClientCallback_Native(HCTraceCallback callback);
            
            // STDAPI_(void) HCTraceSetTraceToDebugger(_In_ bool traceToDebugger) noexcept;
            [DllImport(k_dllName, EntryPoint = "HCTraceSetTraceToDebugger", CallingConvention = CallingConvention.StdCall)]
            private static extern void HCTraceSetTraceToDebugger_Native([MarshalAs(UnmanagedType.U1)] bool traceToDebugger);
        }
    }
}