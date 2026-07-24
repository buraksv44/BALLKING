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
    public class XblMultiplayerSessionStringAttribute
    {
        public XblMultiplayerSessionStringAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        internal XblMultiplayerSessionStringAttribute(Interop.XblMultiplayerSessionStringAttribute interopStruct)
        {
            this.Name = interopStruct.GetName();
            this.Value = interopStruct.GetValue();
        }

        public string Name { get; }
        public string Value { get; }
    }
}
