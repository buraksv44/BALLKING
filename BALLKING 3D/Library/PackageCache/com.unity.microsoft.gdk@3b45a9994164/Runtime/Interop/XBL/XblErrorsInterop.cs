using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    internal partial class XblInterop
    {
        //STDAPI_(XblErrorCondition) XblGetErrorCondition(
        //    _In_ HRESULT hr
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern XblErrorCondition XblGetErrorCondition(Int32 hr);
    }
}
