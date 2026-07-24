using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblSocialRelationshipResult : IDisposable
    {
        internal XblSocialRelationshipResult(Interop.XblSocialRelationshipResultHandle interopHandle)
        {
            this.InteropHandle = interopHandle;
        }

        ~XblSocialRelationshipResult()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            // make sure we close this handle
            XblInterop.XblSocialRelationshipResultCloseHandle(InteropHandle);
            this.InteropHandle = default(Interop.XblSocialRelationshipResultHandle);

            _disposed = true;
        }

        internal Interop.XblSocialRelationshipResultHandle InteropHandle { get; set; }

        // To detect redundant calls
        private bool _disposed = false;
    }
}
