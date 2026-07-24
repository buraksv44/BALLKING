using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.XGamingRuntime;

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)

namespace Unity.Microsoft.GDK.Tests
{
// See: https://developer.microsoft.com/en-us/games/xbox/docs/gdk/xdisplay_members
    public class XDisplayTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            //SceneManager.LoadScene("01_TestScene");
            GdkTestHelper.Initialize("XDisplayTests");
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            GdkTestHelper.Shutdown("XDisplayTests");
        }

        [Test]
        public void XDisplayAcquireTimeoutDeferral()
        {
            XDisplayTimeoutDeferralHandle timeoutDeferralHandle;

            // Try to acquire a new timeout deferral handle.
            int hResult = SDK.XDisplayAcquireTimeoutDeferral(out timeoutDeferralHandle);

            //Assert.Equals(hResult, HR.S_OK);

            if (HR.SUCCEEDED(hResult))
            {
                // Close the timeout deferral handle we're done.
                SDK.XDisplayCloseTimeoutDeferralHandle(timeoutDeferralHandle);
                return;
            }

            Assert.Fail(
                $"Call to XDisplayAcquireTimeoutDeferral() failed! (hResult=0x{hResult:X8} '{HR.NameOf(hResult)}')");
        }

        [Test]
        [UnityPlatform(exclude = new[]
        {
            RuntimePlatform.WindowsPlayer, RuntimePlatform.WindowsEditor
        })] // This causes a player crash in XGamingRuntime on the windows player
        public void XDisplayTryEnableHdrMode()
        {
            // Attempt to enable HDR mode, then initialize based on the
            // result of the attempt.
            XDisplayHdrModeInfo displayModeHdrInfo;

            // We don't really care about the result, we just don't want an exception
            if (XDisplayHdrModeResult.Enabled ==
                SDK.XDisplayTryEnableHdrMode(XDisplayHdrModePreference.PreferHdr, out displayModeHdrInfo))
            {
                // HDR mode is enabled for the attached display.
            }
            else
            {
                // Either HDR mode is disabled for the attached display, or the
                // attached display does not support HDR.
            }
        }
    }
}

#endif //#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)