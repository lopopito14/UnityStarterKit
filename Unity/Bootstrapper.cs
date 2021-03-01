//-----------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Unity
{
    using System;
    using UnityStarterKit.DependencyInjection;

    /// <summary>
    /// Bootstrapper must be the first behavior to start.
    /// It is used to register all Services that can be used in the application
    /// lifecycle.
    /// </summary>
    public abstract class Bootstrapper : UnityEngine.MonoBehaviour
    {
        /// <summary>
        /// UnityServiceLocator of the application.
        /// </summary>
        private UnityServiceLocator unityServiceLocator;

        /// <summary>
        /// Awake Unity method.
        /// </summary>
        private void Awake()
        {
            try
            {
                unityServiceLocator = UnityServiceLocator.Instance as UnityServiceLocator;
                RegisterServices();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }

        /// <summary>
        /// Start Unity method.
        /// </summary>
        private void Start()
        {
            unityServiceLocator.StartServices();
        }

        /// <summary>
        /// OnDestroy Unity method.
        /// </summary>
        private void OnDestroy()
        {
            try
            {
                unityServiceLocator.StopServices();
                UnregisterServices();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }

        /// <summary>
        /// Encapsulates services registration.
        /// </summary>
        private void RegisterServices()
        {
            UnityServiceContainerBuilder unityServiceContainerBuilder = new UnityServiceContainerBuilder();
            RegisterServices(unityServiceContainerBuilder);
            unityServiceLocator.MountContainer(unityServiceContainerBuilder.BuildContainer());
        }

        /// <summary>
        /// Encapsulates services unregistration.
        /// </summary>
        private void UnregisterServices()
        {
            unityServiceLocator.UnmountContainer();
        }

        /// <summary>
        /// Register all your services inside this method.
        /// </summary>
        protected abstract void RegisterServices(IUnityServiceContainerBuilder builder);
    }
}