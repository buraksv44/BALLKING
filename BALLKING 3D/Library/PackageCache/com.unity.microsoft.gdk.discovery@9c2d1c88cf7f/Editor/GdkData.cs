using System.Collections.Generic;

namespace Unity.Microsoft.GDK.Discovery
{
    /// <summary>
    /// Data structure to hold information about the discovered GDK installation.
    /// </summary>
    public struct GdkData
    {
        /// <summary>
        /// The version of the GDK Installation.
        /// </summary>
        public int Edition { get; set; }

        /// <summary>
        /// The path to the GDK installation.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// GDK components that are available.
        /// </summary>
        public Dictionary<string, string> ComponentPaths { get; set;}
    }
}
