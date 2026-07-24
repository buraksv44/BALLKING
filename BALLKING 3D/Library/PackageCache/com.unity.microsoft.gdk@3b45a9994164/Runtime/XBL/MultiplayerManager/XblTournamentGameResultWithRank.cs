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
    public class XblTournamentGameResultWithRank
    {
        internal XblTournamentGameResultWithRank(Interop.XblTournamentGameResultWithRank interopStruct)
        {
            this.Result = interopStruct.Result;
            this.Ranking = interopStruct.Ranking;
        }

        public XblTournamentGameResult Result { get; }
        public UInt64 Ranking { get; }
    }
}
