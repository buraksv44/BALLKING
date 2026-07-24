using System;
using System.Collections.Generic;
using Unity.Microsoft.GDK.Editor;
using UnityEditor;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    /// <summary>
    /// There is no way to get the required GDK Edition for a none Windows Standalone GDK platform in older
    /// Unity Editor versions. For now this may be the only solution.
    /// </summary>
    internal class GdkEditionMapper
    {
        private readonly SortedDictionary<int, List<VersionRange>> m_EditionRanges = new();

        // Represents a range of Unity versions for a specific GDK edition
        private class VersionRange
        {
            private UnityVersion MinVersion { get; }
            private UnityVersion MaxVersion { get; }

            public VersionRange(UnityVersion minVersion, UnityVersion maxVersion)
            {
                MinVersion = minVersion;
                MaxVersion = maxVersion;
            }

            public bool Contains(UnityVersion version)
            {
                return version.CompareTo(MinVersion) >= 0 && version.CompareTo(MaxVersion) <= 0;
            }
        }

        public GdkEditionMapper()
        {
            AddEditionRanges(GdkEditions.October24, new[]
            {
                ("6000.2.0a1", "6000.2.0a7"),
                ("6000.1.0a1", "6000.1.0a10"),
                ("6000.0.32f1", "6000.0.42f1"),
                ("2022.3.55f1", "2022.3.60f1"),
                ("2021.3.47f1", "2021.3.50f1")
            });

            AddEditionRanges(GdkEditions.June24, new[]
            {
                ("6000.0.14f1", "6000.0.31f1"),
                ("2022.3.44f1", "2022.3.54f1"),
                ("2021.3.44f1", "2021.3.46f1")
            });

            AddEditionRanges(GdkEditions.March24Update1, new[]
            {
                ("6000.0.7f1",  "6000.0.13f1"),
                ("2022.3.36f1", "2022.3.43f1"),
                ("2021.3.41f1", "2021.3.43f1")
            });

            // As of Feb 25, these GDKs are no-longer officially supported

            AddEditionRanges(GdkEditions.October23Update4, new[]
            {
                ("6000.0.0f1",  "6000.0.5f1"),
                ("2022.3.28f1", "2022.3.35f1"),
                ("2021.3.39f1", "2021.3.40f1")
            });

            AddEditionRanges(GdkEditions.October23Update2, new[]
            {
                ("6000.0.0b11", "6000.0.0b16")
            });
        }

        private void AddEditionRanges(int editionNumber, (string min, string max)[] ranges)
        {
            if (!m_EditionRanges.ContainsKey(editionNumber))
            {
                m_EditionRanges[editionNumber] = new List<VersionRange>();
            }

            foreach (var (min, max) in ranges)
            {
                m_EditionRanges[editionNumber].Add(new VersionRange(
                    new UnityVersion(min),
                    new UnityVersion(max)
                ));
            }
        }

        public int GetGdkEditionForUnityVersion(string versionStr)
        {
            var version = new UnityVersion(versionStr);
            return GetGdkEditionForUnityVersion(version);
        }

        public int GetGdkEditionForUnityVersion(UnityVersion version)
        {
            foreach (var edition in m_EditionRanges)
            {
                foreach (var range in edition.Value)
                {
                    if (range.Contains(version))
                    {
                        return edition.Key;
                    }
                }
            }

            throw new ArgumentException($"No matching GDK edition found for Unity version: {version}");
        }
    }
}