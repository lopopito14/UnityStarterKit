//-----------------------------------------------------------------------
// <copyright file="IServiceModule.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Modularity
{
    /// <summary>
    /// IServiceModule interface is the main interface of each injectable service.
    /// </summary>
    public interface IServiceModule : IModule
    {
        /// <summary>
        /// Starts the service.
        /// </summary>
        void StartService();

        /// <summary>
        /// Stops the service.
        /// </summary>
        void StopService();
    }
}
