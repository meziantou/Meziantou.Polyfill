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
    /// <returns>The <see cref="Task<TResult>{TResult}"/> representing the asynchronous wait.</returns>
    public static Task<TResult> WaitAsync<TResult>(this Task<TResult> task, TimeSpan timeout)
    {
        if (task.IsCompleted || timeout == Timeout.InfiniteTimeSpan)
        {
            return task;
        }

        if (timeout == TimeSpan.Zero)
        {
            return Task.FromException<TResult>(new TimeoutException());
        }

        return WaitTask.WaitTaskAsync(task, timeout);
    }
}

file static class WaitTask
{
    public static async Task<TResult> WaitTaskAsync<TResult>(Task<TResult> task, TimeSpan timeout)
    {
        var promise = new TaskCompletionSource<TResult>(TaskCreationOptions.RunContinuationsAsynchronously);
        using (var timeoutCts = new CancellationTokenSource(timeout))
        using (timeoutCts.Token.Register(static state => ((TaskCompletionSource<TResult>)state!).TrySetException(new TimeoutException()), promise, false))
        {
            var t = await Task.WhenAny(task, promise.Task).ConfigureAwait(false);
            return t.GetAwaiter().GetResult();
        }
    }
}