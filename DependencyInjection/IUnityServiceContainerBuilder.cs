//-----------------------------------------------------------------------
// <copyright file="IUnityServiceContainerBuilder.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.DependencyInjection
{
    using System;
    using UnityStarterKit.Modularity;

    /// <summary>
    /// IUnityServiceContainerBuilder allows to build a Unity Service container
    /// for the whole application.
    /// </summary>
    public interface IUnityServiceContainerBuilder
    {
        /// <summary>
        /// Gets a specific service from the current builder.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <returns>The service from the builder.</returns>
        T GetService<T>() where T : IServiceModule;

        /// <summary>
        /// Registers a service into the builder.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <param name="resolver">The resolver of the registered service.</param>
        void AddService<T>(Func<IServiceModule> resolver) where T : IServiceModule;

        /// <summary>
        /// Registers a service into the builder.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <typeparam name="U">The implementation type of the service.</typeparam>
        void AddService<T, U>() where T : IServiceModule where U : IServiceModule, new();
    }
}
