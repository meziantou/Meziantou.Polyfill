#pragma warning disable CA1307
#pragma warning disable CA1837
#pragma warning disable CA1849
#pragma warning disable CA2000
#pragma warning disable CA2264
#pragma warning disable CA5351
#pragma warning disable MA0001
#pragma warning disable MA0002
#pragma warning disable MA0015
#pragma warning disable MA0021
#pragma warning disable MA0074
#pragma warning disable MA0131
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security.Cryptography;
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

}