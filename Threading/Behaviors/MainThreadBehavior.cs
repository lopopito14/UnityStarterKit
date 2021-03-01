//-----------------------------------------------------------------------
// <copyright file="MainThreadBehavior.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Threading.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityStarterKit.Attributes;
    using UnityStarterKit.EventArgs;
    using UnityStarterKit.Threading.Services;

    public class MainThreadBehavior : BaseComponent.BaseComponent
    {
#pragma warning disable 0649
        [InjectableService]
        private IMainThreadService mainThreadService;
#pragma warning restore 0649

        [ComponentObject]
        private IList<MainThreadActionBehavior> actionQueues;
        [ComponentObject]
        private IList<MainThreadTaskBehavior> taskQueues;

        private const int QueuesCount = 30;

        /// <summary>
        /// Initializes the current behavior.
        /// </summary>
        protected override void InitializeComponent()
        {
            base.InitializeComponent();

            actionQueues = new List<MainThreadActionBehavior>();
            taskQueues = new List<MainThreadTaskBehavior>();

            for (int i = 0; i < QueuesCount; i++)
            {
                actionQueues.Add(gameObject.AddComponent<MainThreadActionBehavior>());
                taskQueues.Add(gameObject.AddComponent<MainThreadTaskBehavior>());
            }
        }

        protected override void UseComponent()
        {
            base.UseComponent();

            mainThreadService.NewActionExecuted += NewActionExecuted;
            mainThreadService.NewTaskExecuted += NewTaskExecuted;
        }

        protected override void UnuseComponent()
        {
            base.UnuseComponent();

            mainThreadService.NewActionExecuted -= NewActionExecuted;
            mainThreadService.NewTaskExecuted -= NewTaskExecuted;
        }

        protected override void DestroyComponent()
        {
            base.DestroyComponent();

            while (actionQueues.Count > 0)
            {
                Destroy(actionQueues[0]);
                actionQueues.RemoveAt(0);
            }

            while (taskQueues.Count > 0)
            {
                Destroy(taskQueues[0]);
                taskQueues.RemoveAt(0);
            }
        }

        private void NewActionExecuted(EventArgs<Action> e)
        {
            GetBestActionQueue().EnqueueAction(e.Value);
        }

        private void NewTaskExecuted(EventArgs<Func<Task>> e)
        {
            GetBestTaskQueue().EnqueueTask(e.Value);
        }

        private MainThreadActionBehavior GetBestActionQueue()
        {
            MainThreadActionBehavior queue = actionQueues[0];

            if (queue.GetQueueCount == 0)
                return queue;

            for (int i = 1; i < QueuesCount; i++)
            {
                if (actionQueues[i].GetQueueCount < queue.GetQueueCount)
                {
                    queue = actionQueues[i];

                    if (queue.GetQueueCount == 0)
                        break;
                }
            }

            return queue;
        }

        private MainThreadTaskBehavior GetBestTaskQueue()
        {
            MainThreadTaskBehavior queue = taskQueues[0];

            if (queue.GetQueueCount == 0)
                return queue;

            for (int i = 1; i < QueuesCount; i++)
            {
                if (taskQueues[i].GetQueueCount < queue.GetQueueCount)
                {
                    queue = taskQueues[i];

                    if (queue.GetQueueCount == 0)
                        break;
                }
            }

            return queue;
        }
    }
}
