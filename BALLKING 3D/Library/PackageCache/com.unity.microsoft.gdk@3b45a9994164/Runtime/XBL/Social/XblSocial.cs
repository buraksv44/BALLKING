using System;
using System.Collections.Generic;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public struct XblSocialRelationshipChangeEventArgs
    {
        public ulong callerXboxUserId;
        public XblSocialNotificationType socialNotification;
        public ulong[] xboxUserIds;
    }

    public delegate void XblSocialGetSocialRelationshipsResult(Int32 hresult, XblSocialRelationshipResult handle);
    public delegate void XblSocialRelationshipResultGetNextResult(Int32 hresult, XblSocialRelationshipResult handle);
    public delegate void XblSocialRelationshipChangedCallback(XblSocialRelationshipChangeEventArgs eventArgs);

    public partial class SDK
    {
        public partial class XBL
        {
            /// <summary>
            /// Wraps the underlying native XblSocialGetSocialRelationshipsAsync API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/social_c/functions/xblsocialgetsocialrelationshipsasync
            /// </summary>
            /// <param name="xboxLiveContext"></param>
            /// <param name="xboxUserId"></param>
            /// <param name="socialRelationshipFilter"></param>
            /// <param name="startIndex"></param>
            /// <param name="maxItems"></param>
            /// <param name="completionCallback"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static void XblSocialGetSocialRelationshipsAsync(
                XblContextHandle xboxLiveContext,
                UInt64 xboxUserId,
                XblSocialRelationshipFilter socialRelationshipFilter,
                uint startIndex,
                uint maxItems,
                XblSocialGetSocialRelationshipsResult completionRoutine
            )
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG, null);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Interop.XblSocialRelationshipResultHandle result;
                    Int32 hresult = XblInterop.XblSocialGetSocialRelationshipsResult(block, out result);
                    if (HR.FAILED(hresult))
                    {
                        completionRoutine(hresult, null);
                        return;
                    }

                    completionRoutine(hresult, new XblSocialRelationshipResult(result));
                });

                Int32 hr = XblInterop.XblSocialGetSocialRelationshipsAsync(
                    xboxLiveContext.Handle,
                    xboxUserId,
                    socialRelationshipFilter,
                    new SizeT(startIndex),
                    new SizeT(maxItems),
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, null);
                }
            }

            /// <summary>
            /// Wraps the underlying native XblSocialRelationshipResultGetRelationships API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/social_c/functions/xblsocialrelationshipresultgetrelationships
            /// </summary>
            /// <param name="resultHandle"></param>
            /// <param name="relationships"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static Int32 XblSocialRelationshipResultGetRelationships(
                XblSocialRelationshipResult resultHandle,
                out XblSocialRelationship[] relationships
            )
            {
                if (resultHandle == null)
                {
                    relationships = null;
                    return Interop.HR.E_INVALIDARG;
                }

                IntPtr interopRelationships;
                SizeT interopRelationshipsSize;

                Int32 hr = XblInterop.XblSocialRelationshipResultGetRelationships(resultHandle.InteropHandle, out interopRelationships, out interopRelationshipsSize);

                if (Interop.HR.FAILED(hr))
                {
                    relationships = null;
                    return hr;
                }

                if (SDK.GetGdkEdition() >= 241000)
                {
                    relationships = Converters.PtrToClassArray<XblSocialRelationship, Interop.XblSocialRelationship2>(
                        interopRelationships, interopRelationshipsSize, c => new XblSocialRelationship(c));
                }
                else
                {
                    relationships = Converters.PtrToClassArray<XblSocialRelationship, Interop.XblSocialRelationship>(
                        interopRelationships, interopRelationshipsSize, c => new XblSocialRelationship(c));
                }

                return hr;
            }

            /// <summary>
            /// Wraps the underlying native XblSocialRelationshipResultHasNext API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/social_c/functions/xblsocialrelationshipresulthasnext
            /// </summary>
            /// <param name="socialHandle"></param>
            /// <param name="hasNext"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static Int32 XblSocialRelationshipResultHasNext(
                XblSocialRelationshipResult result,
                out bool hasNext
            )
            {
                hasNext = false;
                if (result == null)
                {
                    return HR.E_INVALIDARG;
                }

                Int32 hr = XblInterop.XblSocialRelationshipResultHasNext(result.InteropHandle, out hasNext);

                return hr;
            }

            /// <summary>
            /// Wraps the underlying native XblSocialRelationshipResultGetTotalCount API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/social_c/functions/xblsocialrelationshipresultgettotalcount
            /// </summary>
            /// <param name="socialHandle"></param>
            /// <param name="totalCount"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static Int32 XblSocialRelationshipResultGetTotalCount(
                XblSocialRelationshipResult result,
                out UInt32 totalCount
            )
            {
                totalCount = 0;
                if (result == null)
                {
                    return HR.E_INVALIDARG;
                }

                SizeT totalCountInterop;

                Int32 hr = XblInterop.XblSocialRelationshipResultGetTotalCount(result.InteropHandle, out totalCountInterop);
                if (HR.FAILED(hr))
                {
                    return hr;
                }

                totalCount = totalCountInterop.ToUInt32();

                return hr;
            }

            /// <summary>
            /// Wraps the underlying native XblSocialRelationshipResultGetNextAsync API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/social_c/functions/xblsocialrelationshipresultgetnextasync
            /// </summary>
            /// <param name="xboxLiveContext"></param>
            /// <param name="socialHandle"></param>
            /// <param name="maxItems"></param>
            /// <param name="completionCallback"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static void XblSocialRelationshipResultGetNextAsync(
                XblContextHandle xboxLiveContext,
                XblSocialRelationshipResult result,
                uint maxItems,
                XblSocialRelationshipResultGetNextResult completionRoutine
            )
            {
                if (xboxLiveContext == null || result == null)
                {
                    completionRoutine(HR.E_INVALIDARG, null);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Interop.XblSocialRelationshipResultHandle getNextResult;
                    Int32 hresult = XblInterop.XblSocialRelationshipResultGetNextResult(block, out getNextResult);
                    if (HR.FAILED(hresult))
                    {
                        completionRoutine(hresult, null);
                        return;
                    }

                    completionRoutine(hresult, new XblSocialRelationshipResult(getNextResult));
                });

                Int32 hr = XblInterop.XblSocialRelationshipResultGetNextAsync(
                    xboxLiveContext.Handle,
                    result.InteropHandle,
                    new SizeT(maxItems),
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, null);
                }
            }

            /// <summary>
            /// Wraps the underlying native XblSocialRelationshipResultDuplicateHandle API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/social_c/functions/xblsocialrelationshipresultduplicatehandle
            /// </summary>
            /// <param name="socialHandle"></param>
            /// <param name="duplicatedHandle"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static Int32 XblSocialRelationshipResultDuplicateHandle(
                XblSocialRelationshipResult handle,
                out XblSocialRelationshipResult duplicatedHandle
            )
            {
                duplicatedHandle = null;
                if (handle == null)
                {
                    return HR.E_INVALIDARG;
                }

                XblSocialRelationshipResultHandle duplicatedHandleInterop;

                Int32 hr = XblInterop.XblSocialRelationshipResultDuplicateHandle(handle.InteropHandle, out duplicatedHandleInterop);
                if (HR.FAILED(hr))
                {
                    return hr;
                }

                duplicatedHandle = new XblSocialRelationshipResult(duplicatedHandleInterop);
                return hr;
            }

            /// <summary>
            /// Wraps the underlying native XblSocialRelationshipResultCloseHandle API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/social_c/functions/xblsocialrelationshipresultclosehandle
            /// </summary>
            /// <param name="socialHandle"></param>
            public static void XblSocialRelationshipResultCloseHandle(XblSocialRelationshipResult handle)
            {
                handle.Dispose();
            }

            /// <summary>
            /// Wraps the underlying native XblSocialAddSocialRelationshipChangedHandler API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/social_c/functions/xblsocialaddsocialrelationshipchangedhandler
            /// </summary>
            /// <param name="xboxLiveContext"></param>
            /// <param name="eventCallback"></param>
            /// <returns>an integer representing a callback function ID, or 0 on failure</returns>
            public static int XblSocialAddSocialRelationshipChangedHandler(
                XblContextHandle xboxLiveContext,
                XblSocialRelationshipChangedCallback eventCallback)
            {
                int callbackFunctionId;

                unsafe
                {
                    var context = _socialRelationshipChangeCallbackManager.GetUniqueContext();
                    callbackFunctionId = XblInterop.XblSocialAddSocialRelationshipChangedHandler(
                        xboxLiveContext.Handle,
                        SocialRelationshipChangeCallbackManager.InteropPInvokeCallback,
                        context);

                    if (callbackFunctionId != 0)
                    {
                        _socialRelationshipChangeCallbackManager.AddCallbackForId(
                            callbackFunctionId, context, eventCallback);
                    }
                }

                return callbackFunctionId;
            }

            /// <summary>
            /// Wraps the underlying native XblSocialRemoveSocialRelationshipChangedHandler API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/social_c/functions/xblsocialremovesocialrelationshipchangedhandler
            /// </summary>
            /// <param name="xboxLiveContext"></param>
            /// <param name="callbackFunctionId"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblSocialRemoveSocialRelationshipChangedHandler(
                XblContextHandle xboxLiveContext,
                int callbackFunctionId)
            {
                int result;

                unsafe
                {
                    result = XblInterop.XblSocialRemoveSocialRelationshipChangedHandler(
                        xboxLiveContext.Handle,
                        callbackFunctionId);

                    if (Interop.HR.SUCCEEDED(result))
                    {
                        _socialRelationshipChangeCallbackManager.RemoveCallbackForId(
                            callbackFunctionId);
                    }
                }

                return result;
            }

            private static SocialRelationshipChangeCallbackManager _socialRelationshipChangeCallbackManager =
                new SocialRelationshipChangeCallbackManager();

            private class SocialRelationshipChangeCallbackManager :
                InteropCallbackManager<XblSocialRelationshipChangedCallback>
            {
                [MonoPInvokeCallback]
                internal static unsafe void InteropPInvokeCallback(
                    Interop.XblSocialRelationshipChangeEventArgs* eventArgs,
                    IntPtr context)
                {
                    if (!_socialRelationshipChangeCallbackManager._contextToFunctionId.ContainsKey(context))
                    {
                        return;
                    }

                    var functionId = _socialRelationshipChangeCallbackManager._contextToFunctionId[context];
                    _socialRelationshipChangeCallbackManager.IssueEventCallback(functionId, eventArgs);
                }

                private unsafe void IssueEventCallback(
                    int functionId,
                    Interop.XblSocialRelationshipChangeEventArgs* eventArgs)
                {
                    if (!_functionIdToHandler.ContainsKey(functionId))
                    {
                        return;
                    }

                    var eventHandler = _functionIdToHandler[functionId];

                    var callbackEventArgs = new XblSocialRelationshipChangeEventArgs();

                    callbackEventArgs.callerXboxUserId = eventArgs->callerXboxUserId;
                    callbackEventArgs.socialNotification = eventArgs->socialNotification;
                    callbackEventArgs.xboxUserIds = new ulong[eventArgs->xboxUserIdsCount.ToInt32()];
                    var idsPtr = eventArgs->xboxUserIds;
                    for (var i = 0; i < eventArgs->xboxUserIdsCount.ToInt32(); i++)
                    {
                        callbackEventArgs.xboxUserIds[i] = *idsPtr;
                        idsPtr++;
                    }

                    if (eventHandler.Callback != null)
                    {
                        eventHandler.Callback.Invoke(callbackEventArgs);
                    }
                }
            }
        }
    }
}
