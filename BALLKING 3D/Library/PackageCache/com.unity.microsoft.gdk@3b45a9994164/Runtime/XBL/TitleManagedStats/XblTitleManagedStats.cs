using System;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public delegate void XblTitleManagedStatsOperationCompleted(Int32 hresult);

    public partial class SDK
    {
        public partial class XBL
        {
            public static void XblTitleManagedStatsWriteAsync(
                XblContextHandle xblContextHandle,
                UInt64 xboxUserId,
                XblTitleManagedStatistic[] statistics,
                XblTitleManagedStatsOperationCompleted completionRoutine)
            {
                if (xblContextHandle == null)
                {
                    completionRoutine(HR.E_INVALIDARG);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Int32 hresult = NativeMethods.XAsyncGetStatus(block.InteropPtr, wait: false);
                    completionRoutine(hresult);
                });

                using (var disposableCollection = new DisposableCollection())
                {
                    var interopStatistics = Array.ConvertAll(statistics, s => new Interop.XblTitleManagedStatistic(s, disposableCollection));
                    Int32 hr = XblInterop.XblTitleManagedStatsWriteAsync(
                        xblContextHandle.Handle,
                        xboxUserId,
                        interopStatistics,
                        new SizeT(interopStatistics.Length),
                        asyncBlock);

                    if (HR.FAILED(hr))
                    {
                        AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                        completionRoutine(hr);
                    }
                }
            }
            public static void XblTitleManagedStatsUpdateStatsAsync(
                XblContextHandle xblContextHandle,
                XblTitleManagedStatistic[] statistics,
                XblTitleManagedStatsOperationCompleted completionRoutine)
            {
                if (xblContextHandle == null)
                {
                    completionRoutine(HR.E_INVALIDARG);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Int32 hresult = NativeMethods.XAsyncGetStatus(block.InteropPtr, wait: false);
                    completionRoutine(hresult);
                });

                using (var disposableCollection = new DisposableCollection())
                {
                    var interopStatistics = Array.ConvertAll(statistics, s => new Interop.XblTitleManagedStatistic(s, disposableCollection));
                    Int32 hr = XblInterop.XblTitleManagedStatsUpdateStatsAsync(
                        xblContextHandle.Handle,
                        interopStatistics,
                        new SizeT(interopStatistics.Length),
                        asyncBlock);

                    if (HR.FAILED(hr))
                    {
                        AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                        completionRoutine(hr);
                    }
                }
            }

            public static void XblTitleManagedStatsDeleteStatsAsync(
                XblContextHandle xblContextHandle,
                string[] statisticNames,
                XblTitleManagedStatsOperationCompleted completionRoutine)
            {
                if (xblContextHandle == null)
                {
                    completionRoutine(HR.E_INVALIDARG);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Int32 hresult = NativeMethods.XAsyncGetStatus(block.InteropPtr, wait: false);
                    completionRoutine(hresult);
                });

                using (var disposableCollection = new DisposableCollection())
                {
                    SizeT interopStatisticsCount;
                    var interopStatistics = Interop.Converters.StringArrayToUTF8StringArray(statisticNames, disposableCollection, out interopStatisticsCount);
                    Int32 hr = XblInterop.XblTitleManagedStatsDeleteStatsAsync(
                        xblContextHandle.Handle,
                        interopStatistics,
                        interopStatisticsCount,
                        asyncBlock);

                    if (HR.FAILED(hr))
                    {
                        AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                        completionRoutine(hr);
                    }
                }
            }
        }
    }
}