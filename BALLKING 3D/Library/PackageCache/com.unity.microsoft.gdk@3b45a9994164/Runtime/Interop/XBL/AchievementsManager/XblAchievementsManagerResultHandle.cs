using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    /// <summary>
    /// A handle to an achievement manager result.
    /// </summary>
    /// <remarks>
    /// This handle is used by other APIs to get the achievement objects and to get the
    /// next page of achievements from the service if there is one.
    /// The handle must be closed using <see cref="XblAchievementsManagerResultCloseHandle"/>
    /// when the result is no longer needed.
    /// </remarks>
    /// <memof><see cref="XblAchievementsManagerResultGetAchievements"/></memof>
    /// <memof><see cref="XblAchievementsManagerResultDuplicateHandle"/></memof>
    /// <memof><see cref="XblAchievementsManagerResultCloseHandle"/></memof>
    //typedef struct XblAchievementsManagerResult* XblAchievementsManagerResultHandle;

    [StructLayout(LayoutKind.Sequential)]
    internal struct XblAchievementsManagerResultHandle
    {
        internal readonly IntPtr Ptr;
    }
}