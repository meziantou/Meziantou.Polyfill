using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions_Task
{
    extension(System.Threading.Tasks.Task)
    {
        /// <summary>
        /// Creates an <see cref="IAsyncEnumerable{T}"/> that will yield the supplied tasks as those tasks complete.
        /// </summary>
        /// <param name="tasks">The tasks to iterate through as they complete.</param>
        /// <returns>An <see cref="IAsyncEnumerable{T}"/> for iterating through the supplied tasks.</returns>
        /// <remarks>
        /// The supplied tasks will become available to be output via the enumerable once they've completed.
        /// The exact order in which the tasks will become available is not defined.
        /// </remarks>
        public static IAsyncEnumerable<Task> WhenEach(IEnumerable<Task> tasks)
        {
            return WhenEachImplementation.WhenEachAsync(tasks);
        }
    }
}

file static class WhenEachImplementation
{
    public static async IAsyncEnumerable<Task> WhenEachAsync(IEnumerable<Task> tasks, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (tasks == null)
            throw new System.ArgumentNullException(nameof(tasks));

        var taskList = new List<Task>();
        foreach (var task in tasks)
        {
            if (task == null)
                throw new System.ArgumentException("The tasks argument contained a null element.", nameof(tasks));
            taskList.Add(task);
        }

        if (taskList.Count == 0)
            yield break;

        var remainingTasks = new HashSet<Task>(taskList);

        while (remainingTasks.Count > 0)
        {
            var completedTask = await Task.WhenAny(remainingTasks).ConfigureAwait(false);

            cancellationToken.ThrowIfCancellationRequested();

            remainingTasks.Remove(completedTask);
            yield return completedTask;
        }
    }
}
