//-----------------------------------------------------------------------
// <copyright file="BaseObject.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.BaseComponent
{
    using UnityStarterKit.Attributes;
    using UnityStarterKit.ExtensionMethods;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEngine;

    /// <summary>
    /// BaseObject is used mainly to check public and private fields before computation.
    /// It allows to manage easily event subscription and unsubscription by using overridable methods.
    /// Moreover, it encapsulated native Unity methods (Awake, Start and OnDestroy).
    /// </summary>
    public abstract class BaseObject : ScriptableObject
    {
        /// <summary>
        /// First method called in Unity component render process.
        /// </summary>
        private void Awake()
        {
            try
            {
                CheckBindableObjects();
                InitializeComponent();
                CheckComponentObjects();
                UseComponent();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }

        /// <summary>
        /// OnDestroy() method of Unity render process.
        /// </summary>
        private void OnDestroy()
        {
            try
            {
                UnuseComponent();
                DestroyComponent();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }

        /// <summary>
        /// Allows to define private fields (those decorated with the attribute ComponentObject).
        /// </summary>
        protected virtual void InitializeComponent() { }

        /// <summary>
        /// Allows to use private fields (those decorated with the attribute ComponentObject).
        /// Managed here event subscriptions.
        /// </summary>
        protected virtual void UseComponent() { }

        /// <summary>
        /// Manages here event unsubscriptions.
        /// </summary>
        protected virtual void UnuseComponent() { }

        /// <summary>
        /// Destroy instanciated (runtime) GameObject.
        /// </summary>
        protected virtual void DestroyComponent() { }

        /// <summary>
        /// Checks if all public fields decorated with the attribute 'BindableObject' are not null.
        /// </summary>
        private void CheckBindableObjects()
        {
            IEnumerable<FieldInfo> publicFields = GetType().GetAllFields(BindingFlags.Instance | BindingFlags.Public);

            foreach (FieldInfo publicField in publicFields)
            {
                BindableObjectAttribute bindableObject = publicField.GetCustomAttribute(typeof(BindableObjectAttribute)) as BindableObjectAttribute;
                if (bindableObject != null)
                {
                    if (publicField.GetValue(this) == null)
                    {
                        UnityEngine.Debug.LogError($"The property '{publicField.Name}' of '{GetType().Name}' must be initialized ({name})");
                    }
                }
            }
        }

        /// <summary>
        /// Checks if all private fields decorated with the attribute 'ComponentObject' are not null.
        /// </summary>
        private void CheckComponentObjects()
        {
            IEnumerable<FieldInfo> nonPublicFields = GetType().GetAllFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (FieldInfo nonPublicField in nonPublicFields)
            {
                ComponentObjectAttribute componentObject = nonPublicField.GetCustomAttribute(typeof(ComponentObjectAttribute)) as ComponentObjectAttribute;
                if (componentObject != null)
                {
                    if (nonPublicField.GetValue(this) == null)
                    {
                        UnityEngine.Debug.LogError($"The field '{nonPublicField.Name}' of '{GetType().Name}' must be initialized ({name})");
                    }
                }
            }
        }
    }
}