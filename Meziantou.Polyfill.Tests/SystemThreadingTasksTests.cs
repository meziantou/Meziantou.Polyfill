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

public class SystemThreadingTasksTests
{
    [Fact]
    public async Task Task_WaitAsync()
    {
        var tcs = new TaskCompletionSource<int>();
        _ = Task.Run(async () =>
        {
            await Task.Delay(500);
            tcs.TrySetResult(1);
        });

        var result = await tcs.Task.WaitAsync(CancellationToken.None);
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task Task_WaitAsync_Generic_CancellationToken_Cancelled()
    {
        var tcs = new TaskCompletionSource<int>();
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<TaskCanceledException>(async () => await tcs.Task.WaitAsync(cts.Token));
    }

    [Fact]
    public async Task Task_WaitAsync_Generic_CancellationToken_CancelledWhileWaiting()
    {
        var tcs = new TaskCompletionSource<int>();
        using var cts = new CancellationTokenSource();
        
        var task = tcs.Task.WaitAsync(cts.Token);
        cts.Cancel();
        
        await Assert.ThrowsAsync<TaskCanceledException>(async () => await task);
    }

    [Fact]
    public async Task Task_WaitAsync_NonGeneric_CancellationToken()
    {
        var tcs = new TaskCompletionSource<int>();
        _ = Task.Run(async () =>
        {
            await Task.Delay(100);
            tcs.TrySetResult(1);
        });

        await ((Task)tcs.Task).WaitAsync(CancellationToken.None);
        Assert.Equal(1, tcs.Task.Result);
    }

    [Fact]
    public async Task Task_WaitAsync_NonGeneric_CancellationToken_Cancelled()
    {
        var tcs = new TaskCompletionSource<int>();
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<TaskCanceledException>(async () => await ((Task)tcs.Task).WaitAsync(cts.Token));
    }

    [Fact]
    public async Task Task_WaitAsync_Generic_TimeSpan()
    {
        var tcs = new TaskCompletionSource<int>();
        _ = Task.Run(async () =>
        {
            await Task.Delay(100);
            tcs.TrySetResult(42);
        });

        var result = await tcs.Task.WaitAsync(TimeSpan.FromSeconds(5));
        Assert.Equal(42, result);
    }

    [Fact]
    public async Task Task_WaitAsync_Generic_TimeSpan_Timeout()
    {
        var tcs = new TaskCompletionSource<int>();
        
        await Assert.ThrowsAsync<TimeoutException>(async () => 
            await tcs.Task.WaitAsync(TimeSpan.FromMilliseconds(100)));
    }

    [Fact]
    public async Task Task_WaitAsync_Generic_TimeSpan_Zero_Timeout()
    {
        var tcs = new TaskCompletionSource<int>();
        
        await Assert.ThrowsAsync<TimeoutException>(async () => 
            await tcs.Task.WaitAsync(TimeSpan.Zero));
    }

    [Fact]
    public async Task Task_WaitAsync_NonGeneric_TimeSpan()
    {
        var tcs = new TaskCompletionSource<int>();
        _ = Task.Run(async () =>
        {
            await Task.Delay(100);
            tcs.TrySetResult(42);
        });

        await ((Task)tcs.Task).WaitAsync(TimeSpan.FromSeconds(5));
        Assert.Equal(42, tcs.Task.Result);
    }

    [Fact]
    public async Task Task_WaitAsync_NonGeneric_TimeSpan_Timeout()
    {
        var tcs = new TaskCompletionSource<int>();
        
        await Assert.ThrowsAsync<TimeoutException>(async () => 
            await ((Task)tcs.Task).WaitAsync(TimeSpan.FromMilliseconds(100)));
    }

    [Fact]
    public async Task Task_WaitAsync_Generic_TimeSpan_CancellationToken()
    {
        var tcs = new TaskCompletionSource<int>();
        _ = Task.Run(async () =>
        {
            await Task.Delay(100);
            tcs.TrySetResult(42);
        });

        var result = await tcs.Task.WaitAsync(TimeSpan.FromSeconds(5), CancellationToken.None);
        Assert.Equal(42, result);
    }

    [Fact]
    public async Task Task_WaitAsync_Generic_TimeSpan_CancellationToken_Timeout()
    {
        var tcs = new TaskCompletionSource<int>();
        
        await Assert.ThrowsAsync<TimeoutException>(async () => 
            await tcs.Task.WaitAsync(TimeSpan.FromMilliseconds(100), CancellationToken.None));
    }

    [Fact]
    public async Task Task_WaitAsync_Generic_TimeSpan_CancellationToken_Cancelled()
    {
        var tcs = new TaskCompletionSource<int>();
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<TaskCanceledException>(async () => 
            await tcs.Task.WaitAsync(TimeSpan.FromSeconds(5), cts.Token));
    }

    [Fact]
    public async Task Task_WaitAsync_Generic_TimeSpan_CancellationToken_CancelledWhileWaiting()
    {
        var tcs = new TaskCompletionSource<int>();
        using var cts = new CancellationTokenSource();
        
        var task = tcs.Task.WaitAsync(TimeSpan.FromSeconds(5), cts.Token);
        cts.Cancel();
        
        await Assert.ThrowsAsync<TaskCanceledException>(async () => await task);
    }

    [Fact]
    public async Task Task_WaitAsync_NonGeneric_TimeSpan_CancellationToken()
    {
        var tcs = new TaskCompletionSource<int>();
        _ = Task.Run(async () =>
        {
            await Task.Delay(100);
            tcs.TrySetResult(42);
        });

        await ((Task)tcs.Task).WaitAsync(TimeSpan.FromSeconds(5), CancellationToken.None);
        Assert.Equal(42, tcs.Task.Result);
    }

    [Fact]
    public async Task Task_WaitAsync_NonGeneric_TimeSpan_CancellationToken_Timeout()
    {
        var tcs = new TaskCompletionSource<int>();
        
        await Assert.ThrowsAsync<TimeoutException>(async () => 
            await ((Task)tcs.Task).WaitAsync(TimeSpan.FromMilliseconds(100), CancellationToken.None));
    }

    [Fact]
    public async Task Task_WaitAsync_NonGeneric_TimeSpan_CancellationToken_Cancelled()
    {
        var tcs = new TaskCompletionSource<int>();
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<TaskCanceledException>(async () => 
            await ((Task)tcs.Task).WaitAsync(TimeSpan.FromSeconds(5), cts.Token));
    }

}
