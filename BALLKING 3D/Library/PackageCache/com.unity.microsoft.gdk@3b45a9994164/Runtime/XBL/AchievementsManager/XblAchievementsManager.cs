using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public partial class SDK
    {
        public partial class XBL
        {
            public static Int32 XblAchievementsManagerResultGetAchievements(XblAchievementsManagerResultHandle handle, out XblAchievement[] achievements)
            {
                if (handle == null)
                {
                    achievements = null;
                    return HR.E_INVALIDARG;
                }

                IntPtr achievementsPtr;
                UInt64 achievementsCount;

                var hr = XblInterop.XblAchievementsManagerResultGetAchievements(handle.Handle, out achievementsPtr, out achievementsCount);

                if (HR.FAILED(hr) || achievementsCount == 0)
                {
                    achievements = null;
                    return hr;
                }

                achievements = Converters.PtrToClassArray<XGamingRuntime.XblAchievement, Interop.XblAchievement>(achievementsPtr, new SizeT(achievementsCount), (x) => new XblAchievement(x));
                return hr;
            }

            public static Int32 XblAchievementsManagerResultDuplicateHandle(XblAchievementsManagerResultHandle handle, out XblAchievementsManagerResultHandle duplicatedHandle)
            {
                if (handle == null)
                {
                    duplicatedHandle = default(XblAchievementsManagerResultHandle);
                    return HR.E_INVALIDARG;
                }

                Interop.XblAchievementsManagerResultHandle duplicatedInteropHandle;
                Int32 hr = XblInterop.XblAchievementsManagerResultDuplicateHandle(handle.Handle, out duplicatedInteropHandle);
                return XblAchievementsManagerResultHandle.WrapAndReturnHResult(hr, duplicatedInteropHandle, out duplicatedHandle);
            }

            public static void XblAchievementsManagerResultCloseHandle(XblAchievementsManagerResultHandle handle)
            {
                if (handle == null)
                    return;

                handle.Close();
            }

            public static Int32 XblAchievementsManagerAddLocalUser(XUserHandle user)
            {
                if (user == null)
                    return HR.E_INVALIDARG;

                return XblInterop.XblAchievementsManagerAddLocalUser(user.Handle, defaultQueue);
            }

            public static Int32 XblAchievementsManagerRemoveLocalUser(XUserHandle user)
            {
                if (user == null)
                    return HR.E_INVALIDARG;

                return XblInterop.XblAchievementsManagerRemoveLocalUser(user.Handle);
            }

            public static Int32 XblAchievementsManagerIsUserInitialized(UInt64 xboxUserId)
            {
                return XblInterop.XblAchievementsManagerIsUserInitialized(xboxUserId);
            }

            public static Int32 XblAchievementsManagerDoWork(out XblAchievementsManagerEvent[] events)
            {
                IntPtr achievementsEvents;
                SizeT achievementsEventsCount;
                var hr = XblInterop.XblAchievementsManagerDoWork(out achievementsEvents, out achievementsEventsCount);

                if (HR.FAILED(hr) || achievementsEventsCount.IsZero )
                {
                    events = null;
                    return hr;
                }

                events = Converters.PtrToClassArray<XGamingRuntime.XblAchievementsManagerEvent, Interop.XblAchievementsManagerEvent>(achievementsEvents, achievementsEventsCount, (x) => new XblAchievementsManagerEvent(x));

                return hr;
            }

            public static Int32 XblAchievementsManagerGetAchievement(UInt64 xboxUserId, string achievementId, out XblAchievementsManagerResultHandle handle)
            {
                var nameBytes = Converters.StringToNullTerminatedUTF8ByteArray(achievementId);

                var hr = XblInterop.XblAchievementsManagerGetAchievement(xboxUserId, nameBytes, out var interopHandle);

                return XblAchievementsManagerResultHandle.WrapAndReturnHResult(hr, interopHandle, out handle);
            }

            public static Int32 XblAchievementsManagerGetAchievements(UInt64 xboxUserId, XblAchievementOrderBy sortField, XblAchievementsManagerSortOrder sortOrder, out XblAchievementsManagerResultHandle handle)
            {
                var hr = XblInterop.XblAchievementsManagerGetAchievements(xboxUserId, sortField, sortOrder, out var interopHandle);

                return XblAchievementsManagerResultHandle.WrapAndReturnHResult(hr, interopHandle, out handle);
            }

            public static Int32 XblAchievementsManagerGetAchievementsByState(UInt64 xboxUserId, XblAchievementOrderBy sortField, XblAchievementsManagerSortOrder sortOrder, XblAchievementProgressState achievementState, out XblAchievementsManagerResultHandle handle)
            {
                var hr = XblInterop.XblAchievementsManagerGetAchievementsByState(xboxUserId, sortField, sortOrder, achievementState, out var interopHandle);

                return XblAchievementsManagerResultHandle.WrapAndReturnHResult(hr, interopHandle, out handle);
            }

            public static Int32 XblAchievementsManagerUpdateAchievement(UInt64 xboxUserId, string achievementId, Byte currentProgress)
            {
                var nameBytes = Converters.StringToNullTerminatedUTF8ByteArray(achievementId);

                return XblInterop.XblAchievementsManagerUpdateAchievement(xboxUserId, nameBytes, currentProgress);
            }
        }
    }
}