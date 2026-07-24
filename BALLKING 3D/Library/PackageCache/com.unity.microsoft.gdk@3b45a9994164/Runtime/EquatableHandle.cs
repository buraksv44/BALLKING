// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime
{
    public abstract class EquatableHandle : SafeHandle
    {
        public EquatableHandle(IntPtr invalidHandleValue, bool ownsHandle, IntPtr handle) :
            base(invalidHandleValue, ownsHandle)
        {
            SetHandle(handle);
        }

        public IntPtr Handle { get { return this.handle; } }

        public override bool Equals(object obj)
        {
            if (obj is EquatableHandle)
            {
                EquatableHandle equatableHandle = (EquatableHandle)obj;
                return this.handle == equatableHandle.handle;
            }
            return false;
        }
        public override int GetHashCode() { return this.handle.GetHashCode(); }
        public static bool operator ==(EquatableHandle handle1, EquatableHandle handle2) { return object.ReferenceEquals(handle1, null) ? object.ReferenceEquals(handle2, null) : handle1.Equals(handle2); }
        public static bool operator !=(EquatableHandle handle1, EquatableHandle handle2) { return !(handle1 == handle2); }
    }
}
