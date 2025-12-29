using System.Threading.Tasks;
using System.Threading;
using System;

static partial class PolyfillExtensions
{
    /// <summary>
    /// Gets a <see cref="Task"/> that will complete when the <paramref name="task"/> completes or when the specified <paramref name="cancellationToken"/> has cancellation requested.
    /// </summary>
    /// <param name="task">The task to wait on for completion.</param>
    /// <param name="timeout">The timeout after which the <see cref="Task"/> should be faulted with a <see cref="TimeoutException"/> if it hasn't otherwise completed.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for a cancellation request.</param>
    /// <returns>The <see cref="Task"/> representing the asynchronous wait.</returns>
    public static Task WaitAsync(this Task task, TimeSpan timeout, CancellationToken cancellationToken)
    {
        if (task.IsCompleted || (timeout == Timeout.InfiniteTimeSpan && !cancellationToken.CanBeCanceled))
        {
            return task;
        }

        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        if (timeout == TimeSpan.Zero)
        {
            return Task.FromException(new TimeoutException());
        }

        return WaitTask.WaitTaskAsync(task, timeout, cancellationToken);
    }
}

file static class WaitTask
{
    public static async Task WaitTaskAsync(Task task, TimeSpan timeout, CancellationToken cancellationToken)
    {
        var delayTask = Task.Delay(timeout, cancellationToken);
        var t = await Task.WhenAny(task, delayTask).ConfigureAwait(false);

        if (t == delayTask)
        {
            t.GetAwaiter().GetResult(); // Propagate cancellation
            throw new TimeoutException();
        }

        task.GetAwaiter().GetResult();
    }
}