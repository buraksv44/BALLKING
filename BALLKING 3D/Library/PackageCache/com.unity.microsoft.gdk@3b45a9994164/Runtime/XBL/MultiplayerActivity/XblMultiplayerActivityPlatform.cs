using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblMultiplayerActivityPlatform : uint32_t
    //{
    //    Unknown = 0,
    //    XboxOne = 1,
    //    WindowsOneCore = 2,
    //    Win32 = 3,
    //    Scarlett = 4,
    //    iOS = 20,
    //    Android = 30,
    //    Nintendo = 40,
    //    PlayStation = 50,
    //    All = 60
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblMultiplayerActivityPlatform : UInt32
    {
        Unknown = 0,
        XboxOne = 1,
        WindowsOneCore = 2,
        Win32 = 3,
        Scarlett = 4,
        iOS = 20,
        Android = 30,
        Nintendo = 40,
        PlayStation = 50,
        All = 60
    }
}
