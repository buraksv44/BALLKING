using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    public static unsafe partial class UserStatistics
    {
        [DllImport(XblInterop.XblThunkDllName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        [return: NativeTypeName("XblFunctionContext")]
        public static extern int XblUserStatisticsAddStatisticChangedHandler([NativeTypeName("XblContextHandle")] IntPtr xblContextHandle, [NativeTypeName("XblStatisticChangedHandler")] XblStatisticChangedHandler handler, void* handlerContext);

        [DllImport(XblInterop.XblThunkDllName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        public static extern void XblUserStatisticsRemoveStatisticChangedHandler([NativeTypeName("XblContextHandle")] IntPtr xblContextHandle, [NativeTypeName("XblFunctionContext")] int context);

        [DllImport(XblInterop.XblThunkDllName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        [return: NativeTypeName("HRESULT")]
        public static extern int XblUserStatisticsTrackStatistics([NativeTypeName("XblContextHandle")] IntPtr xblContextHandle, [NativeTypeName("const uint64_t *")] ulong* xboxUserIds, [NativeTypeName("size_t")] UIntPtr xboxUserIdsCount, [NativeTypeName("const char *")] sbyte* serviceConfigurationId, [NativeTypeName("const char **")] sbyte** statisticNames, [NativeTypeName("size_t")] UIntPtr statisticNamesCount);

        [DllImport(XblInterop.XblThunkDllName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        [return: NativeTypeName("HRESULT")]
        public static extern int XblUserStatisticsStopTrackingStatistics([NativeTypeName("XblContextHandle")] IntPtr xblContextHandle, [NativeTypeName("const uint64_t *")] ulong* xboxUserIds, [NativeTypeName("size_t")] UIntPtr xboxUserIdsCount, [NativeTypeName("const char *")] sbyte* serviceConfigurationId, [NativeTypeName("const char **")] sbyte** statisticNames, [NativeTypeName("size_t")] UIntPtr statisticNamesCount);

        [DllImport(XblInterop.XblThunkDllName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
        [return: NativeTypeName("HRESULT")]
        public static extern int XblUserStatisticsStopTrackingUsers([NativeTypeName("XblContextHandle")] IntPtr xblContextHandle, [NativeTypeName("const uint64_t *")] ulong* xboxUserIds, [NativeTypeName("size_t")] UIntPtr xboxUserIdsCount);
    }
}
