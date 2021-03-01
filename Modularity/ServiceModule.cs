//-----------------------------------------------------------------------
// <copyright file="ServiceModule.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Modularity
{
    using System;
    using UnityStarterKit.DependencyInjection;

    /// <summary>
    /// Base class of each injectable service.
    /// </summary>
    public abstract class ServiceModule : Module, IServiceModule
    {
        private IUnityServiceLocator unityServiceLocator;

        /// <summary>
        /// Indicates if the service is running or not.
        /// </summary>
        protected bool IsRunning { get; private set; }

        /// <summary>
        /// ServiceModule constructor.
        /// </summary>
        protected ServiceModule()
        {
            unityServiceLocator = UnityServiceLocator.Instance;
            IsRunning = false;
        }

        /// <summary>
        /// Starts the service.
        /// </summary>
        void IServiceModule.StartService()
        {
            logger.Info($"Service '{GetType()}' has started.");

            StartService();
            IsRunning = true;
        }

        /// <summary>
        /// Stops the service.
        /// </summary>
        void IServiceModule.StopService()
        {
            IsRunning = false;
            StopService();

            logger.Info($"Service '{GetType()}' has ended.");
        }

        /// <summary>
        /// Stack an action that will be executed after a new scene loading.
        /// </summary>
        /// <typeparam name="T">The service that will execute the action.</typeparam>
        /// <param name="action">The action.</param>
        protected void StackSceneLoadedAction<T>(Action<T> action) where T : IServiceModule
        {
            unityServiceLocator.StackSceneLoadedAction<T>(action);
        }

        /// <summary>
        /// Stack an action that will be executed after a new scene unloading.
        /// </summary>
        /// <typeparam name="T">The service that will execute the action.</typeparam>
        /// <param name="action">The action.</param>
        protected void StackSceneUnloadedAction<T>(Action<T> action) where T : IServiceModule
        {
            unityServiceLocator.StackSceneUnloadedAction<T>(action);
        }

        /// <summary>
        /// Starts the service.
        /// </summary>
        protected virtual void StartService()
        {
            // do nothing
        }

        /// <summary>
        /// Stops the service.
        /// </summary>
        protected virtual void StopService()
        {
            // do nothing
        }
    }
}
