using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
    //enum class XStoreCanLicenseStatus : uint32_t
    //{
    //    NotLicensableToUser                 = 0,
    //    Licensable                          = 1,
    //    LicenseActionNotApplicableToProduct = 2,
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XStoreCanLicenseStatus : UInt32
    {
        NotLicensableToUser = 0,
        Licensable = 1,
        LicenseActionNotApplicableToProduct = 2,
    }

    //enum class XStoreProductKind : uint32_t
    //{
    //    None                = 0x00,
    //    Consumable          = 0x01,
    //    Durable             = 0x02,
    //    Game                = 0x04,
    //    Pass                = 0x08,
    //    UnmanagedConsumable = 0x10,
    //};
    [Flags]
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XStoreProductKind : UInt32
    {
        None = 0x00,
        Consumable = 0x01,
        Durable = 0x02,
        Game = 0x04,
        Pass = 0x08,
        UnmanagedConsumable = 0x10,
    }

    //enum class XStoreDurationUnit : uint32_t
    //{
    //    Minute  = 0,
    //    Hour    = 1,
    //    Day     = 2,
    //    Week    = 3,
    //    Month   = 4,
    //    Year    = 5,
    //};
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public enum XStoreDurationUnit : UInt32
    {
        Minute = 0,
        Hour = 1,
        Day = 2,
        Week = 3,
        Month = 4,
        Year = 5,
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XStoreCanAcquireLicenseResult
    {
        internal XStoreCanAcquireLicenseResult(Interop.XStoreCanAcquireLicenseResult interop)
        {
            this.interop = interop;
        }

        public XStoreCanAcquireLicenseResult()
        {
            this.interop = new Interop.XStoreCanAcquireLicenseResult();
        }

        internal Interop.XStoreCanAcquireLicenseResult interop;

        public string LicensableSku
        {
            get => this.interop.licensableSku;
            set => this.interop.licensableSku = value;
        }

        public XStoreCanLicenseStatus Status
        {
            get => this.interop.status;
            set => this.interop.status = value;
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XStoreImage
    {
        internal XStoreImage(Interop.XStoreImage interop)
        {
            this.interop = interop;
        }


        public XStoreImage()
        {
            this.interop = new Interop.XStoreImage();
        }

        internal Interop.XStoreImage interop;

        public string Uri
        {
            get => this.interop.uri;
            set => this.interop.uri = value;
        }

        public UInt32 Height
        {
            get => this.interop.height;
            set => this.interop.height = value;
        }

        public UInt32 Width
        {
            get => this.interop.width;
            set => this.interop.width = value;
        }

        public string Caption
        {
            get => this.interop.caption;
            set => this.interop.caption = value;
        }

        public string ImagePurposeTag
        {
            get => this.interop.imagePurposeTag;
            set => this.interop.imagePurposeTag = value;
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XStorePrice
    {
        internal XStorePrice(Interop.XStorePrice interop)
        {
            this.interop = interop;
        }

        public XStorePrice()
        {
            this.interop = new Interop.XStorePrice();
        }

        internal Interop.XStorePrice interop;

        public float BasePrice
        {
            get => this.interop.basePrice;
            set => this.interop.basePrice = value;
        }

        public float Price
        {
            get => this.interop.price;
            set => this.interop.price = value;
        }

        public float RecurrencePrice
        {
            get => this.interop.recurrencePrice;
            set => this.interop.recurrencePrice = value;
        }

        public string CurrencyCode
        {
            get => this.interop.currencyCode;
            set => this.interop.currencyCode = value;
        }

        public string FormattedBasePrice
        {
            get => this.interop.formattedBasePrice;
            set => this.interop.formattedBasePrice = value;
        }

        public string FormattedPrice
        {
            get => this.interop.formattedPrice;
            set => this.interop.formattedPrice = value;
        }

        public string FormattedRecurrencePrice
        {
            get => this.interop.formattedRecurrencePrice;
            set => this.interop.formattedRecurrencePrice = value;
        }

        public bool IsOnSale
        {
            get => this.interop.isOnSale;
            set => this.interop.isOnSale = value;
        }

        public Int64 SaleEndDate
        {
            get => this.interop.saleEndDate;
            set => this.interop.saleEndDate = value;
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XStoreAvailability
    {
        internal XStoreAvailability(Interop.XStoreAvailability interop)
        {
            _xstorePrice = new XStorePrice(interop.price);
            this._interop = interop;
        }

        public XStoreAvailability()
        {
            this._xstorePrice = new XStorePrice();
            this._interop = new Interop.XStoreAvailability();
            this._interop.price = _xstorePrice.interop;
        }

        internal Interop.XStoreAvailability _interop;

        internal XStorePrice _xstorePrice;

        internal Interop.XStoreAvailability interop
        {
            get
            {
                _interop.price = _xstorePrice.interop;
                return _interop;
            }
            set => this._interop = value;
        }

        public string AvailabilityId
        {
            get => this._interop.availabilityId;
            set => this._interop.availabilityId = value;
        }

        public XStorePrice Price
        {
            get => _xstorePrice;

            set {
                _interop.price = value.interop;
                _xstorePrice = value;
            }
        }

        public Int64 EndDate
        {
            get => this._interop.endDate;
            set => this._interop.endDate = value;
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XStoreCollectionData
    {
        internal XStoreCollectionData(Interop.XStoreCollectionData interop)
        {
            this.interop = interop;
        }

        public XStoreCollectionData()
        {
            this.interop = new Interop.XStoreCollectionData();
        }

        internal Interop.XStoreCollectionData interop;

        public Int64 AcquiredDate
        {
            get => this.interop.acquiredDate;
            set => this.interop.acquiredDate = value;
        }

        public Int64 StartDate
        {
            get => this.interop.startDate;
            set => this.interop.startDate = value;
        }

        public Int64 EndDate
        {
            get => this.interop.endDate;
            set => this.interop.endDate = value;
        }

        public bool IsTrial
        {
            get => this.interop.isTrial;
            set => this.interop.isTrial = value;
        }

        public UInt32 TrialTimeRemainingInSeconds
        {
            get => this.interop.trialTimeRemainingInSeconds;
            set => this.interop.trialTimeRemainingInSeconds = value;
        }

        public UInt32 Quantity
        {
            get => this.interop.quantity;
            set => this.interop.quantity = value;
        }

        public string CampaignId
        {
            get => this.interop.campaignId;
            set => this.interop.campaignId = value;
        }

        public string DeveloperOfferId
        {
            get => this.interop.developerOfferId;
            set => this.interop.developerOfferId = value;
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XStoreSubscriptionInfo
    {
        internal XStoreSubscriptionInfo(Interop.XStoreSubscriptionInfo interop)
        {
            this.interop = interop;
        }

        public XStoreSubscriptionInfo()
        {
            this.interop = new Interop.XStoreSubscriptionInfo();
        }

        internal Interop.XStoreSubscriptionInfo interop;

        public bool HasTrialPeriod
        {
            get => this.interop.hasTrialPeriod;
            set => this.interop.hasTrialPeriod = value;
        }

        public XStoreDurationUnit TrialPeriodUnit
        {
            get => this.interop.trialPeriodUnit;
            set => this.interop.trialPeriodUnit = value;
        }

        public UInt32 TrialPeriod
        {
            get => this.interop.trialPeriod;
            set => this.interop.trialPeriod = value;
        }

        public XStoreDurationUnit BillingPeriodUnit
        {
            get => this.interop.billingPeriodUnit;
            set => this.interop.billingPeriodUnit = value;
        }

        public UInt32 BillingPeriod
        {
            get => this.interop.billingPeriod;
            set => this.interop.billingPeriod = value;
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XStoreVideo
    {
        internal XStoreVideo(Interop.XStoreVideo interop)
        {
            this._interop = interop;
            _previewImage = new XStoreImage(this._interop.previewImage);
        }

        public XStoreVideo()
        {
            this._interop = new Interop.XStoreVideo();
            _previewImage = new XStoreImage();
        }

        internal Interop.XStoreVideo _interop;

        internal Interop.XStoreVideo interop
        {
            get
            {
                _interop.previewImage = _previewImage.interop;
                return _interop;
            }
            set => this._interop = value;
        }

        internal XStoreImage _previewImage;

        public string Uri
        {
            get => this._interop.uri;
            set => this._interop.uri = value;
        }

        public UInt32 Height
        {
            get => this._interop.height;
            set => this._interop.height = value;
        }

        public UInt32 Width
        {
            get => this._interop.width;
            set => this._interop.width = value;
        }

        public string Caption
        {
            get => this._interop.caption;
            set => this._interop.caption = value;
        }

        public string VideoPurposeTag
        {
            get => this._interop.videoPurposeTag;
            set => this._interop.videoPurposeTag = value;
        }

        public XStoreImage PreviewImage
        {
            get => _previewImage;
            set {
                _interop.previewImage = value.interop;
                _previewImage = value;
            }
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XStoreContext : EquatableHandle
    {
        public XStoreContext(IntPtr handle) :
            base(IntPtr.Zero, true, handle)
        { }

        protected override bool ReleaseHandle()
        {
            Interop.NativeMethods.XStoreCloseContextHandle(this.handle);
            SetHandle(IntPtr.Zero);
            return true;
        }

        public override bool IsInvalid
        {
            get { return this.handle == IntPtr.Zero; }
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XStoreLicense : EquatableHandle
    {
        public XStoreLicense(IntPtr handle) :
            base(IntPtr.Zero, true, handle)
        { }

        protected override bool ReleaseHandle()
        {
            Interop.NativeMethods.XStoreCloseLicenseHandle(this.handle);
            SetHandle(IntPtr.Zero);
            return true;
        }

        public override bool IsInvalid
        {
            get { return this.handle == IntPtr.Zero; }
        }
    }

    public class XStoreProductQuery : EquatableHandle
    {
        public XStoreProductQuery(IntPtr handle) :
            base(IntPtr.Zero, true, handle)
        { }

        protected override bool ReleaseHandle()
        {
            Interop.NativeMethods.XStoreCloseProductsQueryHandle(this.handle);
            SetHandle(IntPtr.Zero);
            return true;
        }

        public override bool IsInvalid
        {
            get { return this.handle == IntPtr.Zero; }
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XStoreSku
    {
        internal XStoreSku(ref Interop.XStoreSkuInterop interop)
        {
            this.SkuId = interop.skuId;
            this.Title = interop.title;
            this.Description = interop.description;
            this.Language = interop.language;
            this.Price = new XStorePrice(interop.price);
            this.IsTrial = interop.isTrial;
            this.IsInUserCollection = interop.isInUserCollection;
            this.CollectionData = new XStoreCollectionData(interop.collectionData);
            this.IsSubscription = interop.isSubscription;
            this.SubscriptionInfo = new XStoreSubscriptionInfo(interop.subscriptionInfo);
            this.BundledSkus = InteropHelpers.MarshalStringArrayAnsi(interop.bundledSkus, interop.bundledSkusCount);
            this.Images = InteropHelpers.MarshalArray<Interop.XStoreImage, XStoreImage>(interop.images, interop.imagesCount, (imageInterop) => new XStoreImage(imageInterop));
            this.Videos = InteropHelpers.MarshalArray<Interop.XStoreVideo, XStoreVideo>(interop.videos, interop.videosCount, (videoInterop) => new XStoreVideo(videoInterop));
            this.Availabilities = InteropHelpers.MarshalArray<Interop.XStoreAvailability, XStoreAvailability>(interop.availabilities, interop.availabilitiesCount, (availabilityInterop) => new XStoreAvailability(availabilityInterop));
        }

        public string SkuId { get; }
        public string Title { get; }
        public string Description { get; }
        public string Language { get; }
        public XStorePrice Price { get; }
        public bool IsTrial { get; }
        public bool IsInUserCollection { get; }
        public XStoreCollectionData CollectionData { get; }
        public bool IsSubscription { get; }
        public XStoreSubscriptionInfo SubscriptionInfo { get; }
        public string[] BundledSkus { get; }
        public XStoreImage[] Images { get; }
        public XStoreVideo[] Videos { get; }
        public XStoreAvailability[] Availabilities { get; }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XStoreProduct
    {
        internal XStoreProduct(ref XStoreProductInterop interop)
        {
            this.StoreId = interop.storeId;
            this.Title = interop.title;
            this.Description = interop.description;
            this.Language = interop.language;
            this.InAppOfferToken = interop.inAppOfferToken;
            this.LinkUri = interop.linkUri;
            this.ProductKind = interop.productKind;
            this.Price = new XStorePrice(interop.price);
            this.HasDigitalDownload = interop.hasDigitalDownload;
            this.IsInUserCollection = interop.isInUserCollection;
            this.Keywords = InteropHelpers.MarshalStringArrayAnsi(interop.keywords, interop.keywordsCount);
            this.Skus = InteropHelpers.MarshalArray<Interop.XStoreSkuInterop, XStoreSku>(interop.skus, interop.skusCount, (skuInterop) => new XStoreSku(ref skuInterop));
            this.Images = InteropHelpers.MarshalArray<Interop.XStoreImage, XStoreImage>(interop.images, interop.imagesCount, (imageInterop) => new XStoreImage(imageInterop));
            this.Videos = InteropHelpers.MarshalArray<Interop.XStoreVideo, XStoreVideo>(interop.videos, interop.videosCount, (videoInterop) => new XStoreVideo(videoInterop));
        }

        public string StoreId { get; }
        public string Title { get; }
        public string Description { get; }
        public string Language { get; }
        public string InAppOfferToken { get; }
        public string LinkUri { get; }
        public XStoreProductKind ProductKind { get; }
        public XStorePrice Price { get; }
        public bool HasDigitalDownload { get; }
        public bool IsInUserCollection { get; }
        public string[] Keywords { get; }
        public XStoreSku[] Skus { get; }
        public XStoreImage[] Images { get; }
        public XStoreVideo[] Videos { get; }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XStoreAddonLicense
    {
        internal XStoreAddonLicense(Interop.XStoreAddonLicense interop)
        {
            this.interop = interop;
        }

        public XStoreAddonLicense()
        {
            this.interop = new Interop.XStoreAddonLicense();
        }

        internal Interop.XStoreAddonLicense interop;

        public string SkuStoreId
        {
            get => this.interop.skuStoreId;
            set => this.interop.skuStoreId = value;
        }

        public string InAppOfferToken
        {
            get => this.interop.inAppOfferToken;
            set => this.interop.inAppOfferToken = value;
        }

        public bool IsActive
        {
            get => this.interop.isActive;
            set => this.interop.isActive = value;
        }

        public Int64 ExpirationDate
        {
            get => this.interop.expirationDate;
            set => this.interop.expirationDate = value;
        }
    }

    public class XStoreConsumableResult
    {
        internal XStoreConsumableResult(Interop.XStoreConsumableResult interop)
        {
            this.interop = interop;
        }

        public XStoreConsumableResult()
        {
            this.interop = new Interop.XStoreConsumableResult();
        }

        internal Interop.XStoreConsumableResult interop;

        public UInt32 Quantity
        {
            get => this.interop.quantity;
            set => this.interop.quantity = value;
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XStorePackageUpdate
    {
        internal XStorePackageUpdate(Interop.XStorePackageUpdate interop)
        {
            this.interop = interop;
        }

        public XStorePackageUpdate()
        {
            this.interop = new Interop.XStorePackageUpdate();
        }

        internal Interop.XStorePackageUpdate interop;

        public string PackageIdentifier
        {
            get => this.interop.packageIdentifier;
            set => this.interop.packageIdentifier = value;
        }

        public bool IsMandatory
        {
            get => this.interop.isMandatory;
            set => this.interop.isMandatory = value;
        }
    }

#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XStoreGameLicense
    {
        internal XStoreGameLicense(Interop.XStoreGameLicense interop)
        {
            this.interop = interop;
        }

        public XStoreGameLicense()
        {
            this.interop = new Interop.XStoreGameLicense();
        }

        internal Interop.XStoreGameLicense interop;

        public string SkuStoreId
        {
            get => this.interop.skuStoreId;
            set => this.interop.skuStoreId = value;
        }

        public bool IsActive
        {
            get => this.interop.isActive;
            set => this.interop.isActive = value;
        }

        public bool IsTrialOwnedByThisUser
        {
            get => this.interop.isTrialOwnedByThisUser;
            set => this.interop.isTrialOwnedByThisUser = value;
        }

        public bool IsDiscLicense
        {
            get => this.interop.isDiscLicense;
            set => this.interop.isDiscLicense = value;
        }

        public bool IsTrial
        {
            get => this.interop.isTrial;
            set => this.interop.isTrial = value;
        }

        public UInt32 TrialTimeRemainingInSeconds
        {
            get => this.interop.trialTimeRemainingInSeconds;
            set => this.interop.trialTimeRemainingInSeconds = value;
        }

        public string TrialUniqueId
        {
            get => this.interop.trialUniqueId;
            set => this.interop.trialUniqueId = value;
        }

        public Int64 ExpirationDate
        {
            get => this.interop.expirationDate;
            set => this.interop.expirationDate = value;
        }
    }

    public class XStoreRateAndReviewResult
    {
        internal XStoreRateAndReviewResult(Interop.XStoreRateAndReviewResult interop)
        {
            this.interop = interop;
        }

        public XStoreRateAndReviewResult()
        {
            this.interop = new Interop.XStoreRateAndReviewResult();
        }

        internal Interop.XStoreRateAndReviewResult interop;

        public bool WasUpdated
        {
            get => this.interop.wasUpdated;
            set => this.interop.wasUpdated = value;
        }
    }

    public class GameLicenseChangedCallbackToken
    {
        internal Interop.GameLicenseChangedCallbackToken interop { get; private set; }

        internal GameLicenseChangedCallbackToken(XStoreContext storeContext, Interop.XStoreGameLicenseChangedCallback callback, IntPtr context)
        {
            interop = new Interop.GameLicenseChangedCallbackToken(storeContext, callback, context);
        }

        public bool IsValid { get { return interop.IsValid; } }

        public UInt64 Token
        {
            get => interop.Token;
            set => interop.Token = value;
        }

        public bool Unregister(bool wait)
        {
            return interop.Unregister(wait);
        }

        public void Dispose()
        {
            this.interop.Dispose();
        }
    }

    public class PackageLicenseLostCallbackToken
    {
        internal Interop.PackageLicenseLostCallbackToken interop { get; private set; }

        internal PackageLicenseLostCallbackToken(XStoreLicense licenseHandle, Interop.XStorePackageLicenseLostCallback callback, IntPtr context)
        {
            interop = new Interop.PackageLicenseLostCallbackToken(licenseHandle, callback, context);
        }

        public bool IsValid { get { return interop.IsValid; } }

        public UInt64 Token
        {
            get => interop.Token;
            set => interop.Token = value;
        }

        public bool Unregister(bool wait)
        {
            return interop.Unregister(wait);
        }

        public void Dispose()
        {
            this.interop.Dispose();
        }
    }

    public delegate bool XStoreProductQueryCallback(XStoreProduct product, IntPtr context);

    public delegate void XStorePackageLicenseLostCallback(IntPtr context);

    public delegate void XStoreGameLicenseChangedCallback(IntPtr context);

    partial class SDK
    {
        public static int XStoreCreateContext(XUserHandle user, out XStoreContext storeContext)
        {
            storeContext = null;
            IntPtr userHandle = user != null ? user.Handle : IntPtr.Zero;
            IntPtr storeContextHandle = IntPtr.Zero;
            int hr = NativeMethods.XStoreCreateContext(userHandle, out storeContextHandle);
            if (HR.SUCCEEDED(hr))
            {
                storeContext = new XStoreContext(storeContextHandle);
            }

            return hr;
        }

        public static int XStoreCreateContext(out XStoreContext storeContext)
        {
            return XStoreCreateContext(null, out storeContext);
        }

        public static void XStoreCloseContextHandle(XStoreContext storeContext)
        {
            storeContext.Close();
        }

        public static void XStoreCloseLicenseHandle(XStoreLicense storeLicense)
        {
            storeLicense.Close();
        }

        public static int XStoreAcquireLicenseForDurablesAsync(XStoreContext context,
            string storeId,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreAcquireLicenseForDurablesAsync(context.Handle, storeId, async.InteropPtr);
        }

        public static int XStoreAcquireLicenseForDurablesResult(XAsyncBlock async,
            out XStoreLicense storeLicense)
        {
            IntPtr licensePtr;
            storeLicense = null;
            int hr = NativeMethods.XStoreAcquireLicenseForDurablesResult(async.InteropPtr, out licensePtr);
            if (HR.SUCCEEDED(hr))
            {
                storeLicense = new XStoreLicense(licensePtr);
            }

            return hr;
        }

        public static int XStoreAcquireLicenseForPackageAsync(XStoreContext context,
            string packageIdentifier,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreAcquireLicenseForPackageAsync(context.Handle, packageIdentifier, async.InteropPtr);
        }

        public static int XStoreAcquireLicenseForPackageResult(XAsyncBlock async,
            out XStoreLicense storeLicenseHandle)
        {
            IntPtr licensePtr;
            storeLicenseHandle = null;
            int hr = NativeMethods.XStoreAcquireLicenseForPackageResult(async.InteropPtr, out licensePtr);
            if (HR.SUCCEEDED(hr))
            {
                storeLicenseHandle = new XStoreLicense(licensePtr);
            }

            return hr;
        }

        public static int XStoreCanAcquireLicenseForStoreIdAsync(XStoreContext storeContext,
            string storeProductId,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreCanAcquireLicenseForStoreIdAsync(storeContext.Handle, storeProductId, async.InteropPtr);
        }

        public static int XStoreCanAcquireLicenseForStoreIdResult(XAsyncBlock async, out XStoreCanAcquireLicenseResult storeCanAcquireLicenseResult)
        {
            storeCanAcquireLicenseResult = default;
            Interop.XStoreCanAcquireLicenseResult interopResult;

            int hr = NativeMethods.XStoreCanAcquireLicenseForStoreIdResult(async.InteropPtr, out interopResult);

            if(HR.SUCCEEDED(hr))
            {
                storeCanAcquireLicenseResult = new XStoreCanAcquireLicenseResult(interopResult);
            }

            return hr;
        }

        public static int XStoreCanAcquireLicenseForPackageAsync(XStoreContext storeContext,
            string packageIdentifier,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreCanAcquireLicenseForPackageAsync(storeContext.Handle, packageIdentifier, async.InteropPtr);
        }

        public static int XStoreCanAcquireLicenseForPackageResult(XAsyncBlock async, out XStoreCanAcquireLicenseResult storeCanAcquireLicenseResult)
        {
            storeCanAcquireLicenseResult = default;
            Interop.XStoreCanAcquireLicenseResult interopResult;

            int hr = NativeMethods.XStoreCanAcquireLicenseForPackageResult(async.InteropPtr, out interopResult);

            if(HR.SUCCEEDED(hr))
            {
                storeCanAcquireLicenseResult = new XStoreCanAcquireLicenseResult(interopResult);
            }

            return hr;
        }

        public static int XStoreQueryProductForCurrentGameAsync(XStoreContext storeContext,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreQueryProductForCurrentGameAsync(storeContext.Handle, async.InteropPtr);
        }

        public static int XStoreQueryProductForCurrentGameResult(XAsyncBlock async,
            out XStoreProductQuery productQueryHandle)
        {
            productQueryHandle = null;
            IntPtr productQueryHandlePtr;
            int hr = NativeMethods.XStoreQueryProductForCurrentGameResult(async.InteropPtr, out productQueryHandlePtr);
            if (HR.SUCCEEDED(hr))
            {
                productQueryHandle = new XStoreProductQuery(productQueryHandlePtr);
            }

            return hr;
        }

        [AOT.MonoPInvokeCallback(typeof(XStoreProductQueryCallbackInterop))]
        private static bool OnProductQueryCallback([In] ref XStoreProductInterop product, IntPtr context)
        {
            GCHandle handle = GCHandle.FromIntPtr(context);
            var wrapper = handle.Target as CallbackWrapper<XStoreProductQueryCallbackInterop>;
            return wrapper.Callback(ref product, wrapper.Context);
        }

        public static int XStoreEnumerateProductsQuery(XStoreProductQuery productQueryHandle,
            IntPtr context,
            XStoreProductQueryCallback callback)
        {
            XStoreProductQueryCallbackInterop localCallback = (ref XStoreProductInterop product, IntPtr context) =>
            {
                return callback(new XStoreProduct(ref product), context);
            };

            using (var wrapper = new CallbackWrapper<XStoreProductQueryCallbackInterop>(localCallback, context, OnProductQueryCallback))
            {
                return NativeMethods.XStoreEnumerateProductsQuery(productQueryHandle.Handle,
                    wrapper.CallbackContext,
                    wrapper.StaticCallback);
            }
        }

        public static void XStoreCloseProductsQueryHandle(XStoreProductQuery productQueryHandle)
        {
            productQueryHandle.Close();
        }

        public static int XStoreDownloadPackageUpdatesAsync(XStoreContext storeContext,
            string[] packageIdentifiers,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreDownloadPackageUpdatesAsync(storeContext.Handle,
                packageIdentifiers,
                (UInt64)packageIdentifiers.Length,
                async.InteropPtr);
        }

        public static int XStoreDownloadPackageUpdatesResult(XAsyncBlock async)
        {
            return NativeMethods.XStoreDownloadPackageUpdatesResult(async.InteropPtr);
        }

        public static int XStoreDownloadAndInstallPackageUpdatesAsync(XStoreContext storeContext,
            string[] packageIdentifiers,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreDownloadAndInstallPackageUpdatesAsync(storeContext.Handle,
                packageIdentifiers,
                (UInt64)packageIdentifiers.Length,
                async.InteropPtr);
        }

        public static int XStoreDownloadAndInstallPackageUpdatesResult(XAsyncBlock async)
        {
            return NativeMethods.XStoreDownloadAndInstallPackageUpdatesResult(async.InteropPtr);
        }

        public static int XStoreDownloadAndInstallPackagesAsync(XStoreContext storeContext,
            string[] storeIds,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreDownloadAndInstallPackagesAsync(storeContext.Handle,
                storeIds,
                (UInt64)storeIds.Length,
                async.InteropPtr);
        }

        public static int XStoreDownloadAndInstallPackagesResultCount(XAsyncBlock async,
            out UInt32 count)
        {
            return NativeMethods.XStoreDownloadAndInstallPackagesResultCount(async.InteropPtr, out count);
        }

        public static int XStoreDownloadAndInstallPackagesResult(XAsyncBlock async,
            UInt32 count,
            out string[] packageIdentifiers)
        {
            packageIdentifiers = null;
            var interopData = new XStorePackageIdentifierInterop[count];
            int hr = NativeMethods.XStoreDownloadAndInstallPackagesResult(async.InteropPtr, count, interopData);
            if (HR.SUCCEEDED(hr))
            {
                packageIdentifiers = new string[count];
                for (uint i = 0; i < count; i++)
                {
                    packageIdentifiers[i] = interopData[i].Data;
                }
            }

            return hr;
        }

        public static int XStoreGetUserCollectionsIdAsync(XStoreContext storeContext,
            string serviceTicket,
            string publisherUserId,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreGetUserCollectionsIdAsync(storeContext.Handle,
                serviceTicket,
                publisherUserId,
                async.InteropPtr);
        }

        public static int XStoreGetUserCollectionsIdResultSize(XAsyncBlock async,
            out UInt64 size)
        {
            return NativeMethods.XStoreGetUserCollectionsIdResultSize(async.InteropPtr, out size);
        }

        public static int XStoreGetUserCollectionsIdResult(XAsyncBlock async,
            UInt64 size,
            out string result)
        {
            result = null;
            StringBuilder sb = new StringBuilder((int)size);
            int hr = NativeMethods.XStoreGetUserCollectionsIdResult(async.InteropPtr, size, sb);
            if (HR.SUCCEEDED(hr))
            {
                result = sb.ToString();
            }

            return hr;
        }

        public static int XStoreGetUserPurchaseIdAsync(XStoreContext storeContext,
            string serviceTicket,
            string publisherUserId,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreGetUserPurchaseIdAsync(storeContext.Handle,
                serviceTicket,
                publisherUserId,
                async.InteropPtr);
        }

        public static int XStoreGetUserPurchaseIdResultSize(XAsyncBlock async,
            out UInt64 size)
        {
            return NativeMethods.XStoreGetUserPurchaseIdResultSize(async.InteropPtr, out size);
        }

        public static int XStoreGetUserPurchaseIdResult(XAsyncBlock async,
            UInt64 size,
            out string result)
        {
            result = null;
            StringBuilder sb = new StringBuilder((int)size);
            int hr = NativeMethods.XStoreGetUserPurchaseIdResult(async.InteropPtr, size, sb);
            if (HR.SUCCEEDED(hr))
            {
                result = sb.ToString();
            }

            return hr;
        }

        public static bool XStoreIsAvailabilityPurchasable(XStoreAvailability availability)
        {
            return NativeMethods.XStoreIsAvailabilityPurchasable(availability.interop);
        }

        public static bool XStoreIsLicenseValid(XStoreLicense license)
        {
            return NativeMethods.XStoreIsLicenseValid(license.Handle);
        }

        public static bool XStoreProductsQueryHasMorePages(XStoreProductQuery productQueryHandle)
        {
            return NativeMethods.XStoreProductsQueryHasMorePages(productQueryHandle.Handle);
        }

        public static int XStoreProductsQueryNextPageAsync(XStoreProductQuery productQueryHandle, XAsyncBlock async)
        {
            return NativeMethods.XStoreProductsQueryNextPageAsync(productQueryHandle.Handle, async.InteropPtr);
        }

        public static int XStoreProductsQueryNextPageResult(XAsyncBlock async, out XStoreProductQuery productQueryHandle)
        {
            productQueryHandle = null;

            IntPtr handle;
            int hr = NativeMethods.XStoreProductsQueryNextPageResult(async.InteropPtr, out handle);
            if (HR.SUCCEEDED(hr))
            {
                productQueryHandle = new XStoreProductQuery(handle);
            }

            return hr;
        }

        public static int XStoreQueryAddOnLicensesAsync(XStoreContext storeContext,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreQueryAddOnLicensesAsync(storeContext.Handle, async.InteropPtr);
        }

        public static int XStoreQueryAddOnLicensesResultCount(XAsyncBlock async, out UInt32 count)
        {
            return NativeMethods.XStoreQueryAddOnLicensesResultCount(async.InteropPtr, out count);
        }

        public static int XStoreQueryAddOnLicensesResult(XAsyncBlock async, XStoreAddonLicense[] addOnLicenses)
        {
            Interop.XStoreAddonLicense[] interopAddOnLicenses = new Interop.XStoreAddonLicense[addOnLicenses.Length];

            int hr = NativeMethods.XStoreQueryAddOnLicensesResult(async.InteropPtr, (UInt32)interopAddOnLicenses.Length, interopAddOnLicenses);

            if(HR.SUCCEEDED(hr))
            {
                for (int i = 0; i < addOnLicenses.Length; i++)
                {
                    addOnLicenses[i] = new XStoreAddonLicense(interopAddOnLicenses[i]);
                }
            }

            return hr;
        }

        public static int XStoreQueryAssociatedProductsAsync(XStoreContext storeContext,
            XStoreProductKind productKinds,
            UInt32 maxItemsToRetrievePerPage,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreQueryAssociatedProductsAsync(storeContext.Handle,
                productKinds,
                maxItemsToRetrievePerPage,
                async.InteropPtr);
        }

        public static int XStoreQueryAssociatedProductsResult(XAsyncBlock async, out XStoreProductQuery productQueryHandle)
        {
            productQueryHandle = null;
            IntPtr handle;
            int hr = NativeMethods.XStoreQueryAssociatedProductsResult(async.InteropPtr, out handle);
            if (HR.SUCCEEDED(hr))
            {
                productQueryHandle = new XStoreProductQuery(handle);
            }

            return hr;
        }

        public static int XStoreQueryConsumableBalanceRemainingAsync(XStoreContext storeContext,
            string storeProductId,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreQueryConsumableBalanceRemainingAsync(storeContext.Handle,
                storeProductId,
                async.InteropPtr);
        }

        public static int XStoreQueryConsumableBalanceRemainingResult(XAsyncBlock async,
            out XStoreConsumableResult consumableResult)
        {
            consumableResult = default;
            Interop.XStoreConsumableResult interopResult = default;

            int hr = NativeMethods.XStoreQueryConsumableBalanceRemainingResult(async.InteropPtr, out interopResult);

            if(HR.SUCCEEDED(hr))
            {
                consumableResult = new XStoreConsumableResult(interopResult);
            }

            return hr;
        }

        public static int XStoreQueryEntitledProductsAsync(XStoreContext storeContext,
            XStoreProductKind productKinds,
            UInt32 maxItemsToRetrievePerPage,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreQueryEntitledProductsAsync(storeContext.Handle,
                productKinds,
                maxItemsToRetrievePerPage,
                async.InteropPtr);
        }

        public static int XStoreQueryEntitledProductsResult(XAsyncBlock async, out XStoreProductQuery productQueryHandle)
        {
            productQueryHandle = null;
            IntPtr handle;
            int hr = NativeMethods.XStoreQueryEntitledProductsResult(async.InteropPtr, out handle);
            if (HR.SUCCEEDED(hr))
            {
                productQueryHandle = new XStoreProductQuery(handle);
            }

            return hr;
        }

        public static int XStoreQueryGameAndDlcPackageUpdatesAsync(XStoreContext storeContext,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreQueryGameAndDlcPackageUpdatesAsync(storeContext.Handle,
                async.InteropPtr);
        }

        public static int XStoreQueryGameAndDlcPackageUpdatesResultCount(XAsyncBlock async,
            out UInt32 count)
        {
            return NativeMethods.XStoreQueryGameAndDlcPackageUpdatesResultCount(async.InteropPtr, out count);
        }

        public static int XStoreQueryGameAndDlcPackageUpdatesResult(XAsyncBlock async,
            XStorePackageUpdate[] packageUpdates)
        {
            Interop.XStorePackageUpdate[] interopPackageUpdates = new Interop.XStorePackageUpdate[packageUpdates.Length];

            int hr = NativeMethods.XStoreQueryGameAndDlcPackageUpdatesResult(async.InteropPtr, (UInt32)interopPackageUpdates.Length, interopPackageUpdates);

            if(HR.SUCCEEDED(hr))
            {
                for (int i = 0; i < packageUpdates.Length; i++)
                {
                    packageUpdates[i] = new XStorePackageUpdate(interopPackageUpdates[i]);
                }
            }

            return hr;
        }

        public static int XStoreQueryGameLicenseAsync(XStoreContext storeContext,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreQueryGameLicenseAsync(storeContext.Handle,
                async.InteropPtr);
        }

        public static int XStoreQueryGameLicenseResult(XAsyncBlock async,
            out XStoreGameLicense license)
        {
            license = default;
            Interop.XStoreGameLicense interopLicense = default;

            int hr = NativeMethods.XStoreQueryGameLicenseResult(async.InteropPtr, out interopLicense);

            if(HR.SUCCEEDED(hr))
            {
                license = new XStoreGameLicense(interopLicense);
            }

            return hr;
        }

        public static int XStoreQueryLicenseTokenAsync(XStoreContext storeContext,
            string[] productIds,
            string customDeveloperString,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreQueryLicenseTokenAsync(storeContext.Handle,
                productIds,
                (UInt64)productIds.Length,
                customDeveloperString,
                async.InteropPtr);
        }

        public static int XStoreQueryLicenseTokenResultSize(XAsyncBlock async,
            out UInt64 size)
        {
            return NativeMethods.XStoreQueryLicenseTokenResultSize(async.InteropPtr, out size);
        }

        public static int XStoreQueryLicenseTokenResult(XAsyncBlock async,
            UInt64 size,
            out string result)
        {
            result = null;
            StringBuilder sb = new StringBuilder((int)size);
            int hr = NativeMethods.XStoreQueryLicenseTokenResult(async.InteropPtr, size, sb);
            if (HR.SUCCEEDED(hr))
            {
                result = sb.ToString();
            }

            return hr;
        }

        public static int XStoreQueryPackageIdentifier(string storeId, UInt64 size, out string packageIdentifier)
        {
            packageIdentifier = null;
            StringBuilder sb = new StringBuilder((int)size);
            int hr = NativeMethods.XStoreQueryPackageIdentifier(storeId, size, sb);
            if (HR.SUCCEEDED(hr))
            {
                packageIdentifier = sb.ToString();
            }

            return hr;
        }

        public static int XStoreQueryPackageIdentifier(string storeId, out string packageIdentifier)
        {
            return XStoreQueryPackageIdentifier(storeId, (UInt64)XPACKAGE_IDENTIFIER_MAX_LENGTH, out packageIdentifier);
        }

        public static int XStoreQueryProductForPackageAsync(XStoreContext storeContext,
            XStoreProductKind productKinds,
            string packageIdentifier,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreQueryProductForPackageAsync(storeContext.Handle, productKinds, packageIdentifier, async.InteropPtr);
        }

        public static int XStoreQueryProductForPackageResult(XAsyncBlock async,
            out XStoreProductQuery productQueryHandle)
        {
            productQueryHandle = null;
            IntPtr handle;
            int hr = NativeMethods.XStoreQueryProductForPackageResult(async.InteropPtr, out handle);
            if (HR.SUCCEEDED(hr))
            {
                productQueryHandle = new XStoreProductQuery(handle);
            }

            return hr;
        }

        public static int XStoreQueryProductsAsync(XStoreContext storeContext,
            XStoreProductKind productKinds,
            string[] storeIds,
            string[] actionFilters,
            XAsyncBlock async)
        {
            UInt64 actionFilterCount = actionFilters != null ? (UInt64)actionFilters.Length : 0;

            return NativeMethods.XStoreQueryProductsAsync(storeContext.Handle,
                productKinds,
                storeIds,
                (UInt64)storeIds.Length,
                actionFilters,
                actionFilterCount,
                async.InteropPtr);
        }

        public static int XStoreQueryProductsResult(XAsyncBlock async,
            out XStoreProductQuery productQueryHandle)
        {
            productQueryHandle = null;
            IntPtr handle;
            int hr = NativeMethods.XStoreQueryProductsResult(async.InteropPtr, out handle);
            if (HR.SUCCEEDED(hr))
            {
                productQueryHandle = new XStoreProductQuery(handle);
            }

            return hr;
        }

        public static int XStoreRegisterGameLicenseChanged(XStoreContext storeContext,
            XTaskQueueHandle queue,
            IntPtr context,
            XStoreGameLicenseChangedCallback callback,
            out GameLicenseChangedCallbackToken token)
        {
            Interop.XStoreGameLicenseChangedCallback interopCallback = (IntPtr context) =>
            {
                callback(context);
            };

            token = new GameLicenseChangedCallbackToken(storeContext, interopCallback, context);

            UInt64 tokenValue;
            int hr = NativeMethods.XStoreRegisterGameLicenseChanged(storeContext.Handle,
                queue.Handle,
                token.interop.CallbackContext,
                token.interop.StaticCallback,
                out tokenValue);
            if (HR.SUCCEEDED(hr))
            {
                token.Token = tokenValue;
            }
            else
            {
                token.Dispose();
                token = null;
            }

            return hr;
        }

        public static bool XStoreUnregisterGameLicenseChanged(GameLicenseChangedCallbackToken token,
            bool wait)
        {
            return token.Unregister(wait);
        }

        public static int XStoreRegisterPackageLicenseLost(XStoreLicense licenseHandle,
            XTaskQueueHandle queue,
            IntPtr context,
            XStorePackageLicenseLostCallback callback,
            out PackageLicenseLostCallbackToken token)
        {
            Interop.XStorePackageLicenseLostCallback interopCallback = (IntPtr context) =>
            {
                callback(context);
            };

            token = new PackageLicenseLostCallbackToken(licenseHandle, interopCallback, context);

            UInt64 tokenValue;
            int hr = NativeMethods.XStoreRegisterPackageLicenseLost(licenseHandle.Handle,
                queue.Handle,
                token.interop.CallbackContext,
                token.interop.StaticCallback,
                out tokenValue);
            if (HR.SUCCEEDED(hr))
            {
                token.Token = tokenValue;
            }
            else
            {
                token.Dispose();
                token = null;
            }

            return hr;
        }

        public static bool XStoreUnregisterPackageLicenseLost(PackageLicenseLostCallbackToken token,
            bool wait)
        {
            return token.Unregister(wait);
        }

        public static bool XStoreUnregisterPackageLicenseLost(PackageLicenseLostCallbackToken token)
        {
            return XStoreUnregisterPackageLicenseLost(token, true);
        }

        public static int XStoreReportConsumableFulfillmentAsync(XStoreContext storeContext,
            string storeProductId,
            UInt32 quantity,
            Guid trackingId,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreReportConsumableFulfillmentAsync(storeContext.Handle,
                storeProductId,
                quantity,
                trackingId,
                async.InteropPtr);
        }

        public static int XStoreReportConsumableFulfillmentResult(XAsyncBlock async,
            out XStoreConsumableResult consumableResult)
        {
            consumableResult = default;
            Interop.XStoreConsumableResult interopConsumableResult = default;

            int hr = NativeMethods.XStoreReportConsumableFulfillmentResult(async.InteropPtr, out interopConsumableResult);

            if(HR.SUCCEEDED(hr))
            {
                consumableResult = new XStoreConsumableResult(interopConsumableResult);
            }

            return hr;
        }

        public static int XStoreShowAssociatedProductsUIAsync(XStoreContext storeContext,
            string storeId,
            XStoreProductKind productKinds,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreShowAssociatedProductsUIAsync(storeContext.Handle, storeId, productKinds, async.InteropPtr);
        }

        public static int XStoreShowAssociatedProductsUIResult(XAsyncBlock async)
        {
            return NativeMethods.XStoreShowAssociatedProductsUIResult(async.InteropPtr);
        }

        public static int XStoreShowProductPageUIAsync(XStoreContext storeContext,
            string storeId,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreShowProductPageUIAsync(storeContext.Handle, storeId, async.InteropPtr);
        }

        public static int XStoreShowProductPageUIResult(XAsyncBlock async)
        {
            return NativeMethods.XStoreShowProductPageUIResult(async.InteropPtr);
        }

        public static int XStoreShowPurchaseUIAsync(XStoreContext storeContext,
            string storeId,
            string name,
            string extendedJsonData,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreShowPurchaseUIAsync(storeContext.Handle, storeId, name, extendedJsonData, async.InteropPtr);
        }

        public static int XStoreShowPurchaseUIResult(XAsyncBlock async)
        {
            return NativeMethods.XStoreShowPurchaseUIResult(async.InteropPtr);
        }

        public static int XStoreShowRateAndReviewUIAsync(XStoreContext storeContext,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreShowRateAndReviewUIAsync(storeContext.Handle, async.InteropPtr);
        }

        public static int XStoreShowRateAndReviewUIResult(XAsyncBlock async, out XStoreRateAndReviewResult result)
        {
            result = default;
            Interop.XStoreRateAndReviewResult interopResult = default;
            int hr = NativeMethods.XStoreShowRateAndReviewUIResult(async.InteropPtr, out interopResult);

            if(HR.SUCCEEDED(hr))
            {
                result = new XStoreRateAndReviewResult(interopResult);
            }

            return hr;
        }

        public static int XStoreShowRedeemTokenUIAsync(XStoreContext storeContext,
            string token,
            string[] allowedStoreIds,
            bool disallowCsvRedemption,
            XAsyncBlock async)
        {
            return NativeMethods.XStoreShowRedeemTokenUIAsync(storeContext.Handle, token, allowedStoreIds, (UInt64)allowedStoreIds.Length, disallowCsvRedemption, async.InteropPtr);
        }

        public static int XStoreShowRedeemTokenUIResult(XAsyncBlock async)
        {
            return NativeMethods.XStoreShowRedeemTokenUIResult(async.InteropPtr);
        }
    }
}
