using System;
using System.Globalization;
using System.Linq;
using Unity.Microsoft.GDK.Discovery;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    internal class MicrosoftGdkData
    {
        private static string _latestEdition;

        public static bool IsInstalled()
        {
            // We currently use the latest edition
            var isGdkInstalled = !string.IsNullOrEmpty(LatestGdk?.Path);

            return isGdkInstalled;
        }

        private static readonly GdkData? LatestGdk;
        static MicrosoftGdkData()
        {
            var installedGdKs = GdkEnumerator.DiscoveredGdks.OrderByDescending(data => data.Edition).ToArray();
            LatestGdk = installedGdKs.FirstOrDefault();
        }
        public static string LatestGdkEdition
        {
            get
            {
                if (string.IsNullOrEmpty(_latestEdition) && LatestGdk.HasValue)
                {
                    _latestEdition = LatestGdk.Value.Edition.ToString(CultureInfo.InvariantCulture);
                }

                return _latestEdition;
            }
        }

        internal static bool IsEditionAvailable(int edition)
        {
            return GdkEnumerator.DiscoveredGdks
                .Any(gdkData => gdkData.Edition == edition && !string.IsNullOrEmpty(gdkData.Path));
        }
    }
} // namespace Unity.Microsoft.GDK.Tools.Editor