using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public partial class SDK
    {
        public partial class XBL
        {
            public static bool XblSocialManagerPresenceRecordIsUserPlayingTitle(
                XblSocialManagerPresenceRecord presenceRecord,
                UInt32 titleId)
            {
                Interop.XblSocialManagerPresenceRecord interopPresenceRecord = new Interop.XblSocialManagerPresenceRecord(presenceRecord);
                return XblInterop.XblSocialManagerPresenceRecordIsUserPlayingTitle(ref interopPresenceRecord, titleId);
            }

            public static Int32 XblSocialManagerUserGroupGetUsers(
                XblSocialManagerUserGroupHandle group,
                out XblSocialManagerUser[] xboxSocialUsers)
            {
                xboxSocialUsers = default(XblSocialManagerUser[]);
                if (group == null)
                {
                    return HR.E_INVALIDARG;
                }

                Int32 hresult = XblInterop.XblSocialManagerUserGroupGetUsers(
                    group.Handle,
                    out IntPtr userArrayPtr,
                    out SizeT userArrayCount);

                if (HR.FAILED(hresult))
                {
                    return hresult;
                }

                if (SDK.GetGdkEdition() >= 241000)
                {
                    xboxSocialUsers = Converters.PtrToClassArray<XblSocialManagerUser, IntPtr>(
                        userArrayPtr,
                        userArrayCount,
                        intPtr => Converters.PtrToClass<XblSocialManagerUser, Interop.XblSocialManagerUser2>(intPtr, u => new XblSocialManagerUser(u)));
                }
                else
                {
                    xboxSocialUsers = Converters.PtrToClassArray<XblSocialManagerUser, IntPtr>(
                        userArrayPtr,
                        userArrayCount,
                        intPtr => Converters.PtrToClass<XblSocialManagerUser, Interop.XblSocialManagerUser>(intPtr, u => new XblSocialManagerUser(u)));
                }

                return hresult;
            }

            public static Int32 XblSocialManagerUserGroupGetUsersTrackedByGroup(
                XblSocialManagerUserGroupHandle group,
                out UInt64[] trackedUsers)
            {
                trackedUsers = default(UInt64[]);

                if (group == null)
                {
                    return HR.E_INVALIDARG;
                }

                Int32 hresult = XblInterop.XblSocialManagerUserGroupGetUsersTrackedByGroup(
                    group.Handle,
                    out IntPtr trackedUsersPtr,
                    out SizeT trackedUsersCount);

                if (!HR.FAILED(hresult))
                {
                    trackedUsers = Converters.PtrToClassArray<UInt64, UInt64>(trackedUsersPtr, trackedUsersCount.ToUInt32(), x => x);
                }

                return hresult;
            }

            public static Int32 XblSocialManagerAddLocalUser(
                XUserHandle user,
                XblSocialManagerExtraDetailLevel extraLevelDetail)
            {
                if (user == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblSocialManagerAddLocalUser(user.Handle, extraLevelDetail, defaultQueue);
            }

            public static Int32 XblSocialManagerRemoveLocalUser(
                XUserHandle user,
                XblSocialManagerExtraDetailLevel extraLevelDetail)
            {
                if (user == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblSocialManagerRemoveLocalUser(user.Handle);
            }

            public static Int32 XblSocialManagerDoWork(out XblSocialManagerEvent[] socialEvents)
            {
                Int32 hresult = XblInterop.XblSocialManagerDoWork(out IntPtr interopSocialEvents, out SizeT socialEventsCount);
                if (HR.FAILED(hresult))
                {
                    socialEvents = default(XblSocialManagerEvent[]);
                    return hresult;
                }

                if (interopSocialEvents == IntPtr.Zero)
                {
                    socialEvents = null;
                }
                else
                {
                    socialEvents = Converters.PtrToClassArray<XblSocialManagerEvent, Interop.XblSocialManagerEvent>(interopSocialEvents, socialEventsCount, e => new XblSocialManagerEvent(e));
                }

                return hresult;
            }

            public static Int32 XblSocialManagerCreateSocialUserGroupFromFilters(
                XUserHandle user,
                XblPresenceFilter presenceDetailLevel,
                XblRelationshipFilter filter,
                out XblSocialManagerUserGroupHandle group)
            {
                if (user == null)
                {
                    group = default(XblSocialManagerUserGroupHandle);
                    return HR.E_INVALIDARG;
                }

                Int32 hresult = XblInterop.XblSocialManagerCreateSocialUserGroupFromFilters(
                    user.Handle,
                    presenceDetailLevel,
                    filter,
                    out Interop.XblSocialManagerUserGroupHandle interopGroupPtr);

                return XblSocialManagerUserGroupHandle.WrapAndReturnHResult(hresult, interopGroupPtr, out group);
            }

            public static Int32 XblSocialManagerCreateSocialUserGroupFromList(
                XUserHandle user,
                UInt64[] xboxUserIdList,
                out XblSocialManagerUserGroupHandle group)
            {
                if (user == null)
                {
                    group = default(XblSocialManagerUserGroupHandle);
                    return HR.E_INVALIDARG;
                }

                Int32 hresult = XblInterop.XblSocialManagerCreateSocialUserGroupFromList(
                    user.Handle,
                    xboxUserIdList,
                    new SizeT(xboxUserIdList?.Length ?? 0),
                    out Interop.XblSocialManagerUserGroupHandle interopGroupPtr);

                return XblSocialManagerUserGroupHandle.WrapAndReturnHResult(hresult, interopGroupPtr, out group);
            }

            public static Int32 XblSocialManagerDestroySocialUserGroup(XblSocialManagerUserGroupHandle group)
            {
                if (group == null)
                {
                    return HR.E_INVALIDARG;
                }

                Int32 hresult = XblInterop.XblSocialManagerDestroySocialUserGroup(group.Handle);

                group.Close();

                return hresult;
            }

            public static Int32 XblSocialManagerGetLocalUsers(out XUserHandle[] users)
            {
                SizeT userCount = XblInterop.XblSocialManagerGetLocalUserCount();

                IntPtr[] interopUsers = new IntPtr[userCount.ToInt32()];
                int hresult = XblInterop.XblSocialManagerGetLocalUsers(userCount, interopUsers);
                if (HR.FAILED(hresult))
                {
                    users = default(XUserHandle[]);
                    return hresult;
                }

                // These user handles do not need to be closed
                users = Array.ConvertAll(interopUsers, u => new XUserHandle(u, false));
                return hresult;
            }

            public static Int32 XblSocialManagerUpdateSocialUserGroup(XblSocialManagerUserGroupHandle group, UInt64[] users)
            {
                if (group == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblSocialManagerUpdateSocialUserGroup(group.Handle, users, new SizeT(users?.Length ?? 0));
            }

            public static Int32 XblSocialManagerSetRichPresencePollingStatus(XUserHandle user, bool shouldEnablePolling)
            {
                if (user == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblSocialManagerSetRichPresencePollingStatus(user.Handle, shouldEnablePolling);
            }

            public static Int32 XblSocialManagerUserGroupGetType(XblSocialManagerUserGroupHandle group, out XblSocialUserGroupType type)
            {
                type = default(XblSocialUserGroupType);
                if (group == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblSocialManagerUserGroupGetType(group.Handle, out type);
            }

            public static Int32 XblSocialManagerUserGroupGetLocalUser(XblSocialManagerUserGroupHandle group, out XUserHandle localUser)
            {
                localUser = default(XUserHandle);
                if (group == null)
                {
                    return HR.E_INVALIDARG;
                }

                Int32 hr = XblInterop.XblSocialManagerUserGroupGetLocalUser(group.Handle, out IntPtr interopUser);

                if (HR.SUCCEEDED(hr) && interopUser != IntPtr.Zero)
                {
                    // This user handle does not need to be closed
                    localUser = new XUserHandle(interopUser, false);
                }
                else
                {
                    localUser = null;
                }

                return hr;
            }

            public static Int32 XblSocialManagerUserGroupGetFilters(
                XblSocialManagerUserGroupHandle group,
                out XblPresenceFilter presenceFilter,
                out XblRelationshipFilter relationshipFilter)
            {
                presenceFilter = default(XblPresenceFilter);
                relationshipFilter = default(XblRelationshipFilter);

                if (group == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblSocialManagerUserGroupGetFilters(group.Handle, out presenceFilter, out relationshipFilter);
            }
        }
    }
}