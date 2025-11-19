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

public class SystemLinqAsyncTests
{
        [Fact]
        public void ToBlockingEnumerable()
        {
            Assert.Equal(["a", "b"], CustomAsyncEnumerable().ToBlockingEnumerable());
    
            async IAsyncEnumerable<string> CustomAsyncEnumerable()
            {
                yield return "a";
                await Task.Yield();
                yield return "b";
            }
        }

        [Fact]
        public async Task AsyncEnumerable_AnyAsync()
        {
            Assert.True(await CustomAsyncEnumerable().AnyAsync());
            Assert.True(await CustomAsyncEnumerable().AnyAsync(item => item == "a"));
            Assert.False(await CustomAsyncEnumerable().AnyAsync(item => item == "c"));
            Assert.False(await CustomAsyncEnumerable().AnyAsync(async (item, _) => { await Task.Yield(); return item == "c"; }));
    
            async IAsyncEnumerable<string> CustomAsyncEnumerable()
            {
                yield return "a";
                await Task.Yield();
                yield return "b";
            }
        }

        [Fact]
        public async Task AsyncEnumerable_Where()
        {
            Assert.Equal(["a"], await CustomAsyncEnumerable().Where(item => item == "a").ToListAsync());
            Assert.Equal(["a"], await CustomAsyncEnumerable().Where((item, index) => item == "a").ToListAsync());
            Assert.Equal(["a"], await CustomAsyncEnumerable().Where(async (item, _) => { await Task.Yield(); return item == "a"; }).ToListAsync());
            Assert.Equal(["a"], await CustomAsyncEnumerable().Where(async (item, index, _) => { await Task.Yield(); return item == "a"; }).ToListAsync());
    
            async IAsyncEnumerable<string> CustomAsyncEnumerable()
            {
                yield return "a";
                await Task.Yield();
                yield return "b";
            }
        }

        [Fact]
        public async Task AsyncEnumerable_FirstAsync()
        {
            Assert.Equal("a", await CustomAsyncEnumerable().FirstAsync());
            Assert.Equal("a", await CustomAsyncEnumerable().FirstAsync(item => item == "a"));
            Assert.Equal("a", await CustomAsyncEnumerable().FirstAsync(async (item, _) => { await Task.Yield(); return item == "a"; }));
    
            async IAsyncEnumerable<string> CustomAsyncEnumerable()
            {
                yield return "a";
                await Task.Yield();
                yield return "b";
            }
        }

        [Fact]
        public async Task AsyncEnumerable_FirstOrDefaultAsync()
        {
            Assert.Equal("a", await CustomAsyncEnumerable().FirstOrDefaultAsync());
            Assert.Equal("a", await CustomAsyncEnumerable().FirstOrDefaultAsync("default"));
            Assert.Equal("a", await CustomAsyncEnumerable().FirstOrDefaultAsync(item => item == "a"));
            Assert.Equal("a", await CustomAsyncEnumerable().FirstOrDefaultAsync(item => item == "a", "default"));
            Assert.Equal("a", await CustomAsyncEnumerable().FirstOrDefaultAsync(async (item, _) => { await Task.Yield(); return item == "a"; }));
            Assert.Equal("a", await CustomAsyncEnumerable().FirstOrDefaultAsync(async (item, _) => { await Task.Yield(); return item == "a"; }, "default"));
    
            async IAsyncEnumerable<string> CustomAsyncEnumerable()
            {
                yield return "a";
                await Task.Yield();
                yield return "b";
            }
        }

        [Fact]
        public async Task AsyncEnumerable_Concat()
        {
            Assert.Equal(["a", "b"], await new[] { "a" }.ToAsyncEnumerable().Concat(new[] { "b" }.ToAsyncEnumerable()).ToListAsync());
        }

        [Fact]
        public async Task AsyncEnumerable_AllAsync()
        {
            Assert.True(await CreateAsyncEnumerable([1, 2, 3]).AllAsync(x => x > 0));
            Assert.False(await CreateAsyncEnumerable([1, 2, 3]).AllAsync(x => x > 2));
            Assert.True(await CreateAsyncEnumerable([1, 2, 3]).AllAsync(async (x, _) => { await Task.Yield(); return x > 0; }));
            Assert.False(await CreateAsyncEnumerable([1, 2, 3]).AllAsync(async (x, _) => { await Task.Yield(); return x > 2; }));
        }

        [Fact]
        public async Task AsyncEnumerable_Append()
        {
            var result = await CreateAsyncEnumerable([1, 2]).Append(3).ToListAsync();
            Assert.Equal([1, 2, 3], result);
        }

        [Fact]
        public async Task AsyncEnumerable_Prepend()
        {
            var result = await CreateAsyncEnumerable([2, 3]).Prepend(1).ToListAsync();
            Assert.Equal([1, 2, 3], result);
        }

        [Fact]
        public async Task AsyncEnumerable_Chunk()
        {
            var result = await CreateAsyncEnumerable([1, 2, 3, 4, 5]).Chunk(2).ToListAsync();
            Assert.Equal(3, result.Count);
            Assert.Equal([1, 2], result[0]);
            Assert.Equal([3, 4], result[1]);
            Assert.Equal([5], result[2]);
        }

        [Fact]
        public async Task AsyncEnumerable_ContainsAsync()
        {
            Assert.True(await CreateAsyncEnumerable([1, 2, 3]).ContainsAsync(2));
            Assert.False(await CreateAsyncEnumerable([1, 2, 3]).ContainsAsync(4));
        }

        [Fact]
        public async Task AsyncEnumerable_CountAsync()
        {
            Assert.Equal(3, await CreateAsyncEnumerable([1, 2, 3]).CountAsync());
            Assert.Equal(2, await CreateAsyncEnumerable([1, 2, 3]).CountAsync(x => x > 1));
        }

        [Fact]
        public async Task AsyncEnumerable_LongCountAsync()
        {
            Assert.Equal(3L, await CreateAsyncEnumerable([1, 2, 3]).LongCountAsync());
            Assert.Equal(2L, await CreateAsyncEnumerable([1, 2, 3]).LongCountAsync(x => x > 1));
        }

        [Fact]
        public async Task AsyncEnumerable_DefaultIfEmpty()
        {
            Assert.Equal([1, 2], await CreateAsyncEnumerable([1, 2]).DefaultIfEmpty().ToListAsync());
            Assert.Equal([0], await CreateAsyncEnumerable<int>([]).DefaultIfEmpty().ToListAsync());
            Assert.Equal([5], await CreateAsyncEnumerable<int>([]).DefaultIfEmpty(5).ToListAsync());
        }

        [Fact]
        public async Task AsyncEnumerable_Distinct()
        {
            var result = await CreateAsyncEnumerable([1, 2, 2, 3, 1]).Distinct().ToListAsync();
            Assert.Equal([1, 2, 3], result);
        }

        [Fact]
        public async Task AsyncEnumerable_DistinctBy()
        {
            var result = await CreateAsyncEnumerable(["a", "bb", "ccc", "dd"]).DistinctBy(x => x.Length).ToListAsync();
            Assert.Equal(["a", "bb", "ccc"], result);
        }

        [Fact]
        public async Task AsyncEnumerable_ElementAtAsync()
        {
            Assert.Equal(2, await CreateAsyncEnumerable([1, 2, 3]).ElementAtAsync(1));
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await CreateAsyncEnumerable([1, 2, 3]).ElementAtAsync(5));
        }

        [Fact]
        public async Task AsyncEnumerable_ElementAtOrDefaultAsync()
        {
            Assert.Equal(2, await CreateAsyncEnumerable([1, 2, 3]).ElementAtOrDefaultAsync(1));
            Assert.Equal(0, await CreateAsyncEnumerable([1, 2, 3]).ElementAtOrDefaultAsync(5));
        }

        [Fact]
        public async Task AsyncEnumerable_Except()
        {
            var result = await CreateAsyncEnumerable([1, 2, 3, 4]).Except(CreateAsyncEnumerable([2, 4])).ToListAsync();
            Assert.Equal([1, 3], result);
        }

        [Fact]
        public async Task AsyncEnumerable_ExceptBy()
        {
            var result = await CreateAsyncEnumerable(["a", "bb", "ccc"]).ExceptBy(CreateAsyncEnumerable([2, 3]), x => x.Length).ToListAsync();
            Assert.Equal(["a"], result);
        }

        [Fact]
        public async Task AsyncEnumerable_LastAsync()
        {
            Assert.Equal(3, await CreateAsyncEnumerable([1, 2, 3]).LastAsync());
            Assert.Equal(2, await CreateAsyncEnumerable([1, 2, 3]).LastAsync(x => x < 3));
            Assert.Equal(2, await CreateAsyncEnumerable([1, 2, 3]).LastAsync(async (x, _) => { await Task.Yield(); return x < 3; }));
        }

        [Fact]
        public async Task AsyncEnumerable_LastOrDefaultAsync()
        {
            Assert.Equal(3, await CreateAsyncEnumerable([1, 2, 3]).LastOrDefaultAsync());
            Assert.Equal(0, await CreateAsyncEnumerable<int>([]).LastOrDefaultAsync());
            Assert.Equal(99, await CreateAsyncEnumerable<int>([]).LastOrDefaultAsync(99));
            Assert.Equal(2, await CreateAsyncEnumerable([1, 2, 3]).LastOrDefaultAsync(x => x < 3));
            Assert.Equal(2, await CreateAsyncEnumerable([1, 2, 3]).LastOrDefaultAsync(async (x, _) => { await Task.Yield(); return x < 3; }));
        }

        [Fact]
        public async Task AsyncEnumerable_MaxAsync()
        {
            Assert.Equal(3, await CreateAsyncEnumerable([1, 2, 3]).MaxAsync());
            Assert.Equal(3, await CreateAsyncEnumerable([1, 2, 3]).MaxAsync(Comparer<int>.Default));
        }

        [Fact]
        public async Task AsyncEnumerable_MaxByAsync()
        {
            Assert.Equal("ccc", await CreateAsyncEnumerable(["a", "bb", "ccc"]).MaxByAsync(x => x.Length));
        }

        [Fact]
        public async Task AsyncEnumerable_MinAsync()
        {
            Assert.Equal(1, await CreateAsyncEnumerable([1, 2, 3]).MinAsync());
            Assert.Equal(1, await CreateAsyncEnumerable([1, 2, 3]).MinAsync(Comparer<int>.Default));
        }

        [Fact]
        public async Task AsyncEnumerable_MinByAsync()
        {
            Assert.Equal("a", await CreateAsyncEnumerable(["ccc", "bb", "a"]).MinByAsync(x => x.Length));
        }

        [Fact]
        public async Task AsyncEnumerable_Select()
        {
            var result = await CreateAsyncEnumerable([1, 2, 3]).Select(x => x * 2).ToListAsync();
            Assert.Equal([2, 4, 6], result);
        }

        [Fact]
        public async Task AsyncEnumerable_SelectMany()
        {
            var result = await CreateAsyncEnumerable([1, 2]).SelectMany(x => new[] { x, x * 10 }).ToListAsync();
            Assert.Equal([1, 10, 2, 20], result);
        }

        [Fact]
        public async Task AsyncEnumerable_SingleAsync()
        {
            Assert.Equal(2, await CreateAsyncEnumerable([2]).SingleAsync());
            Assert.Equal(2, await CreateAsyncEnumerable([1, 2, 3]).SingleAsync(x => x == 2));
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await CreateAsyncEnumerable([1, 2]).SingleAsync());
        }

        [Fact]
        public async Task AsyncEnumerable_SingleOrDefaultAsync()
        {
            Assert.Equal(2, await CreateAsyncEnumerable([2]).SingleOrDefaultAsync());
            Assert.Equal(0, await CreateAsyncEnumerable<int>([]).SingleOrDefaultAsync());
            Assert.Equal(99, await CreateAsyncEnumerable<int>([]).SingleOrDefaultAsync(99));
            Assert.Equal(2, await CreateAsyncEnumerable([1, 2, 3]).SingleOrDefaultAsync(x => x == 2));
        }

        [Fact]
        public async Task AsyncEnumerable_Skip()
        {
            var result = await CreateAsyncEnumerable([1, 2, 3, 4]).Skip(2).ToListAsync();
            Assert.Equal([3, 4], result);
        }

        [Fact]
        public async Task AsyncEnumerable_SkipLast()
        {
            var result = await CreateAsyncEnumerable([1, 2, 3, 4]).SkipLast(2).ToListAsync();
            Assert.Equal([1, 2], result);
        }

        [Fact]
        public async Task AsyncEnumerable_SkipWhile()
        {
            var result = await CreateAsyncEnumerable([1, 2, 3, 4]).SkipWhile(x => x < 3).ToListAsync();
            Assert.Equal([3, 4], result);
        }

        [Fact]
        public async Task AsyncEnumerable_Take()
        {
            var result = await CreateAsyncEnumerable([1, 2, 3, 4]).Take(2).ToListAsync();
            Assert.Equal([1, 2], result);
        }

        [Fact]
        public async Task AsyncEnumerable_TakeLast()
        {
            var result = await CreateAsyncEnumerable([1, 2, 3, 4]).TakeLast(2).ToListAsync();
            Assert.Equal([3, 4], result);
        }

        [Fact]
        public async Task AsyncEnumerable_TakeWhile()
        {
            var result = await CreateAsyncEnumerable([1, 2, 3, 4]).TakeWhile(x => x < 3).ToListAsync();
            Assert.Equal([1, 2], result);
        }

        [Fact]
        public async Task AsyncEnumerable_ToArrayAsync()
        {
            var result = await CreateAsyncEnumerable([1, 2, 3]).ToArrayAsync();
            Assert.Equal([1, 2, 3], result);
        }

        [Fact]
        public async Task AsyncEnumerable_ToListAsync()
        {
            var result = await CreateAsyncEnumerable([1, 2, 3]).ToListAsync();
            Assert.Equal([1, 2, 3], result);
        }

        [Fact]
        public async Task AsyncEnumerable_ToDictionaryAsync()
        {
            var result = await CreateAsyncEnumerable([1, 2, 3]).ToDictionaryAsync(x => x.ToString(CultureInfo.InvariantCulture));
            Assert.Equal(3, result.Count);
            Assert.Equal(1, result["1"]);
            Assert.Equal(2, result["2"]);
            Assert.Equal(3, result["3"]);
        }

        [Fact]
        public async Task AsyncEnumerable_ToHashSetAsync()
        {
            var result = await CreateAsyncEnumerable([1, 2, 2, 3]).ToHashSetAsync();
            Assert.Equal([1, 2, 3], result.OrderBy(x => x));
        }

        [Fact]
        public async Task AsyncEnumerable_Union()
        {
            var result = await CreateAsyncEnumerable([1, 2, 3]).Union(CreateAsyncEnumerable([3, 4, 5])).ToListAsync();
            Assert.Equal([1, 2, 3, 4, 5], result);
        }

        [Fact]
        public async Task AsyncEnumerable_UnionBy()
        {
            var result = await CreateAsyncEnumerable(["a", "bb"]).UnionBy(CreateAsyncEnumerable(["ccc", "dd"]), x => x.Length).ToListAsync();
            Assert.Equal(["a", "bb", "ccc"], result);
        }

        [Fact]
        public async Task AsyncEnumerable_Zip()
        {
            var result = await CreateAsyncEnumerable([1, 2]).Zip(CreateAsyncEnumerable(["a", "b"])).ToListAsync();
            Assert.Equal([(1, "a"), (2, "b")], result);
        }

        [Fact]
        public async Task AsyncEnumerable_SumAsync_Int()
        {
            Assert.Equal(6, await CreateAsyncEnumerable([1, 2, 3]).SumAsync());
        }

        [Fact]
        public async Task AsyncEnumerable_SumAsync_Long()
        {
            Assert.Equal(6L, await CreateAsyncEnumerable([1L, 2L, 3L]).SumAsync());
        }

        [Fact]
        public async Task AsyncEnumerable_SumAsync_Float()
        {
            Assert.Equal(6.0f, await CreateAsyncEnumerable([1.0f, 2.0f, 3.0f]).SumAsync());
        }

        [Fact]
        public async Task AsyncEnumerable_SumAsync_Double()
        {
            Assert.Equal(6.0, await CreateAsyncEnumerable([1.0, 2.0, 3.0]).SumAsync());
        }

        [Fact]
        public async Task AsyncEnumerable_SumAsync_Decimal()
        {
            Assert.Equal(6.0m, await CreateAsyncEnumerable([1.0m, 2.0m, 3.0m]).SumAsync());
        }

        [Fact]
        public async Task AsyncEnumerable_AverageAsync_Int()
        {
            Assert.Equal(2.0, await CreateAsyncEnumerable([1, 2, 3]).AverageAsync());
        }

        [Fact]
        public async Task AsyncEnumerable_AverageAsync_Long()
        {
            Assert.Equal(2.0, await CreateAsyncEnumerable([1L, 2L, 3L]).AverageAsync());
        }

        [Fact]
        public async Task AsyncEnumerable_AverageAsync_Float()
        {
            Assert.Equal(2.0f, await CreateAsyncEnumerable([1.0f, 2.0f, 3.0f]).AverageAsync());
        }

        [Fact]
        public async Task AsyncEnumerable_AverageAsync_Double()
        {
            Assert.Equal(2.0, await CreateAsyncEnumerable([1.0, 2.0, 3.0]).AverageAsync());
        }

        [Fact]
        public async Task AsyncEnumerable_AverageAsync_Decimal()
        {
            Assert.Equal(2.0m, await CreateAsyncEnumerable([1.0m, 2.0m, 3.0m]).AverageAsync());
        }

        [Fact]
        public async Task AsyncEnumerable_AggregateAsync()
        {
            Assert.Equal(6, await CreateAsyncEnumerable([1, 2, 3]).AggregateAsync((a, b) => a + b));
            Assert.Equal(10, await CreateAsyncEnumerable([1, 2, 3]).AggregateAsync(4, (a, b) => a + b));
            Assert.Equal("result:10", await CreateAsyncEnumerable([1, 2, 3]).AggregateAsync(4, (a, b) => a + b, result => $"result:{result}"));
        }

        [Fact]
        public async Task AsyncEnumerable_AggregateBy()
        {
            var result = await CreateAsyncEnumerable([1, 2, 2, 3, 3, 3]).AggregateBy(x => x, 0, (acc, x) => acc + x).ToListAsync();
            Assert.Equal(3, result.Count);
            Assert.Contains(new KeyValuePair<int, int>(1, 1), result);
            Assert.Contains(new KeyValuePair<int, int>(2, 4), result);
            Assert.Contains(new KeyValuePair<int, int>(3, 9), result);
        }

        [Fact]
        public async Task AsyncEnumerable_Index()
        {
            var result = await CreateAsyncEnumerable(["a", "b", "c"]).Index().ToListAsync();
            Assert.Equal((0, "a"), result[0]);
            Assert.Equal((1, "b"), result[1]);
            Assert.Equal((2, "c"), result[2]);
        }

        [Fact]
        public async Task AsyncEnumerable_ToAsyncEnumerable()
        {
            var result = await new[] { 1, 2, 3 }.ToAsyncEnumerable().ToListAsync();
            Assert.Equal([1, 2, 3], result);
        }

    private static async IAsyncEnumerable<T> CreateAsyncEnumerable<T>(T[] items)
    {
        foreach (var item in items)
        {
            await Task.Yield();
            yield return item;
        }
    }
}