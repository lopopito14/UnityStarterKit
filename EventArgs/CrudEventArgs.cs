//-----------------------------------------------------------------------
// <copyright file="CrudEventArgs.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.EventArgs
{
    using System.Collections.Generic;

    public delegate void CrudEventHandler<TEventArgs>(CrudEventArgs<TEventArgs> e);

    public enum CrudType
    {
        Add,
        Remove,
        Update,
        AddRange,
        RemoveRange,
        UpdateRange
    }

    /// <summary>
    /// Instance of the <see cref="CrudEventArgs"/> class.
    /// </summary>
    public abstract class CrudEventArgs
    {
        /// <summary>
        /// Gets the type of the crud.
        /// </summary>
        /// <value>
        /// The type of the crud.
        /// </value>
        public CrudType CrudType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudEventArgs"/> class.
        /// </summary>
        /// <param name="crudType">Type of the crud.</param>
        protected CrudEventArgs(CrudType crudType)
        {
            CrudType = crudType;
        }

        /// <summary>
        /// Adds the specified new item.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="newItem">The new item.</param>
        /// <returns></returns>
        public static CrudEventArgs<U> Add<U>(U newItem)
        {
            return new CrudEventArgs<U>(CrudType.Add, default, newItem, default);
        }

        /// <summary>
        /// Removes the specified old item.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="oldItem">The old item.</param>
        /// <returns></returns>
        public static CrudEventArgs<U> Remove<U>(U oldItem)
        {
            return new CrudEventArgs<U>(CrudType.Remove, oldItem, default, default);
        }

        /// <summary>
        /// Updates the specified updated item.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="updatedItem">The updated item.</param>
        /// <returns></returns>
        public static CrudEventArgs<U> Update<U>(U updatedItem)
        {
            return new CrudEventArgs<U>(CrudType.Update, default, default, updatedItem);
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="newItems">The new items.</param>
        /// <returns></returns>
        public static CrudEventArgs<U> AddRange<U>(IEnumerable<U> newItems)
        {
            return new CrudEventArgs<U>(CrudType.AddRange, default, newItems, default);
        }

        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="oldItems">The old items.</param>
        /// <returns></returns>
        public static CrudEventArgs<U> RemoveRange<U>(IEnumerable<U> oldItems)
        {
            return new CrudEventArgs<U>(CrudType.RemoveRange, oldItems, default, default);
        }

        /// <summary>
        /// Updates the range.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="updatedItems">The updated items.</param>
        /// <returns></returns>
        public static CrudEventArgs<U> UpdateRange<U>(IEnumerable<U> updatedItems)
        {
            return new CrudEventArgs<U>(CrudType.UpdateRange, default, default, updatedItems);
        }
    }

    /// <summary>
    /// Instance of the <see cref="CrudEventArgs{T}"/> class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CrudEventArgs<T> : CrudEventArgs
    {
        /// <summary>
        /// Gets the old item.
        /// </summary>
        /// <value>
        /// The old item.
        /// </value>
        public T OldItem { get; }

        /// <summary>
        /// Creates new item.
        /// </summary>
        /// <value>
        /// The new item.
        /// </value>
        public T NewItem { get; }

        /// <summary>
        /// Gets the updated item.
        /// </summary>
        /// <value>
        /// The updated item.
        /// </value>
        public T UpdatedItem { get; }

        /// <summary>
        /// Gets the old items.
        /// </summary>
        /// <value>
        /// The old items.
        /// </value>
        public IEnumerable<T> OldItems { get; }

        /// <summary>
        /// Creates new items.
        /// </summary>
        /// <value>
        /// The new items.
        /// </value>
        public IEnumerable<T> NewItems { get; }

        /// <summary>
        /// Gets the updated items.
        /// </summary>
        /// <value>
        /// The updated items.
        /// </value>
        public IEnumerable<T> UpdatedItems { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudEventArgs{T}"/> class.
        /// </summary>
        /// <param name="crudType">Type of the crud.</param>
        /// <param name="oldItem">The old item.</param>
        /// <param name="newItem">The new item.</param>
        /// <param name="updatedItem">The updated item.</param>
        internal CrudEventArgs(CrudType crudType, T oldItem, T newItem, T updatedItem) : this(crudType)
        {
            OldItem = oldItem;
            NewItem = newItem;
            UpdatedItem = updatedItem;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudEventArgs{T}"/> class.
        /// </summary>
        /// <param name="crudType">Type of the crud.</param>
        /// <param name="oldItems">The old items.</param>
        /// <param name="newItems">The new items.</param>
        /// <param name="updatedItems">The updated items.</param>
        internal CrudEventArgs(CrudType crudType, IEnumerable<T> oldItems, IEnumerable<T> newItems, IEnumerable<T> updatedItems) : this(crudType)
        {
            OldItems = oldItems;
            NewItems = newItems;
            UpdatedItems = updatedItems;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudEventArgs{T}"/> class.
        /// </summary>
        /// <param name="crudType">Type of the crud.</param>
        private CrudEventArgs(CrudType crudType) : base(crudType)
        {
            OldItem = default;
            NewItem = default;
            UpdatedItem = default;
            OldItems = default;
            NewItems = default;
            UpdatedItems = default;
        }
    }
}
