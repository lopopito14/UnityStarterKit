//-----------------------------------------------------------------------
// <copyright file="Module.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Modularity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityStarterKit.Attributes;
    using UnityStarterKit.DependencyInjection;
    using UnityStarterKit.ExtensionMethods;
    using UnityStarterKit.Logger.Services;

    /// <summary>
    /// Base class of each module.
    /// </summary>
    public abstract class Module : IModule
    {
        private IUnityServiceLocator unityServiceLocator;

#pragma warning disable 0649
        /// <summary>
        /// The logger.
        /// </summary>
        [InjectableService]
        protected ILogService logger;
#pragma warning restore 0649

        /// <summary>
        /// Module constructor.
        /// </summary>
        protected Module()
        {
            unityServiceLocator = UnityServiceLocator.Instance;
        }

        /// <summary>
        /// Initializes the module. (~ constructor call after dependency injection).
        /// </summary>
        void IModule.Initialize()
        {
            Initialize();
        }

        /// <summary>
        /// Injects all IServiceModule used inside this module.
        /// </summary>
        void IModule.AddServiceDependencies()
        {
            InjectServices();
            AddServiceDependencies();
        }

        /// <summary>
        /// Removes all IServiceModule used inside this module.
        /// </summary>
        void IModule.RemoveServiceDependencies()
        {
            RemoveServices();
            RemoveServiceDependencies();
        }

        /// <summary>
        /// Initializes the module. (~ constructor call after dependency injection).
        /// </summary>
        protected virtual void Initialize()
        {
            // do nothing
        }

        /// <summary>
        /// Initializes services using dependency injection.
        /// </summary>
        private void InjectServices()
        {
            IEnumerable<FieldInfo> nonPublicFields = GetType().GetAllFields(BindingFlags.Instance | BindingFlags.NonPublic).Reverse();

            foreach (FieldInfo nonPublicField in nonPublicFields)
            {
                InjectableServiceAttribute injectableServiceAttribute = nonPublicField.GetCustomAttribute(typeof(InjectableServiceAttribute)) as InjectableServiceAttribute;
                if (injectableServiceAttribute != null)
                {
                    IServiceModule service = unityServiceLocator.GetInstance(nonPublicField.FieldType);
                    if (service != null)
                    {
                        nonPublicField.SetValue(this, service);
                    }
                    else
                    {
                        logger.Error($"The service '{nonPublicField.Name}' of '{GetType().Name}' does not exist.");
                    }
                }
            }
        }

        /// <summary>
        /// Removes services injected dependency injection.
        /// </summary>
        private void RemoveServices()
        {
            IEnumerable<FieldInfo> nonPublicFields = GetType().GetAllFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (FieldInfo nonPublicField in nonPublicFields)
            {
                InjectableServiceAttribute injectableServiceAttribute = nonPublicField.GetCustomAttribute(typeof(InjectableServiceAttribute)) as InjectableServiceAttribute;
                if (injectableServiceAttribute != null)
                {
                    //nonPublicField.SetValue(this, null);
                }
            }
        }

        /// <summary>
        /// Injects all IServiceModule used inside this module.
        /// </summary>
        protected virtual void AddServiceDependencies()
        {
            // do nothing
        }

        /// <summary>
        /// Removes all IServiceModule used inside this module.
        /// </summary>
        protected virtual void RemoveServiceDependencies()
        {
            // do nothing
        }
    }
}
