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
    public class XblAchievementsManagerEvent
    {
        internal XblAchievementsManagerEvent(Interop.XblAchievementsManagerEvent interopStruct)
        {
            this.progressInfo = new XblAchievementProgressChangeEntry(interopStruct.progressInfo);
            this.xboxUserId = interopStruct.xboxUserId;
            this.eventType = interopStruct.eventType;
        }

        public XblAchievementProgressChangeEntry progressInfo { get; }
        public UInt64 xboxUserId { get; }
        public XblAchievementsManagerEventType eventType { get; }
    }
}
