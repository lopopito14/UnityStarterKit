//-----------------------------------------------------------------------
// <copyright file="MainThreadService.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Threading.Services
{
    using System.Threading.Tasks;
    using UnityStarterKit.EventArgs;
    using UnityStarterKit.Modularity;

    public class MainThreadService : ServiceModule, IMainThreadService
    {
        private event EventHandler<System.Action> newActionExecuted;
        event EventHandler<System.Action> IMainThreadService.NewActionExecuted
        {
            add { newActionExecuted += value; }
            remove { newActionExecuted -= value; }
        }

        private event EventHandler<System.Func<Task>> newTaskExecuted;
        event EventHandler<System.Func<Task>> IMainThreadService.NewTaskExecuted
        {
            add { newTaskExecuted += value; }
            remove { newTaskExecuted -= value; }
        }

        void IMainThreadService.DispatchActionOnMainThread(System.Action action)
        {
            newActionExecuted?.Invoke(new EventArgs<System.Action>(action));
        }

        void IMainThreadService.DispatchTaskOnMainThread(System.Func<Task> task)
        {
            newTaskExecuted?.Invoke(new EventArgs<System.Func<Task>>(task));
        }
    }
}
