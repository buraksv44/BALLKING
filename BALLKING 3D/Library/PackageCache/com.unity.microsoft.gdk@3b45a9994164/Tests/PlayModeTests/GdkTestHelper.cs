using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Threading;
using NUnit.Framework;
using Unity.XGamingRuntime;

#if UNITY_EDITOR_WIN
using UnityEditor;
#endif

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)

namespace Unity.Microsoft.GDK.Tests
{
    public class GdkTestHelper : MonoBehaviour
    {
        static Thread m_DispatchJob;
        static bool m_StopExecution;
        static bool m_xblInitialized = false;

#if UNITY_STANDALONE_WIN
        private static string testScid = "00000000-0000-0000-0000-000062AB3C24";
#else
        private static string testScid = "00000000-0000-0000-0000-000073eca03c";
#endif

        private static Dictionary<int, string> _hresultToFriendlyErrorLookup;

        public static bool Initialize(string caller)
        {
            UnityEngine.Debug.Log($"GdkTestHelper.Initialize()\n\t- Caller: {caller}, Application.platform: {Application.platform}");

            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            {
                string exePath = Application.dataPath;
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
#if UNITY_EDITOR_WIN
                    exePath = EditorApplication.applicationPath;
#endif
                }

                string microsoftGameConfigPath = Path.Combine(Path.GetDirectoryName(exePath), "MicrosoftGame.Config");

                UnityEngine.Debug.Log($"\t- EXE PATH: {exePath}");
            }

            _hresultToFriendlyErrorLookup = new Dictionary<int, string>();
            InitializeHresultToFriendlyErrorLookup();

            try
            {
                int hr = SDK.XGameRuntimeInitialize();
                UnityEngine.Debug.Log($"\t- SDK.XGameRuntimeInitialize() returned hr={hr} {HR.NameOf(hr)}");
                if (!Succeeded(hr, "Initialize gaming runtime"))
                {
                    UnityEngine.Debug.LogError(
                        "You may need to update your config file for the editor. GDK -> PC -> Update Editor Game Config will copy your current game config to the Unity.exe location to enable GDK features when playing in-editor.");
                    return false;
                }

                hr = SDK.CreateDefaultTaskQueue();
                if (!Succeeded(hr, "Create default task queue"))
                {
                    return false;
                }

                UnityEngine.Debug.Log($"\t- Start the async task dispatch thread");

                m_StopExecution = false;
                m_DispatchJob = new Thread(DispatchGDKTaskQueue) { Name = "GDK Task Queue Dispatch" };
                m_DispatchJob.Start();

                if (!m_xblInitialized)
                {
                    hr = SDK.XBL.XblInitialize(testScid);
                    UnityEngine.Debug.Log($"\t- SDK.XBL.XblInitialize() returned hr={hr} {HR.NameOf(hr)}");
                    if (hr==HR.E_XBL_ALREADY_INITIALIZED)
                    {
                        UnityEngine.Debug.Log("Xbox Live services already initialized");
                    }
                    else if (!Succeeded(hr, "Initialising Xbox Live services"))
                    {
                        UnityEngine.Debug.Log("Error initialising the gaming runtime, hresult: " + hr + " " + "0x" +
                                    (hr).ToString("X"));
                        return false;
                    }

                    m_xblInitialized = true;
                }                
            }
            catch (System.DllNotFoundException)
            {
                throw new IgnoreException("Ignoring test: `XGameRuntime.Thunks assembly not found` is the GDK installed.");
            }

            UnityEngine.Debug.Log($"\t- Initialize: DONE");
            return true;
        }

        public static void Shutdown(string caller)
        {
            UnityEngine.Debug.Log($"{caller} Shutdown START");

            if (m_xblInitialized == false)
                return; // Nothing to shut down

            // Stop task queue execution and wait for thread to join
            m_StopExecution = true;
            m_DispatchJob.Join();

            SDK.XBL.XblCleanup(null);
            m_xblInitialized = false;

            SDK.CloseDefaultXTaskQueue();

            SDK.XGameRuntimeUninitialize();

            UnityEngine.Debug.Log($"{caller} Shutdown DONE");
        }

        private static void InitializeHresultToFriendlyErrorLookup()
        {
            if (_hresultToFriendlyErrorLookup == null)
            {
                return;
            }

            _hresultToFriendlyErrorLookup.Add(-2143330041,
                "IAP_UNEXPECTED: Does the player you are signed in as have a license for the game? " +
                "You can get one by downloading your game from the store and purchasing it first. If you can't find your game in the store, " +
                "have you published it in Partner Center?");

            _hresultToFriendlyErrorLookup.Add(-1994108656,
                "E_GAMEUSER_NO_PACKAGE_IDENTITY: Are you trying to call GDK APIs from the Unity editor?" +
                " To call GDK APIs, you must use the GDK > Build and Run menu. You can debug your code by attaching the Unity debugger once your" +
                "game is launched.");

            _hresultToFriendlyErrorLookup.Add(-1994129152,
                "E_GAMERUNTIME_NOT_INITIALIZED: Are you trying to call GDK APIs from the Unity editor?" +
                " To call GDK APIs, you must use the GDK > Build and Run menu. You can debug your code by attaching the Unity debugger once your" +
                "game is launched.");

            _hresultToFriendlyErrorLookup.Add(-2015559675,
                "AM_E_XAST_UNEXPECTED: Have you added the Windows 10 PC platform on the Xbox Settings page " +
                "in Partner Center? Learn more: aka.ms/sandboxtroubleshootingguide");
        }


        private static void DispatchGDKTaskQueue()
        {
            UnityEngine.Debug.Log($"DispatchGDKTaskQueue: Start...");

            try
            {
                // this will execute all GXDK async work
                while (m_StopExecution == false)
                {
                    try
                    {
                        bool dispatched = SDK.XTaskQueueDispatch(0);
                    }
                    // we need to catch and continue otherwise some tests could wait indefinitely if they don't have a timeout
                    catch (Exception e)
                    {
                        UnityEngine.Debug.Log("Uncaught exception raised while processing callbacks.");
                        UnityEngine.Debug.LogException(e);
                        Assert.Fail(e.ToString());
                    }

                    Thread.Sleep(32);
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
                Assert.Fail(e.ToString());
            }
            finally
            {
                UnityEngine.Debug.Log($"DispatchGXDKTaskQueue: ...End");
            }
        }

        // Helper methods
        protected static bool Succeeded(int hresult, string operationFriendlyName)
        {
            bool succeeded = false;
            if (HR.SUCCEEDED(hresult))
            {
                succeeded = true;
            }
            else
            {
                string errorCode = hresult.ToString("X8");
                string errorMessage = string.Empty;
                if (_hresultToFriendlyErrorLookup.ContainsKey(hresult))
                {
                    errorMessage = _hresultToFriendlyErrorLookup[hresult];
                }
                else
                {
                    errorMessage = operationFriendlyName + " failed.";
                }

                string formattedErrorString = string.Format("{0} Error code: hr=0x{1}", errorMessage, errorCode);
                UnityEngine.Debug.LogError(formattedErrorString);
                //if (Helpers.OnError != null)
                //{
                //    Helpers.OnError(Helpers, new ErrorEventArgs(errorCode, errorMessage));
                //}
            }

            return succeeded;
        }
    }
}

#endif //#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)