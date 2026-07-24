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
    public class XblServiceConfigurationStatistic
    {
        internal XblServiceConfigurationStatistic(Interop.XblServiceConfigurationStatisticInternal interopStatistic)
        {
            this.ServiceConfigurationId = Interop.Converters.ByteArrayToString(interopStatistic.serviceConfigurationId);
            this.Statistics = interopStatistic.GetStatistics(s => new XblStatistic(s));
        }

        public string ServiceConfigurationId { get; private set; }
        public XblStatistic[] Statistics { get; private set; }
    }
}
