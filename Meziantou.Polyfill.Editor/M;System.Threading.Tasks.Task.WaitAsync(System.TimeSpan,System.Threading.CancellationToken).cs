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
        var promise = new CancellationPromise(cancellationToken);
        using (var timeoutCts = new CancellationTokenSource(timeout))
        using (timeoutCts.Token.Register(static state => ((CancellationPromise)state!).TrySetException(new TimeoutException()), promise, false))
        using (cancellationToken.Register(static state => ((CancellationPromise)state!).Cancel(), promise, false))
        {
            var t = await Task.WhenAny(task, promise.Task).ConfigureAwait(false);
            t.GetAwaiter().GetResult();
        }
    }
}

file sealed class CancellationPromise : TaskCompletionSource<object>
{
    private readonly CancellationToken _cancellationToken;

    public CancellationPromise(CancellationToken cancellationToken)
        : base(TaskCreationOptions.RunContinuationsAsynchronously)
    {
        _cancellationToken = cancellationToken;
    }

    public void Cancel() => TrySetCanceled(_cancellationToken);
}