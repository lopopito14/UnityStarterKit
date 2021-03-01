//-----------------------------------------------------------------------
// <copyright file="MainThreadTaskBehavior.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Threading.Behaviors
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using UnityStarterKit.Attributes;

    public class MainThreadTaskBehavior : BaseComponent.BaseComponent
    {
        /// <summary>
        /// Queue used to do action on UI thread.
        /// </summary>
        [ComponentObject]
        private ConcurrentQueue<Func<Task>> asyncTasksQueue;

        /// <summary>
        /// Initializes the current behavior.
        /// </summary>
        protected override void InitializeComponent()
        {
            base.InitializeComponent();

            asyncTasksQueue = new ConcurrentQueue<Func<Task>>();
        }

        /// <summary>
        /// Dequeue and execute task on UI thread.
        /// </summary>
        private async void Update()
        {
            if (asyncTasksQueue.Count > 0)
            {
                //logger.Debug($"Queue {GetInstanceID()} has {GetQueueCount} elements");

                if (asyncTasksQueue.TryDequeue(out Func<Task> task))
                {
                    try
                    {
                        await task.Invoke();
                    }
                    catch (Exception e)
                    {
                        logger.Exception(e);
                    }
                }
            }
        }

        internal int GetQueueCount => asyncTasksQueue.Count;

        /// <summary>
        /// Enqueue task that will be executed in UI thread.
        /// </summary>
        /// <param name="task">The task to enqueue.</param>
        internal void EnqueueTask(Func<Task> task)
        {
            asyncTasksQueue.Enqueue(task);
        }
    }
}
