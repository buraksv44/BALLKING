using System;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public delegate void XblHttpCallPerformCompleted(Int32 hresult);

    public partial class SDK
    {
        public partial class XBL
        {
            public static Int32 XblHttpCallRequestSetRequestBodyBytes(XblHttpCallHandle call, Byte[] requestBodyBytes)
            {
                if (call == null || requestBodyBytes == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblHttpCallRequestSetRequestBodyBytes(call.Handle, requestBodyBytes, (uint)requestBodyBytes.Length);
            }

            public static Int32 XblHttpCallGetNetworkErrorCode(XblHttpCallHandle call, out Int32 networkErrorCode, out UInt32 platformNetworkErrorCode)
            {
                if (call == null)
                {
                    networkErrorCode = 0;
                    platformNetworkErrorCode = 0;
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblHttpCallGetNetworkErrorCode(call.Handle, out networkErrorCode, out platformNetworkErrorCode);
            }

            public static Int32 XblHttpCallRequestSetLongHttpCall(XblHttpCallHandle call, bool longHttpCall)
            {
                if (call == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblHttpCallRequestSetLongHttpCall(call.Handle, new NativeBool(longHttpCall));
            }

            public static void XblHttpCallPerformAsync(XblHttpCallHandle call, XblHttpCallResponseBodyType type, XblHttpCallPerformCompleted completionRoutine)
            {
                if (call == null)
                {
                    completionRoutine(HR.E_INVALIDARG);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    completionRoutine(NativeMethods.XAsyncGetStatus(block.InteropPtr, wait: false));
                });

                int hr = XblInterop.XblHttpCallPerformAsync(call.Handle, type, asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr);
                }
            }

            public static Int32 XblHttpCallSetTracing(XblHttpCallHandle call, bool traceCall)
            {
                if (call == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblHttpCallSetTracing(call.Handle, new NativeBool(traceCall));
            }

            public static Int32 XblHttpCallCreate(XblContextHandle xblContext, string method, string url, out XblHttpCallHandle call)
            {
                if (xblContext == null)
                {
                    call = default(XblHttpCallHandle);
                    return HR.E_INVALIDARG;
                }

                int hr = XblInterop.XblHttpCallCreate(
                    xblContext.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(method),
                    Converters.StringToNullTerminatedUTF8ByteArray(url),
                    out Interop.XblHttpCallHandle callHandle);

                return XblHttpCallHandle.WrapInteropHandleAndReturnHResult(hr, callHandle, out call);
            }

            public static void XblHttpCallCloseHandle(XblHttpCallHandle call)
            {
                if (call == null)
                {
                    return;
                }

                XblInterop.XblHttpCallCloseHandle(call.Handle);
            }

            public static Int32 XblHttpCallRequestSetRequestBodyString(XblHttpCallHandle call, string requestBodyString)
            {
                if (call == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblHttpCallRequestSetRequestBodyString(
                    call.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(requestBodyString));
            }

            public static Int32 XblHttpCallGetResponseString(XblHttpCallHandle call, out string responseString)
            {
                responseString = default(string);
                if (call == null)
                {
                    return HR.E_INVALIDARG;
                }

                int hr = XblInterop.XblHttpCallGetResponseString(call.Handle, out UTF8StringPtr responseStringPtr);
                if (HR.SUCCEEDED(hr))
                {
                    responseString = responseStringPtr.GetString();
                }

                return hr;
            }

            public static Int32 XblHttpCallGetHeaderAtIndex(XblHttpCallHandle call, UInt32 headerIndex, out string headerName, out string headerValue)
            {
                headerName = default(string);
                headerValue = default(string);
                if (call == null)
                {
                    return HR.E_INVALIDARG;
                }

                int hr = XblInterop.XblHttpCallGetHeaderAtIndex(
                    call.Handle,
                    headerIndex,
                    out UTF8StringPtr headerNamePtr,
                    out UTF8StringPtr headerValuePtr);
                if (HR.SUCCEEDED(hr))
                {
                    headerName = headerNamePtr.GetString();
                    headerValue = headerValuePtr.GetString();
                }

                return hr;
            }

            public static Int32 XblHttpCallGetPlatformNetworkErrorMessage(XblHttpCallHandle call, out string platformNetworkErrorMessage)
            {
                platformNetworkErrorMessage = default(string);
                if (call == null)
                {
                    return HR.E_INVALIDARG;
                }

                int hr = XblInterop.XblHttpCallGetPlatformNetworkErrorMessage(call.Handle, out UTF8StringPtr platformNetworkErrorMessagePtr);
                if (HR.SUCCEEDED(hr))
                {
                    platformNetworkErrorMessage = platformNetworkErrorMessagePtr.GetString();
                }

                return hr;
            }

            public static Int32 XblHttpCallGetResponseBodyBytes(XblHttpCallHandle call, out Byte[] buffer)
            {
                buffer = default(Byte[]);
                if (call == null)
                {
                    return HR.E_INVALIDARG;
                }

                int hr = XblInterop.XblHttpCallGetResponseBodyBytesSize(call.Handle, out SizeT bufferSize);
                if (HR.SUCCEEDED(hr))
                {
                    buffer = new byte[bufferSize.ToInt32()];
                    return XblInterop.XblHttpCallGetResponseBodyBytes(call.Handle, bufferSize, buffer, out SizeT bufferUsed);
                }
                else
                {
                    return hr;
                }
            }

            public static Int32 XblHttpCallRequestSetRetryAllowed(XblHttpCallHandle call, bool retryAllowed)
            {
                if (call == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblHttpCallRequestSetRetryAllowed(call.Handle, new NativeBool(retryAllowed));
            }

            public static Int32 XblHttpCallRequestSetHeader(XblHttpCallHandle call, string headerName, string headerValue, bool allowTracing)
            {
                if (call == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblHttpCallRequestSetHeader(
                    call.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(headerName),
                    Converters.StringToNullTerminatedUTF8ByteArray(headerValue),
                    new NativeBool(allowTracing));
            }

            public static Int32 XblHttpCallDuplicateHandle(XblHttpCallHandle call, out XblHttpCallHandle duplicateHandle)
            {
                if (call == null)
                {
                    duplicateHandle = default(XblHttpCallHandle);
                    return HR.E_INVALIDARG;
                }

                int hr = XblInterop.XblHttpCallDuplicateHandle(call.Handle, out Interop.XblHttpCallHandle duplicateHandleInterop);
                return XblHttpCallHandle.WrapInteropHandleAndReturnHResult(hr, duplicateHandleInterop, out duplicateHandle);
            }

            public static Int32 XblHttpCallGetNumHeaders(XblHttpCallHandle call, out UInt32 numHeaders)
            {
                if (call == null)
                {
                    numHeaders = default(UInt32);
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblHttpCallGetNumHeaders(call.Handle, out numHeaders);
            }

            public static Int32 XblHttpCallGetStatusCode(XblHttpCallHandle call, out UInt32 statusCode)
            {
                if (call == null)
                {
                    statusCode = default(UInt32);
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblHttpCallGetStatusCode(call.Handle, out statusCode);
            }

            public static Int32 XblHttpCallGetHeader(XblHttpCallHandle call, string headerName, out string headerValue)
            {
                headerValue = default(string);
                if (call == null)
                {
                    return HR.E_INVALIDARG;
                }

                int hr = XblInterop.XblHttpCallGetHeader(
                    call.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(headerName),
                    out UTF8StringPtr headerValuePtr);
                if (HR.SUCCEEDED(hr))
                {
                    headerValue = headerValuePtr.GetString();
                }

                return hr;
            }

            public static Int32 XblHttpCallGetRequestUrl(XblHttpCallHandle call, out string url)
            {
                url = default(string);
                if (call == null)
                {
                    return HR.E_INVALIDARG;
                }

                int hr = XblInterop.XblHttpCallGetRequestUrl(call.Handle, out UTF8StringPtr urlPtr);
                if (HR.SUCCEEDED(hr))
                {
                    url = urlPtr.GetString();
                }

                return hr;
            }

            public static Int32 XblHttpCallRequestSetRetryCacheId(XblHttpCallHandle call, UInt32 retryAfterCacheId)
            {
                if (call == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblHttpCallRequestSetRetryCacheId(call.Handle, retryAfterCacheId);
            }
        }
    }
}
