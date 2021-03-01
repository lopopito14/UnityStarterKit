//-----------------------------------------------------------------------
// <copyright file="IUnityServiceContainer.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using UnityStarterKit.Modularity;

    /// <summary>
    /// IUnityServiceContainer is a container with a set of services
    /// that are allowed to be shared between component and also are injectable.
    /// </summary>
    public interface IUnityServiceContainer
    {
        /// <summary>
        /// Gets a specific service from the current container.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <returns>The service from the container.</returns>
        T GetInstance<T>() where T : IServiceModule;

        /// <summary>
        /// Gets a specific service from the current container.
        /// </summary>
        /// <param name="type">The type of the service.</param>
        /// <returns>The service from the container.</returns>
        IServiceModule GetInstance(Type type);

        /// <summary>
        /// Gets all services from the current container.
        /// </summary>
        /// <returns>All registered servies.</returns>
        IEnumerable<IServiceModule> GetAllInstances();
    }
}