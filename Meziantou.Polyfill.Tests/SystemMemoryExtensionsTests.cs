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
    public void StartsWith_Value()
    {
        Assert.False(((ReadOnlySpan<int>)[]).StartsWith(0));
        Assert.True(((ReadOnlySpan<int>)[0, 1]).StartsWith(0));
        Assert.False(((ReadOnlySpan<int>)[0, 1]).StartsWith(1));
    }

    [Fact]
    public void StartsWith_Value_WithComparer()
    {
        Assert.False(((ReadOnlySpan<string>)[]).StartsWith("a", StringComparer.OrdinalIgnoreCase));
        Assert.True(((ReadOnlySpan<string>)["a", "b"]).StartsWith("A", StringComparer.OrdinalIgnoreCase));
        Assert.False(((ReadOnlySpan<string>)["a", "b"]).StartsWith("A", StringComparer.Ordinal));
    }

#if NET9_0_OR_GREATER
    [Fact]
    public void IndexOfAny_SearchValues_Char()
    {
        var values = System.Buffers.SearchValues.Create("ow");
        Assert.Equal(4, "helloworld".AsSpan().IndexOfAny(values));
        Assert.Equal(-1, "abcdef".AsSpan().IndexOfAny(values));
    }

    [Fact]
    public void IndexOfAny_SearchValues_String_Ordinal()
    {
        var values = System.Buffers.SearchValues.Create(["hello", "world"], StringComparison.Ordinal);
        Assert.Equal(2, "xxhello yy".AsSpan().IndexOfAny(values));
        Assert.Equal(8, "xxhEllo world".AsSpan().IndexOfAny(values));
        Assert.Equal(-1, "xxhEllo yy".AsSpan().IndexOfAny(values));
    }

    [Fact]
    public void IndexOfAny_SearchValues_String_OrdinalIgnoreCase()
    {
        var values = System.Buffers.SearchValues.Create(["hello", "world"], StringComparison.OrdinalIgnoreCase);
        Assert.Equal(2, "xxHeLlO yy".AsSpan().IndexOfAny(values));
        Assert.Equal(2, "xxWORLD yy".AsSpan().IndexOfAny(values));
    }
#endif
}
