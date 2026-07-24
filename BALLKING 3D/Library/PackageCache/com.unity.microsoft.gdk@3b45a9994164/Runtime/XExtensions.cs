using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Unity.XGamingRuntime.Interop;
using UnityEngine;

// This file contains overloads for GDK wrappers that make calling certain APIs easier

namespace Unity.XGamingRuntime
{
    public partial class SDK
    {
        private const int XUserGamertagComponentClassicMaxBytes = 16;
        private const int XUserGamertagComponentModernMaxBytes = 97;
        private const int XUserGamertagComponentModernSuffixMaxBytes = 15;
        private const int XUserGamertagComponentUniqueModernMaxBytes = 101;

        public static int XGameSaveEnumerateBlobInfo(XGameSaveContainerHandle container, out XGameSaveBlobInfo[] blobInfos)
        {
            blobInfos = null;
            var results = new List<XGameSaveBlobInfo>();
            int hr = XGameSaveEnumerateBlobInfo(container, IntPtr.Zero, (_blobInfo, _context) =>
            {
                results.Add(_blobInfo);
                return true;
            });

            if (HR.SUCCEEDED(hr))
            {
                blobInfos = results.ToArray();
            }

            return hr;
        }

        public static int XGameSaveGetContainerInfo(XGameSaveProviderHandle provider,
            string containerName,
            out XGameSaveContainerInfo containerInfo)
        {
            containerInfo = default;
            XGameSaveContainerInfo info = default;
            int hr = XGameSaveGetContainerInfo(provider, containerName, IntPtr.Zero, (_info, _context) =>
            {
                info = _info;
                return true;
            });

            if (HR.SUCCEEDED(hr))
            {
                containerInfo = info;
            }
            return hr;
        }

        public static int XGameSaveEnumerateContainerInfo(XGameSaveProviderHandle provider,
            out XGameSaveContainerInfo[] localInfos)
        {
            localInfos = null;
            var results = new List<XGameSaveContainerInfo>();
            int hr = XGameSaveEnumerateContainerInfo(provider,
                IntPtr.Zero,
                (_info, _context) =>
                {
                    results.Add(_info);
                    return true;
                });
            if (HR.SUCCEEDED(hr))
            {
                localInfos = results.ToArray();
            }
            return hr;
        }

        public static int XGameSaveEnumerateContainerInfoByName(XGameSaveProviderHandle provider,
            string containerNamePrefix,
            out XGameSaveContainerInfo[] localInfos)
        {
            localInfos = null;
            var results = new List<XGameSaveContainerInfo>();
            int hr = XGameSaveEnumerateContainerInfoByName(provider,
                containerNamePrefix,
                IntPtr.Zero,
                (_info, _context) =>
                {
                    results.Add(_info);
                    return true;
                });
            if (HR.SUCCEEDED(hr))
            {
                localInfos = results.ToArray();
            }
            return hr;
        }

        public static int XGameSaveEnumerateBlobInfoByName(XGameSaveContainerHandle provider,
            string blobNamePrefix,
            out XGameSaveBlobInfo[] blobInfos)
        {
            blobInfos = null;
            var results = new List<XGameSaveBlobInfo>();
            int hr = XGameSaveEnumerateBlobInfoByName(provider,
                blobNamePrefix,
                IntPtr.Zero,
                (_info, _context) =>
                {
                    results.Add(_info);
                    return true;
                });
            if (HR.SUCCEEDED(hr))
            {
                blobInfos = results.ToArray();
            }
            return hr;
        }

        public static int XGameSaveReadBlobData(
            XGameSaveContainerHandle container,
            List<XGameSaveBlobInfo> blobInfos,
            out List<XGameSaveBlob> blobs)
        {
            blobs = null;
            int hr = XGameSaveReadBlobData(container, blobInfos.ToArray(), out XGameSaveBlob[] blobData);
            if (HR.SUCCEEDED(hr))
            {
                blobs = new List<XGameSaveBlob>(blobData);
            }

            return hr;
        }

        public static int XSpeechSynthesizerEnumerateInstalledVoices(out XSpeechSynthesizerVoiceInformation[] voiceInformation)
        {
            voiceInformation = null;
            List<XSpeechSynthesizerVoiceInformation> voices = new List<XSpeechSynthesizerVoiceInformation>();
            bool VoiceCallback(ref XSpeechSynthesizerVoiceInformation information, IntPtr context)
            {
                voices.Add(information);
                return true;
            }

            int hr = SDK.XSpeechSynthesizerEnumerateInstalledVoices(IntPtr.Zero, VoiceCallback);
            if (HR.SUCCEEDED(hr))
            {
                voiceInformation = voices.ToArray();
            }

            return hr;
        }

        public static int XSpeechSynthesizerGetStreamData(XSpeechSynthesizerStreamHandle speechSynthesisStream, out byte[] buffer)
        {
            buffer = null;
            int hr = SDK.XSpeechSynthesizerGetStreamDataSize(speechSynthesisStream, out ulong bufferSize);
            if (HR.FAILED(hr))
            {
                return hr;
            }

            buffer = new byte[bufferSize];
            return SDK.XSpeechSynthesizerGetStreamData(speechSynthesisStream, buffer, out ulong _);
        }

        public static int XStoreEnumerateProductsQuery(XStoreProductQuery productQueryHandle, out XStoreProduct[] products)
        {
            products = null;

            List<XStoreProduct> tmpProducts = new List<XStoreProduct>();
            bool OnProduct(XStoreProduct product, IntPtr _)
            {
                tmpProducts.Add(product);
                return true;
            }

            int hr = SDK.XStoreEnumerateProductsQuery(productQueryHandle, IntPtr.Zero, OnProduct);
            if (HR.SUCCEEDED(hr))
            {
                products = tmpProducts.ToArray();
            }

            return hr;
        }

        public static int XUserGetGamertag(XUserHandle user,
            XUserGamertagComponent gamertagComponent,
            out string gamertag)
        {
            gamertag = null;

            int size = 0;
            switch (gamertagComponent)
            {
                case XUserGamertagComponent.Classic:
                    size = XUserGamertagComponentClassicMaxBytes;
                    break;
                case XUserGamertagComponent.Modern:
                    size = XUserGamertagComponentModernMaxBytes;
                    break;
                case XUserGamertagComponent.ModernSuffix:
                    size = XUserGamertagComponentModernSuffixMaxBytes;
                    break;
                case XUserGamertagComponent.UniqueModern:
                    size = XUserGamertagComponentUniqueModernMaxBytes;
                    break;
                default:
                    return HR.E_INVALIDARG;
            }

            StringBuilder sb = new StringBuilder(size);
            int hr = XUserGetGamertag(user, gamertagComponent, sb, out ulong gamertagUsed);
            if (HR.SUCCEEDED(hr))
            {
                gamertag = sb.ToString();
            }

            return hr;
        }

        public static Int32 XAppCaptureRecordDiagnosticClip(
            System.DateTime startTime,
            uint durationInMS,
            string fileNamePrefix,
            out XAppCaptureRecordClipResult result)
        {
            // This needs to be a UTC date
            if (startTime.Kind != DateTimeKind.Utc)
            {
                startTime = startTime.ToUniversalTime();
            }

            return XAppCaptureRecordDiagnosticClip(new TimeT(startTime).SecondsSinceUnixEpoch, durationInMS, fileNamePrefix, out result);
        }

        public static Int32 XPackageGetMountPath(XPackageMountHandle mountHandle, out string path)
        {
            path = string.Empty;
            Int32 hr = XPackageGetMountPathSize(mountHandle, out ulong size);
            if (HR.FAILED(hr))
            {
                return hr;
            }

            return XPackageGetMountPath(mountHandle, size, out path);
        }

        public static Int32 XPackageEstimateDownloadSize(
            string packageIdentifier,
            out UInt64 downloadSize,
            out bool shouldPresentUserConfirmation)
        {
            return XPackageEstimateDownloadSize(packageIdentifier,
                new XPackageChunkSelector[0],
                out downloadSize,
                out shouldPresentUserConfirmation);
        }


        public static Int32 XPackageEnumerateChunkAvailability(
            string packageIdentifier,
            XPackageChunkSelectorType type,
            XPackageChunkAvailabilityCallback callback)
        {
            return XPackageEnumerateChunkAvailability(packageIdentifier, type, IntPtr.Zero, callback);
        }

        public static Int32 XPackageEnumeratePackages(XPackageKind kind, XPackageEnumerationScope scope, out XPackageDetails[] details)
        {
            details = null;
            List<XPackageDetails> results = new List<XPackageDetails>();

            int hr = XPackageEnumeratePackages(kind, scope, IntPtr.Zero, (IntPtr context, XPackageDetails details) =>
            {
                results.Add(details);
                return true;
            });

            if (HR.SUCCEEDED(hr))
            {
                details = results.ToArray();
            }
            return hr;
        }

        public static int XPackageEnumerateFeatures(string packageIdentifier, out XPackageFeature[] features)
        {
            features = null;
            List<XPackageFeature> results = new List<XPackageFeature>();

            int hr = XPackageEnumerateFeatures(packageIdentifier, IntPtr.Zero, (IntPtr context, XPackageFeature feature) =>
            {
                results.Add(feature);
                return true;
            });

            if (HR.SUCCEEDED(hr))
            {
                features = results.ToArray();
            }
            return hr;
        }

        public static Int32 XSpeechSynthesizerSetCustomVoice(
            XSpeechSynthesizerHandle speechSynthesizer,
            XSpeechSynthesizerVoiceInformation voiceInformation)
        {
            return XSpeechSynthesizerSetCustomVoice(speechSynthesizer, voiceInformation.VoiceId);
        }
    }
}
