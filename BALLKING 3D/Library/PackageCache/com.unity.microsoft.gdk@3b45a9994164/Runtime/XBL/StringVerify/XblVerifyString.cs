using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public delegate void XblStringVerifyStringCompleted(Int32 hresult, XblVerifyStringResult result);
    public delegate void XblStringVerifyStringsCompleted(Int32 hresult, XblVerifyStringResult[] result);

    public partial class SDK
    {
        public partial class XBL
        {
            static public void XblStringVerifyStringAsync(
                XblContextHandle xblContextHandle,
                string stringToVerify,
                XblStringVerifyStringCompleted completionRoutine)
            {
                if (xblContextHandle == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblVerifyStringResult));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Int32 hr = XblInterop.XblStringVerifyStringResultSize(block, out SizeT resultSizeInBytes);
                    if (HR.FAILED(hr))
                    {
                        completionRoutine(hr, default(XblVerifyStringResult));
                        return;
                    }

                    using (DisposableBuffer buffer = new DisposableBuffer(resultSizeInBytes.ToInt32()))
                    {
                        hr = XblInterop.XblStringVerifyStringResult(
                            block,
                            resultSizeInBytes,
                            buffer.IntPtr,
                            out IntPtr result,
                            out SizeT bufferUsed);

                        if (HR.FAILED(hr))
                        {
                            completionRoutine(hr, default(XblVerifyStringResult));
                            return;
                        }

                        completionRoutine(hr, Converters.PtrToClass<XblVerifyStringResult, Interop.XblVerifyStringResult>(result, r => new XblVerifyStringResult(r)));
                    }
                });

                Int32 hresult = XblInterop.XblStringVerifyStringAsync(
                    xblContextHandle.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(stringToVerify),
                    asyncBlock);

                if (HR.FAILED(hresult))
                {
                    completionRoutine(hresult, default(XblVerifyStringResult));
                    return;
                }
            }

            static public void XblStringVerifyStringsAsync(
                XblContextHandle xblContextHandle,
                string[] stringsToVerify,
                XblStringVerifyStringsCompleted completionRoutine)
            {
                if (xblContextHandle == null || stringsToVerify == null || stringsToVerify.Length == 0)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblVerifyStringResult[]));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Int32 hr = XblInterop.XblStringVerifyStringsResultSize(block, out SizeT resultSizeInBytes);
                    if (HR.FAILED(hr))
                    {
                        completionRoutine(hr, default(XblVerifyStringResult[]));
                        return;
                    }

                    using (DisposableBuffer buffer = new DisposableBuffer(resultSizeInBytes.ToInt32()))
                    {
                        hr = XblInterop.XblStringVerifyStringsResult(
                            block,
                            resultSizeInBytes,
                            buffer.IntPtr,
                            out IntPtr ptrToBuffer,
                            out SizeT resultsCount,
                            out SizeT bufferUsed);

                        if (HR.FAILED(hr))
                        {
                            completionRoutine(hr, default(XblVerifyStringResult[]));
                            return;
                        }

                        completionRoutine(hr, Converters.PtrToClassArray<XblVerifyStringResult, Interop.XblVerifyStringResult>(ptrToBuffer, resultsCount, r => new XblVerifyStringResult(r)));
                    }
                });

                using (DisposableBuffer stringsToVerifyBuffer = Converters.StringArrayToUTF8StringArray(stringsToVerify))
                {
                    Int32 hresult = XblInterop.XblStringVerifyStringsAsync(
                        xblContextHandle.Handle,
                        stringsToVerifyBuffer.IntPtr,
                        Convert.ToUInt64(stringsToVerify.Length),
                        asyncBlock);

                    if (HR.FAILED(hresult))
                    {
                        completionRoutine(hresult, default(XblVerifyStringResult[]));
                        return;
                    }
                }
            }
        }
    }
}
