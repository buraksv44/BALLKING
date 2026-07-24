using NUnit.Framework;
using Unity.XGamingRuntime;
using UnityEngine;

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)

namespace Unity.Microsoft.GDK.Tests
{
    public class GetGdkEditionTest : MonoBehaviour
    {
        [Test]
        public void GetGdkEdition_ReturnsNonZero()
        {
            var editionNumber = SDK.GetGdkEdition();
            Debug.Log($"GetGdkEdition() returned: {editionNumber}");

            Assert.AreNotEqual(0, editionNumber, "Expected GetGdkEdition() to return a non-zero edition number");
        }
    }
}

#endif
