using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace Unity.Microsoft.GDK.Discovery
{
    /// <summary>
    /// Provides methods to retrieve GDK (Game Development Kit) data from environment variables.
    /// </summary>
    public static class GdkEnvVarData
    {
        private static GdkData? _gdkFromEnv;

        /// <summary>
        /// Retrieves GDK data from environment variables.
        /// </summary>
        /// <returns>An IEnumerable of GdkData objects containing information about the GDK detected from environment variables.</returns>
        /// <remarks>
        /// This method will only return data on Windows platforms. It caches the result after the first call for improved performance.
        /// </remarks>
        public static IEnumerable<GdkData> GetGDKData()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                yield break;

            if (_gdkFromEnv == null)
            {
                _gdkFromEnv = GetEnvVarGDK();
            }

            if (_gdkFromEnv != null)
                yield return _gdkFromEnv.Value;
        }

        //Detect GDK as per:  https://learn.microsoft.com/en-us/gaming/gdk/_content/gc/tools-console/usingwithoutinstall/build-environment-withoutinstall
        private static GdkData? GetEnvVarGDK()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return null;

            var gameDk = Environment.GetEnvironmentVariable("GameDK");
            if (gameDk == null || !Directory.Exists(gameDk))
                return null;

            var grdkEditionString = Environment.GetEnvironmentVariable("GRDKEDITION");
            var isGRDKEdition = int.TryParse(grdkEditionString, NumberStyles.Any, CultureInfo.InvariantCulture, out var grdkEdition);

            var gxdkEditionString = Environment.GetEnvironmentVariable("GXDKEDITION");
            var isGXDKEdition = int.TryParse(gxdkEditionString, NumberStyles.Any, CultureInfo.InvariantCulture, out var gxdkEdition);

            // Check if we have at least GRDKEDITION or GXDKEDITION environment variables set
            var isGdkEditionsValid = (isGRDKEdition && grdkEdition != 0) || (isGXDKEdition && gxdkEdition != 0);
            if (!isGdkEditionsValid || (!Directory.Exists($"{gameDk}\\{grdkEdition}") && (!Directory.Exists($"{gameDk}\\{gxdkEdition}"))))
                return null;

            var edition = isGXDKEdition ? gxdkEdition : grdkEdition;
            var componentPaths = new Dictionary<string, string>();
  
            var gameDKXboxBuildEnvVar = Environment.GetEnvironmentVariable("GameDKXboxBuild");
            if (!string.IsNullOrEmpty(gameDKXboxBuildEnvVar) && Directory.Exists(gameDKXboxBuildEnvVar))
            {
                componentPaths.Add("XboxDev", Path.Combine(gameDKXboxBuildEnvVar, "xbox"));
            }

            var gameDKCoreBuildEnvVar = Environment.GetEnvironmentVariable("GameDKCoreBuild");
            if (!string.IsNullOrEmpty(gameDKCoreBuildEnvVar) && Directory.Exists(gameDKCoreBuildEnvVar))
            {
                componentPaths.Add("PCDev", Path.Combine(gameDKCoreBuildEnvVar, "windows"));
            }

            // If we don't have any component paths from the new defines added in the October 25 (251000) GDK.
            // Then we check for the older defines 'GamingGRDKDKBuild'/'GamingGXDKBuild'.
            if (componentPaths.Count == 0)
            { 
                foreach (DictionaryEntry envVar in Environment.GetEnvironmentVariables())
                {
                    var envVarKey = envVar.Key.ToString();
                    var envVarValue = envVar.Value?.ToString();
                    if (string.IsNullOrEmpty(envVarKey) || string.IsNullOrEmpty(envVarValue) ||
                        !envVarKey.StartsWith("Gaming") || !envVarKey.EndsWith("DKBuild"))
                        continue;

                    var component = envVarKey.Replace("Gaming","").Replace( "Build","");
                    if(Directory.Exists(envVarValue))
                        componentPaths.Add(component, envVarValue );
                }
            }

            return new GdkData{Edition = edition, Path = gameDk, ComponentPaths = componentPaths};
        }
    }
}