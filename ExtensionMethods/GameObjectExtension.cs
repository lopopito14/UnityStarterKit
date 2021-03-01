//-----------------------------------------------------------------------
// <copyright file="GameObjectExtension.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.ExtensionMethods
{
    using System;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// This class defines an extension method for GameObject type.
    /// </summary>
    public static class GameObjectExtension
    {
        /// <summary>
        /// Returns the first object of type <T> found exclusively in the children of the gameObject (exclude itself).
        /// </summary>
        /// <typeparam name="T">The type </typeparam>
        /// <param name="gameObject">The game object from where the search begins.</param>
        /// <returns>
        /// Returns the first object of type <T> found exclusively in the children of the gameObject.
        /// If there is no occurence, then return null.
        /// </returns>
        public static T GetComponentExclusivelyInChildren<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponentsInChildren<T>().Where(go => go.gameObject != gameObject).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first object of a particular type found in parents.
        /// </summary>
        /// <param name="gameObject">The game object from where the search begins.</param>
        /// <param name="type">The seeking type.</param>
        /// <returns>
        /// The first object of a particular type found in parents.
        /// If there is no occurence, then return null.
        /// </returns>
        public static Component GetFirstParentOfType(this GameObject gameObject, Type type)
        {
            Transform parent = gameObject.transform.parent;
            if (parent == null)
            {
                return null;
            }

            Component component = parent.GetComponent(type);
            if (component != null)
            {
                return component;
            }

            return GetFirstParentOfType(parent.gameObject, type);
        }
    }
}