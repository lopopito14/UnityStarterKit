﻿//-----------------------------------------------------------------------
// <copyright file="BindableObjectAttribute.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Attributes
{
    using System;

    /// <summary>
    /// The BindableObject attribute is used as decorator of a public field of a class inherited of BaseComponent<T>.
    /// It allows to check, before performing computation with it, if the field is not null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class BindableObjectAttribute : Attribute
    {
        /// <summary>
        /// Generates a log message or not.
        /// </summary>
        public bool Trace { get; }

        /// <summary>
        /// Justification about the trace.
        /// </summary>
        public string Justification { get; }

        /// <summary>
        /// Initialized a new BindableObjectAttribute.
        /// </summary>
        /// <param name="trace">Raise a warning or not.</param>
        /// <param name="justification">Justify the trace.</param>
        public BindableObjectAttribute(bool trace = true, string justification = "")
        {
            Trace = trace;
            Justification = justification;
        }
    }
}