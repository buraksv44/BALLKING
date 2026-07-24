using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblPresenceBroadcastRecord
    {
        internal XblPresenceBroadcastRecord(Interop.XblPresenceBroadcastRecord interopRecord)
        {
            this.BroadcastId = interopRecord.broadcastId.GetString();
            this.Session = Converters.ByteArrayToString(interopRecord.session);
            this.Provider = interopRecord.provider;
            this.ViewerCount = interopRecord.viewerCount;
            this.StartTime = interopRecord.startTime.DateTime;
        }

        public string BroadcastId { get; private set; }
        public string Session { get; private set; }
        public XblPresenceBroadcastProvider Provider { get; private set; }
        public UInt32 ViewerCount { get; private set; }
        public DateTime StartTime { get; private set; }
    }
}
