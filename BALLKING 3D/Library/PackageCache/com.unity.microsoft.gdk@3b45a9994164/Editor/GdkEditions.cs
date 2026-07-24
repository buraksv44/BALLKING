using UnityEditor;

namespace Unity.Microsoft.GDK.Editor
{
    /// <summary>
    /// Defines GDK editions.
    /// </summary>
    /// <remarks>
    /// This class maintains a registry of GDK editions using a numerical format YYMMUU where:
    /// - YY: Last two digits of the year (e.g., 24 for 2024)
    /// - MM: Month number (e.g., 10 for October)
    /// - UU: Update number (e.g., 00 for initial release, 01 for Update 1)
    ///
    /// Example: 241000 represents `October 2024` initial release
    ///
    /// The class also provides:
    /// - `Minimum` and `Recommended` editions
    /// - Simple compatibility validation
    /// </remarks>
    internal static class GdkEditions
    {
        internal const int Recommended = April26;
        internal const int Minimum = October23;

        internal const int April26 = 260400;
        internal const int October25Update1 = 251001;
        internal const int October25 = 251000;
        internal const int April25 = 250400;
        internal const int October24 = 241000;
        internal const int June24 = 240600;
        internal const int March24Update1 = 240301;
        internal const int March24 = 240300;
        internal const int October23Update4 = 231004;
        internal const int October23Update2 = 231002;
        internal const int October23 = 231000;
        internal const int October21 = 211000;

        internal enum Compatability
        {
            /// <summary>
            /// Represents a version that is too old to support.
            /// </summary>
            TooOld = -1,

            /// <summary>
            /// Represents an acceptable compatibility level.
            /// </summary>
            OK = 0,

            /// <summary>
            /// Represents a version that is too new and may not be supported.
            /// </summary>
            TooNew = 1
        }

        /// <summary>
        /// Validate the GDK Edition to ensure it meets our requirements.
        /// </summary>
        /// <param name="edition"> The GDK Edition version.</param>
        /// <returns> The validation result. </returns>
        internal static Compatability ValidateEdition(int edition)
        {
            return edition switch
            {
                < Minimum => Compatability.TooOld,
                > Recommended => Compatability.TooNew,
                _ => Compatability.OK
            };
        }
    }
}