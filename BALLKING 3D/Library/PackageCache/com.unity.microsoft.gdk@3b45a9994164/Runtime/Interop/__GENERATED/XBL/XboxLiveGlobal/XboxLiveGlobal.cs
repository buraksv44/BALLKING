using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    public static unsafe partial class XboxLiveGlobal
    {
        [DllImport(XblInterop.XblThunkDllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("HRESULT")]
        public static extern int XblGetScid([NativeTypeName("const char **")] sbyte** scid);
    }
}
