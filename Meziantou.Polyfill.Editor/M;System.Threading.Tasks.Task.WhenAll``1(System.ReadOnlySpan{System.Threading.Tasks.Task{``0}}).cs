using System;
using System.Threading.Tasks;

static partial class PolyfillExtensions_Task
{
    extension(Task)
    {
        /// <summary>
        /// Creates a task that will complete when all of the supplied tasks have completed.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the tasks.</typeparam>
        /// <param name="tasks">The tasks to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        public static Task<TResult[]> WhenAll<TResult>(params ReadOnlySpan<Task<TResult>> tasks)
        {
            var taskArray = new Task<TResult>[tasks.Length];
            tasks.CopyTo(taskArray);
            return Task.WhenAll(taskArray);
        }
    }
}
