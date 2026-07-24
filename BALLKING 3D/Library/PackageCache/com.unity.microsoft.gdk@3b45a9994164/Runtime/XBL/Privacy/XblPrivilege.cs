using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblPrivilege : uint32_t
    //{
    //    Unknown = 0,
    //    AllowIngameVoiceCommunications = 205,
    //    AllowVideoCommunications = 235,
    //    AllowProfileViewing = 249,
    //    AllowCommunications = 252,
    //    AllowMultiplayer = 254,
    //    AllowAddFriend = 255
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblPrivilege : UInt32
    {
        Unknown = 0,
        AllowIngameVoiceCommunications = 205,
        AllowVideoCommunications = 235,
        AllowProfileViewing = 249,
        AllowCommunications = 252,
        AllowMultiplayer = 254,
        AllowAddFriend = 255,
    }
}
