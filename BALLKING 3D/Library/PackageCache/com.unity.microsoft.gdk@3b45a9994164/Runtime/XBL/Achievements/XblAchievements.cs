using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public partial class SDK
    {
        public partial class XBL
        {
            public delegate void XblAchievementsResultGetNextResult(Int32 hresult, XblAchievementsResultHandle result);
            public delegate void XblAchievementsGetAchievementsForTitleIdResult(Int32 hresult, XblAchievementsResultHandle result);
            public delegate void XblAchievementsUpdateAchievementResult(Int32 hresult);
            public delegate void XblAchievementsUpdateAchievementForTitleIdResult(Int32 hresult);
            public delegate void XblAchievementsGetAchievementResult(Int32 hresult, XblAchievementsResultHandle result);
            public delegate void XblAchievementsProgressChangeHandlerResult(Int32 hresult, XblAchievementProgressChangeEventArgs eventArgs, IntPtr context);
            public static Int32 XblAchievementsResultGetAchievements(XblAchievementsResultHandle resultHandle, out XblAchievement[] achievements)
            {
                if (resultHandle == null)
                {
                    achievements = null;
                    return HR.E_INVALIDARG;
                }

                IntPtr interopAchievements;
                SizeT achievementsCount;
                Int32 hresult = XblInterop.XblAchievementsResultGetAchievements(resultHandle.Handle, out interopAchievements, out achievementsCount);

                if (HR.FAILED(hresult))
                {
                    achievements = null;
                    return hresult;
                }

                achievements = Converters.PtrToClassArray<XblAchievement, Interop.XblAchievement>(interopAchievements, achievementsCount, a => new XblAchievement(a));
                return hresult;
            }

            public static Int32 XblAchievementsResultHasNext(XblAchievementsResultHandle resultHandle, out bool hasNext)
            {
                if (resultHandle == null)
                {
                    hasNext = default(bool);
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblAchievementsResultHasNext(resultHandle.Handle, out hasNext);
            }

            public static void XblAchievementsResultGetNextAsync(XblAchievementsResultHandle resultHandle, UInt32 maxItems, XblAchievementsResultGetNextResult completionRoutine)
            {
                if (resultHandle == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblAchievementsResultHandle));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Interop.XblAchievementsResultHandle interopHandle;
                    Int32 hresult = XblInterop.XblAchievementsResultGetNextResult(block, out interopHandle);

                    if (HR.SUCCEEDED(hresult))
                    {
                        completionRoutine(hresult, new XblAchievementsResultHandle(interopHandle));
                    }
                    else
                    {
                        completionRoutine(hresult, default(XblAchievementsResultHandle));
                    }
                });

                Int32 hr = XblInterop.XblAchievementsResultGetNextAsync(resultHandle.Handle, maxItems, asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, default(XblAchievementsResultHandle));
                }
            }

            public static void XblAchievementsGetAchievementsForTitleIdAsync(
                XblContextHandle xboxLiveContext,
                UInt64 xboxUserId,
                UInt32 titleId,
                XblAchievementType type,
                bool unlockedOnly,
                XblAchievementOrderBy orderBy,
                UInt32 skipItems,
                UInt32 maxItems,
                XblAchievementsGetAchievementsForTitleIdResult completionRoutine)
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblAchievementsResultHandle));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Interop.XblAchievementsResultHandle interopHandle;
                    Int32 hresult = XblInterop.XblAchievementsGetAchievementsForTitleIdResult(block, out interopHandle);

                    if (HR.SUCCEEDED(hresult))
                    {
                        completionRoutine(hresult, new XblAchievementsResultHandle(interopHandle));
                    }
                    else
                    {
                        completionRoutine(hresult, default(XblAchievementsResultHandle));
                    }
                });

                Int32 hr = XblInterop.XblAchievementsGetAchievementsForTitleIdAsync(
                    xboxLiveContext.Handle,
                    xboxUserId,
                    titleId,
                    type,
                    unlockedOnly,
                    orderBy,
                    skipItems,
                    maxItems,
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, default(XblAchievementsResultHandle));
                }
            }

            public static void XblAchievementsUpdateAchievementAsync(
                XblContextHandle xboxLiveContext,
                UInt64 xboxUserId,
                string achievementId,
                UInt32 percentComplete,
                XblAchievementsUpdateAchievementResult completionRoutine)
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Int32 hresult = NativeMethods.XAsyncGetStatus(block.InteropPtr, wait: false);
                    completionRoutine(hresult);
                });

                Int32 hr = XblInterop.XblAchievementsUpdateAchievementAsync(
                    xboxLiveContext.Handle,
                    xboxUserId,
                    Converters.StringToNullTerminatedUTF8ByteArray(achievementId),
                    percentComplete,
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr);
                }
            }

            public static void XblAchievementsUpdateAchievementAsync(
                XblContextHandle xboxLiveContext,
                UInt64 xboxUserId,
                UInt32 titleId,
                string serviceConfigurationId,
                string achievementId,
                UInt32 percentComplete,
                XblAchievementsUpdateAchievementForTitleIdResult completionRoutine)
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Int32 hresult = NativeMethods.XAsyncGetStatus(block.InteropPtr, wait: false);
                    completionRoutine(hresult);
                });

                Int32 hr = XblInterop.XblAchievementsUpdateAchievementForTitleIdAsync(
                    xboxLiveContext.Handle,
                    xboxUserId,
                    titleId,
                    Converters.StringToNullTerminatedUTF8ByteArray(serviceConfigurationId),
                    Converters.StringToNullTerminatedUTF8ByteArray(achievementId),
                    percentComplete,
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr);
                }
            }


            public static void XblAchievementsUpdateAchievementForTitleIdAsync(
                XblContextHandle xboxLiveContext,
                UInt64 xboxUserId,
                UInt32 titleId,
                string serviceConfigurationId,
                string achievementId,
                UInt32 percentComplete,
                XblAchievementsUpdateAchievementForTitleIdResult completionRoutine)
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Int32 hresult = NativeMethods.XAsyncGetStatus(block.InteropPtr, wait: false);
                    completionRoutine(hresult);
                });

                Int32 hr = XblInterop.XblAchievementsUpdateAchievementForTitleIdAsync(
                    xboxLiveContext.Handle,
                    xboxUserId,
                    titleId,
                    Converters.StringToNullTerminatedUTF8ByteArray(serviceConfigurationId),
                    Converters.StringToNullTerminatedUTF8ByteArray(achievementId),
                    percentComplete,
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr);
                }
            }

            public static void XblAchievementsGetAchievementAsync(
                XblContextHandle xboxLiveContext,
                UInt64 xboxUserId,
                string serviceConfigurationId,
                string achievementId,
                XblAchievementsGetAchievementResult completionRoutine)
            {
                if (xboxLiveContext == null)
                {
                    ;
                    completionRoutine(HR.E_INVALIDARG, default(XblAchievementsResultHandle));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Interop.XblAchievementsResultHandle interopHandle;
                    Int32 hresult = XblInterop.XblAchievementsGetAchievementResult(block, out interopHandle);
                    if (HR.SUCCEEDED(hresult))
                    {
                        completionRoutine(hresult, new XblAchievementsResultHandle(interopHandle));
                    }
                    else
                    {
                        completionRoutine(hresult, default(XblAchievementsResultHandle));
                    }
                });

                Int32 hr = XblInterop.XblAchievementsGetAchievementAsync(
                    xboxLiveContext.Handle,
                    xboxUserId,
                    Converters.StringToNullTerminatedUTF8ByteArray(serviceConfigurationId),
                    Converters.StringToNullTerminatedUTF8ByteArray(achievementId),
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, default(XblAchievementsResultHandle));
                }
            }


            public static XblFunctionContext XblAchievementsAddAchievementProgressChangeHandler(XblContextHandle xboxLiveContext, XblAchievementsProgressChangeHandlerResult handler, IntPtr handlerContext)
            {
                if (xboxLiveContext == null)
                {
                    handler(HR.E_INVALIDARG, default(XblAchievementProgressChangeEventArgs), IntPtr.Zero);
                    return default(XblFunctionContext);
                }

                XblInterop.XblAchievementsProgressChangeHandler interopHandler = (Interop.XblAchievementProgressChangeEventArgs eventArgsInterop, IntPtr context) =>
                {
                    XblAchievementProgressChangeEventArgs eventArgs = new XblAchievementProgressChangeEventArgs(eventArgsInterop);
                    handler(HR.S_OK, eventArgs, context);
                };

                return XblInterop.XblAchievementsAddAchievementProgressChangeHandler(xboxLiveContext.Handle, interopHandler, handlerContext);

            }

            public static XblFunctionContext XblAchievementsAddAchievementProgressChangeHandler(XblContextHandle xboxLiveContext, XblAchievementsProgressChangeHandlerResult handler)
            {
                return XblAchievementsAddAchievementProgressChangeHandler(xboxLiveContext, handler, IntPtr.Zero);
            }

            public static void XblAchievementsRemoveAchievementProgressChangeHandler(XblContextHandle xblContextHandle, XblFunctionContext functionContext)
            {
                XblInterop.XblAchievementsRemoveAchievementProgressChangeHandler(xblContextHandle.Handle, functionContext);
            }

            public static Int32 XblAchievementsResultDuplicateHandle(XblAchievementsResultHandle handle, out XblAchievementsResultHandle duplicatedHandle)
            {
                if (handle == null)
                {
                    duplicatedHandle = default(XblAchievementsResultHandle);
                    return HR.E_INVALIDARG;
                }

                Interop.XblAchievementsResultHandle duplicatedInteropHandle;
                Int32 hresult = XblInterop.XblAchievementsResultDuplicateHandle(handle.Handle, out duplicatedInteropHandle);
                if (HR.SUCCEEDED(hresult))
                {
                    duplicatedHandle = new XblAchievementsResultHandle(duplicatedInteropHandle);
                }
                else
                {
                    duplicatedHandle = default(XblAchievementsResultHandle);
                }
                return hresult;
            }

            public static void XblAchievementsResultCloseHandle(XblAchievementsResultHandle handle)
            {
                if (handle == null)
                {
                    return;
                }

                handle.Close();
            }
        }
    }
}
