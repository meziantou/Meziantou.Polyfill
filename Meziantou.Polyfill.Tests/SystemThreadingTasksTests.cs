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
#pragma warning disable xUnit1031 // Do not use blocking task operations in test method
        Assert.Equal(1, tcs.Task.Result);
#pragma warning restore xUnit1031 // Do not use blocking task operations in test method
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
#pragma warning disable xUnit1031 // Do not use blocking task operations in test method
        Assert.Equal(42, tcs.Task.Result);
#pragma warning restore xUnit1031
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
#pragma warning disable xUnit1031 // Do not use blocking task operations in test method
        Assert.Equal(42, tcs.Task.Result);
#pragma warning restore xUnit1031
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

    [Fact]
    public async Task Task_WhenAll_ReadOnlySpan()
    {
        Task[] tasks = [Task.CompletedTask, Task.Delay(1)];

        await Task.WhenAll(tasks.AsSpan());
    }

    [Fact]
    public async Task Task_WhenAll_ReadOnlySpan_Generic()
    {
        Task<int>[] tasks = [Task.FromResult(1), Task.FromResult(2)];

        var results = await Task.WhenAll<int>(tasks.AsSpan());

        Assert.Equal([1, 2], results);
    }

    [Fact]
    public async Task Task_WhenAny_ReadOnlySpan()
    {
        var incompleteTask = new TaskCompletionSource<bool>();
        Task[] tasks = [incompleteTask.Task, Task.CompletedTask];

        var completedTask = await Task.WhenAny(tasks.AsSpan());

        Assert.Same(tasks[1], completedTask);
    }

    [Fact]
    public async Task Task_WhenAny_ReadOnlySpan_Generic()
    {
        var incompleteTask = new TaskCompletionSource<int>();
        Task<int>[] tasks = [incompleteTask.Task, Task.FromResult(2)];

        var completedTask = await Task.WhenAny<int>(tasks.AsSpan());

        Assert.Same(tasks[1], completedTask);
        Assert.Equal(2, await completedTask);
    }

    [Fact]
    public async Task Task_WhenEach_YieldsTasksAsTheyCompleteIncludingDuplicates()
    {
        var first = new TaskCompletionSource<bool>();
        var second = new TaskCompletionSource<bool>();
        IEnumerable<Task> tasks = [first.Task, second.Task, first.Task];
        var enumerable = Task.WhenEach(tasks);
        await using var enumerator = enumerable.GetAsyncEnumerator();

        second.SetResult(true);
        Assert.True(await enumerator.MoveNextAsync());
        Assert.Same(second.Task, enumerator.Current);

        first.SetResult(true);
        Assert.True(await enumerator.MoveNextAsync());
        Assert.Same(first.Task, enumerator.Current);
        Assert.True(await enumerator.MoveNextAsync());
        Assert.Same(first.Task, enumerator.Current);
        Assert.False(await enumerator.MoveNextAsync());
    }

    [Fact]
    public async Task Task_WhenEach_Generic_YieldsTasksAsTheyComplete()
    {
        var first = new TaskCompletionSource<int>();
        var second = new TaskCompletionSource<int>();
        IEnumerable<Task<int>> tasks = [first.Task, second.Task];
        var enumerable = Task.WhenEach<int>(tasks);
        await using var enumerator = enumerable.GetAsyncEnumerator();

        second.SetResult(2);
        Assert.True(await enumerator.MoveNextAsync());
        Assert.Same(second.Task, enumerator.Current);

        first.SetResult(1);
        Assert.True(await enumerator.MoveNextAsync());
        Assert.Same(first.Task, enumerator.Current);
        Assert.False(await enumerator.MoveNextAsync());
    }

    [Fact]
    public async Task Task_WhenEach_EnumerationCanBeCanceled()
    {
        var task = new TaskCompletionSource<bool>();
        using var cancellationTokenSource = new CancellationTokenSource();
        IEnumerable<Task> tasks = [task.Task];
        await using var enumerator = Task.WhenEach(tasks).GetAsyncEnumerator(cancellationTokenSource.Token);

        var moveNextTask = enumerator.MoveNextAsync().AsTask();
        cancellationTokenSource.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => moveNextTask);
    }
}
