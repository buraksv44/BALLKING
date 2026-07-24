using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblMultiplayerPerformQoSMeasurementsArgs
    {
        internal XblMultiplayerPerformQoSMeasurementsArgs(Interop.XblMultiplayerPerformQoSMeasurementsArgs interopStruct)
        {
            this.RemoteClients = interopStruct.GetRemoteClients(x => new XblMultiplayerConnectionAddressDeviceTokenPair(x));
        }

        public XblMultiplayerConnectionAddressDeviceTokenPair[] RemoteClients { get; }
    }
}
