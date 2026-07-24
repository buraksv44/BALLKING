// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Unity.XGamingRuntime.Interop
{
    //struct XSpeechSynthesizerVoiceInformation
    //{
    //    _Field_z_ const char* Description;
    //    _Field_z_ const char* DisplayName;
    //    XSpeechSynthesizerVoiceGender Gender;
    //    _Field_z_ const char* VoiceId;
    //    _Field_z_ const char* Language;
    //};
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct XSpeechSynthesizerVoiceInformation
    {
        [MarshalAs(UnmanagedType.LPStr)] internal string Description;
        [MarshalAs(UnmanagedType.LPStr)] internal string DisplayName;
        internal XSpeechSynthesizerVoiceGender Gender;
        [MarshalAs(UnmanagedType.LPStr)] internal string VoiceId;
        [MarshalAs(UnmanagedType.LPStr)] internal string Language;
    };

    //typedef bool CALLBACK XSpeechSynthesizerInstalledVoicesCallback(
    //    _In_ const XSpeechSynthesizerVoiceInformation* information,
    //    _In_ void* context
    //    );
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    internal delegate bool XSpeechSynthesizerInstalledVoicesCallback(
        [In] ref Interop.XSpeechSynthesizerVoiceInformation information,
        IntPtr context);

    partial class NativeMethods
    {
        //STDAPI XSpeechSynthesizerEnumerateInstalledVoices(
        //    _In_opt_ void* context,
        //    _In_ XSpeechSynthesizerInstalledVoicesCallback* callback
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSpeechSynthesizerEnumerateInstalledVoices(IntPtr context,
            XSpeechSynthesizerInstalledVoicesCallback callback);

        //STDAPI XSpeechSynthesizerCreate(
        //    _Out_ XSpeechSynthesizerHandle* speechSynthesizer
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSpeechSynthesizerCreate(out IntPtr speechSynthesizer);

        //STDAPI XSpeechSynthesizerCloseHandle(
        //    _In_opt_ XSpeechSynthesizerHandle speechSynthesizer
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSpeechSynthesizerCloseHandle(IntPtr speechSynthesizer);

        //STDAPI XSpeechSynthesizerSetDefaultVoice(
        //    _In_ XSpeechSynthesizerHandle speechSynthesizer
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSpeechSynthesizerSetDefaultVoice(IntPtr speechSynthesizer);

        //STDAPI XSpeechSynthesizerSetCustomVoice(
        //    _In_ XSpeechSynthesizerHandle speechSynthesizer,
        //    _In_z_ const char* voiceId
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSpeechSynthesizerSetCustomVoice(IntPtr speechSynthesizer,
            [MarshalAs(UnmanagedType.LPStr)] string voiceId);

        //STDAPI XSpeechSynthesizerCreateStreamFromText(
        //    _In_ XSpeechSynthesizerHandle speechSynthesizer,
        //    _In_z_ const char* text,
        //    _Out_ XSpeechSynthesizerStreamHandle* speechSynthesisStream
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSpeechSynthesizerCreateStreamFromText(IntPtr speechSynthesizer,
            [MarshalAs(UnmanagedType.LPStr)] string text,
            out IntPtr speechSynthesisStream);

        //STDAPI XSpeechSynthesizerCreateStreamFromSsml(
        //    _In_ XSpeechSynthesizerHandle speechSynthesizer,
        //    _In_z_ const char* ssml,
        //    _Out_ XSpeechSynthesizerStreamHandle* speechSynthesisStream
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSpeechSynthesizerCreateStreamFromSsml(IntPtr speechSynthesizer,
            [MarshalAs(UnmanagedType.LPStr)] string ssml,
            out IntPtr speechSynthesisStream);

        //STDAPI XSpeechSynthesizerCloseStreamHandle(
        //    _In_ XSpeechSynthesizerStreamHandle speechSynthesisStream
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSpeechSynthesizerCloseStreamHandle(IntPtr speechSynthesisStream);

        //STDAPI XSpeechSynthesizerGetStreamDataSize(
        //    _In_ XSpeechSynthesizerStreamHandle speechSynthesisStream,
        //    _Out_ size_t* bufferSize
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSpeechSynthesizerGetStreamDataSize(IntPtr speechSynthesisStream,
            out UInt64 bufferSize);

        //STDAPI XSpeechSynthesizerGetStreamData(
        //    _In_ XSpeechSynthesizerStreamHandle speechSynthesisStream,
        //    _In_ size_t bufferSize,
        //    _Out_writes_to_(bufferSize, *bufferUsed) void* buffer,
        //    _Out_opt_ size_t* bufferUsed
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSpeechSynthesizerGetStreamData(IntPtr speechSynthesisStream,
            UInt64 bufferSize,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] buffer,
            out UInt64 bufferUsed);
    }
}
