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
    public class XblPreferredColor
    {
        internal XblPreferredColor(Interop.XblPreferredColor interopPreferredColor)
        {
            this.PrimaryColor = Converters.ByteArrayToString(interopPreferredColor.primaryColor);
            this.SecondaryColor = Converters.ByteArrayToString(interopPreferredColor.secondaryColor);
            this.TertiaryColor = Converters.ByteArrayToString(interopPreferredColor.tertiaryColor);
        }

        public string PrimaryColor { get; private set; }
        public string SecondaryColor { get; private set; }
        public string TertiaryColor { get; private set; }
    }
}
