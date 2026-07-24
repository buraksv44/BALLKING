using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public partial class SDK
    {
        public partial class XBL
        {
            public delegate void XblMultiplayerActivityAsyncOperationCompleted(Int32 hresult);

            public delegate void XblMultiplayerGetActivityAsyncOperationCompleted(Int32 hresult, XblMultiplayerActivityInfo[] results);

            public static Int32 XblMultiplayerActivityUpdateRecentPlayers(
                XblContextHandle xboxLiveContext,
                XblMultiplayerActivityRecentPlayerUpdate[] updates
                )
            {
                if (xboxLiveContext == null)
                {
                    return HR.E_INVALIDARG;
                }

                var interopUpdates = Converters.ConvertArrayToFixedLength(updates, updates.Length, r => new Interop.XblMultiplayerActivityRecentPlayerUpdate(r));

                Int32 hresult = XblInterop.XblMultiplayerActivityUpdateRecentPlayers(
                    xboxLiveContext.Handle,
                    interopUpdates,
                    new SizeT(updates == null ? 0 : updates.Length));

                return hresult;
            }

            public static void XblMultiplayerActivityFlushRecentPlayersAsync(
                XblContextHandle xboxLiveContext,
                XblMultiplayerActivityAsyncOperationCompleted completionRoutine)
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    completionRoutine(NativeMethods.XAsyncGetStatus(block.InteropPtr, false));
                });

                int hr = XblInterop.XblMultiplayerActivityFlushRecentPlayersAsync(
                    xboxLiveContext.Handle,
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr);
                }
            }

            public static void XblMultiplayerActivitySetActivityAsync(
                XblContextHandle xboxLiveContext,
                XblMultiplayerActivityInfo activityInfo,
                bool allowCrossPlatformJoin,
                XblMultiplayerActivityAsyncOperationCompleted completionRoutine)
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    completionRoutine(NativeMethods.XAsyncGetStatus(block.InteropPtr, false));
                });

                using (DisposableCollection disposableCollection = new DisposableCollection())
                {
                    Interop.XblMultiplayerActivityInfo interopActivityInfo = new Interop.XblMultiplayerActivityInfo(activityInfo, disposableCollection);
                    int hr = XblInterop.XblMultiplayerActivitySetActivityAsync(
                        xboxLiveContext.Handle,
                        ref interopActivityInfo,
                        allowCrossPlatformJoin,
                        asyncBlock);

                    if (HR.FAILED(hr))
                    {
                        AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                        completionRoutine(hr);
                    }
                }
            }

            public static void XblMultiplayerActivityGetActivityAsync(
                XblContextHandle xboxLiveContext,
                UInt64[] xboxUserIdList,
                XblMultiplayerGetActivityAsyncOperationCompleted completionRoutine
            )
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG, null);
                    return;
                }

                int hr;

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    hr = XblInterop.XblMultiplayerActivityGetActivityResultSize(
                        block,
                        out SizeT resultSizeInBytes
                        );

                    if (HR.FAILED(hr))
                    {
                        completionRoutine(hr, null);
                        return;
                    }

                    using (DisposableBuffer buffer = new DisposableBuffer(resultSizeInBytes.ToInt32()))
                    {
                        hr = XblInterop.XblMultiplayerActivityGetActivityResult(
                            block,
                            resultSizeInBytes,
                            buffer.IntPtr,
                            out IntPtr ptrToResults,
                            out SizeT resultCount,
                            out SizeT bufferUsed
                        );

                        if (HR.FAILED(hr))
                        {
                            completionRoutine(hr, null);
                            return;
                        }

                        completionRoutine(hr, Converters.PtrToClassArray<XblMultiplayerActivityInfo, Interop.XblMultiplayerActivityInfo>(ptrToResults, resultCount, r => new XblMultiplayerActivityInfo(r)));
                    }
                });

                hr = XblInterop.XblMultiplayerActivityGetActivityAsync(
                    xboxLiveContext.Handle,
                    xboxUserIdList,
                    new SizeT(xboxUserIdList?.Length ?? 0),
                    asyncBlock);

                if(HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, null);
                }
            }

            public static void XblMultiplayerActivityDeleteActivityAsync(
                XblContextHandle xboxLiveContext,
                XblMultiplayerActivityAsyncOperationCompleted completionRoutine)
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    completionRoutine(NativeMethods.XAsyncGetStatus(block.InteropPtr, false));
                });

                int hr = XblInterop.XblMultiplayerActivityDeleteActivityAsync(
                    xboxLiveContext.Handle,
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr);
                }
            }

            public static void XblMultiplayerActivitySendInvitesAsync(
                XblContextHandle xboxLiveContext,
                UInt64[] xboxUserIdList,
                bool allowCrossPlatformJoin,
                string connectionString,
                XblMultiplayerActivityAsyncOperationCompleted completionRoutine
            )
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    completionRoutine(NativeMethods.XAsyncGetStatus(block.InteropPtr, false));
                });

                int hr = XblInterop.XblMultiplayerActivitySendInvitesAsync(
                    xboxLiveContext.Handle,
                    xboxUserIdList,
                    new SizeT( xboxUserIdList?.Length??0),
                    allowCrossPlatformJoin,
                    Converters.StringToNullTerminatedUTF8ByteArray (connectionString),
                    asyncBlock
                );

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr);
                }
            }
        }
    }
}
