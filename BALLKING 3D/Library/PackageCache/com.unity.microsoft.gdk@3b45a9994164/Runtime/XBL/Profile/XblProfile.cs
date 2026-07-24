using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;

namespace Unity.XGamingRuntime
{
    public delegate void XblProfileGetUserProfileCompleted(Int32 hresult, XblUserProfile result);
    public delegate void XblProfileGetUserProfilesCompleted(Int32 hresult, XblUserProfile[] result);
    public delegate void XblProfileGetUserProfilesForSocialGroupCompleted(Int32 hresult, XblUserProfile[] result);

    public partial class SDK
    {
        public partial class XBL
        {
            static public void XblProfileGetUserProfileAsync(
                XblContextHandle xblContextHandle,
                UInt64 xboxUserId,
                XblProfileGetUserProfileCompleted completionRoutine)
            {
                if (xblContextHandle == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblUserProfile));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Interop.XblUserProfile profileResult;
                    Int32 hr = XblInterop.XblProfileGetUserProfileResult(block, out profileResult);
                    if (HR.FAILED(hr))
                    {
                        completionRoutine(hr, default(XblUserProfile));
                        return;
                    }

                    completionRoutine(hr, new XblUserProfile(profileResult));
                });

                Int32 hresult = XblInterop.XblProfileGetUserProfileAsync(
                    xblContextHandle.Handle,
                    xboxUserId,
                    asyncBlock);

                if (HR.FAILED(hresult))
                {
                    completionRoutine(hresult, default(XblUserProfile));
                    return;
                }
            }

            static public void XblProfileGetUserProfilesAsync(
                XblContextHandle xblContextHandle,
                UInt64[] xboxUserIds,
                XblProfileGetUserProfilesCompleted completionRoutine
                )
            {
                if (xblContextHandle == null || xboxUserIds == null || xboxUserIds.Length == 0)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblUserProfile[]));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Int32 hr = XblInterop.XblProfileGetUserProfilesResultCount(block, out SizeT profileCount);
                    if (HR.FAILED(hr))
                    {
                        completionRoutine(hr, default(XblUserProfile[]));
                        return;
                    }

                    var interopProfiles = new Interop.XblUserProfile[profileCount.ToInt32()];

                    hr = XblInterop.XblProfileGetUserProfilesResult(block, profileCount, interopProfiles);
                    if (HR.FAILED(hr))
                    {
                        completionRoutine(hr, default(XblUserProfile[]));
                        return;
                    }

                    completionRoutine(hr, Array.ConvertAll(interopProfiles, (x) => new XblUserProfile(x)));
                });

                Int32 hresult = XblInterop.XblProfileGetUserProfilesAsync(
                    xblContextHandle.Handle,
                    xboxUserIds,
                    new SizeT(xboxUserIds.Length),
                    asyncBlock);

                if (HR.FAILED(hresult))
                {
                    completionRoutine(hresult, default(XblUserProfile[]));
                    return;
                }
            }

            static public void XblProfileGetUserProfilesForSocialGroupAsync(
                XblContextHandle xblContextHandle,
                string socialGroup,
                XblProfileGetUserProfilesForSocialGroupCompleted completionRoutine
                )
            {
                if (xblContextHandle == null || socialGroup == null)
                {
                    completionRoutine(HR.E_INVALIDARG, default(XblUserProfile[]));
                    return;
                }

                XAsyncBlock asyncBlock = AsyncHelpers.WrapAsyncBlock(defaultQueue, (XAsyncBlock block) =>
                {
                    Int32 hr = XblInterop.XblProfileGetUserProfilesForSocialGroupResultCount(block, out SizeT profileCount);
                    if (HR.FAILED(hr))
                    {
                        completionRoutine(hr, default(XblUserProfile[]));
                        return;
                    }

                    var interopProfiles = new Interop.XblUserProfile[profileCount.ToInt32()];

                    hr = XblInterop.XblProfileGetUserProfilesForSocialGroupResult(block, profileCount, interopProfiles);
                    if (HR.FAILED(hr))
                    {
                        completionRoutine(hr, default(XblUserProfile[]));
                        return;
                    }

                    completionRoutine(hr, Array.ConvertAll(interopProfiles, (x) => new XblUserProfile(x)));
                });

                Int32 hresult = XblInterop.XblProfileGetUserProfilesForSocialGroupAsync(
                    xblContextHandle.Handle,
                    Converters.StringToNullTerminatedUTF8ByteArray(socialGroup),
                    asyncBlock);

                if (HR.FAILED(hresult))
                {
                    completionRoutine(hresult, default(XblUserProfile[]));
                    return;
                }
            }
        }
    }
}