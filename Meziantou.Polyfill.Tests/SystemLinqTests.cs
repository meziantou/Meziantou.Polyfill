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

public class SystemLinqTests
{
        [Fact]
        public void Enumerable_Order()
        {
            Assert.Equal([1, 2, 3], new[] { 2, 1, 3 }.Order());
            Assert.Equal([1, 2, 3], new[] { 2, 1, 3 }.Order(Comparer<int>.Default));
        }

        [Fact]
        public void Enumerable_OrderDescending()
        {
            Assert.Equal([3, 2, 1], new[] { 2, 1, 3 }.OrderDescending());
            Assert.Equal([3, 2, 1], new[] { 2, 1, 3 }.OrderDescending(Comparer<int>.Default));
        }

        [Fact]
        public void Enumerable_DistinctBy()
        {
            Assert.Equal([2, 1], new[] { 2, 1, 1 }.DistinctBy(_ => _));
            Assert.Equal([2, 1], new[] { 2, 1, 1 }.DistinctBy(_ => _, EqualityComparer<int>.Default));
        }

        [Fact]
        public void Enumerable_Zip()
        {
            Assert.Equal(new[] { ('a', 'b') }, "a".Zip("b"));
        }

        [Fact]
        public void Enumerable_CountBy()
        {
            var collection = new[] { 1, 1, 2, 3, 4, 3, 3 };
            var expected = new[] {
                new KeyValuePair<int, int>(1, 2),
                new KeyValuePair<int, int>(2, 1),
                new KeyValuePair<int, int>(3, 3),
                new KeyValuePair<int, int>(4, 1),
            };
            Assert.Equal(expected, collection.CountBy(item => item));
        }

        [Fact]
        public void Enumerable_AggregateBy_Seed()
        {
            var collection = new[] { 1, 2, 2, 3 };
            var expected = new[] {
                new KeyValuePair<int, int>(1, 1),
                new KeyValuePair<int, int>(2, 4),
                new KeyValuePair<int, int>(3, 3),
            };
            Assert.Equal(expected, collection.AggregateBy(item => item, seed: 0, (acc, item) => acc + item));
        }

        [Fact]
        public void Enumerable_AggregateBy_SeedSelector()
        {
            var collection = new[] { 1, 2, 2, 3 };
            var expected = new[] {
                new KeyValuePair<int, int>(1, 2),
                new KeyValuePair<int, int>(2, 5),
                new KeyValuePair<int, int>(3, 4),
            };
            Assert.Equal(expected, collection.AggregateBy(item => item, seedSelector: item => 1, (acc, item) => acc + item));
        }

        [Fact]
        public void Enumerable_MaxBy()
        {
            Assert.Equal("ab", new[] { "a", "ab", "b" }.MaxBy(a => a.Length));
            Assert.Equal("ab", new[] { "a", "ab", "b" }.MaxBy(a => a.Length, Comparer<int>.Default));
        }

        [Fact]
        public void Enumerable_MinBy()
        {
            Assert.Equal("a", new[] { "a", "ab", "b" }.MinBy(a => a.Length));
            Assert.Equal("a", new[] { "a", "ab", "b" }.MinBy(a => a.Length, Comparer<int>.Default));
        }

        [Fact]
        public void Enumerable_ToDictionary()
        {
            Dictionary<string, int> dict = new() { ["a"] = 1, ["b"] = 2 };
    
            var copied = (Dictionary<string, int>)((IEnumerable<KeyValuePair<string, int>>)dict).ToDictionary();
            Assert.Equal(dict, copied);
            Assert.Equal(dict.Comparer, copied.Comparer);
    
            var caseInsensitive = (Dictionary<string, int>)((IEnumerable<KeyValuePair<string, int>>)dict).ToDictionary(StringComparer.OrdinalIgnoreCase);
            Assert.Equal(dict, caseInsensitive);
            Assert.Equal(StringComparer.OrdinalIgnoreCase, caseInsensitive.Comparer);
        }

        [Fact]
        public void Enumerable_ToHashSet()
        {
            Assert.Equal(new[] { "a", "b" }, new[] { "a", "b", "a" }.ToHashSet());
            Assert.Equal(new[] { "a", "b" }, new[] { "a", "b", "A" }.ToHashSet(StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void Enumerable_Index()
        {
            Assert.Equal([(0, "a"), (1, "b")], (new string[] { "a", "b" }).Index());
        }

        [Fact]
        public void IEnumerable_IntersectBy()
        {
            IEnumerable<int> a = [1, 2, 3];
            IEnumerable<int> b = [2, 3, 4];
            var result = a.IntersectBy(b, i => i);
            Assert.Equal([2, 3], result);
        }

        [Fact]
        public void IEnumerable_IntersectBy_Comparer()
        {
            IEnumerable<int> a = [1, 2, 3];
            IEnumerable<int> b = [2, 3, 4];
            var result = a.IntersectBy(b, i => i, EqualityComparer<int>.Default);
            Assert.Equal([2, 3], result);
        }

        [Fact]
        public void IEnumerable_UnionBy()
        {
            IEnumerable<int> a = [1, 2, 3];
            IEnumerable<int> b = [2, 3, 4];
            var result = a.UnionBy(b, i => i);
            Assert.Equal([1, 2, 3, 4], result);
        }

        [Fact]
        public void IEnumerable_UnionBy_Comparer()
        {
            IEnumerable<int> a = [1, 2, 3];
            IEnumerable<int> b = [2, 3, 4];
            var result = a.UnionBy(b, i => i, EqualityComparer<int>.Default);
            Assert.Equal([1, 2, 3, 4], result);
        }

}