using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public partial class SDK
    {
        public partial class XBL
        {
            public static Int32 XblEventsWriteInGameEvent(
                XblContextHandle xboxLiveContext,
                string eventName,
                string dimensionsJson,
                string measurementsJson)
            {
                if (xboxLiveContext == null)
                {
                    return HR.E_INVALIDARG;
                }

                return XblInterop.XblEventsWriteInGameEvent(
                    xboxLiveContext.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(eventName),
                    Converters.StringToNullTerminatedUTF8ByteArray(dimensionsJson),
                    Converters.StringToNullTerminatedUTF8ByteArray(measurementsJson));
            }
        }
    }
}
