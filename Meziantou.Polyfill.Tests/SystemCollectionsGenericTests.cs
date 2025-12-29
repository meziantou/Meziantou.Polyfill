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

public class SystemCollectionsGenericTests
{
    [Fact]
    public void System_Collections_Generic_KeyValuePair_2_Deconstruct()
    {
        var data = new KeyValuePair<string, object>("test", 2);
        var (key, value) = data;
        Assert.Equal("test", key);
        Assert.Equal(2, value);
    }

    [Fact]
    public void System_Collections_Generic_Queue_1_TryDequeue()
    {
        var queue = new Queue<int>();
        Assert.False(queue.TryDequeue(out _));

        queue.Enqueue(1);
        queue.Enqueue(2);
        Assert.True(queue.TryDequeue(out var item1));
        Assert.True(queue.TryDequeue(out var item2));
        Assert.False(queue.TryDequeue(out _));

        Assert.Equal(1, item1);
        Assert.Equal(2, item2);
    }

    [Fact]
    public void PriorityQueueTests()
    {
        var queue = new PriorityQueue<string, int>();
        queue.Enqueue("1", 1);
        queue.Enqueue("0", 0);
        queue.Enqueue("2", 2);

        Assert.Equal("0", queue.Dequeue());
        Assert.Equal("1", queue.Dequeue());
        Assert.Equal("2", queue.Dequeue());
    }

    [Fact]
    public void ReferenceEqualityComparerTests()
    {
        Assert.False(ReferenceEqualityComparer.Instance.Equals(new object(), new object()));
        Assert.NotEqual(ReferenceEqualityComparer.Instance.GetHashCode(new object()), ReferenceEqualityComparer.Instance.GetHashCode(new object()));

        var obj = new object();
        Assert.True(ReferenceEqualityComparer.Instance.Equals(obj, obj));
        Assert.Equal(ReferenceEqualityComparer.Instance.GetHashCode(obj), ReferenceEqualityComparer.Instance.GetHashCode(obj));
    }

    [Fact]
    public void Dictionary_Remove()
    {
        var dict = new Dictionary<int, int>();
        Assert.False(dict.Remove(1, out _));

        dict.Add(1, 2);
        Assert.True(dict.Remove(1, out var value));
        Assert.Equal(2, value);
    }

    [Fact]
    public void Dictionary_TryAdd_KeyDoesNotExist()
    {
        var dict = new Dictionary<string, int>();
        Assert.True(dict.TryAdd("key1", 100));
        Assert.Equal(100, dict["key1"]);
    }

    [Fact]
    public void Dictionary_TryAdd_KeyAlreadyExists()
    {
        var dict = new Dictionary<string, int>
        {
            ["key1"] = 100
        };

        Assert.False(dict.TryAdd("key1", 200));
        Assert.Equal(100, dict["key1"]);
    }

    [Fact]
    public void Dictionary_TryAdd_NullDictionary()
    {
        Dictionary<string, int> dict = null!;
        Assert.Throws<ArgumentNullException>(() => dict.TryAdd("key1", 100));
    }

    [Fact]
    public void Dictionary_TryAdd_MultipleOperations()
    {
        var dict = new Dictionary<int, string>();

        Assert.True(dict.TryAdd(1, "one"));
        Assert.True(dict.TryAdd(2, "two"));
        Assert.False(dict.TryAdd(1, "uno"));
        Assert.True(dict.TryAdd(3, "three"));

        Assert.Equal(3, dict.Count);
        Assert.Equal("one", dict[1]);
        Assert.Equal("two", dict[2]);
        Assert.Equal("three", dict[3]);
    }
}
