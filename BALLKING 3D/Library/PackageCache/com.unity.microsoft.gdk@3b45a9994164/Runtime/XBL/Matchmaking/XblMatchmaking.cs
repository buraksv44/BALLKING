using System;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public partial class SDK
    {
        public partial class XBL
        {
            public delegate void XblMatchmakingCreateMatchTicketHandleResult(Int32 hresult, XblCreateMatchTicketResponse handle);
            public delegate void XblMatchmakingDeleteMatchTicketHandleResult(Int32 hresult);
            public delegate void XblMatchmakingGetMatchTicketDetailsHandleResult(Int32 hresult, XblMatchTicketDetailsResponse result);
            public delegate void XblMatchmakingGetHopperStatisticsHandleResult(Int32 hresult, XblHopperStatisticsResponse result);

            public static void XblMatchmakingCreateMatchTicketAsync(
                XblContextHandle xboxLiveContext,
                XblMultiplayerSessionReference ticketSessionReference,
                string matchmakingServiceConfigurationId,
                string hopperName,
                UInt64 ticketTimeout,
                XblPreserveSessionMode preserveSession,
                string ticketAttributesJson,
                XblMatchmakingCreateMatchTicketHandleResult completionRoutine
                )
            {
                if (xboxLiveContext == null || ticketSessionReference == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblCreateMatchTicketResponse));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Interop.XblCreateMatchTicketResponse result;
                    Int32 hresult = XblInterop.XblMatchmakingCreateMatchTicketResult(block, out result);
                    if (HR.FAILED(hresult))
                    {
                        completionRoutine(hresult, default(XblCreateMatchTicketResponse));
                        return;
                    }

                    completionRoutine(hresult, new XblCreateMatchTicketResponse(result));
                });

                Int32 hr = XblInterop.XblMatchmakingCreateMatchTicketAsync(
                    xboxLiveContext.Handle,
                    new Interop.XblMultiplayerSessionReference(ticketSessionReference),
                    Converters.StringToNullTerminatedUTF8ByteArray(matchmakingServiceConfigurationId),
                    Converters.StringToNullTerminatedUTF8ByteArray(hopperName),
                    ticketTimeout,
                    preserveSession,
                    Converters.StringToNullTerminatedUTF8ByteArray(ticketAttributesJson),
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr, default(XblCreateMatchTicketResponse));
                }
            }

            public static void XblMatchmakingDeleteMatchTicketAsync(
                XblContextHandle xboxLiveContext,
                string serviceConfigurationId,
                string hopperName,
                string ticketId,
                XblMatchmakingDeleteMatchTicketHandleResult completionRoutine
                )
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG);
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(SDK.defaultQueue, (XAsyncCompletionRoutine)(block => completionRoutine(NativeMethods.XAsyncGetStatus(block.InteropPtr, false))));
                int hr = XblInterop.XblMatchmakingDeleteMatchTicketAsync(
                    xboxLiveContext.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(serviceConfigurationId),
                    Converters.StringToNullTerminatedUTF8ByteArray(hopperName),
                    Converters.StringToNullTerminatedUTF8ByteArray(ticketId),
                    asyncBlock);

                if (HR.FAILED(hr))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hr);
                }
            }

            public static void XblMatchmakingGetMatchTicketDetailsAsync(
                XblContextHandle xboxLiveContext,
                string serviceConfigurationId,
                string hopperName,
                string ticketId,
                XblMatchmakingGetMatchTicketDetailsHandleResult completionRoutine
                )
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblMatchTicketDetailsResponse));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    int hr = XblInterop.XblMatchmakingGetMatchTicketDetailsResultSize(
                        block,
                        out SizeT resultSizeInBytes
                        );

                    if (HR.FAILED(hr) || resultSizeInBytes.IsZero)
                    {
                        completionRoutine(hr, default(XblMatchTicketDetailsResponse));
                        return;
                    }

                    using (DisposableBuffer buffer = new DisposableBuffer(resultSizeInBytes.ToInt32()))
                    {
                        hr = XblInterop.XblMatchmakingGetMatchTicketDetailsResult(
                            block,
                            resultSizeInBytes,
                            buffer.IntPtr,
                            out IntPtr ptrToResults,
                            out SizeT bufferUsed
                        );

                        if (HR.FAILED(hr))
                        {
                            completionRoutine(hr, default(XblMatchTicketDetailsResponse));
                            return;
                        }

                        var response = Converters.PtrToClassArray<XblMatchTicketDetailsResponse, Interop.XblMatchTicketDetailsResponse>(ptrToResults, 1, r => new XblMatchTicketDetailsResponse(r));

                        completionRoutine(hr, response[0]);
                    }
                });

                int hresult = XblInterop.XblMatchmakingGetMatchTicketDetailsAsync(
                    xboxLiveContext.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(serviceConfigurationId),
                    Converters.StringToNullTerminatedUTF8ByteArray(hopperName),
                    Converters.StringToNullTerminatedUTF8ByteArray(ticketId),
                    asyncBlock
                );

                if (HR.FAILED(hresult))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hresult, default(XblMatchTicketDetailsResponse));
                }
            }

            public static void XblMatchmakingGetHopperStatisticsAsync(
                XblContextHandle xboxLiveContext,
                string serviceConfigurationId,
                string hopperName,
                XblMatchmakingGetHopperStatisticsHandleResult completionRoutine
                )
            {
                if (xboxLiveContext == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblHopperStatisticsResponse));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    int hr = XblInterop.XblMatchmakingGetMatchTicketDetailsResultSize(
                        block,
                        out SizeT resultSizeInBytes
                        );

                    if (HR.FAILED(hr) || resultSizeInBytes.IsZero)
                    {
                        completionRoutine(hr, default(XblHopperStatisticsResponse));
                        return;
                    }

                    using (DisposableBuffer buffer = new DisposableBuffer(resultSizeInBytes.ToInt32()))
                    {
                        hr = XblInterop.XblMatchmakingGetMatchTicketDetailsResult(
                            block,
                            resultSizeInBytes,
                            buffer.IntPtr,
                            out IntPtr ptrToResults,
                            out SizeT bufferUsed
                        );

                        if (HR.FAILED(hr))
                        {
                            completionRoutine(hr, default(XblHopperStatisticsResponse));
                            return;
                        }

                        var response = Converters.PtrToClassArray<XblHopperStatisticsResponse, Interop.XblHopperStatisticsResponse>(ptrToResults, 1, r => new XblHopperStatisticsResponse(r));

                        completionRoutine(hr, response[0]);
                    }
                });

                int hresult = XblInterop.XblMatchmakingGetHopperStatisticsAsync(
                    xboxLiveContext.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(serviceConfigurationId),
                    Converters.StringToNullTerminatedUTF8ByteArray(hopperName),
                    asyncBlock
                );

                if (HR.FAILED(hresult))
                {
                    AsyncHelpers.CleanupAsyncBlock(asyncBlock);
                    completionRoutine(hresult, default(XblHopperStatisticsResponse));
                }
            }
        }
    }
}
