using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime.Interop
{
    [StructLayout(LayoutKind.Sequential)]
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime.Interop")]
#else
    [MovedFrom("Unity.GameCore.Interop")]
#endif
    public struct SizeT
    {
        public bool IsZero { get => value == UIntPtr.Zero; }

        public SizeT(Int32 length)
        {
            this.value = new UIntPtr(Convert.ToUInt64(length));
        }

        public SizeT(UInt32 length)
        {
            this.value = new UIntPtr(Convert.ToUInt64(length));
        }

        public SizeT(Int64 length)
        {
            this.value = new UIntPtr(Convert.ToUInt64(length));
        }

        public SizeT(UInt64 length)
        {
            this.value = new UIntPtr(length);
        }

        public UInt32 ToUInt32()
        {
            return Convert.ToUInt32(value.ToUInt64());
        }

        public Int32 ToInt32()
        {
            return Convert.ToInt32(value.ToUInt64());
        }

        public UInt64 ToUInt64()
        {
            return value.ToUInt64();
        }

        public Int64 ToInt64()
        {
            return Convert.ToInt64(value.ToUInt64());
        }

        private readonly UIntPtr value;
    }
}
