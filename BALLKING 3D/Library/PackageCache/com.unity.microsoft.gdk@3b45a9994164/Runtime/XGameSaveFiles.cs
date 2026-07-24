using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.CSharp;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    partial class SDK
    {

        public static Int32 XGameSaveFilesGetFolderWithUiAsync(XUserHandle requestingUser,
            string configurationId,
            XAsyncBlock async)
        {
            IntPtr userHandle = (requestingUser != null) ? requestingUser.Handle : IntPtr.Zero;

            return NativeMethods.XGameSaveFilesGetFolderWithUiAsync(userHandle, configurationId, async.InteropPtr);
        }

        public static Int32 XGameSaveFilesGetFolderWithUiResult(XAsyncBlock async,
            UInt64 folderSize,
            out string folderResult)
        {
            folderResult = null;

            StringBuilder folderResultSB = new StringBuilder((int)folderSize);
            int hr = NativeMethods.XGameSaveFilesGetFolderWithUiResult(async.InteropPtr, folderSize, folderResultSB);
            if (HR.SUCCEEDED(hr))
            {
                folderResult = folderResultSB.ToString();
            }

            return hr;
        }

        [Obsolete("Please use XGameSaveFilesGetRemainingQuota(XUserHandle, string, out UInt64) instead", false)]
        public static Int32 XGameSaveFilesGetRemainingQuota(XUserHandle userContext,
            string configurationId,
            out Int64 remainingQuota)
        {
            UInt64 uint64RemainingQuota;
            Int32 returnVal = NativeMethods.XGameSaveFilesGetRemainingQuota(userContext.Handle, configurationId, out uint64RemainingQuota);
            remainingQuota = (Int64)uint64RemainingQuota;

            return returnVal;
        }

        public static Int32 XGameSaveFilesGetRemainingQuota(XUserHandle userContext,
            string configurationId,
            out UInt64 remainingQuota)
        {
            IntPtr userHandle = (userContext != null) ? userContext.Handle : IntPtr.Zero;

            return NativeMethods.XGameSaveFilesGetRemainingQuota(userHandle, configurationId, out remainingQuota);
        }
    }
}