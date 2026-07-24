// Copyright (c) Microsoft Corporation. All rights reserved.

using System;

namespace Unity.XGamingRuntime.Interop
{
    public class HR
    {
        public const Int32 S_OK = 0x00000000;
        public const Int32 E_NOTIMPL = unchecked((Int32)0x80004001);
        public const Int32 E_NOINTERFACE = unchecked((Int32)0x80004002);
        public const Int32 E_POINTER = unchecked((Int32)0x80004003);
        public const Int32 E_ABORT = unchecked((Int32)0x80004004);
        public const Int32 E_ACCESSDENIED = unchecked((Int32)0x80070005);
        public const Int32 E_OUTOFMEMORY = unchecked((Int32)0x8007000E);
        public const Int32 E_INVALIDARG = unchecked((Int32)0x80070057);
        public const Int32 E_PENDING = unchecked((Int32)0x8000000A);
        public const Int32 E_UNEXPECTED = unchecked((Int32)0x8000FFFF);
        public const Int32 E_NOT_SUPPORTED = unchecked((Int32)0x80070032);
        public const Int32 E_TIME_CRITICAL_THREAD = unchecked((Int32)0x800701A0);
        public const Int32 E_NO_TASK_QUEUE = unchecked((Int32)0x800701AB);
        public const Int32 E_NOT_SUFFICIENT_BUFFER = unchecked((Int32)0x8007007A);
        public const Int32 E_BOUNDS = unchecked((Int32)0x8000000B);

        public static bool SUCCEEDED(Int32 hr)
        {
            return hr >= 0;
        }

        public static bool FAILED(Int32 hr)
        {
            return hr < 0;
        }
    }
}