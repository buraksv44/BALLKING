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
    public class XPackagingTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            GdkTestHelper.Initialize("XPackagingTests");
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            GdkTestHelper.Shutdown("XPackagingTests");
        }

        // https://developer.microsoft.com/en-us/games/xbox/docs/gdk/xpackagegetcurrentprocesspackageidentifier
        // Disabled on windows as this will fail on none packaged player builds.
        [Test]
        [UnityPlatform(exclude = new[]{ RuntimePlatform.WindowsPlayer, RuntimePlatform.WindowsEditor })]
        public void XPackagingGetCurrentProcessPackageIdentifier()
        {
            string identifier = "";
            int hResult = SDK.XPackageGetCurrentProcessPackageIdentifier(out identifier);
            if (HR.SUCCEEDED(hResult))
            {
                Debug.Log($"Call to XPackageGetCurrentProcessPackageIdentifier() result: identifier={identifier}");

                Assert.Greater(identifier.Length, 0, "Expected a identifier string length more than 0 characters");
                Assert.LessOrEqual(identifier.Length, SDK.XPACKAGE_IDENTIFIER_MAX_LENGTH,
                    $"Expected a identifier string length <= {SDK.XPACKAGE_IDENTIFIER_MAX_LENGTH}");

                return;
            }

            Assert.Fail(
                $"Call to XPackageGetCurrentProcessPackageIdentifier() failed! (hResult=0x{hResult:X8} '{HR.NameOf(hResult)}')");
        }
    }
}

#endif //#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)