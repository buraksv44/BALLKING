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
    public class XblMultiplayerInviteHandle
    {
        internal XblMultiplayerInviteHandle(Interop.XblMultiplayerInviteHandle interopStruct)
        {
            this.Data = interopStruct.GetData();
        }

        public string Data { get; }
    }
}
