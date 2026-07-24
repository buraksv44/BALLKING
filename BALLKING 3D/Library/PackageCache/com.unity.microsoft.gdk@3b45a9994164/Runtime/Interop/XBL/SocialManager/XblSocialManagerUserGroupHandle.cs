using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblSocialManagerUserGroupHandle
    {
        internal readonly IntPtr Handle;
    }
}
