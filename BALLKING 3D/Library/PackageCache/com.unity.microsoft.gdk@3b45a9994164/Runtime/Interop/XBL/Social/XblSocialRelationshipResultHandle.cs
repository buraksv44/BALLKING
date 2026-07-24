using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblSocialRelationshipResult* XblSocialRelationshipResultHandle;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblSocialRelationshipResultHandle
    {
        private readonly IntPtr intPtr;
    }
}
