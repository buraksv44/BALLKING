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
    public class XblMultiplayerSessionInitializationInfo
    {
        internal XblMultiplayerSessionInitializationInfo(Interop.XblMultiplayerSessionInitializationInfo interopHandle)
        {
            this.Stage = interopHandle.Stage;
            this.StageStartTime = interopHandle.StageStartTime.DateTime;
            this.Episode = interopHandle.Episode;
        }

        public XblMultiplayerInitializationStage Stage { get; }

        public DateTime StageStartTime { get; }

        public UInt32 Episode { get; }
    }
}
