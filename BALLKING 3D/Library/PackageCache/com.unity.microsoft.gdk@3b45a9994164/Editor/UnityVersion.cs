using System;
using UnityEditor;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    /// <summary>
    /// Represents a Unity Engine version number with support for release suffixes (a, b, f).
    /// Provides comparison capabilities to determine version ordering.
    /// </summary>
    /// <remarks>
    /// Unity versions follow the format "X.Y.Z[suffix][number]" where:
    /// - X.Y.Z is the semantic version number
    /// - suffix is one of 'a' (alpha), 'b' (beta), or 'f' (final)
    /// - number is the build number for that suffix
    /// Example: "2022.3.1f1" or "2023.1.0b5"
    /// </remarks>
    internal class UnityVersion : IComparable<UnityVersion>
    {
        private static readonly char[] k_ValidSuffixes = { 'f', 'b', 'a' };

        private readonly Version m_BaseVersion;
        private readonly char m_Suffix;
        private readonly int m_SuffixVersion;

        /// <summary>
        /// Initializes a new instance of UnityVersion by parsing a version string.
        /// </summary>
        /// <param name="version">The version string to parse (e.g., "2022.3.1f1").</param>
        /// <exception cref="ArgumentNullException">Thrown when version is null.</exception>
        /// <exception cref="FormatException">Thrown when version string is in an invalid format.</exception>
        public UnityVersion(string version)
        {
            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentNullException(nameof(version));
            }

            // Find the position of the suffix
            var suffixIndex = version.IndexOfAny(k_ValidSuffixes);
            if (suffixIndex != -1)
            {
                // Split the version into numeric part and suffix part
                var numericPart = version.Substring(0, suffixIndex);
                m_Suffix = version[suffixIndex];
                m_SuffixVersion = int.Parse(version.Substring(suffixIndex + 1));
                m_BaseVersion = Version.Parse(numericPart);
            }
            else
            {
                m_BaseVersion = Version.Parse(version);
                m_Suffix = 'f'; // Default to final release
                m_SuffixVersion = 0;
            }
        }

        /// <summary>
        /// Compares this version with another Unity version.
        /// </summary>
        /// <param name="other">The Unity version to compare against.</param>
        /// <returns>
        /// Less than zero if this version is lower than other, zero if they are equal or greater than zero
        /// if this version is higher than other.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when other is invalid.</exception>
        public int CompareTo(UnityVersion other)
        {
            if (other == null)
            {
                throw new ArgumentException($"other can not be null");
            }

            // Compare numeric versions first
            var baseComparison = m_BaseVersion.CompareTo(other.m_BaseVersion);
            if (baseComparison != 0)
            {
                return baseComparison;
            }

            // Compare suffix type (a < b < f)
            var suffixComparison = CompareSuffix(m_Suffix, other.m_Suffix);
            if (suffixComparison != 0)
            {
                return suffixComparison;
            }

            // Compare suffix version
            return m_SuffixVersion.CompareTo(other.m_SuffixVersion);
        }

        /// <summary>
        /// Compares two version suffixes based on their release stage.
        /// </summary>
        /// <param name="suffix1">The first suffix to compare.</param>
        /// <param name="suffix2">The second suffix to compare.</param>
        /// <returns>
        /// Less than zero if suffix1 is earlier than suffix2, zero if they are equal, or greater than zero
        /// if suffix1 is later than suffix2.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when either suffix is invalid.</exception>
        private int CompareSuffix(char suffix1, char suffix2)
        {
            int GetSuffixWeight(char suffix) => suffix switch
            {
                'a' => 0,
                'b' => 1,
                'f' => 2,
                _ => throw new ArgumentException($"Invalid suffix: {suffix}")
            };

            return GetSuffixWeight(suffix1).CompareTo(GetSuffixWeight(suffix2));
        }


        /// <summary>
        /// Returns a string representation of the Unity version.
        /// </summary>
        /// <returns>A string in the format "X.Y.Z[suffix][number]".</returns>
        public override string ToString()
        {
            return $"{m_BaseVersion}{m_Suffix}{m_SuffixVersion}";
        }
    }
}