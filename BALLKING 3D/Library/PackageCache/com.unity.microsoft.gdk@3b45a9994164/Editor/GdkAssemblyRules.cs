using System.Collections.Generic;
using System.IO;

namespace Unity.Microsoft.GDK.Editor
{
    /// <summary>
    /// Describes where to find a DLL for editions >= <see cref="MinEdition"/>.
    /// </summary>
    internal readonly struct GdkSourceVariant
    {
        /// <summary>
        /// Minimum edition (inclusive) for which this variant applies.
        /// Uses >= comparison, so unlisted in-between editions (e.g. October25Update3)
        /// automatically match the closest boundary.
        /// </summary>
        internal int MinEdition { get; }

        /// <summary>
        /// Path relative to the GDK edition install folder (e.g. "GRDK\GameKit\Lib").
        /// Null when <see cref="PackagePath"/> is set (package-bundled DLL).
        /// </summary>
        internal string RelativePath { get; }

        /// <summary>
        /// Filename at the source location. Also used as the destination filename in the
        /// project unless <see cref="PluginFileName"/> overrides it.
        /// </summary>
        internal string SourceFileName { get; }

        /// <summary>
        /// Destination filename in the project when it differs from <see cref="SourceFileName"/>.
        /// Null means use <see cref="SourceFileName"/> as the destination.
        /// </summary>
        internal string PluginFileName { get; }

        /// <summary>
        /// Full Unity-relative path for DLLs bundled with the package rather than the GDK install.
        /// Null for GDK-installed DLLs.
        /// </summary>
        internal string PackagePath { get; }

        internal string EffectivePluginFileName => PluginFileName ?? SourceFileName;

        internal bool IsPackageBundled => PackagePath != null;

        internal GdkSourceVariant(int minEdition, string relativePath, string sourceFileName, string pluginFileName = null)
        {
            MinEdition = minEdition;
            RelativePath = relativePath;
            SourceFileName = sourceFileName;
            PluginFileName = pluginFileName;
            PackagePath = null;
        }

        GdkSourceVariant(string packagePath, string pluginFileName)
        {
            MinEdition = 0;
            RelativePath = null;
            SourceFileName = null;
            PluginFileName = pluginFileName;
            PackagePath = packagePath;
        }

        /// <summary>
        /// Creates a variant for a DLL bundled with the Unity package (not in the GDK install).
        /// </summary>
        internal static GdkSourceVariant Packaged(string packagePath, string pluginFileName) =>
            new(packagePath, pluginFileName);
    }

    /// <summary>
    /// Groups all edition-specific variants for one logical assembly.
    /// Variants must be ordered newest-first (highest MinEdition first).
    /// </summary>
    sealed class GdkAssemblyDefinition
    {
        internal GdkSourceVariant[] Variants { get; }

        internal GdkAssemblyDefinition(params GdkSourceVariant[] variants)
        {
            Variants = variants;
        }
    }

    /// <summary>
    /// Central registry of all GDK assembly mapping rules and resolution logic.
    /// This is the single source of truth for edition-specific DLL locations and naming.
    ///
    /// To add support for a new GDK edition, add a new <see cref="GdkSourceVariant"/>
    /// at the top of each definition (newest-first ordering).
    /// </summary>
    static class GdkAssemblyRules
    {
        // Each definition groups all edition-specific variants for one assembly.
        // Variants are ordered newest-first. The first variant where
        // editionNum >= variant.MinEdition wins. If no variant matches,
        // the assembly is excluded for that edition.
        static readonly GdkAssemblyDefinition[] s_Definitions =
        {
            // XCurl
            new(
                new GdkSourceVariant(GdkEditions.October25, @"windows\bin\x64", "XCurl.dll"),
                new GdkSourceVariant(GdkEditions.October24, @"GRDK\ExtensionLibraries\Xbox.XCurl.API\Redist\x64", "XCurl.dll"),
                new GdkSourceVariant(GdkEditions.October21, @"GRDK\ExtensionLibraries\Xbox.XCurl.API\Redist\CommonConfiguration\neutral", "XCurl.dll")
            ),

            // libHttpClient
            new(
                new GdkSourceVariant(GdkEditions.April26, @"windows\bin\x64", "libHttpClient.dll"),
                new GdkSourceVariant(GdkEditions.October25, @"windows\bin\x64", "libHttpClient.dll", "libHttpClient.GDK.dll"),
                new GdkSourceVariant(GdkEditions.October24, @"GRDK\ExtensionLibraries\Xbox.LibHttpClient\Redist\x64", "libHttpClient.GDK.dll"),
                new GdkSourceVariant(GdkEditions.June24, @"GRDK\ExtensionLibraries\Xbox.LibHttpClient\Redist\CommonConfiguration\neutral", "libHttpClient.GDK.dll")
            ),

            // XsApi
            new(
                new GdkSourceVariant(GdkEditions.October25, @"windows\bin\x64", "Microsoft.Xbox.Services.C.Thunks.dll", "Microsoft.Xbox.Services.GDK.C.Thunks.dll"),
                new GdkSourceVariant(GdkEditions.October24, @"GRDK\ExtensionLibraries\Xbox.Services.API.C\Lib\x64\Release", "Microsoft.Xbox.Services.GDK.C.Thunks.dll"),
                new GdkSourceVariant(GdkEditions.October23, @"GRDK\ExtensionLibraries\Xbox.Services.API.C\DesignTime\CommonConfiguration\Neutral\Lib\Release", "Microsoft.Xbox.Services.GDK.C.Thunks.dll"),
                new GdkSourceVariant(0, @"GRDK\ExtensionLibraries\Xbox.Services.API.C\DesignTime\CommonConfiguration\Neutral\Lib\Release", "Microsoft.Xbox.Services.141.GDK.C.Thunks.dll", "Microsoft.Xbox.Services.GDK.C.Thunks.dll")
            ),

            // XGameRuntime
            new(
                new GdkSourceVariant(GdkEditions.October25, @"windows\bin\x64", "XGameRuntime.Thunks.dll"),
                new GdkSourceVariant(GdkEditions.March24, @"GRDK\GameKit\Lib", "XGameRuntime.Thunks.dll"),
                GdkSourceVariant.Packaged("Packages/com.unity.microsoft.gdk/Assemblies/XGamingRuntimeThunks.dll", "XGameRuntime.Thunks.dll")
            ),
        };

        /// <summary>
        /// Resolves all assembly rules for the given edition, producing GdkAssemblyInfo entries
        /// for both active assemblies (Include=true) and stale files that need cleanup (Include=false).
        /// </summary>
        internal static IEnumerable<GdkAssemblyInfo> Resolve(int editionNum, string gdkInstallPath)
        {
            foreach (var definition in s_Definitions)
            {
                GdkSourceVariant? matched = null;
                foreach (var variant in definition.Variants)
                {
                    if (editionNum >= variant.MinEdition)
                    {
                        matched = variant;
                        break;
                    }
                }

                if (matched.HasValue)
                {
                    var active = matched.Value;
                    string sourcePath;
                    if (active.IsPackageBundled)
                    {
                        sourcePath = active.PackagePath;
                    }
                    else
                    {
                        var fileName = active.SourceFileName;
                        sourcePath = Path.Combine(
                            gdkInstallPath,
                            Path.Combine(active.RelativePath, fileName));
                    }

                    yield return new GdkAssemblyInfo(active.EffectivePluginFileName, sourcePath, true);

                    // Yield cleanup entries for old-named files from other variants
                    var activePluginName = active.EffectivePluginFileName;
                    var seen = new HashSet<string> { activePluginName };
                    foreach (var variant in definition.Variants)
                    {
                        var name = variant.EffectivePluginFileName;
                        if (seen.Add(name))
                        {
                            yield return new GdkAssemblyInfo(name, null, false);
                        }
                    }
                }
                else
                {
                    // No variant matched — exclude all possible filenames for this assembly
                    var seen = new HashSet<string>();
                    foreach (var variant in definition.Variants)
                    {
                        var name = variant.EffectivePluginFileName;
                        if (seen.Add(name))
                        {
                            yield return new GdkAssemblyInfo(name, null, false);
                        }
                    }
                }
            }
        }
    }
}