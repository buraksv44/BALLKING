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
    public class XblTournamentTeamResult
    {
        internal XblTournamentTeamResult(Interop.XblTournamentTeamResult interopStruct)
        {
            this.Team = interopStruct.Team.GetString();
            this.GameResult = new XblTournamentGameResultWithRank(interopStruct.GameResult);
        }

        public string Team { get; }
        public XblTournamentGameResultWithRank GameResult { get; }
    }
}
