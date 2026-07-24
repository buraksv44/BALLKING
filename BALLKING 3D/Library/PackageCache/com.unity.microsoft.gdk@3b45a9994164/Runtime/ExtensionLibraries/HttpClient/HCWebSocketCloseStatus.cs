using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class HCWebSocketCloseStatus : uint32_t
    //{
    //    Normal = 1000,
    //    GoingAway = 1001,
    //    ProtocolError = 1002,
    //    Unsupported = 1003,
    //    EmptyStatus = 1005,
    //    AbnormalClose = 1006,
    //    InconsistentDatatype = 1007,
    //    PolicyViolation = 1008,
    //    TooLarge = 1009,
    //    NegotiateError = 1010,
    //    ServerTerminate = 1011,
    //    HandshakeError = 1015,
    //    UnknownError = 4000,
    //    ErrorWinhttpTimeout = 12002
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum HCWebSocketCloseStatus : UInt32
    {
        Normal = 1000,
        GoingAway = 1001,
        ProtocolError = 1002,
        Unsupported = 1003,
        EmptyStatus = 1005,
        AbnormalClose = 1006,
        InconsistentDatatype = 1007,
        PolicyViolation = 1008,
        TooLarge = 1009,
        NegotiateError = 1010,
        ServerTerminate = 1011,
        HandshakeError = 1015,
        UnknownError = 4000,
        ErrorWinhttpTimeout = 12002
    }
}
