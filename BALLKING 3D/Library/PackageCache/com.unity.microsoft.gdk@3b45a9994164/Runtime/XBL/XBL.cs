using System;
using System.Runtime.InteropServices;

using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public partial class SDK
    {
        public partial class XBL
        {
            public const int StandardScidLength = 36;

            public delegate void XblCleanupResult(Int32 hresult);

            // Prevent users from calling close handles after XBLCleanup has been called
            static internal bool IsXblInitialized = false;

#if (!UNITY_STANDALONE_WIN && ENABLE_IL2CPP)
            public static Int32 XblInitialize(string scid)
            {
                IntPtr scidPtr = Marshal.StringToHGlobalAnsi(scid);

                XblInitArgs args = new XblInitArgs();
                args.queue = defaultQueue;
                args.scid = scidPtr;

                Int32 blockSize = Marshal.SizeOf(args);
                IntPtr argsPnt = Marshal.AllocHGlobal(blockSize);
                Marshal.StructureToPtr(args, argsPnt, false);

                XblInitArgsPtr argsPtr = new XblInitArgsPtr(argsPnt);

                Int32 result = XblInterop.XblInitialize(argsPtr);
                Marshal.FreeHGlobal(scidPtr);
                Marshal.FreeHGlobal(argsPnt);

                IsXblInitialized = true;

                return result;
            }
#else // (!UNITY_STANDALONE_WIN && ENABLE_IL2CPP)
            public static Int32 XblInitialize(string scid)
            {
                IsXblInitialized = true;

                return XblInterop.XblWrapper_XblInitialize(
                    Converters.StringToNullTerminatedUTF8ByteArray(scid),
                    defaultQueue);
            }
#endif // (!UNITY_STANDALONE_WIN && ENABLE_IL2CPP)
            public static void XblCleanup(XblCleanupResult completionRoutine)
            {
                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Int32 hresult = NativeMethods.XAsyncGetStatus(block.InteropPtr, wait: false);
                    if (completionRoutine != null)
                        completionRoutine(hresult);
                });

                Int32 hr = XblInterop.XblCleanupAsync(asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    if (completionRoutine != null)
                        completionRoutine(hr);
                }

#if (UNITY_STANDALONE_WIN || !ENABLE_IL2CPP)
                // XblWrapper_XblCleanupAsync is only available in GDK versions later than April 2025
                if (SDK.GetGdkEdition() >= 250400)
                {
                    XAsyncBlock wrapperAsyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                    {
                        Int32 hresult = NativeMethods.XAsyncGetStatus(block.InteropPtr, wait: false);
                        // XblCleanupAsync already invoke completionRoutine, only report it something wrong happend
                        if (completionRoutine != null || hresult == HR.S_OK)
                            completionRoutine(hresult);
                    });

                    hr = XblInterop.XblWrapper_XblCleanupAsync(wrapperAsyncBlock);

                    if (HR.FAILED(hr))
                    {
                        AsyncHelpers.CleanupAsyncBlock(wrapperAsyncBlock);
                        if (completionRoutine != null)
                            completionRoutine(hr);
                    }
                }
#endif
                IsXblInitialized = false;
            }

            public static Int32 XblContextCreateHandle(XUserHandle user, out XblContextHandle context)
            {
                if (user == null)
                {
                    context = default(XblContextHandle);
                    return HR.E_INVALIDARG;
                }

                Interop.XblContextHandle interopContext;
                Int32 hresult = XblInterop.XblContextCreateHandle(user.Handle, out interopContext);
                if (HR.SUCCEEDED(hresult))
                {
                    context = new XblContextHandle(interopContext);
                    context.m_gCHandle = GCHandle.Alloc(context, GCHandleType.Normal);
                }
                else
                {
                    context = default(XblContextHandle);
                }
                return hresult;
            }

            public static void XblContextCloseHandle(XblContextHandle xboxLiveContextHandle)
            {
                xboxLiveContextHandle.Close();
            }

            /// <summary>
            /// Wraps the underlying native XblContextDuplicateHandle API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/xbox_live_context_c/functions/xblcontextduplicatehandle
            /// </summary>
            /// <param name="srcXboxLiveContextHandle"></param>
            /// <param name="dstXboxLiveContextHandle"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblContextDuplicateHandle(
                XblContextHandle srcXboxLiveContextHandle,
                out XblContextHandle dstXboxLiveContextHandle)
            {
                var duplicatedHandle = new Interop.XblContextHandle();
                int result = HR.S_OK;

                unsafe
                {
                    result = XboxLiveContext.XblContextDuplicateHandle(
                        srcXboxLiveContextHandle.Handle,
                        out duplicatedHandle.handle);
                }

                if (HR.SUCCEEDED(result))
                {
                    dstXboxLiveContextHandle = new XblContextHandle(duplicatedHandle);
                }
                else
                {
                    dstXboxLiveContextHandle = null;
                }

                return result;
            }

            /// <summary>
            /// Wraps the underlying native XblContextGetUser API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/xbox_live_context_c/functions/xblcontextgetuser
            /// </summary>
            /// <param name="xboxLiveContextHandle"></param>
            /// <param name="dstUserHandle"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblContextGetUser(
                XblContextHandle xboxLiveContextHandle,
                out XUserHandle dstUserHandle)
            {
                var resultUserHandle = new IntPtr();
                int result = HR.S_OK;

                unsafe
                {
                    result = XboxLiveContext.XblContextGetUser(
                        xboxLiveContextHandle.Handle,
                        out resultUserHandle);
                }

                if (HR.SUCCEEDED(result))
                {
                    dstUserHandle = new XUserHandle(resultUserHandle);
                }
                else
                {
                    dstUserHandle = null;
                }

                return result;
            }

            /// <summary>
            /// Wraps the underlying native XblContextGetXboxUserId API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/xbox_live_context_c/functions/xblcontextgetxboxuserid
            /// </summary>
            /// <param name="xboxLiveContextHandle"></param>
            /// <param name="dstXboxUserId"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblContextGetXboxUserId(
                XblContextHandle xboxLiveContextHandle,
                ref ulong dstXboxUserId)
            {
                ulong resultUserId = 0;
                int result = HR.S_OK;

                unsafe
                {
                    result = XboxLiveContext.XblContextGetXboxUserId(
                        xboxLiveContextHandle.Handle,
                        out resultUserId);
                }

                if (HR.SUCCEEDED(result))
                {
                    dstXboxUserId = resultUserId;
                }
                else
                {
                    dstXboxUserId = 0;
                }

                return result;
            }

            /// <summary>
            /// Wraps the underlying native XblGetScid API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/xbox_live_global_c/functions/xblgetscid
            /// </summary>
            /// <param name="resultScid"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblGetScid(ref string resultScid)
            {
                resultScid = string.Empty;
                int result = HR.S_OK;

                unsafe
                {
                    sbyte* scidPointer;
                    result = XboxLiveGlobal.XblGetScid(&scidPointer);
                    if (HR.SUCCEEDED(result))
                    {
                        resultScid = Converters.BytePointerToString(
                            (byte*)scidPointer,
                            StandardScidLength);
                    }
                }

                return result;
            }
        }
    }
}
