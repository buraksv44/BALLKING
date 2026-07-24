using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblMultiplayerSessionProperties
    //{
    //    const char** Keywords;
    //    size_t KeywordCount;
    //    XblMultiplayerSessionRestriction JoinRestriction;
    //    XblMultiplayerSessionRestriction ReadRestriction;
    //    uint32_t* TurnCollection;
    //    size_t TurnCollectionCount;
    //    const char* MatchmakingTargetSessionConstantsJson;
    //    const char* SessionCustomPropertiesJson;
    //    const char* MatchmakingServerConnectionString;
    //    const char** ServerConnectionStringCandidates;
    //    size_t ServerConnectionStringCandidatesCount;
    //    uint32_t* SessionOwnerMemberIds;
    //    size_t SessionOwnerMemberIdsCount;
    //    XblDeviceToken HostDeviceToken;
    //    bool Closed;
    //    bool Locked;
    //    bool AllocateCloudCompute;
    //    bool MatchmakingResubmit;
    //}
    //XblMultiplayerSessionProperties;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblMultiplayerSessionProperties
    {
        internal readonly IntPtr Keywords;
        internal readonly SizeT KeywordCount;
        internal XblMultiplayerSessionRestriction JoinRestriction;
        internal XblMultiplayerSessionRestriction ReadRestriction;
        private readonly IntPtr TurnCollection;
        private readonly SizeT TurnCollectionCount;
        internal readonly UTF8StringPtr MatchmakingTargetSessionConstantsJson;
        internal readonly UTF8StringPtr SessionCustomPropertiesJson;
        internal readonly UTF8StringPtr MatchmakingServerConnectionString;
        private readonly IntPtr ServerConnectionStringCandidates;
        private readonly SizeT ServerConnectionStringCandidatesCount;
        private readonly IntPtr SessionOwnerMemberIds;
        private readonly SizeT SessionOwnerMemberIdsCount;
        internal readonly XblDeviceToken HostDeviceToken;
        [MarshalAs(UnmanagedType.U1)]
        internal bool Closed;
        [MarshalAs(UnmanagedType.U1)]
        internal bool Locked;
        [MarshalAs(UnmanagedType.U1)]
        internal bool AllocateCloudCompute;
        [MarshalAs(UnmanagedType.U1)]
        internal bool MatchmakingResubmit;

        internal string[] GetKeywords() => Converters.PtrToClassArray<string, UTF8StringPtr>(this.Keywords, this.KeywordCount, s => s.GetString());
        internal T[] GetTurnCollection<T>(Func<UInt32, T> ctor) { unsafe { return Converters.PtrToClassArray<T, UInt32>((IntPtr)this.TurnCollection, this.TurnCollectionCount, ctor); } }
        internal string[] GetServerConnectionStringCandidates() => Converters.PtrToClassArray<string, UTF8StringPtr>(this.ServerConnectionStringCandidates, this.ServerConnectionStringCandidatesCount, s => s.GetString());
        internal T[] GetSessionOwnerMemberIds<T>(Func<UInt32, T> ctor) { unsafe { return Converters.PtrToClassArray<T, UInt32>((IntPtr)this.SessionOwnerMemberIds, this.SessionOwnerMemberIdsCount, ctor); } }
    }
}
