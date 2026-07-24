using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.XGamingRuntime;
using Unity.XGamingRuntime.Interop;

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)

namespace Unity.Microsoft.GDK.Tests
{
    [UnityPlatform(exclude = new[]
    {
        RuntimePlatform.WindowsPlayer,          // TODO: Fails on Windows CI due to lack of default user
        RuntimePlatform.WindowsEditor,          // TODO: Fails on Windows CI due to lack of default user
        RuntimePlatform.GameCoreXboxOne,        // Require different settings which cannot be set mid test run
        RuntimePlatform.GameCoreXboxSeries      // Require different settings which cannot be set mid test run
    })]  
    public class XStoreTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            GdkTestHelper.Initialize("XStoreTests");
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            GdkTestHelper.Shutdown("XStoreTests");
        }

        // https://developer.microsoft.com/en-us/games/xbox/docs/gdk/xstorecreatecontext
        [Test]
        [UnityPlatform(exclude = new[]
        {
            RuntimePlatform.WindowsPlayer, RuntimePlatform.WindowsEditor
        })] //TODO: Fails on Windows CI due to lack of default user
        public void XStoreCreateContext()
        {
            XUserHandle userHandle = XUserTests.GetDefaultUser();

            XStoreContext storeContext;
            int hr = SDK.XStoreCreateContext(userHandle, out storeContext);
            Assert.AreEqual(0, hr);

            storeContext.Close();
        }

        [Test]       
        public IEnumerator XStoreQueryAssociatedProductsAsync()
        {
            XUserHandle userHandle = XUserTests.GetDefaultUser();

            int hr = SDK.XStoreCreateContext(userHandle, out var storeContext);
            Assert.AreEqual(0, hr);
            Assert.IsNotNull(storeContext);

            List<XStoreProduct> products = new List<XStoreProduct>();

            bool done = false;
            int callbackHr = 0;

            void OnStoreQueryCompleted(int hresult, XStoreQueryResult result)
            {
                callbackHr = hresult;

                if (hresult == 0 && result != null)
                {
                    foreach (var product in result.PageItems)
                    {
                        products.Add(product);
                    }
                }

                done = true;
            }

            hr = SDK.XStoreQueryAssociatedProductsAsync(
                storeContext,
                XStoreProductKind.Game,
                10,
                OnStoreQueryCompleted);

            Assert.AreEqual(0, hr);

            // Wait for completion (with timeout so CI doesn't hang forever)
            float start = Time.realtimeSinceStartup;
            while (!done && Time.realtimeSinceStartup - start < 10f)
                yield return null;

            Assert.IsTrue(done, "XStoreQueryAssociatedProductsAsync timed out.");

            Assert.AreEqual(0, callbackHr, $"Store query failed: hr=0x{callbackHr:X8}");

            storeContext.Close();
            userHandle.Close();
        }
    }
}

#endif //#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)