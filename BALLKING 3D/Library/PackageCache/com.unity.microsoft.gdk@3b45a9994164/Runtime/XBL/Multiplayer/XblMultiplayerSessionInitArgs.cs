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
    public class XblMultiplayerSessionInitArgs
    {
        public XblMultiplayerSessionInitArgs()
        {
        }

        public UInt32 MaxMembersInSession { get; set; }
        public  XblMultiplayerSessionVisibility Visibility { get; set; }
        public UInt64[] InitiatorXuids { get; set; }
        public string CustomJson { get; set; }
    }
}
