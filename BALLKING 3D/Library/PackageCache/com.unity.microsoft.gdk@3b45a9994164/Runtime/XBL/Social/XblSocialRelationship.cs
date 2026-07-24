using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime

{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblSocialRelationship
    {
        internal XblSocialRelationship(Interop.XblSocialRelationship interopHandle)
        {
            this.XboxUserId = interopHandle.xboxUserId;
            this.IsFavourite = interopHandle.isFavorite;
            this.IsFriend = false;
            this.IsFollowingCaller = interopHandle.isFollowingCaller;
            this.SocialNetworks = interopHandle.GetSocialNetworks();
        }

        internal XblSocialRelationship(Interop.XblSocialRelationship2 interopHandle)
        {
            this.XboxUserId = interopHandle.xboxUserId;
            this.IsFavourite = interopHandle.isFavorite;
            this.IsFriend = interopHandle.isFriend;
            this.IsFollowingCaller = interopHandle.isFollowingCaller;
            this.SocialNetworks = interopHandle.GetSocialNetworks();
        }

        public UInt64 XboxUserId { get; }

        public bool IsFavourite { get; }

        public bool IsFriend 
        {
            get
            {
                // Version check only in debug/development builds
#if DEBUG
                if (!s_isFriendWarningShown && SDK.GetGdkEdition() < 241000)
                {
                    Debug.LogWarning(
                        "XblSocialRelationship.IsFriend property is only supported for SDK.GDKEdition() >= 241000. " +
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

        public bool IsFollowingCaller { get; }

        public string[] SocialNetworks { get; }

#if DEBUG
        // Static flag only exists in none release builds
        private static bool s_isFriendWarningShown = false;
#endif

        private bool m_isFriend;
    }
}   
