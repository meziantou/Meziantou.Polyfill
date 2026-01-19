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

}
