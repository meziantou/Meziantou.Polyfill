using System;
using System.Collections;
using System.Collections.Concurrent;
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

public class SystemCollectionsConcurrentTests
{
    [Fact]
    public void ConcurrentDictionary_GetOrAdd()
    {
        var dict = new ConcurrentDictionary<int, string>();
        var actual = dict.GetOrAdd(0, (key, arg) => arg, "arg");
        Assert.Equal("arg", actual);
    }

    [Fact]
    public void ConcurrentBag_Clear()
    {
        var bag = new ConcurrentBag<int>();
        bag.Add(1);
        bag.Add(2);
        bag.Add(3);

        Assert.Equal(3, bag.Count);

        bag.Clear();

        Assert.Equal(0, bag.Count);
        Assert.Empty(bag);
    }

    [Fact]
    public void ConcurrentQueue_Clear()
    {
        var queue = new ConcurrentQueue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        Assert.Equal(3, queue.Count);

        queue.Clear();

        Assert.Equal(0, queue.Count);
        Assert.Empty(queue);
    }

    [Fact]
    public void ConcurrentQueue_Clear_EmptyQueue()
    {
        var queue = new ConcurrentQueue<int>();

        Assert.Equal(0, queue.Count);

        queue.Clear();

        Assert.Equal(0, queue.Count);
        Assert.Empty(queue);
    }

    [Fact]
    public void ConcurrentQueue_Clear_PreservesOrder()
    {
        var queue = new ConcurrentQueue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        queue.Clear();

        queue.Enqueue(10);
        queue.Enqueue(20);

        Assert.True(queue.TryDequeue(out var first));
        Assert.Equal(10, first);
        Assert.True(queue.TryDequeue(out var second));
        Assert.Equal(20, second);
    }

}
