//-----------------------------------------------------------------------
// <copyright file="UnityCallAttribute.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Attributes
{
    using System;

    /// <summary>
    /// The UnityCallAttribute attribute is used as decorator of a public method that is called by Unity.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class UnityCallAttribute : Attribute { }
}