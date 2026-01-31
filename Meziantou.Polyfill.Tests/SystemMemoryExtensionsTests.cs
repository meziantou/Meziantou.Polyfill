using System;
using System.Buffers;
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

#if !NET8_0_OR_GREATER
    [Fact]
    public void IndexOfAny_ReadOnlySpan_SearchValues_T()
    {
        var values = SearchValues.Create([1, 3, 5]);
        
        Assert.Equal(0, ((ReadOnlySpan<int>)[1, 2, 3]).IndexOfAny(values));
        Assert.Equal(1, ((ReadOnlySpan<int>)[2, 3, 4]).IndexOfAny(values));
        Assert.Equal(2, ((ReadOnlySpan<int>)[2, 4, 5]).IndexOfAny(values));
        Assert.Equal(-1, ((ReadOnlySpan<int>)[2, 4, 6]).IndexOfAny(values));
        Assert.Equal(-1, ((ReadOnlySpan<int>)[]).IndexOfAny(values));
    }

    [Fact]
    public void IndexOfAny_Span_SearchValues_T()
    {
        var values = SearchValues.Create([1, 3, 5]);
        
        Assert.Equal(0, ((Span<int>)[1, 2, 3]).IndexOfAny(values));
        Assert.Equal(1, ((Span<int>)[2, 3, 4]).IndexOfAny(values));
        Assert.Equal(2, ((Span<int>)[2, 4, 5]).IndexOfAny(values));
        Assert.Equal(-1, ((Span<int>)[2, 4, 6]).IndexOfAny(values));
        Assert.Equal(-1, ((Span<int>)[]).IndexOfAny(values));
    }

    [Fact]
    public void IndexOfAny_ReadOnlySpan_SearchValues_String()
    {
        var values = SearchValues.Create(["hello", "world"], StringComparison.Ordinal);
        
        Assert.Equal(0, "hello there".AsSpan().IndexOfAny(values));
        Assert.Equal(0, "world peace".AsSpan().IndexOfAny(values));
        Assert.Equal(6, "see hello".AsSpan().IndexOfAny(values));
        Assert.Equal(-1, "goodbye".AsSpan().IndexOfAny(values));
        Assert.Equal(-1, "".AsSpan().IndexOfAny(values));
    }

    [Fact]
    public void IndexOfAny_Span_SearchValues_String()
    {
        var values = SearchValues.Create(["hello", "world"], StringComparison.Ordinal);
        char[] buffer = "hello there".ToCharArray();
        
        Assert.Equal(0, new Span<char>(buffer).IndexOfAny(values));
        
        buffer = "see hello".ToCharArray();
        Assert.Equal(6, new Span<char>(buffer).IndexOfAny(values));
        
        buffer = "goodbye".ToCharArray();
        Assert.Equal(-1, new Span<char>(buffer).IndexOfAny(values));
    }
#endif

}
