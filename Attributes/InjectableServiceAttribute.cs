//-----------------------------------------------------------------------
// <copyright file="InjectableServiceAttribute.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Attributes
{
    using System;

    /// <summary>
    /// The InjectableService attribute is used as decorator of a public field of a class inherited of BaseComponent<T>.
    /// It uses dependency injection to initialize this field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter)]
    public class InjectableServiceAttribute : Attribute { }
}