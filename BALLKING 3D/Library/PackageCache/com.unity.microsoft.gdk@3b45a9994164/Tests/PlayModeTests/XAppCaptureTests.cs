using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.XGamingRuntime;

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)

namespace Unity.Microsoft.GDK.Tests
{
// https://developer.microsoft.com/en-us/games/xbox/docs/gdk/xappcapture_members

    [UnityPlatform(exclude = new[] { RuntimePlatform.WindowsPlayer, RuntimePlatform.WindowsEditor })] // Currently have no way for headless signin on CI
    public class XAppCaptureTests
    {
        private XUserHandle userHandle;

        [OneTimeSetUp]
        public void Setup()
        {
            GdkTestHelper.Initialize("XAppCaptureTests");
            userHandle = XUserTests.GetDefaultUser();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            GdkTestHelper.Shutdown("XAppCaptureTests");
        }

        [Test]
        public void XAppBroadcastGetStatus()
        {
            // act
            SDK.XAppBroadcastGetStatus(userHandle, out XAppBroadcastStatus appBroadcastStatus).AssertSucceeded();

            // assert
#if (UNITY_STANDALONE_WIN)
            Assert.IsFalse(appBroadcastStatus
                .CanStartBroadcast); // This seems to be returning 'false' on windows standalone... it may need permissions configuring
#else
            Assert.IsTrue(appBroadcastStatus.CanStartBroadcast);
#endif
            Assert.IsFalse(appBroadcastStatus.IsAnyAppBroadcasting);
            Assert.IsFalse(appBroadcastStatus.IsCaptureResourceUnavailable);
            Assert.IsFalse(appBroadcastStatus.IsGameStreamInProgress);
            Assert.IsFalse(appBroadcastStatus.IsGpuConstrained);
            Assert.IsFalse(appBroadcastStatus.IsAppInactive);
            Assert.IsFalse(appBroadcastStatus.IsBlockedForApp);
            Assert.IsFalse(appBroadcastStatus.IsDisabledByUser);
            Assert.IsFalse(appBroadcastStatus.IsDisabledBySystem);
        }

        [Test]
        public void TakeScreenshot()
        {
            System.Threading.Thread.Sleep(5000); // Fails on Xbox if you try to screen shot too soon

            // act
            SDK.XAppCaptureTakeScreenshot(userHandle, out XAppCaptureTakeScreenshotResult result).AssertSucceeded("SDK.XAppCaptureTakeScreenshot");
            SDK.XAppCaptureOpenScreenshotStream(result.LocalId, XAppCaptureScreenshotFormatFlag.SDR, out XAppCaptureScreenshotStreamHandle handle, out ulong totalBytes).AssertSucceeded("SDK.XAppCaptureOpenScreenshotStream");
            var data = new byte[totalBytes];
            SDK.XAppCaptureReadScreenshotStream(handle, 0, (uint)totalBytes, data, out uint bytesWritten).AssertSucceeded("SDK.XAppCaptureReadScreenshotStream");
            SDK.XAppCaptureCloseScreenshotStream(handle).AssertSucceeded("SDK.XAppCaptureCloseScreenshotStream");

            // assert
            Assert.AreEqual(totalBytes, bytesWritten);
        }

        [Test]
        public void TakeDiagnosticScreenshot()
        {
            // act
            SDK.XAppCaptureTakeDiagnosticScreenshot(false, XAppCaptureScreenshotFormatFlag.SDR,
                $"Test_{nameof(TakeDiagnosticScreenshot)}", out XAppCaptureDiagnosticScreenshotResult result);

            // assert
            Assert.AreEqual(1, result.Files.Length);
            Assert.True(result.Files.First().Path.Contains($"Test_{nameof(TakeDiagnosticScreenshot)}"),
                $"Unexpected file path: \"{result.Files.First().Path}\"");
        }
    }
}

#endif //#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)