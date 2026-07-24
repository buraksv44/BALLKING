using NUnit.Framework;
using Unity.XGamingRuntime;

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)

namespace Unity.Microsoft.GDK.Tests
{
    public static class AssertUtils
    {
        public static void AssertSucceeded(this int hr, string message="")
        {
            if (HR.SUCCEEDED(hr)) return;

            var name = HR.NameOf(hr);
            if (!string.IsNullOrEmpty(name))
            {
                Assert.Fail($"Fail: hResult=0x{hr:X8}, '{name}' ({message})");
            }
            else
            {
                Assert.Fail($"Fail: hResult=0x{hr:X8} ({message})");
            }
        }
    }
}

#endif //#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)