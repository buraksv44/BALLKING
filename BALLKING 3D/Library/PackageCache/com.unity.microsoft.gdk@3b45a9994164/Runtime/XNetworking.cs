// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XNetworkingConnectivityLevelHint : uint32_t
    //{
    //    Unknown = 0,
    //    None = 1,
    //    LocalAccess = 2,
    //    InternetAccess = 3,
    //    ConstrainedInternetAccess = 4,
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XNetworkingConnectivityLevelHint : UInt32
    {
        Unknown = 0,
        None = 1,
        LocalAccess = 2,
        InternetAccess = 3,
        ConstrainedInternetAccess = 4,
    }

    //enum class XNetworkingConnectivityCostHint : uint32_t
    //{
    //    Unknown = 0,
    //    Unrestricted = 1,
    //    Fixed = 2,
    //    Variable = 3,
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XNetworkingConnectivityCostHint : UInt32
    {
        Unknown = 0,
        Unrestricted = 1,
        Fixed = 2,
        Variable = 3,
    };

    //enum class XNetworkingConfigurationSetting : uint32_t
    //{
    //    MaxTitleTcpQueuedReceiveBufferSize = 0,
    //    MaxSystemTcpQueuedReceiveBufferSize = 1,
    //    MaxToolsTcpQueuedReceiveBufferSize = 2,
    //};
    public enum XNetworkingConfigurationSetting : UInt32
    {
        MaxTitleTcpQueuedReceiveBufferSize = 0,
        MaxSystemTcpQueuedReceiveBufferSize = 1,
        MaxToolsTcpQueuedReceiveBufferSize = 2,
    };

    //enum class XNetworkingThumbprintType : uint32_t
    //{
    //    Leaf = 0,
    //    Issuer = 1,
    //    Root = 2,
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XNetworkingThumbprintType : UInt32
    {
        Leaf = 0,
        Issuer = 1,
        Root = 2,
    };

    //enum class XNetworkingStatisticsType : uint32_t
    //{
    //    TitleTcpQueuedReceivedBufferUsage = 0,
    //    SystemTcpQueuedReceivedBufferUsage = 1,
    //    ToolsTcpQueuedReceivedBufferUsage = 2,
    //};
    public enum XNetworkingStatisticsType : UInt32
    {
        TitleTcpQueuedReceivedBufferUsage = 0,
        SystemTcpQueuedReceivedBufferUsage = 1,
        ToolsTcpQueuedReceivedBufferUsage = 2,
    };

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XNetworkingConnectivityHint
    {
        internal XNetworkingConnectivityHint(Interop.XNetworkingConnectivityHint interop)
        {
            data = interop;
        }

        public XNetworkingConnectivityHint()
        {
            data = new Interop.XNetworkingConnectivityHint();
        }

        internal Interop.XNetworkingConnectivityHint data;

        public XNetworkingConnectivityLevelHint ConnectivityLevel
        {
            get => this.data.connectivityLevel;
            set => this.data.connectivityLevel = value;
        }

        public XNetworkingConnectivityCostHint ConnectivityCost
        {
            get => this.data.connectivityCost;
            set => this.data.connectivityCost = value;
        }

        public UInt32 IanaInterfaceType
        {
            get => this.data.ianaInterfaceType;
            set => this.data.ianaInterfaceType = value;
        }

        public bool NetworkInitialized
        {
            get => this.data.networkInitialized;
            set => this.data.networkInitialized = value;
        }

        public bool ApproachingDataLimit
        {
            get => this.data.approachingDataLimit;
            set => this.data.approachingDataLimit = value;
        }

        public bool OverDataLimit
        {
            get => this.data.overDataLimit;
            set => this.data.overDataLimit = value;
        }

        public bool Roaming
        {
            get => this.data.roaming;
            set => this.data.roaming = value;
        }
    };

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XNetworkingThumbprint
    {
        public XNetworkingThumbprintType ThumbprintType { get; set; }

        public byte[] ThumbprintBuffer { get; set; }
    };

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XNetworkingSecurityInformation
    {
        public UInt32 EnabledHttpSecurityProtocolFlags { get; set; }
        public XNetworkingThumbprint[] Thumbprints { get; set; }
    };

    public class XNetworkingTcpQueuedReceivedBufferUsageStatistics
    {
        internal XNetworkingTcpQueuedReceivedBufferUsageStatistics(Interop.XNetworkingTcpQueuedReceivedBufferUsageStatistics interop)
        {
            this.interop = interop;
        }

        public XNetworkingTcpQueuedReceivedBufferUsageStatistics()
        {
            this.interop = new Interop.XNetworkingTcpQueuedReceivedBufferUsageStatistics();
        }

        internal Interop.XNetworkingTcpQueuedReceivedBufferUsageStatistics interop;

        public UInt64 NumBytesCurrentlyQueued
        {
            get => this.interop.numBytesCurrentlyQueued;
            set => this.interop.numBytesCurrentlyQueued = value;
        }

        public UInt64 PeakNumBytesEverQueued
        {
            get => this.interop.peakNumBytesEverQueued;
            set => this.interop.peakNumBytesEverQueued = value;
        }

        public UInt64 TotalNumBytesQueued
        {
            get => this.interop.totalNumBytesQueued;
            set => this.interop.totalNumBytesQueued = value;
        }

        public UInt64 NumBytesDroppedForExceedingConfiguredMax
        {
            get => this.interop.numBytesDroppedForExceedingConfiguredMax;
            set => this.interop.numBytesDroppedForExceedingConfiguredMax = value;
        }

        public UInt64 NumBytesDroppedDueToAnyFailure
        {
            get => this.interop.numBytesDroppedDueToAnyFailure;
            set => this.interop.numBytesDroppedDueToAnyFailure = value;
        }
    };

    public class XNetworkingStatisticsBuffer
    {
        internal XNetworkingStatisticsBuffer(Interop.XNetworkingStatisticsBuffer interop)
        {
            this._interop = interop;
            _tcpQueuedReceivedBufferUsageStatistics = new XNetworkingTcpQueuedReceivedBufferUsageStatistics(interop.tcpQueuedReceiveBufferUsage);
        }

        public XNetworkingStatisticsBuffer()
        {
            this._interop = new Interop.XNetworkingStatisticsBuffer();
        }

        internal Interop.XNetworkingStatisticsBuffer _interop;

        internal Interop.XNetworkingStatisticsBuffer interop
        {
            get
            {
                this._interop.tcpQueuedReceiveBufferUsage = this._tcpQueuedReceivedBufferUsageStatistics.interop;
                return this._interop;
            }
        }

        internal XNetworkingTcpQueuedReceivedBufferUsageStatistics _tcpQueuedReceivedBufferUsageStatistics;

        public XNetworkingTcpQueuedReceivedBufferUsageStatistics TcpQueuedReceiveBufferUsage
        {
            get => this._tcpQueuedReceivedBufferUsageStatistics;
            set => this._tcpQueuedReceivedBufferUsageStatistics = value;
        }

    }

    public delegate void XNetworkingConnectivityHintChangedCallback(IntPtr context, XNetworkingConnectivityHint connectivityHint);

    public delegate void XNetworkingPreferredLocalUdpMultiplayerPortChangedCallback(IntPtr context, UInt16 preferredLocalUdpMultiplayerPort);

    public partial class SDK
    {
        public static Int32 XNetworkingGetConnectivityHint(out XNetworkingConnectivityHint getConnectivityHint)
        {
            getConnectivityHint = default;
            Interop.XNetworkingConnectivityHint getConnectivityHintInterop;

            Int32 hr = NativeMethods.XNetworkingGetConnectivityHint(out getConnectivityHintInterop);

            if(HR.SUCCEEDED(hr))
            {
                getConnectivityHint = new XNetworkingConnectivityHint(getConnectivityHintInterop);
            }

            return hr;
        }

        public static Int32 XNetworkingQueryConfigurationSetting(XNetworkingConfigurationSetting getConfigurationSetting, out UInt64 value)
        {
            return NativeMethods.XNetworkingQueryConfigurationSetting(getConfigurationSetting, out value);
        }

        public static Int32 XNetworkingQueryPreferredLocalUdpMultiplayerPort(out UInt16 value)
        {
            return NativeMethods.XNetworkingQueryPreferredLocalUdpMultiplayerPort(out value);
        }

        public static Int32 XNetworkingQueryPreferredLocalUdpMultiplayerPortAsync(XAsyncBlock async)
        {
            return NativeMethods.XNetworkingQueryPreferredLocalUdpMultiplayerPortAsync(async.InteropPtr);
        }

        public static Int32 XNetworkingQueryPreferredLocalUdpMultiplayerPortAsyncResult(XAsyncBlock async, out UInt16 preferredLocalUdpMultiplayerPort)
        {
            return NativeMethods.XNetworkingQueryPreferredLocalUdpMultiplayerPortAsyncResult(async.InteropPtr, out preferredLocalUdpMultiplayerPort);
        }

        public static Int32 XNetworkingQuerySecurityInformationForUrlAsync(string url, XAsyncBlock async)
        {
            return NativeMethods.XNetworkingQuerySecurityInformationForUrlAsync(url, async.InteropPtr);
        }

        public static Int32 XNetworkingQuerySecurityInformationForUrlAsyncResultSize(XAsyncBlock async, out UInt64 securityInformationBufferByteCount)
        {
            return NativeMethods.XNetworkingQuerySecurityInformationForUrlAsyncResultSize(async.InteropPtr, out securityInformationBufferByteCount);
        }

        public static Int32 XNetworkingQuerySecurityInformationForUrlAsyncResult(XAsyncBlock async,
            byte[] securityInformationBuffer,
            out UInt64 securityInformationBufferByteCountUsed,
            out XNetworkingSecurityInformation securityInformation)
        {
            securityInformation = new XNetworkingSecurityInformation();
            IntPtr securityInformationInteropPtr;

            Int32 hresult = NativeMethods.XNetworkingQuerySecurityInformationForUrlAsyncResult(async.InteropPtr,
                (ulong)securityInformationBuffer.Length,
                out securityInformationBufferByteCountUsed,
                securityInformationBuffer,
                out securityInformationInteropPtr);

            if (HR.SUCCEEDED(hresult))
            {
                MarshalXNetworkingSecurityInformationInteropToManaged(securityInformationInteropPtr, ref securityInformation);
            }

            return hresult;
        }

        public static Int32 XNetworkingQuerySecurityInformationForUrlUtf16Async(string url, XAsyncBlock async)
        {
            return NativeMethods.XNetworkingQuerySecurityInformationForUrlUtf16Async(url, async.InteropPtr);
        }

        public static Int32 XNetworkingQuerySecurityInformationForUrlUtf16AsyncResultSize(XAsyncBlock async, out UInt64 securityInformationBufferByteCount)
        {
            return NativeMethods.XNetworkingQuerySecurityInformationForUrlUtf16AsyncResultSize(async.InteropPtr, out securityInformationBufferByteCount);
        }

        public static Int32 XNetworkingQuerySecurityInformationForUrlUtf16AsyncResult(XAsyncBlock async,
            byte[] securityInformationBuffer,
            out UInt64 securityInformationBufferByteCountUsed,
            out XNetworkingSecurityInformation securityInformation)
        {
            securityInformation = new XNetworkingSecurityInformation();
            IntPtr securityInformationInteropPtr;

            Int32 hresult = NativeMethods.XNetworkingQuerySecurityInformationForUrlUtf16AsyncResult(async.InteropPtr,
                (ulong)securityInformationBuffer.Length,
                out securityInformationBufferByteCountUsed,
                securityInformationBuffer,
                out securityInformationInteropPtr);

            if (HR.SUCCEEDED(hresult))
            {
                MarshalXNetworkingSecurityInformationInteropToManaged(securityInformationInteropPtr, ref securityInformation);
            }

            return hresult;
        }

        public static Int32 XNetworkingQueryStatistics(XNetworkingStatisticsType statisticsType,
            out XNetworkingStatisticsBuffer statisticsBuffer)
        {
            Interop.XNetworkingStatisticsBuffer statisticsBufferInterop = default;
            statisticsBuffer = default;

            Int32 hr = NativeMethods.XNetworkingQueryStatistics(statisticsType, out statisticsBufferInterop);

            if(HR.SUCCEEDED(hr))
            {
                statisticsBuffer = new XNetworkingStatisticsBuffer(statisticsBufferInterop);
            }

            return hr;
        }

        public static int XNetworkingRegisterConnectivityHintChanged(XTaskQueueHandle queue,
             IntPtr context,
             XNetworkingConnectivityHintChangedCallback callback,
             out XNetworkingRegisterConnectivityHintChangedCallbackToken token)
        {
            Interop.XNetworkingConnectivityHintChangedCallback interopCallback = (IntPtr context, Interop.XNetworkingConnectivityHint connectivityHint) =>
            {
                callback(context, new XNetworkingConnectivityHint(connectivityHint));
            };

            UInt64 tokenValue = 0;
            IntPtr interopQueue = queue != null ? queue.Handle : IntPtr.Zero;
            token = new XNetworkingRegisterConnectivityHintChangedCallbackToken(interopCallback, context);

            int hresult = NativeMethods.XNetworkingRegisterConnectivityHintChanged(interopQueue,
                token.interop.CallbackContext,
                token.interop.StaticCallback,
                out tokenValue);

            if (HR.SUCCEEDED(hresult))
            {
                token.Token = tokenValue;
            }
            else
            {
                token.interop.Dispose();
                token = null;
            }

            return hresult;
        }

        public static bool XNetworkingUnregisterConnectivityHintChanged(XNetworkingRegisterConnectivityHintChangedCallbackToken token,
            bool wait)
        {
            return token.Unregister(wait);
        }

        public static int XNetworkingRegisterPreferredLocalUdpMultiplayerPortChanged(XTaskQueueHandle queue,
             IntPtr context,
             XNetworkingPreferredLocalUdpMultiplayerPortChangedCallback callback,
             out XNetworkingPreferredLocalUdpMultiplayerPortChangedCallbackToken token)
        {
            Interop.XNetworkingPreferredLocalUdpMultiplayerPortChangedCallback interopCallback = (IntPtr context, UInt16 preferredLocalUdpMultiplayerPort) =>
            {
                callback(context, preferredLocalUdpMultiplayerPort);
            };

            UInt64 tokenValue = 0;
            token = new XNetworkingPreferredLocalUdpMultiplayerPortChangedCallbackToken(interopCallback, context);

            IntPtr interopQueue = queue != null ? queue.Handle : IntPtr.Zero;

            int hresult = NativeMethods.XNetworkingRegisterPreferredLocalUdpMultiplayerPortChanged(interopQueue,
                token.interop.CallbackContext,
                token.interop.StaticCallback,
                out tokenValue);

            if (HR.SUCCEEDED(hresult))
            {
                token.Token = tokenValue;
            }
            else
            {
                token.interop.Dispose();
                token = null;
            }

            return hresult;
        }

        public static bool XNetworkingUnregisterPreferredLocalUdpMultiplayerPortChanged(XNetworkingPreferredLocalUdpMultiplayerPortChangedCallbackToken token,
            bool wait)
        {
            return token.Unregister(wait);
        }

        public static Int32 XNetworkingVerifyServerCertificate(IntPtr requestHandle, XNetworkingSecurityInformation securityInformation)
        {
            IntPtr securityInformationInteropPtr;
            MarshalXNetworkingSecurityInformationManagedToInterop(securityInformation, out securityInformationInteropPtr);

            int hresult = NativeMethods.XNetworkingVerifyServerCertificate(requestHandle, securityInformationInteropPtr);
            if (securityInformationInteropPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(securityInformationInteropPtr);
                securityInformationInteropPtr = IntPtr.Zero;
            }

            return hresult;
        }

        //
        // Helper methods.
        //

        public static void MarshalXNetworkingSecurityInformationInteropToManaged(IntPtr securityInformationInteropPtr,
            ref XNetworkingSecurityInformation securityInformation)
        {
            Interop.XNetworkingSecurityInformation securityInformationInterop =
                (Interop.XNetworkingSecurityInformation)
                    Marshal.PtrToStructure(securityInformationInteropPtr, typeof(Interop.XNetworkingSecurityInformation));

            int countSigned = Convert.ToInt32(securityInformationInterop.thumbprintCount);
            securityInformation.EnabledHttpSecurityProtocolFlags =
                securityInformationInterop.enabledHttpSecurityProtocolFlags;
            securityInformation.Thumbprints = new XNetworkingThumbprint[countSigned];

            Int64 basePtr = securityInformationInterop.thumbprints.ToInt64();
            int tpiSize = Marshal.SizeOf(typeof(Interop.XNetworkingThumbprint));
            for (int i = 0; i < countSigned; i++)
            {
                Interop.XNetworkingThumbprint thumbprintInterop =
                    (Interop.XNetworkingThumbprint)
                        Marshal.PtrToStructure(new IntPtr(basePtr + i * tpiSize),
                            typeof(Interop.XNetworkingThumbprint));
                securityInformation.Thumbprints[i] = new XNetworkingThumbprint();
                securityInformation.Thumbprints[i].ThumbprintType = thumbprintInterop.thumbprintType;
                securityInformation.Thumbprints[i].ThumbprintBuffer = new byte[thumbprintInterop.thumbprintBufferByteCount];

                Marshal.Copy(thumbprintInterop.thumbprintBuffer,
                    securityInformation.Thumbprints[i].ThumbprintBuffer,
                    0,
                    securityInformation.Thumbprints[i].ThumbprintBuffer.Length);
            }
        }

        public static void MarshalXNetworkingSecurityInformationManagedToInterop(
            XNetworkingSecurityInformation securityInformation,
            out IntPtr securityInformationInteropPtr)
        {
            int blockSize = Marshal.SizeOf(typeof(Interop.XNetworkingSecurityInformation));
            IntPtr arrayStart = new IntPtr(blockSize);
            blockSize += Marshal.SizeOf(typeof(Interop.XNetworkingThumbprint)) *
                Convert.ToInt32(securityInformation.Thumbprints.Length);
            IntPtr bufferOffset = new IntPtr(blockSize);
            foreach(XNetworkingThumbprint thumb in securityInformation.Thumbprints)
            {
                blockSize += Convert.ToInt32(thumb.ThumbprintBuffer.Length);
            }

            securityInformationInteropPtr = Marshal.AllocHGlobal(Convert.ToInt32(blockSize));

            IntPtr arrayOffset = securityInformationInteropPtr + arrayStart.ToInt32();
            arrayStart = securityInformationInteropPtr + arrayStart.ToInt32();
            bufferOffset = securityInformationInteropPtr + bufferOffset.ToInt32();
            Interop.XNetworkingThumbprint transferThumb = new Interop.XNetworkingThumbprint();
            for (int i = 0; i < securityInformation.Thumbprints.Length; i++)
            {
                transferThumb.thumbprintType = securityInformation.Thumbprints[i].ThumbprintType;
                transferThumb.thumbprintBufferByteCount = Convert.ToUInt64(securityInformation.Thumbprints[i].ThumbprintBuffer.Length);
                transferThumb.thumbprintBuffer = new IntPtr(bufferOffset.ToInt64());

                Marshal.Copy(securityInformation.Thumbprints[i].ThumbprintBuffer,
                    0,
                    transferThumb.thumbprintBuffer,
                    securityInformation.Thumbprints[i].ThumbprintBuffer.Length);

                Marshal.StructureToPtr(transferThumb, arrayOffset, false);

                arrayOffset += Marshal.SizeOf(typeof(Interop.XNetworkingThumbprint));
                bufferOffset += securityInformation.Thumbprints[i].ThumbprintBuffer.Length;
            }

            Interop.XNetworkingSecurityInformation securityInformationInterop =
                new Interop.XNetworkingSecurityInformation();
            securityInformationInterop.enabledHttpSecurityProtocolFlags = securityInformation.EnabledHttpSecurityProtocolFlags;
            securityInformationInterop.thumbprintCount = Convert.ToUInt64(securityInformation.Thumbprints.Length);
            securityInformationInterop.thumbprints = new IntPtr(arrayStart.ToInt64());

            Marshal.StructureToPtr(securityInformationInterop, securityInformationInteropPtr, false);
        }
    }

    // Callback token wrapper classes.
    public class XNetworkingRegisterConnectivityHintChangedCallbackToken
    {
        internal Interop.XNetworkingRegisterConnectivityHintChangedCallbackToken interop;


        internal XNetworkingRegisterConnectivityHintChangedCallbackToken(Interop.XNetworkingConnectivityHintChangedCallback callback,
            IntPtr context)
        {
            interop = new Interop.XNetworkingRegisterConnectivityHintChangedCallbackToken(callback, context);
        }

        public bool IsValid { get { return interop.IsValid; } }

        public UInt64 Token
        {
            get => interop.Token;
            set => interop.Token = value;
        }

        public bool Unregister(bool wait)
        {
            return interop.Unregister(wait);
        }

        public void Dispose()
        {
            interop.Dispose();
        }
    }

    // Callback token wrapper classes.
    public class XNetworkingPreferredLocalUdpMultiplayerPortChangedCallbackToken
    {
        internal Interop.XNetworkingPreferredLocalUdpMultiplayerPortChangedCallbackToken interop;


        internal XNetworkingPreferredLocalUdpMultiplayerPortChangedCallbackToken(Interop.XNetworkingPreferredLocalUdpMultiplayerPortChangedCallback callback,
            IntPtr context)
        {
            interop = new Interop.XNetworkingPreferredLocalUdpMultiplayerPortChangedCallbackToken(callback, context);
        }

        public bool IsValid { get { return interop.IsValid; } }

        public UInt64 Token
        {
            get => interop.Token;
            set => interop.Token = value;
        }

        public bool Unregister(bool wait)
        {
            return interop.Unregister(wait);
        }

        public void Dispose()
        {
            interop.Dispose();
        }
    }
}
