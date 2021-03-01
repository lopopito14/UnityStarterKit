//-----------------------------------------------------------------------
// <copyright file="IModule.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Modularity
{
    /// <summary>
    /// IModule interface is the main interface of each injectable service.
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Initializes the module. (~ constructor call after dependency injection).
        /// </summary>
        void Initialize();

        /// <summary>
        /// Injects all IServiceModule used inside this module.
        /// </summary>
        void AddServiceDependencies();

        /// <summary>
        /// Removes all IServiceModule used inside this module.
        /// </summary>
        void RemoveServiceDependencies();
    }
}
