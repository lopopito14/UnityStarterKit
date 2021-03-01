//-----------------------------------------------------------------------
// <copyright file="TaskExtensions.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.ExtensionMethods
{
    using System.Collections;
    using System.Threading.Tasks;

    public static class TaskExtensions
    {
        public static IEnumerator AsIEnumerator(this Task task)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (task.IsFaulted)
            {
                throw task.Exception;
            }
        }

        public static IEnumerator AsIEnumerator<T>(this Task<T> task)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }

            if (task.IsFaulted)
            {
                throw task.Exception;
            }
        }

        public static async Task AsTask(this IEnumerator enumerator)
        {
            await Task.Run(() =>
            {
                while (enumerator.MoveNext())
                {
                    Task.Delay(10);
                }
            });
        }
    }
}
