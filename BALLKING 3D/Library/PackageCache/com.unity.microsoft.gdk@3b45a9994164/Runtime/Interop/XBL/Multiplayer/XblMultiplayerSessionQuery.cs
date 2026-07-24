using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblMultiplayerSessionQuery
    //{
    //    char Scid[XBL_SCID_LENGTH];
    //    uint32_t MaxItems;
    //    bool IncludePrivateSessions;
    //    bool IncludeReservations;
    //    bool IncludeInactiveSessions;
    //    uint64_t* XuidFilters;
    //    size_t XuidFiltersCount;
    //    const char* KeywordFilter;
    //    char SessionTemplateNameFilter[XBL_MULTIPLAYER_SESSION_TEMPLATE_NAME_MAX_LENGTH];
    //    XblMultiplayerSessionVisibility VisibilityFilter;
    //    uint32_t ContractVersionFilter;
    //}
    //XblMultiplayerSessionQuery;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblMultiplayerSessionQuery
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = XblInterop.XBL_SCID_LENGTH)]
        internal readonly byte[] Scid;
        UInt32 MaxItems;
        [MarshalAs(UnmanagedType.U1)]
        internal bool IncludePrivateSessions;
        [MarshalAs(UnmanagedType.U1)]
        internal bool IncludeReservations;
        [MarshalAs(UnmanagedType.U1)]
        internal bool IncludeInactiveSessions;
        private readonly IntPtr XuidFilters;
        SizeT XuidFiltersCount;
        internal UTF8StringPtr KeywordFilter;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = XblInterop.XBL_MULTIPLAYER_SESSION_TEMPLATE_NAME_MAX_LENGTH)]
        internal readonly byte[] SessionTemplateNameFilter;
        XblMultiplayerSessionVisibility VisibilityFilter;
        UInt32 ContractVersionFilter;

        internal XblMultiplayerSessionQuery(Unity.XGamingRuntime.XblMultiplayerSessionQuery publicObject, DisposableCollection disposableCollection)
        {
            this.Scid = Converters.StringToNullTerminatedUTF8ByteArray(publicObject.Scid ?? "", XblInterop.XBL_SCID_LENGTH);
            this.MaxItems = publicObject.MaxItems;
            this.IncludePrivateSessions = publicObject.IncludePrivateSessions;
            this.IncludeReservations = publicObject.IncludeReservations;
            this.IncludeInactiveSessions = publicObject.IncludeInactiveSessions;
            this.XuidFilters = Converters.ClassArrayToPtr(publicObject.XuidFilters, (x, dc) => x, disposableCollection, out this.XuidFiltersCount);
            this.KeywordFilter = new UTF8StringPtr(publicObject.KeywordFilter, disposableCollection);
            this.SessionTemplateNameFilter = Converters.StringToNullTerminatedUTF8ByteArray(publicObject.SessionTemplateNameFilter ?? "", XblInterop.XBL_MULTIPLAYER_SESSION_TEMPLATE_NAME_MAX_LENGTH);
            this.VisibilityFilter = publicObject.VisibilityFilter;
            this.ContractVersionFilter = publicObject.ContractVersionFilter;
        }
    }
}
