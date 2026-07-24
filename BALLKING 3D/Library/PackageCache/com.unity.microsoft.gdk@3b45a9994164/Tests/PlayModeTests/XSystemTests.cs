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
    public class XSystemTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            GdkTestHelper.Initialize("XSystemTests");
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            GdkTestHelper.Shutdown("XSystemTests");
        }

        // https://developer.microsoft.com/en-us/games/xbox/docs/gdk/xsystemgetanalyticsinfo
        [Test]
        public void XSystemGetAnalyticsInfo()
        {
            XSystemAnalyticsInfo analyticsInfo = SDK.XSystemGetAnalyticsInfo();

            Assert.Greater(analyticsInfo.OsVersion.Major, 0,
                $"Expected XSystemGetAnalyticsInfo() to return a value >0 for analyticsInfo.OsVersion.Major, got {analyticsInfo.OsVersion.Major}");
        }

        // https://developer.microsoft.com/en-us/games/xbox/docs/gdk/xsystemgetconsoleid
        [Test]
        public void XSystemGetConsoleId()
        {
            string consoleId;
            int hResult = SDK.XSystemGetConsoleId(out consoleId);
            if (HR.SUCCEEDED(hResult))
            {
                Debug.Log($"Call to XSystemGetConsoleId() returned: {consoleId}");

                Assert.Greater(consoleId.Length, 0, "Expected a consoleId string length more than 0 characters");

                return;
            }

            Assert.Fail($"Call to XSystemGetConsoleId() failed! (hResult=0x{hResult:X8} '{HR.NameOf(hResult)}')");
        }

        // https://developer.microsoft.com/en-us/games/xbox/docs/gdk/xsystemgetxboxlivesandboxid
        [Test]
        public void XSystemGetXboxLiveSandboxId()
        {
            string sandboxId;
            int hResult = SDK.XSystemGetXboxLiveSandboxId(out sandboxId);
            if (HR.SUCCEEDED(hResult))
            {
                Debug.Log($"Call to XSystemGetXboxLiveSandboxId() returned: {sandboxId}");

                Assert.Greater(sandboxId.Length, 0, "Expected a sandboxId string length more than 0 characters");

                return;
            }

            Assert.Fail(
                $"Call to XSystemGetXboxLiveSandboxId() failed! (hResult=0x{hResult:X8} '{HR.NameOf(hResult)}')");
        }

        // https://learn.microsoft.com/en-us/gaming/gdk/_content/gc/reference/system/xsystem/functions/xsystemhandletrack
        [Test]
        [UnityPlatform(exclude = new[]
        {
            RuntimePlatform.WindowsPlayer, RuntimePlatform.WindowsEditor
        })] //TODO: "System.EntryPointNotFoundException
        public void XSystemHandleTrack()
        {
            int hr = SDK.XSystemHandleTrack((IntPtr handle,
                XSystemHandleType type,
                XSystemHandleCallbackReason reason,
                IntPtr context) =>
            {
                Debug.Log("Handle callback called");
            }, IntPtr.Zero, out var handle);
            Assert.AreEqual(0, hr);

            handle.Unregister();
        }

        // https://developer.microsoft.com/en-us/games/xbox/docs/gdk/xsystemgetappspecificdeviceid
        [Test]
        [UnityPlatform(exclude = new[]
        {
            RuntimePlatform.WindowsPlayer, RuntimePlatform.WindowsEditor, RuntimePlatform.GameCoreXboxOne,
            RuntimePlatform.GameCoreXboxSeries
        })] //TODO: "System.EntryPointNotFoundException : XSystemGetAppSpecificDevi
        public void XSystemGetAppSpecificDeviceId()
        {
            string appSpecificDeviceId;
            int hResult = SDK.XSystemGetAppSpecificDeviceId(out appSpecificDeviceId);
            if (HR.SUCCEEDED(hResult))
            {
                Debug.Log($"Call to XSystemGetAppSpecificDeviceId() returned: {appSpecificDeviceId}");

                Assert.Greater(appSpecificDeviceId.Length, 0,
                    "Expected a appSpecificDeviceId string length more than 0 characters");

                return;
            }

            Assert.Fail(
                $"Call to XSystemGetAppSpecificDeviceId() failed! (hResult=0x{hResult:X8} '{HR.NameOf(hResult)}')");
        }

        // https://learn.microsoft.com/en-us/gaming/gdk/_content/gc/reference/system/xsystem/functions/xsystemgetdevicetype
        [Test]
        public void XSystemGetDeviceType()
        {
            // A user reported that the entry point was missing from the thunks DLL
            XSystemDeviceType  systemDeviceType = SDK.XSystemGetDeviceType();
            Debug.Log($"Call to XSystemGetDeviceType() returned: {systemDeviceType}");
        }
    }
}

#endif //#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)