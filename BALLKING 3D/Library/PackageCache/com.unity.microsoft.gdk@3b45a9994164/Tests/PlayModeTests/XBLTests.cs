using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Unity.XGamingRuntime;
using UnityEngine;
using UnityEngine.TestTools;

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE)

namespace Unity.Microsoft.GDK.Tests
{
    [TestFixture]
    [UnityPlatform(exclude = new[]
    {
        RuntimePlatform.WindowsPlayer, RuntimePlatform.WindowsEditor
    })] // Currently have no way for headless signin on CI
    public class XBLTests
    {
        /// <summary>
        ///     This is a sandbox user that isn't logged in when performing the tests. Instead, it is used when performing Xbox
        ///     live calls that need to target <b>another</b> user, such as checking the privacy settings of another user.
        /// </summary>
        private const ulong SocialUserXuid = 2814651361706400;

        private XUserHandle userHandle;
        private XblContextHandle contextHandle;
        private string configurationId;

        [OneTimeSetUp]
        public void Init()
        {
            // get user handle
            GdkTestHelper.Initialize("XBLTests");
            userHandle = XUserTests.GetDefaultUser();

            // get context handle
            Assert.That(SDK.XBL.XblContextCreateHandle(userHandle, out contextHandle), Is.GreaterThanOrEqualTo(0));
            Assert.IsNotNull(contextHandle);

#if (UNITY_STANDALONE_WIN)
            configurationId = "00000000-0000-0000-0000-000062AB3C24";
#else
    #if (UNITY_GAMECORE)
            configurationId = UnityEngine.GameCore.GameCoreSettings.SCID;
    #endif
#endif
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            SDK.XBL.XblContextCloseHandle(contextHandle);
            userHandle.Close();
            GdkTestHelper.Shutdown("XBLTests");
        }

        [TestCase(XblPermission.PlayMultiplayer, true)]
        [TestCase(XblPermission.CommunicateUsingText, false)]
        [TestCase(XblPermission.CommunicateUsingVoice, false)]
        [Timeout(5000)]
        public void CheckPermission(XblPermission permission, bool expected)
        {
            // arrange
            var taskCompletionSource = new TaskCompletionSource<Empty>();

            // act
            SDK.XBL.XblPrivacyCheckPermissionAsync(contextHandle, permission, SocialUserXuid, (hresult, result) =>
            {
                CallbackAsTask.SafeExecute(taskCompletionSource, () =>
                {
                    // assert
                    hresult.AssertSucceeded();
                    Assert.AreEqual(expected, result.IsAllowed);
                });
            });
            taskCompletionSource.Task.Wait();
        }

        [TestCase(XblPermission.PlayMultiplayer, true)]
        [TestCase(XblPermission.CommunicateUsingText, true)]
        [TestCase(XblPermission.CommunicateUsingVoice, true)]
        [Timeout(5000)]
        public void CheckPermissionForAnonymousUser(XblPermission permission, bool expected)
        {
            // arrange
            var taskCompletionSource = new TaskCompletionSource<Empty>();

            // act
            SDK.XBL.XblPrivacyCheckPermissionForAnonymousUserAsync(contextHandle, permission, XblAnonymousUserType.CrossNetworkFriend, (hresult, result) =>
            {
                CallbackAsTask.SafeExecute(taskCompletionSource, () =>
                {
                    // assert
                    hresult.AssertSucceeded();
                    Assert.AreEqual(expected, result.IsAllowed);
                });
            });
            taskCompletionSource.Task.Wait();
        }

        [TestCase]
        [Timeout(5000)]
        public void CheckUserPermissionsBatch()
        {
            // arrange
            var taskCompletionSource = new TaskCompletionSource<Empty>();
            var permissions = new Dictionary<XblPermission, bool>
            {
                { XblPermission.ShareItem, true },
                { XblPermission.WriteComment, true },
                { XblPermission.ViewTargetProfile, false },
                { XblPermission.ViewTargetGameHistory, false }
            };

            // act
            SDK.XBL.XblPrivacyBatchCheckPermissionAsync(
                contextHandle,
                permissions.Keys.ToArray(),
                new ulong[] { SocialUserXuid },
                new XblAnonymousUserType[] { },
                (hresult, result) =>
                {
                    CallbackAsTask.SafeExecute(taskCompletionSource, () =>
                    {
                        // assert
                        hresult.AssertSucceeded();
                        Assert.AreEqual(permissions.Count, result.Length);
                        foreach (XblPermissionCheckResult checkResult in result)
                        {
                            Assert.AreEqual(permissions[checkResult.PermissionRequested], checkResult.IsAllowed);
                        }
                    });
                });
            taskCompletionSource.Task.Wait();
        }

        [TestCase]
        [Timeout(5000)]
        public void CheckAnonymousUserPermissionsBatch()
        {
            // arrange
            var taskCompletionSource = new TaskCompletionSource<Empty>();
            var permissions = new Dictionary<XblPermission, bool>
            {
                { XblPermission.ShareItem, true },
                { XblPermission.WriteComment, true },
                { XblPermission.ViewTargetProfile, true },
                { XblPermission.ViewTargetGameHistory, true }
            };

            // act
            SDK.XBL.XblPrivacyBatchCheckPermissionAsync(
                contextHandle,
                permissions.Keys.ToArray(),
                new ulong[] { },
                new[] { XblAnonymousUserType.CrossNetworkUser },
                (hresult, result) =>
                {
                    CallbackAsTask.SafeExecute(taskCompletionSource, () =>
                    {
                        // assert
                        hresult.AssertSucceeded();
                        Assert.AreEqual(permissions.Count, result.Length);
                        foreach (XblPermissionCheckResult checkResult in result)
                        {
                            Assert.AreEqual(permissions[checkResult.PermissionRequested], checkResult.IsAllowed);
                        }
                    });
                });
            taskCompletionSource.Task.Wait();
        }

        [Test]
        [Timeout(5000)]
        public void GetAvoidList()
        {
            // arrange
            var taskCompletionSource = new TaskCompletionSource<Empty>();

            // act
            SDK.XBL.XblPrivacyGetAvoidListAsync(contextHandle, (hresult, xuids) =>
            {
                CallbackAsTask.SafeExecute(taskCompletionSource, () =>
                {
                    // assert
                    hresult.AssertSucceeded();
                    Assert.AreEqual(0, xuids.Length);
                });
            });
            taskCompletionSource.Task.Wait();
        }

        [Test]
        [Timeout(5000)]
        public void GetMuteList()
        {
            // arrange
            var taskCompletionSource = new TaskCompletionSource<Empty>();

            // act
            SDK.XBL.XblPrivacyGetMuteListAsync(contextHandle, (hresult, xuids) =>
            {
                CallbackAsTask.SafeExecute(taskCompletionSource, () =>
                {
                    // assert
                    hresult.AssertSucceeded();
                    Assert.AreEqual(0, xuids.Length);
                });
            });
            taskCompletionSource.Task.Wait();
        }

        [Test]
        [Timeout(5000)]
        public void WriteTitleManagedStats()
        {
            // arrange
            var taskCompletionSource = new TaskCompletionSource<Empty>();
            XblTitleManagedStatistic.Create("GameCompletion", 0.56, out XblTitleManagedStatistic gameCompletionStat)
                .AssertSucceeded();
            XblTitleManagedStatistic.Create("LastPurchasedCar", "UnityConceptCar", out XblTitleManagedStatistic carStat)
                .AssertSucceeded();

            // act
            SDK.XBL.XblTitleManagedStatsUpdateStatsAsync(contextHandle, new[] { gameCompletionStat, carStat },
                hresult =>
                {
                    CallbackAsTask.SafeExecute(taskCompletionSource, () =>
                    {
                        // assert
                        hresult.AssertSucceeded();
                    });
                });
            taskCompletionSource.Task.Wait();
        }

        [Test]
        public void TitleStorageSaveDataBlob()
        {
            XblTitleStorageBlobMetadata blobMetadata = new XblTitleStorageBlobMetadata();
            blobMetadata.DisplayName = "title storage test";
            blobMetadata.ServiceConfigurationId = configurationId;
            blobMetadata.BlobPath = "./testBlob.dat";
            blobMetadata.StorageType = XblTitleStorageType.GlobalStorage;
            blobMetadata.BlobType = XblTitleStorageBlobType.Binary;

            string blobData = "This is a Test.";

            const UInt32 preferredUploadBlockSize = (1024 * 256);

            var taskCompletionSource = new TaskCompletionSource<Empty>();
            SDK.XBL.XblTitleStorageUploadBlobAsync(
                contextHandle,
                blobMetadata,
                Encoding.ASCII.GetBytes(blobData),
                XblTitleStorageETagMatchCondition.NotUsed,
                preferredUploadBlockSize,
                (hresult, result) =>
                {
                    CallbackAsTask.SafeExecute(taskCompletionSource, () =>
                    {
                        hresult.AssertSucceeded();
                    });
                });
        }
    }
}

#endif //#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)