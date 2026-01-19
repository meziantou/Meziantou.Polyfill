using System;
using System.Threading;

static partial class PolyfillExtensions
{
    public static CancellationTokenRegistration Register(this CancellationToken cancellationToken, Action<object?, CancellationToken> callback, object? state)
    {
        return cancellationToken.Register(state =>
        {
            callback(state, cancellationToken);
        }, state);
    }
}
