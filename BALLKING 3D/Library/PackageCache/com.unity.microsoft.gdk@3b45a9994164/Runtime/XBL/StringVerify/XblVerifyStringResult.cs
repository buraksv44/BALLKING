using System;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblVerifyStringResult
    {
        internal XblVerifyStringResult(Interop.XblVerifyStringResult interopStruct)
        {
            this.ResultCode = interopStruct.resultCode;
            this.FirstOffendingSubstring = interopStruct.firstOffendingSubstring.GetString();
        }

        public XblVerifyStringResultCode ResultCode { get; private set; }
        public string FirstOffendingSubstring { get; private set; }
    }
}
