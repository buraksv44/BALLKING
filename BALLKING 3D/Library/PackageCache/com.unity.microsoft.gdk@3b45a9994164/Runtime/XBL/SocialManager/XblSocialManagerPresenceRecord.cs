using System;
using System.Linq;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblSocialManagerPresenceRecord
    {
        internal XblSocialManagerPresenceRecord(Interop.XblSocialManagerPresenceRecord interopRecord)
        {
            this.UserState = interopRecord.userState;
            this.PresenceTitleRecords = interopRecord.presenceTitleRecords
                .Where((r, index) => (UInt32)index < interopRecord.presenceTitleCount)
                .Select(r => new XblSocialManagerPresenceTitleRecord(r))
                .ToArray();
        }

        public XblPresenceUserState UserState { get; private set; }
        public XblSocialManagerPresenceTitleRecord[] PresenceTitleRecords;
    }
}
