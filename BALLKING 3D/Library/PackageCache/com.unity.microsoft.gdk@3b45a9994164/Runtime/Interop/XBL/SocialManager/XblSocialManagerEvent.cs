using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    // typedef struct XblSocialManagerEvent
    // {
    //     XblUserHandle user;
    //     XblSocialManagerEventType eventType;
    //     HRESULT hr;
    //     XblSocialManagerUserGroupHandle groupAffected;
    //     XblSocialManagerUser* usersAffected[XBL_SOCIAL_MANAGER_MAX_AFFECTED_USERS_PER_EVENT];
    // } XblSocialManagerEvent;

    [StructLayout(LayoutKind.Sequential)]
    internal struct XblSocialManagerEvent
    {
        internal readonly IntPtr user;
        internal readonly XblSocialManagerEventType eventType;
        internal readonly Int32 hr;
        internal readonly XblSocialManagerUserGroupHandle loadedGroup;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = XblInterop.XBL_SOCIAL_MANAGER_MAX_AFFECTED_USERS_PER_EVENT)]
        internal readonly IntPtr[] usersAffected;

        private T[] GetUserArrayGeneric<T>()
        {
            var ret = new List<T>();
            foreach (var structPtr in usersAffected)
            {
                if (structPtr != IntPtr.Zero)
                    ret.Add((T)Marshal.PtrToStructure(structPtr, typeof(T)));
                else
                    break;
            }
            return ret.ToArray();
        }

        internal XblSocialManagerUser[] GetUserArray() => GetUserArrayGeneric<XblSocialManagerUser>();
        internal XblSocialManagerUser2[] GetUser2Array() => GetUserArrayGeneric<XblSocialManagerUser2>();
    }
}
