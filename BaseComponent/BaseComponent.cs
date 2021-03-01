//-----------------------------------------------------------------------
// <copyright file="BaseComponent.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.BaseComponent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;
    using UnityStarterKit.Attributes;
    using UnityStarterKit.DependencyInjection;
    using UnityStarterKit.ExtensionMethods;
    using UnityStarterKit.Logger.Services;
    using UnityStarterKit.Modularity;

    /// <summary>
    /// BaseComponent is used mainly to check public and private fields before computation.
    /// It allows to manage easily event subscription and unsubscription by using overridable methods.
    /// Moreover, it encapsulated native Unity methods (Awake, Start and OnDestroy).
    /// </summary>
    public abstract class BaseComponent : MonoBehaviour
    {
        private IUnityServiceLocator unityServiceLocator;

#pragma warning disable 0649
        /// <summary>
        /// The logger.
        /// </summary>
        [InjectableService]
        protected ILogService logger;
#pragma warning restore 0649

        protected bool started;

        /// <summary>
        /// First method called in Unity component render process.
        /// </summary>
        private void Awake()
        {
            unityServiceLocator = UnityServiceLocator.Instance;

            try
            {
                InjectServices();
                CheckBindableObjects();
                InitializeComponent();
            }
            catch (Exception e)
            {
                logger.Exception(e);
            }
        }

        /// <summary>
        /// Start() method of Unity render process.
        /// </summary>
        private void Start()
        {
            try
            {
                CheckComponentObjects();
                UseComponent();
            }
            catch (Exception e)
            {
                logger.Exception(e);
            }

            started = true;
        }

        /// <summary>
        /// OnDestroy() method of Unity render process.
        /// </summary>
        private void OnDestroy()
        {
            try
            {
                if (started)
                {
                    UnuseComponent();
                }

                DestroyComponent();
            }
            catch (Exception e)
            {
                logger.Exception(e);
            }

            started = false;
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
        /// Initializes services using dependency injection.
        /// </summary>
        private void InjectServices()
        {
            IEnumerable<FieldInfo> nonPublicFields = GetType().GetAllFields(BindingFlags.Instance | BindingFlags.NonPublic).Reverse();

            foreach (FieldInfo nonPublicField in nonPublicFields)
            {
                if (nonPublicField.GetCustomAttribute(typeof(InjectableServiceAttribute)) is InjectableServiceAttribute injectableServiceAttribute)
                {
                    IServiceModule service = unityServiceLocator.GetInstance(nonPublicField.FieldType);
                    if (service != null)
                    {
                        nonPublicField.SetValue(this, service);
                    }
                    else
                    {
                        logger.Warning($"The service '{nonPublicField.Name}' of '{GetType().Name}' does not exist ({gameObject.name})");
                    }
                }
            }
        }

        /// <summary>
        /// Checks if all public fields decorated with the attribute 'BindableObject' are not null.
        /// </summary>
        private void CheckBindableObjects()
        {
            IEnumerable<FieldInfo> publicFields = GetType().GetAllFields(BindingFlags.Instance | BindingFlags.Public);

            foreach (FieldInfo publicField in publicFields)
            {
                if (publicField.GetCustomAttribute(typeof(BindableObjectAttribute)) is BindableObjectAttribute bindableObject)
                {
                    if (publicField.GetValue(this) == null || publicField.GetValue(this).Equals(null))
                    {
                        if (bindableObject.Trace)
                        {
                            logger.Warning($"The property '{publicField.Name}' of '{GetType().Name}' must be initialized ({gameObject.name})");
                        }
                        else
                        {
                            logger.Debug($"The field '{publicField.Name}' of '{GetType().Name}' is null ({gameObject.name}) but '{bindableObject.Justification}'");
                        }
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
                if (nonPublicField.GetCustomAttribute(typeof(ComponentObjectAttribute)) is ComponentObjectAttribute componentObject)
                {
                    if (nonPublicField.GetValue(this) == null || nonPublicField.GetValue(this).Equals(null))
                    {
                        if (componentObject.Trace)
                        {
                            logger.Warning($"The field '{nonPublicField.Name}' of '{GetType().Name}' must be initialized ({gameObject.name})");
                        }
                        else
                        {
                            logger.Debug($"The field '{nonPublicField.Name}' of '{GetType().Name}' is null ({gameObject.name}) but '{componentObject.Justification}'");
                        }
                    }
                }
            }
        }
    }
}
