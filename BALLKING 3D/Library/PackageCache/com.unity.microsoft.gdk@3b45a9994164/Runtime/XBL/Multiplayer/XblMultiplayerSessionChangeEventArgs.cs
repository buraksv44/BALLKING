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
    public class XblMultiplayerSessionChangeEventArgs
    {
        internal XblMultiplayerSessionChangeEventArgs(Interop.XblMultiplayerSessionChangeEventArgs interopStruct)
        {
            this.SessionReference = new XblMultiplayerSessionReference(interopStruct.SessionReference);
            this.Branch = interopStruct.GetBranch();
            this.ChangeNumber = interopStruct.ChangeNumber;
        }

        public XblMultiplayerSessionReference SessionReference { get; }
        public string Branch { get; }
        public ulong ChangeNumber { get; }
    }
}
