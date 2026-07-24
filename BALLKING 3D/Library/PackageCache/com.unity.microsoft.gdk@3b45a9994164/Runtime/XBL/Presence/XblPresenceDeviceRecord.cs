using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblPresenceDeviceRecord
    {
        internal XblPresenceDeviceRecord(Interop.XblPresenceDeviceRecord interopRecord)
        {
            this.DeviceType = interopRecord.deviceType;
            this.TitleRecords = interopRecord.GetTitleRecords(tr => new XblPresenceTitleRecord(tr));
        }

        public XblPresenceDeviceType DeviceType { get; private set; }
        public XblPresenceTitleRecord[] TitleRecords { get; private set; }
    }
}
