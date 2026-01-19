using System;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static void SetCanceled<T>(this TaskCompletionSource<T> taskCompletionSource, CancellationToken cancellationToken)
    {
        if (!taskCompletionSource.TrySetCanceled(cancellationToken))
        {
            throw new InvalidOperationException("An attempt was made to transition a task to a final state when it had already completed.");
        }
    }
}
