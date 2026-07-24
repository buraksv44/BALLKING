using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblSocialManagerUser
    {
        internal XblSocialManagerUser(Interop.XblSocialManagerUser interopUser)
        {
            this.XboxUserId = interopUser.xboxUserId;
            this.IsFavorite = interopUser.isFavorite;
            this.IsFriend = false;
            this.IsFollowingUser = interopUser.isFollowingUser;
            this.IsFollowedByCaller = interopUser.isFollowedByCaller;
            this.DisplayName = Interop.Converters.ByteArrayToString(interopUser.displayName);
            this.RealName = Interop.Converters.ByteArrayToString(interopUser.realName);
            this.DisplayPicUrlRaw = Interop.Converters.ByteArrayToString(interopUser.displayPicUrlRaw);
            this.UseAvatar = interopUser.useAvatar;
            this.Gamerscore = Interop.Converters.ByteArrayToString(interopUser.gamerscore);
            this.Gamertag = Interop.Converters.ByteArrayToString(interopUser.gamertag);
            this.ModernGamertag = Interop.Converters.ByteArrayToString(interopUser.modernGamertag);
            this.ModernGamertagSuffix = Interop.Converters.ByteArrayToString(interopUser.modernGamertagSuffix);
            this.UniqueModernGamertag = Interop.Converters.ByteArrayToString(interopUser.uniqueModernGamertag);
            this.PresenceRecord = new XblSocialManagerPresenceRecord(interopUser.presenceRecord);
            this.TitleHistory = new XblTitleHistory(interopUser.titleHistory);
            this.PreferredColor = new XblPreferredColor(interopUser.preferredColor);
        }

        internal XblSocialManagerUser(Interop.XblSocialManagerUser2 interopUser)
        {
            this.XboxUserId = interopUser.xboxUserId;
            this.IsFavorite = interopUser.isFavorite;
            this.IsFriend = interopUser.isFriend;
            this.IsFollowingUser = interopUser.isFollowingUser;
            this.IsFollowedByCaller = interopUser.isFollowedByCaller;
            this.DisplayName = Interop.Converters.ByteArrayToString(interopUser.displayName);
            this.RealName = Interop.Converters.ByteArrayToString(interopUser.realName);
            this.DisplayPicUrlRaw = Interop.Converters.ByteArrayToString(interopUser.displayPicUrlRaw);
            this.UseAvatar = interopUser.useAvatar;
            this.Gamerscore = Interop.Converters.ByteArrayToString(interopUser.gamerscore);
            this.Gamertag = Interop.Converters.ByteArrayToString(interopUser.gamertag);
            this.ModernGamertag = Interop.Converters.ByteArrayToString(interopUser.modernGamertag);
            this.ModernGamertagSuffix = Interop.Converters.ByteArrayToString(interopUser.modernGamertagSuffix);
            this.UniqueModernGamertag = Interop.Converters.ByteArrayToString(interopUser.uniqueModernGamertag);
            this.PresenceRecord = new XblSocialManagerPresenceRecord(interopUser.presenceRecord);
            this.TitleHistory = new XblTitleHistory(interopUser.titleHistory);
            this.PreferredColor = new XblPreferredColor(interopUser.preferredColor);
        }

        public UInt64 XboxUserId { get; private set; }
        public bool IsFavorite { get; private set; }
        public bool IsFriend 
        {
            get
            {
                // Version check only in debug/development builds
#if DEBUG
                if (!s_isFriendWarningShown && SDK.GetGdkEdition() < 241000)
                {
                    Debug.LogWarning(
                        "XblSocialManagerUser.IsFriend property is only supported for SDK.GDKEdition() >= 241000. " +
                        "This property will always return false on older SDK versions.");
                    s_isFriendWarningShown = true;
                }
#endif

                return m_isFriend;
            }
            private set
            {
                m_isFriend = value;
            }
        }
        public bool IsFollowingUser { get; private set; }
        public bool IsFollowedByCaller { get; private set; }
        public string DisplayName { get; private set; }
        public string RealName { get; private set; }
        public string DisplayPicUrlRaw { get; private set; }
        public bool UseAvatar { get; private set; }
        public string Gamerscore { get; private set; }
        public string Gamertag { get; private set; }
        public string ModernGamertag { get; private set; }
        public string ModernGamertagSuffix { get; private set; }
        public string UniqueModernGamertag { get; private set; }

        public XblSocialManagerPresenceRecord PresenceRecord { get; private set; }
        public XblTitleHistory TitleHistory { get; private set; }
        public XblPreferredColor PreferredColor { get; private set; }

#if DEBUG
        // Static flag only exists in none release builds
        private static bool s_isFriendWarningShown = false;
#endif

        private bool m_isFriend;
    }
}
