using System;
using System.Threading;
using Unity.XGamingRuntime.Interop;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

// This file contains overloads for GDK wrappers that make calling certain APIs easier

namespace Unity.XGamingRuntime
{
    public delegate void XNetworkingQueryPreferredLocalUdpMultiplayerPortResultFunction(Int32 errorCode, UInt16 preferredLocalUdpMultiplayerPort);
    public delegate void XGameSaveGetRemainingQuotaCompleted(int hr, long remainingQuota);
    public delegate void XGameSaveDeleteContainerCompleted(int hr);
    public delegate void XStoreQueryComplete(Int32 hresult, XStoreQueryResult result);
    public delegate void XNetworkingQuerySecurityInformationForUrlResultCallback(Int32 errorCode, XNetworkingSecurityInformation result);
    public delegate void XStoreShowProductPageUICompleted(Int32 hresult);
    public delegate void XStoreShowAssociatedProductsUICompleted(Int32 hresult);
    public delegate void XStoreAcquireLicenseForDurablesCompleted(Int32 hresult, XStoreLicense license);
    public delegate void XStoreAcquireLicenseForPackageCompleted(Int32 hresult, XStoreLicense license);
    public delegate void XStoreCanAcquireLicenseForPackageCompleted(Int32 hresult, XStoreCanAcquireLicenseResult result);
    public delegate void XStoreCanAcquireLicenseForStoreIdCompleted(Int32 hresult, XStoreCanAcquireLicenseResult result);
    public delegate void XStoreQueryAddOnLicensesCompleted(Int32 hresult, XStoreAddonLicense[] licenses);
    public delegate void XStoreQueryGameLicenseCompleted(Int32 hresult, XStoreGameLicense license);
    public delegate void XStoreQueryLicenseTokenCompleted(Int32 hresult, string token);
    public delegate void XUserAddCompleted(Int32 hresult, XUserHandle userHandle);
    public delegate void XUserGetGamerPictureCompleted(Int32 hresult, Byte[] buffer);
    public delegate void XUserResolvePrivilegeWithUiCompleted(Int32 hresult);
    public delegate void XUserGetTokenAndSignatureUtf16Result(Int32 hresult, XUserGetTokenAndSignatureUtf16Data tokenAndSignature);
    public delegate void XUserResolveIssueWithUiUtf16Result(Int32 hresult);
    public delegate void XUserFindControllerForUserWithUiResult(Int32 Hresult, APP_LOCAL_DEVICE_ID deviceId);
    public delegate void XGameSaveInitializeProviderCompleted(Int32 hresult, XGameSaveProviderHandle gameSaveProviderHandle);
    public delegate void XGameSaveSubmitUpdateCompleted(Int32 hresult);
    public delegate void XGameSaveReadBlobDataCompleted(Int32 hresult, XGameSaveBlob[] blobs);
    public delegate void XGameSaveFilesGetFolderWithUiCompleted(Int32 hresult, string folderResult);

    public delegate void XStoreShowRedeemTokenUICompleted(Int32 hresult);
    public delegate void XStoreShowRateAndReviewUICompleted(Int32 hresult, XStoreRateAndReviewResult result);
    public delegate void XStoreShowPurchaseUICompleted(Int32 hresult);
    public delegate void XStoreQueryConsumableBalanceRemainingCompleted(Int32 hresult, XStoreConsumableResult result);
    public delegate void XStoreReportConsumableFulfillmentCompleted(Int32 hresult, XStoreConsumableResult result);
    public delegate void XStoreGetUserCollectionsIdCompleted(Int32 hresult, string token);
    public delegate void XStoreGetUserPurchaseIdCompleted(Int32 hresult, string token);
    public delegate void XStoreQueryGameAndDlcPackageUpdatesCompleted(Int32 hresult, XStorePackageUpdate[] packageUpdates);
    public delegate void XStoreDownloadAndInstallPackagesCompleted(Int32 hresult, string[] packageIdentifiers);
    public delegate void XStoreDownloadAndInstallPackageUpdatesCompleted(Int32 hresult);
    public delegate void XStoreDownloadPackageUpdatesCompleted(Int32 hresult);

    public delegate void XPackageMountWithUiAsyncCompleted(Int32 hresult, XPackageMountHandle mountHandle);
    public delegate void XPackageInstallChunksCompleted(Int32 hresult, XPackageInstallationMonitorHandle installationMonitor);

    public delegate void XGameUiShowAchievementsCompleted(Int32 hresult);
    public delegate void XGameUiShowMessageDialogCompleted(Int32 hresult, XGameUiMessageDialogButton resultButton);
    public delegate void XGameUiShowErrorDialogCompleted(Int32 hresult);
    public delegate void XGameUiShowSendGameInviteCompleted(Int32 hresult);
    public delegate void XGameUiShowMultiplayerActivityGameInviteCompleted(Int32 hresult);
    public delegate void XGameUiShowPlayerProfileCardCompleted(Int32 hresult);
    public delegate void XGameUiShowPlayerPickerCompleted(Int32 hresult, UInt64[] resultPlayers);
    public delegate void XGameUiShowTextEntryCompleted(Int32 hresult, string resultText);
    public delegate void XGameUiShowWebAuthenticationAsyncCompleted(int hresult, XGameUiWebAuthenticationResultData result);

    public delegate void XPersistentLocalStoragePromptUserForSpaceAsyncCallback(Int32 hresult);

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public partial class SDK
    {
        private static XTaskQueueHandle defaultQueue;
        static bool canBreak = false;
        static Thread m_DispatchJob;

        // Because of the access pattern, volatile is enough here
        static volatile bool isInitialized = false;
        static volatile bool m_StopExecution;

        static void DispatchGXDKTaskQueue()
        {
            // this will execute all GXDK async work
            while (m_StopExecution == false)
            {
                XTaskQueueDispatch(defaultQueue, XTaskQueuePort.Work, 32);
            }

            DispatchGXDKTaskDone();
        }

        static void DispatchGXDKTaskDone()
        {
            isInitialized = false;
        }

        public static int CreateDefaultTaskQueue()
        {
            if (isInitialized)
                return HR.S_OK;

            int hr = XTaskQueueCreate(XTaskQueueDispatchMode.Manual, XTaskQueueDispatchMode.Manual, out defaultQueue);
            if (HR.SUCCEEDED(hr))
            {
                m_StopExecution = false;

                m_DispatchJob = new Thread(DispatchGXDKTaskQueue) { Name = "GXDK Task Queue Dispatch" };
                m_DispatchJob.Start();

                isInitialized = true;
            }

            return hr;
        }

        public static void CloseDefaultXTaskQueue()
        {
            if (isInitialized)
            {
                if (m_StopExecution == false)
                {
                    canBreak = false;
                    int hr = XTaskQueueTerminate(defaultQueue, false, IntPtr.Zero, (IntPtr context) =>
                    {
                        canBreak = true;
                    }, out XTaskQueueTerminateCallbackHandle terminateHandle);

                    while (!canBreak)
                    {
                        XTaskQueueDispatch(defaultQueue, XTaskQueuePort.Completion, 0);
                        Thread.Yield();
                    }

                    m_StopExecution = true;
                    terminateHandle.Dispose();
                }

                defaultQueue.Close();
                m_DispatchJob.Join();
            }
        }

        public static bool XTaskQueueDispatch(UInt32 timeoutInMs)
        {
            return XTaskQueueDispatch(defaultQueue, XTaskQueuePort.Completion, timeoutInMs);
        }

        public static int XUserRegisterForChangeEvent(XUserChangeEventCallback callback,
            out XUserChangeRegistrationToken token)
        {
            return XUserRegisterForChangeEvent(defaultQueue, IntPtr.Zero, callback, out token);
        }


        [Obsolete("Please use XGameSaveCloseUpdate(XGameSaveUpdateHandle context) instead. (UnityUpgradable) -> XGameSaveCloseUpdate(*)", true)]
        public static void XGameSaveCloseUpdateHandle(XGameSaveUpdateHandle updateHandle) => XGameSaveCloseUpdate(updateHandle);

        public static int XGameSaveDeleteContainerAsync(XGameSaveProviderHandle gameSaveProviderHandle,
            string containerName,
            XGameSaveDeleteContainerCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XGameSaveDeleteContainerResult(_async);
                onCompleted(hr);
            }, IntPtr.Zero);

            int hr = XGameSaveDeleteContainerAsync(gameSaveProviderHandle, containerName, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr);
            }

            return hr;
        }

        public static int XGameSaveGetRemainingQuotaAsync(XGameSaveProviderHandle provider,
            XGameSaveGetRemainingQuotaCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XGameSaveGetRemainingQuotaResult(_async, out long remainingQuota);
                onCompleted(hr, remainingQuota);
            }, IntPtr.Zero);

            int hr = XGameSaveGetRemainingQuotaAsync(provider, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XGameSaveInitializeProviderAsync(XUserHandle requestingUser,
            string configurationId,
            bool syncOnDemand,
            XGameSaveInitializeProviderCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                XGameSaveProviderHandle providerHandle;
                int hr = XGameSaveInitializeProviderResult(_async, out providerHandle);
                onCompleted(hr, providerHandle);
            }, IntPtr.Zero);

            int hr = XGameSaveInitializeProviderAsync(requestingUser, configurationId, syncOnDemand, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XGameSaveReadBlobDataAsync(XGameSaveContainerHandle container,
            string[] blobNames,
            XGameSaveReadBlobDataCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XAsyncGetResultSize(_async, out ulong blobsSize);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, null);
                    return;
                }

                hr = XGameSaveReadBlobDataResult(_async, blobsSize, out XGameSaveBlob[] blobData);
                onCompleted(hr, blobData);
            }, IntPtr.Zero);

            int hr = XGameSaveReadBlobDataAsync(container, blobNames, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XGameSaveSubmitUpdateAsync(XGameSaveUpdateHandle updateContext,
            XGameSaveSubmitUpdateCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XGameSaveSubmitUpdateResult(_async);
                onCompleted(hr);
            }, IntPtr.Zero);

            int hr = XGameSaveSubmitUpdateAsync(updateContext, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr);
            }

            return hr;
        }

        public static int XGameSaveFilesGetFolderWithUiAsync(XUserHandle requestingUser,
            string configurationId,
            XGameSaveFilesGetFolderWithUiCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                ulong maxPath = 261; // MAX_PATH + /0
                int hr = XGameSaveFilesGetFolderWithUiResult(_async, maxPath, out string folderResult);
                onCompleted(hr, folderResult);
            }, IntPtr.Zero);

            int hr = XGameSaveFilesGetFolderWithUiAsync(requestingUser, configurationId, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static Int32 XGameUiShowAchievementsAsync(XUserHandle requestingUser,
            UInt32 titleId,
            XGameUiShowAchievementsCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XGameUiShowAchievementsResult(_async);
                onCompleted(hr);
            }, IntPtr.Zero);

            int hr = XGameUiShowAchievementsAsync(block, requestingUser, titleId);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr);
            }

            return hr;
        }

        public static Int32 XGameUiShowErrorDialogAsync(Int32 errorCode,
            string context,
            XGameUiShowErrorDialogCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XGameUiShowErrorDialogResult(_async);
                onCompleted(hr);
            }, IntPtr.Zero);

            int hr = XGameUiShowErrorDialogAsync(block, errorCode, context);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr);
            }

            return hr;
        }

        public static Int32 XGameUiShowMessageDialogAsync(string titleText,
            string contextText, string firstButtonText,
            string secondButtonText, string thirdButtonText,
            XGameUiMessageDialogButton defaultButton, XGameUiMessageDialogButton cancelButton,
            XGameUiShowMessageDialogCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XGameUiShowMessageDialogResult(_async, out XGameUiMessageDialogButton resultButton);
                onCompleted(hr, resultButton);
            }, IntPtr.Zero);

            int hr = XGameUiShowMessageDialogAsync(block, titleText, contextText, firstButtonText, secondButtonText, thirdButtonText, defaultButton, cancelButton);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static Int32 XGameUiShowMultiplayerActivityGameInviteAsync(XUserHandle requestingUser,
            XGameUiShowMultiplayerActivityGameInviteCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XGameUiShowMultiplayerActivityGameInviteResult(_async);
                onCompleted(hr);
            }, IntPtr.Zero);

            int hr = XGameUiShowMultiplayerActivityGameInviteAsync(block, requestingUser);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr);
            }

            return hr;
        }

        public static Int32 XGameUiShowPlayerPickerAsync(XUserHandle requestingUser,
            string promptText,
            UInt64[] selectFromPlayers,
            UInt64[] preSelectedPlayers,
            UInt32 minSelectionCount,
            UInt32 maxSelectionCount,
            XGameUiShowPlayerPickerCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XGameUiShowPlayerPickerResultCount(_async, out uint resultPlayersCount);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, null);
                    return;
                }

                UInt64[] resultPlayers = new UInt64[resultPlayersCount];
                hr = XGameUiShowPlayerPickerResult(_async, resultPlayers, out uint resultPlayerUsed);
                onCompleted(hr, resultPlayers);
            }, IntPtr.Zero);

            int hr = XGameUiShowPlayerPickerAsync(block, requestingUser, promptText, selectFromPlayers, preSelectedPlayers, minSelectionCount, maxSelectionCount);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static Int32 XGameUiShowPlayerProfileCardAsync(XUserHandle requestingUser,
            UInt64 targetPlayer,
            XGameUiShowPlayerProfileCardCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XGameUiShowPlayerProfileCardResult(_async);
                onCompleted(hr);
            }, IntPtr.Zero);

            int hr = XGameUiShowPlayerProfileCardAsync(block, requestingUser, targetPlayer);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr);
            }

            return hr;
        }

        public static Int32 XGameUiShowSendGameInviteAsync(XUserHandle requestingUser,
            string sessionConfigurationId,
            string sessionTemplateName,
            string sessionId,
            string invitationText,
            string customActivationContext,
            XGameUiShowSendGameInviteCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XGameUiShowSendGameInviteResult(_async);
                onCompleted(hr);
            }, IntPtr.Zero);

            int hr = XGameUiShowSendGameInviteAsync(block, requestingUser, sessionConfigurationId, sessionTemplateName, sessionId, invitationText, customActivationContext);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr);
            }

            return hr;
        }

        public static Int32 XGameUiShowTextEntryAsync(string titleText,
            string descriptionText,
            string defaultText,
            XGameUiTextEntryInputScope inputScope,
            UInt32 maxTextLength,
            XGameUiShowTextEntryCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XGameUiShowTextEntryResultSize(_async, out uint resultTextBufferSize);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, null);
                    return;
                }

                hr = XGameUiShowTextEntryResult(_async, resultTextBufferSize, out string resultTextBuffer);
                onCompleted(hr, resultTextBuffer);
            }, IntPtr.Zero);

            int hr = XGameUiShowTextEntryAsync(block, titleText, descriptionText, defaultText, inputScope, maxTextLength);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static Int32 XGameUiShowWebAuthenticationAsync(XUserHandle requestingUser,
            string requestUri,
            string completeUri,
            XGameUiShowWebAuthenticationAsyncCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XGameUiShowWebAuthenticationResultSize(_async, out ulong bufferSize);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                byte[] buffer = new byte[bufferSize];
                hr = XGameUiShowWebAuthenticationResult(_async, buffer, out XGameUiWebAuthenticationResultData result);
                onCompleted(hr, result);
            }, IntPtr.Zero);

            int hr = XGameUiShowWebAuthenticationAsync(block, requestingUser, requestUri, completeUri);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static Int32 XGameUiShowWebAuthenticationWithOptionsAsync(XUserHandle requestingUser,
            string requestUri,
            string completeUri,
            XGameUiWebAuthenticationOptions options,
            Action<int, XGameUiWebAuthenticationResultData> onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XGameUiShowWebAuthenticationResultSize(_async, out ulong bufferSize);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                byte[] buffer = new byte[bufferSize];
                hr = XGameUiShowWebAuthenticationResult(_async, buffer, out XGameUiWebAuthenticationResultData result);
                onCompleted(hr, result);
            }, IntPtr.Zero);

            int hr = XGameUiShowWebAuthenticationWithOptionsAsync(block, requestingUser, requestUri, completeUri, options);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static Int32 XNetworkingQueryPreferredLocalUdpMultiplayerPortAsync(XNetworkingQueryPreferredLocalUdpMultiplayerPortResultFunction onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XNetworkingQueryPreferredLocalUdpMultiplayerPortAsyncResult(_async, out ushort preferredLocalUdpMultiplayerPort);
                onCompleted(hr, preferredLocalUdpMultiplayerPort);
            }, IntPtr.Zero);

            int hr = XNetworkingQueryPreferredLocalUdpMultiplayerPortAsync(block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static Int32 XNetworkingQuerySecurityInformationForUrlAsync(string url,
            XNetworkingQuerySecurityInformationForUrlResultCallback onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XNetworkingQuerySecurityInformationForUrlAsyncResultSize(_async, out ulong bufferSize);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                byte[] securityInformationBuffer = new byte[bufferSize];
                hr = XNetworkingQuerySecurityInformationForUrlAsyncResult(_async,
                    securityInformationBuffer,
                    out ulong securityInformationBufferByteCountUsed,
                    out XNetworkingSecurityInformation securityInformation);
                onCompleted(hr, securityInformation);
            }, IntPtr.Zero);

            int hr = XNetworkingQuerySecurityInformationForUrlAsync(url, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static Int32 XNetworkingQuerySecurityInformationForUrlUtf16Async(string url,
            XNetworkingQuerySecurityInformationForUrlResultCallback onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XNetworkingQuerySecurityInformationForUrlUtf16AsyncResultSize(_async, out ulong bufferSize);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                byte[] securityInformationBuffer = new byte[bufferSize];
                hr = XNetworkingQuerySecurityInformationForUrlUtf16AsyncResult(_async,
                    securityInformationBuffer,
                    out ulong securityInformationBufferByteCountUsed,
                    out XNetworkingSecurityInformation securityInformation);
                onCompleted(hr, securityInformation);
            }, IntPtr.Zero);

            int hr = XNetworkingQuerySecurityInformationForUrlUtf16Async(url, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static Int32 XNetworkingRegisterConnectivityHintChanged(
            XNetworkingConnectivityHintChangedCallback callback,
            out XNetworkingRegisterConnectivityHintChangedCallbackToken token)
        {
            return XNetworkingRegisterConnectivityHintChanged(defaultQueue, IntPtr.Zero, callback, out token);
        }

        public static Int32 XNetworkingRegisterPreferredLocalUdpMultiplayerPortChanged(
            XNetworkingPreferredLocalUdpMultiplayerPortChangedCallback callback,
            out XNetworkingPreferredLocalUdpMultiplayerPortChangedCallbackToken token)
        {
            return XNetworkingRegisterPreferredLocalUdpMultiplayerPortChanged(defaultQueue, IntPtr.Zero, callback, out token);
        }

        public static bool XNetworkingUnregisterPreferredLocalUdpMultiplayerPortChanged(XNetworkingPreferredLocalUdpMultiplayerPortChangedCallbackToken token)
        {
            return XNetworkingUnregisterPreferredLocalUdpMultiplayerPortChanged(token, true);
        }

        public static Int32 XPackageCreateInstallationMonitor(
            string packageIdentifier,
            XPackageChunkSelector[] selectors,
            UInt32 minimumUpdateIntervalMs,
            out XPackageInstallationMonitorHandle installationMonitor)
        {
            return XPackageCreateInstallationMonitor(packageIdentifier,
                selectors,
                minimumUpdateIntervalMs,
                defaultQueue,
                out installationMonitor);
        }

        public static Int32 XPackageCreateInstallationMonitor(
            string packageIdentifier,
            UInt32 minimumUpdateIntervalMs,
            out XPackageInstallationMonitorHandle installationMonitor)
        {
            return XPackageCreateInstallationMonitor(
                packageIdentifier,
                new XPackageChunkSelector[0],
                minimumUpdateIntervalMs,
                out installationMonitor);
        }

        public static Int32 XPackageInstallChunksAsync(string packageIdentifier,
            XPackageChunkSelector[] selectors,
            UInt32 minimumUpdateIntervalMs,
            bool suppressUserConfirmation,
            XPackageInstallChunksCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XPackageInstallChunksResult(_async, out XPackageInstallationMonitorHandle installationMonitor);
                onCompleted(hr, installationMonitor);
            }, IntPtr.Zero);

            int hr = XPackageInstallChunksAsync(packageIdentifier, selectors, minimumUpdateIntervalMs, suppressUserConfirmation, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        const string obsoleteXPackageMountMsg = "XPackageMount(string, out XPackageMountHandle) has been removed. Please use XPackageMountWithUiAsync(string packageIdentifier, XPackageMountWithUiAsyncCompleted) instead.";
        [Obsolete(obsoleteXPackageMountMsg, true)]
        public static Int32 XPackageMount(string packageIdentifier, out XPackageMountHandle mountHandle) => throw new NotSupportedException(obsoleteXPackageMountMsg);

        public static Int32 XPackageMountWithUiAsync(string packageIdentifier,
            XPackageMountWithUiAsyncCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XPackageMountWithUiResult(_async, out XPackageMountHandle mountHandle);
                onCompleted(hr, mountHandle);
            }, IntPtr.Zero);

            int hr = XPackageMountWithUiAsync(packageIdentifier, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static Int32 XPackageRegisterPackageInstalled(XPackageInstalledCallback callback, out XPackageRegisterPackageInstalledToken token)
        {
            return XPackageRegisterPackageInstalled(defaultQueue, IntPtr.Zero, callback, out token);
        }

        public static Int32 XPackageRegisterInstallationProgressChanged(
            XPackageInstallationMonitorHandle installationMonitor,
            XPackageInstallationProgressCallback callback,
            out XPackageRegisterInstallationProgressChangedToken token)
        {
            return XPackageRegisterInstallationProgressChanged(installationMonitor, IntPtr.Zero, callback, out token);
        }

        public static Int32 XPersistentLocalStoragePromptUserForSpaceAsync(ulong requestedBytes,
            XPersistentLocalStoragePromptUserForSpaceAsyncCallback onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XPersistentLocalStoragePromptUserForSpaceResult(_async);
                onCompleted(hr);
            }, IntPtr.Zero);

            int hr = XPersistentLocalStoragePromptUserForSpaceAsync(requestedBytes, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr);
            }

            return hr;
        }

        public static int XStoreAcquireLicenseForDurablesAsync(XStoreContext context,
            string storeId,
            XStoreAcquireLicenseForDurablesCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreAcquireLicenseForDurablesResult(_async, out XStoreLicense storeLicenseHandle);
                onCompleted(hr, storeLicenseHandle);
            }, IntPtr.Zero);

            int hr = XStoreAcquireLicenseForDurablesAsync(context, storeId, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XStoreAcquireLicenseForPackageAsync(XStoreContext context,
            string packageIdentifier,
            XStoreAcquireLicenseForPackageCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreAcquireLicenseForPackageResult(_async, out XStoreLicense storeLicenseHandle);
                onCompleted(hr, storeLicenseHandle);
            }, IntPtr.Zero);

            int hr = XStoreAcquireLicenseForPackageAsync(context, packageIdentifier, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XStoreCanAcquireLicenseForPackageAsync(XStoreContext storeContextHandle,
            string packageIdentifier,
            XStoreCanAcquireLicenseForPackageCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreCanAcquireLicenseForPackageResult(_async, out XStoreCanAcquireLicenseResult storeCanAcquireLicenseResult);
                onCompleted(hr, storeCanAcquireLicenseResult);
            }, IntPtr.Zero);

            int hr = XStoreCanAcquireLicenseForPackageAsync(storeContextHandle, packageIdentifier, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XStoreCanAcquireLicenseForStoreIdAsync(XStoreContext storeContextHandle,
            string storeProductId,
            XStoreCanAcquireLicenseForStoreIdCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreCanAcquireLicenseForStoreIdResult(_async, out XStoreCanAcquireLicenseResult storeCanAcquireLicenseResult);
                onCompleted(hr, storeCanAcquireLicenseResult);
            }, IntPtr.Zero);

            int hr = XStoreCanAcquireLicenseForStoreIdAsync(storeContextHandle, storeProductId, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static Int32 XStoreDownloadAndInstallPackagesAsync(XStoreContext storeContextHandle,
            string[] storeIds,
            XStoreDownloadAndInstallPackagesCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreDownloadAndInstallPackagesResultCount(_async, out uint count);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                hr = XStoreDownloadAndInstallPackagesResult(_async,
                    count,
                    out string[] packageIdentifiers);
                onCompleted(hr, packageIdentifiers);
            }, IntPtr.Zero);

            int hr = XStoreDownloadAndInstallPackagesAsync(storeContextHandle, storeIds, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XStoreDownloadAndInstallPackageUpdatesAsync(XStoreContext storeContextHandle,
            string[] packageIdentifiers,
            XStoreDownloadAndInstallPackageUpdatesCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreDownloadAndInstallPackageUpdatesResult(_async);
                onCompleted(hr);
            }, IntPtr.Zero);

            int hr = XStoreDownloadAndInstallPackageUpdatesAsync(storeContextHandle, packageIdentifiers, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr);
            }

            return hr;
        }

        public static int XStoreDownloadPackageUpdatesAsync(XStoreContext storeContextHandle,
            string[] packageIdentifiers,
            XStoreDownloadPackageUpdatesCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreDownloadPackageUpdatesResult(_async);
                onCompleted(hr);
            }, IntPtr.Zero);

            int hr = XStoreDownloadPackageUpdatesAsync(storeContextHandle, packageIdentifiers, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr);
            }

            return hr;
        }

        public static int XStoreGetUserCollectionsIdAsync(XStoreContext storeContextHandle,
            string serviceTicket,
            string publisherUserId,
            XStoreGetUserCollectionsIdCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreGetUserCollectionsIdResultSize(_async, out ulong size);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                hr = XStoreGetUserCollectionsIdResult(_async,
                    size,
                    out string result);
                onCompleted(hr, result);
            }, IntPtr.Zero);

            int hr = XStoreGetUserCollectionsIdAsync(storeContextHandle, serviceTicket, publisherUserId, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XStoreGetUserPurchaseIdAsync(XStoreContext storeContextHandle,
            string serviceTicket,
            string publisherUserId,
            XStoreGetUserPurchaseIdCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreGetUserPurchaseIdResultSize(_async, out ulong size);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                hr = XStoreGetUserPurchaseIdResult(_async,
                    size,
                    out string result);
                onCompleted(hr, result);
            }, IntPtr.Zero);

            int hr = XStoreGetUserPurchaseIdAsync(storeContextHandle, serviceTicket, publisherUserId, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XStoreProductsQueryNextPageAsync(XStoreProductQuery productQueryHandle,
            XStoreQueryComplete onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreProductsQueryNextPageResult(_async, out XStoreProductQuery productQueryHandle);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                hr = SDK.XStoreEnumerateProductsQuery(productQueryHandle, out XStoreProduct[] pageItems);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                bool hasMorePages = SDK.XStoreProductsQueryHasMorePages(productQueryHandle);
                XStoreQueryResult result = new XStoreQueryResult(productQueryHandle, pageItems, hasMorePages);
                onCompleted(hr, result);
            }, IntPtr.Zero);

            int hr = XStoreProductsQueryNextPageAsync(productQueryHandle, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XStoreProductsQueryNextPageAsync(XStoreQueryResult currentPage, XStoreQueryComplete completionRoutine)
        {
            return XStoreProductsQueryNextPageAsync(currentPage.QueryHandle, completionRoutine);
        }

        public static int XStoreQueryAssociatedProductsAsync(XStoreContext storeContextHandle,
            XStoreProductKind productKinds,
            UInt32 maxItemsToRetrievePerPage,
            Action<int, XStoreProductQuery> onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreQueryAssociatedProductsResult(_async, out XStoreProductQuery productQueryHandle);
                onCompleted(hr, productQueryHandle);
            }, IntPtr.Zero);

            int hr = XStoreQueryAssociatedProductsAsync(storeContextHandle, productKinds, maxItemsToRetrievePerPage, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XStoreQueryAssociatedProductsAsync(XStoreContext storeContextHandle,
            XStoreProductKind productKinds,
            UInt32 maxItemsToRetrievePerPage,
            XStoreQueryComplete onCompleted)
        {
            return XStoreQueryAssociatedProductsAsync(storeContextHandle,
                productKinds,
                maxItemsToRetrievePerPage,
                (int hr, XStoreProductQuery queryResult) =>
                {
                    if (HR.FAILED(hr))
                    {
                        onCompleted(hr, default);
                        return;
                    }

                    hr = SDK.XStoreEnumerateProductsQuery(queryResult, out XStoreProduct[] pageItems);
                    if (HR.FAILED(hr))
                    {
                        onCompleted(hr, default);
                        return;
                    }

                    bool hasMorePages = SDK.XStoreProductsQueryHasMorePages(queryResult);
                    XStoreQueryResult result = new XStoreQueryResult(queryResult, pageItems, hasMorePages);
                    onCompleted(hr, result);
                });
        }

        public static void XStoreCloseProductsQueryHandle(XStoreQueryResult result)
        {
            XStoreCloseProductsQueryHandle(result.QueryHandle);
        }

        public static int XStoreQueryAddOnLicensesAsync(XStoreContext storeContextHandle,
            XStoreQueryAddOnLicensesCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreQueryAddOnLicensesResultCount(_async, out uint count);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                XStoreAddonLicense[] addOnLicenses = new XStoreAddonLicense[count];
                hr = XStoreQueryAddOnLicensesResult(_async, addOnLicenses);
                onCompleted(hr, addOnLicenses);
            }, IntPtr.Zero);

            int hr = XStoreQueryAddOnLicensesAsync(storeContextHandle, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XStoreQueryConsumableBalanceRemainingAsync(XStoreContext storeContextHandle,
            string storeProductId,
            XStoreQueryConsumableBalanceRemainingCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreQueryConsumableBalanceRemainingResult(_async, out XStoreConsumableResult consumableResult);
                onCompleted(hr, consumableResult);
            }, IntPtr.Zero);

            int hr = XStoreQueryConsumableBalanceRemainingAsync(storeContextHandle, storeProductId, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XStoreQueryEntitledProductsAsync(XStoreContext storeContextHandle,
            XStoreProductKind productKinds,
            UInt32 maxItemsToRetrievePerPage,
            XStoreQueryComplete onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreQueryEntitledProductsResult(_async, out XStoreProductQuery productQueryHandle);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                hr = XStoreEnumerateProductsQuery(productQueryHandle, out XStoreProduct[] products);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                bool hasMorePages = SDK.XStoreProductsQueryHasMorePages(productQueryHandle);
                var result = new XStoreQueryResult(productQueryHandle, products, hasMorePages);
                onCompleted(hr, result);
            }, IntPtr.Zero);

            int hr = XStoreQueryEntitledProductsAsync(storeContextHandle, productKinds, maxItemsToRetrievePerPage, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XStoreQueryGameAndDlcPackageUpdatesAsync(XStoreContext storeContextHandle,
            XStoreQueryGameAndDlcPackageUpdatesCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreQueryGameAndDlcPackageUpdatesResultCount(_async, out uint count);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                XStorePackageUpdate[] packageUpdates = new XStorePackageUpdate[count];
                hr = XStoreQueryGameAndDlcPackageUpdatesResult(_async, packageUpdates);
                onCompleted(hr, packageUpdates);
            }, IntPtr.Zero);

            int hr = XStoreQueryGameAndDlcPackageUpdatesAsync(storeContextHandle, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XStoreQueryGameLicenseAsync(XStoreContext storeContextHandle,
            XStoreQueryGameLicenseCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreQueryGameLicenseResult(_async, out XStoreGameLicense license);
                onCompleted(hr, license);
            }, IntPtr.Zero);

            int hr = XStoreQueryGameLicenseAsync(storeContextHandle, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XStoreQueryLicenseTokenAsync(XStoreContext storeContextHandle,
            string[] productIds,
            string customDeveloperString,
            XStoreQueryLicenseTokenCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreQueryLicenseTokenResultSize(_async, out ulong size);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                hr = XStoreQueryLicenseTokenResult(_async, size, out string result);
                onCompleted(hr, result);
            }, IntPtr.Zero);

            int hr = XStoreQueryLicenseTokenAsync(storeContextHandle, productIds, customDeveloperString, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        [Obsolete("Please use XStoreQueryLicenseTokenAsync(XStoreContext, string[], string, XStoreQueryLicenseTokenCompleted) instead.", false)]
        public static void XStoreQueryLicenseTokenAsync(XStoreContext context, string[] productIds, UInt32 productIdsCount, string customDeveloperString, XStoreQueryLicenseTokenCompleted completionRoutine)
        {
            XStoreQueryLicenseTokenAsync(context, productIds, customDeveloperString, completionRoutine);
        }

        public static int XStoreQueryProductForCurrentGameAsync(XStoreContext storeContextHandle,
            XStoreQueryComplete onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreQueryProductForCurrentGameResult(_async, out XStoreProductQuery productQueryHandle);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                hr = XStoreEnumerateProductsQuery(productQueryHandle, out XStoreProduct[] products);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                bool hasMorePages = SDK.XStoreProductsQueryHasMorePages(productQueryHandle);
                var result = new XStoreQueryResult(productQueryHandle, products, hasMorePages);
                onCompleted(hr, result);
            }, IntPtr.Zero);

            int hr = XStoreQueryProductForCurrentGameAsync(storeContextHandle, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XStoreQueryProductForPackageAsync(XStoreContext storeContextHandle,
            XStoreProductKind productKinds,
            string packageIdentifier,
            XStoreQueryComplete onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreQueryProductForPackageResult(_async, out XStoreProductQuery productQueryHandle);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                hr = XStoreEnumerateProductsQuery(productQueryHandle, out XStoreProduct[] products);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                bool hasMorePages = SDK.XStoreProductsQueryHasMorePages(productQueryHandle);
                var result = new XStoreQueryResult(productQueryHandle, products, hasMorePages);
                onCompleted(hr, result);
            }, IntPtr.Zero);

            int hr = XStoreQueryProductForPackageAsync(storeContextHandle, productKinds, packageIdentifier, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XStoreQueryProductsAsync(XStoreContext storeContextHandle,
            XStoreProductKind productKinds,
            string[] storeIds,
            string[] actionFilters,
            XStoreQueryComplete onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreQueryProductsResult(_async, out XStoreProductQuery productQueryHandle);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                hr = XStoreEnumerateProductsQuery(productQueryHandle, out XStoreProduct[] products);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                bool hasMorePages = SDK.XStoreProductsQueryHasMorePages(productQueryHandle);
                var result = new XStoreQueryResult(productQueryHandle, products, hasMorePages);
                onCompleted(hr, result);
            }, IntPtr.Zero);

            int hr = XStoreQueryProductsAsync(storeContextHandle, productKinds, storeIds, actionFilters, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;

        }

        public static Int32 XStoreRegisterGameLicenseChanged(XStoreContext context, XStoreGameLicenseChangedCallback callback, out GameLicenseChangedCallbackToken token)
        {
            return XStoreRegisterGameLicenseChanged(context, defaultQueue, IntPtr.Zero, callback, out token);
        }

        public static void XStoreUnregisterGameLicenseChanged(XStoreContext context, GameLicenseChangedCallbackToken token)
        {
            token.Unregister(true);
        }

        public static Int32 XStoreRegisterPackageLicenseLost(XStoreLicense license, XStorePackageLicenseLostCallback callback, out PackageLicenseLostCallbackToken token)
        {
            return XStoreRegisterPackageLicenseLost(license, defaultQueue, IntPtr.Zero, callback, out token);
        }

        public static void XStoreUnregisterPackageLicenseLost(XStoreLicense license, PackageLicenseLostCallbackToken token)
        {
            token.Unregister(true);
        }

        public static int XStoreReportConsumableFulfillmentAsync(XStoreContext storeContextHandle,
            string storeProductId,
            UInt32 quantity,
            Guid trackingId,
            XStoreReportConsumableFulfillmentCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreReportConsumableFulfillmentResult(_async, out XStoreConsumableResult consumableResult);
                onCompleted(hr, consumableResult);
            }, IntPtr.Zero);

            int hr = XStoreReportConsumableFulfillmentAsync(storeContextHandle, storeProductId, quantity, trackingId, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XStoreShowAssociatedProductsUIAsync(XStoreContext storeContextHandle,
            string storeId,
            XStoreProductKind productKinds,
            XStoreShowAssociatedProductsUICompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreShowAssociatedProductsUIResult(_async);
                onCompleted(hr);
            }, IntPtr.Zero);

            int hr = XStoreShowAssociatedProductsUIAsync(storeContextHandle, storeId, productKinds, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr);
            }

            return hr;
        }

        public static int XStoreShowProductPageUIAsync(XStoreContext storeContextHandle,
            string storeId,
            XStoreShowProductPageUICompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreShowProductPageUIResult(_async);
                onCompleted(hr);
            }, IntPtr.Zero);

            int hr = XStoreShowProductPageUIAsync(storeContextHandle, storeId, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr);
            }

            return hr;
        }

        public static int XStoreShowPurchaseUIAsync(XStoreContext storeContextHandle,
            string storeId,
            string name,
            string extendedJsonData,
            XStoreShowPurchaseUICompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreShowPurchaseUIResult(_async);
                onCompleted(hr);
            }, IntPtr.Zero);

            int hr = XStoreShowPurchaseUIAsync(storeContextHandle, storeId, name, extendedJsonData, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr);
            }

            return hr;
        }

        public static int XStoreShowRateAndReviewUIAsync(XStoreContext storeContextHandle,
            XStoreShowRateAndReviewUICompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreShowRateAndReviewUIResult(_async, out XStoreRateAndReviewResult result);
                onCompleted(hr, result);
            }, IntPtr.Zero);

            int hr = XStoreShowRateAndReviewUIAsync(storeContextHandle, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XStoreShowRedeemTokenUIAsync(XStoreContext storeContextHandle,
            string token,
            string[] allowedStoreIds,
            bool disallowCsvRedemption,
            XStoreShowRedeemTokenUICompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XStoreShowRedeemTokenUIResult(_async);
                onCompleted(hr);
            }, IntPtr.Zero);

            int hr = XStoreShowRedeemTokenUIAsync(storeContextHandle, token, allowedStoreIds, disallowCsvRedemption, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr);
            }

            return hr;
        }

        public static int XUserAddByIdWithUiAsync(UInt64 userId,
            XUserAddCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XUserAddByIdWithUiResult(_async, out XUserHandle newUser);
                onCompleted(hr, newUser);
            }, IntPtr.Zero);

            int hr = XUserAddByIdWithUiAsync(userId, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XUserFindControllerForUserWithUiAsync(XUserHandle user,
            XUserFindControllerForUserWithUiResult onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XUserFindControllerForUserWithUiResult(_async, out APP_LOCAL_DEVICE_ID deviceId);
                onCompleted(hr, deviceId);
            }, IntPtr.Zero);

            int hr = XUserFindControllerForUserWithUiAsync(user, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XUserGetTokenAndSignatureUtf16Async(XUserHandle user,
            XUserGetTokenAndSignatureOptions options,
            string method,
            string url,
            XUserGetTokenAndSignatureUtf16HttpHeader[] headers,
            byte[] bodyBuffer,
            XUserGetTokenAndSignatureUtf16Result onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XUserGetTokenAndSignatureUtf16ResultSize(_async, out ulong size);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, default);
                    return;
                }

                byte[] buffer = new byte[size];
                hr = XUserGetTokenAndSignatureUtf16Result(_async, buffer, out XUserGetTokenAndSignatureUtf16Data result);
                onCompleted(hr, result);
            }, IntPtr.Zero);

            int hr = XUserGetTokenAndSignatureUtf16Async(user, options, method, url, headers, bodyBuffer, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }

            return hr;
        }

        public static int XUserResolveIssueWithUiUtf16Async(XUserHandle user,
            string url,
            XUserResolveIssueWithUiUtf16Result onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XUserResolveIssueWithUiUtf16Result(_async);
                onCompleted(hr);
            }, IntPtr.Zero);

            int hr = XUserResolveIssueWithUiUtf16Async(user, url, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr);
            }

            return hr;
        }

        public static int XUserResolvePrivilegeWithUiAsync(XUserHandle user,
            XUserPrivilegeOptions options,
            XUserPrivilege privilege,
            XUserResolvePrivilegeWithUiCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                int hr = XUserResolvePrivilegeWithUiResult(_async);
                onCompleted(hr);
            }, IntPtr.Zero);

            int hr = XUserResolvePrivilegeWithUiAsync(user, options, privilege, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr);
            }

            return hr;
        }

        public static int XUserGetGamerPictureAsync(XUserHandle user,
            XUserGamerPictureSize pictureSize,
            XUserGetGamerPictureCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                ulong bufferSize;
                int hr = XUserGetGamerPictureResultSize(_async, out bufferSize);
                if (HR.FAILED(hr))
                {
                    onCompleted(hr, null);
                    return;
                }

                ulong bufferUsed;
                byte[] buffer = new byte[bufferSize];
                hr = XUserGetGamerPictureResult(_async, buffer, out bufferUsed);
                onCompleted(hr, buffer);
            }, IntPtr.Zero);

            int hr = XUserGetGamerPictureAsync(user, pictureSize, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }
            return hr;
        }

        public static int XUserAddAsync(XUserAddOptions options,
            XUserAddCompleted onCompleted)
        {
            XAsyncBlock block = new XAsyncBlock(defaultQueue, (_async) =>
            {
                XUserHandle user;
                int hr = XUserAddResult(_async, out user);
                onCompleted(hr, user);
            }, IntPtr.Zero);

            int hr = XUserAddAsync(options, block);
            if (HR.FAILED(hr))
            {
                block.Dispose();
                onCompleted(hr, default);
            }
            return hr;
        }

        public static Int32 XAppBroadcastRegisterIsAppBroadcastingChanged(
            XAppBroadcastMonitorCallback callback,
            out XIsAppBroadcastingChangedRegistrationToken token)
        {
            return XAppBroadcastRegisterIsAppBroadcastingChanged(defaultQueue, callback, out token);
        }

        public static bool XAppBroadcastUnregisterIsAppBroadcastingChanged(XIsAppBroadcastingChangedRegistrationToken token)
        {
            return XAppBroadcastUnregisterIsAppBroadcastingChanged(token, true);
        }

        public static Int32 XAppCaptureRegisterMetadataPurged(XAppCaptureMetadataPurgedCallback callback,
            out XMetadataPurgedToken token)
        {
            return XAppCaptureRegisterMetadataPurged(defaultQueue, callback, out token);
        }

        public static bool XAppCaptureUnRegisterMetadataPurged(XMetadataPurgedToken token)
        {
            return XAppCaptureUnRegisterMetadataPurged(token, true);
        }

        public static Int32 XGameInviteRegisterForEvent(XGameInviteEventCallback callback, out XGameInviteRegistrationToken token)
        {
            return XGameInviteRegisterForEvent(defaultQueue, callback, out token);
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XStoreQueryResult
    {
        internal XStoreQueryResult(XStoreProductQuery queryHandle, XStoreProduct[] pageItems, bool hasMorePages)
        {
            this.QueryHandle = queryHandle;
            this.PageItems = pageItems;
            this.HasMorePages = hasMorePages;
        }

        internal XStoreProductQuery QueryHandle { get; private set; }

        public bool HasMorePages { get; private set; }

        public XStoreProduct[] PageItems { get; private set; }

        public static implicit operator XStoreProductQuery(XStoreQueryResult result) { return result.QueryHandle; }
    }
}
