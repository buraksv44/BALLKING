using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Unity.XGamingRuntime.Interop
{
    //typedef struct XblTitleStorageBlobMetadata
    //{
    //    _Null_terminated_ char blobPath[XBL_TITLE_STORAGE_BLOB_PATH_MAX_LENGTH];
    //    XblTitleStorageBlobType blobType;
    //    XblTitleStorageType storageType;
    //    _Null_terminated_ char displayName[XBL_TITLE_STORAGE_BLOB_DISPLAY_NAME_MAX_LENGTH];
    //    _Null_terminated_ char eTag[XBL_TITLE_STORAGE_BLOB_ETAG_MAX_LENGTH];
    //    time_t clientTimestamp;
    //    size_t length;
    //    _Null_terminated_ char serviceConfigurationId[XBL_SCID_LENGTH];
    //    uint64_t xboxUserId;
    //}
    //XblTitleStorageBlobMetadata;

    [StructLayout(LayoutKind.Sequential)]
    internal struct XblTitleStorageBlobMetadata
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = XblInterop.XBL_TITLE_STORAGE_BLOB_PATH_MAX_LENGTH)]
        internal string blobPath;
        internal XblTitleStorageBlobType blobType;
        internal XblTitleStorageType storageType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = XblInterop.XBL_TITLE_STORAGE_BLOB_DISPLAY_NAME_MAX_LENGTH)]
        internal string displayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = XblInterop.XBL_TITLE_STORAGE_BLOB_ETAG_MAX_LENGTH)]
        internal string eTag;
        internal TimeT clientTimestamp;
        internal SizeT length;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = XblInterop.XBL_SCID_LENGTH)] internal string serviceConfigurationId;
        internal UInt64 xboxUserId;
    }
}
