using System.Globalization;
using System.Linq;

namespace Unity.Microsoft.GDK.Discovery
{
    /// <summary>
    /// Utility class for the GDK Discovery package.
    /// </summary>
    public static class GdkVersion
    {
        private static readonly CultureInfo k_EnglishCulture = CultureInfo.GetCultureInfo("en-US");

        /// <summary>
        /// Converts a GDK edition number to its string representation.
        /// </summary>
        /// <param name="editionNumber">The GDK edition number to convert.</param>
        /// <returns>A formatted string representing the GDK edition.</returns>
        /// <remarks>
        /// A GDK edition number alone does not provide sufficient information to know the edition type
        /// (generally "GDK" or "GDKX"). When the type can not be detected form the locally available editions
        /// the type will be shown as "GDK". Use `Format(int editionNumber, string editionType)` if more control is needed.
        /// </remarks>
        public static string Format(int editionNumber)
        {
            foreach (var gdk in GdkEnumerator.DiscoveredGdks)
            {
                if(gdk.Edition == editionNumber)
                    return Format(gdk);
            }
            return Format(editionNumber, "GDK");
        }

        /// <summary>
        /// Converts a GDK edition to its string representation.
        /// </summary>
        /// <param name="gdkData">The GDK data structure to convert.</param>
        /// <returns>A formatted string representing the GDK data.</returns>
        public static string Format(GdkData gdkData)
        {
            bool hasXboxComponent = gdkData.ComponentPaths != null && 
                              (gdkData.ComponentPaths.ContainsKey("GXDK") || gdkData.ComponentPaths.ContainsKey("XboxDev"));

            var gdk = hasXboxComponent ? "GDKX" : "GDK";

            return Format(gdkData.Edition, gdk);
        }

        /// <summary>
        /// Formats a GDK edition number and type into a standardized string representation.
        /// </summary>
        /// <param name="editionNumber">
        /// The GDK edition number in the format YYMMUU where:
        /// - YY represents the last two digits of the year
        /// - MM represents the month (01-12)
        /// - UU represents the update number (00 when no update applies)
        /// </param>
        /// <param name="editionType">The type of GDK edition (generally "GDK" or "GDKX").</param>
        /// <returns>
        /// A formatted string in the pattern "Month 20YY editionType Update UU (editionNumber)".
        /// For example: "March 2023 GDKX Update 1 (230301)" or "June 2023 GDK (230600)".
        /// </returns>
        /// <remarks>
        /// The method uses English (en-US) culture for month names regardless of the system's current culture.
        /// Update number is only included in the output if it's non-zero.
        /// </remarks>
        public static string Format(int editionNumber, string editionType)
        {
            var dateTimeFormat = k_EnglishCulture.DateTimeFormat;
            var month = dateTimeFormat.GetMonthName((int)(editionNumber / 100) % 100);
            var update = editionNumber % 100;
            var updateString = update != 0 ? $" Update {update}" : "";
            var year = 2000 + editionNumber / 10000;

            return $"{month} {year} {editionType}{updateString} ({editionNumber})";
        }
    }
}