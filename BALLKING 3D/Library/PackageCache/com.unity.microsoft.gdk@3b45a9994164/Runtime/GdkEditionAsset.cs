using System;
using UnityEngine;

namespace Unity.XGamingRuntime
{
    /// <summary>
    /// Stores the GDK Edition number for runtime API compatibility checks.
    /// </summary>
    /// <remarks>
    /// This ScriptableObject is automatically generated during the build process to provide
    /// the active GDK edition number to the Microsoft GDK API wrappers at runtime. This information
    /// enables the wrappers to handle compatibility differences between GDK editions.
    /// </remarks>
    public class GdkEditionAsset : ScriptableObject
    {
        [SerializeField]
        [Tooltip("The GDK Edition number used for API compatibility checks")]
        private Int32 m_EditionNumber;

        public Int32 EditionNumber => m_EditionNumber;

        public static GdkEditionAsset CreateInstance(Int32 editionNumber)
        {
            var asset = ScriptableObject.CreateInstance<GdkEditionAsset>();
            asset.m_EditionNumber = editionNumber;
            return asset;
        }
    }
}