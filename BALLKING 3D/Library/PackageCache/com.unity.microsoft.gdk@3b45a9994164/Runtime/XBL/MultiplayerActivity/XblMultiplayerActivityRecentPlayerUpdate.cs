using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblMultiplayerActivityRecentPlayerUpdate
    {
        public XblMultiplayerActivityRecentPlayerUpdate()
        {

        }

        public UInt64 Xuid { get; set; }

        public XblMultiplayerActivityEncounterType EncounterType { get; set; }
    }
}
