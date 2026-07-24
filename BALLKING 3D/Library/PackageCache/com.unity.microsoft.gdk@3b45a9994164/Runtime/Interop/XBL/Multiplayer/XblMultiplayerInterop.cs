using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    internal partial class XblInterop
    {
        //STDAPI_(XblMultiplayerSessionHandle) XblMultiplayerSessionCreateHandle(
        //    _In_ uint64_t xuid,
        //    _In_opt_ const XblMultiplayerSessionReference* sessionReference,
        //    _In_opt_ const XblMultiplayerSessionInitArgs* initArgs
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern XblMultiplayerSessionHandle XblMultiplayerSessionCreateHandle(
            UInt64 xboxUserId,
            [In] ref XblMultiplayerSessionReference sessionRef,
            [In] ref XblMultiplayerSessionInitArgs initArgs
            );

        //STDAPI_(void) XblMultiplayerSessionCloseHandle(
        //    _In_ XblMultiplayerSessionHandle handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void XblMultiplayerSessionCloseHandle(
            IntPtr handle
            );

        //STDAPI_(time_t) XblMultiplayerSessionTimeOfSession(
        //    _In_ XblMultiplayerSessionHandle handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern TimeT XblMultiplayerSessionTimeOfSession(
            IntPtr handle
            );

        //STDAPI_(const XblMultiplayerSessionInitializationInfo*) XblMultiplayerSessionGetInitializationInfo(
        //    _In_ XblMultiplayerSessionHandle handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal unsafe static extern XblMultiplayerSessionInitializationInfo * XblMultiplayerSessionGetInitializationInfo(
            IntPtr handle
            );

        //STDAPI_(XblMultiplayerSessionChangeTypes) XblMultiplayerSessionSubscribedChangeTypes(
        //    _In_ XblMultiplayerSessionHandle handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern XblMultiplayerSessionChangeTypes XblMultiplayerSessionSubscribedChangeTypes(
            IntPtr handle
            );

        //STDAPI XblMultiplayerSessionHostCandidates(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _Out_ const XblDeviceToken** deviceTokens,
        //    _Out_ size_t* deviceTokensCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSessionHostCandidates(
            IntPtr handle,
            out IntPtr deviceTokens,
            out SizeT deviceTokensCount
            );

        //STDAPI_(const XblMultiplayerSessionReference*) XblMultiplayerSessionSessionReference(
        //    _In_ XblMultiplayerSessionHandle handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal unsafe static extern XblMultiplayerSessionReference* XblMultiplayerSessionSessionReference(
            IntPtr handle
            );

        //STDAPI_(const XblMultiplayerSessionConstants*) XblMultiplayerSessionSessionConstants(
        //    _In_ XblMultiplayerSessionHandle handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal unsafe static extern XblMultiplayerSessionConstants* XblMultiplayerSessionSessionConstants(
            IntPtr handle
            );

        //STDAPI_(void) XblMultiplayerSessionConstantsSetMaxMembersInSession(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    uint32_t maxMembersInSession
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void XblMultiplayerSessionConstantsSetMaxMembersInSession(
            IntPtr handle,
            UInt32 maxMembersInSession
            );

        //STDAPI_(void) XblMultiplayerSessionConstantsSetVisibility(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_ XblMultiplayerSessionVisibility visibility
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void XblMultiplayerSessionConstantsSetVisibility(
            IntPtr handle,
            XblMultiplayerSessionVisibility visibility
            );

        //STDAPI XblMultiplayerSessionConstantsSetTimeouts(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_ uint64_t memberReservedTimeout,
        //    _In_ uint64_t memberInactiveTimeout,
        //    _In_ uint64_t memberReadyTimeout,
        //    _In_ uint64_t sessionEmptyTimeout
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSessionConstantsSetTimeouts(
            IntPtr handle,
            UInt64 memberReservedTimeout,
            UInt64 memberInactiveTimeout,
            UInt64 memberReadyTimeout,
            UInt64 sessionEmptyTimeout
            );

        //STDAPI XblMultiplayerSessionConstantsSetQosConnectivityMetrics(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_ bool enableLatencyMetric,
        //    _In_ bool enableBandwidthDownMetric,
        //    _In_ bool enableBandwidthUpMetric,
        //    _In_ bool enableCustomMetric
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSessionConstantsSetQosConnectivityMetrics(
            IntPtr handle,
            NativeBool enableLatencyMetric,
            NativeBool enableBandwidthDownMetric,
            NativeBool enableBandwidthUpMetric,
            NativeBool enableCustomMetric
            );

        //STDAPI XblMultiplayerSessionConstantsSetMemberInitialization(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_ XblMultiplayerMemberInitialization memberInitialization
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSessionConstantsSetMemberInitialization(
            IntPtr handle,
            XblMultiplayerMemberInitialization memberInitialization
            );

        //STDAPI XblMultiplayerSessionConstantsSetPeerToPeerRequirements(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_ XblMultiplayerPeerToPeerRequirements requirements
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSessionConstantsSetPeerToPeerRequirements(
            IntPtr handle,
            XblMultiplayerPeerToPeerRequirements requirements
            );

        //STDAPI XblMultiplayerSessionConstantsSetMeasurementServerAddressesJson(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_ const char* measurementServerAddressesJson
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSessionConstantsSetMeasurementServerAddressesJson(
            IntPtr handle,
            Byte[] measurementServerAddressesJson
            );

        //STDAPI XblMultiplayerSessionConstantsSetCapabilities(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_ XblMultiplayerSessionCapabilities capabilities
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal unsafe static extern Int32 XblMultiplayerSessionConstantsSetCapabilities(
            IntPtr handle,
            XblMultiplayerSessionCapabilities capabilities
            );

        //STDAPI_(const XblMultiplayerSessionProperties*) XblMultiplayerSessionSessionProperties(
        //    _In_ XblMultiplayerSessionHandle handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal unsafe static extern XblMultiplayerSessionProperties* XblMultiplayerSessionSessionProperties(
            IntPtr handle
            );

        //STDAPI XblMultiplayerSessionPropertiesSetKeywords(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_ const char** keywords,
        //    _In_ size_t keywordsCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal unsafe static extern Int32 XblMultiplayerSessionPropertiesSetKeywords(
            IntPtr handle,
            IntPtr keywords,
            SizeT keywordsCount
            );

        //STDAPI_(void) XblMultiplayerSessionPropertiesSetJoinRestriction(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_ XblMultiplayerSessionRestriction joinRestriction
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void XblMultiplayerSessionPropertiesSetJoinRestriction(
            IntPtr handle,
            XblMultiplayerSessionRestriction joinRestriction
            );

        //STDAPI_(void) XblMultiplayerSessionPropertiesSetReadRestriction(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_ XblMultiplayerSessionRestriction readRestriction
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void XblMultiplayerSessionPropertiesSetReadRestriction(
            IntPtr handle,
            XblMultiplayerSessionRestriction readRestriction
            );

        //STDAPI XblMultiplayerSessionPropertiesSetTurnCollection(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_ const uint32_t* turnCollectionMemberIds,
        //    _In_ size_t turnCollectionMemberIdsCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal unsafe static extern Int32 XblMultiplayerSessionPropertiesSetTurnCollection(
            IntPtr handle,
            [In] UInt32[] turnCollectionMemberIds,
            SizeT turnCollectionMemberIdsCount
            );

        //STDAPI XblMultiplayerSessionMembers(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _Out_ const XblMultiplayerSessionMember** members,
        //    _Out_ size_t* membersCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal unsafe static extern Int32 XblMultiplayerSessionMembers(
            IntPtr handle,
            out IntPtr members,
            out SizeT membersCount
            );

        //STDAPI_(const XblMultiplayerMatchmakingServer*) XblMultiplayerSessionMatchmakingServer(
        //    _In_ XblMultiplayerSessionHandle handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal unsafe static extern XblMultiplayerMatchmakingServer* XblMultiplayerSessionMatchmakingServer(
         IntPtr handle
         );

        //STDAPI_(const XblMultiplayerSessionMember*) XblMultiplayerSessionCurrentUser(
        //    _In_ XblMultiplayerSessionHandle handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal unsafe static extern XblMultiplayerSessionMember* XblMultiplayerSessionCurrentUser(
            IntPtr handle
            );


        //STDAPI_(XblWriteSessionStatus) XblMultiplayerSessionWriteStatus(
        //    _In_ XblMultiplayerSessionHandle handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern XblWriteSessionStatus XblMultiplayerSessionWriteStatus(
            IntPtr handle
            );

        //STDAPI XblMultiplayerSessionJoin(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_opt_z_ const char* memberCustomConstantsJson,
        //    _In_ bool initializeRequested,
        //    _In_ bool joinWithActiveStatus
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSessionJoin(
            IntPtr handle,
            byte[] memberCustomConstantsJson,
            [MarshalAs(UnmanagedType.U1)] bool initializeRequested,
            [MarshalAs(UnmanagedType.U1)] bool joinWithActiveStatus
            );

        //STDAPI_(void) XblMultiplayerSessionSetHostDeviceToken(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_ XblDeviceToken hostDeviceToken
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void XblMultiplayerSessionSetHostDeviceToken(
            IntPtr handle,
            XblDeviceToken hostDeviceToken
            );

        //STDAPI_(void) XblMultiplayerSessionSetClosed(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_ bool closed
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void XblMultiplayerSessionSetClosed(
            IntPtr handle,
            [MarshalAs(UnmanagedType.U1)] bool closed
            );

        //STDAPI XblMultiplayerSessionSetSessionChangeSubscription(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_ XblMultiplayerSessionChangeTypes changeTypes
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSessionSetSessionChangeSubscription(
            IntPtr handle,
            XblMultiplayerSessionChangeTypes changeTypes
            );

        //STDAPI XblMultiplayerSessionLeave(
        //    _In_ XblMultiplayerSessionHandle handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSessionLeave(
            IntPtr handle
            );

        //STDAPI XblMultiplayerSessionCurrentUserSetRoles(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_ const XblMultiplayerSessionMemberRole* roles,
        //    _In_ size_t rolesCount
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSessionCurrentUserSetRoles(
            IntPtr handle,
            IntPtr roles,
            SizeT rolesCount
        );
        //STDAPI XblMultiplayerSessionCurrentUserSetEncounters(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_reads_(encountersCount) const char** encounters,
        //    _In_ size_t encountersCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSessionCurrentUserSetEncounters(
            IntPtr handle,
            IntPtr encounters,
            SizeT encountersCount
            );

        //STDAPI XblMultiplayerSessionCurrentUserSetMembersInGroup(
        //    _In_ XblMultiplayerSessionHandle session,
        //    _In_reads_(memberIdsCount) uint32_t* memberIds,
        //    _In_ size_t memberIdsCount
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSessionCurrentUserSetMembersInGroup(
            IntPtr handle,
            UInt32[] memberIds,
            SizeT memberIdsCount
        );

        //STDAPI XblMultiplayerSessionCurrentUserSetGroups(
        //     _In_ XblMultiplayerSessionHandle handle,
        //     _In_reads_(groupsCount) const char** groups,
        //     _In_ size_t groupsCount
        //     ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSessionCurrentUserSetGroups(
            IntPtr handle,
            IntPtr groups,
            SizeT groupsCount
            );

        //STDAPI XblMultiplayerSessionCurrentUserSetCustomPropertyJson(
        //     _In_ XblMultiplayerSessionHandle handle,
        //     _In_z_ const char* name,
        //     _In_z_ const char* valueJson
        //     ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSessionCurrentUserSetCustomPropertyJson(
            IntPtr handle,
            byte[] name,
            byte[] valueJson
        );

        //STDAPI XblMultiplayerSessionCurrentUserDeleteCustomPropertyJson(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_z_ const char* name
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSessionCurrentUserDeleteCustomPropertyJson(
            IntPtr handle,
            byte[] name
        );

        //STDAPI XblMultiplayerSessionCurrentUserSetStatus(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_ XblMultiplayerSessionMemberStatus status
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSessionCurrentUserSetStatus(
            IntPtr handle,
            XblMultiplayerSessionMemberStatus status
            );

        //STDAPI XblMultiplayerSessionCurrentUserSetSecureDeviceAddressBase64(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_ const char* value
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSessionCurrentUserSetSecureDeviceAddressBase64(
            IntPtr handle,
            Byte[] value
            );

        //STDAPI XblFormatSecureDeviceAddress(
        //    _In_ const char* deviceId,
        //    _Out_ XblFormattedSecureDeviceAddress* address
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblFormatSecureDeviceAddress(
            Byte[] deviceId,
            out XblFormattedSecureDeviceAddress address
            );

        //STDAPI XblMultiplayerSearchHandleDuplicateHandle(
        //    _In_ XblMultiplayerSearchHandle handle,
        //    _Out_ XblMultiplayerSearchHandle* duplicatedHandle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSearchHandleDuplicateHandle(
            [In] XblMultiplayerSearchHandle handle,
            out XblMultiplayerSearchHandle duplicatedHandle
            );

        //STDAPI_(void) XblMultiplayerSearchHandleCloseHandle(
        //    _In_ XblMultiplayerSearchHandle handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void XblMultiplayerSearchHandleCloseHandle(IntPtr handle);

        //STDAPI XblMultiplayerSearchHandleGetSessionReference(
        //    _In_ XblMultiplayerSearchHandle handle,
        //    _Out_ XblMultiplayerSessionReference* sessionRef
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSearchHandleGetSessionReference(
            [In] IntPtr handle,
            out XblMultiplayerSessionReference sessionRef
            );

        //STDAPI XblMultiplayerSearchHandleGetId(
        //    _In_ XblMultiplayerSearchHandle handle,
        //    _Out_ const char** id
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSearchHandleGetId(
            [In] IntPtr handle,
            out UTF8StringPtr id
            );

        //STDAPI XblMultiplayerSearchHandleGetSessionOwnerXuids(
        //    _In_ XblMultiplayerSearchHandle handle,
        //    _Out_ const uint64_t** xuids,
        //    _Out_ size_t* xuidsCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSearchHandleGetSessionOwnerXuids(
            [In] IntPtr handle,
            out IntPtr xuids,
            out SizeT xuidsCount
            );

        //STDAPI XblMultiplayerSearchHandleGetTags(
        //    _In_ XblMultiplayerSearchHandle handle,
        //    _Out_ const XblMultiplayerSessionTag** tags,
        //    _Out_ size_t* tagsCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSearchHandleGetTags(
            [In] IntPtr handle,
            out IntPtr tags,
            out SizeT tagsCount
            );

        //STDAPI XblMultiplayerSearchHandleGetStringAttributes(
        //    _In_ XblMultiplayerSearchHandle handle,
        //    _Out_ const XblMultiplayerSessionStringAttribute** attributes,
        //    _Out_ size_t* attributesCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSearchHandleGetStringAttributes(
            [In] IntPtr handle,
            out IntPtr attributes,
            out SizeT attributesCount
            );

        //STDAPI XblMultiplayerSearchHandleGetNumberAttributes(
        //    _In_ XblMultiplayerSearchHandle handle,
        //    _Out_ const XblMultiplayerSessionNumberAttribute** attributes,
        //    _Out_ size_t* attributesCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSearchHandleGetNumberAttributes(
            [In] IntPtr handle,
            out IntPtr attributes,
            out SizeT attributesCount
            );

        //STDAPI XblMultiplayerSearchHandleGetVisibility(
        //    _In_ XblMultiplayerSearchHandle handle,
        //    _Out_ XblMultiplayerSessionVisibility* visibility
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSearchHandleGetVisibility(
            [In] IntPtr handle,
            out XblMultiplayerSessionVisibility visibility
            );

        //STDAPI XblMultiplayerSearchHandleGetJoinRestriction(
        //    _In_ XblMultiplayerSearchHandle handle,
        //    _Out_ XblMultiplayerSessionRestriction* joinRestriction
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSearchHandleGetJoinRestriction(
            [In] IntPtr handle,
            out XblMultiplayerSessionRestriction joinRestriction
            );

        //STDAPI XblMultiplayerSearchHandleGetSessionClosed(
        //    _In_ XblMultiplayerSearchHandle handle,
        //    _Out_ bool* closed
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSearchHandleGetSessionClosed(
            [In] IntPtr handle,
            [MarshalAs(UnmanagedType.U1)] out bool closed
            );

        //STDAPI XblMultiplayerSearchHandleGetMemberCounts(
        //    _In_ XblMultiplayerSearchHandle handle,
        //    _Out_opt_ size_t* maxMembers,
        //    _Out_opt_ size_t* currentMembers
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSearchHandleGetMemberCounts(
            [In] IntPtr handle,
            out SizeT maxMembers,
            out SizeT currentMembers
            );

        //STDAPI XblMultiplayerSearchHandleGetCreationTime(
        //    _In_ XblMultiplayerSearchHandle handle,
        //    _Out_ time_t* creationTime
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSearchHandleGetCreationTime(
            [In] IntPtr handle,
            out TimeT creationTime
            );

        //STDAPI XblMultiplayerSearchHandleGetCustomSessionPropertiesJson(
        //    _In_ XblMultiplayerSearchHandle handle,
        //    _Out_ const char** customPropertiesJson
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSearchHandleGetCustomSessionPropertiesJson(
            [In] IntPtr handle,
            out UTF8StringPtr customPropertiesJson
            );

        //STDAPI XblMultiplayerWriteSessionAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_ XblMultiplayerSessionHandle multiplayerSession,
        //    _In_ XblMultiplayerSessionWriteMode writeMode,
        //    _Inout_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerWriteSessionAsync(
            IntPtr xblContext,
            IntPtr multiplayerSession,
            XblMultiplayerSessionWriteMode writeMode,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerWriteSessionResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XblMultiplayerSessionHandle* handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerWriteSessionResult(
            XAsyncBlockPtr async,
            out XblMultiplayerSessionHandle handle
            );

        //STDAPI XblMultiplayerWriteSessionByHandleAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_ XblMultiplayerSessionHandle multiplayerSession,
        //    _In_ XblMultiplayerSessionWriteMode writeMode,
        //    _In_ const char* handleId,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerWriteSessionByHandleAsync(
            IntPtr xblContext,
            IntPtr multiplayerSession,
            XblMultiplayerSessionWriteMode writeMode,
            Byte[] handleId,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerWriteSessionByHandleResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XblMultiplayerSessionHandle* handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerWriteSessionByHandleResult(
            XAsyncBlockPtr async,
            out XblMultiplayerSessionHandle handle
            );

        //STDAPI XblMultiplayerGetSessionAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_ const XblMultiplayerSessionReference* sessionReference,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerGetSessionAsync(
            IntPtr xblContext,
            [In] ref XblMultiplayerSessionReference sessionRef,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerGetSessionResult(
        //    _In_ XAsyncBlock* async,
        //    _Out_ XblMultiplayerSessionHandle* handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerGetSessionResult(
            XAsyncBlockPtr async,
            out XblMultiplayerSessionHandle handle
            );

        //STDAPI XblMultiplayerGetSessionByHandleAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_ const char* handleId,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerGetSessionByHandleAsync(
            IntPtr xblContext,
            Byte[] handleId,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerGetSessionByHandleResult(
        //    _In_ XAsyncBlock* async,
        //    _Out_ XblMultiplayerSessionHandle* handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerGetSessionByHandleResult(
            XAsyncBlockPtr async,
            out XblMultiplayerSessionHandle handle
            );

        //STDAPI XblMultiplayerQuerySessionsAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_ const XblMultiplayerSessionQuery* sessionQuery,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerQuerySessionsAsync(
            IntPtr xblContext,
            [In] ref XblMultiplayerSessionQuery sessionQuery,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerQuerySessionsResultCount(
        //    _In_ XAsyncBlock* async,
        //    _Out_ size_t* sessionCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerQuerySessionsResultCount(
            XAsyncBlockPtr async,
            out SizeT sessionCount
            );

        //STDAPI XblMultiplayerQuerySessionsResult(
        //    _In_ XAsyncBlock* async,
        //    _In_ size_t sessionCount,
        //    _Out_writes_(sessionCount) XblMultiplayerSessionQueryResult* sessions
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerQuerySessionsResult(
            XAsyncBlockPtr async,
            SizeT sessionCount,
            [Out] XblMultiplayerSessionQueryResult[] sessions
            );

        //STDAPI XblMultiplayerSetActivityAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_ const XblMultiplayerSessionReference* sessionReference,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerSetActivityAsync(
            IntPtr xblContext,
            [In] ref XblMultiplayerSessionReference sessionReference,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerClearActivityAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_z_ const char* scid,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerClearActivityAsync(
            IntPtr xblContext,
            Byte[] scid,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerSetTransferHandleAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_ XblMultiplayerSessionReference targetSessionReference,
        //    _In_ XblMultiplayerSessionReference originSessionReference,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerSetTransferHandleAsync(
            IntPtr xblContext,
            [In] ref XblMultiplayerSessionReference targetSessionReference,
            [In] ref XblMultiplayerSessionReference originSessionReference,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerSetTransferHandleResult(
        //    _In_ XAsyncBlock* async,
        //    _Out_ XblMultiplayerSessionHandleId* handleId
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSetTransferHandleResult(
            XAsyncBlockPtr async,
            out XblMultiplayerSessionHandleId handle);

        //STDAPI XblMultiplayerCreateSearchHandleAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_ const XblMultiplayerSessionReference* sessionRef,
        //    _In_reads_opt_(tagsCount) const XblMultiplayerSessionTag* tags,
        //    _In_ size_t tagsCount,
        //    _In_reads_opt_(numberAttributesCount) const XblMultiplayerSessionNumberAttribute* numberAttributes,
        //    _In_ size_t numberAttributesCount,
        //    _In_reads_opt_(stringAttributesCount) const XblMultiplayerSessionStringAttribute* stringAttributes,
        //    _In_ size_t stringAttributesCount,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerCreateSearchHandleAsync(
            IntPtr xblContext,
            [In] ref XblMultiplayerSessionReference sessionRef,
            [Optional] XblMultiplayerSessionTag[] tags,
            SizeT tagsCount,
            [Optional] XblMultiplayerSessionNumberAttribute[] numberAttributes,
            SizeT numberAttributesCount,
            [Optional] XblMultiplayerSessionStringAttribute[] stringAttributes,
            SizeT stringAttributesCount,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerCreateSearchHandleResult(
        //    _In_ XAsyncBlock* async,
        //    _Out_opt_ XblMultiplayerSearchHandle* handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerCreateSearchHandleResult(XAsyncBlockPtr async, out XblMultiplayerSearchHandle handle);

        //STDAPI XblMultiplayerDeleteSearchHandleAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_ const char* handleId,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerDeleteSearchHandleAsync(
            IntPtr xblContext,
            Byte[] handleId,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerGetSearchHandlesAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_z_ const char* scid,
        //    _In_z_ const char* sessionTemplateName,
        //    _In_opt_z_ const char* orderByAttribute,
        //    _In_ bool orderAscending,
        //    _In_opt_z_ const char* searchFilter,
        //    _In_opt_z_ const char* socialGroup,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerGetSearchHandlesAsync(
            IntPtr xblContext,
            Byte[] scid,
            Byte[] sessionTemplateName,
            [Optional] Byte[] orderByAttribute,
            [MarshalAs(UnmanagedType.U1)] bool orderAscending,
            [Optional] Byte[] searchFilter,
            [Optional] Byte[] socialGroup,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerGetSearchHandlesResultCount(
        //    _In_ XAsyncBlock* async,
        //    _Out_ size_t* searchHandleCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerGetSearchHandlesResultCount(
            XAsyncBlockPtr async,
            out SizeT searchHandleCount
            );

        //STDAPI XblMultiplayerGetSearchHandlesResult(
        //    _In_ XAsyncBlock* async,
        //    _Out_writes_(searchHandlesCount) XblMultiplayerSearchHandle* searchHandles,
        //    _In_ size_t searchHandlesCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerGetSearchHandlesResult(
            XAsyncBlockPtr async,
            [Out] XblMultiplayerSearchHandle[] searchHandles,
            SizeT searchHandleCount
            );

        //STDAPI XblMultiplayerSendInvitesAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_ const XblMultiplayerSessionReference* sessionReference,
        //    _In_ const uint64_t* xuids,
        //    _In_ size_t xuidsCount,
        //    _In_ uint32_t titleId,
        //    _In_opt_z_ const char* contextStringId,
        //    _In_opt_z_ const char* customActivationContext,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSendInvitesAsync(
            IntPtr xblContext,
            [In] ref XblMultiplayerSessionReference sessionRef,
            [In] UInt64[] xuids,
            SizeT xuidsCount,
            UInt32 titleId,
            [Optional] byte[] contextStringId,
            [Optional] byte[] customActivationContext,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerSendInvitesResult(
        //    _In_ XAsyncBlock* async,
        //    _In_ size_t handlesCount,
        //    _Out_writes_(handlesCount) XblMultiplayerInviteHandle* handles
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSendInvitesResult(
            XAsyncBlockPtr async,
            SizeT handlesCount,
            [Out] XblMultiplayerInviteHandle[] handles
            );

        //STDAPI XblMultiplayerGetActivitiesForSocialGroupAsync(
        //    _In_ XblContextHandle xboxLiveContext,
        //    _In_ const char* scid,
        //    _In_ uint64_t socialGroupOwnerXuid,
        //    _In_ const char* socialGroup,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerGetActivitiesForSocialGroupAsync(
            IntPtr xboxLiveContext,
            Byte[] scid,
            UInt64 socialGroupOwnerXuid,
            Byte[] socialGroup,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerGetActivitiesWithPropertiesForSocialGroupAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_ const char* scid,
        //    _In_ uint64_t socialGroupOwnerXuid,
        //    _In_ const char* socialGroup,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerGetActivitiesWithPropertiesForSocialGroupAsync(
            IntPtr xboxLiveContext,
            Byte[] scid,
            UInt64 socialGroupOwnerXuid,
            Byte[] socialGroup,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerGetActivitiesForSocialGroupResultCount(
        //    _In_ XAsyncBlock* async,
        //    _Out_ size_t* activityCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerGetActivitiesForSocialGroupResultCount(
            XAsyncBlockPtr async,
            out SizeT activityCount
            );

        //STDAPI XblMultiplayerGetActivitiesForSocialGroupResult(
        //    _In_ XAsyncBlock* async,
        //    _In_ size_t activityCount,
        //    _Out_writes_(activityCount) XblMultiplayerActivityDetails* activities
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerGetActivitiesForSocialGroupResult(
            XAsyncBlockPtr async,
            SizeT activityCount,
            [Out] XblMultiplayerActivityDetails[] activities
            );

        //STDAPI XblMultiplayerGetActivitiesWithPropertiesForSocialGroupResultSize(
        //    _In_ XAsyncBlock* async,
        //    _Out_ size_t* resultSizeInBytes
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerGetActivitiesWithPropertiesForSocialGroupResultSize(
            XAsyncBlockPtr async,
            out SizeT resultSizeInBytes
            );

        //STDAPI XblMultiplayerGetActivitiesWithPropertiesForSocialGroupResult(
        //    _In_ XAsyncBlock* async,
        //    _In_ size_t bufferSize,
        //    _Out_writes_bytes_to_(bufferSize, * bufferUsed) void* buffer,
        //    _Outptr_ XblMultiplayerActivityDetails** ptrToBuffer,
        //    _Out_ size_t* ptrToBufferCount,
        //    _Out_opt_ size_t* bufferUsed
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerGetActivitiesWithPropertiesForSocialGroupResult(
            XAsyncBlockPtr async,
            SizeT bufferSize,
            IntPtr buffer,
            out IntPtr ptrToBuffer,
            out SizeT ptrToBufferCount,
            out SizeT bufferUsed
            );

        //STDAPI XblMultiplayerGetActivitiesForUsersAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_ const char* scid,
        //    _In_reads_(xuidsCount) const uint64_t* xuids,
        //    _In_ size_t xuidsCount,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerGetActivitiesForUsersAsync(
            IntPtr xboxLiveContext,
            Byte[] scid,
            [In] UInt64[] xuids,
            SizeT xuidsCount,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerGetActivitiesWithPropertiesForUsersAsync(
        //    _In_ XblContextHandle xblContext,
        //    _In_ const char* scid,
        //    _In_reads_(xuidsCount) const uint64_t* xuids,
        //    _In_ size_t xuidsCount,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerGetActivitiesWithPropertiesForUsersAsync(
            IntPtr xboxLiveContext,
            Byte[] scid,
            [In] UInt64[] xuids,
            SizeT xuidsCount,
            XAsyncBlockPtr async
            );

        //STDAPI XblMultiplayerGetActivitiesForUsersResultCount(
        //    _In_ XAsyncBlock* async,
        //    _Out_ size_t* activityCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerGetActivitiesForUsersResultCount(
            XAsyncBlockPtr async,
            out SizeT activityCount
            );

        //STDAPI XblMultiplayerGetActivitiesForUsersResult(
        //    _In_ XAsyncBlock* async,
        //    _In_ size_t activityCount,
        //    _Out_writes_(activityCount) XblMultiplayerActivityDetails* activities
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerGetActivitiesForUsersResult(
            XAsyncBlockPtr async,
            SizeT activityCount,
            [Out] XblMultiplayerActivityDetails[] activities
            );

        //STDAPI XblMultiplayerGetActivitiesWithPropertiesForUsersResultSize(
        //    _In_ XAsyncBlock* async,
        //    _Out_ size_t* resultSizeInBytes
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerGetActivitiesWithPropertiesForUsersResultSize(
            XAsyncBlockPtr async,
            out SizeT resultSizeInBytes
            );

        //STDAPI XblMultiplayerGetActivitiesWithPropertiesForUsersResult(
        //    _In_ XAsyncBlock* async,
        //    _In_ size_t bufferSize,
        //    _Out_writes_bytes_to_(bufferSize, * bufferUsed) void* buffer,
        //    _Outptr_ XblMultiplayerActivityDetails** ptrToBuffer,
        //    _Out_ size_t* ptrToBufferCount,
        //    _Out_opt_ size_t* bufferUsed
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerGetActivitiesWithPropertiesForUsersResult(
            XAsyncBlockPtr async,
            SizeT bufferSize,
            IntPtr buffer,
            out IntPtr ptrToBuffer,
            out SizeT ptrToBufferCount,
            out SizeT bufferUsed
            );

        //STDAPI XblMultiplayerSetSubscriptionsEnabled(
        //    _In_ XblContextHandle xblContext,
        //    _In_ bool subscriptionsEnabled
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 XblMultiplayerSetSubscriptionsEnabled(
            IntPtr xblContext,
            [MarshalAs(UnmanagedType.U1)] bool subscriptionsEnabled
            );

        //STDAPI_(bool) XblMultiplayerSubscriptionsEnabled(
        //    _In_ XblContextHandle xblHandle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool XblMultiplayerSubscriptionsEnabled(
            IntPtr xblHandle
            );

        //typedef void CALLBACK XblMultiplayerSessionChangedHandler(
        //    _In_opt_ void* context,
        //    _In_ XblMultiplayerSessionChangeEventArgs args
        //);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void XblMultiplayerSessionChangedHandler(
            IntPtr context,
            XblMultiplayerSessionChangeEventArgs args
            );

        //STDAPI_(XblFunctionContext) XblMultiplayerAddSessionChangedHandler(
        //    _In_ XblContextHandle xblContext,
        //    _In_ XblMultiplayerSessionChangedHandler* handler,
        //    _In_opt_ void* context
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern XblFunctionContext XblMultiplayerAddSessionChangedHandler(
            IntPtr xblContext,
            XblMultiplayerSessionChangedHandler handler,
            IntPtr context
            );

        //STDAPI_(void) XblMultiplayerRemoveSessionChangedHandler(
        //    _In_ XblContextHandle xblContext,
        //    _In_ XblFunctionContext token
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerRemoveSessionChangedHandler(
            IntPtr xblContext,
            XblFunctionContext token
            );

        //typedef void CALLBACK XblMultiplayerSessionSubscriptionLostHandler(
        //    _In_opt_ void* context
        //);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void XblMultiplayerSessionSubscriptionLostHandler(
            IntPtr context
            );

        //STDAPI_(XblFunctionContext) XblMultiplayerAddSubscriptionLostHandler(
        //    _In_ XblContextHandle xblContext,
        //    _In_ XblMultiplayerSessionSubscriptionLostHandler* handler,
        //    _In_opt_ void* context
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern XblFunctionContext XblMultiplayerAddSubscriptionLostHandler(
            IntPtr xblContext,
            XblMultiplayerSessionSubscriptionLostHandler handler,
            IntPtr context
            );

        //STDAPI_(void) XblMultiplayerRemoveSubscriptionLostHandler(
        //    _In_ XblContextHandle xblContext,
        //    _In_ XblFunctionContext token
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerRemoveSubscriptionLostHandler(
            IntPtr xblContext,
            XblFunctionContext token
            );

        //typedef void CALLBACK XblMultiplayerConnectionIdChangedHandler(
        //    _In_opt_ void* context
        //);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void XblMultiplayerConnectionIdChangedHandler(
            IntPtr context
            );

        //STDAPI XblMultiplayerSessionSetCustomPropertyJson(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_z_ const char* name,
        //    _In_z_ const char* valueJson
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerSessionSetCustomPropertyJson(
            IntPtr handle,
            Byte[] name,
            Byte[] valueJson
            );

        //STDAPI XblMultiplayerSessionDeleteCustomPropertyJson(
        //    _In_ XblMultiplayerSessionHandle handle,
        //    _In_z_ const char* name
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerSessionDeleteCustomPropertyJson(
            IntPtr handle,
            Byte[] name
            );

        //STDAPI_(XblMultiplayerSessionChangeTypes) XblMultiplayerSessionCompare(
        //    _In_ XblMultiplayerSessionHandle currentSessionHandle,
        //    _In_ XblMultiplayerSessionHandle oldSessionHandle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern XblMultiplayerSessionChangeTypes XblMultiplayerSessionCompare(
            IntPtr currentSessionHandle,
            IntPtr oldSessionHandle
            );

        //STDAPI_(XblFunctionContext) XblMultiplayerAddConnectionIdChangedHandler(
        //    _In_ XblContextHandle xblContext,
        //    _In_ XblMultiplayerConnectionIdChangedHandler* handler,
        //    _In_opt_ void* context
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern XblFunctionContext XblMultiplayerAddConnectionIdChangedHandler(
            IntPtr xblContext,
            XblMultiplayerConnectionIdChangedHandler handler,
            IntPtr context
            );

        //STDAPI_(void) XblMultiplayerRemoveConnectionIdChangedHandler(
        //    _In_ XblContextHandle xblContext,
        //    _In_ XblFunctionContext token
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblMultiplayerRemoveConnectionIdChangedHandler(
            IntPtr xblContext,
            XblFunctionContext token
            );
    }
}
