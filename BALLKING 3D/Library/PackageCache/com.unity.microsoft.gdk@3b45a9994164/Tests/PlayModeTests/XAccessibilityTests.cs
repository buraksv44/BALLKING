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
// https://developer.microsoft.com/en-us/games/xbox/docs/gdk/xaccessibility_members
    public class XAccessibilityTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            GdkTestHelper.Initialize("XAccessibilityTests");
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            GdkTestHelper.Shutdown("XAccessibilityTests");
        }

        [Test]
        public void XClosedCaptionGetProperties()
        {
            XClosedCaptionProperties closedCaptionProperties;

            int hResult = SDK.XClosedCaptionGetProperties(out closedCaptionProperties);
            if (HR.SUCCEEDED(hResult))
            {
                return;
            }

            Assert.Fail($"Fail: hResult=0x{hResult:X8}, '{HR.NameOf(hResult)}'");
        }

        [Test]
        public void XClosedCaptionSetEnabled()
        {
            int hResult = SDK.XClosedCaptionSetEnabled(false);
            if (HR.SUCCEEDED(hResult))
            {
                return;
            }

            Assert.Fail($"Fail: hResult=0x{hResult:X8}, '{HR.NameOf(hResult)}'");
        }

        [Test]
        public void XHighContrastGetMode()
        {
            XHighContrastMode highContrastMode;

            int hResult = SDK.XHighContrastGetMode(out highContrastMode);
            if (HR.SUCCEEDED(hResult))
            {
                return;
            }

            Assert.Fail($"Fail: hResult=0x{hResult:X8}, '{HR.NameOf(hResult)}'");
        }

        [Test]
        public void XSpeechToTextBeginHypothesisString()
        {
            uint hypothesisId;

            int hResult = SDK.XSpeechToTextBeginHypothesisString("XSpeechToTextBeginHypothesisString",
                "Initial hypothesis string", XSpeechToTextType.Text, out hypothesisId);
            if (HR.FAILED(hResult))
            {
                Assert.Fail(
                    $"XSpeechToTextBeginHypothesisString() Fail: hResult=0x{hResult:X8}, '{HR.NameOf(hResult)}'");
                return;
            }

            hResult = SDK.XSpeechToTextUpdateHypothesisString(hypothesisId, "Updated hypothesis string.");
            if (HR.FAILED(hResult))
            {
                Assert.Fail(
                    $"XSpeechToTextUpdateHypothesisString() Fail: hResult=0x{hResult:X8}, '{HR.NameOf(hResult)}'");
                return;
            }

            hResult = SDK.XSpeechToTextCancelHypothesisString(hypothesisId);
            if (HR.FAILED(hResult))
            {
                Assert.Fail(
                    $"XSpeechToTextCancelHypothesisString() Fail: hResult=0x{hResult:X8}, '{HR.NameOf(hResult)}'");
                return;
            }
        }

        [Test]
        [UnityPlatform(exclude = new[]
            { RuntimePlatform.WindowsPlayer, RuntimePlatform.WindowsEditor })] // TODO: HR ERROR: 0x80070490
        public void XSpeechToTextFinalizeHypothesisString()
        {
            uint hypothesisId;

            int hResult = SDK.XSpeechToTextBeginHypothesisString("XSpeechToTextBeginHypothesisString",
                "Initial hypothesis string", XSpeechToTextType.Text, out hypothesisId);
            if (HR.FAILED(hResult))
            {
                Assert.Fail(
                    $"XSpeechToTextBeginHypothesisString() Fail: hResult=0x{hResult:X8}, '{HR.NameOf(hResult)}'");
                return;
            }

            hResult = SDK.XSpeechToTextUpdateHypothesisString(hypothesisId, "Updated hypothesis string.");
            if (HR.FAILED(hResult))
            {
                Assert.Fail(
                    $"XSpeechToTextUpdateHypothesisString() Fail: hResult=0x{hResult:X8}, '{HR.NameOf(hResult)}'");
                return;
            }

            hResult = SDK.XSpeechToTextFinalizeHypothesisString(hypothesisId, "Finalized hypothesis string.");
            if (HR.FAILED(hResult))
            {
                Assert.Fail(
                    $"XSpeechToTextFinalizeHypothesisString() Fail: hResult=0x{hResult:X8}, '{HR.NameOf(hResult)}'");
                return;
            }
        }

        [Test]
        [UnityPlatform(exclude = new[]
            { RuntimePlatform.WindowsPlayer, RuntimePlatform.WindowsEditor })] // TODO: HR ERROR: 0x80070490
        public void XSpeechToTextSendString()
        {
            int hResult = SDK.XSpeechToTextSendString("XSpeechToTextSendString",
                "Text to display on the Game title screen.", XSpeechToTextType.Text);
            if (HR.SUCCEEDED(hResult))
            {
                return;
            }

            Assert.Fail($"Fail: hResult=0x{hResult:X8}, '{HR.NameOf(hResult)}'");
        }

        [Test]
        [UnityPlatform(exclude = new[]
            { RuntimePlatform.WindowsPlayer, RuntimePlatform.WindowsEditor })] // TODO: HR ERROR: 0x80070490
        public void XSpeechToTextSetPositionHint()
        {
            int hResult = SDK.XSpeechToTextSetPositionHint(XSpeechToTextPositionHint.BottomCenter);
            if (HR.SUCCEEDED(hResult))
            {
                return;
            }

            Assert.Fail($"Fail: hResult=0x{hResult:X8}, '{HR.NameOf(hResult)}'");
        }
    }
}

#endif //#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)