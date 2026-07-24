using System;
using System.Xml;
using UnityEditor;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    /// <summary>
    /// Provides public events for the GDK Tools package.
    /// </summary>
    public static class GdkToolsEvents
    {
        /// <summary>
        /// Raised to provide an opportunity to update/patch the `makepkg.exe` package mapping layout file.
        /// </summary>
        /// <param name="mappingDoc">The XML document representing the layout file.</param>
        /// <param name="looseContentPath">The full path to the loose content directory.</param>
        /// <param name="buildTarget">The build target platform for the current build.</param>
        public static event Action<XmlDocument, string, BuildTarget> OnUpdatePackageMapping;

        /// <summary>
        /// Raises the <see cref="OnUpdatePackageMapping"/> event.
        /// </summary>
        /// <param name="mappingDoc">The XML document representing the layout file.</param>
        /// <param name="looseContentPath">The full path to the loose content directory.</param>
        /// <param name="buildTarget">The build target platform for the current build.</param>
        public static void RaiseUpdatePackageMapping(XmlDocument mappingDoc, string looseContentPath, BuildTarget buildTarget)
        {
            OnUpdatePackageMapping?.Invoke(mappingDoc, looseContentPath, buildTarget);
        }
    }
}