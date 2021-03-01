//-----------------------------------------------------------------------
// <copyright file="UnityLog.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Logger.Services
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Logger used to log message into native Unity debug console.
    /// </summary>
    public class UnityLogService : LogService
    {
        /// <summary>
        /// Log an exception message in Unity debug console.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="memberName">The caller member.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        protected override void LogException(
            Exception exception,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            UnityEngine.Debug.LogError(FormatMessage(exception.ToString(), nameof(Color.cyan), memberName, sourceFilePath, sourceLineNumber));
        }

        /// <summary>
        /// Log an error message in Unity debug console.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="memberName">The caller member.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        protected override void LogError(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            UnityEngine.Debug.LogError(FormatMessage(message, nameof(Color.red), memberName, sourceFilePath, sourceLineNumber));
        }

        /// <summary>
        /// Log a warning message in Unity debug console.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="memberName">The caller member.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        protected override void LogWarning(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            UnityEngine.Debug.LogWarning(FormatMessage(message, nameof(Color.yellow), memberName, sourceFilePath, sourceLineNumber));
        }

        /// <summary>
        /// Log an info message in Unity debug console.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="memberName">The caller member.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        protected override void LogInfo(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            UnityEngine.Debug.Log(FormatMessage(message, nameof(Color.green), memberName, sourceFilePath, sourceLineNumber));
        }

        /// <summary>
        /// Log a debug message in Unity debug console.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="memberName">The caller member.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        protected override void LogDebug(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            UnityEngine.Debug.Log(FormatMessage(message, nameof(Color.white), memberName, sourceFilePath, sourceLineNumber));
        }

        /// <summary>
        /// Format the input message.
        /// </summary>
        /// <param name="message">The message to format</param>
        /// <param name="color">The color of the message in debug console.</param>
        /// <param name="memberName">The caller member.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        /// <returns>The formated message.</returns>
        private static string FormatMessage(string message, string color, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            if (message == null) message = string.Empty;

            message = message.Replace("\r\n", " ");

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"[<b>{memberName}</b>]");
            stringBuilder.Append($" ");
            stringBuilder.Append($"<size=10><<i>{ExtractFileName(sourceFilePath)} (l.{sourceLineNumber})</i>></size>");
            stringBuilder.Append($" ");
            stringBuilder.Append($"<color={color}><size=14>{message}</size></color>.");
            stringBuilder.Append($"\n");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Gets the file name from a source file path.
        /// </summary>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <returns>The file name.</returns>
        private static string ExtractFileName(string sourceFilePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(sourceFilePath);
                return fileInfo.Name;
            }
            catch (Exception)
            {
                return sourceFilePath;
            }
        }
    }
}