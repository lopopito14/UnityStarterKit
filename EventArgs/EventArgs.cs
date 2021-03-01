//-----------------------------------------------------------------------
// <copyright file="EventArgs.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.EventArgs
{
    public delegate void EventHandler();

    public delegate void EventHandler<TEventArgs>(EventArgs<TEventArgs> e);

    /// <summary>
    /// Instance of the <see cref="EventArgs{T}"/> class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventArgs<T>
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventArgs{T}"/> class.
        /// </summary>
        /// <param name="val">The value.</param>
        public EventArgs(T val)
        {
            Value = val;
        }
    }
}
