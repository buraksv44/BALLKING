using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.XGamingRuntime;

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)

namespace Unity.Microsoft.GDK.Tests
{
    public class XSpeechSynthesizerTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            GdkTestHelper.Initialize("XSpeechSynthesizerTests");
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            GdkTestHelper.Shutdown("XSpeechSynthesizerTests");
        }

        [Test]
        public void XSpeechSynthesizerEnumerateInstalledVoices()
        {
            XSpeechSynthesizerVoiceInformation[] voiceInformation;
            int hr = SDK.XSpeechSynthesizerEnumerateInstalledVoices(out voiceInformation);
            Assert.AreEqual(0, hr);
            Assert.IsNotNull(voiceInformation);
        }

        [Test]
        public void XSpeechSynthesizeText()
        {
            int hr = 0;
            hr = SDK.XSpeechSynthesizerCreate(out XSpeechSynthesizerHandle synthesizer);
            Assert.AreEqual(0, hr);
            Assert.IsNotNull(synthesizer);

            hr = SDK.XSpeechSynthesizerCreateStreamFromText(synthesizer, "Hello world",
                out XSpeechSynthesizerStreamHandle stream);
            Assert.AreEqual(0, hr);
            Assert.IsNotNull(stream);

            hr = SDK.XSpeechSynthesizerGetStreamData(stream, out byte[] buffer);
            Assert.AreEqual(0, hr);
            Assert.IsNotNull(buffer);
            Assert.AreNotEqual(0, buffer.Length);
        }
    }
}

#endif //#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)