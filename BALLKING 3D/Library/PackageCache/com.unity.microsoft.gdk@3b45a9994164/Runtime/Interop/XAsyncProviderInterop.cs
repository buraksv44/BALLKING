using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Unity.XGamingRuntime.Interop
{
    // struct XAsyncProviderData
    // {

    //     XAsyncBlock* async;

    //     size_t bufferSize;

    //     void* buffer;

    //     void* context;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XAsyncProviderData
    {
        internal IntPtr async;
        internal UInt64 bufferSize;
        internal IntPtr buffer;
        internal IntPtr context;
    }

    // typedef HRESULT CALLBACK XAsyncProvider(_In_ XAsyncOp op, _Inout_ const XAsyncProviderData* data);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate Int32 XAsyncProviderInterop(XAsyncOp op, XAsyncProviderData data);

    partial class NativeMethods
    {
        // STDAPI XAsyncBegin(
        //     _Inout_ XAsyncBlock* asyncBlock,
        //     _In_opt_ void* context,
        //     _In_opt_ const void* identity,
        //     _In_opt_ const char* identityName,
        //     _In_ XAsyncProvider* provider) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAsyncBegin(IntPtr asyncInterop,
            IntPtr context,
            IntPtr identity,
            [MarshalAs(UnmanagedType.LPStr)] string identityName,
            XAsyncProviderInterop provider);

        // STDAPI XAsyncSchedule(
        //     _Inout_ XAsyncBlock* asyncBlock,
        //     _In_ uint32_t delayInMs) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAsyncSchedule(IntPtr asyncInterop,
            UInt32 delayInMs);

        // STDAPI_(void) XAsyncComplete(
        //     _Inout_ XAsyncBlock* asyncBlock,
        //     _In_ HRESULT result,
        //     _In_ size_t requiredBufferSize) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XAsyncComplete(IntPtr asyncInterop,
            UInt32 result,
            UInt64 requiredBufferSize);

        // STDAPI XAsyncGetResult(
        //     _Inout_ XAsyncBlock* asyncBlock,
        //     _In_opt_ const void* identity,
        //     _In_ size_t bufferSize,
        //     _Out_writes_bytes_to_opt_(bufferSize, *bufferUsed) void* buffer,
        //     _Out_opt_ size_t* bufferUsed) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XAsyncGetResult(IntPtr asyncInterop,
            IntPtr identity,
            UInt64 bufferSize,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer,
            out UInt64 bufferUsed);
    }
}
