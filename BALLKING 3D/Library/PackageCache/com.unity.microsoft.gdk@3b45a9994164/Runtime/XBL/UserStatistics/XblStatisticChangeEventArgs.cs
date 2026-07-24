using System;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public struct XblStatisticChangeEventArgs
    {
        internal XblStatisticChangeEventArgs(Interop.XblStatisticChangeEventArgs interopStruct)
        {
            unsafe
            {
                this.xboxUserId = interopStruct.xboxUserId;
                this.serviceConfigurationId = new string(interopStruct.serviceConfigurationId);
                this.latestStatistic = new XblStatistic(interopStruct.latestStatistic);
            }
        }

        public ulong xboxUserId;
        public string serviceConfigurationId;
        public XblStatistic latestStatistic;
    }
}
