using System;
using System.Collections.Generic;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public sealed class SystemArrayTests
{
    [Fact]
    public void ArraySegment_CopyTo_Array()
    {
        var source = new ArraySegment<int>([1, 2, 3, 4], 1, 2);
        var destination = new int[3];

        source.CopyTo(destination);

        Assert.Equal([2, 3, 0], destination);
    }

    [Fact]
    public void ArraySegment_CopyTo_ArrayWithDestinationIndex()
    {
        var source = new ArraySegment<int>([1, 2, 3, 4], 1, 2);
        var destination = new int[4];

        source.CopyTo(destination, 1);

        Assert.Equal([0, 2, 3, 0], destination);
    }

    [Fact]
    public void ArraySegment_CopyTo_ArraySegment()
    {
        var source = new ArraySegment<int>([1, 2, 3, 4], 1, 2);
        var destinationArray = new[] { 0, 0, 0, 0 };
        var destination = new ArraySegment<int>(destinationArray, 1, 2);

        source.CopyTo(destination);

        Assert.Equal([0, 2, 3, 0], destinationArray);
    }

    [Fact]
    public void ArraySegment_CopyTo_DefaultSource_Throws()
    {
        var source = default(ArraySegment<int>);

        Assert.Throws<InvalidOperationException>(() => source.CopyTo(Array.Empty<int>()));
        Assert.Throws<InvalidOperationException>(() => source.CopyTo(Array.Empty<int>(), 0));
        Assert.Throws<InvalidOperationException>(() => source.CopyTo(new ArraySegment<int>(Array.Empty<int>())));
    }

    [Fact]
    public void ArraySegment_CopyTo_DefaultDestinationSegment_Throws()
    {
        var source = new ArraySegment<int>([]);

        Assert.Throws<InvalidOperationException>(() => source.CopyTo(default(ArraySegment<int>)));
    }

    [Fact]
    public void ArraySegment_CopyTo_DestinationSegmentTooShort_Throws()
    {
        var source = new ArraySegment<int>([1, 2], 0, 2);
        var destination = new ArraySegment<int>([0], 0, 1);

        Assert.Throws<ArgumentException>(() => source.CopyTo(destination));
    }

    [Fact]
    public void ArraySegment_GetEnumerator()
    {
        var segment = new ArraySegment<int>([1, 2, 3, 4], 1, 2);

        var enumerator = segment.GetEnumerator();

        Assert.Throws<InvalidOperationException>(() => enumerator.Current);
        Assert.True(enumerator.MoveNext());
        Assert.Equal(2, enumerator.Current);
        Assert.True(enumerator.MoveNext());
        Assert.Equal(3, enumerator.Current);
        Assert.False(enumerator.MoveNext());
        Assert.Throws<InvalidOperationException>(() => enumerator.Current);
    }

    [Fact]
    public void ArraySegment_GetEnumerator_DefaultSource_Throws()
    {
        var source = default(ArraySegment<int>);

        Assert.Throws<InvalidOperationException>(() => source.GetEnumerator());
    }

    [Fact]
    public void ArraySegment_Foreach_UsesEnumerator()
    {
        var source = new ArraySegment<int>([1, 2, 3, 4], 1, 2);
        var result = new List<int>();

        foreach (var item in source)
        {
            result.Add(item);
        }

        Assert.Equal([2, 3], result);
    }

    [Fact]
    public void Fill_AllElements()
    {
        var values = new[] { 1, 2, 3, 4 };
        Array.Fill(values, 42);

        Assert.Equal(new[] { 42, 42, 42, 42 }, values);
    }

    [Fact]
    public void Fill_Range()
    {
        var values = new[] { 1, 2, 3, 4, 5 };
        Array.Fill(values, 9, 1, 3);

        Assert.Equal(new[] { 1, 9, 9, 9, 5 }, values);
    }

    [Fact]
    public void Fill_NullArray_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => Array.Fill<int>(null!, 1));
    }

    [Fact]
    public void Fill_Range_NullArray_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => Array.Fill<int>(null!, 1, 0, 0));
    }

    [Fact]
    public void Fill_Range_StartIndexNegative_Throws()
    {
        var values = new[] { 1, 2, 3 };
        Assert.Throws<ArgumentOutOfRangeException>(() => Array.Fill(values, 0, -1, 1));
    }

    [Fact]
    public void Fill_Range_StartIndexTooLarge_Throws()
    {
        var values = new[] { 1, 2, 3 };
        Assert.Throws<ArgumentOutOfRangeException>(() => Array.Fill(values, 0, 4, 0));
    }

    [Fact]
    public void Fill_Range_CountNegative_Throws()
    {
        var values = new[] { 1, 2, 3 };
        Assert.Throws<ArgumentOutOfRangeException>(() => Array.Fill(values, 0, 0, -1));
    }

    [Fact]
    public void Fill_Range_CountTooLarge_Throws()
    {
        var values = new[] { 1, 2, 3 };
        Assert.Throws<ArgumentOutOfRangeException>(() => Array.Fill(values, 0, 1, 3));
    }

    [Fact]
    public void Fill_Range_EmptySegmentAtEnd_DoesNotThrow()
    {
        var values = new[] { 1, 2, 3 };
        Array.Fill(values, 0, 3, 0);

        Assert.Equal(new[] { 1, 2, 3 }, values);
    }
}
