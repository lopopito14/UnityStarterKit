//-----------------------------------------------------------------------
// <copyright file="UnityServiceLocator.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using UnityEngine.SceneManagement;
    using UnityStarterKit.Modularity;

    /// <summary>
    /// Entry point to access to all registered services in a specific container.
    /// </summary>
    internal class UnityServiceLocator : IUnityServiceLocator
    {
        /// <summary>
        /// Lock to prevent threads conflicts.
        /// </summary>
        private static readonly object obj = new object();

        public static IUnityServiceLocator _instance;
        public static IUnityServiceLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (obj)
                    {
                        if (_instance == null)
                        {
                            _instance = new UnityServiceLocator();
                        }
                    }
                }

                return _instance;
            }
        }

        private Stack<DelayedAction> sceneLoadedActions;
        private Stack<DelayedAction> sceneUnloadedActions;

        /// <summary>
        /// Prevents a default instance of the <see cref="UnityServiceLocator"/> class from being created.
        /// </summary>
        private UnityServiceLocator()
        {
            sceneLoadedActions = new Stack<DelayedAction>();
            sceneUnloadedActions = new Stack<DelayedAction>();

            SceneManager.sceneUnloaded += OnSceneUnloaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        /// <summary>
        /// Called when [scene loaded].
        /// </summary>
        /// <param name="scene">The scene.</param>
        /// <param name="loadSceneMode">The load scene mode.</param>
        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            while (sceneLoadedActions.Count != 0)
            {
                DelayedAction delayedAction = sceneLoadedActions.Pop();
                delayedAction.Action.Invoke(GetInstance(delayedAction.ServiceType()));
            }
        }

        /// <summary>
        /// Called when [scene unloaded].
        /// </summary>
        /// <param name="scene">The scene.</param>
        private void OnSceneUnloaded(Scene scene)
        {
            while (sceneUnloadedActions.Count != 0)
            {
                DelayedAction delayedAction = sceneUnloadedActions.Pop();
                delayedAction.Action.Invoke(GetInstance(delayedAction.ServiceType()));
            }
        }

        private IUnityServiceContainer unityServiceContainer;

        /// <summary>
        /// Mounts a container inside the service locator.
        /// </summary>
        /// <param name="unityServiceContainer">The new container.</param>
        internal void MountContainer(IUnityServiceContainer unityServiceContainer)
        {
            UnmountContainer();

            this.unityServiceContainer = unityServiceContainer;

            foreach (IServiceModule serviceModule in unityServiceContainer.GetAllInstances())
            {
                serviceModule.AddServiceDependencies();
            }

            foreach (IServiceModule serviceModule in unityServiceContainer.GetAllInstances())
            {
                serviceModule.Initialize();
            }
        }

        /// <summary>
        /// Unmount the current container.
        /// </summary>
        internal void UnmountContainer()
        {
            if (unityServiceContainer != null)
            {
                foreach (IServiceModule serviceModule in unityServiceContainer.GetAllInstances())
                {
                    serviceModule.RemoveServiceDependencies();
                }

                unityServiceContainer = null;
            }
        }

        /// <summary>
        /// Starts all registered services.
        /// </summary>
        internal void StartServices()
        {
            if (unityServiceContainer != null)
            {
                foreach (IServiceModule service in unityServiceContainer.GetAllInstances())
                {
                    service.StartService();
                }
            }
        }

        /// <summary>
        /// Stops all registered services.
        /// </summary>
        internal void StopServices()
        {
            if (unityServiceContainer != null)
            {
                foreach (IServiceModule service in unityServiceContainer.GetAllInstances())
                {
                    service.StopService();
                }
            }
        }

        /// <summary>
        /// Gets a specific service from the current container.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <returns>The service from the container.</returns>
        T IUnityServiceLocator.GetInstance<T>()
        {
            return GetInstance<T>();
        }

        /// <summary>
        /// Gets a specific service from the current container.
        /// </summary>
        /// <param name="type">The type of the service.</param>
        /// <returns>The service from the container.</returns>
        IServiceModule IUnityServiceLocator.GetInstance(Type type)
        {
            return GetInstance(type);
        }

        /// <summary>
        /// Stack an action that will be executed after a new scene loading.
        /// </summary>
        /// <typeparam name="T">The service that will execute the action.</typeparam>
        /// <param name="action">The action.</param>
        void IUnityServiceLocator.StackSceneLoadedAction<T>(Action<T> action)
        {
            sceneLoadedActions.Push(new DelayedAction<T>(action));
        }

        /// <summary>
        /// Stack an action that will be executed after a new scene unloading.
        /// </summary>
        /// <typeparam name="T">The service that will execute the action.</typeparam>
        /// <param name="action">The action.</param>
        void IUnityServiceLocator.StackSceneUnloadedAction<T>(Action<T> action)
        {
            sceneUnloadedActions.Push(new DelayedAction<T>(action));
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        /// <summary>
        /// Gets a specific service from the current container.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <returns>The service from the container.</returns>
        private T GetInstance<T>() where T : IServiceModule
        {
            if (unityServiceContainer == null)
            {
                throw new InvalidOperationException("You must first create a unity service container.");
            }

            return unityServiceContainer.GetInstance<T>();
        }

        /// <summary>
        /// Gets a specific service from the current container.
        /// </summary>
        /// <param name="type">The type of the service.</param>
        /// <returns>The service from the container.</returns>
        private IServiceModule GetInstance(Type type)
        {
            if (unityServiceContainer == null)
            {
                throw new InvalidOperationException("You must first create a unity service container.");
            }

            return unityServiceContainer.GetInstance(type);
        }
    }
}
