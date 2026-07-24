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
    public class XblMultiplayerSessionHandleId
    {
        internal XblMultiplayerSessionHandleId(Interop.XblMultiplayerSessionHandleId interopHandle)
        {
            this.Value = interopHandle.GetValue();
        }

        public string Value { get; }
    }
}
