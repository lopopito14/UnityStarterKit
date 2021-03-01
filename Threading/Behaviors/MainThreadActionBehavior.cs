//-----------------------------------------------------------------------
// <copyright file="MainThreadActionBehavior.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.Threading.Behaviors
{
    using System;
    using System.Collections.Concurrent;
    using UnityStarterKit.Attributes;

    public class MainThreadActionBehavior : BaseComponent.BaseComponent
    {
        /// <summary>
        /// Queue used to do action on UI thread.
        /// </summary>
        [ComponentObject]
        private ConcurrentQueue<Action> actionsQueue;

        /// <summary>
        /// Initializes the current behavior.
        /// </summary>
        protected override void InitializeComponent()
        {
            base.InitializeComponent();

            actionsQueue = new ConcurrentQueue<Action>();
        }

        /// <summary>
        /// Dequeue and execute actions on UI thread.
        /// </summary>
        private void Update()
        {
            lock (actionsQueue)
            {
                if (actionsQueue.Count > 0)
                {
                    //logger.Debug($"Queue {GetInstanceID()} has {GetQueueCount} elements");

                    if (actionsQueue.TryDequeue(out Action action))
                    {
                        try
                        {
                            action.Invoke();
                        }
                        catch (Exception e)
                        {
                            logger.Exception(e);
                        }
                    }
                }
            }
        }

        internal int GetQueueCount => actionsQueue.Count;

        /// <summary>
        /// Enqueue task that will be executed in UI thread.
        /// </summary>
        /// <param name="task">The task to enqueue.</param>
        internal void EnqueueAction(Action action)
        {
            lock (actionsQueue)
            {
                actionsQueue.Enqueue(action);
            }
        }
    }
}
