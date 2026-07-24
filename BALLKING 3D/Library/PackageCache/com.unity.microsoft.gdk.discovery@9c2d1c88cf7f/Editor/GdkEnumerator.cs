using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.Microsoft.GDK.Discovery
{
    /// <summary>
    /// Provides methods to enumerate and access discovered GDK (Game Development Kit) installations.
    /// </summary>
    public static class GdkEnumerator
    {
        /// <summary>
        /// Gets a list of all discovered GDK installations.
        /// </summary>
        /// <value>An IEnumerable of GdkData objects representing discovered GDK installations.</value>
        public static IEnumerable<GdkData> DiscoveredGdks => Gdks.Value;

        private static readonly Lazy<IEnumerable<GdkData>> Gdks = new Lazy<IEnumerable<GdkData>> (() => GdkEnvVarData.GetGDKData().Concat(GdkRegistryData.GetGDKData()));
    }
}
