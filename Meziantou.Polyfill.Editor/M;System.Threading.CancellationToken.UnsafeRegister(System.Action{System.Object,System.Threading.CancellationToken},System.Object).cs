using System;
using System.Threading;

static partial class PolyfillExtensions
{
    public static CancellationTokenRegistration UnsafeRegister(this CancellationToken cancellationToken, Action<object?, CancellationToken> callback, object? state)
    {
        return cancellationToken.UnsafeRegister(state =>
        {
            callback(state, cancellationToken);
        }, state);
    }
}
