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
    /// <returns>The <see cref="Task"/> representing the asynchronous wait.</returns>
    public static Task WaitAsync(this Task task, TimeSpan timeout)
    {
        if (task.IsCompleted || timeout == Timeout.InfiniteTimeSpan)
        {
            return task;
        }

        if (timeout == TimeSpan.Zero)
        {
            return Task.FromException(new TimeoutException());
        }

        return WaitTask.WaitTaskAsync(task, timeout);
    }
}

file static class WaitTask
{
    public static async Task WaitTaskAsync(Task task, TimeSpan timeout)
    {
        var promise = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);
        using (var timeoutCts = new CancellationTokenSource(timeout))
        using (timeoutCts.Token.Register(static state => ((TaskCompletionSource<object>)state!).TrySetException(new TimeoutException()), promise, false))
        {
            var t = await Task.WhenAny(task, promise.Task).ConfigureAwait(false);
            t.GetAwaiter().GetResult();
        }
    }
}