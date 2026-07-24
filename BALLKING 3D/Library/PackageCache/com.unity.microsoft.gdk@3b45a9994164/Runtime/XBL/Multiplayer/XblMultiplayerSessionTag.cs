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
    public class XblMultiplayerSessionTag
    {
        public XblMultiplayerSessionTag(string value)
        {
            Value = value;
        }

        internal XblMultiplayerSessionTag(Interop.XblMultiplayerSessionTag interopStruct)
        {
            this.Value = interopStruct.GetValue();
        }

        public string Value { get; }
    }
}
