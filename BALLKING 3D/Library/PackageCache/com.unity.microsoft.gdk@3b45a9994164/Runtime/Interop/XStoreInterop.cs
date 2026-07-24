// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace Unity.XGamingRuntime.Interop
{
    //struct XStoreCanAcquireLicenseResult
    //{
    //    _Field_z_ char licensableSku[SKU_ID_SIZE];
    //    XStoreCanLicenseStatus status;
    //};
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct XStoreCanAcquireLicenseResult
    {
        // #define SKU_ID_SIZE (5)
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)] internal string licensableSku;
        internal XStoreCanLicenseStatus status;
    }

    //struct XStoreImage
    //{
    //    _Field_z_ const char* uri;
    //    uint32_t height;
    //    uint32_t width;
    //    _Field_z_ const char* caption;
    //    _Field_z_ const char* imagePurposeTag;
    //};
    [StructLayout(LayoutKind.Sequential)]
    internal struct XStoreImage
    {
        [MarshalAs(UnmanagedType.LPStr)] internal string uri;
        internal UInt32 height;
        internal UInt32 width;
        [MarshalAs(UnmanagedType.LPStr)] internal string caption;
        [MarshalAs(UnmanagedType.LPStr)] internal string imagePurposeTag;
    }

    //#define PRICE_MAX_SIZE (16)

    //struct XStorePrice
    //{
    //    float basePrice;
    //    float price;
    //    float recurrencePrice;
    //    _Field_z_ const char* currencyCode;
    //    _Field_z_ char formattedBasePrice[PRICE_MAX_SIZE];
    //    _Field_z_ char formattedPrice[PRICE_MAX_SIZE];
    //    _Field_z_ char formattedRecurrencePrice[PRICE_MAX_SIZE];
    //    bool isOnSale;
    //    time_t saleEndDate;
    //};
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct XStorePrice
    {
        internal float basePrice;
        internal float price;
        internal float recurrencePrice;
        [MarshalAs(UnmanagedType.LPStr)] internal string currencyCode;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)] internal string formattedBasePrice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)] internal string formattedPrice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)] internal string formattedRecurrencePrice;
        [MarshalAs(UnmanagedType.I1)] internal bool isOnSale;
        internal Int64 saleEndDate;
    }

    //struct XStoreAvailability
    //{
    //    _Field_z_ const char* availabilityId;
    //    XStorePrice price;
    //    time_t endDate;
    //};
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct XStoreAvailability
    {
        [MarshalAs(UnmanagedType.LPStr)] internal string availabilityId;
        internal XStorePrice price;
        internal Int64 endDate;
    }

    //struct XStoreCollectionData
    //{
    //    time_t acquiredDate;
    //    time_t startDate;
    //    time_t endDate;
    //    bool isTrial;
    //    uint32_t trialTimeRemainingInSeconds;
    //    uint32_t quantity;
    //    _Field_z_ const char* campaignId;
    //    _Field_z_ const char* developerOfferId;
    //};
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct XStoreCollectionData
    {
        internal Int64 acquiredDate;
        internal Int64 startDate;
        internal Int64 endDate;
        [MarshalAs(UnmanagedType.I1)] internal bool isTrial;
        internal UInt32 trialTimeRemainingInSeconds;
        internal UInt32 quantity;
        [MarshalAs(UnmanagedType.LPStr)] internal string campaignId;
        [MarshalAs(UnmanagedType.LPStr)] internal string developerOfferId;
    }

    //struct XStoreSubscriptionInfo
    //{
    //    bool hasTrialPeriod;
    //    XStoreDurationUnit trialPeriodUnit;
    //    uint32_t trialPeriod;
    //    XStoreDurationUnit billingPeriodUnit;
    //    uint32_t billingPeriod;
    //};
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct XStoreSubscriptionInfo
    {
        [MarshalAs(UnmanagedType.I1)] internal bool hasTrialPeriod;
        internal XStoreDurationUnit trialPeriodUnit;
        internal UInt32 trialPeriod;
        internal XStoreDurationUnit billingPeriodUnit;
        internal UInt32 billingPeriod;
    }

    //struct XStoreVideo
    //{
    //    _Field_z_ const char* uri;
    //    uint32_t height;
    //    uint32_t width;
    //    _Field_z_ const char* caption;
    //    _Field_z_ const char* videoPurposeTag;
    //    XStoreImage previewImage;
    //};
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct XStoreVideo
    {
        [MarshalAs(UnmanagedType.LPStr)] internal string uri;
        internal UInt32 height;
        internal UInt32 width;
        [MarshalAs(UnmanagedType.LPStr)] internal string caption;
        [MarshalAs(UnmanagedType.LPStr)] internal string videoPurposeTag;
        internal XStoreImage previewImage;
    }

    //struct XStoreSku
    //{
    //    _Field_z_ const char* skuId;
    //    _Field_z_ const char* title;
    //    _Field_z_ const char* description;
    //    _Field_z_ const char* language;
    //    XStorePrice price;
    //    bool isTrial;
    //    bool isInUserCollection;
    //    XStoreCollectionData collectionData;
    //    bool isSubscription;
    //    XStoreSubscriptionInfo subscriptionInfo;
    //    uint32_t bundledSkusCount;
    //    _Field_z_ const char** bundledSkus;
    //    uint32_t imagesCount;
    //    XStoreImage* images;
    //    uint32_t videosCount;
    //    XStoreVideo* videos;
    //    uint32_t availabilitiesCount;
    //    XStoreAvailability* availabilities;
    //};
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct XStoreSkuInterop
    {
        [MarshalAs(UnmanagedType.LPStr)] internal string skuId;
        [MarshalAs(UnmanagedType.LPStr)] internal string title;
        [MarshalAs(UnmanagedType.LPStr)] internal string description;
        [MarshalAs(UnmanagedType.LPStr)] internal string language;
        internal XStorePrice price;
        [MarshalAs(UnmanagedType.I1)] internal bool isTrial;
        [MarshalAs(UnmanagedType.I1)] internal bool isInUserCollection;
        internal XStoreCollectionData collectionData;
        [MarshalAs(UnmanagedType.I1)] internal bool isSubscription;
        internal XStoreSubscriptionInfo subscriptionInfo;
        internal UInt32 bundledSkusCount;
        internal IntPtr bundledSkus;
        internal UInt32 imagesCount;
        internal IntPtr images;
        internal UInt32 videosCount;
        internal IntPtr videos;
        internal UInt32 availabilitiesCount;
        internal IntPtr availabilities;
    }

    //struct XStoreProduct
    //{
    //    _Field_z_ const char* storeId;
    //    _Field_z_ const char* title;
    //    _Field_z_ const char* description;
    //    _Field_z_ const char* language;
    //    _Field_z_ const char* inAppOfferToken;
    //    _Field_z_ char* linkUri;
    //    XStoreProductKind productKind;
    //    XStorePrice price;
    //    bool hasDigitalDownload;
    //    bool isInUserCollection;
    //    uint32_t keywordsCount;
    //    _Field_z_ const char** keywords;
    //    uint32_t skusCount;
    //    XStoreSku* skus;
    //    uint32_t imagesCount;
    //    XStoreImage* images;
    //    uint32_t videosCount;
    //    XStoreVideo* videos;
    //};
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct XStoreProductInterop
    {
        [MarshalAs(UnmanagedType.LPStr)] internal string storeId;
        [MarshalAs(UnmanagedType.LPStr)] internal string title;
        [MarshalAs(UnmanagedType.LPStr)] internal string description;
        [MarshalAs(UnmanagedType.LPStr)] internal string language;
        [MarshalAs(UnmanagedType.LPStr)] internal string inAppOfferToken;
        [MarshalAs(UnmanagedType.LPStr)] internal string linkUri;
        internal XStoreProductKind productKind;
        internal XStorePrice price;
        [MarshalAs(UnmanagedType.I1)] internal bool hasDigitalDownload;
        [MarshalAs(UnmanagedType.I1)] internal bool isInUserCollection;
        internal UInt32 keywordsCount;
        internal IntPtr keywords;
        internal UInt32 skusCount;
        internal IntPtr skus;
        internal UInt32 imagesCount;
        internal IntPtr images;
        internal UInt32 videosCount;
        internal IntPtr videos;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct XStorePackageIdentifierInterop
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)] internal string Data;
    }

    //struct XStoreAddonLicense
    //{
    //    _Field_z_ char skuStoreId[STORE_SKU_ID_SIZE];
    //    _Field_z_ char inAppOfferToken[IN_APP_OFFER_TOKEN_MAX_SIZE];
    //    bool isActive;
    //    time_t expirationDate;
    //};
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct XStoreAddonLicense
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)] internal string skuStoreId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)] internal string inAppOfferToken;
        [MarshalAs(UnmanagedType.I1)] internal bool isActive;
        internal Int64 expirationDate;
    }

    //struct XStoreConsumableResult
    //{
    //    uint32_t quantity;
    //};
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct XStoreConsumableResult
    {
        internal UInt32 quantity;
    }

    //struct XStorePackageUpdate
    //{
    //    _Field_z_ char packageIdentifier[XPACKAGE_IDENTIFIER_MAX_LENGTH];
    //    bool isMandatory;
    //};
    internal struct XStorePackageUpdate
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)] internal string packageIdentifier;
        [MarshalAs(UnmanagedType.I1)] internal bool isMandatory;
    }

    //struct XStoreGameLicense
    //{
    //    _Field_z_ char skuStoreId[STORE_SKU_ID_SIZE];
    //    bool isActive;
    //    bool isTrialOwnedByThisUser;
    //    bool isDiscLicense;
    //    bool isTrial;
    //    uint32_t trialTimeRemainingInSeconds;
    //    _Field_z_ char trialUniqueId[TRIAL_UNIQUE_ID_MAX_SIZE];
    //    time_t expirationDate;
    //};
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct XStoreGameLicense
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)] internal string skuStoreId;
        [MarshalAs(UnmanagedType.I1)] internal bool isActive;
        [MarshalAs(UnmanagedType.I1)] internal bool isTrialOwnedByThisUser;
        [MarshalAs(UnmanagedType.I1)] internal bool isDiscLicense;
        [MarshalAs(UnmanagedType.I1)] internal bool isTrial;
        internal UInt32 trialTimeRemainingInSeconds;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)] internal string trialUniqueId;
        internal Int64 expirationDate;
    }

    //struct XStoreRateAndReviewResult
    //{
    //    bool wasUpdated;
    //};
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct XStoreRateAndReviewResult
    {
        [MarshalAs(UnmanagedType.I1)] internal bool wasUpdated;
    }

    internal class GameLicenseChangedCallbackToken : XRegistrationToken<Interop.XStoreGameLicenseChangedCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XStoreGameLicenseChangedCallback))]
        static void OnGameLicenseChanged(IntPtr context)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XStoreGameLicenseChangedCallback>;
            wrapper.Callback(wrapper.Context);
        }

        XStoreContext storeContext;

        public GameLicenseChangedCallbackToken(XStoreContext storeContext,
            Interop.XStoreGameLicenseChangedCallback callback,
            IntPtr context) : base(callback, context, OnGameLicenseChanged)
        {
            this.storeContext = storeContext;
        }

        public bool Unregister(bool wait)
        {
            bool result = true;
            if (this.Token != 0)
            {
                result = NativeMethods.XStoreUnregisterGameLicenseChanged(this.storeContext.Handle, this.Token, wait);
                this.Token = 0;
            }

            return result;
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister(true);
        }
    }

    internal class PackageLicenseLostCallbackToken : XRegistrationToken<Interop.XStorePackageLicenseLostCallback>
    {
        [AOT.MonoPInvokeCallback(typeof(Interop.XStorePackageLicenseLostCallback))]
        static void OnPackageLicenseLostCallback(IntPtr context)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(context);
            var wrapper = gcHandle.Target as CallbackWrapper<Interop.XStorePackageLicenseLostCallback>;
            wrapper.Callback(wrapper.Context);
        }

        XStoreLicense licenseHandle;

        public PackageLicenseLostCallbackToken(XStoreLicense licenseHandle,
            Interop.XStorePackageLicenseLostCallback callback,
            IntPtr context) : base(callback, context, OnPackageLicenseLostCallback)
        {
            this.licenseHandle = licenseHandle;
        }

        public bool Unregister(bool wait)
        {
            bool result = true;
            if (this.Token != 0)
            {
                result = NativeMethods.XStoreUnregisterPackageLicenseLost(this.licenseHandle.Handle, this.Token, wait);
                this.Token = 0;
            }

            return result;
        }

        protected override void DisposeInternal(bool disposing)
        {
            Unregister(true);
        }
    }

    //typedef bool CALLBACK XStoreProductQueryCallback(_In_ const XStoreProduct* product, _In_opt_ void* context);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.I1)]
    internal delegate bool XStoreProductQueryCallbackInterop([In] ref XStoreProductInterop product, IntPtr context);

    //typedef void CALLBACK XStorePackageLicenseLostCallback(_In_ void* context);
    internal delegate void XStorePackageLicenseLostCallback(IntPtr context);

    //typedef void CALLBACK XStoreGameLicenseChangedCallback(_In_ void* context);
    internal delegate void XStoreGameLicenseChangedCallback(IntPtr context);

    partial class NativeMethods
    {
        //STDAPI XStoreCreateContext(
        //    _In_opt_ const XUserHandle user,
        //    _Out_ XStoreContextHandle* storeContextHandle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreCreateContext(IntPtr user, out IntPtr storeContextHandle);

        //STDAPI_(void) XStoreCloseContextHandle(
        //    _In_ XStoreContextHandle storeContextHandle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XStoreCloseContextHandle(IntPtr storeContextHandle);

        //STDAPI_(void) XStoreCloseLicenseHandle(
        //    _In_ XStoreLicenseHandle storeLicenseHandle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XStoreCloseLicenseHandle(IntPtr storeLicenseHandle);

        //STDAPI XStoreAcquireLicenseForDurablesAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_z_ const char* storeId,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreAcquireLicenseForDurablesAsync(IntPtr storeContextHandle,
            [MarshalAs(UnmanagedType.LPStr)] string storeId,
            IntPtr async);

        //STDAPI XStoreAcquireLicenseForDurablesResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XStoreLicenseHandle* storeLicenseHandle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreAcquireLicenseForDurablesResult(IntPtr async,
            out IntPtr storeLicenseHandle);

        //STDAPI XStoreAcquireLicenseForPackageAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_z_ const char* packageIdentifier,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreAcquireLicenseForPackageAsync(IntPtr storeContextHandle,
            [MarshalAs(UnmanagedType.LPStr)] string packageIdentifier,
            IntPtr async);

        //STDAPI XStoreAcquireLicenseForPackageResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XStoreLicenseHandle* storeLicenseHandle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreAcquireLicenseForPackageResult(IntPtr async,
            out IntPtr storeLicenseHandle);

        //STDAPI XStoreCanAcquireLicenseForStoreIdAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_z_ const char* storeProductId,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreCanAcquireLicenseForStoreIdAsync(IntPtr storeContextHandle,
            [MarshalAs(UnmanagedType.LPStr)] string storeProductId,
            IntPtr async);

        //STDAPI XStoreCanAcquireLicenseForStoreIdResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XStoreCanAcquireLicenseResult* storeCanAcquireLicense
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreCanAcquireLicenseForStoreIdResult(IntPtr async,
            out XStoreCanAcquireLicenseResult storeCanAcquireLicenseResult);

        //STDAPI XStoreCanAcquireLicenseForPackageAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_z_ const char* packageIdentifier,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreCanAcquireLicenseForPackageAsync(IntPtr storeContextHandle,
            [MarshalAs(UnmanagedType.LPStr)] string packageIdentifier,
            IntPtr async);

        //STDAPI XStoreCanAcquireLicenseForPackageResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XStoreCanAcquireLicenseResult* storeCanAcquireLicense
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreCanAcquireLicenseForPackageResult(IntPtr async,
            out XStoreCanAcquireLicenseResult storeCanAcquireLicenseResult);

        //STDAPI XStoreQueryProductForCurrentGameAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryProductForCurrentGameAsync(IntPtr storeContextHandle,
            IntPtr async);

        //STDAPI XStoreQueryProductForCurrentGameResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XStoreProductQueryHandle* productQueryHandle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryProductForCurrentGameResult(IntPtr async,
            out IntPtr productQueryHandle);

        //STDAPI XStoreEnumerateProductsQuery(
        //  _In_ const XStoreProductQueryHandle productQueryHandle,
        //  _In_opt_ void* context,
        //  _In_ XStoreProductQueryCallback* callback
        //  ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreEnumerateProductsQuery(IntPtr productQueryHandle,
            IntPtr context,
            XStoreProductQueryCallbackInterop callback);

        //STDAPI_(void) XStoreCloseProductsQueryHandle(
        //    _In_ XStoreProductQueryHandle productQueryHandle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern void XStoreCloseProductsQueryHandle(IntPtr productQueryHandle);

        //STDAPI XStoreDownloadPackageUpdatesAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_z_count_(packageIdentifiersCount) const char** packageIdentifiers,
        //    _In_ size_t packageIdentifiersCount,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreDownloadPackageUpdatesAsync(IntPtr storeContextHandle,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 2)] string[] packageIdentifiers,
            UInt64 packageIdentifiersCount,
            IntPtr async);

        //STDAPI XStoreDownloadPackageUpdatesResult(
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreDownloadPackageUpdatesResult(IntPtr async);

        //STDAPI XStoreDownloadAndInstallPackageUpdatesAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_z_count_(packageIdentifiersCount) const char** packageIdentifiers,
        //    _In_ size_t packageIdentifiersCount,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreDownloadAndInstallPackageUpdatesAsync(IntPtr storeContextHandle,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 2)] string[] packageIdentifiers,
            UInt64 packageIdentifiersCount,
            IntPtr async);

        //STDAPI XStoreDownloadAndInstallPackageUpdatesResult(
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreDownloadAndInstallPackageUpdatesResult(IntPtr async);

        //STDAPI XStoreDownloadAndInstallPackagesAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_z_count_(storeIdsCount) const char** storeIds,
        //    _In_ size_t storeIdsCount,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreDownloadAndInstallPackagesAsync(IntPtr storeContextHandle,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 2)] string[] storeIds,
            UInt64 storeIdsCount,
            IntPtr async);

        //STDAPI XStoreDownloadAndInstallPackagesResultCount(
        //    _In_ XAsyncBlock* async,
        //    _Out_ uint32_t* count
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreDownloadAndInstallPackagesResultCount(IntPtr async,
            out UInt32 count);

        //STDAPI XStoreDownloadAndInstallPackagesResult(
        //    _Inout_ XAsyncBlock* async,
        //    _In_ uint32_t count,
        //    _Out_writes_z_(count) char packageIdentifiers[][XPACKAGE_IDENTIFIER_MAX_LENGTH]
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreDownloadAndInstallPackagesResult(IntPtr async,
            UInt32 count,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] XStorePackageIdentifierInterop[] identifiers);

        //STDAPI XStoreGetUserCollectionsIdAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_z_ const char* serviceTicket,
        //    _In_z_ const char* publisherUserId,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreGetUserCollectionsIdAsync(IntPtr storeContextHandle,
            [MarshalAs(UnmanagedType.LPStr)] string serviceTicket,
            [MarshalAs(UnmanagedType.LPStr)] string publisherUserId,
            IntPtr async);

        //STDAPI XStoreGetUserCollectionsIdResultSize(
        //    _In_ XAsyncBlock* async,
        //    _Out_ size_t* size
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreGetUserCollectionsIdResultSize(IntPtr async,
            out UInt64 size);

        //STDAPI XStoreGetUserCollectionsIdResult(
        //    _Inout_ XAsyncBlock* async,
        //    _In_ size_t size,
        //    _Out_writes_z_(size) char* result
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreGetUserCollectionsIdResult(IntPtr async,
            UInt64 size,
            StringBuilder result);

        //STDAPI XStoreGetUserPurchaseIdAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_z_ const char* serviceTicket,
        //    _In_z_ const char* publisherUserId,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreGetUserPurchaseIdAsync(IntPtr storeContextHandle,
            [MarshalAs(UnmanagedType.LPStr)] string serviceTicket,
            [MarshalAs(UnmanagedType.LPStr)] string publisherUserId,
            IntPtr async);

        //STDAPI XStoreGetUserPurchaseIdResultSize(
        //    _In_ XAsyncBlock* async,
        //    _Out_ size_t* size
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreGetUserPurchaseIdResultSize(IntPtr async,
            out UInt64 size);

        //STDAPI XStoreGetUserPurchaseIdResult(
        //    _Inout_ XAsyncBlock* async,
        //    _In_ size_t size,
        //    _Out_writes_z_(size) char* result
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreGetUserPurchaseIdResult(IntPtr async,
            UInt64 size,
            StringBuilder result);

        //STDAPI_(bool) XStoreIsAvailabilityPurchasable(
        //    _In_ const XStoreAvailability availability
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XStoreIsAvailabilityPurchasable(XStoreAvailability availability);

        //STDAPI_(bool) XStoreIsLicenseValid(
        //    _In_ const XStoreLicenseHandle storeLicenseHandle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XStoreIsLicenseValid(IntPtr storeLicenseHandle);

        //STDAPI_(bool) XStoreProductsQueryHasMorePages(
        //    _In_ const XStoreProductQueryHandle productQueryHandle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XStoreProductsQueryHasMorePages(IntPtr productQueryHandle);

        //STDAPI XStoreProductsQueryNextPageAsync(
        //    _In_ const XStoreProductQueryHandle productQueryHandle,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreProductsQueryNextPageAsync(IntPtr productQueryHandle,
            IntPtr async);

        //STDAPI XStoreProductsQueryNextPageResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XStoreProductQueryHandle* productQueryHandle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreProductsQueryNextPageResult(IntPtr async,
            out IntPtr productQueryHandle);

        //STDAPI XStoreQueryAddOnLicensesAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryAddOnLicensesAsync(IntPtr storeContextHandle,
            IntPtr async);

        //STDAPI XStoreQueryAddOnLicensesResultCount(
        //    _In_ XAsyncBlock* async,
        //    _Out_ uint32_t* count
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryAddOnLicensesResultCount(IntPtr async,
            out UInt32 count);

        //STDAPI XStoreQueryAddOnLicensesResult(
        //    _Inout_ XAsyncBlock* async,
        //    _In_ uint32_t count,
        //    _Out_writes_(count) XStoreAddonLicense* addOnLicenses
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryAddOnLicensesResult(IntPtr async,
            UInt32 count,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] XStoreAddonLicense[] addOnLicenses);

        //STDAPI XStoreQueryAssociatedProductsAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_ XStoreProductKind productKinds,
        //    _In_ uint32_t maxItemsToRetrievePerPage,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryAssociatedProductsAsync(IntPtr storeContextHandle,
            XStoreProductKind productKinds,
            UInt32 maxItemsToRetrievePerPage,
            IntPtr async);

        //STDAPI XStoreQueryAssociatedProductsResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XStoreProductQueryHandle* productQueryHandle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryAssociatedProductsResult(IntPtr async,
            out IntPtr productQueryHandle);

        //STDAPI XStoreQueryConsumableBalanceRemainingAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_z_ const char* storeProductId,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryConsumableBalanceRemainingAsync(IntPtr storeContextHandle,
            [MarshalAs(UnmanagedType.LPStr)] string storeProductId,
            IntPtr async);

        //STDAPI XStoreQueryConsumableBalanceRemainingResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XStoreConsumableResult* consumableResult
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryConsumableBalanceRemainingResult(IntPtr async,
            out XStoreConsumableResult consumableResult);

        //STDAPI XStoreQueryEntitledProductsAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_ XStoreProductKind productKinds,
        //    _In_ uint32_t maxItemsToRetrievePerPage,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryEntitledProductsAsync(IntPtr storeContextHandle,
            XStoreProductKind productKinds,
            UInt32 maxItemsToRetrievePerPage,
            IntPtr async);

        //STDAPI XStoreQueryEntitledProductsResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XStoreProductQueryHandle* productQueryHandle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryEntitledProductsResult(IntPtr async,
            out IntPtr productQueryHandle);

        //STDAPI XStoreQueryGameAndDlcPackageUpdatesAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryGameAndDlcPackageUpdatesAsync(IntPtr storeContextHandle,
            IntPtr async);

        //STDAPI XStoreQueryGameAndDlcPackageUpdatesResultCount(
        //    _In_ XAsyncBlock* async,
        //    _Out_ uint32_t* count
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryGameAndDlcPackageUpdatesResultCount(IntPtr async,
            out UInt32 count);

        //STDAPI XStoreQueryGameAndDlcPackageUpdatesResult(
        //    _Inout_ XAsyncBlock* async,
        //    _In_ uint32_t count,
        //    _Out_writes_(count) XStorePackageUpdate* packageUpdates
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryGameAndDlcPackageUpdatesResult(IntPtr async,
            UInt32 count,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] XStorePackageUpdate[] packageUpdates);

        //STDAPI XStoreQueryGameLicenseAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryGameLicenseAsync(IntPtr storeContextHandle,
            IntPtr async);

        //STDAPI XStoreQueryGameLicenseResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XStoreGameLicense* license
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryGameLicenseResult(IntPtr async,
            out XStoreGameLicense license);

        //STDAPI XStoreQueryLicenseTokenAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_z_count_(productIdsCount) const char** productIds,
        //    _In_ size_t productIdsCount,
        //    _In_z_ const char* customDeveloperString,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryLicenseTokenAsync(IntPtr storeContextHandle,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1, ArraySubType = UnmanagedType.LPStr)] string[] productIds,
            UInt64 productIdsCount,
            [MarshalAs(UnmanagedType.LPStr)] string customDeveloperString,
            IntPtr async);

        //STDAPI XStoreQueryLicenseTokenResultSize(
        //    _In_ XAsyncBlock* async,
        //    _Out_ size_t* size
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryLicenseTokenResultSize(IntPtr async,
            out UInt64 size);

        //STDAPI XStoreQueryLicenseTokenResult(
        //    _Inout_ XAsyncBlock* async,
        //    _In_ size_t size,
        //    _Out_writes_z_(size) char* result
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryLicenseTokenResult(IntPtr async,
            UInt64 size,
            StringBuilder result);

        //STDAPI XStoreQueryPackageIdentifier(
        //    _In_z_ const char* storeId,
        //    _In_ size_t size,
        //    _Out_writes_z_(size) char* packageIdentifier
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryPackageIdentifier(
            [MarshalAs(UnmanagedType.LPStr)] string storeId,
            UInt64 size,
            StringBuilder packageIdentifier);

        //STDAPI XStoreQueryProductForPackageAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_ XStoreProductKind productKinds,
        //    _In_z_ const char* packageIdentifier,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryProductForPackageAsync(IntPtr storeContextHandle,
            XStoreProductKind productKinds,
            [MarshalAs(UnmanagedType.LPStr)] string packageIdentifier,
            IntPtr async);

        //STDAPI XStoreQueryProductForPackageResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XStoreProductQueryHandle* productQueryHandle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryProductForPackageResult(IntPtr async,
            out IntPtr productQueryHandle);

        //STDAPI XStoreQueryProductsAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_ XStoreProductKind productKinds,
        //    _In_z_count_(storeIdsCount) const char** storeIds,
        //    _In_ size_t storeIdsCount,
        //    _In_opt_z_count_(actionFiltersCount) const char** actionFilters,
        //    _In_ size_t actionFiltersCount,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryProductsAsync(IntPtr storeContextHandle,
            XStoreProductKind productKinds,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] storeIds,
            UInt64 storeIdsCount,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 5)] string[] actionFilters,
            UInt64 actionFiltersCount,
            IntPtr async);

        //STDAPI XStoreQueryProductsResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XStoreProductQueryHandle* productQueryHandle
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreQueryProductsResult(IntPtr async,
            out IntPtr productQueryHandle);

        //STDAPI XStoreRegisterGameLicenseChanged(
        //    _In_ XStoreContextHandle storeContextHandle,
        //    _In_ XTaskQueueHandle queue,
        //    _In_opt_ void* context,
        //    _In_ XStoreGameLicenseChangedCallback* callback,
        //    _Out_ XTaskQueueRegistrationToken* token
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreRegisterGameLicenseChanged(IntPtr storeContextHandle,
            IntPtr queue,
            IntPtr context,
            XStoreGameLicenseChangedCallback callback,
            out UInt64 token);

        //STDAPI_(bool) XStoreUnregisterGameLicenseChanged(
        //    _In_ XStoreContextHandle storeContextHandle,
        //    _In_ XTaskQueueRegistrationToken token,
        //    _In_ bool wait
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XStoreUnregisterGameLicenseChanged(IntPtr storeContextHandle,
            UInt64 token,
            [MarshalAs(UnmanagedType.I1)] bool wait);

        //STDAPI XStoreRegisterPackageLicenseLost(
        //    _In_ XStoreLicenseHandle licenseHandle,
        //    _In_ XTaskQueueHandle queue,
        //    _In_opt_ void* context,
        //    _In_ XStorePackageLicenseLostCallback* callback,
        //    _Out_ XTaskQueueRegistrationToken* token
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreRegisterPackageLicenseLost(IntPtr licenseHandle,
            IntPtr queue,
            IntPtr context,
            XStorePackageLicenseLostCallback callback,
            out UInt64 token);

        //STDAPI_(bool) XStoreUnregisterPackageLicenseLost(
        //    _In_ XStoreLicenseHandle licenseHandle,
        //    _In_ XTaskQueueRegistrationToken token,
        //    _In_ bool wait
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool XStoreUnregisterPackageLicenseLost(IntPtr licenseHandle,
            UInt64 token,
            [MarshalAs(UnmanagedType.I1)] bool wait);

        //STDAPI XStoreReportConsumableFulfillmentAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_z_ const char* storeProductId,
        //    _In_ uint32_t quantity,
        //    _In_ GUID trackingId,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreReportConsumableFulfillmentAsync(IntPtr storeContextHandle,
            [MarshalAs(UnmanagedType.LPStr)] string storeProductId,
            UInt32 quantity,
            Guid trackingId,
            IntPtr async);

        //STDAPI XStoreReportConsumableFulfillmentResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XStoreConsumableResult* consumableResult
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreReportConsumableFulfillmentResult(IntPtr async,
            out XStoreConsumableResult consumableResult);

        //STDAPI XStoreShowAssociatedProductsUIAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_z_ const char* storeId,
        //    _In_ XStoreProductKind productKinds,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreShowAssociatedProductsUIAsync(IntPtr storeContextHandle,
            [MarshalAs(UnmanagedType.LPStr)] string storeId,
            XStoreProductKind productKinds,
            IntPtr async);

        //STDAPI XStoreShowAssociatedProductsUIResult(
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreShowAssociatedProductsUIResult(IntPtr async);

        //STDAPI XStoreShowProductPageUIAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_z_ const char* storeId,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreShowProductPageUIAsync(IntPtr storeContextHandle,
            [MarshalAs(UnmanagedType.LPStr)] string storeId,
            IntPtr async);

        //STDAPI XStoreShowProductPageUIResult(
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreShowProductPageUIResult(IntPtr async);

        //STDAPI XStoreShowPurchaseUIAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_z_ const char* storeId,
        //    _In_opt_z_ const char* name,
        //    _In_opt_z_ const char* extendedJsonData,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreShowPurchaseUIAsync(IntPtr storeContextHandle,
            [MarshalAs(UnmanagedType.LPStr)] string storeId,
            [MarshalAs(UnmanagedType.LPStr)] string name,
            [MarshalAs(UnmanagedType.LPStr)] string extendedJsonData,
            IntPtr async);

        //STDAPI XStoreShowPurchaseUIResult(
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreShowPurchaseUIResult(IntPtr async);

        //STDAPI XStoreShowRateAndReviewUIAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreShowRateAndReviewUIAsync(IntPtr storeContextHandle,
            IntPtr async);

        //STDAPI XStoreShowRateAndReviewUIResult(
        //    _Inout_ XAsyncBlock* async,
        //    _Out_ XStoreRateAndReviewResult* result
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreShowRateAndReviewUIResult(IntPtr async,
            out XStoreRateAndReviewResult result);

        //STDAPI XStoreShowRedeemTokenUIAsync(
        //    _In_ const XStoreContextHandle storeContextHandle,
        //    _In_z_ const char* token,
        //    _In_z_count_(allowedStoreIdsCount) const char** allowedStoreIds,
        //    _In_ size_t allowedStoreIdsCount,
        //    _In_ bool disallowCsvRedemption,
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreShowRedeemTokenUIAsync(IntPtr storeContextHandle,
            [MarshalAs(UnmanagedType.LPStr)] string token,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)] string[] allowedStoreIds,
            UInt64 allowedStoreIdsCount,
            [MarshalAs(UnmanagedType.I1)] bool disallowCsvRedemption,
            IntPtr async);

        //STDAPI XStoreShowRedeemTokenUIResult(
        //    _Inout_ XAsyncBlock* async
        //    ) noexcept;
        [DllImport(XGamingRuntimeInterop.ThunkDllName, CallingConvention = CallingConvention.StdCall)]
        internal static extern Int32 XStoreShowRedeemTokenUIResult(IntPtr async);
    }
}
