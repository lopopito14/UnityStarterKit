//-----------------------------------------------------------------------
// <copyright file="ILogService.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Logger.Services
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityStarterKit.Modularity;

    /// <summary>
    /// ILog main interface.
    /// </summary>
    public interface ILogService : IServiceModule
    {
        /// <summary>
        /// Log an exception message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="memberName">The caller member.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        void Exception(
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
        void Error(
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
        void Warning(
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
        void Info(
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
        void Debug(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);
    }
}
