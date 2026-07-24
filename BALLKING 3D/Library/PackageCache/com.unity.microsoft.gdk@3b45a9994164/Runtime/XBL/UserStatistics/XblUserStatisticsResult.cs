using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblUserStatisticsResult
    {
        internal XblUserStatisticsResult(Interop.XblUserStatisticsResultInternal interopResult)
        {
            this.XboxUserId = interopResult.xboxUserId;
            this.ServiceConfigStatistics = interopResult.GetServiceConfigStatistics(scs => new XblServiceConfigurationStatistic(scs));
        }

        public UInt64 XboxUserId { get; private set; }
        public XblServiceConfigurationStatistic[] ServiceConfigStatistics { get; private set; }
    }
}
