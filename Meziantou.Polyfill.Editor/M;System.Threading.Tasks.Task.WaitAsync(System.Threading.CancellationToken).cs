using System.Threading.Tasks;
using System.Threading;

static partial class PolyfillExtensions
{
    /// <summary>
    /// Gets a <see cref="Task"/> that will complete when the <paramref name="task"/> completes or when the specified <paramref name="cancellationToken"/> has cancellation requested.
    /// </summary>
    /// <param name="task">The task to wait on for completion.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for a cancellation request.</param>
    /// <returns>The <see cref="Task"/> representing the asynchronous wait.</returns>
    public static Task WaitAsync(this Task task, CancellationToken cancellationToken)
    {
        if (task.IsCompleted || (!cancellationToken.CanBeCanceled))
        {
            return task;
        }

        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        return WaitTask.WaitTaskAsync(task, cancellationToken);
    }
}

file static class WaitTask
{
    public static async Task WaitTaskAsync(Task task, CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);
        using (cancellationToken.Register(static state => ((TaskCompletionSource<object>)state!).SetCanceled(), tcs, false))
        {
            var t = await Task.WhenAny(task, tcs.Task).ConfigureAwait(false);
            t.GetAwaiter().GetResult();
        }
    }
}