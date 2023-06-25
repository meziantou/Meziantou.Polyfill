using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public class UnitTest1
{
    [Fact]
    public void AttributesAreAvailables()
    {
        _ = new AllowNullAttribute();
        _ = new DisallowNullAttribute();
        _ = new DoesNotReturnAttribute();
        _ = new DoesNotReturnIfAttribute(true);
        _ = new DynamicallyAccessedMembersAttribute(DynamicallyAccessedMemberTypes.All);
        _ = new DynamicDependencyAttribute("");
        _ = new MaybeNullAttribute();
        _ = new MaybeNullWhenAttribute(true);
        _ = new MemberNotNullAttribute("");
        _ = new MemberNotNullWhenAttribute(false);
        _ = new NotNullAttribute();
        _ = new NotNullIfNotNullAttribute("");
        _ = new NotNullWhenAttribute(false);
        _ = new RequiresAssemblyFilesAttribute();
        _ = new RequiresDynamicCodeAttribute("");
        _ = new RequiresUnreferencedCodeAttribute("");
        _ = new SetsRequiredMembersAttribute();
        _ = new StringSyntaxAttribute("");
        _ = new UnconditionalSuppressMessageAttribute("", "");
        _ = new UnscopedRefAttribute();
        _ = new StackTraceHiddenAttribute();
        _ = new AsyncMethodBuilderAttribute(typeof(UnitTest1));
        _ = new CallerArgumentExpressionAttribute("");
        _ = new CompilerFeatureRequiredAttribute("");
        _ = new DisableRuntimeMarshallingAttribute();
        _ = new InterpolatedStringHandlerArgumentAttribute("");
        _ = new InterpolatedStringHandlerAttribute();
        _ = new ModuleInitializerAttribute();
        _ = new RequiredMemberAttribute();
        _ = new SkipLocalsInitAttribute();
        _ = new SuppressGCTransitionAttribute();
        _ = new UnmanagedCallersOnlyAttribute();
        _ = new ObsoletedOSPlatformAttribute("");
        _ = new RequiresPreviewFeaturesAttribute();
        _ = new SupportedOSPlatformAttribute("");
        _ = new SupportedOSPlatformGuardAttribute("");
        _ = new TargetPlatformAttribute("");
        _ = new UnsupportedOSPlatformAttribute("");
        _ = new UnsupportedOSPlatformGuardAttribute("");

        _ = typeof(IsExternalInit);
    }

    [Fact]
    public void System_Collections_Generic_KeyValuePair_2_Deconstruct()
    {
        var data = new KeyValuePair<string, object>("test", 2);
        var (key, value) = data;
        Assert.Equal("test", key);
        Assert.Equal(2, value);
    }

    [Fact]
    public void System_Collections_Generic_Queue_1_TryDequeue()
    {
        var queue = new Queue<int>();
        Assert.False(queue.TryDequeue(out _));

        queue.Enqueue(1);
        queue.Enqueue(2);
        Assert.True(queue.TryDequeue(out var item1));
        Assert.True(queue.TryDequeue(out var item2));
        Assert.False(queue.TryDequeue(out _));

        Assert.Equal(1, item1);
        Assert.Equal(2, item2);
    }

    [Fact]
    public void ReadOnlySpan_Contains()
    {
        ReadOnlySpan<int> span = new int[] { 1, 2 }.AsSpan();
        Assert.True(span.Contains(1));
        Assert.True(span.Contains(2));
        Assert.False(span.Contains(3));
    }

    [Fact]
    public void Span_Contains()
    {
        Span<int> span = new int[] { 1, 2 }.AsSpan();
        Assert.True(span.Contains(1));
        Assert.True(span.Contains(2));
        Assert.False(span.Contains(3));
    }

    [Fact]
    public void String_Contains_Char()
    {
        Assert.True("test".Contains('t'));
        Assert.False("test".Contains('T'));
    }

    [Fact]
    public void String_Contains()
    {
        Assert.True("test".Contains("tes", StringComparison.Ordinal));
        Assert.True("test".Contains("TEs", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void String_CopyTo()
    {
        Assert.Throws<ArgumentException>(() => "test".CopyTo(new char[1].AsSpan()));

        var data = new char[4];
        "test".CopyTo(data.AsSpan());
        Assert.Equal("test".ToCharArray(), data);
    }

    [Fact]
    public void String_EndsWith_Char()
    {
        Assert.True("test".EndsWith('t'));
        Assert.False("test".EndsWith('T'));
    }

    [Fact]
    public void String_GetHashCode()
    {
        Assert.Equal("test".GetHashCode(), "test".GetHashCode(StringComparison.Ordinal));
        Assert.Equal(StringComparer.OrdinalIgnoreCase.GetHashCode("Test"), "test".GetHashCode(StringComparison.OrdinalIgnoreCase));

    }

    [Fact]
    public void String_Replace()
    {
        Assert.Equal("sample", "samDumMyle2".Replace("dummy", "p", StringComparison.OrdinalIgnoreCase).Replace("2", "", StringComparison.Ordinal));
        Assert.Throws<ArgumentException>(() => "".Replace("", "dummy", StringComparison.OrdinalIgnoreCase));
        Assert.Throws<ArgumentNullException>(() => "".Replace(null!, "dummy", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void String_Split()
    {
        Assert.Equal(new[] { "a", "b", "c" }, "a;b;c".Split(';'));
        Assert.Equal(new[] { "a", " b", "c" }, "a; b;c".Split(';'));
        Assert.Equal(new[] { "a", "", "b", "c" }, "a;;b;c".Split(';', StringSplitOptions.None));
        Assert.Equal(new[] { "a", "b", "c" }, "a;;b;c".Split(';', StringSplitOptions.RemoveEmptyEntries));
        Assert.Equal(new[] { "a", " b;c" }, "a; b;c".Split(';', 2, StringSplitOptions.None));
    }

    [Fact]
    public void String_StartsWith_Char()
    {
        Assert.True("test".StartsWith('t'));
        Assert.False("test".StartsWith('T'));
    }

    [Fact]
    public void String_TryCopyTo()
    {
        Assert.False("test".TryCopyTo(new char[1].AsSpan()));

        var data = new char[4];
        Assert.True("test".TryCopyTo(data.AsSpan()));
        Assert.Equal("test".ToCharArray(), data);
    }

    [Fact]
    public void StringBuilder_Append_ReadonlySpan()
    {
        Assert.Equal("test", new StringBuilder().Append("test".AsSpan()).ToString());
    }

    [Fact]
    public void StringBuilder_Append_ReadonlyMemory()
    {
        Assert.Equal("test", new StringBuilder().Append("test".AsMemory()).ToString());
    }

    [Fact]
    public async Task CancellationTokenSource_CancelAsync()
    {
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();
        Assert.True(cts.Token.IsCancellationRequested);
    }

#if NET461_OR_GREATER
    [Fact]
    public async Task StreamWriter_WriteAsync()
    {
        using var sr = new System.IO.StringWriter();
        await sr.WriteAsync("test".AsMemory(), CancellationToken.None);
        Assert.Equal("test", sr.ToString());
    }
#endif

    [Fact]
    public async Task StreamReader_ReadToEndAsync()
    {
        using var sr = new System.IO.StringReader("test");
        var result = await sr.ReadToEndAsync(CancellationToken.None);
        Assert.Equal("test", result);
    }

#if NET461_OR_GREATER
    [Fact]
    public async Task StreamReader_ReadAsync()
    {
        using var sr = new System.IO.StringReader("test");
        var buffer = new char[2];
        var result = await sr.ReadAsync(buffer.AsMemory(), CancellationToken.None);
        Assert.Equal(2, result);
        Assert.Equal("te", new string(buffer));
    }
#endif

    [Fact]
    public void String_IndexOf_Char_StringComparison()
    {
        Assert.Equal(2, "test".IndexOf('S', StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void HashCode_Combine()
    {
        Assert.Equal(HashCode.Combine("foo", "bar"), HashCode.Combine("foo", "bar"));
        Assert.NotEqual(0, HashCode.Combine("foo", "bar"));
    }

    [Fact]
    public void Enumerable_Order()
    {
        Assert.Equal(new[] { 1, 2, 3 }, new[] { 2, 1, 3 }.Order().ToArray());
        Assert.Equal(new[] { 1, 2, 3 }, new[] { 2, 1, 3 }.Order(Comparer<int>.Default).ToArray());
    }

    [Fact]
    public void Enumerable_OrderDescending()
    {
        Assert.Equal(new[] { 3, 2, 1 }, new[] { 2, 1, 3 }.OrderDescending().ToArray());
        Assert.Equal(new[] { 3, 2, 1 }, new[] { 2, 1, 3 }.OrderDescending(Comparer<int>.Default).ToArray());
    }

    [Fact]
    public void Enumerable_DistinctBy()
    {
        Assert.Equal(new[] { 2, 1 }, new[] { 2, 1, 1 }.DistinctBy(_ => _).ToArray());
        Assert.Equal(new[] { 2, 1 }, new[] { 2, 1, 1 }.DistinctBy(_ => _, EqualityComparer<int>.Default).ToArray());
    }

    [Fact]
    public void String_ReplaceLineEndings()
    {
        Assert.Equal("\n\n\n", "\r\n\n\f".ReplaceLineEndings("\n"));
    }

    [Fact]
    public void String_ReplaceLineEndings2()
    {
        Assert.Equal("\r\n\r\n\r\n", "\r\n\n\f".ReplaceLineEndings("\r\n"));
    }

    [Fact]
    public void Enumerable_Zip()
    {
        Assert.Equal(new[] { ('a', 'b') }, "a".Zip("b"));
    }

    [Fact]
    public void ValueTuple()
    {
        Assert.Equal((1, 2, 3, 4, 5, 6, 7, 8, 9, 10), (1, 2, 3, 4, 5, 6, 7, 8, 9, 10));
    }
}
