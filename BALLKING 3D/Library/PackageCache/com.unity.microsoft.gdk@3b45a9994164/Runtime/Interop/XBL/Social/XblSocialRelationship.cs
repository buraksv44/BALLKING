using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblSocialRelationship
    //{
    //    uint64_t xboxUserId;
    //    bool isFavorite;
    //    bool isFollowingCaller;
    //    _Field_z_ const char** socialNetworks;
    //    size_t socialNetworksCount;
    //}
    //XblSocialRelationship;

    [StructLayout(LayoutKind.Sequential)]
    internal struct XblSocialRelationship
    {
        internal string[] GetSocialNetworks() => Converters.PtrToClassArray<string, UTF8StringPtr>(this.socialNetworks, this.socialNetworksCount, s => s.GetString());

        internal readonly UInt64 xboxUserId;
        [MarshalAs(UnmanagedType.U1)]
        internal readonly bool isFavorite;
        [MarshalAs(UnmanagedType.U1)]
        internal readonly bool isFollowingCaller;
        internal readonly IntPtr socialNetworks;
        internal readonly SizeT socialNetworksCount;
    }

    // XblSocialRelationship, GDK Edition >= 241000:
    //   ADDS: bool isFriend
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblSocialRelationship2
    {
        internal string[] GetSocialNetworks() => Converters.PtrToClassArray<string, UTF8StringPtr>(this.socialNetworks, this.socialNetworksCount, s => s.GetString());

        internal readonly UInt64 xboxUserId;
        [MarshalAs(UnmanagedType.U1)]
        internal readonly bool isFavorite;
        [MarshalAs(UnmanagedType.U1)]
        internal readonly bool isFriend;
        [MarshalAs(UnmanagedType.U1)]
        internal readonly bool isFollowingCaller;
        internal readonly IntPtr socialNetworks;
        internal readonly SizeT socialNetworksCount;
    }
}