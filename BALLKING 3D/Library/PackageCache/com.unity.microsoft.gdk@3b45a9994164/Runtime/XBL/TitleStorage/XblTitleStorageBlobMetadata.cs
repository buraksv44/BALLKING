using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblTitleStorageBlobMetadata
    {
        internal Interop.XblTitleStorageBlobMetadata interop;

        internal XblTitleStorageBlobMetadata(Interop.XblTitleStorageBlobMetadata interopHandle)
        {
            this.interop = interopHandle;
        }

        public XblTitleStorageBlobMetadata()
        {
            this.interop = new Interop.XblTitleStorageBlobMetadata();
        }

        public string BlobPath
        {
            get => this.interop.blobPath;
            set => this.interop.blobPath = value;
        }
        public XblTitleStorageBlobType BlobType
        {
            get => this.interop.blobType;
            set => this.interop.blobType = value;
        }
        public XblTitleStorageType StorageType
        {
            get => this.interop.storageType;
            set => this.interop.storageType = value;
        }
        public string DisplayName
        {
            get => this.interop.displayName;
            set => this.interop.displayName = value;
        }
        public string ETag
        {
            get => this.interop.eTag;
            set => this.interop.eTag = value;
        }
        public DateTime ClientTimestamp
        {
            get => this.interop.clientTimestamp.DateTime;
            set => this.interop.clientTimestamp = new Interop.TimeT(value);
        }
        public UInt64 Length
        {
            get => this.interop.length.ToUInt64();
            set => this.interop.length = new Interop.SizeT(value);
        }
        public string ServiceConfigurationId
        {
            get => this.interop.serviceConfigurationId;
            set => this.interop.serviceConfigurationId = value;
        }
        public UInt64 XboxUserId
        {
            get => this.interop.xboxUserId;
            set => this.interop.xboxUserId = value;
        }
    }
}
