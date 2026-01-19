using System;
using System.Threading;

static partial class PolyfillExtensions
{
    public static CancellationTokenRegistration UnsafeRegister(this CancellationToken cancellationToken, Action<object?> callback, object? state)
    {
#if NET8_0_OR_GREATER
        return cancellationToken.UnsafeRegister(callback, state);
#else
        return cancellationToken.Register(callback, state, useSynchronizationContext: false);
#endif
    }
}
