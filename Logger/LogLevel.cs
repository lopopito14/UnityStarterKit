//-----------------------------------------------------------------------
// <copyright file="LogLevel.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Logger
{
    using System;

    [Flags]
    public enum LogLevel
    {
        Nothing = 0,
        Exception = 1,
        Error = 2,
        Warning = 4,
        Info = 8,
        Debug = 16,

        Development = Exception | Error | Warning | Info | Debug,
        Production = Exception | Error | Warning | Info,
    }
}
