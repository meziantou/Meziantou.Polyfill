using System;
using System.Threading.Tasks;

static partial class PolyfillExtensions_Task
{
    extension(Task)
    {
        /// <summary>
        /// Creates a task that will complete when all of the supplied tasks have completed.
        /// </summary>
        /// <param name="tasks">The tasks to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        public static Task WhenAll(params ReadOnlySpan<Task> tasks)
        {
            var taskArray = new Task[tasks.Length];
            tasks.CopyTo(taskArray);
            return Task.WhenAll(taskArray);
        }
    }
}
