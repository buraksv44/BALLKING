using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    internal partial class XblInterop
    {
        //STDAPI XblHttpCallRequestSetRequestBodyBytes(
        //    _In_ XblHttpCallHandle call,
        //    _In_reads_bytes_(requestBodySize) const uint8_t* requestBodyBytes,
        //    _In_ uint32_t requestBodySize
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallRequestSetRequestBodyBytes(
            IntPtr call,
            Byte[] requestBodyBytes,
            UInt32 requestBodySize);

        //STDAPI XblHttpCallGetNetworkErrorCode(
        //    _In_ XblHttpCallHandle call,
        //    _Out_ HRESULT* networkErrorCode,
        //    _Out_ uint32_t* platformNetworkErrorCode
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallGetNetworkErrorCode(
            IntPtr call,
            out Int32 networkErrorCode,
            out UInt32 platformNetworkErrorCode);

        //STDAPI XblHttpCallRequestSetLongHttpCall(
        //    _In_ XblHttpCallHandle call,
        //    _In_ bool longHttpCall
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallRequestSetLongHttpCall(
            IntPtr call,
            NativeBool longHttpCall);

        //STDAPI XblHttpCallPerformAsync(
        //    _In_ XblHttpCallHandle call,
        //    _In_ XblHttpCallResponseBodyType type,
        //    _Inout_ XAsyncBlock* asyncBlock
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallPerformAsync(
            IntPtr call,
            XblHttpCallResponseBodyType type,
            XAsyncBlockPtr asyncBlock);

        //STDAPI XblHttpCallSetTracing(
        //    _In_ XblHttpCallHandle call,
        //    _In_ bool traceCall
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallSetTracing(
            IntPtr call,
            NativeBool traceCall);

        //STDAPI XblHttpCallCreate(
        //    _In_ XblContextHandle xblContext,
        //    _In_z_ const char* method,
        //    _In_z_ const char* url,
        //    _Out_ XblHttpCallHandle* call
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallCreate(
            IntPtr xblContext,
            Byte[] method,
            Byte[] url,
            out XblHttpCallHandle call);

        //STDAPI_(void) XblHttpCallCloseHandle(
        //    _In_ XblHttpCallHandle call
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XblHttpCallCloseHandle(
            IntPtr call);

        //STDAPI XblHttpCallRequestSetRequestBodyString(
        //    _In_ XblHttpCallHandle call,
        //    _In_z_ const char* requestBodyString
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallRequestSetRequestBodyString(
            IntPtr call,
            Byte[] requestBodyString);

        //STDAPI XblHttpCallGetResponseString(
        //    _In_ XblHttpCallHandle call,
        //    _Out_ const char** responseString
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallGetResponseString(
            IntPtr call,
            out UTF8StringPtr responseString);

        //STDAPI XblHttpCallGetHeaderAtIndex(
        //    _In_ XblHttpCallHandle call,
        //    _In_ uint32_t headerIndex,
        //    _Out_ const char** headerName,
        //    _Out_ const char** headerValue
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallGetHeaderAtIndex(
            IntPtr call,
            UInt32 headerIndex,
            out UTF8StringPtr headerName,
            out UTF8StringPtr headerValue);

        //STDAPI XblHttpCallGetResponseBodyBytesSize(
        //    _In_ XblHttpCallHandle call,
        //    _Out_ size_t* bufferSize
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallGetResponseBodyBytesSize(
            IntPtr call,
            out SizeT bufferSize);

        //STDAPI XblHttpCallGetPlatformNetworkErrorMessage(
        //    _In_ XblHttpCallHandle call,
        //    _Out_ const char** platformNetworkErrorMessage
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallGetPlatformNetworkErrorMessage(
            IntPtr call,
            out UTF8StringPtr platformNetworkErrorMessage);

        //STDAPI XblHttpCallGetResponseBodyBytes(
        //    _In_ XblHttpCallHandle call,
        //    _In_ size_t bufferSize,
        //    _Out_writes_bytes_to_(bufferSize, *bufferUsed) uint8_t* buffer,
        //    _Out_opt_ size_t* bufferUsed
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallGetResponseBodyBytes(
            IntPtr call,
            SizeT bufferSize,
            [Out] Byte[] buffer,
            out SizeT bufferUsed);

        //STDAPI XblHttpCallRequestSetRetryAllowed(
        //    _In_ XblHttpCallHandle call,
        //    _In_ bool retryAllowed
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallRequestSetRetryAllowed(
            IntPtr call,
            NativeBool retryAllowed);

        //STDAPI XblHttpCallRequestSetHeader(
        //    _In_ XblHttpCallHandle call,
        //    _In_z_ const char* headerName,
        //    _In_z_ const char* headerValue,
        //    _In_ bool allowTracing
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallRequestSetHeader(
            IntPtr call,
            Byte[] headerName,
            Byte[] headerValue,
            NativeBool allowTracing);

        //STDAPI XblHttpCallDuplicateHandle(
        //    _In_ XblHttpCallHandle call,
        //    _Out_ XblHttpCallHandle* duplicateHandle
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallDuplicateHandle(
            IntPtr call,
            out XblHttpCallHandle duplicateHandle);

        //STDAPI XblHttpCallGetNumHeaders(
        //    _In_ XblHttpCallHandle call,
        //    _Out_ uint32_t* numHeaders
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallGetNumHeaders(
            IntPtr call,
            out UInt32 numHeaders);

        //STDAPI XblHttpCallGetStatusCode(
        //    _In_ XblHttpCallHandle call,
        //    _Out_ uint32_t* statusCode
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallGetStatusCode(
            IntPtr call,
            out UInt32 statusCode);

        //STDAPI XblHttpCallGetHeader(
        //    _In_ XblHttpCallHandle call,
        //    _In_z_ const char* headerName,
        //    _Out_ const char** headerValue
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallGetHeader(
            IntPtr call,
            Byte[] headerName,
            out UTF8StringPtr headerValue);

        //STDAPI XblHttpCallGetRequestUrl(
        //    _In_ XblHttpCallHandle call,
        //    _Out_ const char** url
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallGetRequestUrl(
            IntPtr call,
            out UTF8StringPtr url);

        //STDAPI XblHttpCallRequestSetRetryCacheId(
        //    _In_ XblHttpCallHandle call,
        //    _In_ uint32_t retryAfterCacheId
        //    ) XBL_NOEXCEPT;
        [DllImport(XblThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XblHttpCallRequestSetRetryCacheId(
            IntPtr call,
            UInt32 retryAfterCacheId);
    }
}