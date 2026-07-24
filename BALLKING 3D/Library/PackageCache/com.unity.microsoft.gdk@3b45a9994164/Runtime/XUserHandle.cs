// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XUserHandle : EquatableHandle
    {
        internal XUserHandle(IntPtr interopHandle) :
            base(IntPtr.Zero, true, interopHandle)
        {
        }

        internal XUserHandle(IntPtr interopHandle, bool ownsHandle) :
            base(IntPtr.Zero, ownsHandle, interopHandle)
        {
        }

        internal static Int32 WrapAndReturnHResult(Int32 hresult, IntPtr interopHandle, out XUserHandle handle)
        {
            // MSFT:21489553: Some underlying APIs have a bug where invalid ids return S_OK
            // but a null XUserHandle; don't wrap a null interopHandle in the public XUserHandle.
            if (Interop.HR.SUCCEEDED(hresult) && interopHandle != IntPtr.Zero)
            {
                handle = new XUserHandle(interopHandle);
            }
            else
            {
                handle = null;
            }
            return hresult;
        }

        protected override bool ReleaseHandle()
        {
            NativeMethods.XUserCloseHandle(this.handle);
            SetHandle(IntPtr.Zero);
            return true;
        }

        public override bool IsInvalid
        {
            get { return this.handle == IntPtr.Zero; }
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XUserSignOutDeferralHandle : EquatableHandle
    {
        public XUserSignOutDeferralHandle(IntPtr interopHandle) :
            base(IntPtr.Zero, true, interopHandle)
        {
        }

        protected override bool ReleaseHandle()
        {
            NativeMethods.XUserCloseSignOutDeferralHandle(this.handle);
            SetHandle(IntPtr.Zero);
            return true;
        }

        public override bool IsInvalid
        {
            get { return this.handle == IntPtr.Zero; }
        }
    }
}