using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public partial class SDK
    {
        public partial class XBL
        {
            public delegate void XblMultiplayerWriteSessionHandleResult(Int32 hresult, XblMultiplayerSessionHandle handle);
            public delegate void XblMultiplayerGetSessionHandleResult(Int32 hresult, XblMultiplayerSessionHandle handle);
            public delegate void XblMultiplayerSessionQueryHandleResult(Int32 hresult, XblMultiplayerSessionQueryResult[] sessions);
            public delegate void XblMultiplayerSetActivityHandleResult(Int32 hresult);
            public delegate void XblMultiplayerCreateSearchHandleResult(Int32 hresult, XblMultiplayerSearchHandle handle);
            public delegate void XblMultiplayerSetTransferHandleResult(Int32 hresult, XblMultiplayerSessionHandleId handle);
            public delegate void XblMultiplayerDeleteSearchHandleResult(Int32 hresult);
            public delegate void XblMultiplayerClearActivityHandleResult(Int32 hresult);
            public delegate void XblMultiplayerGetSearchHandlesResult(Int32 hresult, XblMultiplayerSearchHandle[] searchHandles);

            public delegate void XblMultiplayerSessionChangedHandler(XblMultiplayerSessionChangeEventArgs args);
            public delegate void XblMultiplayerSessionSubscriptionLostHandler();
            public delegate void XblMultiplayerConnectionIdChangedHandler();

            public delegate void XblMultiplayerSendInvitesResult(Int32 hresult, XblMultiplayerInviteHandle[] handles);
            public delegate void XblMultiplayerGetActivitiesResult(Int32 hresult, XblMultiplayerActivityDetails[] activities);

            public static XblMultiplayerSessionHandle XblMultiplayerSessionCreateHandle(
                ulong xboxUserId,
                XblMultiplayerSessionReference sessionRef,
                XblMultiplayerSessionInitArgs initArgs
                )
            {
                using (DisposableCollection disposableCollection = new DisposableCollection())
                {
                    var interopSessionRef = new Interop.XblMultiplayerSessionReference(sessionRef);
                    var interopInitArgs = new Interop.XblMultiplayerSessionInitArgs(initArgs, disposableCollection);
                    return new XblMultiplayerSessionHandle(XblInterop.XblMultiplayerSessionCreateHandle(xboxUserId, ref interopSessionRef, ref interopInitArgs));
                }
            }

            public static void XblMultiplayerSessionCloseHandle(
                XblMultiplayerSessionHandle handle
                )
            {
                if (handle != null)
                {
                    handle.Close();
                }
            }

            public static DateTime XblMultiplayerSessionTimeOfSession(
                XblMultiplayerSessionHandle handle
                )
            {
                if (handle == null)
                    return default;

                TimeT interop = XblInterop.XblMultiplayerSessionTimeOfSession(handle.Handle);

                return interop.DateTime;
            }

            public static XblMultiplayerSessionInitializationInfo XblMultiplayerSessionGetInitializationInfo(
                XblMultiplayerSessionHandle handle
                )
            {
                unsafe
                {
                    Interop.XblMultiplayerSessionInitializationInfo* interop = null;
                    if (handle != null)
                    {
                        interop = XblInterop.XblMultiplayerSessionGetInitializationInfo(handle.Handle);
                    }

                    if (interop == null)
                    {
                        return null;
                    }

                    return new XblMultiplayerSessionInitializationInfo(*interop);
                }
            }

            public static XblMultiplayerSessionChangeTypes XblMultiplayerSessionSubscribedChangeTypes(
                XblMultiplayerSessionHandle handle
                )
            {
                if (handle == null)
                    return XblMultiplayerSessionChangeTypes.None;

                return XblInterop.XblMultiplayerSessionSubscribedChangeTypes(handle.Handle);
            }

            public static Int32 XblMultiplayerSessionHostCandidates(
                XblMultiplayerSessionHandle handle,
                out XblDeviceToken[] deviceTokens
                )
            {
                deviceTokens = null;
                if (handle == null)
                    return HR.E_INVALIDARG;

                Int32 hresult = XblInterop.XblMultiplayerSessionHostCandidates(handle.Handle,
                    out IntPtr deviceTokensInterop,
                    out SizeT deviceTokensCount);

                if (HR.SUCCEEDED(hresult))
                {
                    deviceTokens = Converters.PtrToClassArray<XblDeviceToken, Interop.XblDeviceToken>(deviceTokensInterop, deviceTokensCount, x => new XblDeviceToken(x));
                }

                return hresult;
            }

            public static XblMultiplayerSessionReference XblMultiplayerSessionSessionReference(
                XblMultiplayerSessionHandle handle
                )
            {
                unsafe
                {
                    Interop.XblMultiplayerSessionReference* interop = null;
                    if (handle != null)
                    {
                        interop = XblInterop.XblMultiplayerSessionSessionReference(handle.Handle);
                    }

                    if (interop == null)
                    {
                        return null;
                    }

                    return new XblMultiplayerSessionReference(*interop);
                }
            }

            public static XblMultiplayerSessionConstants XblMultiplayerSessionSessionConstants(
                XblMultiplayerSessionHandle handle
                )
            {
                if (handle == null)
                {
                    return null;
                }

                unsafe
                {
                    var interop = XblInterop.XblMultiplayerSessionSessionConstants(handle.Handle);

                    if (interop == null)
                        return null;

                    return new XblMultiplayerSessionConstants(*interop);
                }
            }

            public static void XblMultiplayerSessionConstantsSetMaxMembersInSession(
                XblMultiplayerSessionHandle handle,
                UInt32 maxMembersInSession
                )
            {
                if (handle != null)
                {
                    XblInterop.XblMultiplayerSessionConstantsSetMaxMembersInSession(
                        handle.Handle,
                        maxMembersInSession
                        );
                }
            }

            public static void XblMultiplayerSessionConstantsSetVisibility(
                XblMultiplayerSessionHandle handle,
                XblMultiplayerSessionVisibility visibility
                )
            {
                if (handle != null)
                {
                    XblInterop.XblMultiplayerSessionConstantsSetVisibility(
                        handle.Handle,
                        visibility
                        );
                }
            }

            public static Int32 XblMultiplayerSessionConstantsSetTimeouts(
                XblMultiplayerSessionHandle handle,
                UInt64 memberReservedTimeout,
                UInt64 memberInactiveTimeout,
                UInt64 memberReadyTimeout,
                UInt64 sessionEmptyTimeout
                )
            {
                if (handle == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblMultiplayerSessionConstantsSetTimeouts(
                    handle.Handle,
                    memberReservedTimeout,
                    memberInactiveTimeout,
                    memberReadyTimeout,
                    sessionEmptyTimeout
                    );
            }

            public static Int32 XblMultiplayerSessionConstantsSetQosConnectivityMetrics(
                XblMultiplayerSessionHandle handle,
                bool enableLatencyMetric,
                bool enableBandwidthDownMetric,
                bool enableBandwidthUpMetric,
                bool enableCustomMetric
                )
            {
                if (handle == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblMultiplayerSessionConstantsSetQosConnectivityMetrics(
                    handle.Handle,
                    new NativeBool(enableLatencyMetric),
                    new NativeBool(enableBandwidthDownMetric),
                    new NativeBool(enableBandwidthUpMetric),
                    new NativeBool(enableCustomMetric)
                    );
            }

            public static Int32 XblMultiplayerSessionConstantsSetMemberInitialization(
                XblMultiplayerSessionHandle handle,
                XblMultiplayerMemberInitialization memberInitialization
                )
            {
                if (handle == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblMultiplayerSessionConstantsSetMemberInitialization(
                    handle.Handle,
                    new Interop.XblMultiplayerMemberInitialization(memberInitialization)
                    );
            }

            public static Int32 XblMultiplayerSessionConstantsSetPeerToPeerRequirements(
                XblMultiplayerSessionHandle handle,
                XblMultiplayerPeerToPeerRequirements requirements
                )
            {
                if (handle == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblMultiplayerSessionConstantsSetPeerToPeerRequirements(
                    handle.Handle,
                    new Interop.XblMultiplayerPeerToPeerRequirements(requirements)
                    );
            }

            public static Int32 XblMultiplayerSessionConstantsSetMeasurementServerAddressesJson(
                XblMultiplayerSessionHandle handle,
                string measurementServerAddressesJson
                )
            {
                if (handle == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblMultiplayerSessionConstantsSetMeasurementServerAddressesJson(
                    handle.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(measurementServerAddressesJson)
                    );
            }

            public static Int32 XblMultiplayerSessionConstantsSetCapabilities(
                XblMultiplayerSessionHandle handle,
                XblMultiplayerSessionCapabilities capabilities
                )
            {
                if (handle == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblMultiplayerSessionConstantsSetCapabilities(
                    handle.Handle,
                    capabilities == null? default : new Interop.XblMultiplayerSessionCapabilities(capabilities)
                    );
            }

            public static XblMultiplayerSessionProperties XblMultiplayerSessionSessionProperties(
                XblMultiplayerSessionHandle handle
                )
            {
                if (handle == null)
                {
                    return null;
                }

                unsafe
                {
                    var interop = XblInterop.XblMultiplayerSessionSessionProperties(handle.Handle);

                    if (interop == null)
                        return null;

                    return new XblMultiplayerSessionProperties(*interop);
                }
            }

            public static Int32 XblMultiplayerSessionPropertiesSetKeywords(
                XblMultiplayerSessionHandle handle,
                string[] keywords
                )
            {
                if (handle == null)
                {
                    return HR.E_INVALIDARG;
                }

                using (DisposableBuffer keywordsBuffer = Converters.StringArrayToUTF8StringArray(keywords))
                {
                    return XblInterop.XblMultiplayerSessionPropertiesSetKeywords(handle.Handle, keywordsBuffer.IntPtr, new SizeT(keywords.Length));
                }
            }

            public static void XblMultiplayerSessionPropertiesSetJoinRestriction(
                XblMultiplayerSessionHandle handle,
                XblMultiplayerSessionRestriction joinRestriction
                )
            {
                if (handle == null)
                    return;

                XblInterop.XblMultiplayerSessionPropertiesSetJoinRestriction(handle.Handle, joinRestriction);
            }

            public static void XblMultiplayerSessionPropertiesSetReadRestriction(
                XblMultiplayerSessionHandle handle,
                XblMultiplayerSessionRestriction readRestriction
                )
            {
                if (handle == null)
                    return;

                XblInterop.XblMultiplayerSessionPropertiesSetReadRestriction(handle.Handle, readRestriction);
            }

            public static Int32 XblMultiplayerSessionPropertiesSetTurnCollection(
                XblMultiplayerSessionHandle handle,
                UInt32[] turnCollectionMemberIds
                )
            {
                if (handle != null)
                {
                    return XblInterop.XblMultiplayerSessionPropertiesSetTurnCollection(handle.Handle, turnCollectionMemberIds, new SizeT(turnCollectionMemberIds.Length));
                }

                return HR.E_INVALIDARG;
            }

            public static Int32 XblMultiplayerSessionMembers(
                XblMultiplayerSessionHandle handle,
                out XblMultiplayerSessionMember[] members
                )
            {
                IntPtr interopMembers;
                SizeT membersCount;

                int hr = XblInterop.XblMultiplayerSessionMembers(handle.Handle, out interopMembers, out membersCount);

                if (HR.FAILED(hr) || membersCount.IsZero)
                {
                    members = null;
                    return hr;
                }

                members = Converters.PtrToClassArray<XblMultiplayerSessionMember, Interop.XblMultiplayerSessionMember>(interopMembers, membersCount, (x => new XblMultiplayerSessionMember(x)));
                return hr;
            }

            public static XblMultiplayerMatchmakingServer XblMultiplayerSessionMatchmakingServer(
                XblMultiplayerSessionHandle handle
                )
            {
                if (handle == null)
                {
                    return null;
                }

                unsafe
                {
                    var interop = XblInterop.XblMultiplayerSessionMatchmakingServer(handle.Handle);

                    if (interop == null)
                        return null;

                    return new XblMultiplayerMatchmakingServer(*interop);
                }
            }


            public static XblMultiplayerSessionMember XblMultiplayerSessionCurrentUser(
                XblMultiplayerSessionHandle handle
                )
            {
                if (handle == null)
                {
                    return null;
                }

                unsafe
                {
                    var interop = XblInterop.XblMultiplayerSessionCurrentUser(handle.Handle);

                    if (interop == null)
                        return null;

                    return new XblMultiplayerSessionMember(*interop);
                }
            }

            public static Int32 XblMultiplayerSessionCurrentUserSetRoles(
                XblMultiplayerSessionHandle handle,
                XblMultiplayerSessionMemberRole[] roles
            )
            {
                if (handle == null)
                {
                    return HR.E_INVALIDARG;
                }

                using (DisposableCollection disposableCollection = new DisposableCollection())
                {
                    var rolesBuffer = Converters.ClassArrayToPtr(
                        roles,
                        (role, collection) => new Interop.XblMultiplayerSessionMemberRole( role, disposableCollection),
                        disposableCollection,
                        out var rolesCount);

                    return XblInterop.XblMultiplayerSessionCurrentUserSetRoles(handle.Handle, rolesBuffer, rolesCount);
                }
            }

            public static Int32 XblMultiplayerSessionCurrentUserSetEncounters(
                XblMultiplayerSessionHandle handle,
                string[] encounters
                )
            {
                if (handle == null)
                {
                    return HR.E_INVALIDARG;
                }

                using (DisposableBuffer encounterssBuffer = Converters.StringArrayToUTF8StringArray(encounters))
                {
                    return XblInterop.XblMultiplayerSessionCurrentUserSetEncounters(handle.Handle, encounterssBuffer.IntPtr, new SizeT(encounters.Length));
                }
            }

            public static Int32 XblMultiplayerSessionCurrentUserSetMembersInGroup(
                XblMultiplayerSessionHandle handle,
                UInt32[] memberIds
            )
            {
                if (handle == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblMultiplayerSessionCurrentUserSetMembersInGroup(handle.Handle, memberIds, new SizeT(memberIds.Length));
            }

            public static Int32 XblMultiplayerSessionCurrentUserSetGroups(
                XblMultiplayerSessionHandle handle,
                string[] groups
                )
            {
                if (handle == null)
                {
                    return HR.E_INVALIDARG;
                }

                using (DisposableBuffer groupssBuffer = Converters.StringArrayToUTF8StringArray(groups))
                {
                    return XblInterop.XblMultiplayerSessionCurrentUserSetGroups(handle.Handle, groupssBuffer.IntPtr, new SizeT(groups.Length));
                }
            }

            public static Int32 XblMultiplayerSessionCurrentUserSetCustomPropertyJson(
                XblMultiplayerSessionHandle handle,
                string name,
                string valueJson
            )
            {
                if (handle == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblMultiplayerSessionCurrentUserSetCustomPropertyJson(handle.Handle, Converters.StringToNullTerminatedUTF8ByteArray(name), Converters.StringToNullTerminatedUTF8ByteArray(valueJson));
            }

            public static Int32 XblMultiplayerSessionCurrentUserDeleteCustomPropertyJson(
                XblMultiplayerSessionHandle handle,
                string name
            )
            {
                if (handle == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblMultiplayerSessionCurrentUserDeleteCustomPropertyJson(handle.Handle, Converters.StringToNullTerminatedUTF8ByteArray(name));
            }

            public static XblWriteSessionStatus XblMultiplayerSessionWriteStatus(
                XblMultiplayerSessionHandle handle
                )
            {
                return XblInterop.XblMultiplayerSessionWriteStatus(handle.Handle);
            }

            public static Int32 XblMultiplayerSessionJoin(
                XblMultiplayerSessionHandle handle,
                string memberCustomConstantsJson,
                bool initializeRequested,
                bool joinWithActiveStatus
                )
            {
                return XblInterop.XblMultiplayerSessionJoin(handle.Handle, Converters.StringToNullTerminatedUTF8ByteArray(memberCustomConstantsJson), initializeRequested, joinWithActiveStatus);
            }

            public static void XblMultiplayerSessionSetHostDeviceToken(
                XblMultiplayerSessionHandle handle,
                XblDeviceToken hostDeviceToken
                )
            {
                if (handle == null)
                    return;

                XblInterop.XblMultiplayerSessionSetHostDeviceToken(handle.Handle, new Interop.XblDeviceToken(hostDeviceToken));
            }

            public static void XblMultiplayerSessionSetClosed(
                XblMultiplayerSessionHandle handle,
                bool closed
                )
            {
                if (handle != null)
                {
                    XblInterop.XblMultiplayerSessionSetClosed(handle.Handle, closed);
                }
            }

            //public static extern Int32 XblMultiplayerSessionSetSessionChangeSubscription(
            //    XblMultiplayerSessionHandle handle,
            //    XblMultiplayerSessionChangeTypes changeTypes
            //    );
            public static Int32 XblMultiplayerSessionSetSessionChangeSubscription(
                XblMultiplayerSessionHandle handle,
                XblMultiplayerSessionChangeTypes changeTypes
                )
            {
                if (handle != null)
                {
                    return XblInterop.XblMultiplayerSessionSetSessionChangeSubscription(handle.Handle, changeTypes);
                }

                return HR.E_INVALIDARG;
            }

            public static Int32 XblMultiplayerSessionLeave(
                XblMultiplayerSessionHandle handle
                )
            {
                if (handle != null)
                {
                    return XblInterop.XblMultiplayerSessionLeave(handle.Handle);
                }

                return HR.E_INVALIDARG;
            }

            public static Int32 XblMultiplayerSessionCurrentUserSetStatus(
                XblMultiplayerSessionHandle handle,
                XblMultiplayerSessionMemberStatus status
                )
            {
                if (handle != null)
                {
                    return XblInterop.XblMultiplayerSessionCurrentUserSetStatus(handle.Handle, status);
                }

                return HR.E_INVALIDARG;
            }

            public static Int32 XblMultiplayerSessionCurrentUserSetSecureDeviceAddressBase64(
                XblMultiplayerSessionHandle handle,
                string value
                )
            {
                if (handle != null)
                {
                    return XblInterop.XblMultiplayerSessionCurrentUserSetSecureDeviceAddressBase64(handle.Handle, Converters.StringToNullTerminatedUTF8ByteArray(value));
                }

                return HR.E_INVALIDARG;
            }

            public static Int32 XblFormatSecureDeviceAddress(
                string deviceId,
                out string address
                )
            {
                if (deviceId != null)
                {
                    Interop.XblFormattedSecureDeviceAddress result;
                    Int32 hr = XblInterop.XblFormatSecureDeviceAddress(Converters.StringToNullTerminatedUTF8ByteArray(deviceId), out result);
                    address = result.GetValue();
                    return hr;
                }

                address = null;
                return HR.E_INVALIDARG;
            }

            public static void XblMultiplayerSearchHandleCloseHandle(XblMultiplayerSearchHandle handle)
            {
                if (handle != null)
                {
                    handle.Close();
                }
            }

            public static Int32 XblMultiplayerSearchHandleGetSessionReference(
                XblMultiplayerSearchHandle handle,
                out XblMultiplayerSessionReference sessionRef
                )
            {
                Interop.XblMultiplayerSessionReference interopSessionRef;
                Int32 hr = XblInterop.XblMultiplayerSearchHandleGetSessionReference(handle.Handle, out interopSessionRef);

                if (HR.FAILED(hr))
                {
                    sessionRef = null;
                }
                else
                {
                    sessionRef = new XblMultiplayerSessionReference(interopSessionRef);
                }

                return hr;
            }

            public static Int32 XblMultiplayerSearchHandleGetId(
                XblMultiplayerSearchHandle handle,
                out string id
                )
            {
                int hr = XblInterop.XblMultiplayerSearchHandleGetId(handle.Handle, out UTF8StringPtr interopId);

                if (HR.FAILED(hr))
                {
                    id = null;
                }
                else
                {
                    id = interopId.GetString();
                }

                return hr;
            }

            public static Int32 XblMultiplayerSearchHandleGetSessionOwnerXuids(
                XblMultiplayerSearchHandle handle,
                out ulong[] xuids
                )
            {
                IntPtr interopXuids;
                SizeT xuidsCount;

                int hr = XblInterop.XblMultiplayerSearchHandleGetSessionOwnerXuids(handle.Handle, out interopXuids, out xuidsCount);

                if (HR.FAILED(hr) || xuidsCount.IsZero)
                {
                    xuids = null;
                    return hr;
                }

                xuids = Converters.PtrToClassArray<ulong, ulong>(interopXuids, xuidsCount.ToUInt32(), (x => x));
                return hr;
            }

            public static Int32 XblMultiplayerSearchHandleGetTags(
                XblMultiplayerSearchHandle handle,
                out XblMultiplayerSessionTag[] tags
                )
            {
                IntPtr interopTags;
                SizeT tagsCount;

                int hr = XblInterop.XblMultiplayerSearchHandleGetTags(handle.Handle, out interopTags, out tagsCount);

                if (HR.FAILED(hr) || tagsCount.IsZero)
                {
                    tags = null;
                    return hr;
                }

                tags = Converters.PtrToClassArray<XblMultiplayerSessionTag, Interop.XblMultiplayerSessionTag>(interopTags, tagsCount, (x => new XblMultiplayerSessionTag(x)));
                return hr;
            }

            public static Int32 XblMultiplayerSearchHandleGetStringAttributes(
                XblMultiplayerSearchHandle handle,
                out XblMultiplayerSessionStringAttribute[] attributes
                )
            {
                IntPtr interopAttributes;
                SizeT attributesCount;

                int hr = XblInterop.XblMultiplayerSearchHandleGetStringAttributes(handle.Handle, out interopAttributes, out attributesCount);

                if (HR.FAILED(hr) || attributesCount.IsZero)
                {
                    attributes = null;
                    return hr;
                }

                attributes = Converters.PtrToClassArray<XblMultiplayerSessionStringAttribute, Interop.XblMultiplayerSessionStringAttribute>(interopAttributes, attributesCount, (x => new XblMultiplayerSessionStringAttribute(x)));
                return hr;
            }

            public static Int32 XblMultiplayerSearchHandleGetNumberAttributes(
                XblMultiplayerSearchHandle handle,
                out XblMultiplayerSessionNumberAttribute[] attributes
                )
            {
                IntPtr interopAttributes;
                SizeT attributesCount;

                int hr = XblInterop.XblMultiplayerSearchHandleGetNumberAttributes(handle.Handle, out interopAttributes, out attributesCount);

                if (HR.FAILED(hr) || attributesCount.IsZero)
                {
                    attributes = null;
                    return hr;
                }

                attributes = Converters.PtrToClassArray<XblMultiplayerSessionNumberAttribute, Interop.XblMultiplayerSessionNumberAttribute>(interopAttributes, attributesCount, (x => new XblMultiplayerSessionNumberAttribute(x)));
                return hr;
            }

            public static Int32 XblMultiplayerSearchHandleGetVisibility(
                XblMultiplayerSearchHandle handle,
                out XblMultiplayerSessionVisibility visibility
                )
            {
                Int32 hr = XblInterop.XblMultiplayerSearchHandleGetVisibility(handle.Handle, out visibility);

                return hr;
            }

            public static Int32 XblMultiplayerSearchHandleGetJoinRestriction(
                XblMultiplayerSearchHandle handle,
                out XblMultiplayerSessionRestriction joinRestriction
                )
            {
                Int32 hr = XblInterop.XblMultiplayerSearchHandleGetJoinRestriction(handle.Handle, out joinRestriction);

                return hr;
            }

            public static Int32 XblMultiplayerSearchHandleGetSessionClosed(
                XblMultiplayerSearchHandle handle,
                out bool closed
                )
            {
                Int32 hr = XblInterop.XblMultiplayerSearchHandleGetSessionClosed(handle.Handle, out closed);

                return hr;
            }

            public static Int32 XblMultiplayerSearchHandleGetMemberCounts(
                XblMultiplayerSearchHandle handle,
                out uint maxMembers,
                out uint currentMembers
                )
            {
                maxMembers = default;
                currentMembers = default;
                if (handle == null)
                {
                    return HR.E_INVALIDARG;
                }

                int hr = XblInterop.XblMultiplayerSearchHandleGetMemberCounts(handle.Handle, out SizeT interopMaxMembers, out SizeT interopCurrentMembers);
                if (HR.SUCCEEDED(hr))
                {
                    maxMembers = interopMaxMembers.ToUInt32();
                    currentMembers = interopCurrentMembers.ToUInt32();
                }

                return hr;
            }

            public static Int32 XblMultiplayerSearchHandleGetCreationTime(
                XblMultiplayerSearchHandle handle,
                out DateTime creationTime
                )
            {
                creationTime = default(DateTime);
                if (handle == null)
                {
                    return HR.E_INVALIDARG;
                }

                int hr = XblInterop.XblMultiplayerSearchHandleGetCreationTime(handle.Handle, out TimeT creationTimeT);
                if (HR.SUCCEEDED(hr))
                {
                    creationTime = creationTimeT.DateTime;
                }

                return hr;
            }

            public static Int32 XblMultiplayerSearchHandleGetCustomSessionPropertiesJson(
                XblMultiplayerSearchHandle handle,
                out string customPropertiesJson
                )
            {
                customPropertiesJson = default;
                if (handle == null)
                {
                    return HR.E_INVALIDARG;
                }

                int hr = XblInterop.XblMultiplayerSearchHandleGetCustomSessionPropertiesJson(handle.Handle, out UTF8StringPtr interopCustomPropertiesJson);
                if (HR.SUCCEEDED(hr))
                {
                    customPropertiesJson = interopCustomPropertiesJson.GetString();
                }

                return hr;
            }

            public static void XblMultiplayerWriteSessionAsync(
                XblContextHandle xblContext,
                XblMultiplayerSessionHandle handle,
                XblMultiplayerSessionWriteMode writeMode,
                XblMultiplayerWriteSessionHandleResult completionRoutine
                )
            {
                if (xblContext == null || handle == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblMultiplayerSessionHandle));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Interop.XblMultiplayerSessionHandle result;
                    Int32 hresult = XblInterop.XblMultiplayerWriteSessionResult(block, out result);
                    if (HR.FAILED(hresult))
                    {
                        completionRoutine(hresult, default(XblMultiplayerSessionHandle));
                        return;
                    }

                    completionRoutine(hresult, new XblMultiplayerSessionHandle(result));
                });

                Int32 hr = XblInterop.XblMultiplayerWriteSessionAsync(
                    xblContext.Handle,
                    handle.Handle,
                    writeMode,
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, default(XblMultiplayerSessionHandle));
                }
            }

            public static void XblMultiplayerWriteSessionByHandleAsync(
                XblContextHandle xblContext,
                XblMultiplayerSessionHandle handle,
                XblMultiplayerSessionWriteMode writeMode,
                string handleId,
                XblMultiplayerWriteSessionHandleResult completionRoutine
                )
            {
                if (xblContext == null || handle == null || handleId == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblMultiplayerSessionHandle));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Interop.XblMultiplayerSessionHandle result;
                    Int32 hresult = XblInterop.XblMultiplayerWriteSessionByHandleResult(block, out result);
                    if (HR.FAILED(hresult))
                    {
                        completionRoutine(hresult, default(XblMultiplayerSessionHandle));
                        return;
                    }

                    completionRoutine(hresult, new XblMultiplayerSessionHandle(result));
                });

                Int32 hr = XblInterop.XblMultiplayerWriteSessionByHandleAsync(
                    xblContext.Handle,
                    handle.Handle,
                    writeMode,
                    Converters.StringToNullTerminatedUTF8ByteArray(handleId),
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, default(XblMultiplayerSessionHandle));
                }
            }

            public static void XblMultiplayerGetSessionAsync(
                XblContextHandle xblContext,
                XblMultiplayerSessionReference sessionRef,
                XblMultiplayerGetSessionHandleResult completionRoutine
                )
            {
                if (xblContext == null || sessionRef == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblMultiplayerSessionHandle));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Interop.XblMultiplayerSessionHandle result;
                    Int32 hresult = XblInterop.XblMultiplayerGetSessionResult(block, out result);
                    if (HR.FAILED(hresult))
                    {
                        completionRoutine(hresult, default(XblMultiplayerSessionHandle));
                        return;
                    }

                    completionRoutine(hresult, new XblMultiplayerSessionHandle(result));
                });

                var interopSessionRef = new Interop.XblMultiplayerSessionReference(sessionRef);

                Int32 hr = XblInterop.XblMultiplayerGetSessionAsync(
                    xblContext.Handle,
                    ref interopSessionRef,
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, default(XblMultiplayerSessionHandle));
                }
            }

            public static void XblMultiplayerGetSessionByHandleAsync(
                XblContextHandle xblContext,
                string handleId,
                XblMultiplayerGetSessionHandleResult completionRoutine
                )
            {
                if (xblContext == null || handleId == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblMultiplayerSessionHandle));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Interop.XblMultiplayerSessionHandle result;
                    Int32 hresult = XblInterop.XblMultiplayerGetSessionByHandleResult(block, out result);
                    if (HR.FAILED(hresult))
                    {
                        completionRoutine(hresult, default(XblMultiplayerSessionHandle));
                        return;
                    }

                    completionRoutine(hresult, new XblMultiplayerSessionHandle(result));
                });

                Int32 hr = XblInterop.XblMultiplayerGetSessionByHandleAsync(
                    xblContext.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(handleId),
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, default(XblMultiplayerSessionHandle));
                }
            }

            public static void XblMultiplayerQuerySessionsAsync(
                XblContextHandle xblContext,
                XblMultiplayerSessionQuery sessionQuery,
                XblMultiplayerSessionQueryHandleResult completionRoutine
                )
            {
                if (xblContext == null || sessionQuery == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblMultiplayerSessionQueryResult[]));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Int32 hr = XblInterop.XblMultiplayerQuerySessionsResultCount(block, out SizeT sessionCount);
                    if (HR.FAILED(hr))
                    {
                        completionRoutine(hr, default(XblMultiplayerSessionQueryResult[]));
                        return;
                    }

                    Interop.XblMultiplayerSessionQueryResult[] sessionHandles = new Interop.XblMultiplayerSessionQueryResult[sessionCount.ToInt32()];
                    hr = XblInterop.XblMultiplayerQuerySessionsResult(block, sessionCount, sessionHandles);
                    if (HR.FAILED(hr))
                    {
                        completionRoutine(HR.E_INVALIDARG, default(XblMultiplayerSessionQueryResult[]));
                        return;
                    }

                    completionRoutine(hr, Array.ConvertAll(sessionHandles, h => new XblMultiplayerSessionQueryResult(h)));
                });

                using (DisposableCollection disposableCollection = new DisposableCollection())
                {
                    var sessionQueryRef = new Interop.XblMultiplayerSessionQuery(sessionQuery, disposableCollection);

                    Int32 hr = XblInterop.XblMultiplayerQuerySessionsAsync(
                        xblContext.Handle,
                        ref sessionQueryRef,
                        asyncBlock);

                    if (HR.FAILED(hr))
                    {
                        AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                        completionRoutine(HR.E_INVALIDARG, default(XblMultiplayerSessionQueryResult[]));
                    }
                }
            }

            public static void XblMultiplayerSetActivityAsync(
                XblContextHandle xblContext,
                XblMultiplayerSessionReference sessionReference,
                XblMultiplayerSetActivityHandleResult completionRoutine
                )
            {
                if (xblContext == null || sessionReference == null)
                {
                    completionRoutine(HR.E_INVALIDARG);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(SDK.defaultQueue, (XAsyncCompletionRoutine)(block => completionRoutine(NativeMethods.XAsyncGetStatus(block.InteropPtr, false))));
                var sessionReferenceInterop = new Interop.XblMultiplayerSessionReference(sessionReference);
                int hr = XblInterop.XblMultiplayerSetActivityAsync(xblContext.Handle, ref sessionReferenceInterop, asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr);
                }
            }

            public static void XblMultiplayerClearActivityAsync(
                XblContextHandle xblContext,
                string scid,
                XblMultiplayerClearActivityHandleResult completionRoutine
                )
            {
                if (xblContext == null || scid == null)
                {
                    completionRoutine(HR.E_INVALIDARG);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(SDK.defaultQueue, (XAsyncCompletionRoutine)(block => completionRoutine(NativeMethods.XAsyncGetStatus(block.InteropPtr, false))));
                int hr = XblInterop.XblMultiplayerClearActivityAsync(xblContext.Handle, Converters.StringToNullTerminatedUTF8ByteArray(scid), asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr);
                }
            }

            public static void XblMultiplayerSetTransferHandleAsync(
                XblContextHandle xblContext,
                XblMultiplayerSessionReference targetSessionReference,
                XblMultiplayerSessionReference originSessionReference,
                XblMultiplayerSetTransferHandleResult completionRoutine
                )
            {
                if (xblContext == null || originSessionReference == null || targetSessionReference == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblMultiplayerSessionHandleId));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Interop.XblMultiplayerSessionHandleId result;
                    Int32 hresult = XblInterop.XblMultiplayerSetTransferHandleResult(block, out result);
                    if (HR.FAILED(hresult))
                    {
                        completionRoutine(hresult, default(XblMultiplayerSessionHandleId));
                        return;
                    }

                    completionRoutine(hresult, new XblMultiplayerSessionHandleId(result));
                });

                var interopOriginSessionRef = new Interop.XblMultiplayerSessionReference(originSessionReference);
                var interopTargetSessionRef = new Interop.XblMultiplayerSessionReference(targetSessionReference);

                Int32 hr = XblInterop.XblMultiplayerSetTransferHandleAsync(
                    xblContext.Handle,
                    ref interopTargetSessionRef,
                    ref interopOriginSessionRef,
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, default(XblMultiplayerSessionHandleId));
                }
            }

            public static void XblMultiplayerCreateSearchHandleAsync(
                XblContextHandle xblContext,
                XblMultiplayerSessionReference sessionRef,
                XblMultiplayerSessionTag[] tags,
                XblMultiplayerSessionNumberAttribute[] numberAttributes,
                XblMultiplayerSessionStringAttribute[] stringAttributes,
                XblMultiplayerCreateSearchHandleResult completionRoutine
                )
            {
                if (xblContext == null || sessionRef == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblMultiplayerSearchHandle));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Interop.XblMultiplayerSearchHandle result;
                    Int32 hresult = XblInterop.XblMultiplayerCreateSearchHandleResult(block, out result);
                    if (HR.FAILED(hresult))
                    {
                        completionRoutine(hresult, default(XblMultiplayerSearchHandle));
                        return;
                    }

                    completionRoutine(hresult, new XblMultiplayerSearchHandle(result));
                });

                var interopSessionRef = new Interop.XblMultiplayerSessionReference(sessionRef);
                var interopTags = Converters.ConvertArrayToFixedLength(tags, tags.Length, r => new Interop.XblMultiplayerSessionTag(r));
                var interopNumberAttributes = Converters.ConvertArrayToFixedLength(numberAttributes, numberAttributes.Length, r => new Interop.XblMultiplayerSessionNumberAttribute(r));
                var interopStringAttributes = Converters.ConvertArrayToFixedLength(stringAttributes, stringAttributes.Length, r => new Interop.XblMultiplayerSessionStringAttribute(r));

                Int32 hr = XblInterop.XblMultiplayerCreateSearchHandleAsync(
                    xblContext.Handle,
                    ref interopSessionRef,
                    interopTags,
                    new SizeT(interopTags.Length),
                    interopNumberAttributes,
                    new SizeT(interopNumberAttributes.Length),
                    interopStringAttributes,
                    new SizeT(interopStringAttributes.Length),
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, default(XblMultiplayerSearchHandle));
                }
            }

            public static void XblMultiplayerDeleteSearchHandleAsync(
                XblContextHandle xblContext,
                string handleId,
                XblMultiplayerDeleteSearchHandleResult completionRoutine
                )
            {
                if (xblContext == null || handleId == null)
                {
                    completionRoutine(HR.E_INVALIDARG);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(SDK.defaultQueue, (XAsyncCompletionRoutine)(block => completionRoutine(NativeMethods.XAsyncGetStatus(block.InteropPtr, false))));
                int hr = XblInterop.XblMultiplayerDeleteSearchHandleAsync(xblContext.Handle, Converters.StringToNullTerminatedUTF8ByteArray(handleId), asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr);
                }
            }

            public static void XblMultiplayerGetSearchHandlesAsync(
                XblContextHandle xboxLiveContext,
                string scid,
                string sessionTemplateName,
                string orderByAttribute,
                bool orderAscending,
                string searchFilter,
                string socialGroup,
                XblMultiplayerGetSearchHandlesResult completionRoutine)
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG, new XblMultiplayerSearchHandle[0]);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(SDK.defaultQueue, (XAsyncCompletionRoutine)(block =>
                {
                    SizeT searchHandleCount;
                    int handlesResultCount = XblInterop.XblMultiplayerGetSearchHandlesResultCount(block, out searchHandleCount);
                    if (HR.FAILED(handlesResultCount) || searchHandleCount.IsZero)
                    {
                        completionRoutine(handlesResultCount, new XblMultiplayerSearchHandle[0]);
                    }
                    else
                    {
                        Interop.XblMultiplayerSearchHandle[] multiplayerSearchHandleArray = new Interop.XblMultiplayerSearchHandle[searchHandleCount.ToInt32()];
                        int hresult = XblInterop.XblMultiplayerGetSearchHandlesResult(block, multiplayerSearchHandleArray, searchHandleCount);
                        if (!HR.FAILED(hresult))
                            completionRoutine(hresult, Array.ConvertAll(multiplayerSearchHandleArray, h => new XblMultiplayerSearchHandle(h)));
                        else
                            completionRoutine(hresult, new XblMultiplayerSearchHandle[0]);
                    }
                }));

                int hr = XblInterop.XblMultiplayerGetSearchHandlesAsync(
                    xboxLiveContext.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(scid),
                    Converters.StringToNullTerminatedUTF8ByteArray(sessionTemplateName),
                    Converters.StringToNullTerminatedUTF8ByteArray(orderByAttribute),
                    orderAscending,
                    Converters.StringToNullTerminatedUTF8ByteArray(searchFilter),
                    Converters.StringToNullTerminatedUTF8ByteArray(socialGroup),
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, new XblMultiplayerSearchHandle[0]);
                }
            }

            public static void XblMultiplayerSendInvitesAsync(
                XblContextHandle xboxLiveContext,
                XblMultiplayerSessionReference sessionRef,
                UInt64[] xboxUserIdList,
                UInt32 titleId,
                string contextStringId,
                string customActivationContext,
                XblMultiplayerSendInvitesResult completionRoutine
            )
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG, new XblMultiplayerInviteHandle[0]);
                    return;
                }

                SizeT xuidsCount = new SizeT(xboxUserIdList?.Length ?? 0);

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Interop.XblMultiplayerInviteHandle[] handles = new Interop.XblMultiplayerInviteHandle[xuidsCount.ToInt32()];
                    int hresult = XblInterop.XblMultiplayerSendInvitesResult(block, xuidsCount, handles);
                    if (HR.FAILED(hresult))
                    {
                        completionRoutine(hresult, new XblMultiplayerInviteHandle[0]);
                    }
                    else
                    {
                       completionRoutine(hresult, Array.ConvertAll(handles, h => new XblMultiplayerInviteHandle(h)));
                    }
                });

                var interopSessionRef = new Interop.XblMultiplayerSessionReference(sessionRef);
                int hr = XblInterop.XblMultiplayerSendInvitesAsync(
                    xboxLiveContext.Handle,
                    ref interopSessionRef,
                    xboxUserIdList,
                    xuidsCount,
                    titleId,
                    Converters.StringToNullTerminatedUTF8ByteArray(contextStringId),
                    Converters.StringToNullTerminatedUTF8ByteArray(customActivationContext),
                    asyncBlock
                );

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, new XblMultiplayerInviteHandle[0]);
                }
            }

            public static void XblMultiplayerGetActivitiesForSocialGroupAsync(
                XblContextHandle xboxLiveContext,
                string scid,
                UInt64 socialGroupOwnerXuid,
                string socialGroup,
                XblMultiplayerGetActivitiesResult completionRoutine
                )
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG, new XblMultiplayerActivityDetails[0]);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    SizeT activityCount;
                    int activitiesResultCount = XblInterop.XblMultiplayerGetActivitiesForSocialGroupResultCount(block, out activityCount);
                    if (HR.FAILED(activitiesResultCount) || activityCount.IsZero)
                    {
                        completionRoutine(activitiesResultCount, new XblMultiplayerActivityDetails[0]);
                    }
                    else
                    {
                        Interop.XblMultiplayerActivityDetails[] activitiesArray = new Interop.XblMultiplayerActivityDetails[activityCount.ToInt32()];
                        int hresult = XblInterop.XblMultiplayerGetActivitiesForSocialGroupResult(block, activityCount, activitiesArray);
                        if (!HR.FAILED(hresult))
                            completionRoutine(hresult, Array.ConvertAll(activitiesArray, h => new XblMultiplayerActivityDetails(h)));
                        else
                            completionRoutine(hresult, new XblMultiplayerActivityDetails[0]);
                    }
                });

                int hr = XblInterop.XblMultiplayerGetActivitiesForSocialGroupAsync(
                    xboxLiveContext.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(scid),
                    socialGroupOwnerXuid,
                    Converters.StringToNullTerminatedUTF8ByteArray(socialGroup),
                    asyncBlock
                );

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, new XblMultiplayerActivityDetails[0]);
                }
            }

            public static void XblMultiplayerGetActivitiesWithPropertiesForSocialGroupAsync(
                XblContextHandle xboxLiveContext,
                string scid,
                UInt64 socialGroupOwnerXuid,
                string socialGroup,
                XblMultiplayerGetActivitiesResult completionRoutine
                )
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG, new XblMultiplayerActivityDetails[0]);
                    return;
                }

                int hr;

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    hr = XblInterop.XblMultiplayerGetActivitiesWithPropertiesForSocialGroupResultSize(
                        block,
                        out SizeT resultSizeInBytes
                        );

                    if (HR.FAILED(hr) || resultSizeInBytes.IsZero)
                    {
                        completionRoutine(hr, new XblMultiplayerActivityDetails[0]);
                        return;
                    }

                    using (DisposableBuffer buffer = new DisposableBuffer(resultSizeInBytes.ToInt32()))
                    {
                        hr = XblInterop.XblMultiplayerGetActivitiesWithPropertiesForSocialGroupResult(
                            block,
                            resultSizeInBytes,
                            buffer.IntPtr,
                            out IntPtr ptrToResults,
                            out SizeT resultCount,
                            out SizeT bufferUsed
                        );

                        if (HR.FAILED(hr))
                        {
                            completionRoutine(hr, null);
                            return;
                        }

                        completionRoutine(hr, Converters.PtrToClassArray<XblMultiplayerActivityDetails, Interop.XblMultiplayerActivityDetails>(ptrToResults, resultCount, r => new XblMultiplayerActivityDetails(r)));
                    }
                });

                hr = XblInterop.XblMultiplayerGetActivitiesWithPropertiesForSocialGroupAsync(
                    xboxLiveContext.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(scid),
                    socialGroupOwnerXuid,
                    Converters.StringToNullTerminatedUTF8ByteArray(socialGroup),
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, new XblMultiplayerActivityDetails[0]);
                }
            }

            public static void XblMultiplayerGetActivitiesForUsersAsync(
                XblContextHandle xboxLiveContext,
                string scid,
                UInt64[] xuids,
                XblMultiplayerGetActivitiesResult completionRoutine
                )
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG, new XblMultiplayerActivityDetails[0]);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    SizeT activityCount;
                    int activitiesResultCount = XblInterop.XblMultiplayerGetActivitiesForUsersResultCount(block, out activityCount);
                    if (HR.FAILED(activitiesResultCount) || activityCount.IsZero)
                    {
                        completionRoutine(activitiesResultCount, new XblMultiplayerActivityDetails[0]);
                    }
                    else
                    {
                        Interop.XblMultiplayerActivityDetails[] activitiesArray = new Interop.XblMultiplayerActivityDetails[activityCount.ToInt32()];
                        int hresult = XblInterop.XblMultiplayerGetActivitiesForUsersResult(block, activityCount, activitiesArray);
                        if (!HR.FAILED(hresult))
                            completionRoutine(hresult, Array.ConvertAll(activitiesArray, h => new XblMultiplayerActivityDetails(h)));
                        else
                            completionRoutine(hresult, new XblMultiplayerActivityDetails[0]);
                    }
                });

                int hr = XblInterop.XblMultiplayerGetActivitiesForUsersAsync(
                    xboxLiveContext.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(scid),
                    xuids,
                    new SizeT(xuids?.Length ?? 0),
                    asyncBlock
                );

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, new XblMultiplayerActivityDetails[0]);
                }
            }

            public static void XblMultiplayerGetActivitiesWithPropertiesForUsersAsync(
                XblContextHandle xboxLiveContext,
                string scid,
                UInt64[] xuids,
                XblMultiplayerGetActivitiesResult completionRoutine
                )
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG, new XblMultiplayerActivityDetails[0]);
                    return;
                }

                int hr;

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    hr = XblInterop.XblMultiplayerGetActivitiesWithPropertiesForUsersResultSize(
                        block,
                        out SizeT resultSizeInBytes
                        );

                    if (HR.FAILED(hr))
                    {
                        completionRoutine(hr, new XblMultiplayerActivityDetails[0]);
                        return;
                    }

                    using (DisposableBuffer buffer = new DisposableBuffer(resultSizeInBytes.ToInt32()))
                    {
                        hr = XblInterop.XblMultiplayerGetActivitiesWithPropertiesForUsersResult(
                            block,
                            resultSizeInBytes,
                            buffer.IntPtr,
                            out IntPtr ptrToResults,
                            out SizeT resultCount,
                            out SizeT bufferUsed
                        );

                        if (HR.FAILED(hr))
                        {
                            completionRoutine(hr, null);
                            return;
                        }

                        completionRoutine(hr, Converters.PtrToClassArray<XblMultiplayerActivityDetails, Interop.XblMultiplayerActivityDetails>(ptrToResults, resultCount, r => new XblMultiplayerActivityDetails(r)));
                    }
                });

                hr = XblInterop.XblMultiplayerGetActivitiesWithPropertiesForUsersAsync(
                    xboxLiveContext.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(scid),
                    xuids,
                    new SizeT(xuids?.Length ?? 0),
                    asyncBlock
                    );

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, new XblMultiplayerActivityDetails[0]);
                }
            }

            public static Int32 XblMultiplayerSetSubscriptionsEnabled(
                XblContextHandle xblContext,
                bool subscriptionsEnabled
                )
            {
                return XblInterop.XblMultiplayerSetSubscriptionsEnabled(xblContext.Handle, subscriptionsEnabled);
            }

            public static bool XblMultiplayerSubscriptionsEnabled(
                XblContextHandle xblHandle
                )
            {
                return XblInterop.XblMultiplayerSubscriptionsEnabled(xblHandle.Handle);
            }

            public static Int32 XblMultiplayerSessionSetCustomPropertyJson(
                XblMultiplayerSessionHandle handle,
                string name,
                string valueJson
                )
            {
                return XblInterop.XblMultiplayerSessionSetCustomPropertyJson(handle.Handle, Converters.StringToNullTerminatedUTF8ByteArray(name), Converters.StringToNullTerminatedUTF8ByteArray(valueJson));
            }

            public static Int32 XblMultiplayerSessionDeleteCustomPropertyJson(
                XblMultiplayerSessionHandle handle,
                string name
                )
            {
                return XblInterop.XblMultiplayerSessionDeleteCustomPropertyJson(handle.Handle, Converters.StringToNullTerminatedUTF8ByteArray(name));
            }

            public static XblMultiplayerSessionChangeTypes XblMultiplayerSessionCompare(
                XblMultiplayerSessionHandle currentSessionHandle,
                XblMultiplayerSessionHandle oldSessionHandle
                )
            {
                if (currentSessionHandle == null || oldSessionHandle == null)
                    return XblMultiplayerSessionChangeTypes.Everything;

                return XblInterop.XblMultiplayerSessionCompare(currentSessionHandle.Handle, oldSessionHandle.Handle);
            }
        }
    }
}
