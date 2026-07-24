using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.XGamingRuntime;

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)

namespace Unity.Microsoft.GDK.Tests
{
    public class XUserTests
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

        [Test]
        public void XUserRegisterForChangeEvent()
        {
            int hr = SDK.XUserRegisterForChangeEvent(UserChangeEventCallback, out var token);
            Assert.AreEqual(0, hr);

            Assert.IsTrue(token.Unregister(true));
        }

        static void UserChangeEventCallback(IntPtr _, XUserLocalId userLocalId, XUserChangeEvent eventType)
        {
            Debug.Log("User change event");
        }

        // https://developer.microsoft.com/en-us/games/xbox/docs/gdk/xsystemgetconsoleid
        [Test]
        public void XUserGetMaxUsers()
        {
            uint maxUsers;
            int hResult = SDK.XUserGetMaxUsers(out maxUsers);
            if (HR.SUCCEEDED(hResult))
            {
                Debug.Log($"Call to XSystemGetConsoleId() returned: {maxUsers.ToString()}");

                Assert.Greater(maxUsers, 0, $"Expected maxUsers to be > 0, got: maxUsers={maxUsers.ToString()}");
                return;
            }

            Assert.Fail($"Call to XUserGetMaxUsers() failed! (hResult=0x{hResult:X8} '{HR.NameOf(hResult)}')");
        }

        [Test]
        [UnityPlatform(exclude = new[]
        {
            RuntimePlatform.WindowsPlayer, RuntimePlatform.WindowsEditor
        })] // Currently have no way for headless signin on CI
        public void GetGamertag()
        {
            // arrange
            XUserHandle userHandle = GetDefaultUser();

            // act
            // get gamertag
            SDK.XUserGetGamertag(userHandle, XUserGamertagComponent.Classic, out string gamertagClassic)
                .AssertSucceeded();
            SDK.XUserGetGamertag(userHandle, XUserGamertagComponent.Modern, out string gamertagModern)
                .AssertSucceeded();
            SDK.XUserGetGamertag(userHandle, XUserGamertagComponent.ModernSuffix, out string gamertagSuffix)
                .AssertSucceeded();
            SDK.XUserGetGamertag(userHandle, XUserGamertagComponent.UniqueModern, out string gamertagUniqueModern)
                .AssertSucceeded();

            // assert
            Assert.That(gamertagClassic, Is.Not.Null);
            Assert.IsFalse(string.IsNullOrEmpty(gamertagModern));
            Assert.That(gamertagSuffix, Is.Not.Null);
            Assert.IsFalse(string.IsNullOrEmpty(gamertagUniqueModern));
        }

        [Test]
        [UnityPlatform(exclude = new[]
        {
            RuntimePlatform.WindowsPlayer, RuntimePlatform.WindowsEditor
        })] // Currently have no way for headless signin on CI
        public void GetId()
        {
            // arrange
            XUserHandle userHandle = GetDefaultUser();

            // act
            SDK.XUserGetId(userHandle, out ulong userId).AssertSucceeded();

            // assert
            Assert.That(userId, Is.GreaterThan(0));
        }

        [Test]
        [UnityPlatform(exclude = new[]
        {
            RuntimePlatform.WindowsPlayer, RuntimePlatform.WindowsEditor
        })] // Currently have no way for headless signin on CI
        public void GetIsGuest()
        {
            // arrange
            XUserHandle userHandle = GetDefaultUser();

            // act
            SDK.XUserGetIsGuest(userHandle, out bool isGuest).AssertSucceeded();

            // assert
            Assert.IsFalse(isGuest);
        }

        [Test]
        [UnityPlatform(exclude = new[]
        {
            RuntimePlatform.WindowsPlayer, RuntimePlatform.WindowsEditor
        })] // Currently have no way for headless signin on CI
        [Timeout(1000)]
        public void GetUserImage()
        {
            // arrange
            XUserHandle userHandle = GetDefaultUser();
            var taskCompletionSource1 = new TaskCompletionSource<Empty>();
            var taskCompletionSource2 = new TaskCompletionSource<Empty>();
            var taskCompletionSource3 = new TaskCompletionSource<Empty>();
            var taskCompletionSource4 = new TaskCompletionSource<Empty>();

            // act
            SDK.XUserGetGamerPictureAsync(userHandle, XUserGamerPictureSize.Small, (hr, buffer) =>
            {
                CallbackAsTask.SafeExecute(taskCompletionSource1, () =>
                {
                    // assert
                    Assert.AreEqual(0, hr, $"Fail: hResult=0x{hr:X8}, '{HR.NameOf(hr)}'");
                    Assert.NotNull(buffer);
                });
            });
            SDK.XUserGetGamerPictureAsync(userHandle, XUserGamerPictureSize.Medium, (hr, buffer) =>
            {
                CallbackAsTask.SafeExecute(taskCompletionSource2, () =>
                {
                    // assert
                    Assert.AreEqual(0, hr, $"Fail: hResult=0x{hr:X8}, '{HR.NameOf(hr)}'");
                    Assert.NotNull(buffer);
                });
            });
            SDK.XUserGetGamerPictureAsync(userHandle, XUserGamerPictureSize.Large, (hr, buffer) =>
            {
                CallbackAsTask.SafeExecute(taskCompletionSource3, () =>
                {
                    // assert
                    Assert.AreEqual(0, hr, $"Fail: hResult=0x{hr:X8}, '{HR.NameOf(hr)}'");
                    Assert.NotNull(buffer);
                });
            });
            SDK.XUserGetGamerPictureAsync(userHandle, XUserGamerPictureSize.ExtraLarge, (hr, buffer) =>
            {
                CallbackAsTask.SafeExecute(taskCompletionSource4, () =>
                {
                    // assert
                    Assert.AreEqual(0, hr, $"Fail: hResult=0x{hr:X8}, '{HR.NameOf(hr)}'");
                    Assert.NotNull(buffer);
                });
            });
            Task.WaitAll(taskCompletionSource1.Task, taskCompletionSource2.Task, taskCompletionSource3.Task,
                taskCompletionSource4.Task);
        }

        [Timeout(1000)]
        internal static XUserHandle GetDefaultUser()
        {
            // using task completion source to wait for the callback
            var taskCompletionSource = new TaskCompletionSource<XUserHandle>();

            // act
            // get local user
            SDK.XUserAddAsync(XUserAddOptions.AddDefaultUserSilently, (hresultAddUser, userHandle) =>
            {
                CallbackAsTask.SafeExecute(taskCompletionSource, () =>
                {
                    // assert
                    hresultAddUser.AssertSucceeded();
                    Assert.IsNotNull(userHandle);

                    return userHandle;
                });
            });
            taskCompletionSource.Task.Wait(TimeSpan.FromSeconds(30));
            if (!taskCompletionSource.Task.IsCompleted)
            {
                Assert.Fail("Timeout waiting for user");
            }

            return taskCompletionSource.Task.Result;
        }
    }
}

#endif //#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)