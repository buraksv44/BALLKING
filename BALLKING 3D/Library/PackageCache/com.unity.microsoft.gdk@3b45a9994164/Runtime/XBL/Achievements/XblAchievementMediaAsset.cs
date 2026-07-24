using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblAchievementMediaAsset
    {
        internal XblAchievementMediaAsset(Interop.XblAchievementMediaAsset mediaAsset)
        {
            this.Name = mediaAsset.name.GetString();
            this.MediaAssetType = mediaAsset.mediaAssetType;
            this.Url = mediaAsset.url.GetString();
        }

        public string Name { get; private set; }
        public XblAchievementMediaAssetType MediaAssetType { get; private set; }
        public string Url { get; private set; }
    }
}
