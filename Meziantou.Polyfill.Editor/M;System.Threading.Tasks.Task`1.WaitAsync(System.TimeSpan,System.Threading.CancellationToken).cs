using System.Threading.Tasks;
using System.Threading;
using System;

static partial class PolyfillExtensions
{
    /// <summary>
    /// Gets a <see cref="Task<TResult>{TResult}"/> that will complete when the <paramref name="task"/> completes or when the specified <paramref name="cancellationToken"/> has cancellation requested.
    /// </summary>
    /// <typeparam name="TResult">The type of the task result.</typeparam>
    /// <param name="task">The task to wait on for completion.</param>
    /// <param name="timeout">The timeout after which the <see cref="Task<TResult>{TResult}"/> should be faulted with a <see cref="TimeoutException"/> if it hasn't otherwise completed.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for a cancellation request.</param>
    /// <returns>The <see cref="Task<TResult>{TResult}"/> representing the asynchronous wait.</returns>
    public static Task<TResult> WaitAsync<TResult>(this Task<TResult> task, TimeSpan timeout, CancellationToken cancellationToken)
    {
        if (task.IsCompleted || timeout == Timeout.InfiniteTimeSpan || (!cancellationToken.CanBeCanceled))
        {
            return task;
        }

        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled<TResult>(cancellationToken);
        }

        if (timeout == TimeSpan.Zero)
        {
            return Task.FromException<TResult>(new TimeoutException());
        }

        return WaitTask.WaitTaskAsync(task, timeout, cancellationToken);
    }
}

file static class WaitTask
{
    public static async Task<TResult> WaitTaskAsync<TResult>(Task<TResult> task, TimeSpan timeout, CancellationToken cancellationToken)
    {
        var delayTask = Task<TResult>.Delay(timeout, cancellationToken);
        var t = await Task<TResult>.WhenAny(task, delayTask).ConfigureAwait(false);

        if (t == delayTask)
        {
            t.GetAwaiter().GetResult(); // Propagate cancellation
            throw new TimeoutException();
        }

        return task.GetAwaiter().GetResult();
    }
}