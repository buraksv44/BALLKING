using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    public static partial class XboxLiveContext
    {
        [DllImport(XblInterop.XblThunkDllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("HRESULT")]
        public static extern int XblContextDuplicateHandle([NativeTypeName("XblContextHandle")] IntPtr xboxLiveContextHandle, [NativeTypeName("XblContextHandle *")] out IntPtr duplicatedHandle);

        [DllImport(XblInterop.XblThunkDllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("HRESULT")]
        public static extern int XblContextGetUser([NativeTypeName("XblContextHandle")] IntPtr context, [NativeTypeName("XblUserHandle *")] out IntPtr user);

        [DllImport(XblInterop.XblThunkDllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("HRESULT")]
        public static extern int XblContextGetXboxUserId([NativeTypeName("XblContextHandle")] IntPtr context, [NativeTypeName("uint64_t *")] out ulong xboxUserId);
    }
}
