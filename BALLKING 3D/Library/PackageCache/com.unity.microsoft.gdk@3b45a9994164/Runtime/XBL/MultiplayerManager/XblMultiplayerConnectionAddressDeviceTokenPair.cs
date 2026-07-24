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
    public class XblMultiplayerConnectionAddressDeviceTokenPair
    {
        internal XblMultiplayerConnectionAddressDeviceTokenPair(Interop.XblMultiplayerConnectionAddressDeviceTokenPair interopStruct)
        {
            this.ConnectionAddress = interopStruct.connectionAddress.GetString();
            this.DeviceToken = new XblDeviceToken(interopStruct.deviceToken);
        }

        public string ConnectionAddress { get; }
        public XblDeviceToken DeviceToken { get; }
    }
}
