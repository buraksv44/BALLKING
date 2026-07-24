using System;
using System.Linq;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblSocialManagerPresenceTitleRecord
    {
        internal XblSocialManagerPresenceTitleRecord(Interop.XblSocialManagerPresenceTitleRecord interopRecord)
        {
            this.TitleId = interopRecord.titleId;
            this.TitleName = Converters.ByteArrayToString(interopRecord.titleName);
            this.IsTitleActive = interopRecord.isTitleActive;
            this.PresenceText = Converters.ByteArrayToString(interopRecord.presenceText);
            this.IsBroadcasting = interopRecord.isBroadcasting;
            this.DeviceType = interopRecord.deviceType;
            this.IsPrimary = interopRecord.isPrimary;
        }

        public UInt32 TitleId { get; private set; }
        public string TitleName { get; private set; }
        public bool IsTitleActive { get; private set; }
        public string PresenceText { get; private set; }
        public bool IsBroadcasting { get; private set; }
        public XblPresenceDeviceType DeviceType { get; private set; }
        public bool IsPrimary { get; private set; }
    }
}
