using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    /// <summary>
    /// An achievement event that will be returned from <see cref="XblAchievementsManagerDoWork"/>.
    /// </summary>

    //typedef struct XblAchievementsManagerEvent
    //{
    //    /// <summary>
    //    /// Current state of progress for an achievement for AchievementUnlocked and
    //    /// AchievementProgressUpdated events. The values of this struct are not
    //    /// populated for LocalUserInitialStateSynced events.
    //    /// </summary>
    //    XblAchievementProgressChangeEntry progressInfo;
    //
    //    /// <summary>
    //    /// The ID for the user that has made progress on an achievement.
    //    /// </summary>
    //    uint64_t xboxUserId;
    //
    //    /// <summary>
    //    /// Type of the event triggered.
    //    /// </summary>
    //    XblAchievementsManagerEventType eventType;
    //}
    //XblAchievementsManagerEvent;

    [StructLayout(LayoutKind.Sequential)]

    internal struct XblAchievementsManagerEvent
    {
        internal XblAchievementProgressChangeEntry progressInfo;
        internal UInt64 xboxUserId;
        internal XblAchievementsManagerEventType eventType;
    }
}