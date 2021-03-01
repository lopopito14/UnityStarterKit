//-----------------------------------------------------------------------
// <copyright file="IMainThreadService.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Threading.Services
{
    using System.Threading.Tasks;
    using UnityStarterKit.EventArgs;
    using UnityStarterKit.Modularity;

    public interface IMainThreadService : IServiceModule
    {
        event EventHandler<System.Action> NewActionExecuted;
        event EventHandler<System.Func<Task>> NewTaskExecuted;

        void DispatchActionOnMainThread(System.Action action);
        void DispatchTaskOnMainThread(System.Func<Task> task);
    }
}
