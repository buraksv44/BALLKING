using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    // enum class XClosedCaptionFontEdgeAttribute : uint32_t
    // {
    //     Default = 0,
    //     NoEdgeAttribute = 1,
    //     RaisedEdges = 2,
    //     DepressedEdges = 3,
    //     UniformedEdges = 4,
    //     DropShadowedEdges= 5
    // };
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XClosedCaptionFontEdgeAttribute : UInt32
    {
        Default = 0,
        NoEdgeAttribute = 1,
        RaisedEdges = 2,
        DepressedEdges = 3,
        UniformedEdges = 4,
        DropShadowedEdges = 5
    }

    // enum class XClosedCaptionFontStyle : uint32_t
    // {
    //     Default = 0,
    //     MonospacedWithSerifs = 1,
    //     ProportionalWithSerifs = 2,
    //     MonospacedWithoutSerifs = 3,
    //     ProportionalWithoutSerifs = 4,
    //     Casual = 5,
    //     Cursive = 6,
    //     SmallCapitals = 7
    // };
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XClosedCaptionFontStyle : UInt32
    {
        Default = 0,
        MonospacedWithSerifs = 1,
        ProportionalWithSerifs = 2,
        MonospacedWithoutSerifs = 3,
        ProportionalWithoutSerifs = 4,
        Casual = 5,
        Cursive = 6,
        SmallCapitals = 7
    };

    // enum class XHighContrastMode : uint32_t
    // {
    //     Off = 0,
    //     Dark = 1,
    //     Light = 2,
    //     Other = 3,
    // };
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XHighContrastMode : UInt32
    {
        Off = 0,
        Dark = 1,
        Light = 2,
        Other = 3,
    };

    // enum class XSpeechToTextPositionHint : uint32_t
    // {
    //     BottomCenter = 0,
    //     BottomLeft = 1,
    //     BottomRight = 2,
    //     MiddleRight = 3,
    //     MiddleLeft = 4,
    //     TopCenter = 5,
    //     TopLeft = 6,
    //     TopRight = 7
    // };
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XSpeechToTextPositionHint : UInt32
    {
        BottomCenter = 0,
        BottomLeft = 1,
        BottomRight = 2,
        MiddleRight = 3,
        MiddleLeft = 4,
        TopCenter = 5,
        TopLeft = 6,
        TopRight = 7
    };

    // enum class XSpeechToTextType : uint32_t
    // {
    //     Voice = 0,
    //     Text = 1
    // };
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XSpeechToTextType : UInt32
    {
        Voice = 0,
        Text = 1
    };

    public class ARGB
    {
        internal ARGB(Interop.ARGB interopARGB)
        {
            this.interop = interopARGB;
        }

        public ARGB()
        {
            this.interop = new Interop.ARGB();
        }

        internal Interop.ARGB interop;

        public byte A
        {
            get => interop.A;
            set => interop.A = value;
        }

        public byte R
        {
            get => interop.R;
            set => interop.R = value;
        }

        public byte G
        {
            get => interop.G;
            set => interop.G = value;
        }

        public byte B
        {
            get => interop.B;
            set => interop.B = value;
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XColor
    {
        internal XColor(Interop.XColor interop)
        {
            this._argb = new ARGB(interop.Argb);
            this._interop = interop;
        }

        public XColor()
        {
            this._interop = new Interop.XColor();
        }

        internal Interop.XColor _interop;
        internal ARGB _argb;

        internal Interop.XColor interop
        {
            get
            {
                this._interop.Argb = this._argb.interop;
                return this._interop;
            }
        }

        public ARGB Argb
        {
            get => _argb;
            set => _argb = value;
        }

        public UInt32 Value
        {
            get => _interop.Value;
            set => _interop.Value = value;
        }
    };

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XClosedCaptionProperties
    {
        internal XClosedCaptionProperties(Interop.XClosedCaptionProperties interop)
        {
            BackgroundColor = new XColor(interop.BackgroundColor);
            FontColor = new XColor(interop.FontColor);
            WindowColor = new XColor(interop.WindowColor);
            this._interop = interop;
        }

        public XClosedCaptionProperties()
        {
            this._interop = new Interop.XClosedCaptionProperties();
        }

        internal Interop.XClosedCaptionProperties _interop;
        internal XColor _backgroundColor;
        internal XColor _fontColor;
        internal XColor _windowColor;

        internal Interop.XClosedCaptionProperties interop
        {
            get
            {
                this._interop.BackgroundColor = this._backgroundColor.interop;
                this._interop.FontColor = this._fontColor.interop;
                this._interop.WindowColor = this._windowColor.interop;
                return this._interop;
            }
        }

        public XColor BackgroundColor
        {
            get => _backgroundColor;
            set => _backgroundColor = value;
        }

        public XColor FontColor
        {
            get => _fontColor;
            set => _fontColor = value;
        }

        public XColor WindowColor
        {
            get => _windowColor;
            set => _windowColor = value;
        }

        public XClosedCaptionFontEdgeAttribute FontEdgeAttribute
        {
            get => _interop.FontEdgeAttribute;
            set => _interop.FontEdgeAttribute = value;
        }

        public XClosedCaptionFontStyle FontStyle
        {
            get => _interop.FontStyle;
            set => _interop.FontStyle = value;
        }

        public float FontScale
        {
            get => _interop.FontScale;
            set => _interop.FontScale = value;
        }

        public bool Enabled
        {
            get => _interop.Enabled;
            set => _interop.Enabled = value;
        }
    };

    partial class SDK
    {
        public static Int32 XClosedCaptionGetProperties(out XClosedCaptionProperties properties)
        {
            properties = default;
            Interop.XClosedCaptionProperties interopProperties = default;

            Int32 hr = NativeMethods.XClosedCaptionGetProperties(out interopProperties);

            if(HR.SUCCEEDED(hr))
            {
                properties = new XClosedCaptionProperties(interopProperties);
            }

            return hr;
        }

        public static Int32 XClosedCaptionSetEnabled(bool enabled)
        {
            return NativeMethods.XClosedCaptionSetEnabled(enabled);
        }

        public static Int32 XHighContrastGetMode(out XHighContrastMode mode)
        {
            return NativeMethods.XHighContrastGetMode(out mode);
        }

        public static Int32 XSpeechToTextSetPositionHint(XSpeechToTextPositionHint position)
        {
            return NativeMethods.XSpeechToTextSetPositionHint(position);
        }

        public static Int32 XSpeechToTextSendString(string speakerName, string content, XSpeechToTextType type)
        {
            return NativeMethods.XSpeechToTextSendString(speakerName, content, type);
        }

        public static Int32 XSpeechToTextBeginHypothesisString(string speakerName,
            string content,
            XSpeechToTextType type,
            out UInt32 hypothesisId)
        {
            return NativeMethods.XSpeechToTextBeginHypothesisString(speakerName, content, type, out hypothesisId);
        }

        public static Int32 XSpeechToTextUpdateHypothesisString(UInt32 hypothesisId, string content)
        {
            return NativeMethods.XSpeechToTextUpdateHypothesisString(hypothesisId, content);
        }

        public static Int32 XSpeechToTextFinalizeHypothesisString(UInt32 hypothesisId, string content)
        {
            return NativeMethods.XSpeechToTextFinalizeHypothesisString(hypothesisId, content);
        }

        public static Int32 XSpeechToTextCancelHypothesisString(UInt32 hypothesisId)
        {
            return NativeMethods.XSpeechToTextCancelHypothesisString(hypothesisId);
        }
    }
}