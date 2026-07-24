using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblNetworkAddressTranslationSetting : uint32_t
    //{
    //    Unknown,
    //    Open,
    //    Moderate,
    //    Strict
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblNetworkAddressTranslationSetting
    {
        Unknown = 0,
        Open = 1,
        Moderate = 2,
        Strict = 3,
    }
}
