using System;
using UnityEditor;

namespace Unity.Microsoft.GDK.Editor
{
    /// <summary>
    /// Represents information about a GDK assembly that needs to be processed during import.
    /// </summary>
    internal sealed class GdkAssemblyInfo
    {
        /// <summary>
        /// Gets the name of the assembly file, including its extension.
        /// </summary>
        /// <value>The full name of the assembly file (e.g., "XCurl.dll").</value>
        public string Name { get; }

        /// <summary>
        /// Gets the full path to the source location of the assembly.
        /// </summary>
        /// <value>The absolute file system path where the assembly can be found in the GDK installation.</value>
        public string SourcePath { get; }

        /// <summary>
        /// Gets a value indicating whether this assembly should be included in the current GDK configuration.
        /// </summary>
        /// <value>
        /// <c>true</c> if the assembly should be included; otherwise, <c>false</c>.
        /// This may vary based on the GDK edition being used.
        /// </value>
        public bool Include { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GdkAssemblyInfo"/> class.
        /// </summary>
        /// <param name="name">The name of the assembly file, including its extension.</param>
        /// <param name="sourcePath">The full path to the source location of the assembly.</param>
        /// <param name="include">Whether this assembly should be included in the current GDK configuration.</param>
        public GdkAssemblyInfo(string name, string sourcePath, bool include)
        {
            Name = name;
            SourcePath = sourcePath;
            Include = include;
        }
    }
}