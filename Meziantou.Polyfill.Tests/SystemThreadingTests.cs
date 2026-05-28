using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public class SystemThreadingTests
{
    [Fact]
    public async Task CancellationTokenSource_CancelAsync()
    {
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();
        Assert.True(cts.Token.IsCancellationRequested);
    }

    [Fact]
    public void Lock()
    {
        var instance = new Lock();
        Assert.False(instance.IsHeldByCurrentThread);

        RunOnThread(() =>
        {
            instance.Enter();
            Assert.True(instance.TryEnter());
            instance.Exit(); // exit tryenter
            TryEnterFromOtherThreadShouldFail();
            instance.Exit();
        });

        RunOnThread(() =>
        {
            using var scope = instance.EnterScope();
            TryEnterFromOtherThreadShouldFail();
        });

        RunOnThread(() =>
        {
            Assert.True(instance.TryEnter());
            Assert.True(instance.IsHeldByCurrentThread);
            instance.Exit();
        });

        RunOnThread(() =>
        {
            lock (instance)
            {
                Assert.True(instance.IsHeldByCurrentThread);
            }
        });

        Assert.False(instance.IsHeldByCurrentThread);

        void RunOnThread(Action action)
        {
            var thread = new Thread(() => action());
            thread.Start();
            thread.Join();
        }

        void TryEnterFromOtherThreadShouldFail()
        {
            var thread = new Thread(() => Assert.False(instance.TryEnter()));
            thread.Start();
            thread.Join();
        }
    }

    [Fact]
    public void CancellationToken_Register_WithCancellationToken()
    {
        using var cts = new CancellationTokenSource();
        var token = cts.Token;

        object? callbackState = null;
        CancellationToken callbackToken = default;
        var testState = new object();

        var registration = token.Register((state, ct) =>
        {
            callbackState = state;
            callbackToken = ct;
        }, testState);

        cts.Cancel();

        Assert.Same(testState, callbackState);
        Assert.Equal(token, callbackToken);
        Assert.True(callbackToken.IsCancellationRequested);

        registration.Dispose();
    }

    [Fact]
    public void CancellationToken_UnsafeRegister_WithCancellationToken()
    {
        using var cts = new CancellationTokenSource();
        var token = cts.Token;

        object? callbackState = null;
        CancellationToken callbackToken = default;
        var testState = new object();

        var registration = token.UnsafeRegister((state, ct) =>
        {
            callbackState = state;
            callbackToken = ct;
        }, testState);

        cts.Cancel();

        Assert.Same(testState, callbackState);
        Assert.Equal(token, callbackToken);
        Assert.True(callbackToken.IsCancellationRequested);

        registration.Dispose();
    }

    [Fact]
    public void CancellationToken_UnsafeRegister_WithoutCancellationToken()
    {
        using var cts = new CancellationTokenSource();
        var token = cts.Token;

        object? callbackState = null;
        var testState = new object();

        var registration = token.UnsafeRegister((state) =>
        {
            callbackState = state;
        }, testState);

        cts.Cancel();

        Assert.Same(testState, callbackState);

        registration.Dispose();
    }

    [Fact]
    public async Task PeriodicTimer_WaitForNextTickAsync()
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(50));
        Assert.True(await WaitWithTimeout(timer.WaitForNextTickAsync()));
        Assert.True(await WaitWithTimeout(timer.WaitForNextTickAsync()));
    }

    [Fact]
    public async Task PeriodicTimer_Dispose_ReturnsFalse()
    {
        var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(50));
        timer.Dispose();
        Assert.False(await WaitWithTimeout(timer.WaitForNextTickAsync()));
    }

    [Fact]
    public async Task PeriodicTimer_Dispose_CompletesActiveAndFutureWaits()
    {
        var timer = new PeriodicTimer(TimeSpan.FromHours(1));
        var wait = timer.WaitForNextTickAsync().AsTask();

        timer.Dispose();

        Assert.False(await WaitWithTimeout(wait));

        var nextWait = timer.WaitForNextTickAsync().AsTask();
        Assert.False(await WaitWithTimeout(nextWait));
    }

    [Fact]
    public async Task PeriodicTimer_Cancellation()
    {
        using var timer = new PeriodicTimer(TimeSpan.FromHours(1));
        using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(50));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => WaitWithTimeout(timer.WaitForNextTickAsync(cts.Token)));
    }

    [Fact]
    public async Task PeriodicTimer_Cancellation_PreservesTokenAndAllowsNextWait()
    {
        using var timer = new PeriodicTimer(TimeSpan.FromHours(1));
        using var cts = new CancellationTokenSource();

        var wait = timer.WaitForNextTickAsync(cts.Token);
        cts.Cancel();

        var exception = await Assert.ThrowsAnyAsync<OperationCanceledException>(() => WaitWithTimeout(wait));
        Assert.Equal(cts.Token, exception.CancellationToken);

        timer.Period = TimeSpan.FromMilliseconds(50);
        Assert.True(await WaitWithTimeout(timer.WaitForNextTickAsync()));
    }

    [Fact]
    public async Task PeriodicTimer_ConcurrentWait_Throws()
    {
        var timer = new PeriodicTimer(TimeSpan.FromHours(1));
        var wait = timer.WaitForNextTickAsync().AsTask();

        try
        {
#pragma warning disable CA2012 // ValueTask must be consumed; the call throws synchronously so no ValueTask is produced
            Assert.Throws<InvalidOperationException>(() => timer.WaitForNextTickAsync());
#pragma warning restore CA2012
        }
        finally
        {
            timer.Dispose();
        }

        Assert.False(await WaitWithTimeout(wait));
    }

    [Fact]
    public async Task PeriodicTimer_InfinitePeriod_CanBeRestarted()
    {
        using var timer = new PeriodicTimer(Timeout.InfiniteTimeSpan);
        Assert.Equal(Timeout.InfiniteTimeSpan, timer.Period);

        timer.Period = TimeSpan.FromMilliseconds(50);
        Assert.True(await WaitWithTimeout(timer.WaitForNextTickAsync()));
    }

    [Fact]
    public void PeriodicTimer_InvalidPeriod_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new PeriodicTimer(TimeSpan.Zero));
        Assert.Throws<ArgumentOutOfRangeException>(() => new PeriodicTimer(TimeSpan.FromMilliseconds(-2)));
        Assert.Throws<ArgumentOutOfRangeException>(() => new PeriodicTimer(TimeSpan.FromMilliseconds(4294967295)));
    }

    [Fact]
    public void PeriodicTimer_TimeProviderConstructor_ValidatesArguments()
    {
        Assert.Throws<ArgumentNullException>(() => new PeriodicTimer(TimeSpan.FromMilliseconds(1), null!));
        Assert.Throws<ArgumentOutOfRangeException>(() => new PeriodicTimer(TimeSpan.Zero, null!));
    }

    [Fact]
    public async Task PeriodicTimer_TimeProviderConstructor_UsesProviderTimer()
    {
        var timeProvider = new ManualTimeProvider();
        using var timer = new PeriodicTimer(TimeSpan.FromHours(1), timeProvider);
        var manualTimer = Assert.IsType<ManualTimer>(timeProvider.Timer);

        Assert.Equal(TimeSpan.FromHours(1), manualTimer.DueTime);
        Assert.Equal(TimeSpan.FromHours(1), manualTimer.Period);

        var wait = timer.WaitForNextTickAsync().AsTask();
        manualTimer.Fire();
        Assert.True(await WaitWithTimeout(wait));

        timer.Period = TimeSpan.FromMilliseconds(250);
        Assert.Equal(TimeSpan.FromMilliseconds(250), timer.Period);
        Assert.Equal(TimeSpan.FromMilliseconds(250), manualTimer.DueTime);
        Assert.Equal(TimeSpan.FromMilliseconds(250), manualTimer.Period);

        timer.Dispose();
        Assert.True(manualTimer.IsDisposed);
    }

    [Fact]
    public void PeriodicTimer_TimeProviderConstructor_SuppressesExecutionContextFlow()
    {
        Assert.False(ExecutionContext.IsFlowSuppressed());

        var timeProvider = new ManualTimeProvider();
        using var timer = new PeriodicTimer(TimeSpan.FromHours(1), timeProvider);

        Assert.True(timeProvider.IsExecutionContextFlowSuppressed);
        Assert.False(ExecutionContext.IsFlowSuppressed());
    }

    [Fact]
    public async Task PeriodicTimer_TicksAreCoalesced()
    {
        var timeProvider = new ManualTimeProvider();
        using var timer = new PeriodicTimer(TimeSpan.FromHours(1), timeProvider);
        var manualTimer = Assert.IsType<ManualTimer>(timeProvider.Timer);

        manualTimer.Fire();
        manualTimer.Fire();

        Assert.True(await WaitWithTimeout(timer.WaitForNextTickAsync()));

        var wait = timer.WaitForNextTickAsync().AsTask();
        Assert.False(wait.IsCompleted);

        manualTimer.Fire();
        Assert.True(await WaitWithTimeout(wait));
    }

    [Fact]
    public async Task PeriodicTimer_TicksBeforeCompletedWaitIsConsumedAreCoalesced()
    {
        var timeProvider = new ManualTimeProvider();
        using var timer = new PeriodicTimer(TimeSpan.FromHours(1), timeProvider);
        var manualTimer = Assert.IsType<ManualTimer>(timeProvider.Timer);

        var wait = timer.WaitForNextTickAsync();
        manualTimer.Fire();
        Assert.True(wait.IsCompleted);

        manualTimer.Fire();
        Assert.True(await WaitWithTimeout(wait));

        var nextWait = timer.WaitForNextTickAsync().AsTask();
        Assert.False(nextWait.IsCompleted);

        manualTimer.Fire();
        Assert.True(await WaitWithTimeout(nextWait));
    }

    [Fact]
    public async Task PeriodicTimer_CompletedWaitIsActiveUntilConsumed()
    {
        var timeProvider = new ManualTimeProvider();
        using var timer = new PeriodicTimer(TimeSpan.FromHours(1), timeProvider);
        var manualTimer = Assert.IsType<ManualTimer>(timeProvider.Timer);

        var wait = timer.WaitForNextTickAsync();
        manualTimer.Fire();
        Assert.True(wait.IsCompleted);

#pragma warning disable CA2012 // ValueTask must be consumed; the call throws synchronously so no ValueTask is produced
        Assert.Throws<InvalidOperationException>(() => timer.WaitForNextTickAsync());
#pragma warning restore CA2012
        Assert.True(await WaitWithTimeout(wait));
    }

    [Fact]
    public async Task PeriodicTimer_DisposeVoidsUnconsumedTick()
    {
        var timeProvider = new ManualTimeProvider();
        var timer = new PeriodicTimer(TimeSpan.FromHours(1), timeProvider);
        var manualTimer = Assert.IsType<ManualTimer>(timeProvider.Timer);

        var wait = timer.WaitForNextTickAsync();
        manualTimer.Fire();
        Assert.True(wait.IsCompleted);

        timer.Dispose();
        Assert.False(await WaitWithTimeout(wait));
    }

    [Fact]
    public async Task PeriodicTimer_Period_CanChange()
    {
        using var timer = new PeriodicTimer(TimeSpan.FromHours(1));
        timer.Period = TimeSpan.FromMilliseconds(50);
        Assert.Equal(TimeSpan.FromMilliseconds(50), timer.Period);

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        Assert.True(await WaitWithTimeout(timer.WaitForNextTickAsync(cts.Token)));
    }

    [Fact]
    public void PeriodicTimer_Period_InvalidValue_Throws()
    {
        using var timer = new PeriodicTimer(TimeSpan.FromHours(1));

        Assert.Throws<ArgumentOutOfRangeException>(() => timer.Period = TimeSpan.Zero);
        Assert.Throws<ArgumentOutOfRangeException>(() => timer.Period = TimeSpan.FromMilliseconds(-2));
        Assert.Throws<ArgumentOutOfRangeException>(() => timer.Period = TimeSpan.FromMilliseconds(4294967295));
    }

    [Fact]
    public void PeriodicTimer_Period_AfterDispose_ThrowsAndStoresValue()
    {
        var timer = new PeriodicTimer(TimeSpan.FromHours(1));
        timer.Dispose();

        var newPeriod = TimeSpan.FromMilliseconds(1);
        Assert.Throws<ObjectDisposedException>(() => timer.Period = newPeriod);
        Assert.Equal(newPeriod, timer.Period);
    }

    [Fact]
    public void TimeProvider_System_GetUtcNow()
    {
        var before = DateTimeOffset.UtcNow;
        var result = TimeProvider.System.GetUtcNow();
        var after = DateTimeOffset.UtcNow;

        Assert.True(result >= before && result <= after);
    }

    [Fact]
    public void TimeProvider_System_GetLocalNow()
    {
        var result = TimeProvider.System.GetLocalNow();
        Assert.Equal(TimeZoneInfo.Local.GetUtcOffset(result), result.Offset);
    }

    [Fact]
    public void TimeProvider_System_GetTimestamp()
    {
        var t1 = TimeProvider.System.GetTimestamp();
        var t2 = TimeProvider.System.GetTimestamp();
        Assert.True(t2 >= t1);
    }

    [Fact]
    public void TimeProvider_System_GetElapsedTime()
    {
        var start = TimeProvider.System.GetTimestamp();
        Thread.Sleep(15);
        var elapsed = TimeProvider.System.GetElapsedTime(start);
        Assert.True(elapsed.TotalMilliseconds >= 10, $"Expected >= 10ms, got {elapsed.TotalMilliseconds}ms");
    }

    [Fact]
    public void TimeProvider_System_TimestampFrequency()
    {
        Assert.Equal(Stopwatch.Frequency, TimeProvider.System.TimestampFrequency);
    }

    [Fact]
    public void TimeProvider_System_LocalTimeZone()
    {
        Assert.Equal(TimeZoneInfo.Local, TimeProvider.System.LocalTimeZone);
    }

    [Fact]
    public async Task TimeProvider_System_CreateTimer()
    {
        var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var timer = TimeProvider.System.CreateTimer(static state =>
        {
            var completionSource = (TaskCompletionSource<bool>)state!;
            completionSource.TrySetResult(true);
        }, tcs, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);

        try
        {
            Assert.True(timer.Change(TimeSpan.FromMilliseconds(50), Timeout.InfiniteTimeSpan));
            Assert.True(await WaitWithTimeout(tcs.Task));
        }
        finally
        {
            await timer.DisposeAsync();
        }
    }

    private static Task<T> WaitWithTimeout<T>(ValueTask<T> valueTask) => WaitWithTimeout(valueTask.AsTask());

    private static Task<T> WaitWithTimeout<T>(Task<T> task) => task.WaitAsync(TimeSpan.FromSeconds(5));

    private sealed class ManualTimeProvider : TimeProvider
    {
        public ManualTimer? Timer { get; private set; }
        public bool? IsExecutionContextFlowSuppressed { get; private set; }

        public override ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period)
        {
            IsExecutionContextFlowSuppressed = ExecutionContext.IsFlowSuppressed();
            Timer = new ManualTimer(callback, state, dueTime, period);
            return Timer;
        }
    }

    private sealed class ManualTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period) : ITimer
    {
        public TimeSpan DueTime { get; private set; } = dueTime;
        public TimeSpan Period { get; private set; } = period;
        public bool IsDisposed { get; private set; }

        public bool Change(TimeSpan dueTime, TimeSpan period)
        {
            if (IsDisposed)
                return false;

            DueTime = dueTime;
            Period = period;
            return true;
        }

        public void Dispose() => IsDisposed = true;

        public ValueTask DisposeAsync()
        {
            Dispose();
            return default;
        }

        public void Fire()
        {
            if (!IsDisposed)
                callback(state);
        }
    }

}
