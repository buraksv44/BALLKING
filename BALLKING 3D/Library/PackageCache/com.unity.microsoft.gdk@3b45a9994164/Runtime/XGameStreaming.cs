using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.CSharp;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    // enum class XGameStreamingConnectionState : uint32_t
    // {
    //     Disconnected = 0,
    //     Connected = 1
    // };
    public enum XGameStreamingConnectionState : UInt32
    {
        Disconnected = 0,
        Connected = 1
    };

    // enum class XGameStreamingClientProperty : uint32_t
    // {
    //     None = 0,
    //     StreamPhysicalDimensions = 1,
    //     TouchInputEnabled = 2,
    //     TouchBundleVersion = 4,
    //     IPAddress = 5,
    //     SessionId = 6,
    //     DisplayDetails = 7,
    // };
    public enum XGameStreamingClientProperty : UInt32
    {
        None = 0,
        StreamPhysicalDimensions = 1,
        TouchInputEnabled = 2,
        TouchBundleVersion = 4,
        IPAddress = 5,
        SessionId = 6,
        DisplayDetails = 7,
    }

    // enum class XGameStreamingGamepadPhysicality : uint64_t
    // {
    //     None = 0x0000000000000000,
    //     DPadUpPhysical = 0x0000000000000001,
    //     DPadDownPhysical = 0x0000000000000002,
    //     DPadLeftPhysical = 0x0000000000000004,
    //     DPadRightPhysical = 0x0000000000000008,
    //     MenuPhysical = 0x0000000000000010,
    //     ViewPhysical = 0x0000000000000020,
    //     LeftThumbstickPhysical = 0x0000000000000040,
    //     RightThumbstickPhysical = 0x0000000000000080,
    //     LeftShoulderPhysical = 0x0000000000000100,
    //     RightShoulderPhysical = 0x0000000000000200,
    //     APhysical = 0x0000000000001000,
    //     BPhysical = 0x0000000000002000,
    //     XPhysical = 0x0000000000004000,
    //     YPhysical = 0x0000000000008000,
    //     LeftTriggerPhysical = 0x0000000000010000,
    //     RightTriggerPhysical = 0x0000000000020000,
    //     LeftThumbstickXPhysical = 0x0000000000040000,
    //     LeftThumbstickYPhysical = 0x0000000000080000,
    //     RightThumbstickXPhysical = 0x0000000000100000,
    //     RightThumbstickYPhysical = 0x0000000000200000,
    //     ButtonsPhysical = 0x000000000000F3FF,
    //     AnalogsPhysical = 0x00000000003F0000,
    //     AllPhysical = 0x00000000003FF3FF,

    //     DPadUpVirtual = 0x0000000100000000,
    //     DPadDownVirtual = 0x0000000200000000,
    //     DPadLeftVirtual = 0x0000000400000000,
    //     DPadRightVirtual = 0x0000000800000000,
    //     MenuVirtual = 0x0000001000000000,
    //     ViewVirtual = 0x0000002000000000,
    //     LeftThumbstickVirtual = 0x0000004000000000,
    //     RightThumbstickVirtual = 0x0000008000000000,
    //     LeftShoulderVirtual = 0x0000010000000000,
    //     RightShoulderVirtual = 0x0000020000000000,
    //     AVirtual = 0x0000100000000000,
    //     BVirtual = 0x0000200000000000,
    //     XVirtual = 0x0000400000000000,
    //     YVirtual = 0x0000800000000000,
    //     LeftTriggerVirtual = 0x0001000000000000,
    //     RightTriggerVirtual = 0x0002000000000000,
    //     LeftThumbstickXVirtual = 0x0004000000000000,
    //     LeftThumbstickYVirtual = 0x0008000000000000,
    //     RightThumbstickXVirtual = 0x0010000000000000,
    //     RightThumbstickYVirtual = 0x0020000000000000,
    //     ButtonsVirtual = 0x0000F3FF00000000,
    //     AnalogsVirtual = 0x003F000000000000,
    //     AllVirtual = 0x003FF3FF00000000,
    // };
    public enum XGameStreamingGamepadPhysicality : UInt64
    {
        None = 0x0000000000000000,
        DPadUpPhysical = 0x0000000000000001,
        DPadDownPhysical = 0x0000000000000002,
        DPadLeftPhysical = 0x0000000000000004,
        DPadRightPhysical = 0x0000000000000008,
        MenuPhysical = 0x0000000000000010,
        ViewPhysical = 0x0000000000000020,
        LeftThumbstickPhysical = 0x0000000000000040,
        RightThumbstickPhysical = 0x0000000000000080,
        LeftShoulderPhysical = 0x0000000000000100,
        RightShoulderPhysical = 0x0000000000000200,
        APhysical = 0x0000000000001000,
        BPhysical = 0x0000000000002000,
        XPhysical = 0x0000000000004000,
        YPhysical = 0x0000000000008000,
        LeftTriggerPhysical = 0x0000000000010000,
        RightTriggerPhysical = 0x0000000000020000,
        LeftThumbstickXPhysical = 0x0000000000040000,
        LeftThumbstickYPhysical = 0x0000000000080000,
        RightThumbstickXPhysical = 0x0000000000100000,
        RightThumbstickYPhysical = 0x0000000000200000,
        ButtonsPhysical = 0x000000000000F3FF,
        AnalogsPhysical = 0x00000000003F0000,
        AllPhysical = 0x00000000003FF3FF,

        DPadUpVirtual = 0x0000000100000000,
        DPadDownVirtual = 0x0000000200000000,
        DPadLeftVirtual = 0x0000000400000000,
        DPadRightVirtual = 0x0000000800000000,
        MenuVirtual = 0x0000001000000000,
        ViewVirtual = 0x0000002000000000,
        LeftThumbstickVirtual = 0x0000004000000000,
        RightThumbstickVirtual = 0x0000008000000000,
        LeftShoulderVirtual = 0x0000010000000000,
        RightShoulderVirtual = 0x0000020000000000,
        AVirtual = 0x0000100000000000,
        BVirtual = 0x0000200000000000,
        XVirtual = 0x0000400000000000,
        YVirtual = 0x0000800000000000,
        LeftTriggerVirtual = 0x0001000000000000,
        RightTriggerVirtual = 0x0002000000000000,
        LeftThumbstickXVirtual = 0x0004000000000000,
        LeftThumbstickYVirtual = 0x0008000000000000,
        RightThumbstickXVirtual = 0x0010000000000000,
        RightThumbstickYVirtual = 0x0020000000000000,
        ButtonsVirtual = 0x0000F3FF00000000,
        AnalogsVirtual = 0x003F000000000000,
        AllVirtual = 0x003FF3FF00000000,
    };

    // enum class XGameStreamingTouchControlsStateOperationKind : uint32_t
    // {
    //     Replace = 0
    // };
    public enum XGameStreamingTouchControlsStateOperationKind : UInt32
    {
        Replace = 0
    };

    // enum class XGameStreamingTouchControlsStateValueKind : uint32_t
    // {
    //     Boolean = 0,
    //     Integer = 1,
    //     Double = 2,
    //     String = 3
    // };
    public enum XGameStreamingTouchControlsStateValueKind : UInt32
    {
        Boolean = 0,
        Integer = 1,
        Double = 2,
        String = 3
    };

    // enum class XGameStreamingVideoFlags : uint32_t
    // {
    //     None = 0x0,
    //     SupportsCustomAspectRatio = 0x1,
    //     SupportsPresentScaling = 0x2,
    //     All = 0x3,
    // };
    public enum XGameStreamingVideoFlags : UInt32
    {
        None = 0x0,
        SupportsCustomAspectRatio = 0x1,
        SupportsPresentScaling = 0x2,
        All = 0x3,
    };

    public class XGameStreamingClientId
    {
        public XGameStreamingClientId(UInt64 value)
        {
            data = new Interop.XGameStreamingClientId();
            data.value = value;
        }

        internal XGameStreamingClientId(Interop.XGameStreamingClientId interop)
        {
            this.data = interop;
        }

        internal Interop.XGameStreamingClientId data;

        public UInt64 Value
        {
            get => data.value;
            set => data.value = value;
        }
    };

    public class D3D12XBOX_FRAME_PIPELINE_TOKEN
    {
        public D3D12XBOX_FRAME_PIPELINE_TOKEN(UInt64 value)
        {
            data = new Interop.D3D12XBOX_FRAME_PIPELINE_TOKEN();
            data.value = value;
        }

        internal D3D12XBOX_FRAME_PIPELINE_TOKEN(Interop.D3D12XBOX_FRAME_PIPELINE_TOKEN interop)
        {
            this.data = interop;
        }

        internal Interop.D3D12XBOX_FRAME_PIPELINE_TOKEN data;

        public UInt64 Value
        {
            get => data.value;
            set => data.value = value;
        }
    }

    public class XGameStreamingTouchControlsStateValue
    {
        internal XGameStreamingTouchControlsStateValue(Interop.XGameStreamingTouchControlsStateValue interop)
        {
            this.interop = interop;
        }

        public XGameStreamingTouchControlsStateValue()
        {
            interop = new Interop.XGameStreamingTouchControlsStateValue();
        }

        internal Interop.XGameStreamingTouchControlsStateValue interop;
        internal string _stringValue;

        public XGameStreamingTouchControlsStateValueKind ValueKind
        {
            get => interop.valueKind;
            set => interop.valueKind = value;
        }

        public string StringValue
        {
            get => _stringValue;
            set => _stringValue = value;
        }

        public double DoubleValue
        {
            get => interop.doubleValue;
            set => interop.doubleValue = value;
        }

        public bool BoolValue
        {
            get => interop.boolValue;
            set => interop.boolValue = value;
        }

        public UInt32 IntegerValue
        {
            get => interop.integerValue;
            set => interop.integerValue = value;
        }
    }

    public class XGameStreamingTouchControlsStateOperation
    {
        internal XGameStreamingTouchControlsStateOperation(Interop.XGameStreamingTouchControlsStateOperation interop)
        {
            this._interop = interop;
            _value = new XGameStreamingTouchControlsStateValue(interop.value);
        }

        public XGameStreamingTouchControlsStateOperation()
        {
            this._interop = new Interop.XGameStreamingTouchControlsStateOperation();
            _value = new XGameStreamingTouchControlsStateValue();
        }

        internal Interop.XGameStreamingTouchControlsStateOperation _interop;
        internal XGameStreamingTouchControlsStateValue _value;

        internal Interop.XGameStreamingTouchControlsStateOperation interop
        {
            get
            {
                _interop.value = _value.interop;
                return _interop;
            }
        }

        public XGameStreamingTouchControlsStateOperationKind OperationKind
        {
            get => this._interop.operationKind;
            set => this._interop.operationKind = value;
        }

        public string Path
        {
            get => this._interop.path;
            set => this._interop.path = value;
        }

        public XGameStreamingTouchControlsStateValue Value
        {
            get => this._value;
            set => this._value = value;
        }
    }

    public class RECT
    {
        internal RECT(Interop.RECT interop)
        {
            this.interop = interop;
        }

        public RECT()
        {
            interop = new Interop.RECT();
        }

        internal Interop.RECT interop;

        public UInt32 Left
        {
            get => interop.left;
            set => interop.left = value;
        }

        public UInt32 Top
        {
            get => interop.top;
            set => interop.top = value;
        }

        public UInt32 Right
        {
            get => interop.right;
            set => interop.right = value;
        }

        public UInt32 Bottom
        {
            get => interop.bottom;
            set => interop.bottom = value;
        }
    }

    public class XGameStreamingDisplayDetails
    {
        internal XGameStreamingDisplayDetails(Interop.XGameStreamingDisplayDetails interop)
        {
            this._interop = interop;
            this.rect = new RECT(interop.safeArea);
        }

        public XGameStreamingDisplayDetails()
        {
            _interop = new Interop.XGameStreamingDisplayDetails();
            rect = new RECT();
        }

        internal Interop.XGameStreamingDisplayDetails _interop;
        internal RECT rect;

        internal Interop.XGameStreamingDisplayDetails interop
        {
            get
            {
                _interop.safeArea = rect.interop;
                return _interop;
            }
        }

        public UInt32 PreferredWidth
        {
            get => _interop.preferredWidth;
            set => _interop.preferredWidth = value;
        }

        public UInt32 PreferredHeight
        {
            get => _interop.preferredHeight;
            set => _interop.preferredHeight = value;
        }

        public RECT SafeArea
        {
            get => rect;
            set => rect = value;
        }

        public UInt32 MaxPixels
        {
            get => _interop.maxPixels;
            set => _interop.maxPixels = value;
        }

        public UInt32 MaxWidth
        {
            get => _interop.maxWidth;
            set => _interop.maxWidth = value;
        }

        public UInt32 MaxHeight
        {
            get => _interop.maxHeight;
            set => _interop.maxHeight = value;
        }

        public XGameStreamingVideoFlags Flags
        {
            get => _interop.flags;
            set => _interop.flags = value;
        }
    };

    public class XGameStreamingConnectionStateChangedToken
    {
        internal Interop.XGameStreamingConnectionStateChangedToken interop { get; private set; }

        internal XGameStreamingConnectionStateChangedToken(Interop.XGameStreamingConnectionStateChangedCallback callback, IntPtr context)
        {
            interop = new Interop.XGameStreamingConnectionStateChangedToken(callback, context);
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
    }

    public class XGameStreamingRegisterClientPropertiesChangedToken
    {
        internal Interop.XGameStreamingRegisterClientPropertiesChangedToken interop { get; private set; }

        internal XGameStreamingRegisterClientPropertiesChangedToken(XGameStreamingClientId clientId, Interop.XGameStreamingClientPropertiesChangedCallback callback, IntPtr context)
        {
            interop = new Interop.XGameStreamingRegisterClientPropertiesChangedToken(clientId.data, callback, context);
        }

        public bool IsValid { get { return interop.IsValid; } }

        public UInt64 Token
        {
            get => interop.Token;
            set => interop.Token = value;
        }

        public bool Unregister(XGameStreamingClientId clientId, bool wait)
        {
            return interop.Unregister(clientId.data, wait);
        }
    }

    public delegate void XGameStreamingClientPropertiesChangedCallback(IntPtr context,
        XGameStreamingClientId client,
        XGameStreamingClientProperty[] updatedProperties);

    public delegate void XGameStreamingConnectionStateChangedCallback(IntPtr context,
        XGameStreamingClientId client,
        XGameStreamingConnectionState state);

    partial class SDK
    {
        // Sentinel ID which represents the lack of a client.
        XGameStreamingClientId XGameStreamingNullClientId = new XGameStreamingClientId(0);

        // Sentinel ID which represents all connect clients
        XGameStreamingClientId XGameStreamingAllClients = new XGameStreamingClientId(0xFFFFFFFFFFFFFFFF);

        // The recommended size of the ipAddress parameter for the XGameStreamingGetClientIPAddress API.
        const UInt64 ClientIPAddressMaxBytes = 65;

        // The recommended size of the sessionId parameter for XGameStreamingGetSessionId API.
        const UInt64 SessionIdMaxBytes = 256;

        private static Int32 ProcessTouchControlOperation(ref XGameStreamingTouchControlsStateOperation[] operations,
            out Interop.XGameStreamingTouchControlsStateOperation[] nativeOperations, out List<IntPtr> stringsToFree)
        {
            nativeOperations = new Interop.XGameStreamingTouchControlsStateOperation[operations.Length];

            stringsToFree = new List<IntPtr>();

            for (int i = 0; i < operations.Length; i++)
            {
                XGameStreamingTouchControlsStateOperation operation = operations[i];
                nativeOperations[i].path = operation.Path;
                nativeOperations[i].value.valueKind = operation.Value.ValueKind;

                switch (operation.Value.ValueKind)
                {
                    case XGameStreamingTouchControlsStateValueKind.Boolean:
                        nativeOperations[i].value.boolValue = operation.Value.BoolValue;
                        break;
                    case XGameStreamingTouchControlsStateValueKind.Integer:
                        nativeOperations[i].value.integerValue = operation.Value.IntegerValue;
                        break;
                    case XGameStreamingTouchControlsStateValueKind.Double:
                        nativeOperations[i].value.doubleValue = operation.Value.DoubleValue;
                        break;
                    case XGameStreamingTouchControlsStateValueKind.String:
                        nativeOperations[i].value.stringValue = InteropHelpers.MarshalStringUtf8(operation.Value.StringValue);
                        stringsToFree.Add(nativeOperations[i].value.stringValue);
                        break;
                    default:
                        return HR.E_INVALIDARG;
                }
            }

            return HR.S_OK;
        }

        public static int XGameStreamingInitialize()
        {
            return NativeMethods.XGameStreamingInitialize();
        }

        public static void XGameStreamingUninitialize()
        {
            NativeMethods.XGameStreamingUninitialize();
        }

        public static bool XGameStreamingIsStreaming()
        {
            return NativeMethods.XGameStreamingIsStreaming();
        }

        public static UInt32 XGameStreamingGetClientCount()
        {
            return NativeMethods.XGameStreamingGetClientCount();
        }

        public static Int32 XGameStreamingGetClients(ref XGameStreamingClientId[] clients, out UInt32 clientUsed)
        {
            var clientsNative = new Interop.XGameStreamingClientId[clients.Length];
            int hr = NativeMethods.XGameStreamingGetClients((UInt32)clients.Length, clientsNative, out clientUsed);

            if (HR.SUCCEEDED(hr))
            {
                clients = new XGameStreamingClientId[clientUsed];

                for (int i = 0; i < clientUsed; i++)
                {
                    clients[i] = new XGameStreamingClientId(clientsNative[i]);
                }
            }

            return hr;
        }

        public static XGameStreamingConnectionState XGameStreamingGetConnectionState(XGameStreamingClientId client)
        {
            return NativeMethods.XGameStreamingGetConnectionState(client.data);
        }

        public static Int32 XGameStreamingRegisterConnectionStateChanged(XTaskQueueHandle queue, IntPtr context,
            XGameStreamingConnectionStateChangedCallback callback, out XGameStreamingConnectionStateChangedToken token)
        {
            Interop.XGameStreamingConnectionStateChangedCallback callbackInterop = (_context, _client, _state) =>
            {
                callback(_context, new XGameStreamingClientId(_client), _state);
            };

            token = new XGameStreamingConnectionStateChangedToken(callbackInterop, context);

            UInt64 tokenValue = 0;
            IntPtr interopQueue = queue != null ? queue.Handle : IntPtr.Zero;
            int hr = NativeMethods.XGameStreamingRegisterConnectionStateChanged(interopQueue,
                token.interop.CallbackContext,
                token.interop.StaticCallback,
                out tokenValue);
            if (HR.SUCCEEDED(hr) && tokenValue != 0)
            {
                token.Token = tokenValue;
            }
            else
            {
                token.interop.Dispose();
                token = null;
            }

            return hr;
        }

        public static bool XGameStreamingUnregisterConnectionStateChanged(XGameStreamingConnectionStateChangedToken token, bool wait)
        {
            return token.Unregister(wait);
        }

        public static void XGameStreamingHideTouchControls()
        {
            NativeMethods.XGameStreamingHideTouchControls();
        }

        public static void XGameStreamingHideTouchControlsOnClient(XGameStreamingClientId client)
        {
            NativeMethods.XGameStreamingHideTouchControlsOnClient(client.data);
        }

        public static void XGameStreamingShowTouchControlLayout(string layout)
        {
            NativeMethods.XGameStreamingShowTouchControlLayout(layout);
        }

        public static void XGameStreamingShowTouchControlLayoutOnClient(XGameStreamingClientId client, string layout)
        {
            NativeMethods.XGameStreamingShowTouchControlLayoutOnClient(client.data, layout);
        }

        public static Int32 XGameStreamingRegisterClientPropertiesChanged(XGameStreamingClientId client,
            XTaskQueueHandle queue,
            IntPtr context,
            XGameStreamingClientPropertiesChangedCallback callback,
            out XGameStreamingRegisterClientPropertiesChangedToken token)
        {
            Interop.XGameStreamingClientPropertiesChangedCallback interopCallback = (_context, _clientId, _updatedPropertiesCount, _updatedProperties) =>
            {
                callback(_context, new XGameStreamingClientId(_clientId), _updatedProperties);
            };

            token = new XGameStreamingRegisterClientPropertiesChangedToken(client, interopCallback, context);

            UInt64 tokenValue = 0;
            int hr = NativeMethods.XGameStreamingRegisterClientPropertiesChanged(client.data,
                queue.Handle,
                token.interop.CallbackContext,
                token.interop.StaticCallback,
                out tokenValue);
            if (HR.SUCCEEDED(hr))
            {
                token.Token = tokenValue;
            }
            else
            {
                token.interop.Dispose();
                token = null;
            }

            return hr;
        }

        public static bool XGameStreamingUnregisterClientPropertiesChanged(XGameStreamingClientId client,
            XGameStreamingRegisterClientPropertiesChangedToken token,
            bool wait)
        {
            return token.Unregister(client, wait);
        }

        public static Int32 XGameStreamingGetStreamPhysicalDimensions(XGameStreamingClientId client,
            out UInt32 horizontalMm,
            out UInt32 verticalMm)
        {
            return NativeMethods.XGameStreamingGetStreamPhysicalDimensions(client.data,
                out horizontalMm,
                out verticalMm);
        }

        public static Int32 XGameStreamingGetStreamAddedLatency(XGameStreamingClientId client,
            out UInt32 averageInputLatencyUs,
            out UInt32 averageOutputLatencyUs,
            out UInt32 standardDeviationUs)
        {
            return NativeMethods.XGameStreamingGetStreamAddedLatency(client.data,
                out averageInputLatencyUs,
                out averageOutputLatencyUs,
                out standardDeviationUs);
        }

        public static UInt64 XGameStreamingGetServerLocationNameSize()
        {
            return NativeMethods.XGameStreamingGetServerLocationNameSize();
        }

        public static Int32 XGameStreamingGetServerLocationName(UInt64 serverLocalNameSize, out string serverLocationName)
        {
            serverLocationName = null;

            StringBuilder serverNameSb = new StringBuilder((int)serverLocalNameSize);
            int hr = NativeMethods.XGameStreamingGetServerLocationName(serverLocalNameSize, serverNameSb);

            if (HR.SUCCEEDED(hr))
            {
                serverLocationName = serverNameSb.ToString();
            }

            return hr;
        }

        public static Int32 XGameStreamingIsTouchInputEnabled(XGameStreamingClientId client, out bool touchInputEnabled)
        {
            return NativeMethods.XGameStreamingIsTouchInputEnabled(client.data, out touchInputEnabled);
        }

        public static Int32 XGameStreamingGetLastFrameDisplayed(XGameStreamingClientId client,
            out D3D12XBOX_FRAME_PIPELINE_TOKEN framePipelineToken)
        {
            framePipelineToken = default;
            UInt64 value = default;

            Int32 hr = NativeMethods.XGameStreamingGetLastFrameDisplayed(client.data, out value);

            if(HR.SUCCEEDED(hr))
            {
                framePipelineToken = new D3D12XBOX_FRAME_PIPELINE_TOKEN(value);
            }

            return hr;
        }

        public static Int32 XGameStreamingUpdateTouchControlsState(XGameStreamingTouchControlsStateOperation[] operations)
        {
            Int32 hr = HR.S_OK;

            if (operations != null)
            {
                List<IntPtr> stringsToFree = new List<IntPtr>();

                try
                {
                    Interop.XGameStreamingTouchControlsStateOperation[] nativeOperations;

                    hr = ProcessTouchControlOperation(ref operations, out nativeOperations, out stringsToFree);

                    if (HR.SUCCEEDED(hr))
                    {
                        hr = NativeMethods.XGameStreamingUpdateTouchControlsState((UInt64)operations.Length, nativeOperations);
                    }
                }
                finally
                {
                    foreach (IntPtr strPtr in stringsToFree)
                    {
                        Marshal.FreeCoTaskMem(strPtr);
                    }
                }
            }
            else
            {
                hr = NativeMethods.XGameStreamingUpdateTouchControlsState(0, null);

            }

            return hr;
        }

        public static Int32 XGameStreamingUpdateTouchControlsStateOnClient(XGameStreamingClientId client,
            XGameStreamingTouchControlsStateOperation[] operations)
        {
            Int32 hr = HR.S_OK;

            if(operations != null)
            {
                List<IntPtr> stringsToFree = new List<IntPtr>();

                try
                {
                    Interop.XGameStreamingTouchControlsStateOperation[] nativeOperations = default;

                    hr = ProcessTouchControlOperation(ref operations, out nativeOperations, out stringsToFree);

                    if (HR.SUCCEEDED(hr))
                    {
                        hr = NativeMethods.XGameStreamingUpdateTouchControlsStateOnClient(client.data,
                            (UInt64)operations.Length,
                            nativeOperations);
                    }
                }
                finally
                {
                    foreach (IntPtr strPtr in stringsToFree)
                    {
                        Marshal.FreeCoTaskMem(strPtr);
                    }
                }
            }
            else
            {
                hr = NativeMethods.XGameStreamingUpdateTouchControlsStateOnClient(client.data,
                            0,
                            null);
            }

            return hr;
        }

        public static Int32 XGameStreamingShowTouchControlsWithStateUpdate(string layout, XGameStreamingTouchControlsStateOperation[] operations)
        {
            Int32 hr = HR.S_OK;

            if (operations != null)
            {
                List<IntPtr> stringsToFree = new List<IntPtr>();

                try
                {
                    Interop.XGameStreamingTouchControlsStateOperation[] nativeOperations;

                    hr = ProcessTouchControlOperation(ref operations, out nativeOperations, out stringsToFree);

                    if (HR.SUCCEEDED(hr))
                    {
                        hr = NativeMethods.XGameStreamingShowTouchControlsWithStateUpdate(layout, (UInt64)operations.Length, nativeOperations);
                    }
                }
                finally
                {
                    foreach (IntPtr strPtr in stringsToFree)
                    {
                        Marshal.FreeCoTaskMem(strPtr);
                    }
                }
            }
            else
            {
                hr = NativeMethods.XGameStreamingShowTouchControlsWithStateUpdate(layout, 0, null);
            }

            return hr;
        }

        public static Int32 XGameStreamingShowTouchControlsWithStateUpdateOnClient(XGameStreamingClientId client, string layout, XGameStreamingTouchControlsStateOperation[] operations)
        {
            Int32 hr = HR.S_OK;
            List<IntPtr> stringsToFree = new List<IntPtr>();

            if(operations != null)
            {
                try
                {
                    Interop.XGameStreamingTouchControlsStateOperation[] nativeOperations;

                    hr = ProcessTouchControlOperation(ref operations, out nativeOperations, out stringsToFree);

                    if (HR.SUCCEEDED(hr))
                    {
                        hr = NativeMethods.XGameStreamingShowTouchControlsWithStateUpdateOnClient(client.data, layout, (UInt64)operations.Length, nativeOperations);
                    }
                }
                finally
                {
                    foreach (IntPtr strPtr in stringsToFree)
                    {
                        Marshal.FreeCoTaskMem(strPtr);
                    }
                }
            }
            else
            {
                hr = NativeMethods.XGameStreamingShowTouchControlsWithStateUpdateOnClient(client.data, layout, 0, null);
            }

            return hr;
        }

        public static UInt64 XGameStreamingGetTouchBundleVersionNameSize(XGameStreamingClientId client)
        {
            return NativeMethods.XGameStreamingGetTouchBundleVersionNameSize(client.data);
        }

        public static Int32 XGameStreamingGetTouchBundleVersion(XGameStreamingClientId client,
            out XVersion version,
            UInt64 versionNameSize,
            out string versionName)
        {
            versionName = null;

            StringBuilder versionNameNative = new StringBuilder((int)versionNameSize);
            version = default;
            Interop.XVersion versionInterop = default;

            int hr = NativeMethods.XGameStreamingGetTouchBundleVersion(client.data, out versionInterop, versionNameSize, versionNameNative);

            if (HR.SUCCEEDED(hr))
            {
                versionName = versionNameNative.ToString();
                version = new XVersion(versionInterop);
            }

            return hr;
        }

        public static Int32 XGameStreamingGetClientIPAddress(XGameStreamingClientId client, out string ipAddress)
        {
            ipAddress = null;

            StringBuilder ipAddressSb = new StringBuilder((int)ClientIPAddressMaxBytes);
            int hr = NativeMethods.XGameStreamingGetClientIPAddress(client.data, (ulong)ClientIPAddressMaxBytes, ipAddressSb);

            if (HR.SUCCEEDED(hr))
            {
                ipAddress = ipAddressSb.ToString();
            }

            return hr;
        }

        public static Int32 XGameStreamingGetSessionId(XGameStreamingClientId client, out string sessionId)
        {
            sessionId = null;

            StringBuilder sessionIdSb = new StringBuilder((int)SessionIdMaxBytes);
            UInt64 sessionIdUsed = 0;
            int hr = NativeMethods.XGameStreamingGetSessionId(client.data, SessionIdMaxBytes, sessionIdSb, out sessionIdUsed);

            if (HR.SUCCEEDED(hr))
            {
                sessionId = sessionIdSb.ToString();
            }

            return hr;
        }

        public static Int32 XGameStreamingGetDisplayDetails(XGameStreamingClientId clientId,
            UInt32 maxSupportedPixels,
            float widestSupportedAspectRatio,
            float tallestSupportedAspectRatio,
            out XGameStreamingDisplayDetails displayDetails)
        {
            displayDetails = default;
            Interop.XGameStreamingDisplayDetails displayDetailsInterop = default;

            Int32 hr = NativeMethods.XGameStreamingGetDisplayDetails(clientId.data,
                maxSupportedPixels,
                widestSupportedAspectRatio,
                tallestSupportedAspectRatio,
                out displayDetailsInterop);

            if(HR.SUCCEEDED(hr))
            {
                displayDetails = new XGameStreamingDisplayDetails(displayDetailsInterop);
            }

            return hr;
        }

        public static Int32 XGameStreamingSetResolution(UInt32 width, UInt32 height)
        {
            return NativeMethods.XGameStreamingSetResolution(width, height);
        }
    }
}
