using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblAchievementProgressChangeEntry
    {
        internal XblAchievementProgressChangeEntry(Interop.XblAchievementProgressChangeEntry interopStruct)
        {
            AchievementId = interopStruct.achievementId.GetString();
            ProgressState = interopStruct.progressState;
            Progression = new XblAchievementProgression(interopStruct.progression);
        }

        public string AchievementId { get; }
        public XblAchievementProgressState ProgressState { get; }
        public XblAchievementProgression Progression { get; }
    }
}
