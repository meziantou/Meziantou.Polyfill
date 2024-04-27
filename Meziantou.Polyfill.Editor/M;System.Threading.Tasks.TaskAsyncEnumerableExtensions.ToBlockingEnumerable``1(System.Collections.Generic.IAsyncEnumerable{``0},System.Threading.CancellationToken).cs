﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    [UnsupportedOSPlatform("browser")]
    public static IEnumerable<T> ToBlockingEnumerable<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        IAsyncEnumerator<T> enumerator = source.GetAsyncEnumerator(cancellationToken);
        // A ManualResetEventSlim variant that lets us reuse the same
        // awaiter callback allocation across the entire enumeration.
        ManualResetEventWithAwaiterSupport? mres = null;

        try
        {
            while (true)
            {
#pragma warning disable CA2012 // Use ValueTasks correctly
                ValueTask<bool> moveNextTask = enumerator.MoveNextAsync();
#pragma warning restore CA2012 // Use ValueTasks correctly

                if (!moveNextTask.IsCompleted)
                {
                    (mres ??= new ManualResetEventWithAwaiterSupport()).Wait(moveNextTask.ConfigureAwait(false).GetAwaiter());
                    Debug.Assert(moveNextTask.IsCompleted);
                }

                if (!moveNextTask.Result)
                {
                    yield break;
                }

                yield return enumerator.Current;
            }
        }
        finally
        {
            ValueTask disposeTask = enumerator.DisposeAsync();

            if (!disposeTask.IsCompleted)
            {
                (mres ?? new ManualResetEventWithAwaiterSupport()).Wait(disposeTask.ConfigureAwait(false).GetAwaiter());
                Debug.Assert(disposeTask.IsCompleted);
            }

            disposeTask.GetAwaiter().GetResult();
        }
    }
}

file sealed class ManualResetEventWithAwaiterSupport : ManualResetEventSlim
{
    private readonly Action _onCompleted;

    public ManualResetEventWithAwaiterSupport()
    {
        _onCompleted = Set;
    }

    [UnsupportedOSPlatform("browser")]
    public void Wait<TAwaiter>(TAwaiter awaiter) where TAwaiter : ICriticalNotifyCompletion
    {
        awaiter.UnsafeOnCompleted(_onCompleted);
        Wait();
        Reset();
    }
}
