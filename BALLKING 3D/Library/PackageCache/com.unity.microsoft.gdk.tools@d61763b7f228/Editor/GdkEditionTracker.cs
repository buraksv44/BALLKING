using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Unity.Microsoft.GDK.Tools.Editor
{
    /// <summary>
    /// Manages the tracking and persistence of GDK Edition numbers through a JSON file.
    /// </summary>
    internal class GdkEditionTracker
    {
        internal const int Min_Value = 200000;

        private const string k_DefaultGdkEditionTrackerPath = @"Plugins/GDK/GdkEdition.json";

        [System.Serializable]
        private class EditionData
        {
            public int GdkEdition;
        }

        private readonly string m_FilePath;

        /// <summary>
        /// Initializes a new instance of GdkEditionTracker.
        /// </summary>
        /// <param name="filePath">The location of the JSON file tracking the GdkEdition.</param>
        public GdkEditionTracker(string filePath=null)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = Path.Combine(Application.dataPath, k_DefaultGdkEditionTrackerPath);
            }

            m_FilePath = filePath;
        }

        /// <summary>
        /// Checks if the edition file exists in the specified path.
        /// </summary>
        /// <returns>True if the file exists, false otherwise.</returns>
        public bool TrackerFileExists()
        {
            return File.Exists(m_FilePath);
        }

        /// <summary>
        /// Loads the GDK Edition number from the JSON file.
        /// </summary>
        /// <returns>The GDK Edition number if successful, -1 otherwise.</returns>
        public int ReadGdkEdition()
        {
            try
            {
                if (!TrackerFileExists())
                {
                    return -1;
                }

                var jsonContent = File.ReadAllText(m_FilePath);
                var editionData = JsonUtility.FromJson<EditionData>(jsonContent);

                return (editionData.GdkEdition < Min_Value ? -1 : editionData.GdkEdition);
            }
            catch (Exception /*e*/)
            {
                return -1;
            }
        }

        /// <summary>
        /// Saves the provided GDK Edition number to the JSON file. NOTE: This method does not
        /// verify that the edition value being written to the tracker file is valid.
        /// </summary>
        /// <param name="edition">The GDK Edition number to save.</param>
        /// <returns>True if save was successful, false otherwise.</returns>
        public bool WriteGdkEdition(int edition)
        {
            try
            {
                var directoryPath = Path.GetDirectoryName(m_FilePath);
                if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var editionData = new EditionData { GdkEdition = edition };
                var jsonContent = JsonUtility.ToJson(editionData, true);

                File.WriteAllText(m_FilePath, jsonContent);
                return true;
            }
            catch (Exception /*e*/)
            {
                return false;
            }
        }
    }
}