using NUnit.Framework;
using System;
using System.Threading;
using Unity.XGamingRuntime;
using UnityEngine;

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)

namespace Unity.Microsoft.GDK.Tests
{
    public class HttpClientTests : MonoBehaviour
    {
        [OneTimeSetUp]
        public void Setup()
        {
            GdkTestHelper.Initialize("HttpClientTests");

            SDK.HCInitialize();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            SDK.HCCleanupAsync(() =>
            {
                Debug.Log("HC Cleanup completed");
            });

            GdkTestHelper.Shutdown("HttpClientTests");
        }

        [Test]
        [Timeout(5000)]
        public void HttpClientCreateConnection()
        {
            HCWebsocketHandle websocket;
            string incomingMessage = string.Empty;
            bool connected = false;

            SDK.HCWebSocketCreate(out websocket,
                (HCWebsocketHandle websocket, string incomingBodyString) =>
                {
                    incomingMessage = incomingBodyString;
                },
                (HCWebsocketHandle websocket, byte[] payloadBytes) =>
                {
                },
                (HCWebsocketHandle websocket, HCWebSocketCloseStatus closeStatus) =>
                {
                }).AssertSucceeded();

            // Connect to echo server
            SDK.HCWebSocketConnectAsync("https://ws.ifelse.io", "", websocket, (HCWebsocketHandle websocket, Int32 errorCode, UInt32 platformErrorCode) =>
            {
                Assert.AreEqual(0, errorCode, "Validate socket connect string result error code");
                Assert.AreEqual(0, platformErrorCode, "Validate socket connect string result platform error");
                connected = true;
            }).AssertSucceeded();

            while(!connected)
            {
                Thread.Yield();
            }

            Assert.IsFalse(string.IsNullOrEmpty(incomingMessage));

            SDK.HCWebSocketSendMessageAsync(websocket, "TestObject",
                (HCWebsocketHandle websocket, Int32 errorCode, UInt32 platformErrorCod) =>
                {
                    Assert.AreEqual(HR.S_OK, errorCode, "Validate message send string result error code");
                    Assert.AreEqual(HR.S_OK, platformErrorCod, "Validate message send string result platform result");

                }).AssertSucceeded();

            byte[] data = { 1, 2, 3 };
            SDK.HCWebSocketSendBinaryMessageAsync(websocket, data, (uint)data.Length,
                (HCWebsocketHandle websocket, Int32 errorCode, UInt32 platformErrorCod) =>
            {
                Assert.AreEqual(HR.S_OK, errorCode, "Validate message send string result error code");
                Assert.AreEqual(HR.S_OK, platformErrorCod, "Validate message send string result platform result");

            }).AssertSucceeded();

            SDK.HCWebSocketDisconnect(websocket).AssertSucceeded();

            websocket.Close();
        }
    }

}

#endif //#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)