using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    // typedef bool CALLBACK XErrorCallback(_In_ HRESULT hr, _In_z_ const char* msg, _In_opt_ void* context);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    internal delegate bool XErrorCallback(Int32 hr, [MarshalAs(UnmanagedType.LPStr)] string msg, IntPtr context);

    partial class NativeMethods
    {
        // STDAPI_(void) XErrorSetCallback(_In_opt_ XErrorCallback* callback, _In_opt_ void* context) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XErrorSetCallback(XErrorCallback callback, IntPtr context);

        // STDAPI_(void) XErrorSetOptions(
        // _In_ XErrorOptions optionsDebuggerPresent,
        // _In_ XErrorOptions optionsDebuggerNotPresent
        // ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XErrorSetOptions(XErrorOptions optionsDebuggerPresent, XErrorOptions optionsDebuggerNotPresent);
    }
}
