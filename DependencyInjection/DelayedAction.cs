//-----------------------------------------------------------------------
// <copyright file="DelayedAction.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.DependencyInjection
{
    using System;
    using UnityStarterKit.Modularity;

    /// <summary>
    /// Instance of the <see cref="DelayedAction"/> class.
    /// </summary>
    public abstract class DelayedAction
    {
        /// <summary>
        /// Services the type.
        /// </summary>
        /// <returns></returns>
        public abstract Type ServiceType();

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public abstract Action<object> Action { get; }
    }

    /// <summary>
    /// Instance of the <see cref="DelayedAction{T}"/> class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DelayedAction<T> : DelayedAction where T : IServiceModule
    {
        private readonly Action<T> action;

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public override Action<object> Action => new Action<object>(o => action((T)o));

        /// <summary>
        /// Services the type.
        /// </summary>
        /// <returns></returns>
        public override Type ServiceType()
        {
            return typeof(T);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelayedAction{T}"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public DelayedAction(Action<T> action)
        {
            this.action = action;
        }
    }
}
