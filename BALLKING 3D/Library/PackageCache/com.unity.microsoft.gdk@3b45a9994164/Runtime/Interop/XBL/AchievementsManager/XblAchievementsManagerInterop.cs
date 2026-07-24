using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    internal static partial class XblInterop
    {
        /// <summary>
        /// Get a list of XblAchievement objects.
        /// </summary>
        /// <param name="resultHandle">Achievement result handle.</param>
        /// <param name="achievements">Pointer to an array of XblAchievement objects.
        /// The memory for the returned pointer will remain valid for the life of the
        /// XblAchievementsManagerResultHandle object until it is closed.</param>
        /// <param name="achievementsCount">The count of objects in the returned array.</param>
        /// <returns>HRESULT return code for this API operation.</returns>
        /// <remarks>
        /// The returned array of XblAchievement objects is freed when all outstanding handles
        /// to the object have been closed with <see cref="XblAchievementsManagerResultCloseHandle"/>.
        /// However the data might become stale.
        /// </remarks>
        //STDAPI XblAchievementsManagerResultGetAchievements(
        //    _In_ XblAchievementsManagerResultHandle resultHandle,
        //    _Out_ const XblAchievement** achievements,
        //    _Out_ uint64_t* achievementsCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblAchievementsManagerResultGetAchievements(
            IntPtr resultHandle,
            out IntPtr achievements,
            out UInt64 achievementsCount);

        /// <summary>
        /// Duplicates a XblAchievementsManagerResultHandle.
        /// </summary>
        /// <param name="handle">The XblAchievementsManagerResultHandle to duplicate.</param>
        /// <param name="duplicatedHandle">The duplicated handle.</param>
        /// <returns>HRESULT return code for this API operation.</returns>

        //STDAPI XblAchievementsManagerResultDuplicateHandle(
        //    _In_ XblAchievementsManagerResultHandle handle,
        //    _Out_ XblAchievementsManagerResultHandle* duplicatedHandle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblAchievementsManagerResultDuplicateHandle(
            IntPtr handle,
            out XblAchievementsManagerResultHandle duplicatedHandle);

        /// <summary>
        /// Closes the XblAchievementsManagerResultHandle.
        /// </summary>
        /// <param name="handle">The XblAchievementsManagerResultHandle to close.</param>
        /// <returns></returns>
        /// <remarks>
        /// When all outstanding handles have been closed, the memory associated with the achievement result will be freed.
        /// </remarks>
        //STDAPI_(void) XblAchievementsManagerResultCloseHandle(
        //    _In_ XblAchievementsManagerResultHandle handle
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XblAchievementsManagerResultCloseHandle(
            IntPtr handle);

        /// <summary>
        /// Generate a local cache of achievements for a user.
        /// </summary>
        /// <param name="user">Xbox Live User to fetch achievements for.</param>
        /// <param name="queue">Queue to be used for background operation for this user (Optional).</param>
        /// <returns>HRESULT return code for this API operation.</returns>
        //STDAPI XblAchievementsManagerAddLocalUser(
        //    _In_ XblUserHandle user,
        //    _In_opt_ XTaskQueueHandle queue
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblAchievementsManagerAddLocalUser(
            IntPtr user,
            XTaskQueueHandle queue);

        /// <summary>
        /// Immediately remove the local cache of achievements for a user.
        /// </summary>
        /// <param name="user">Xbox Live User to clear the cache for.</param>
        /// <returns>HRESULT return code for this API operation.</returns>
        //STDAPI XblAchievementsManagerRemoveLocalUser(
        //    _In_ XblUserHandle user
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblAchievementsManagerRemoveLocalUser(
            IntPtr user);

        /// <summary>
        /// Checks whether a specific user has had its initial state synced.
        /// </summary>
        /// <param name="xboxUserId">Xbox Live User to check.</param>
        /// <returns>HRESULT return code for this API operation. If the user is not
        /// initialized, this function will return E_FAIL.</returns>
        //STDAPI XblAchievementsManagerIsUserInitialized(
        //    _In_ uint64_t xboxUserId
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblAchievementsManagerIsUserInitialized(
            UInt64 xboxUserId);

        /// <summary>
        /// Called whenever the title wants to update the state of achievements and get list of change events.
        /// </summary>
        /// <param name="achievementsEvents">Passes back a pointer to the array of achievement events that have occurred since the last call to XblAchievementsManagerDoWork.</param>
        /// <param name="achievementsEventsCount">Passes back the number of events in the achievement events array.</param>
        /// <returns>HRESULT return code for this API operation.</returns>
        /// <remarks>
        /// Must be called every frame for data to be up to date.
        /// The array of achievement events that is sent back is only valid until the next call to <see cref="XblAchievementsManagerDoWork"/>.
        /// Make sure to check if there were achievement events sent back.
        /// If the achievement events array is null, no results.
        /// If the achievement events count is 0, no results.
        /// If there were achievement events sent back then handle each <see cref="XblAchievementsManagerEvent"/>
        /// by their respective <see cref="XblAchievementsManagerEventType"/>.
        /// </remarks>
        //STDAPI XblAchievementsManagerDoWork(
        //    _Outptr_result_maybenull_ const XblAchievementsManagerEvent** achievementsEvents,
        //    _Out_ size_t* achievementsEventsCount
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblAchievementsManagerDoWork(
            out IntPtr achievementsEvents,
            out SizeT achievementsEventsCount);

        /// <summary>
        /// Gets the current local state of an achievement for a local player with a specific achievement ID.
        /// </summary>
        /// <param name="xboxUserId">The Xbox User ID of the player.</param>
        /// <param name="achievementId">The UTF-8 encoded unique identifier of the Achievement as defined by Dev Center.</param>
        /// <param name="achievementResult">The handle to the result of AchievementsManager API calls.
        /// This handle is used by other APIs to get the achievement objects matching the
        /// API that was called.
        /// The handle must be closed using <see cref="XblAchievementsManagerResultCloseHandle"/> when
        /// the result is no longer needed.
        /// </param>
        /// <returns>HRESULT return code for this API operation.</returns>
        //STDAPI XblAchievementsManagerGetAchievement(
        //    _In_ uint64_t xboxUserId,
        //    _In_ const char* achievementId,
        //    _Outptr_result_maybenull_ XblAchievementsManagerResultHandle* achievementResult
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblAchievementsManagerGetAchievement(
            UInt64 xboxUserId,
            Byte[] achievementId,
            out XblAchievementsManagerResultHandle achievementResult);

        /// <summary>
        /// Gets a list of all achievements for a player.
        /// </summary>
        /// <param name="xboxUserId">The Xbox User ID of the player.</param>
        /// <param name="sortField">
        /// The field to sort the list of achievements on.
        /// </param>
        /// <param name="sortOrder">The direction by which to sort the list of achievements.</param>
        /// <param name="achievementsResult">The handle to the result of AchievementsManager API calls.
        /// This handle is used by other APIs to get the achievement objects matching the
        /// API that was called.
        /// The handle must be closed using <see cref="XblAchievementsManagerResultCloseHandle"/> when
        /// the result is no longer needed.
        /// </param>
        /// <remarks>
        /// Passing in XblAchievementOrderBy::TitleId for sortField yields the same results
        /// as passing in XblAchievementOrderBy::DefaultOrder since all achievements tracked
        /// by achievement manager will have the same TitleId.
        /// </remarks>
        /// <returns>HRESULT return code for this API operation.</returns>
        //STDAPI XblAchievementsManagerGetAchievements(
        //    _In_ uint64_t xboxUserId,
        //    _In_ XblAchievementOrderBy sortField,
        //    _In_ XblAchievementsManagerSortOrder sortOrder,
        //    _Outptr_result_maybenull_ XblAchievementsManagerResultHandle* achievementsResult
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblAchievementsManagerGetAchievements(
            UInt64 xboxUserId,
            XblAchievementOrderBy sortField,
            XblAchievementsManagerSortOrder sortOrder,
            out XblAchievementsManagerResultHandle achievementsResult);

        /// <summary>
        /// Gets a list of achievements in a specific progress state for a player.
        /// </summary>
        /// <param name="xboxUserId">The Xbox User ID of the player.</param>
        /// <param name="sortField">
        /// The field to sort the list of achievements on. TitleId will behave
        /// the same as DefaultOrder, as AchievementsManager only handles one title
        /// at a time.
        /// </param>
        /// <param name="sortOrder">The direction by which to sort the list of achievements.</param>
        /// <param name="achievementState">The achievement state to include in the results.</param>
        /// <param name="achievementsResult">The handle to the result of AchievementsManager API calls.
        /// This handle is used by other APIs to get the achievement objects matching the
        /// API that was called.
        /// The handle must be closed using <see cref="XblAchievementsManagerResultCloseHandle"/> when
        /// the result is no longer needed.
        /// </param>
        /// <returns>HRESULT return code for this API operation.</returns>
        //STDAPI XblAchievementsManagerGetAchievementsByState(
        //    _In_ uint64_t xboxUserId,
        //    _In_ XblAchievementOrderBy sortField,
        //    _In_ XblAchievementsManagerSortOrder sortOrder,
        //    _In_ XblAchievementProgressState achievementState,
        //    _Outptr_result_maybenull_ XblAchievementsManagerResultHandle* achievementsResult
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblAchievementsManagerGetAchievementsByState(
            UInt64 xboxUserId,
            XblAchievementOrderBy sortField,
            XblAchievementsManagerSortOrder sortOrder,
            XblAchievementProgressState achievementState,
            out XblAchievementsManagerResultHandle achievementResult);

        /// <summary>
        /// Allow achievement progress to be updated and achievements to be unlocked.
        /// </summary>
        /// <param name="xboxUserId">The Xbox User ID of the player.</param>
        /// <param name="achievementId">The UTF-8 encoded achievement ID as defined by Dev Center.</param>
        /// <param name="currentProgess">The completion percentage of the achievement to indicate progress.
        /// Valid values are from 1 to 100. Set to 100 to unlock the achievement.
        /// Progress will be set by the server to the highest value sent</param>
        /// <returns>HRESULT return code for this API operation.</returns>
        /// <remarks>
        /// This API will work even when offline on PC and Xbox consoles. Offline updates will be
        /// posted by the system when connection is re-established even if the title isn't running.
        ///
        /// The result of the operation will not be represented locally immediately. The
        /// earliest the update will be reflected will be after the next frame's call to
        /// DoWork. Once the change is reflected, the array returned by DoWork
        /// will contain a <see cref="XblAchievementsManagerEvent" /> of with
        /// an event type of AchievementProgressUpdated, and potentially an
        /// additional event of type AchievementUnlocked if the new progress
        /// resulted in unlocking the achievement.
        ///
        /// If the achievement has already been unlocked or the progress value is less than or
        /// equal to what is cached from the server, this function will return E_NOT_VALID_STATE
        /// or E_INVALIDARG respectively.
        ///
        /// Only title based achievements may be updated with this function. Event based
        /// achievements cannot be updated with this function.
        /// </remarks>
        /// <rest>V2 POST /users/xuid({xuid})/achievements/{scid}/update</rest>
        //STDAPI XblAchievementsManagerUpdateAchievement(
        //    _In_ uint64_t xboxUserId,
        //    _In_ const char* achievementId,
        //    _In_ uint8_t currentProgess
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblAchievementsManagerUpdateAchievement(
            UInt64 xboxUserId,
            Byte[] achievementId,
            Byte currentProgress );
    }
}

