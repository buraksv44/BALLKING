using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime

{
    //enum class XblReputationFeedbackType : uint32_t
    //{
    //    FairPlayKillsTeammates,
    //    FairPlayCheater,
    //    FairPlayTampering,
    //    FairPlayQuitter,
    //    FairPlayKicked,
    //    CommunicationsInappropriateVideo,
    //    CommunicationsAbusiveVoice,
    //    InappropriateUserGeneratedContent,
    //    PositiveSkilledPlayer,
    //    PositiveHelpfulPlayer,
    //    PositiveHighQualityUserGeneratedContent,
    //    CommsPhishing,
    //    CommsPictureMessage,
    //    CommsSpam,
    //    CommsTextMessage,
    //    CommsVoiceMessage,
    //    FairPlayConsoleBanRequest,
    //    FairPlayIdler,
    //    FairPlayUserBanRequest,
    //    UserContentGamerpic,
    //    UserContentPersonalInfo,
    //    FairPlayUnsporting,
    //    FairPlayLeaderboardCheater
    //};

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XblReputationFeedbackType : UInt32
    {
        FairPlayKillsTeammates = 0,
        FairPlayCheater,
        FairPlayTampering,
        FairPlayQuitter,
        FairPlayKicked,
        CommunicationsInappropriateVideo,
        CommunicationsAbusiveVoice,
        InappropriateUserGeneratedContent,
        PositiveSkilledPlayer,
        PositiveHelpfulPlayer,
        PositiveHighQualityUserGeneratedContent,
        CommsPhishing,
        CommsPictureMessage,
        CommsSpam,
        CommsTextMessage,
        CommsVoiceMessage,
        FairPlayConsoleBanRequest,
        FairPlayIdler,
        FairPlayUserBanRequest,
        UserContentGamerpic,
        UserContentPersonalInfo,
        FairPlayUnsporting,
        FairPlayLeaderboardCheater
    }
}
