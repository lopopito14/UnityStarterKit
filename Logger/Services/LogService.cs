//-----------------------------------------------------------------------
// <copyright file="LogService.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Logger.Services
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityStarterKit.Modularity;

    /// <summary>
    /// Logger base class.
    /// </summary>
    public abstract class LogService : ServiceModule, ILogService
    {
        /// <summary>
        /// The Log level of this logger.
        /// </summary>
        private readonly LogLevel logLevel;

        /// <summary>
        /// Defines a new Unity logger with a log level.
        /// </summary>
        protected LogService()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            this.logLevel = LogLevel.Development;
#else
            this.logLevel = LogLevel.Production;
#endif
        }

        /// <summary>
        /// Log an exception message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="memberName">The caller member.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        public void Exception(
            Exception exception,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if ((logLevel & LogLevel.Exception) == LogLevel.Exception)
            {
                LogException(exception, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// Log an error message.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="memberName">The caller member.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        public void Error(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if ((logLevel & LogLevel.Error) == LogLevel.Error)
            {
                LogError(message, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// Log a warning message.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="memberName">The caller member.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        public void Warning(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if ((logLevel & LogLevel.Warning) == LogLevel.Warning)
            {
                LogWarning(message, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// Log an info message.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="memberName">The caller member.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        public void Info(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if ((logLevel & LogLevel.Info) == LogLevel.Info)
            {
                LogInfo(message, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// Log a debug message.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="memberName">The caller member.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        public void Debug(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if ((logLevel & LogLevel.Debug) == LogLevel.Debug)
            {
                LogDebug(message, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// Log an exception message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="memberName">The caller member.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        protected abstract void LogException(
            Exception exception,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        /// <summary>
        /// Log an error message.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="memberName">The caller member.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        protected abstract void LogError(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        /// <summary>
        /// Log a warning message.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="memberName">The caller member.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        protected abstract void LogWarning(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        /// <summary>
        /// Log an info message.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="memberName">The caller member.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        protected abstract void LogInfo(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        /// <summary>
        /// Log a debug message.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="memberName">The caller member.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        protected abstract void LogDebug(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);
    }
}
