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
    public class XNetworkingTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            GdkTestHelper.Initialize(nameof(XNetworkingTests));
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            GdkTestHelper.Shutdown(nameof(XNetworkingTests));
        }

        // https://developer.microsoft.com/en-us/games/xbox/docs/gdk/xnetworkingquerypreferredlocaludpmultiplayerport
        [Test]
        public void XNetworkingQueryPreferredLocalUdpMultiplayerPort()
        {
            UInt16 preferredLocalUdpMultiplayerPort = 0;
            int hResult = SDK.XNetworkingQueryPreferredLocalUdpMultiplayerPort(out preferredLocalUdpMultiplayerPort);
            if (HR.SUCCEEDED(hResult))
            {
                Assert.NotZero(preferredLocalUdpMultiplayerPort, "Expected none zero value for multiplayer port");
                return;
            }

            Assert.Fail(
                $"Call to XNetworkingQueryPreferredLocalUdpMultiplayerPort() failed! (hResult=0x{hResult:X8} '{HR.NameOf(hResult)}')");
        }

        //https://developer.microsoft.com/en-us/games/xbox/docs/gdk/xnetworkinggetconnectivityhint
        [Test]
        public void XNetworkingGetConnectivityHint()
        {
            XNetworkingConnectivityHint connectivityHint;
            int hResult = SDK.XNetworkingGetConnectivityHint(out connectivityHint);
            if (HR.SUCCEEDED(hResult))
            {
                return;
            }

            Assert.Fail(
                $"Call to XNetworkingGetConnectivityHint() failed! (hResult=0x{hResult:X8} '{HR.NameOf(hResult)}')");
        }

        /*
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator XNetworkingTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
        */
    }
}

#endif //#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)