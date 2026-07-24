// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XPersistentLocalStorageSpaceInfo
    {
        internal XPersistentLocalStorageSpaceInfo(Interop.XPersistentLocalStorageSpaceInfo interop)
        {
            this.interop = interop;
        }

        public XPersistentLocalStorageSpaceInfo()
        {
            this.interop = new Interop.XPersistentLocalStorageSpaceInfo();
        }

        internal Interop.XPersistentLocalStorageSpaceInfo interop;

        // Bytes that are available to be written to PLS.
        public UInt64 AvailableFreeBytes
        {
            get => this.interop.availableFreeBytes;
            set => this.interop.availableFreeBytes = value;
        }

        // Bytes left in PLS allocation. May require prompting user to free up
        // space to make these bytes available.
        public UInt64 TotalFreeBytes
        {
            get => this.interop.totalFreeBytes;
            set => this.interop.totalFreeBytes = value;
        }

        // Bytes already used in PLS.
        public UInt64 UsedBytes
        {
            get => this.interop.usedBytes;
            set => this.interop.usedBytes = value;
        }

        // Maximum total bytes that can be stored in PLS.
        public UInt64 TotalBytes
        {
            get => this.interop.totalBytes;
            set => this.interop.totalBytes = value;
        }
    }

    partial class SDK
    {
        public static Int32 XPersistentLocalStorageGetPathSize(out ulong pathSize)
        {
            return NativeMethods.XPersistentLocalStorageGetPathSize(out pathSize);
        }

        public static Int32 XPersistentLocalStorageGetPath(ulong pathSize,
            out string path)
        {
            path = null;

            UInt64 pathUsed = 0;
            byte[] pathChars = new byte[pathSize];
            int hr = NativeMethods.XPersistentLocalStorageGetPath(pathSize,
                pathChars,
                out pathUsed);
            if (HR.SUCCEEDED(hr))
            {
                path = Encoding.UTF8.GetString(pathChars, 0, (int)pathUsed).TrimEnd('\0'); // remove null terminator
            }

            return hr;
        }

        public static Int32 XPersistentLocalStorageGetSpaceInfo(out XPersistentLocalStorageSpaceInfo spaceInfo)
        {
            spaceInfo = default;
            Interop.XPersistentLocalStorageSpaceInfo interopSpaceInfo = default;

            Int32 hr = NativeMethods.XPersistentLocalStorageGetSpaceInfo(out interopSpaceInfo);

            if(HR.SUCCEEDED(hr))
            {
                spaceInfo = new XPersistentLocalStorageSpaceInfo(interopSpaceInfo);
            }

            return hr;
        }

        public static Int32 XPersistentLocalStoragePromptUserForSpaceAsync(ulong requestedBytes,
            XAsyncBlock asyncBlock)
        {
            return NativeMethods.XPersistentLocalStoragePromptUserForSpaceAsync(requestedBytes, asyncBlock.InteropPtr);
        }

        public static Int32 XPersistentLocalStoragePromptUserForSpaceResult(XAsyncBlock asyncBlock)
        {
            return NativeMethods.XPersistentLocalStoragePromptUserForSpaceResult(asyncBlock.InteropPtr);
        }
    }
}