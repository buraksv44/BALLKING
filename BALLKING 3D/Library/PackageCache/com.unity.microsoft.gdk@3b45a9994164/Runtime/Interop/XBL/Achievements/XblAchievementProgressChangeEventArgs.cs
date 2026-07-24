using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblAchievementProgressChangeEventArgs
    //{
    //    XblAchievementProgressChangeEntry* updatedAchievementEntries;
    //    size_t entryCount;
    //}
    [StructLayout(LayoutKind.Sequential)]
    internal struct XblAchievementProgressChangeEventArgs
    {
        internal readonly IntPtr updatedAchievementEntries;
        internal readonly uint entryCount;
    }
}
