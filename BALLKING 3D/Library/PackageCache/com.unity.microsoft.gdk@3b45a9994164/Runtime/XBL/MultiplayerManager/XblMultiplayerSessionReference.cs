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
    public class XblMultiplayerSessionReference
    {
        internal XblMultiplayerSessionReference(Interop.XblMultiplayerSessionReference interopStruct)
        {
            this.Scid = interopStruct.GetScid();
            this.SessionTemplateName = interopStruct.GetSessionTemplateName();
            this.SessionName = interopStruct.GetSessionName();
        }

        public string Scid { get; }
        public string SessionTemplateName { get; }
        public string SessionName { get; }
    }
}
