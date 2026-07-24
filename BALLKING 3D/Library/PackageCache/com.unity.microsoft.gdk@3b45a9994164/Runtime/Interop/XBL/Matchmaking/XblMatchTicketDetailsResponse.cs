using System;
using System.Runtime.InteropServices;
namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblMatchTicketDetailsResponse
    //{
    //    XblTicketStatus matchStatus;
    //    int64_t estimatedWaitTime;
    //    XblPreserveSessionMode preserveSession;
    //    XblMultiplayerSessionReference ticketSession;
    //    XblMultiplayerSessionReference targetSession;
    //    char* ticketAttributes;
    //}
    //XblMatchTicketDetailsResponse;
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblMatchTicketDetailsResponse
    {
        internal XblTicketStatus matchStatus;
        internal Int64 estimatedWaitTime;
        internal XblPreserveSessionMode preserveSession;
        internal XblMultiplayerSessionReference ticketSession;
        internal XblMultiplayerSessionReference targetSession;
        internal readonly UTF8StringPtr ticketAttributes;
    }
}
