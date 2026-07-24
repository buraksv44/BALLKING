using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Unity.XGamingRuntime.Interop
{
    public static class InteropHelpers
    {
        public static IntPtr MarshalStringUtf8(string str)
        {
            byte[] strBytes = Encoding.UTF8.GetBytes(str);
            IntPtr strPtr = Marshal.AllocCoTaskMem(strBytes.Length + 1); // +1 for null terminator
            Marshal.Copy(strBytes, 0, strPtr, strBytes.Length);
            Marshal.WriteByte(strPtr, strBytes.Length, 0); // Ensure null terminator is written
            return strPtr;
        }

        public static U[] MarshalArray<T, U>(IntPtr ptr, uint count, Func<T, U> converter)
            where T : struct
        {
            IntPtr curPtr = ptr;
            U[] results = new U[count];
            for (uint i = 0; i < count; i++)
            {
                T interop = (T)Marshal.PtrToStructure(curPtr, typeof(T));
                results[i] = converter(interop);
                curPtr += Marshal.SizeOf(typeof(T));
            }

            return results;
        }

        public static T[] MarshalArray<T>(IntPtr ptr, uint count)
            where T : struct
        {
            IntPtr curPtr = ptr;
            T[] results = new T[count];
            for (uint i = 0; i < count; i++)
            {
                results[i] = (T)Marshal.PtrToStructure(curPtr, typeof(T));
                curPtr += Marshal.SizeOf(typeof(T));
            }

            return results;
        }

        public static string[] MarshalStringArrayAnsi(IntPtr ptr, uint count)
        {
            IntPtr curPtr = ptr;
            string[] results = new string[count];
            for (uint i = 0; i < count; i++)
            {
                IntPtr dataPtr = (IntPtr)Marshal.PtrToStructure(curPtr, typeof(IntPtr));
                results[i] = Marshal.PtrToStringAnsi(dataPtr);
                curPtr += Marshal.SizeOf(typeof(IntPtr));
            }

            return results;
        }
    }
}
