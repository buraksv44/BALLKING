using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    // typedef struct XblUserStatisticsResult
    // {
    //     uint64_t xboxUserId;
    //     XblServiceConfigurationStatistic* serviceConfigStatistics;
    //     uint32_t serviceConfigStatisticsCount;
    // } XblUserStatisticsResult;

    [StructLayout(LayoutKind.Sequential)]
    internal struct XblUserStatisticsResultInternal
    {
        internal T[] GetServiceConfigStatistics<T>(Func<XblServiceConfigurationStatisticInternal, T> ctor) =>
            Converters.PtrToClassArray(this.serviceConfigStatistics, this.serviceConfigStatisticsCount, ctor);

        internal readonly UInt64 xboxUserId;
        private readonly IntPtr serviceConfigStatistics;
        private readonly UInt32 serviceConfigStatisticsCount;
    }
}
