using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XblTicketStatus : uint32_t
    //{
    //    Unknown,
    //    Expired,
    //    Searching,
    //    Found,
    //    Canceled
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblTicketStatus
    {
        Unknown,
        Expired,
        Searching,
        Found,
        Canceled
    }
}
