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
    public class XblMultiplayerSessionNumberAttribute
    {
        public XblMultiplayerSessionNumberAttribute(string name, double value)
        {
            Name = name;
            Value = value;
        }

        internal XblMultiplayerSessionNumberAttribute(Interop.XblMultiplayerSessionNumberAttribute interopStruct)
        {
            this.Name = interopStruct.GetName();
            this.Value = interopStruct.value;
        }

        public string Name { get; }
        public double Value { get; }
    }
}
