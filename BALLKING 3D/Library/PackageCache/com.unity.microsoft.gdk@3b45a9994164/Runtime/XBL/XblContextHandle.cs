using System;
using System.Runtime.InteropServices;
using Unity.XGamingRuntime.Interop;
using UnityEngine.Scripting.APIUpdating;

namespace Unity.XGamingRuntime
{
#if LEGACY_GDK_UPGRADE_PATH
    [MovedFrom("XGamingRuntime")]
#else
    [MovedFrom("Unity.GameCore")]
#endif
    public class XblContextHandle : EquatableHandle
    {
#region CALLBACKS
        [MonoPInvokeCallback]
        private static unsafe void XblMultiplayerSessionChangedCallback(
            IntPtr context,
            Interop.XblMultiplayerSessionChangeEventArgs args
            )
        {
            GCHandle contextHandle = GCHandle.FromIntPtr(context);
            ((XblContextHandle)contextHandle.Target).sessionChangedCallback?.Invoke(new XblMultiplayerSessionChangeEventArgs(args));
        }

        [MonoPInvokeCallback]
        private static unsafe void XblMultiplayerSessionSubscriptionLostCallback(
            IntPtr context
            )
        {
            GCHandle contextHandle = GCHandle.FromIntPtr(context);
            ((XblContextHandle)contextHandle.Target).sessionSubscriptionLostCallback?.Invoke();
        }

        [MonoPInvokeCallback]
        private static unsafe void XblMultiplayerConnectionIdChangedCallback(
            IntPtr context
            )
        {
            GCHandle contextHandle = GCHandle.FromIntPtr(context);
            ((XblContextHandle)contextHandle.Target).connectionIdChangedCallback?.Invoke();
        }

        [MonoPInvokeCallback]
        private static unsafe void XblUserStatisticsAddChangedCallback(
           Interop.XblStatisticChangeEventArgs args,
           IntPtr context
           )
        {
            GCHandle contextHandle = GCHandle.FromIntPtr(context);
            ((XblContextHandle)contextHandle.Target).statisticChangedCallback?.Invoke(new XblStatisticChangeEventArgs(args));
        }
#endregion

        public event SDK.XBL.XblMultiplayerSessionChangedHandler XblMultiplayerSessionChanged
        {
            add
            {
                if (sessionChangedCallback == null)
                {
                    sessionChangedHandlerId = XblInterop.XblMultiplayerAddSessionChangedHandler(this.Handle, XblMultiplayerSessionChangedCallback, GCHandle.ToIntPtr(m_gCHandle));
                }
                sessionChangedCallback += value;
            }
            remove
            {
                sessionChangedCallback -= value;
                if (sessionChangedCallback == null)
                {
                    XblInterop.XblMultiplayerRemoveSessionChangedHandler(this.Handle, sessionChangedHandlerId);
                    sessionChangedHandlerId = default(XblFunctionContext);
                }
            }
        }

        public event SDK.XBL.XblMultiplayerSessionSubscriptionLostHandler XblMultiplayerSessionSubscriptionLost
        {
            add
            {
                if (sessionSubscriptionLostCallback == null)
                {
                    sessionSubscriptionLostId = XblInterop.XblMultiplayerAddSubscriptionLostHandler(this.Handle, XblMultiplayerSessionSubscriptionLostCallback, GCHandle.ToIntPtr(m_gCHandle));
                }
                sessionSubscriptionLostCallback += value;
            }
            remove
            {
                sessionSubscriptionLostCallback -= value;
                if (sessionSubscriptionLostCallback == null)
                {
                    XblInterop.XblMultiplayerRemoveSubscriptionLostHandler(this.Handle, sessionSubscriptionLostId);
                    sessionSubscriptionLostId = default(XblFunctionContext);
                }
            }
        }

        public event SDK.XBL.XblMultiplayerConnectionIdChangedHandler XblMultiplayerConnectionIdChanged
        {
            add
            {
                if (connectionIdChangedCallback == null)
                {
                    connectionIdChangedId = XblInterop.XblMultiplayerAddConnectionIdChangedHandler(this.Handle, XblMultiplayerConnectionIdChangedCallback, GCHandle.ToIntPtr(m_gCHandle));
                }
                connectionIdChangedCallback += value;
            }
            remove
            {
                connectionIdChangedCallback -= value;
                if (connectionIdChangedCallback == null)
                {
                    XblInterop.XblMultiplayerRemoveConnectionIdChangedHandler(this.Handle, connectionIdChangedId);
                    connectionIdChangedId = default(XblFunctionContext);
                }
            }
        }

        public event SDK.XBL.XblUserStatisticsStatisticChangedHandler XblUserStatisticsStatisticChanged
        {
            add
            {
                if (statisticChangedCallback == null)
                {
                    statisiticsAddChangeId = XblInterop.XblUserStatisticsAddStatisticChangedHandler(this.Handle, XblUserStatisticsAddChangedCallback, GCHandle.ToIntPtr(m_gCHandle));
                }
                statisticChangedCallback += value;
            }
            remove
            {
                statisticChangedCallback -= value;
                if (statisticChangedCallback == null)
                {
                    XblInterop.XblUserStatisticsRemoveStatisticChangedHandler(this.Handle, GCHandle.ToIntPtr(m_gCHandle));
                    statisiticsAddChangeId = default(XblFunctionContext);
                }
            }
        }

#if UNITY_GDK_MOCK_XBL
        public XblContextHandle(Interop.XblContextHandle interopHandle) :
            base(IntPtr.Zero, true, interopHandle.handle)
        {
        }
#else
        internal XblContextHandle(Interop.XblContextHandle interopHandle) :
            base(IntPtr.Zero, true, interopHandle.handle)
        {
        }
#endif

        protected override bool ReleaseHandle()
        {
            if (this.sessionChangedCallback != null)
            {
                foreach (var i in this.sessionChangedCallback.GetInvocationList())
                {
                    this.sessionChangedCallback -= (SDK.XBL.XblMultiplayerSessionChangedHandler)i;
                }
            }

            if (this.sessionSubscriptionLostCallback != null)
            {
                foreach (var i in this.sessionSubscriptionLostCallback.GetInvocationList())
                {
                    this.sessionSubscriptionLostCallback -= (SDK.XBL.XblMultiplayerSessionSubscriptionLostHandler)i;
                }
            }

            if (this.connectionIdChangedCallback != null)
            {
                foreach (var i in this.connectionIdChangedCallback.GetInvocationList())
                {
                    this.connectionIdChangedCallback -= (SDK.XBL.XblMultiplayerConnectionIdChangedHandler)i;
                }
            }

            if (this.statisticChangedCallback != null)
            {
                foreach (var i in this.statisticChangedCallback.GetInvocationList())
                {
                    this.statisticChangedCallback -= (SDK.XBL.XblUserStatisticsStatisticChangedHandler)i;
                }
            }

            if (this.m_gCHandle != null)
                this.m_gCHandle.Free();

            if(SDK.XBL.IsXblInitialized)
            {
                XblInterop.XblContextCloseHandle(this.Handle);
            }

            return true;
        }

        public override bool IsInvalid => this.Handle == IntPtr.Zero;

        internal GCHandle m_gCHandle;

        internal SDK.XBL.XblMultiplayerSessionChangedHandler sessionChangedCallback;
        internal XblFunctionContext sessionChangedHandlerId;

        internal SDK.XBL.XblMultiplayerSessionSubscriptionLostHandler sessionSubscriptionLostCallback;
        internal XblFunctionContext sessionSubscriptionLostId;

        internal SDK.XBL.XblMultiplayerConnectionIdChangedHandler connectionIdChangedCallback;
        internal XblFunctionContext connectionIdChangedId;

        internal SDK.XBL.XblUserStatisticsStatisticChangedHandler statisticChangedCallback;
        internal XblFunctionContext statisiticsAddChangeId;
    }
}
