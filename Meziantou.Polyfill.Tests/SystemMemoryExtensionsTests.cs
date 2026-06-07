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

    [Fact]
    public void CharSpan_NewMembers()
    {
        ReadOnlySpan<char> value = "  a,b ,,c  ";
        Span<Range> ranges = stackalloc Range[4];
        var count = value.Split(ranges, ',', (StringSplitOptions)2 | StringSplitOptions.RemoveEmptyEntries);

        Assert.Equal(3, count);
        Assert.Equal("a", value[ranges[0]].ToString());
        Assert.Equal("b", value[ranges[1]].ToString());
        Assert.Equal("c", value[ranges[2]].ToString());
        Assert.True("Hello".AsSpan().StartsWith("he".AsSpan(), StringComparison.OrdinalIgnoreCase));
        Assert.True("Hello".AsSpan().EndsWith("LO".AsSpan(), StringComparison.OrdinalIgnoreCase));

        var lines = new List<string>();
        foreach (var line in "a\r\nb\nc".AsSpan().EnumerateLines())
            lines.Add(line.ToString());
        Assert.Equal(["a", "b", "c"], lines);
    }

    [Fact]
    public void Span_Normalization()
    {
        ReadOnlySpan<char> value = "e\u0301";
        Assert.False(value.IsNormalized(NormalizationForm.FormC));
        Assert.Equal(1, value.GetNormalizedLength(NormalizationForm.FormC));
        Span<char> destination = stackalloc char[1];
        Assert.True(value.TryNormalize(destination, out var charsWritten, NormalizationForm.FormC));
        Assert.Equal(1, charsWritten);
        Assert.Equal("é", destination.ToString());
    }

    [Fact]
    public void WhiteSpaceMembers()
    {
        ReadOnlySpan<char> empty = [];
        Assert.False(empty.ContainsAnyWhiteSpace());
        Assert.Equal(-1, empty.IndexOfAnyWhiteSpace());
        Assert.Equal(-1, empty.IndexOfAnyExceptWhiteSpace());
        Assert.Equal(-1, empty.LastIndexOfAnyWhiteSpace());
        Assert.Equal(-1, empty.LastIndexOfAnyExceptWhiteSpace());

        ReadOnlySpan<char> value = "\u2003ab\t ";
        Assert.True(value.ContainsAnyWhiteSpace());
        Assert.Equal(0, value.IndexOfAnyWhiteSpace());
        Assert.Equal(1, value.IndexOfAnyExceptWhiteSpace());
        Assert.Equal(4, value.LastIndexOfAnyWhiteSpace());
        Assert.Equal(2, value.LastIndexOfAnyExceptWhiteSpace());

        Assert.False("abc".AsSpan().ContainsAnyWhiteSpace());
        Assert.Equal(-1, "abc".AsSpan().IndexOfAnyWhiteSpace());
        Assert.Equal(-1, " \t".AsSpan().IndexOfAnyExceptWhiteSpace());
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
    public void IndexOfAnyExcept_SearchValues_Char()
    {
        var values = System.Buffers.SearchValues.Create("he");
        Assert.Equal(4, "heeelloworld".AsSpan().IndexOfAnyExcept(values));
        Assert.Equal(-1, "eheeh".AsSpan().IndexOfAnyExcept(values));
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
