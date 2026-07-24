using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblAchievementTitleAssociation
    {
        internal XblAchievementTitleAssociation(Interop.XblAchievementTitleAssociation interopTitleAssociation)
        {
            this.Name = interopTitleAssociation.name.GetString();
            this.TitleId = interopTitleAssociation.titleId;
        }

        public string Name { get; private set; }
        public UInt32 TitleId { get; private set; }
    }
}
