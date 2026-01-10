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

public class SystemMemoryExtensionsTests
{
    [Fact]
    public void CommonPrefixLength()
    {
        Assert.Equal(0, ((Span<int>)[0]).CommonPrefixLength([1]));
        Assert.Equal(1, ((Span<int>)[0]).CommonPrefixLength([0]));
        Assert.Equal(2, ((Span<int>)[0, 1]).CommonPrefixLength([0, 1, 2]));
    }

    [Fact]
    public void Split_CharSeparator_Simple()
    {
        ReadOnlySpan<char> source = "a,b,c";
        Span<Range> ranges = stackalloc Range[10];
        
        var count = source.Split(ranges, ',');
        
        Assert.Equal(3, count);
        Assert.Equal("a", source[ranges[0]].ToString());
        Assert.Equal("b", source[ranges[1]].ToString());
        Assert.Equal("c", source[ranges[2]].ToString());
    }

    [Fact]
    public void Split_CharSeparator_RemoveEmptyEntries()
    {
        ReadOnlySpan<char> source = "a,,b,c";
        Span<Range> ranges = stackalloc Range[10];
        
        var count = source.Split(ranges, ',', StringSplitOptions.RemoveEmptyEntries);
        
        Assert.Equal(3, count);
        Assert.Equal("a", source[ranges[0]].ToString());
        Assert.Equal("b", source[ranges[1]].ToString());
        Assert.Equal("c", source[ranges[2]].ToString());
    }

    [Fact]
    public void Split_CharSeparator_LimitedDestination()
    {
        ReadOnlySpan<char> source = "a,b,c,d,e";
        Span<Range> ranges = stackalloc Range[3];
        
        var count = source.Split(ranges, ',');
        
        Assert.Equal(3, count);
        var first = source[ranges[0]].ToString();
        var second = source[ranges[1]].ToString();
        var third = source[ranges[2]].ToString();
        
        Assert.Equal("a", first);
        Assert.Equal("b", second);
        Assert.Equal("c", third);
    }

    [Fact]
    public void Split_StringSeparator_Simple()
    {
        ReadOnlySpan<char> source = "a::b::c";
        Span<Range> ranges = stackalloc Range[10];
        
        var count = source.Split(ranges, "::");
        
        Assert.Equal(3, count);
        Assert.Equal("a", source[ranges[0]].ToString());
        Assert.Equal("b", source[ranges[1]].ToString());
        Assert.Equal("c", source[ranges[2]].ToString());
    }

    [Fact]
    public void Split_StringSeparator_RemoveEmptyEntries()
    {
        ReadOnlySpan<char> source = "a::::b::c";
        Span<Range> ranges = stackalloc Range[10];
        
        var count = source.Split(ranges, "::", StringSplitOptions.RemoveEmptyEntries);
        
        Assert.Equal(3, count);
        Assert.Equal("a", source[ranges[0]].ToString());
        Assert.Equal("b", source[ranges[1]].ToString());
        Assert.Equal("c", source[ranges[2]].ToString());
    }

    [Fact]
    public void Split_StringSeparator_EmptySeparatorThrows()
    {
        // Note: Cannot use Assert.Throws<T> here because ReadOnlySpan and Span are ref structs
        // that cannot be captured in lambda expressions
        try
        {
            ReadOnlySpan<char> source = "a b c";
            Span<Range> ranges = stackalloc Range[10];
            _ = source.Split(ranges, "");
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException ex)
        {
            Assert.Equal("separator", ex.ParamName);
        }
    }

    [Fact]
    public void Split_Generic_CharSeparator_Simple()
    {
        ReadOnlySpan<char> source = "a,b,c";
        var count = 0;
        foreach (var range in source.Split(','))
        {
            var segment = source[range].ToString();
            count++;
            Assert.True(segment == "a" || segment == "b" || segment == "c");
        }
        Assert.Equal(3, count);
    }

    [Fact]
    public void Split_Generic_StringSeparator_Simple()
    {
        ReadOnlySpan<char> source = "a::b::c";
        var count = 0;
        foreach (var range in source.Split("::".AsSpan()))
        {
            var segment = source[range].ToString();
            count++;
            Assert.True(segment == "a" || segment == "b" || segment == "c");
        }
        Assert.Equal(3, count);
    }

    [Fact]
    public void Split_Generic_IntArray()
    {
        ReadOnlySpan<int> source = stackalloc int[] { 1, 0, 2, 0, 3 };
        var count = 0;
        foreach (var range in source.Split(0))
        {
            var segment = source[range];
            count++;
            Assert.True(segment.Length == 1);
            Assert.True(segment[0] == 1 || segment[0] == 2 || segment[0] == 3);
        }
        Assert.Equal(3, count);
    }
}

