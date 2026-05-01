using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
    public void Enumerable_Chunk()
    {
        var result = new[] { 1, 2, 3, 4, 5 }.Chunk(2).ToList();
        Assert.Equal(3, result.Count);
        Assert.Equal([1, 2], result[0]);
        Assert.Equal([3, 4], result[1]);
        Assert.Equal([5], result[2]);
    }

    [Fact]
    public void Enumerable_Chunk_SizeEqualToLength()
    {
        var result = new[] { 1, 2, 3 }.Chunk(3).ToList();
        Assert.Single(result);
        Assert.Equal([1, 2, 3], result[0]);
    }

    [Fact]
    public void Enumerable_Chunk_SizeLargerThanLength()
    {
        var result = new[] { 1, 2 }.Chunk(10).ToList();
        Assert.Single(result);
        Assert.Equal([1, 2], result[0]);
    }

    [Fact]
    public void Enumerable_Chunk_EmptySource()
    {
        var result = Array.Empty<int>().Chunk(2).ToList();
        Assert.Empty(result);
    }

    [Fact]
    public void Enumerable_Chunk_InvalidArguments()
    {
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<int>)null!).Chunk(2).ToList());
        Assert.Throws<ArgumentOutOfRangeException>(() => new[] { 1, 2 }.Chunk(0).ToList());
        Assert.Throws<ArgumentOutOfRangeException>(() => new[] { 1, 2 }.Chunk(-1).ToList());
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

    [Fact]
    public void Enumerable_LeftJoin()
    {
        var outer = new[]
        {
            new { Id = 1, Name = "a1" },
            new { Id = 1, Name = "a2" },
            new { Id = 2, Name = "b" },
        };

        var inner = new[]
        {
            new { Id = 1, Value = "x" },
            new { Id = 3, Value = "z" },
            new { Id = 1, Value = "y" },
        };

        var result = outer.LeftJoin(inner, o => o.Id, i => i.Id, (o, i) => (o.Name, Value: i?.Value ?? "none"));
        Assert.Equal([("a1", "x"), ("a1", "y"), ("a2", "x"), ("a2", "y"), ("b", "none")], result);
    }

    [Fact]
    public void Enumerable_LeftJoin_Comparer()
    {
        var result = new[] { "A", "b" }.LeftJoin(new[] { "a" }, outer => outer, inner => inner, (outer, inner) => (outer, Inner: inner ?? "none"), StringComparer.OrdinalIgnoreCase);
        Assert.Equal([("A", "a"), ("b", "none")], result);
    }

    [Fact]
    public void Enumerable_RightJoin()
    {
        var outer = new[]
        {
            new { Id = 1, Name = "a1" },
            new { Id = 1, Name = "a2" },
            new { Id = 2, Name = "b" },
        };

        var inner = new[]
        {
            new { Id = 1, Value = "x" },
            new { Id = 3, Value = "z" },
            new { Id = 1, Value = "y" },
        };

        var result = outer.RightJoin(inner, o => o.Id, i => i.Id, (o, i) => (Name: o?.Name ?? "none", i.Value));
        Assert.Equal([("a1", "x"), ("a2", "x"), ("none", "z"), ("a1", "y"), ("a2", "y")], result);
    }

    [Fact]
    public void Enumerable_RightJoin_Comparer()
    {
        var result = new[] { "a" }.RightJoin(new[] { "A", "b" }, outer => outer, inner => inner, (outer, inner) => (Outer: outer ?? "none", inner), StringComparer.OrdinalIgnoreCase);
        Assert.Equal([("a", "A"), ("none", "b")], result);
    }

    [Fact]
    public void Queryable_LeftJoin()
    {
        IQueryable<int> outer = new[] { 1, 2 }.AsQueryable();
        IEnumerable<int> inner = [1, 3];
        var comparer = EqualityComparer<int>.Default;

        var query = outer.LeftJoin(inner, o => o, i => i, (o, i) => new { o, i });
        var queryExpression = Assert.IsAssignableFrom<MethodCallExpression>(query.Expression);
        Assert.Equal("LeftJoin", queryExpression.Method.Name);
        Assert.Equal(5, queryExpression.Arguments.Count);

        var queryWithComparer = outer.LeftJoin(inner, o => o, i => i, (o, i) => new { o, i }, comparer);
        var queryWithComparerExpression = Assert.IsAssignableFrom<MethodCallExpression>(queryWithComparer.Expression);
        Assert.Equal("LeftJoin", queryWithComparerExpression.Method.Name);
        Assert.Equal(6, queryWithComparerExpression.Arguments.Count);
        var comparerExpression = Assert.IsAssignableFrom<ConstantExpression>(queryWithComparerExpression.Arguments[5]);
        Assert.Equal(typeof(IEqualityComparer<int>), comparerExpression.Type);
        Assert.Same(comparer, comparerExpression.Value);
    }

    [Fact]
    public void Queryable_RightJoin()
    {
        IQueryable<int> outer = new[] { 1, 2 }.AsQueryable();
        IEnumerable<int> inner = [1, 3];
        var comparer = EqualityComparer<int>.Default;

        var query = outer.RightJoin(inner, o => o, i => i, (o, i) => new { o, i });
        var queryExpression = Assert.IsAssignableFrom<MethodCallExpression>(query.Expression);
        Assert.Equal("RightJoin", queryExpression.Method.Name);
        Assert.Equal(5, queryExpression.Arguments.Count);

        var queryWithComparer = outer.RightJoin(inner, o => o, i => i, (o, i) => new { o, i }, comparer);
        var queryWithComparerExpression = Assert.IsAssignableFrom<MethodCallExpression>(queryWithComparer.Expression);
        Assert.Equal("RightJoin", queryWithComparerExpression.Method.Name);
        Assert.Equal(6, queryWithComparerExpression.Arguments.Count);
        var comparerExpression = Assert.IsAssignableFrom<ConstantExpression>(queryWithComparerExpression.Arguments[5]);
        Assert.Equal(typeof(IEqualityComparer<int>), comparerExpression.Type);
        Assert.Same(comparer, comparerExpression.Value);
    }

}
