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
        /// <typeparam name="TResult">The type of the result returned by the tasks.</typeparam>
        /// <param name="tasks">The tasks to iterate through as they complete.</param>
        /// <returns>An <see cref="IAsyncEnumerable{T}"/> for iterating through the supplied tasks.</returns>
        /// <remarks>
        /// The supplied tasks will become available to be output via the enumerable once they've completed.
        /// The exact order in which the tasks will become available is not defined.
        /// </remarks>
        public static IAsyncEnumerable<Task<TResult>> WhenEach<TResult>(IEnumerable<Task<TResult>> tasks)
        {
            return WhenEachGenericImplementation<TResult>.WhenEachAsync(tasks);
        }
    }
}

file static class WhenEachGenericImplementation<TResult>
{
    public static async IAsyncEnumerable<Task<TResult>> WhenEachAsync(IEnumerable<Task<TResult>> tasks, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (tasks == null)
            throw new System.ArgumentNullException(nameof(tasks));

        var taskList = new List<Task<TResult>>();
        foreach (var task in tasks)
        {
            if (task == null)
                throw new System.ArgumentException("The tasks argument contained a null element.", nameof(tasks));
            taskList.Add(task);
        }

        if (taskList.Count == 0)
            yield break;

        var remainingTasks = taskList;

        while (remainingTasks.Count > 0)
        {
            var completedTask = await WhenAnyAsync(remainingTasks, cancellationToken).ConfigureAwait(false);

            remainingTasks.Remove(completedTask);
            yield return completedTask;
        }
    }

    private static async Task<Task<TResult>> WhenAnyAsync(IEnumerable<Task<TResult>> tasks, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (!cancellationToken.CanBeCanceled)
            return await Task.WhenAny(tasks).ConfigureAwait(false);

        var cancellationTaskSource = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        using var registration = cancellationToken.Register(static state => ((TaskCompletionSource<bool>)state).TrySetResult(true), cancellationTaskSource);
        var whenAnyTask = Task.WhenAny(tasks);
        if (await Task.WhenAny(whenAnyTask, cancellationTaskSource.Task).ConfigureAwait(false) != whenAnyTask)
            cancellationToken.ThrowIfCancellationRequested();

        return await whenAnyTask.ConfigureAwait(false);
    }
}
