using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblMultiplayerSessionMember
    //{
    //    uint32_t MemberId;
    //    XBL_DEPRECATED const char* TeamId;
    //    const char* InitialTeam;
    //    XBL_DEPRECATED XblTournamentArbitrationStatus ArbitrationStatus;
    //    uint64_t Xuid;
    //    const char* CustomConstantsJson;
    //    const char* SecureDeviceBaseAddress64;
    //    const XblMultiplayerSessionMemberRole* Roles;
    //    size_t RolesCount;
    //    const char* CustomPropertiesJson;
    //    char Gamertag[XBL_GAMERTAG_CHAR_SIZE];
    //    XblMultiplayerSessionMemberStatus Status;
    //    bool IsTurnAvailable;
    //    bool IsCurrentUser;
    //    bool InitializeRequested;
    //    const char* MatchmakingResultServerMeasurementsJson;
    //    const char* ServerMeasurementsJson;
    //    const uint32_t* MembersInGroupIds;
    //    size_t MembersInGroupCount;
    //    const char* QosMeasurementsJson;
    //    XblDeviceToken DeviceToken;
    //    XblNetworkAddressTranslationSetting Nat;
    //    uint32_t ActiveTitleId;
    //    uint32_t InitializationEpisode;
    //    time_t JoinTime;
    //    XblMultiplayerMeasurementFailure InitializationFailureCause;
    //    const char** Groups;
    //    size_t GroupsCount;
    //    const char** Encounters;
    //    size_t EncountersCount;
    //    XBL_DEPRECATED XblMultiplayerSessionReference TournamentTeamSessionReference;
    //    void* Internal;
    //} XblMultiplayerSessionMember;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblMultiplayerSessionMember
    {
        internal UInt32 MemberId;
        internal UTF8StringPtr TeamId;
        internal UTF8StringPtr InitialTeam;
        internal XblTournamentArbitrationStatus ArbitrationStatus;
        internal UInt64 Xuid;
        internal UTF8StringPtr CustomConstantsJson;
        internal UTF8StringPtr SecureDeviceBaseAddress64;
        private readonly IntPtr Roles;
        private readonly SizeT RolesCount;
        internal UTF8StringPtr CustomPropertiesJson;
        private unsafe fixed Byte Gamertag[XblInterop.XBL_GAMERTAG_CHAR_SIZE];
        internal XblMultiplayerSessionMemberStatus Status;
        [MarshalAs(UnmanagedType.U1)]
        internal bool IsTurnAvailable;
        [MarshalAs(UnmanagedType.U1)]
        internal bool IsCurrentUser;
        [MarshalAs(UnmanagedType.U1)]
        internal bool InitializeRequested;
        internal UTF8StringPtr MatchmakingResultServerMeasurementsJson;
        internal UTF8StringPtr ServerMeasurementsJson;
        private readonly IntPtr MembersInGroupIds;
        private readonly SizeT MembersInGroupCount;
        internal UTF8StringPtr QosMeasurementsJson;
        internal XblDeviceToken DeviceToken;
        internal XblNetworkAddressTranslationSetting Nat;
        internal UInt32 ActiveTitleId;
        internal UInt32 InitializationEpisode;
        internal TimeT JoinTime;
        internal XblMultiplayerMeasurementFailure InitializationFailureCause;
        private readonly IntPtr Groups;
        private readonly SizeT GroupsCount;
        private readonly IntPtr Encounters;
        private readonly SizeT EncountersCount;
        internal XblMultiplayerSessionReference TournamentTeamSessionReference;
        internal readonly IntPtr Internal;

        internal string GetGamertag() { unsafe { fixed (Byte* ptr = this.Gamertag) { return Converters.BytePointerToString(ptr, XblInterop.XBL_MODERN_GAMERTAG_CHAR_SIZE); } } }
        internal T[] GetRoles<T>(Func<XblMultiplayerSessionMemberRole, T> ctor) => Converters.PtrToClassArray(this.Roles, this.RolesCount, ctor);
        internal T[] GetMembersInGroupIds<T>(Func<UInt32, T> ctor) { unsafe { return Converters.PtrToClassArray<T, UInt32>((IntPtr)this.MembersInGroupIds, this.MembersInGroupCount, ctor); } }
        internal string[] GetGroups() => Converters.PtrToClassArray<string, UTF8StringPtr>(this.Groups, this.GroupsCount, s => s.GetString());
        internal string[] GetEncounters() => Converters.PtrToClassArray<string, UTF8StringPtr>(this.Encounters, this.EncountersCount, s => s.GetString());
    }
}
