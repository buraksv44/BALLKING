using System;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    [Obsolete("XRegistrationToken has been removed. " +
        "If you are upgrading from a legacy package, please refer to the migration guide.", true)]
    public class XRegistrationToken
    { }

    public abstract class XRegistrationToken<T> : CallbackWrapper<T> where T : Delegate
    {
        public UInt64 Token;

        protected XRegistrationToken(T callback, IntPtr context, T staticCallback) :
            base(callback, context, staticCallback)
        {
        }

        public bool IsValid { get { return this.Token != 0;} }

        protected override void Dispose(bool disposing)
        {
            if (this.Token != 0)
            {
                DisposeInternal(disposing);
                this.Token = 0;
            }

            base.Dispose(disposing);
        }

        protected abstract void DisposeInternal(bool disposing);
    }
}