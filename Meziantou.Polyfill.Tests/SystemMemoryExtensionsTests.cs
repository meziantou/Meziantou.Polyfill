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
}
