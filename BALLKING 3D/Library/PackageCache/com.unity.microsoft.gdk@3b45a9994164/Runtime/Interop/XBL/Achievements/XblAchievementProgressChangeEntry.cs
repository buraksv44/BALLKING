using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    /// <summary>
    /// An entry for XblAchievementProgressChangeEventArgs representing a progress update for a single achievement.
    /// </summary>

    //typedef struct XblAchievementProgressChangeEntry
    //{
    //    /// <summary>
    //    /// The UTF-8 encoded achievement ID. Represents a uint.
    //    /// </summary>
    //    _Field_z_ const char* achievementId;
    //
    //    /// <summary>
    //    /// The state of a user's progress towards the earning of the achievement.
    //    /// </summary>
    //    XblAchievementProgressState progressState;
    //
    //    /// <summary>
    //    /// The progression object containing progress details about the achievement,
    //    /// including requirements.
    //    /// </summary>
    //    XblAchievementProgression progression;
    //}
    //XblAchievementProgressChangeEntry;
    [StructLayout(LayoutKind.Sequential)]

    internal struct XblAchievementProgressChangeEntry
    {
        internal UTF8StringPtr achievementId;
        internal XblAchievementProgressState progressState;
        internal XblAchievementProgression progression;
    }
}