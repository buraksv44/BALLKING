using System;
using UnityEditor;
using UnityEngine;

namespace Unity.Microsoft.GDK.Editor
{
    /// <summary>
    /// Provides debug logging utilities for the Microsoft GDK API package.
    /// </summary>
    internal static class GdkApiDebug
    {
        /// <summary>
        /// Prefix added to all log messages to identify them as GDK API related.
        /// </summary>
        const string k_LogPrefix = @"[Microsoft GDK] ";

        /// <summary>
        /// Default logging options used when none are specified.
        /// </summary>
        private const LogOption k_DefaultLogOptions = LogOption.NoStacktrace;

        /// <summary>
        /// Logs an informational message to the Unity console.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="logOptions">Optional logging options. Defaults to NoStacktrace.</param>
        public static void Log(string message, LogOption logOptions=k_DefaultLogOptions)
        {
            Debug.LogFormat(LogType.Log, logOptions, null, $"{k_LogPrefix}{message}");
        }

        /// <summary>
        /// Logs a warning message to the Unity console.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        /// <param name="logOptions">Optional logging options. Defaults to NoStacktrace.</param>
        public static void LogWarning(string message, LogOption logOptions=k_DefaultLogOptions)
        {
            Debug.LogFormat(LogType.Warning, logOptions, null, $"{k_LogPrefix}{message}");
        }

        /// <summary>
        /// Logs an error message to the Unity console.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        /// <param name="logOptions">Optional logging options. Defaults to NoStacktrace.</param>
        public static void LogError(string message, LogOption logOptions=k_DefaultLogOptions)
        {
            Debug.LogFormat(LogType.Error, logOptions, null, $"{k_LogPrefix}{message}");
        }
    }
}