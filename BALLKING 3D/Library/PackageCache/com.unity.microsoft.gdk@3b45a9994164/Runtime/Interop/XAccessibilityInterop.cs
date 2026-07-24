using System;
using System.Runtime.InteropServices;

namespace Unity.XGamingRuntime.Interop
{
    // struct XColor
    // {
    //     union
    //     {
    //         struct
    //         {
    //             uint8_t A;
    //             uint8_t R;
    //             uint8_t G;
    //             uint8_t B;
    //         };
    //         uint32_t Value;
    //     };
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct ARGB
    {
        internal byte A;

        internal byte R;

        internal byte G;

        internal byte B;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct XColor
    {
        [FieldOffset(0)]
        internal ARGB Argb;

        [FieldOffset(0)]
        internal UInt32 Value;
    };

    // struct XClosedCaptionProperties
    // {
    //     XColor BackgroundColor;
    //     XColor FontColor;
    //     XColor WindowColor;
    //     XClosedCaptionFontEdgeAttribute FontEdgeAttribute;
    //     XClosedCaptionFontStyle FontStyle;
    //     float FontScale;
    //     bool Enabled;
    // };
    [StructLayout(LayoutKind.Sequential)]
    internal struct XClosedCaptionProperties
    {
        internal XColor BackgroundColor;
        internal XColor FontColor;
        internal XColor WindowColor;
        internal XClosedCaptionFontEdgeAttribute FontEdgeAttribute;
        internal XClosedCaptionFontStyle FontStyle;
        internal float FontScale;
        [MarshalAs(UnmanagedType.I1)] internal bool Enabled;
    };

    partial class NativeMethods
    {
        // STDAPI XClosedCaptionGetProperties(_Out_ XClosedCaptionProperties* properties) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XClosedCaptionGetProperties(out XClosedCaptionProperties properties);

        // STDAPI XClosedCaptionSetEnabled(_In_ bool enabled) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XClosedCaptionSetEnabled([MarshalAs(UnmanagedType.I1)] bool enabled);

        // STDAPI XHighContrastGetMode(_Out_ XHighContrastMode* mode) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XHighContrastGetMode(out XHighContrastMode mode);

        // STDAPI XSpeechToTextSetPositionHint(_In_ XSpeechToTextPositionHint position) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSpeechToTextSetPositionHint(XSpeechToTextPositionHint position);

        // STDAPI XSpeechToTextSendString(
        //   _In_z_ const char* speakerName,
        //   _In_z_ const char* content,
        //   _In_ XSpeechToTextType type) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSpeechToTextSendString([MarshalAs(UnmanagedType.LPStr)] string speakerName,
            [MarshalAs(UnmanagedType.LPStr)] string content,
            XSpeechToTextType type);

        //STDAPI XSpeechToTextBeginHypothesisString(
        //  _In_z_ const char* speakerName,
        //  _In_z_ const char* content,
        //  _In_ XSpeechToTextType type,
        //  _Out_ uint32_t* hypothesisId) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSpeechToTextBeginHypothesisString([MarshalAs(UnmanagedType.LPStr)] string speakerName,
            [MarshalAs(UnmanagedType.LPStr)] string content,
            XSpeechToTextType type,
            out UInt32 hypothesisId);

        //STDAPI XSpeechToTextUpdateHypothesisString(
        // _In_ uint32_t hypothesisId,
        // _In_z_ const char* content) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSpeechToTextUpdateHypothesisString(UInt32 hypothesisId, [MarshalAs(UnmanagedType.LPStr)] string content);

        // STDAPI XSpeechToTextFinalizeHypothesisString(
        //   _In_ uint32_t hypothesisId,
        //   _In_z_ const char* content) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSpeechToTextFinalizeHypothesisString(UInt32 hypothesisId,
            [MarshalAs(UnmanagedType.LPStr)] string content);

        //STDAPI XSpeechToTextCancelHypothesisString(
        //   _In_ uint32_t hypothesisId) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XSpeechToTextCancelHypothesisString(UInt32 hypothesisId);
    }
}
