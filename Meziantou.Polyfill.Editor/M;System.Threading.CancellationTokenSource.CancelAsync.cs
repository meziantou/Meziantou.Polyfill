﻿using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static Task CancelAsync(this CancellationTokenSource target)
    {
        target.Cancel();
        return Task.CompletedTask;
    }
}