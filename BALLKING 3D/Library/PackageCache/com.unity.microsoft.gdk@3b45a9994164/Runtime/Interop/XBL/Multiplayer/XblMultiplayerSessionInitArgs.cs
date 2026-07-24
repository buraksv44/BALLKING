using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblMultiplayerSessionInitArgs
    //{
    //    uint32_t MaxMembersInSession;
    //    XblMultiplayerSessionVisibility Visibility;
    //    const uint64_t* InitiatorXuids;
    //    size_t InitiatorXuidsCount;
    //    _Null_terminated_ const char* CustomJson;
    //}
    //XblMultiplayerSessionInitArgs;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblMultiplayerSessionInitArgs
    {
        internal readonly UInt32 MaxMembersInSession;
        internal readonly XblMultiplayerSessionVisibility Visibility;
        private readonly IntPtr InitiatorXuids;
        internal readonly SizeT InitiatorXuidsCount;
        internal readonly UTF8StringPtr CustomJson;

        internal XblMultiplayerSessionInitArgs(Unity.XGamingRuntime.XblMultiplayerSessionInitArgs publicObject, DisposableCollection disposableCollection)
        {
            this.MaxMembersInSession = publicObject.MaxMembersInSession;
            this.Visibility = publicObject.Visibility;
            this.InitiatorXuids = Converters.ClassArrayToPtr(publicObject.InitiatorXuids, (x, dc) => x, disposableCollection, out this.InitiatorXuidsCount);
            this.CustomJson = new UTF8StringPtr(publicObject.CustomJson, disposableCollection);
        }
    }
}