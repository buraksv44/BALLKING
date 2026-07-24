using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using Unity.XGamingRuntime;
using UnityEngine.TestTools;

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE)

namespace Unity.Microsoft.GDK.Tests
{
    [UnityPlatform(exclude = new[]
    {
        RuntimePlatform.WindowsPlayer, RuntimePlatform.WindowsEditor
    })] // Currently have no way for headless signin on CI
    public class XGameSaveTests
    {
        private XUserHandle userHandle;
        private string configurationId;

        [OneTimeSetUp]
        public void Setup()
        {
            GdkTestHelper.Initialize("XGameSaveTests");
            userHandle = XUserTests.GetDefaultUser();

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
            GdkTestHelper.Shutdown("XGameSaveTests");
        }

        private static void CreateSave(XGameSaveProviderHandle providerHandle, byte[] saveData, string containerName,
            string containerDisplayName, string blobName)
        {
            SDK.XGameSaveCreateContainer(providerHandle, containerName, out XGameSaveContainerHandle containerHandle)
                .AssertSucceeded();
            SDK.XGameSaveCreateUpdate(containerHandle, containerDisplayName, out XGameSaveUpdateHandle updateHandle)
                .AssertSucceeded();
            SDK.XGameSaveSubmitBlobWrite(updateHandle, blobName, saveData).AssertSucceeded();
            SDK.XGameSaveSubmitUpdate(updateHandle).AssertSucceeded();
            SDK.XGameSaveCloseUpdate(updateHandle);
            SDK.XGameSaveCloseContainer(containerHandle);
        }

        [Test]
        [Timeout(100)]
        public void InitializeCloseTest()
        {
            // arrange
            var taskCompletionSource = new TaskCompletionSource<XGameSaveProviderHandle>();

            // act
            SDK.XGameSaveInitializeProviderAsync(userHandle, configurationId, false,
                (hresult, gameSaveProviderHandle) =>
                {
                    CallbackAsTask.SafeExecute(taskCompletionSource, () =>
                    {
                        // assert
                        hresult.AssertSucceeded();
                        return gameSaveProviderHandle;
                    });
                });

            taskCompletionSource.Task.Wait();

            // act
            SDK.XGameSaveCloseProvider(taskCompletionSource.Task.Result);
        }

        [Test]
        public void SaveLoadDataBlob()
        {
            // arrange
            var saveData = new byte[] { 1, 2, 3, 4 };
            var taskCompletionSource = new TaskCompletionSource<XGameSaveProviderHandle>();

            // act
            SDK.XGameSaveInitializeProviderAsync(userHandle, configurationId, false,
                (hresult, gameSaveProviderHandle) =>
                {
                    CallbackAsTask.SafeExecute(taskCompletionSource, () =>
                    {
                        // assert
                        hresult.AssertSucceeded();

                        // save data
                        {
                            CreateSave(gameSaveProviderHandle, saveData, "TestContainer", "A test save", "Test1");
                        }

                        // load data
                        {
                            // act
                            SDK.XGameSaveGetContainerInfo(gameSaveProviderHandle, "TestContainer",
                                out XGameSaveContainerInfo containerInfo).AssertSucceeded();

                            // assert
                            Assert.AreEqual(1, containerInfo.BlobCount);
                            Assert.AreEqual(4, containerInfo.TotalSize);

                            // act
                            SDK.XGameSaveCreateContainer(gameSaveProviderHandle, "TestContainer",
                                out XGameSaveContainerHandle containerHandle).AssertSucceeded();
                            SDK.XGameSaveEnumerateBlobInfo(containerHandle, out var blobInfos).AssertSucceeded();
                            SDK.XGameSaveReadBlobData(containerHandle, blobInfos, out var blobs).AssertSucceeded();

                            // assert
                            Assert.AreEqual(1, blobs.Length);
                            CollectionAssert.AreEqual(blobs.First().Data, saveData);

                            SDK.XGameSaveCloseContainer(containerHandle);
                        }

                        return gameSaveProviderHandle;
                    });
                });

            taskCompletionSource.Task.Wait();

            // act
            SDK.XGameSaveCloseProvider(taskCompletionSource.Task.Result);
        }

        [Test]
        public void DeleteDataBlob()
        {
            // arrange
            var taskCompletionSource = new TaskCompletionSource<XGameSaveProviderHandle>();

            // act
            SDK.XGameSaveInitializeProviderAsync(userHandle, configurationId, false,
                (hresult, gameSaveProviderHandle) =>
                {
                    CallbackAsTask.SafeExecute(taskCompletionSource, () =>
                    {
                        // assert
                        hresult.AssertSucceeded();

                        // create container
                        CreateSave(gameSaveProviderHandle, new byte[] { 24 }, "Test", "Test", "Test");

                        // act
                        // delete container
                        SDK.XGameSaveDeleteContainer(gameSaveProviderHandle, "TestContainerForDeletion")
                            .AssertSucceeded();

                        // assert
                        SDK.XGameSaveEnumerateContainerInfoByName(gameSaveProviderHandle, "TestContainerForDeletion",
                            out var namedContainers);
                        Assert.That(namedContainers, Has.Length.EqualTo(0));

                        return gameSaveProviderHandle;
                    });
                });

            taskCompletionSource.Task.Wait();

            // act
            SDK.XGameSaveCloseProvider(taskCompletionSource.Task.Result);
        }

        [Test]
        [Timeout(100)]
        public void GetQuota()
        {
            // arrange
            var taskCompletionSource = new TaskCompletionSource<XGameSaveProviderHandle>();

            // act
            SDK.XGameSaveInitializeProviderAsync(userHandle, configurationId, false,
                (hresult, gameSaveProviderHandle) =>
                {
                    CallbackAsTask.SafeExecute(taskCompletionSource, () =>
                    {
                        // assert
                        hresult.AssertSucceeded();

                        // get quota
                        long remaingQuota;
                        SDK.XGameSaveGetRemainingQuota(gameSaveProviderHandle, out remaingQuota).AssertSucceeded();
                        Assert.That((long)remaingQuota, Is.GreaterThan(100000));

                        return gameSaveProviderHandle;
                    });
                });

            taskCompletionSource.Task.Wait();

            // act
            SDK.XGameSaveCloseProvider(taskCompletionSource.Task.Result);
        }

        [Test]
        public void EnumerateContainers()
        {
            // arrange
            var taskCompletionSource = new TaskCompletionSource<XGameSaveProviderHandle>();

            // act
            SDK.XGameSaveInitializeProviderAsync(userHandle, configurationId, false,
                (hresult, gameSaveProviderHandle) =>
                {
                    CallbackAsTask.SafeExecute(taskCompletionSource, () =>
                    {
                        // assert
                        hresult.AssertSucceeded();

                        // arrange
                        const string containerName = "TestContainer_" + nameof(EnumerateContainers);
                        CreateSave(gameSaveProviderHandle, new byte[] { 24 }, containerName, "Test", "Test");

                        // act
                        SDK.XGameSaveEnumerateContainerInfo(gameSaveProviderHandle, out var enumeratedContainers)
                            .AssertSucceeded();

                        // assert
                        Assert.That(enumeratedContainers.Where(c => c.Name == containerName).ToArray(),
                            Has.Length.EqualTo(1));

                        // act
                        SDK.XGameSaveEnumerateContainerInfoByName(gameSaveProviderHandle, containerName,
                            out var namedContainers);

                        // assert
                        Assert.That(namedContainers, Has.Length.GreaterThanOrEqualTo(1));

                        return gameSaveProviderHandle;
                    });
                });

            taskCompletionSource.Task.Wait();

            // act
            SDK.XGameSaveCloseProvider(taskCompletionSource.Task.Result);
        }

        [Test]
        public void EnumerateBlobs_ForEmptySave_ReturnsEmpty()
        {
            // arrange
            var taskCompletionSource = new TaskCompletionSource<XGameSaveProviderHandle>();

            // act
            SDK.XGameSaveInitializeProviderAsync(userHandle, configurationId, false,
                (hresult, gameSaveProviderHandle) =>
                {
                    CallbackAsTask.SafeExecute(taskCompletionSource, () =>
                    {
                        // assert
                        hresult.AssertSucceeded();

                        SDK.XGameSaveCreateContainer(gameSaveProviderHandle,
                            $"TestContainer_{nameof(EnumerateBlobs_ForEmptySave_ReturnsEmpty)}",
                            out XGameSaveContainerHandle containerHandle).AssertSucceeded();
                        SDK.XGameSaveCreateUpdate(containerHandle, "Test", out XGameSaveUpdateHandle updateHandle)
                            .AssertSucceeded();
                        SDK.XGameSaveSubmitUpdate(updateHandle).AssertSucceeded();

                        // act
                        SDK.XGameSaveEnumerateBlobInfo(containerHandle, out var blobInfos).AssertSucceeded();

                        // assert
                        Assert.That(blobInfos, Has.Length.EqualTo(0));

                        // act
                        SDK.XGameSaveEnumerateBlobInfoByName(containerHandle, "unknown", out var namedBlobs)
                            .AssertSucceeded();

                        // assert
                        Assert.That(namedBlobs, Has.Length.EqualTo(0));

                        return gameSaveProviderHandle;
                    });
                });

            taskCompletionSource.Task.Wait();

            // act
            SDK.XGameSaveCloseProvider(taskCompletionSource.Task.Result);
        }

        [Test]
        public void EnumerateBlobs_ForSaveWithData_ReturnsSave()
        {
            // arrange
            var taskCompletionSource = new TaskCompletionSource<XGameSaveProviderHandle>();

            // act
            SDK.XGameSaveInitializeProviderAsync(userHandle, configurationId, false,
                (hresult, gameSaveProviderHandle) =>
                {
                    CallbackAsTask.SafeExecute(taskCompletionSource, () =>
                    {
                        // assert
                        hresult.AssertSucceeded();

                        SDK.XGameSaveCreateContainer(gameSaveProviderHandle,
                            $"TestContainer_{nameof(EnumerateBlobs_ForSaveWithData_ReturnsSave)}",
                            out XGameSaveContainerHandle containerHandle).AssertSucceeded();
                        SDK.XGameSaveCreateUpdate(containerHandle, "Test", out XGameSaveUpdateHandle updateHandle)
                            .AssertSucceeded();
                        SDK.XGameSaveSubmitBlobWrite(updateHandle, "Test", new byte[] { 1, 2 }).AssertSucceeded();
                        SDK.XGameSaveSubmitUpdate(updateHandle).AssertSucceeded();
                        SDK.XGameSaveCloseUpdate(updateHandle);

                        // act
                        SDK.XGameSaveEnumerateBlobInfo(containerHandle, out var blobInfos).AssertSucceeded();

                        // assert
                        Assert.That(blobInfos, Has.Length.EqualTo(1));

                        // act
                        SDK.XGameSaveEnumerateBlobInfoByName(containerHandle, "Test", out var namedBlobs)
                            .AssertSucceeded();

                        // assert
                        Assert.That(namedBlobs, Has.Length.EqualTo(1));

                        return gameSaveProviderHandle;
                    });
                });

            taskCompletionSource.Task.Wait();

            // act
            SDK.XGameSaveCloseProvider(taskCompletionSource.Task.Result);
        }
    }
}

#endif //#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)