using System;
using System.Runtime.CompilerServices;
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
    public enum XSpeechSynthesizerVoiceGender : UInt32
    {
        Female = 0,
        Male = 1,
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XSpeechSynthesizerVoiceInformation
    {
        internal XSpeechSynthesizerVoiceInformation(Interop.XSpeechSynthesizerVoiceInformation interop)
        {
            this.interop = interop;
        }

        public XSpeechSynthesizerVoiceInformation()
        {
            this.interop = new Interop.XSpeechSynthesizerVoiceInformation();
        }

        internal Interop.XSpeechSynthesizerVoiceInformation interop;

        public string Description
        {
            get => this.interop.Description;
            set => this.interop.Description = value;
        }

        public string DisplayName
        {
            get => this.interop.DisplayName;
            set => this.interop.DisplayName = value;
        }

        public XSpeechSynthesizerVoiceGender Gender
        {
            get => this.interop.Gender;
            set => this.interop.Gender = value;
        }

        public string VoiceId
        {
            get => this.interop.VoiceId;
            set => this.interop.VoiceId = value;
        }

        public string Language
        {
            get => this.interop.Language;
            set => this.interop.Language = value;
        }
    };

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XSpeechSynthesizerHandle : EquatableHandle
    {
        public XSpeechSynthesizerHandle(IntPtr handle) :
            base(IntPtr.Zero, true, handle)
        { }

        public int CloseResult { get; private set; }

        protected override bool ReleaseHandle()
        {
            this.CloseResult = NativeMethods.XSpeechSynthesizerCloseHandle(this.handle);
            SetHandle(IntPtr.Zero);
            return HR.SUCCEEDED(this.CloseResult);
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
    public class XSpeechSynthesizerStreamHandle : EquatableHandle
    {
        public XSpeechSynthesizerStreamHandle(IntPtr handle) :
            base(IntPtr.Zero, true, handle)
        { }

        public int CloseResult { get; private set; }

        protected override bool ReleaseHandle()
        {
            this.CloseResult = NativeMethods.XSpeechSynthesizerCloseStreamHandle(this.handle);
            SetHandle(IntPtr.Zero);
            return HR.SUCCEEDED(this.CloseResult);
        }

        public override bool IsInvalid
        {
            get { return this.handle == IntPtr.Zero; }
        }
    }

    public delegate bool XSpeechSynthesizerInstalledVoicesCallback(
        [In] ref XSpeechSynthesizerVoiceInformation information,
        IntPtr context);

    partial class SDK
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XSpeechSynthesizerInstalledVoicesCallback))]
        private static bool OnSpeechSynthesizerInstalledVoicesCallback(ref Interop.XSpeechSynthesizerVoiceInformation information, IntPtr context)
        {
            GCHandle handle = GCHandle.FromIntPtr(context);
            var wrapper = handle.Target as CallbackWrapper<Interop.XSpeechSynthesizerInstalledVoicesCallback>;
            return wrapper.Callback(ref information, context);
        }

        public static int XSpeechSynthesizerEnumerateInstalledVoices(IntPtr context, XSpeechSynthesizerInstalledVoicesCallback callback)
        {
            Interop.XSpeechSynthesizerInstalledVoicesCallback interopCallback = (ref Interop.XSpeechSynthesizerVoiceInformation _information, IntPtr _context) =>
            {
                XSpeechSynthesizerVoiceInformation info = new XSpeechSynthesizerVoiceInformation(_information);
                return callback(ref info, _context);
            };

            using (var wrapper = new CallbackWrapper<Interop.XSpeechSynthesizerInstalledVoicesCallback>(interopCallback, context, OnSpeechSynthesizerInstalledVoicesCallback))
            {
                return NativeMethods.XSpeechSynthesizerEnumerateInstalledVoices(wrapper.CallbackContext, wrapper.StaticCallback);
            }
        }

        public static int XSpeechSynthesizerCreate(out XSpeechSynthesizerHandle speechSynthesizer)
        {
            speechSynthesizer = null;

            IntPtr handle;
            int hr = NativeMethods.XSpeechSynthesizerCreate(out handle);
            if (HR.SUCCEEDED(hr))
            {
                speechSynthesizer = new XSpeechSynthesizerHandle(handle);
            }

            return hr;
        }

        public static int XSpeechSynthesizerCloseHandle(XSpeechSynthesizerHandle speechSynthesizer)
        {
            speechSynthesizer.Close();
            return speechSynthesizer.CloseResult;
        }

        public static int XSpeechSynthesizerSetDefaultVoice(XSpeechSynthesizerHandle speechSynthesizer)
        {
            return NativeMethods.XSpeechSynthesizerSetDefaultVoice(speechSynthesizer.Handle);
        }

        public static int XSpeechSynthesizerSetCustomVoice(XSpeechSynthesizerHandle speechSynthesizer, string voiceId)
        {
            return NativeMethods.XSpeechSynthesizerSetCustomVoice(speechSynthesizer.Handle, voiceId);
        }

        public static int XSpeechSynthesizerCreateStreamFromText(XSpeechSynthesizerHandle speechSynthesizer,
            string text,
            out XSpeechSynthesizerStreamHandle speechSynthesisStream)
        {
            speechSynthesisStream = null;

            IntPtr handle;
            int hr = NativeMethods.XSpeechSynthesizerCreateStreamFromText(speechSynthesizer.Handle,
                text,
                out handle);
            if (HR.SUCCEEDED(hr))
            {
                speechSynthesisStream = new XSpeechSynthesizerStreamHandle(handle);
            }

            return hr;
        }

        public static int XSpeechSynthesizerCreateStreamFromSsml(XSpeechSynthesizerHandle speechSynthesizer,
            string ssml,
            out XSpeechSynthesizerStreamHandle speechSynthesisStream)
        {
            speechSynthesisStream = null;

            IntPtr handle;
            int hr = NativeMethods.XSpeechSynthesizerCreateStreamFromSsml(speechSynthesizer.Handle,
                ssml,
                out handle);
            if (HR.SUCCEEDED(hr))
            {
                speechSynthesisStream = new XSpeechSynthesizerStreamHandle(handle);
            }

            return hr;
        }

        public static int XSpeechSynthesizerCloseStreamHandle(XSpeechSynthesizerStreamHandle speechSynthesisStream)
        {
            speechSynthesisStream.Close();
            return speechSynthesisStream.CloseResult;
        }

        public static int XSpeechSynthesizerGetStreamDataSize(XSpeechSynthesizerStreamHandle speechSynthesisStream, out UInt64 bufferSize)
        {
            return NativeMethods.XSpeechSynthesizerGetStreamDataSize(speechSynthesisStream.Handle, out bufferSize);
        }

        public static int XSpeechSynthesizerGetStreamData(XSpeechSynthesizerStreamHandle speechSynthesisStream, byte[] buffer, out UInt64 bufferUsed)
        {
            return NativeMethods.XSpeechSynthesizerGetStreamData(speechSynthesisStream.Handle,
                (ulong)buffer.Length,
                buffer,
                out bufferUsed);
        }
    }
}