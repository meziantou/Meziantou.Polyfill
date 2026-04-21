using System;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public sealed class SystemArrayTests
{
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
