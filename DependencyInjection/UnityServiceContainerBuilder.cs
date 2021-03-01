//-----------------------------------------------------------------------
// <copyright file="UnityServiceContainerBuilder.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityStarterKit.Modularity;

    /// <summary>
    /// UnityServiceContainerBuilder allows to build a Unity Service container
    /// for the whole application.
    /// </summary>
    public class UnityServiceContainerBuilder : IUnityServiceContainerBuilder
    {
        /// <summary>
        /// All services resolvers.
        /// </summary>
        private readonly Dictionary<Type, Func<IServiceModule>> serviceResolvers;

        /// <summary>
        /// Cache of service already resolved but not added to the resulting container.
        /// </summary>
        private readonly Dictionary<Type, IServiceModule> dictionnary;

        /// <summary>
        /// UnityServiceContainerBuilder contructor.
        /// </summary>
        public UnityServiceContainerBuilder()
        {
            serviceResolvers = new Dictionary<Type, Func<IServiceModule>>();
            dictionnary = new Dictionary<Type, IServiceModule>();
        }

        /// <summary>
        /// Gets a specific service from the current builder.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <returns>The service from the builder.</returns>
        public T GetService<T>() where T : IServiceModule
        {
            if (dictionnary.TryGetValue(typeof(T), out IServiceModule serviceCache))
            {
                return (T)serviceCache;
            }
            else
            {
                if (serviceResolvers.TryGetValue(typeof(T), out Func<IServiceModule> serviceResolver))
                {
                    IServiceModule service = serviceResolver.Invoke();
                    dictionnary.Add(typeof(T), service);
                    return (T)service;
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
        }

        /// <summary>
        /// Registers a service into the builder.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <param name="resolver">The resolver of the registered service.</param>
        public void AddService<T>(Func<IServiceModule> resolver) where T : IServiceModule
        {
            serviceResolvers.Add(typeof(T), resolver);
        }

        /// <summary>
        /// Registers a service into the builder.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <typeparam name="U">The implementation type of the service.</typeparam>
        public void AddService<T, U>() where T : IServiceModule where U : IServiceModule, new()
        {
            serviceResolvers.Add(typeof(T), () => new U());
        }

        /// <summary>
        /// Build the resulting container and returns it.
        /// </summary>
        /// <returns>The resulting container.</returns>
        internal IUnityServiceContainer BuildContainer()
        {
            UnityServiceContainer unityServiceContainer = new UnityServiceContainer();

            foreach (KeyValuePair<Type, Func<IServiceModule>> serviceResolver in serviceResolvers)
            {
                IServiceModule service;
                if (dictionnary.TryGetValue(serviceResolver.Key, out IServiceModule serviceCache))
                {
                    service = serviceCache;
                }
                else
                {
                    service = serviceResolver.Value.Invoke();
                    dictionnary.Add(serviceResolver.Key, service);
                }

                unityServiceContainer.SetInstance(serviceResolver.Key, service);
            }

            return unityServiceContainer;
        }

        private class UnityServiceContainer : IUnityServiceContainer
        {
            private readonly Dictionary<Type, IServiceModule> dictionnary;

            internal UnityServiceContainer()
            {
                dictionnary = new Dictionary<Type, IServiceModule>();
            }

            internal void SetInstance(Type type, IServiceModule instance)
            {
                if (!dictionnary.ContainsKey(type))
                {
                    dictionnary.Add(type, instance);
                }
            }

            public T GetInstance<T>() where T : IServiceModule
            {
                if (dictionnary.TryGetValue(typeof(T), out IServiceModule output))
                {
                    return (T)output;
                }

                return default;
            }

            public IServiceModule GetInstance(Type type)
            {
                if (dictionnary.TryGetValue(type, out IServiceModule output))
                {
                    return output;
                }

                return default;
            }

            public IEnumerable<IServiceModule> GetAllInstances()
            {
                return dictionnary.Values.Distinct();
            }
        }
    }
}
