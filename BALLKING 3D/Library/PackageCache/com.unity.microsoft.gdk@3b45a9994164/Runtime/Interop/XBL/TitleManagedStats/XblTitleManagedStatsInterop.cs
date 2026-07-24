using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{

    internal partial class XblInterop
    {
        //STDAPI XblTitleManagedStatsWriteAsync(
        //    _In_ XblContextHandle xblContextHandle,
        //    _In_ uint64_t xboxUserId,
        //    _In_ const XblTitleManagedStatistic* statistics,
        //    _In_ size_t statisticsCount,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblTitleManagedStatsWriteAsync(
            IntPtr xblContextHandle,
            UInt64 xboxUserId,
            [In] XblTitleManagedStatistic[] statistics,
            SizeT statisticsCount,
            XAsyncBlockPtr async);

        //STDAPI XblTitleManagedStatsUpdateStatsAsync(
        //    _In_ XblContextHandle xblContextHandle,
        //    _In_ const XblTitleManagedStatistic* statistics,
        //    _In_ size_t statisticsCount,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblTitleManagedStatsUpdateStatsAsync(
            IntPtr xblContextHandle,
            [In] XblTitleManagedStatistic[] statistics,
            SizeT statisticsCount,
            XAsyncBlockPtr async);

        //STDAPI XblTitleManagedStatsDeleteStatsAsync(
        //    _In_ XblContextHandle xblContextHandle,
        //    _In_ const char** statisticNames,
        //    _In_ size_t statisticNamesCount,
        //    _In_ XAsyncBlock* async
        //) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblTitleManagedStatsDeleteStatsAsync(
            IntPtr xblContextHandle,
            IntPtr statisticNames,
            SizeT statisticNamesCount,
            XAsyncBlockPtr async);


    }
}