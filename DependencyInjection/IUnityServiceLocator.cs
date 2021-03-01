//-----------------------------------------------------------------------
// <copyright file="IUnityServiceLocator.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.DependencyInjection
{
    using System;
    using UnityStarterKit.Modularity;

    /// <summary>
    /// Entry point to access to all registered services in a specific container.
    /// </summary>
    public interface IUnityServiceLocator : IDisposable
    {
        /// <summary>
        /// Stack an action that will be executed after a new scene loading.
        /// </summary>
        /// <typeparam name="T">The service that will execute the action.</typeparam>
        /// <param name="action">The action.</param>
        void StackSceneLoadedAction<T>(Action<T> action) where T : IServiceModule;

        /// <summary>
        /// Stack an action that will be executed after a new scene unloading.
        /// </summary>
        /// <typeparam name="T">The service that will execute the action.</typeparam>
        /// <param name="action">The action.</param>
        void StackSceneUnloadedAction<T>(Action<T> action) where T : IServiceModule;

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
    }
}
