using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
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

public class SystemTests
{
    [Fact]
    public void ReadOnlySpan_Contains()
    {
        ReadOnlySpan<int> span = [1, 2];
        Assert.True(span.Contains(1));
        Assert.True(span.Contains(2));
        Assert.False(span.Contains(3));
    }

    [Fact]
    public void Span_Contains()
    {
        Span<int> span = [1, 2];
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
    public void String_Contains_Char_StringComparison()
    {
        Assert.True("test".Contains('t', StringComparison.Ordinal));
        Assert.True("test".Contains('T', StringComparison.OrdinalIgnoreCase));
        Assert.False("test".Contains('T', StringComparison.Ordinal));
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
        Assert.Equal<string[]>(["a", "b", "c"], "a;b;c".Split(';'));
        Assert.Equal<string[]>(["a", " b", "c"], "a; b;c".Split(';'));
        Assert.Equal<string[]>(["a", "", "b", "c"], "a;;b;c".Split(';', StringSplitOptions.None));
        Assert.Equal<string[]>(["a", "b", "c"], "a;;b;c".Split(';', StringSplitOptions.RemoveEmptyEntries));
        Assert.Equal<string[]>(["a", " b;c"], "a; b;c".Split(';', 2, StringSplitOptions.None));
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
    public void ValueTuple()
    {
        Assert.Equal((1, 2, 3, 4, 5, 6, 7, 8, 9, 10), (1, 2, 3, 4, 5, 6, 7, 8, 9, 10));
    }

    [Fact]
    public void String_AsSpan()
    {
        var actual = "test".AsSpan(1, 2);
        Assert.Equal("es", actual.ToString());
    }

    [Fact]
    public void Span_ContainsAny_2()
    {
        Span<int> span = [0, 1, 2];
        Assert.False(span.ContainsAny(10, 11));
        Assert.True(span.ContainsAny(10, 1));
        Assert.True(span.ContainsAny(1, 10));
    }

    [Fact]
    public void Span_ContainsAny_3()
    {
        Span<int> span = [0, 1, 2];
        Assert.False(span.ContainsAny(10, 11, 12));
        Assert.True(span.ContainsAny(1, 10, 11));
        Assert.True(span.ContainsAny(10, 1, 11));
        Assert.True(span.ContainsAny(10, 11, 1));
    }

    [Fact]
    public void Span_ContainsAny_ReadOnlySpan()
    {
        Span<int> span = [0, 1, 2];

        ReadOnlySpan<int> search = [10, 11, 12];
        Assert.False(span.ContainsAny(search));

        search = [1, 11, 12];
        Assert.True(span.ContainsAny(search));

        search = [10, 1, 12];
        Assert.True(span.ContainsAny(search));

        search = [10, 11, 1];
        Assert.True(span.ContainsAny(search));

        search = [10, 11, 12, 1];
        Assert.True(span.ContainsAny(search));
    }

    [Fact]
    public void ReadOnlySpan_ContainsAnyExcept_1()
    {
        Assert.False(((ReadOnlySpan<int>)[0]).ContainsAnyExcept(0));
        Assert.True(((ReadOnlySpan<int>)[0]).ContainsAnyExcept(1));
        Assert.True(((ReadOnlySpan<int>)[0, 1]).ContainsAnyExcept(0));
    }

    [Fact]
    public void ReadOnlySpan_ContainsAnyExcept_2()
    {
        Assert.False(((ReadOnlySpan<int>)[0]).ContainsAnyExcept(0, 1));
        Assert.False(((ReadOnlySpan<int>)[0, 1]).ContainsAnyExcept(0, 1));
        Assert.True(((ReadOnlySpan<int>)[0, 1, 2]).ContainsAnyExcept(0, 1));
    }

    [Fact]
    public void ReadOnlySpan_ContainsAnyExcept_3()
    {
        Assert.False(((ReadOnlySpan<int>)[0]).ContainsAnyExcept(0, 1, 2));
        Assert.False(((ReadOnlySpan<int>)[0, 1]).ContainsAnyExcept(0, 1, 2));
        Assert.False(((ReadOnlySpan<int>)[0, 1, 2]).ContainsAnyExcept(0, 1, 2));
        Assert.True(((ReadOnlySpan<int>)[0, 1, 2, 3]).ContainsAnyExcept(0, 1, 2));
    }

    [Fact]
    public void ReadOnlySpan_ContainsAnyExcept_ReadOnlySpan()
    {
        Assert.False(((ReadOnlySpan<int>)[0]).ContainsAnyExcept([0]));
        Assert.False(((ReadOnlySpan<int>)[0, 1]).ContainsAnyExcept([0, 1, 2]));
        Assert.True(((ReadOnlySpan<int>)[0, 1]).ContainsAnyExcept([0]));
    }

    [Fact]
    public void Span_ContainsAnyExcept_1()
    {
        Assert.False(((Span<int>)[0]).ContainsAnyExcept(0));
        Assert.True(((Span<int>)[0]).ContainsAnyExcept(1));
        Assert.True(((Span<int>)[0, 1]).ContainsAnyExcept(0));
    }

    [Fact]
    public void Span_ContainsAnyExcept_2()
    {
        Assert.False(((Span<int>)[0]).ContainsAnyExcept(0, 1));
        Assert.False(((Span<int>)[0, 1]).ContainsAnyExcept(0, 1));
        Assert.True(((Span<int>)[0, 1, 2]).ContainsAnyExcept(0, 1));
    }

    [Fact]
    public void Span_ContainsAnyExcept_3()
    {
        Assert.False(((Span<int>)[0]).ContainsAnyExcept(0, 1, 2));
        Assert.False(((Span<int>)[0, 1]).ContainsAnyExcept(0, 1, 2));
        Assert.False(((Span<int>)[0, 1, 2]).ContainsAnyExcept(0, 1, 2));
        Assert.True(((Span<int>)[0, 1, 2, 3]).ContainsAnyExcept(0, 1, 2));
    }

    [Fact]
    public void Span_ContainsAnyExcept_ReadOnlySpan()
    {
        Assert.False(((Span<int>)[0]).ContainsAnyExcept([0]));
        Assert.False(((Span<int>)[0, 1]).ContainsAnyExcept([0, 1, 2]));
        Assert.True(((Span<int>)[0, 1]).ContainsAnyExcept([0]));
    }

    [Fact]
    public void TimeSpan_Multiply()
    {
        Assert.Equal(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(1).Multiply(2));
    }

    [Fact]
    public void TimeSpan_Multiply_Double_TimeSpan()
    {
        // Test basic multiplication: 2.0 * 1 second = 2 seconds
        Assert.Equal(TimeSpan.FromSeconds(2), 2.0 * TimeSpan.FromSeconds(1));

        // Test fractional multiplication: 0.5 * 10 seconds = 5 seconds
        Assert.Equal(TimeSpan.FromSeconds(5), 0.5 * TimeSpan.FromSeconds(10));

        // Test negative multiplication: -2.0 * 3 seconds = -6 seconds
        Assert.Equal(TimeSpan.FromSeconds(-6), -2.0 * TimeSpan.FromSeconds(3));

        // Test zero multiplication
        Assert.Equal(TimeSpan.Zero, 0.0 * TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void TimeSpan_Multiply_TimeSpan_Double()
    {
        // Test basic multiplication: 1 second * 2.0 = 2 seconds
        Assert.Equal(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(1) * 2.0);

        // Test fractional multiplication: 10 seconds * 0.5 = 5 seconds
        Assert.Equal(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10) * 0.5);

        // Test negative multiplication: 3 seconds * -2.0 = -6 seconds
        Assert.Equal(TimeSpan.FromSeconds(-6), TimeSpan.FromSeconds(3) * -2.0);

        // Test zero multiplication
        Assert.Equal(TimeSpan.Zero, TimeSpan.FromSeconds(5) * 0.0);
    }

    [Fact]
    public void TimeSpan_Division_Double()
    {
        // Test basic division: 10 seconds / 2.0 = 5 seconds
        Assert.Equal(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10) / 2.0);

        // Test fractional division: 10 seconds / 0.5 = 20 seconds
        Assert.Equal(TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(10) / 0.5);

        // Test negative division: 6 seconds / -2.0 = -3 seconds
        Assert.Equal(TimeSpan.FromSeconds(-3), TimeSpan.FromSeconds(6) / -2.0);

        // Test large values: 1 hour / 60 = 1 minute
        Assert.Equal(TimeSpan.FromMinutes(1), TimeSpan.FromHours(1) / 60.0);
    }

    [Fact]
    public void Environment_ProcessId()
    {
        Assert.Equal(Process.GetCurrentProcess().Id, Environment.ProcessId);
    }

    [Fact]
    public void String_Join_Char()
    {
        Assert.Equal("a,b,c", string.Join(',', (object[])["a", "b", "c"]));
        Assert.Equal("a,b,c", string.Join(',', (string[])["a", "b", "c"]));
        Assert.Equal("a,b,c", string.Join(',', (ReadOnlySpan<object>)["a", "b", "c"]));
        Assert.Equal("a,b,c", string.Join(',', (ReadOnlySpan<string>)["a", "b", "c"]));
        Assert.Equal("a,b,c", string.Join(',', (IEnumerable<string>)["a", "b", "c"]));
    }

    [Fact]
    public void ArgumentNullException_ThrowIfNull()
    {
        object? sample = null;
        var ex = Assert.Throws<ArgumentNullException>(() => ArgumentNullException.ThrowIfNull(sample));
        Assert.Equal("sample", ex.ParamName);

        object? argument = new();
        ArgumentNullException.ThrowIfNull(argument);
    }

    [Fact]
    public unsafe void ArgumentNullException_ThrowIfNull_Pointer()
    {
        void* sample = null;
        var ex = Assert.Throws<ArgumentNullException>(() => ArgumentNullException.ThrowIfNull(sample));
        Assert.Equal("sample", ex.ParamName);

        nint argument = 1;
        ArgumentNullException.ThrowIfNull(argument);
    }

    [Fact]
    public unsafe void ArgumentException_ThrowIfNullOrEmpty()
    {
        var sample = "";
        var ex = Assert.Throws<ArgumentException>(() => ArgumentException.ThrowIfNullOrEmpty(sample));
        Assert.Equal("sample", ex.ParamName);

        var argument = "a";
        ArgumentException.ThrowIfNullOrEmpty(argument);
    }

    [Fact]
    public unsafe void ArgumentException_ThrowIfNullOrWhiteSpace()
    {
        var sample = "  ";
        var ex = Assert.Throws<ArgumentException>(() => ArgumentException.ThrowIfNullOrWhiteSpace(sample));
        Assert.Equal("sample", ex.ParamName);

        var argument = "a";
        ArgumentException.ThrowIfNullOrWhiteSpace(argument);
    }

    [Fact]
    public unsafe void ObjectDisposedException_ThrowIf()
    {
        Assert.Throws<ObjectDisposedException>(() => ObjectDisposedException.ThrowIf(true, new object()));
        Assert.Throws<ObjectDisposedException>(() => ObjectDisposedException.ThrowIf(true, typeof(object)));

        ObjectDisposedException.ThrowIf(false, new object());
        ObjectDisposedException.ThrowIf(false, typeof(object));
    }

    [Fact]
    public void ArgumentOutOfRangeException_ThrowIfEqual()
    {
        var sample = 5;
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentOutOfRangeException.ThrowIfEqual(sample, 5));
        Assert.Equal("sample", ex.ParamName);

        var value = 5;
        var other = 10;
        ArgumentOutOfRangeException.ThrowIfEqual(value, other);
        var value2 = "a";
        var other2 = "b";
        ArgumentOutOfRangeException.ThrowIfEqual(value2, other2);
    }

    [Fact]
    public void ArgumentOutOfRangeException_ThrowIfNotEqual()
    {
        var sample = 5;
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentOutOfRangeException.ThrowIfNotEqual(sample, 10));
        Assert.Equal("sample", ex.ParamName);

        var value = 5;
        var other = 5;
        ArgumentOutOfRangeException.ThrowIfNotEqual(value, other);
        var value2 = "a";
        var other2 = "a";
        ArgumentOutOfRangeException.ThrowIfNotEqual(value2, other2);
    }

    [Fact]
    public void ArgumentOutOfRangeException_ThrowIfGreaterThan()
    {
        var sample = 10;
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentOutOfRangeException.ThrowIfGreaterThan(sample, 5));
        Assert.Equal("sample", ex.ParamName);

        var value = 5;
        var other = 10;
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value, other);
        var value2 = 5;
        var other2 = 5;
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value2, other2);
    }

    [Fact]
    public void ArgumentOutOfRangeException_ThrowIfGreaterThanOrEqual()
    {
        var sample = 10;
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(sample, 5));
        Assert.Equal("sample", ex.ParamName);

        sample = 5;
        ex = Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(sample, 5));
        Assert.Equal("sample", ex.ParamName);

        var value = 5;
        var other = 10;
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, other);
    }

    [Fact]
    public void ArgumentOutOfRangeException_ThrowIfLessThan()
    {
        var sample = 5;
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentOutOfRangeException.ThrowIfLessThan(sample, 10));
        Assert.Equal("sample", ex.ParamName);

        var value = 10;
        var other = 5;
        ArgumentOutOfRangeException.ThrowIfLessThan(value, other);
        var value2 = 5;
        var other2 = 5;
        ArgumentOutOfRangeException.ThrowIfLessThan(value2, other2);
    }

    [Fact]
    public void ArgumentOutOfRangeException_ThrowIfLessThanOrEqual()
    {
        var sample = 5;
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(sample, 10));
        Assert.Equal("sample", ex.ParamName);

        sample = 5;
        ex = Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(sample, 5));
        Assert.Equal("sample", ex.ParamName);

        var value = 10;
        var other = 5;
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, other);
    }

    private static void ArgumentOutOfRangeException_ThrowIfNegative_Invocation(object sample)
    {
        switch (sample)
        {
            case char n:
                ArgumentOutOfRangeException.ThrowIfNegative(n);
                break;
            case sbyte n:
                ArgumentOutOfRangeException.ThrowIfNegative(n);
                break;
            case byte n:
                ArgumentOutOfRangeException.ThrowIfNegative(n);
                break;
            case short n:
                ArgumentOutOfRangeException.ThrowIfNegative(n);
                break;
            case ushort n:
                ArgumentOutOfRangeException.ThrowIfNegative(n);
                break;
            case int n:
                ArgumentOutOfRangeException.ThrowIfNegative(n);
                break;
            case uint n:
                ArgumentOutOfRangeException.ThrowIfNegative(n);
                break;
            case long n:
                ArgumentOutOfRangeException.ThrowIfNegative(n);
                break;
            case ulong n:
                ArgumentOutOfRangeException.ThrowIfNegative(n);
                break;
#if NET5_0_OR_GREATER
            case System.Half n:
                ArgumentOutOfRangeException.ThrowIfNegative(n);
                break;
#endif
            case float n:
                ArgumentOutOfRangeException.ThrowIfNegative(n);
                break;
            case double n:
                ArgumentOutOfRangeException.ThrowIfNegative(n);
                break;
            case decimal n:
                ArgumentOutOfRangeException.ThrowIfNegative(n);
                break;
        }
    }

#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable MA0069 // Non-constant static fields should not be visible
    public static TheoryData<object> ArgumentOutOfRangeException_ThrowIfNegative_ValidInputData =
#pragma warning restore MA0069 // Non-constant static fields should not be visible
#pragma warning restore CA2211 // Non-constant fields should not be visible
        new TheoryData<object>
        {
            (char)0,
            char.MaxValue,
            (sbyte)0,
            sbyte.MaxValue,
            (byte)0,
            byte.MaxValue,
            (short)0,
            short.MaxValue,
            (ushort)0,
            ushort.MaxValue,
            (int)0,
            int.MaxValue,
            (uint)0,
            uint.MaxValue,
            (long)0,
            long.MaxValue,
            (ulong)0,
            ulong.MaxValue,
#if NET5_0_OR_GREATER
            (System.Half)0,
            System.Half.MaxValue,
#endif
            (float)0F,
            float.MaxValue,
            (double)0F,
            double.MaxValue,
            (decimal)0F,
            decimal.MaxValue,
        };

    [Theory]
    [MemberData(nameof(ArgumentOutOfRangeException_ThrowIfNegative_ValidInputData))]
    public void ArgumentOutOfRangeException_ThrowIfNegative_WithValidInput(object sample)
    {
        ArgumentOutOfRangeException_ThrowIfNegative_Invocation(sample);
    }

#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable MA0069 // Non-constant static fields should not be visible
    public static TheoryData<object> ArgumentOutOfRangeException_ThrowIfNegative_InvalidInputData =
#pragma warning restore MA0069 // Non-constant static fields should not be visible
#pragma warning restore CA2211 // Non-constant fields should not be visible
        new TheoryData<object>
        {
            sbyte.MinValue,
            short.MinValue,
            int.MinValue,
            long.MinValue,
#if NET5_0_OR_GREATER
            System.Half.MinValue,
#endif
            float.MinValue,
            double.MinValue,
            decimal.MinValue,
        };

    [Theory]
    [MemberData(nameof(ArgumentOutOfRangeException_ThrowIfNegative_InvalidInputData))]
    public void ArgumentOutOfRangeException_ThrowIfNegative_WithInvalidInput(object sample)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentOutOfRangeException_ThrowIfNegative_Invocation(sample));
        Assert.Equal(sample, ex.ActualValue);
        Assert.Equal("n", ex.ParamName);
    }

    private static void ArgumentOutOfRangeException_ThrowIfNegativeOrZero_Invocation(object sample)
    {
        switch (sample)
        {
            case char n:
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
                break;
            case sbyte n:
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
                break;
            case byte n:
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
                break;
            case short n:
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
                break;
            case ushort n:
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
                break;
            case int n:
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
                break;
            case uint n:
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
                break;
            case long n:
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
                break;
            case ulong n:
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
                break;
#if NET5_0_OR_GREATER
            case System.Half n:
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
                break;
#endif
            case float n:
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
                break;
            case double n:
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
                break;
            case decimal n:
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
                break;
        }
    }

#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable MA0069 // Non-constant static fields should not be visible
    public static TheoryData<object> ArgumentOutOfRangeException_ThrowIfNegativeOrZero_ValidInputData =
#pragma warning restore MA0069 // Non-constant static fields should not be visible
#pragma warning restore CA2211 // Non-constant fields should not be visible
        new TheoryData<object>
        {
            char.MaxValue,
            sbyte.MaxValue,
            byte.MaxValue,
            short.MaxValue,
            ushort.MaxValue,
            int.MaxValue,
            uint.MaxValue,
            long.MaxValue,
            ulong.MaxValue,
#if NET5_0_OR_GREATER
            System.Half.MaxValue,
#endif
            float.MaxValue,
            double.MaxValue,
            decimal.MaxValue,
        };

    [Theory]
    [MemberData(nameof(ArgumentOutOfRangeException_ThrowIfNegativeOrZero_ValidInputData))]
    public void ArgumentOutOfRangeException_ThrowIfNegativeOrZero_WithValidInput(object sample)
    {
        ArgumentOutOfRangeException_ThrowIfNegativeOrZero_Invocation(sample);
    }

#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable MA0069 // Non-constant static fields should not be visible
    public static TheoryData<object> ArgumentOutOfRangeException_ThrowIfNegativeOrZero_InvalidInputData =
#pragma warning restore MA0069 // Non-constant static fields should not be visible
#pragma warning restore CA2211 // Non-constant fields should not be visible
        new TheoryData<object>
        {
            (char)0,
            (sbyte)0,
            sbyte.MinValue,
            (byte)0,
            (short)0,
            short.MinValue,
            (ushort)0,
            (int)0,
            int.MinValue,
            (uint)0,
            (long)0,
            long.MinValue,
            (ulong)0,
#if NET5_0_OR_GREATER
            (System.Half)0,
            System.Half.MinValue,
#endif
            (float)0F,
            float.MinValue,
            (double)0F,
            double.MinValue,
            (decimal)0F,
            decimal.MinValue,
        };

    [Theory]
    [MemberData(nameof(ArgumentOutOfRangeException_ThrowIfNegativeOrZero_InvalidInputData))]
    public void ArgumentOutOfRangeException_ThrowIfNegativeOrZero_WithInvalidInput(object sample)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentOutOfRangeException_ThrowIfNegativeOrZero_Invocation(sample));
        Assert.Equal(sample, ex.ActualValue);
        Assert.Equal("n", ex.ParamName);
    }

    private static void ArgumentOutOfRangeException_ThrowIfZero_Invocation(object sample)
    {
        switch (sample)
        {
            case char n:
                ArgumentOutOfRangeException.ThrowIfZero(n);
                break;
            case sbyte n:
                ArgumentOutOfRangeException.ThrowIfZero(n);
                break;
            case byte n:
                ArgumentOutOfRangeException.ThrowIfZero(n);
                break;
            case short n:
                ArgumentOutOfRangeException.ThrowIfZero(n);
                break;
            case ushort n:
                ArgumentOutOfRangeException.ThrowIfZero(n);
                break;
            case int n:
                ArgumentOutOfRangeException.ThrowIfZero(n);
                break;
            case uint n:
                ArgumentOutOfRangeException.ThrowIfZero(n);
                break;
            case long n:
                ArgumentOutOfRangeException.ThrowIfZero(n);
                break;
            case ulong n:
                ArgumentOutOfRangeException.ThrowIfZero(n);
                break;
#if NET5_0_OR_GREATER
            case System.Half n:
                ArgumentOutOfRangeException.ThrowIfZero(n);
                break;
#endif
            case float n:
                ArgumentOutOfRangeException.ThrowIfZero(n);
                break;
            case double n:
                ArgumentOutOfRangeException.ThrowIfZero(n);
                break;
            case decimal n:
                ArgumentOutOfRangeException.ThrowIfZero(n);
                break;
        }
    }

#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable MA0069 // Non-constant static fields should not be visible
    public static TheoryData<object> ArgumentOutOfRangeException_ThrowIfZero_ValidInputData =
#pragma warning restore MA0069 // Non-constant static fields should not be visible
#pragma warning restore CA2211 // Non-constant fields should not be visible
        new TheoryData<object>
        {
            char.MaxValue,
            sbyte.MinValue,
            sbyte.MaxValue,
            byte.MaxValue,
            short.MinValue,
            short.MaxValue,
            ushort.MaxValue,
            int.MinValue,
            int.MaxValue,
            uint.MaxValue,
            long.MinValue,
            long.MaxValue,
            ulong.MaxValue,
#if NET5_0_OR_GREATER
            System.Half.MinValue,
            System.Half.MaxValue,
#endif
            float.MinValue,
            float.MaxValue,
            double.MinValue,
            double.MaxValue,
            decimal.MinValue,
            decimal.MaxValue,
        };

    [Theory]
    [MemberData(nameof(ArgumentOutOfRangeException_ThrowIfZero_ValidInputData))]
    public void ArgumentOutOfRangeException_ThrowIfZero_WithValidInput(object sample)
    {
        ArgumentOutOfRangeException_ThrowIfZero_Invocation(sample);
    }

#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable MA0069 // Non-constant static fields should not be visible
    public static TheoryData<object> ArgumentOutOfRangeException_ThrowIfZero_InvalidInputData =
#pragma warning restore MA0069 // Non-constant static fields should not be visible
#pragma warning restore CA2211 // Non-constant fields should not be visible
        new TheoryData<object>
        {
            (char)0,
            (sbyte)0,
            (byte)0,
            (short)0,
            (ushort)0,
            (int)0,
            (uint)0,
            (long)0,
            (ulong)0,
#if NET5_0_OR_GREATER
            (System.Half)0,
#endif
            (float)0F,
            (double)0F,
            (decimal)0F,
        };

    [Theory]
    [MemberData(nameof(ArgumentOutOfRangeException_ThrowIfZero_InvalidInputData))]
    public void ArgumentOutOfRangeException_ThrowIfZero_WithInvalidInput(object sample)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentOutOfRangeException_ThrowIfZero_Invocation(sample));
        Assert.Equal(sample, ex.ActualValue);
        Assert.Equal("n", ex.ParamName);
    }

#if !NET5_0_OR_GREATER
#pragma warning disable MA0096 // A class that implements IComparable<T> should also implement IEquatable<T>
#pragma warning disable MA0097 // A class that implements IComparable<T> or IComparable should override comparison operators
    private struct ArgumentOutOfRangeException_ThrowIf_InvalidInputType : IComparable<ArgumentOutOfRangeException_ThrowIf_InvalidInputType>
#pragma warning restore MA0097 // A class that implements IComparable<T> or IComparable should override comparison operators
#pragma warning restore MA0096 // A class that implements IComparable<T> should also implement IEquatable<T>
    {
#pragma warning disable MA0025 // Implement the functionality instead of throwing NotImplementedException
        public int CompareTo(ArgumentOutOfRangeException_ThrowIf_InvalidInputType other) => throw new NotImplementedException();
#pragma warning restore MA0025 // Implement the functionality instead of throwing NotImplementedException
    }

    [Fact]
    public void ArgumentOutOfRangeException_ThrowIfNegative_WithInvalidInputType()
    {
        var ex = Assert.Throws<InvalidOperationException>(() => ArgumentOutOfRangeException.ThrowIfNegative(default(ArgumentOutOfRangeException_ThrowIf_InvalidInputType)));
    }

    [Fact]
    public void ArgumentOutOfRangeException_ThrowIfNegativeOrZero_WithInvalidInputType()
    {
        var ex = Assert.Throws<InvalidOperationException>(() => ArgumentOutOfRangeException.ThrowIfNegativeOrZero(default(ArgumentOutOfRangeException_ThrowIf_InvalidInputType)));
    }

    [Fact]
    public void ArgumentOutOfRangeException_ThrowIfZero_WithInvalidInputType()
    {
        var ex = Assert.Throws<InvalidOperationException>(() => ArgumentOutOfRangeException.ThrowIfZero(default(ArgumentOutOfRangeException_ThrowIf_InvalidInputType)));
    }
#endif

    [Fact]
    public void String_Concat()
    {
        Assert.Equal("ab", string.Concat("a".AsSpan(), "b".AsSpan()));
        Assert.Equal("abc", string.Concat("a".AsSpan(), "b".AsSpan(), "c".AsSpan()));
        Assert.Equal("abcd", string.Concat("a".AsSpan(), "b".AsSpan(), "c".AsSpan(), "d".AsSpan()));
    }

    [Fact]
    public void OperatingSystem_IsWindows()
    {
        OperatingSystem.IsWindows();
        OperatingSystem.IsWindowsVersionAtLeast(6);
    }

    [Fact]
    public void OperatingSystem_IsMacOS()
    {
        OperatingSystem.IsMacOS();
    }

    [Fact]
    public void OperatingSystem_IsLinux()
    {
        OperatingSystem.IsLinux();
    }

    [Fact]
    public void Enum_GetNames()
    {
        Assert.Equal(Enum.GetNames(typeof(DayOfWeek)), Enum.GetNames<DayOfWeek>());
    }

    [Fact]
    public void Enum_GetValues()
    {
        var expected = (DayOfWeek[])Enum.GetValues(typeof(DayOfWeek));
        var actual = Enum.GetValues<DayOfWeek>();
        Assert.Equal(expected, actual);
        Assert.Equal(7, actual.Length);
        Assert.Contains(DayOfWeek.Monday, actual);
        Assert.Contains(DayOfWeek.Sunday, actual);
    }

    [Fact]
    public void Enum_IsDefined()
    {
        Assert.True(Enum.IsDefined(DayOfWeek.Monday));
        Assert.True(Enum.IsDefined(DayOfWeek.Friday));
        Assert.True(Enum.IsDefined(DayOfWeek.Sunday));
        Assert.False(Enum.IsDefined((DayOfWeek)99));
    }

    [Fact]
    public void Enum_Parse()
    {
        Assert.Equal(DayOfWeek.Monday, Enum.Parse<DayOfWeek>("Monday"));
        Assert.Equal(DayOfWeek.Friday, Enum.Parse<DayOfWeek>("Friday"));
        Assert.Throws<ArgumentException>(() => Enum.Parse<DayOfWeek>("InvalidDay"));
        Assert.Throws<ArgumentNullException>(() => Enum.Parse<DayOfWeek>(null!));
    }

    [Fact]
    public void Enum_Parse_IgnoreCase()
    {
        Assert.Equal(DayOfWeek.Monday, Enum.Parse<DayOfWeek>("monday", ignoreCase: true));
        Assert.Equal(DayOfWeek.Friday, Enum.Parse<DayOfWeek>("FRIDAY", ignoreCase: true));
        Assert.Throws<ArgumentException>(() => Enum.Parse<DayOfWeek>("monday", ignoreCase: false));
    }

    [Fact]
    public void Enum_Parse_ReadOnlySpan()
    {
        Assert.Equal(DayOfWeek.Monday, Enum.Parse<DayOfWeek>("Monday".AsSpan()));
        Assert.Equal(DayOfWeek.Friday, Enum.Parse<DayOfWeek>("Friday".AsSpan()));
        Assert.Throws<ArgumentException>(() => Enum.Parse<DayOfWeek>("InvalidDay".AsSpan()));
    }

    [Fact]
    public void Enum_Parse_ReadOnlySpan_IgnoreCase()
    {
        Assert.Equal(DayOfWeek.Monday, Enum.Parse<DayOfWeek>("monday".AsSpan(), ignoreCase: true));
        Assert.Equal(DayOfWeek.Friday, Enum.Parse<DayOfWeek>("FRIDAY".AsSpan(), ignoreCase: true));
        Assert.Throws<ArgumentException>(() => Enum.Parse<DayOfWeek>("monday".AsSpan(), ignoreCase: false));
    }

    [Fact]
    public void Enum_TryParse()
    {
        Assert.True(Enum.TryParse<DayOfWeek>("Monday", ignoreCase: false, out var result1));
        Assert.Equal(DayOfWeek.Monday, result1);

        Assert.True(Enum.TryParse<DayOfWeek>("Friday", ignoreCase: false, out var result2));
        Assert.Equal(DayOfWeek.Friday, result2);

        Assert.False(Enum.TryParse<DayOfWeek>("InvalidDay", ignoreCase: false, out var result3));
        Assert.Equal(default(DayOfWeek), result3);
    }

    [Fact]
    public void Enum_TryParse_IgnoreCase()
    {
        Assert.True(Enum.TryParse<DayOfWeek>("monday", ignoreCase: true, out var result1));
        Assert.Equal(DayOfWeek.Monday, result1);

        Assert.True(Enum.TryParse<DayOfWeek>("FRIDAY", ignoreCase: true, out var result2));
        Assert.Equal(DayOfWeek.Friday, result2);

        Assert.False(Enum.TryParse<DayOfWeek>("monday", ignoreCase: false, out var result3));
        Assert.Equal(default(DayOfWeek), result3);
    }

    [Fact]
    public void Enum_TryParse_ReadOnlySpan()
    {
        Assert.True(Enum.TryParse<DayOfWeek>("Monday".AsSpan(), ignoreCase: false, out var result1));
        Assert.Equal(DayOfWeek.Monday, result1);

        Assert.True(Enum.TryParse<DayOfWeek>("Friday".AsSpan(), ignoreCase: false, out var result2));
        Assert.Equal(DayOfWeek.Friday, result2);

        Assert.False(Enum.TryParse<DayOfWeek>("InvalidDay".AsSpan(), ignoreCase: false, out var result3));
        Assert.Equal(default(DayOfWeek), result3);
    }

    [Fact]
    public void Enum_TryParse_ReadOnlySpan_IgnoreCase()
    {
        Assert.True(Enum.TryParse<DayOfWeek>("monday".AsSpan(), ignoreCase: true, out var result1));
        Assert.Equal(DayOfWeek.Monday, result1);

        Assert.True(Enum.TryParse<DayOfWeek>("FRIDAY".AsSpan(), ignoreCase: true, out var result2));
        Assert.Equal(DayOfWeek.Friday, result2);

        Assert.False(Enum.TryParse<DayOfWeek>("monday".AsSpan(), ignoreCase: false, out var result3));
        Assert.Equal(default(DayOfWeek), result3);
    }

    [Fact]
    public void Convert_ToBase64String_ReadOnlySpan()
    {
        ReadOnlySpan<byte> data = [1, 2, 3, 4];
        var result = Convert.ToBase64String(data);
        Assert.Equal("AQIDBA==", result);

        // Test with Base64FormattingOptions.None
        result = Convert.ToBase64String(data, Base64FormattingOptions.None);
        Assert.Equal("AQIDBA==", result);

        // Test with larger data and InsertLineBreaks option
        var largeData = new byte[100];
        for (int i = 0; i < largeData.Length; i++)
            largeData[i] = (byte)(i % 256);

        var resultWithBreaks = Convert.ToBase64String((ReadOnlySpan<byte>)largeData, Base64FormattingOptions.InsertLineBreaks);
        var expectedWithBreaks = Convert.ToBase64String(largeData, Base64FormattingOptions.InsertLineBreaks);
        Assert.Equal(expectedWithBreaks, resultWithBreaks);
    }

    [Fact]
    public void Convert_ToHexStringLower_ByteArray()
    {
        // Test basic conversion
        byte[] data = [0x0A, 0x1B, 0x2C, 0x3D];
        var result = Convert.ToHexStringLower(data);
        Assert.Equal("0a1b2c3d", result);

        // Test with various byte values
        byte[] data2 = [0x00, 0xFF, 0xAB, 0xCD, 0xEF];
        var result2 = Convert.ToHexStringLower(data2);
        Assert.Equal("00ffabcdef", result2);

        // Test with empty array
        byte[] emptyData = [];
        var emptyResult = Convert.ToHexStringLower(emptyData);
        Assert.Equal("", emptyResult);

        // Test null throws exception
        Assert.Throws<ArgumentNullException>(() => Convert.ToHexStringLower((byte[])null!));
    }

    [Fact]
    public void Convert_ToHexStringLower_ByteArray_Offset_Length()
    {
        // Test basic conversion with offset and length
        byte[] data = [0x00, 0x0A, 0x1B, 0x2C, 0x3D, 0xFF];
        var result = Convert.ToHexStringLower(data, 1, 3);
        Assert.Equal("0a1b2c", result);

        // Test with full array
        var result2 = Convert.ToHexStringLower(data, 0, data.Length);
        Assert.Equal("000a1b2c3dff", result2);

        // Test with zero length
        var result3 = Convert.ToHexStringLower(data, 0, 0);
        Assert.Equal("", result3);

        // Test null array throws exception
        Assert.Throws<ArgumentNullException>(() => Convert.ToHexStringLower(null!, 0, 0));

        // Test negative offset throws exception
        Assert.Throws<ArgumentOutOfRangeException>(() => Convert.ToHexStringLower(data, -1, 1));

        // Test negative length throws exception
        Assert.Throws<ArgumentOutOfRangeException>(() => Convert.ToHexStringLower(data, 0, -1));

        // Test offset beyond array throws exception
        Assert.Throws<ArgumentOutOfRangeException>(() => Convert.ToHexStringLower(data, data.Length + 1, 0));

        // Test offset + length beyond array throws exception
        Assert.Throws<ArgumentOutOfRangeException>(() => Convert.ToHexStringLower(data, 1, data.Length));
    }

    [Fact]
    public void Convert_ToHexStringLower_ReadOnlySpan()
    {
        // Test basic conversion
        ReadOnlySpan<byte> data = [0x0A, 0x1B, 0x2C, 0x3D];
        var result = Convert.ToHexStringLower(data);
        Assert.Equal("0a1b2c3d", result);

        // Test with various byte values
        ReadOnlySpan<byte> data2 = [0x00, 0xFF, 0xAB, 0xCD, 0xEF];
        var result2 = Convert.ToHexStringLower(data2);
        Assert.Equal("00ffabcdef", result2);

        // Test with empty span
        ReadOnlySpan<byte> emptyData = [];
        var emptyResult = Convert.ToHexStringLower(emptyData);
        Assert.Equal("", emptyResult);

        // Test single byte
        ReadOnlySpan<byte> singleByte = [0xA5];
        var singleResult = Convert.ToHexStringLower(singleByte);
        Assert.Equal("a5", singleResult);
    }

    [Fact]
    public void Convert_ToHexString_ByteArray()
    {
        // Test basic conversion
        byte[] data = [0x0A, 0x1B, 0x2C, 0x3D];
        var result = Convert.ToHexString(data);
        Assert.Equal("0A1B2C3D", result);

        // Test with various byte values
        byte[] data2 = [0x00, 0xFF, 0xAB, 0xCD, 0xEF];
        var result2 = Convert.ToHexString(data2);
        Assert.Equal("00FFABCDEF", result2);

        // Test with empty array
        byte[] emptyData = [];
        var emptyResult = Convert.ToHexString(emptyData);
        Assert.Equal("", emptyResult);

        // Test null throws exception
        Assert.Throws<ArgumentNullException>(() => Convert.ToHexString((byte[])null!));
    }

    [Fact]
    public void Convert_ToHexString_ByteArray_Offset_Length()
    {
        // Test basic conversion with offset and length
        byte[] data = [0x00, 0x0A, 0x1B, 0x2C, 0x3D, 0xFF];
        var result = Convert.ToHexString(data, 1, 3);
        Assert.Equal("0A1B2C", result);

        // Test with full array
        var result2 = Convert.ToHexString(data, 0, data.Length);
        Assert.Equal("000A1B2C3DFF", result2);

        // Test with zero length
        var result3 = Convert.ToHexString(data, 0, 0);
        Assert.Equal("", result3);

        // Test null array throws exception
        Assert.Throws<ArgumentNullException>(() => Convert.ToHexString(null!, 0, 0));

        // Test negative offset throws exception
        Assert.Throws<ArgumentOutOfRangeException>(() => Convert.ToHexString(data, -1, 1));

        // Test negative length throws exception
        Assert.Throws<ArgumentOutOfRangeException>(() => Convert.ToHexString(data, 0, -1));

        // Test offset beyond array throws exception
        Assert.Throws<ArgumentOutOfRangeException>(() => Convert.ToHexString(data, data.Length + 1, 0));

        // Test offset + length beyond array throws exception
        Assert.Throws<ArgumentOutOfRangeException>(() => Convert.ToHexString(data, 1, data.Length));
    }

    [Fact]
    public void Convert_ToHexString_ReadOnlySpan()
    {
        // Test basic conversion
        ReadOnlySpan<byte> data = [0x0A, 0x1B, 0x2C, 0x3D];
        var result = Convert.ToHexString(data);
        Assert.Equal("0A1B2C3D", result);

        // Test with various byte values
        ReadOnlySpan<byte> data2 = [0x00, 0xFF, 0xAB, 0xCD, 0xEF];
        var result2 = Convert.ToHexString(data2);
        Assert.Equal("00FFABCDEF", result2);

        // Test with empty span
        ReadOnlySpan<byte> emptyData = [];
        var emptyResult = Convert.ToHexString(emptyData);
        Assert.Equal("", emptyResult);

        // Test single byte
        ReadOnlySpan<byte> singleByte = [0xA5];
        var singleResult = Convert.ToHexString(singleByte);
        Assert.Equal("A5", singleResult);
    }

    [Fact]
    public void BitConverter_ToInt16_ReadOnlySpan()
    {
        // Test with little-endian bytes representing 42
        ReadOnlySpan<byte> data1 = [0x2A, 0x00];
        var result1 = BitConverter.ToInt16(data1);
        Assert.Equal(42, result1);

        // Test with little-endian bytes representing -1
        ReadOnlySpan<byte> data2 = [0xFF, 0xFF];
        var result2 = BitConverter.ToInt16(data2);
        Assert.Equal(-1, result2);

        // Test with little-endian bytes representing 256
        ReadOnlySpan<byte> data3 = [0x00, 0x01];
        var result3 = BitConverter.ToInt16(data3);
        Assert.Equal(256, result3);

        // Test with little-endian bytes representing short.MaxValue (32767)
        ReadOnlySpan<byte> data4 = [0xFF, 0x7F];
        var result4 = BitConverter.ToInt16(data4);
        Assert.Equal(short.MaxValue, result4);

        // Test with little-endian bytes representing short.MinValue (-32768)
        ReadOnlySpan<byte> data5 = [0x00, 0x80];
        var result5 = BitConverter.ToInt16(data5);
        Assert.Equal(short.MinValue, result5);
    }

    [Fact]
    public void BitConverter_ToInt32_ReadOnlySpan()
    {
        // Test with little-endian bytes representing 42
        ReadOnlySpan<byte> data1 = [0x2A, 0x00, 0x00, 0x00];
        var result1 = BitConverter.ToInt32(data1);
        Assert.Equal(42, result1);

        // Test with little-endian bytes representing -1
        ReadOnlySpan<byte> data2 = [0xFF, 0xFF, 0xFF, 0xFF];
        var result2 = BitConverter.ToInt32(data2);
        Assert.Equal(-1, result2);

        // Test with little-endian bytes representing 256
        ReadOnlySpan<byte> data3 = [0x00, 0x01, 0x00, 0x00];
        var result3 = BitConverter.ToInt32(data3);
        Assert.Equal(256, result3);

        // Test with little-endian bytes representing int.MaxValue (2147483647)
        ReadOnlySpan<byte> data4 = [0xFF, 0xFF, 0xFF, 0x7F];
        var result4 = BitConverter.ToInt32(data4);
        Assert.Equal(int.MaxValue, result4);

        // Test with little-endian bytes representing int.MinValue (-2147483648)
        ReadOnlySpan<byte> data5 = [0x00, 0x00, 0x00, 0x80];
        var result5 = BitConverter.ToInt32(data5);
        Assert.Equal(int.MinValue, result5);
    }

    [Fact]
    public void String_Create()
    {
        var actual = string.Create(CultureInfo.InvariantCulture, $"a{1}b");
        Assert.Equal("a1b", actual);
    }

    [Fact]
    public void Single_Parse_ReadOnlySpan_Byte_IFormatProvider()
    {
        // Basic parsing from UTF8
        ReadOnlySpan<byte> utf8Data = "123.45"u8;
        var result = float.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(123.45f, result, 2);

        // Scientific notation
        utf8Data = "1.23e+2"u8;
        result = float.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(123f, result, 0);

        // Negative numbers
        utf8Data = "-456.78"u8;
        result = float.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(-456.78f, result, 2);
    }

    [Fact]
    public void Single_Parse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        // Basic parsing with style
        ReadOnlySpan<byte> utf8Data = "123.45"u8;
        var result = float.Parse(utf8Data, NumberStyles.Float, CultureInfo.InvariantCulture);
        Assert.Equal(123.45f, result, 2);

        // Allow leading whitespace
        utf8Data = "  456.78"u8;
        result = float.Parse(utf8Data, NumberStyles.Float, CultureInfo.InvariantCulture);
        Assert.Equal(456.78f, result, 2);
    }

    [Fact]
    public void Single_TryParse_ReadOnlySpan_Byte_IFormatProvider()
    {
        // Basic parsing from UTF8
        ReadOnlySpan<byte> utf8Data = "123.45"u8;
        Assert.True(float.TryParse(utf8Data, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(123.45f, result, 2);

        // Invalid UTF8 input
        utf8Data = "abc"u8;
        Assert.False(float.TryParse(utf8Data, CultureInfo.InvariantCulture, out result));
        Assert.Equal(0f, result);

        // Empty span
        Assert.False(float.TryParse(ReadOnlySpan<byte>.Empty, CultureInfo.InvariantCulture, out _));

        // Null provider
        utf8Data = "789.01"u8;
        Assert.True(float.TryParse(utf8Data, null, out result));
        Assert.Equal(789.01f, result, 2);
    }

    [Fact]
    public void Single_TryParse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        // Parsing with style
        ReadOnlySpan<byte> utf8Data = "123.45"u8;
        Assert.True(float.TryParse(utf8Data, NumberStyles.Float, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(123.45f, result, 2);

        // Invalid with specified style
        utf8Data = "ABC"u8;
        Assert.False(float.TryParse(utf8Data, NumberStyles.Float, CultureInfo.InvariantCulture, out result));
        Assert.Equal(0f, result);

        // Null provider
        utf8Data = "42.5"u8;
        Assert.True(float.TryParse(utf8Data, NumberStyles.Float, null, out result));
        Assert.Equal(42.5f, result, 1);
    }

    [Fact]
    public void Double_Parse_ReadOnlySpan_Byte_IFormatProvider()
    {
        // Basic parsing from UTF8
        ReadOnlySpan<byte> utf8Data = "123.456"u8;
        var result = double.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(123.456d, result, 3);

        // Scientific notation
        utf8Data = "1.23e+2"u8;
        result = double.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(123d, result, 0);

        // Negative numbers
        utf8Data = "-456.789"u8;
        result = double.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(-456.789d, result, 3);
    }

    [Fact]
    public void Double_Parse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        // Basic parsing with style
        ReadOnlySpan<byte> utf8Data = "123.456"u8;
        var result = double.Parse(utf8Data, NumberStyles.Float, CultureInfo.InvariantCulture);
        Assert.Equal(123.456d, result, 3);

        // Allow leading whitespace
        utf8Data = "  456.789"u8;
        result = double.Parse(utf8Data, NumberStyles.Float, CultureInfo.InvariantCulture);
        Assert.Equal(456.789d, result, 3);
    }

    [Fact]
    public void Double_TryParse_ReadOnlySpan_Byte_IFormatProvider()
    {
        // Basic parsing from UTF8
        ReadOnlySpan<byte> utf8Data = "123.456"u8;
        Assert.True(double.TryParse(utf8Data, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(123.456d, result, 3);

        // Invalid UTF8 input
        utf8Data = "abc"u8;
        Assert.False(double.TryParse(utf8Data, CultureInfo.InvariantCulture, out result));
        Assert.Equal(0d, result);

        // Empty span
        Assert.False(double.TryParse(ReadOnlySpan<byte>.Empty, CultureInfo.InvariantCulture, out _));

        // Null provider
        utf8Data = "789.012"u8;
        Assert.True(double.TryParse(utf8Data, null, out result));
        Assert.Equal(789.012d, result, 3);
    }

    [Fact]
    public void Double_TryParse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        // Parsing with style
        ReadOnlySpan<byte> utf8Data = "123.456"u8;
        Assert.True(double.TryParse(utf8Data, NumberStyles.Float, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(123.456d, result, 3);

        // Invalid with specified style
        utf8Data = "ABC"u8;
        Assert.False(double.TryParse(utf8Data, NumberStyles.Float, CultureInfo.InvariantCulture, out result));
        Assert.Equal(0d, result);

        // Null provider
        utf8Data = "42.5"u8;
        Assert.True(double.TryParse(utf8Data, NumberStyles.Float, null, out result));
        Assert.Equal(42.5d, result, 1);
    }

    [Fact]
    public void Decimal_Parse_ReadOnlySpan_Byte_IFormatProvider()
    {
        // Basic parsing from UTF8
        ReadOnlySpan<byte> utf8Data = "123.45"u8;
        var result = decimal.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(123.45m, result);

        // Large number
        utf8Data = "999999999999.99"u8;
        result = decimal.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(999999999999.99m, result);

        // Negative numbers
        utf8Data = "-456.78"u8;
        result = decimal.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(-456.78m, result);
    }

    [Fact]
    public void Decimal_Parse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        // Basic parsing with style
        ReadOnlySpan<byte> utf8Data = "123.45"u8;
        var result = decimal.Parse(utf8Data, NumberStyles.Number, CultureInfo.InvariantCulture);
        Assert.Equal(123.45m, result);

        // Allow leading whitespace
        utf8Data = "  456.78"u8;
        result = decimal.Parse(utf8Data, NumberStyles.Number, CultureInfo.InvariantCulture);
        Assert.Equal(456.78m, result);
    }

    [Fact]
    public void Decimal_TryParse_ReadOnlySpan_Byte_IFormatProvider()
    {
        // Basic parsing from UTF8
        ReadOnlySpan<byte> utf8Data = "123.45"u8;
        Assert.True(decimal.TryParse(utf8Data, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(123.45m, result);

        // Invalid UTF8 input
        utf8Data = "abc"u8;
        Assert.False(decimal.TryParse(utf8Data, CultureInfo.InvariantCulture, out result));
        Assert.Equal(0m, result);

        // Empty span
        Assert.False(decimal.TryParse(ReadOnlySpan<byte>.Empty, CultureInfo.InvariantCulture, out _));

        // Null provider
        utf8Data = "789.01"u8;
        Assert.True(decimal.TryParse(utf8Data, null, out result));
        Assert.Equal(789.01m, result);
    }

    [Fact]
    public void Decimal_TryParse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        // Parsing with style
        ReadOnlySpan<byte> utf8Data = "123.45"u8;
        Assert.True(decimal.TryParse(utf8Data, NumberStyles.Number, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(123.45m, result);

        // Invalid with specified style
        utf8Data = "ABC"u8;
        Assert.False(decimal.TryParse(utf8Data, NumberStyles.Number, CultureInfo.InvariantCulture, out result));
        Assert.Equal(0m, result);

        // Null provider
        utf8Data = "42.5"u8;
        Assert.True(decimal.TryParse(utf8Data, NumberStyles.Number, null, out result));
        Assert.Equal(42.5m, result);
    }

    [Fact]
    public void Byte_Parse_ReadOnlySpan_Byte_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "255"u8;
        var result = byte.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(255, result);

        utf8Data = "0"u8;
        result = byte.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Byte_Parse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "FF"u8;
        var result = byte.Parse(utf8Data, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        Assert.Equal(255, result);
    }

    [Fact]
    public void Byte_TryParse_ReadOnlySpan_Byte_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "123"u8;
        Assert.True(byte.TryParse(utf8Data, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(123, result);

        utf8Data = "256"u8;
        Assert.False(byte.TryParse(utf8Data, CultureInfo.InvariantCulture, out _));
    }

    [Fact]
    public void Byte_TryParse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "FF"u8;
        Assert.True(byte.TryParse(utf8Data, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(255, result);
    }

    [Fact]
    public void SByte_Parse_ReadOnlySpan_Byte_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "127"u8;
        var result = sbyte.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(127, result);

        utf8Data = "-128"u8;
        result = sbyte.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(-128, result);
    }

    [Fact]
    public void SByte_Parse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "7F"u8;
        var result = sbyte.Parse(utf8Data, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        Assert.Equal(127, result);
    }

    [Fact]
    public void SByte_TryParse_ReadOnlySpan_Byte_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "100"u8;
        Assert.True(sbyte.TryParse(utf8Data, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(100, result);

        utf8Data = "128"u8;
        Assert.False(sbyte.TryParse(utf8Data, CultureInfo.InvariantCulture, out _));
    }

    [Fact]
    public void SByte_TryParse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "7F"u8;
        Assert.True(sbyte.TryParse(utf8Data, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(127, result);
    }

    [Fact]
    public void Int16_Parse_ReadOnlySpan_Byte_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "32767"u8;
        var result = short.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(32767, result);

        utf8Data = "-32768"u8;
        result = short.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(-32768, result);
    }

    [Fact]
    public void Int16_Parse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "7FFF"u8;
        var result = short.Parse(utf8Data, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        Assert.Equal(32767, result);
    }

    [Fact]
    public void Int16_TryParse_ReadOnlySpan_Byte_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "1000"u8;
        Assert.True(short.TryParse(utf8Data, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(1000, result);

        utf8Data = "32768"u8;
        Assert.False(short.TryParse(utf8Data, CultureInfo.InvariantCulture, out _));
    }

    [Fact]
    public void Int16_TryParse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "7FFF"u8;
        Assert.True(short.TryParse(utf8Data, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(32767, result);
    }

    [Fact]
    public void UInt16_Parse_ReadOnlySpan_Byte_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "65535"u8;
        var result = ushort.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(65535, result);

        utf8Data = "0"u8;
        result = ushort.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(0, result);
    }

    [Fact]
    public void UInt16_Parse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "FFFF"u8;
        var result = ushort.Parse(utf8Data, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        Assert.Equal(65535, result);
    }

    [Fact]
    public void UInt16_TryParse_ReadOnlySpan_Byte_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "50000"u8;
        Assert.True(ushort.TryParse(utf8Data, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(50000, result);

        utf8Data = "65536"u8;
        Assert.False(ushort.TryParse(utf8Data, CultureInfo.InvariantCulture, out _));
    }

    [Fact]
    public void UInt16_TryParse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "FFFF"u8;
        Assert.True(ushort.TryParse(utf8Data, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(65535, result);
    }

    [Fact]
    public void UInt32_Parse_ReadOnlySpan_Byte_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "4294967295"u8;
        var result = uint.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(4294967295u, result);

        utf8Data = "0"u8;
        result = uint.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(0u, result);
    }

    [Fact]
    public void UInt32_Parse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "FFFFFFFF"u8;
        var result = uint.Parse(utf8Data, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        Assert.Equal(4294967295u, result);
    }

    [Fact]
    public void UInt32_TryParse_ReadOnlySpan_Byte_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "1000000000"u8;
        Assert.True(uint.TryParse(utf8Data, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(1000000000u, result);

        utf8Data = "4294967296"u8;
        Assert.False(uint.TryParse(utf8Data, CultureInfo.InvariantCulture, out _));
    }

    [Fact]
    public void UInt32_TryParse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "FFFFFFFF"u8;
        Assert.True(uint.TryParse(utf8Data, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(4294967295u, result);
    }

    [Fact]
    public void Int32_TryParse_ReadOnlySpan_Char()
    {
        // Basic parsing
        Assert.True(int.TryParse("123".AsSpan(), CultureInfo.InvariantCulture, out var result));
        Assert.Equal(123, result);

        // Negative numbers
        Assert.True(int.TryParse("-456".AsSpan(), CultureInfo.InvariantCulture, out result));
        Assert.Equal(-456, result);

        // Invalid input
        Assert.False(int.TryParse("abc".AsSpan(), CultureInfo.InvariantCulture, out result));
        Assert.Equal(0, result);

        // Empty span
        Assert.False(int.TryParse(ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture, out _));

        // Null provider
        Assert.True(int.TryParse("789".AsSpan(), (IFormatProvider?)null, out result));
        Assert.Equal(789, result);
    }

    [Fact]
    public void Int32_TryParse_ReadOnlySpan_Char_NumberStyles()
    {
        // Hex parsing
        Assert.True(int.TryParse("FF".AsSpan(), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(255, result);

        // Allow leading white space
        Assert.True(int.TryParse("  123".AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out result));
        Assert.Equal(123, result);

        // Invalid with specified style
        Assert.False(int.TryParse("ABC".AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out result));
        Assert.Equal(0, result);

        // Null provider
        Assert.True(int.TryParse("42".AsSpan(), NumberStyles.Integer, null, out result));
        Assert.Equal(42, result);
    }

    [Fact]
    public void Int32_TryParse_ReadOnlySpan_Byte()
    {
        // Basic parsing from UTF8
        ReadOnlySpan<byte> utf8Data = "123"u8;
        Assert.True(int.TryParse(utf8Data, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(123, result);

        // Negative numbers
        utf8Data = "-456"u8;
        Assert.True(int.TryParse(utf8Data, CultureInfo.InvariantCulture, out result));
        Assert.Equal(-456, result);

        // Invalid UTF8 input
        utf8Data = "abc"u8;
        Assert.False(int.TryParse(utf8Data, CultureInfo.InvariantCulture, out result));
        Assert.Equal(0, result);

        // Empty span
        Assert.False(int.TryParse(ReadOnlySpan<byte>.Empty, CultureInfo.InvariantCulture, out _));

        // Null provider
        utf8Data = "789"u8;
        Assert.True(int.TryParse(utf8Data, (IFormatProvider?)null, out result));
        Assert.Equal(789, result);
    }

    [Fact]
    public void Int32_TryParse_ReadOnlySpan_Byte_NumberStyles()
    {
        // Hex parsing from UTF8
        ReadOnlySpan<byte> utf8Data = "FF"u8;
        Assert.True(int.TryParse(utf8Data, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(255, result);

        // Allow leading white space
        utf8Data = "  123"u8;
        Assert.True(int.TryParse(utf8Data, NumberStyles.Integer, CultureInfo.InvariantCulture, out result));
        Assert.Equal(123, result);

        // Invalid with specified style
        utf8Data = "ABC"u8;
        Assert.False(int.TryParse(utf8Data, NumberStyles.Integer, CultureInfo.InvariantCulture, out result));
        Assert.Equal(0, result);

        // Null provider
        utf8Data = "42"u8;
        Assert.True(int.TryParse(utf8Data, NumberStyles.Integer, null, out result));
        Assert.Equal(42, result);
    }

    [Fact]
    public void UInt64_Parse_ReadOnlySpan_Byte_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "18446744073709551615"u8;
        var result = ulong.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(18446744073709551615ul, result);

        utf8Data = "0"u8;
        result = ulong.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(0ul, result);
    }

    [Fact]
    public void UInt64_Parse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "FFFFFFFFFFFFFFFF"u8;
        var result = ulong.Parse(utf8Data, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        Assert.Equal(18446744073709551615ul, result);
    }

    [Fact]
    public void UInt64_TryParse_ReadOnlySpan_Byte_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "10000000000000000000"u8;
        Assert.True(ulong.TryParse(utf8Data, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(10000000000000000000ul, result);

        utf8Data = "18446744073709551616"u8;
        Assert.False(ulong.TryParse(utf8Data, CultureInfo.InvariantCulture, out _));
    }

    [Fact]
    public void UInt64_TryParse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "FFFFFFFFFFFFFFFF"u8;
        Assert.True(ulong.TryParse(utf8Data, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(18446744073709551615ul, result);
    }

    [Fact]
    public void Int64_Parse_ReadOnlySpan_Byte_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "9223372036854775807"u8;
        var result = long.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(9223372036854775807L, result);

        utf8Data = "-9223372036854775808"u8;
        result = long.Parse(utf8Data, CultureInfo.InvariantCulture);
        Assert.Equal(-9223372036854775808L, result);
    }

    [Fact]
    public void Int64_Parse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "7FFFFFFFFFFFFFFF"u8;
        var result = long.Parse(utf8Data, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        Assert.Equal(9223372036854775807L, result);
    }

    [Fact]
    public void Int64_TryParse_ReadOnlySpan_Byte_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "1000000000000000000"u8;
        Assert.True(long.TryParse(utf8Data, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(1000000000000000000L, result);

        utf8Data = "9223372036854775808"u8;
        Assert.False(long.TryParse(utf8Data, CultureInfo.InvariantCulture, out _));
    }

    [Fact]
    public void Int64_TryParse_ReadOnlySpan_Byte_NumberStyles_IFormatProvider()
    {
        ReadOnlySpan<byte> utf8Data = "7FFFFFFFFFFFFFFF"u8;
        Assert.True(long.TryParse(utf8Data, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(9223372036854775807L, result);
    }

    [Fact]
    public void DateTime_Nanosecond()
    {
        // DateTime with 0 nanoseconds
        var dt1 = new DateTime(2024, 1, 1, 12, 30, 45, 123);
        Assert.Equal(0, dt1.Nanosecond);

        // DateTime with 100 nanoseconds (1 tick)
        var dt2 = new DateTime(2024, 1, 1, 12, 30, 45, 123).AddTicks(1);
        Assert.Equal(100, dt2.Nanosecond);

        // DateTime with 900 nanoseconds (9 ticks)
        var dt3 = new DateTime(2024, 1, 1, 12, 30, 45, 123).AddTicks(9);
        Assert.Equal(900, dt3.Nanosecond);

        // DateTime at maximum nanoseconds
        var dt4 = new DateTime(2024, 1, 1, 0, 0, 0).AddTicks(9);
        Assert.Equal(900, dt4.Nanosecond);

        // DateTime at minimum value
        Assert.Equal(0, DateTime.MinValue.Nanosecond);

        // DateTime at maximum value (9999-12-31 23:59:59.9999999)
        // MaxValue has 9 ticks remainder
        Assert.Equal(900, DateTime.MaxValue.Nanosecond);
    }

    [Fact]
    public void DateTimeOffset_Nanosecond()
    {
        // DateTimeOffset with 0 nanoseconds
        var dto1 = new DateTimeOffset(2024, 1, 1, 12, 30, 45, 123, TimeSpan.Zero);
        Assert.Equal(0, dto1.Nanosecond);

        // DateTimeOffset with 100 nanoseconds (1 tick)
        var dto2 = new DateTimeOffset(2024, 1, 1, 12, 30, 45, 123, TimeSpan.Zero).AddTicks(1);
        Assert.Equal(100, dto2.Nanosecond);

        // DateTimeOffset with 900 nanoseconds (9 ticks)
        var dto3 = new DateTimeOffset(2024, 1, 1, 12, 30, 45, 123, TimeSpan.Zero).AddTicks(9);
        Assert.Equal(900, dto3.Nanosecond);

        // DateTimeOffset with different timezone offset
        var dto4 = new DateTimeOffset(2024, 1, 1, 12, 30, 45, 123, TimeSpan.FromHours(5)).AddTicks(5);
        Assert.Equal(500, dto4.Nanosecond);

        // DateTimeOffset at minimum value
        Assert.Equal(0, DateTimeOffset.MinValue.Nanosecond);

        // DateTimeOffset at maximum value
        Assert.Equal(900, DateTimeOffset.MaxValue.Nanosecond);
    }

#if NET6_0_OR_GREATER
    [Fact]
    public void DateOnly_Deconstruct()
    {
        var date = new DateOnly(2024, 5, 15);
        var (year, month, day) = date;
        Assert.Equal(2024, year);
        Assert.Equal(5, month);
        Assert.Equal(15, day);
    }

    [Fact]
    public void TimeOnly_Deconstruct_HourMinute()
    {
        var time = new TimeOnly(14, 30, 45, 123);
        time.Deconstruct(out int hour, out int minute);
        Assert.Equal(14, hour);
        Assert.Equal(30, minute);
    }

    [Fact]
    public void TimeOnly_Deconstruct_HourMinuteSecond()
    {
        var time = new TimeOnly(14, 30, 45, 123);
        time.Deconstruct(out int hour, out int minute, out int second);
        Assert.Equal(14, hour);
        Assert.Equal(30, minute);
        Assert.Equal(45, second);
    }

    [Fact]
    public void TimeOnly_Deconstruct_HourMinuteSecondMillisecond()
    {
        var time = new TimeOnly(14, 30, 45, 123);
        time.Deconstruct(out int hour, out int minute, out int second, out int millisecond);
        Assert.Equal(14, hour);
        Assert.Equal(30, minute);
        Assert.Equal(45, second);
        Assert.Equal(123, millisecond);
    }

    [Fact]
    public void TimeOnly_Deconstruct_Full()
    {
        var time = new TimeOnly(14, 30, 45, 123, 456);
        var (hour, minute, second, millisecond, microsecond) = time;
        Assert.Equal(14, hour);
        Assert.Equal(30, minute);
        Assert.Equal(45, second);
        Assert.Equal(123, millisecond);
        Assert.Equal(456, microsecond);
    }
#endif

    [Fact]
    public void TimeSpan_Microseconds()
    {
        // TimeSpan with 0 microseconds
        var ts1 = new TimeSpan(hours: 1, minutes: 2, seconds: 3);
        Assert.Equal(0, ts1.Microseconds);

        // TimeSpan with specific microseconds
        var ts2 = TimeSpan.FromTicks(12345678); // 1.2345678 seconds
        Assert.Equal(567, ts2.Microseconds); // 5678 ticks = 567.8 microseconds, integer part is 567

        // TimeSpan from hours with microseconds
        var ts3 = new TimeSpan(0, 0, 0, 0, 123) + TimeSpan.FromTicks(4567);
        Assert.Equal(456, ts3.Microseconds);
    }

    [Fact]
    public void TimeSpan_Nanoseconds()
    {
        // TimeSpan with 0 nanoseconds
        var ts1 = new TimeSpan(hours: 1, minutes: 2, seconds: 3);
        Assert.Equal(0, ts1.Nanoseconds);

        // TimeSpan with specific nanoseconds (1 tick = 100 nanoseconds)
        var ts2 = TimeSpan.FromTicks(1);
        Assert.Equal(100, ts2.Nanoseconds);

        var ts3 = TimeSpan.FromTicks(9);
        Assert.Equal(900, ts3.Nanoseconds);

        var ts4 = TimeSpan.FromTicks(12345679);
        Assert.Equal(900, ts4.Nanoseconds); // 9 ticks remainder = 900 nanoseconds
    }

    [Fact]
    public void Delegate_HasSingleTarget()
    {
        Action action1 = () => { };
        Assert.True(action1.HasSingleTarget);

        Action action2 = () => { };
        Action combined = action1 + action2;
        Assert.False(combined.HasSingleTarget);
    }

    [Fact]
    public void Delegate_EnumerateInvocationList()
    {
        var count = 0;
        Action action1 = () => count += 1;
        Action action2 = () => count += 10;
        Action action3 = () => count += 100;
        Action combined = action1 + action2 + action3;

        var invocationCount = 0;
        foreach (var d in Delegate.EnumerateInvocationList(combined))
        {
            invocationCount++;
            d();
        }

        Assert.Equal(3, invocationCount);
        Assert.Equal(111, count);
    }

    [Fact]
    public void Delegate_EnumerateInvocationList_Single()
    {
        Action action = () => { };
        var count = 0;
        foreach (var d in Delegate.EnumerateInvocationList(action))
        {
            count++;
        }
        Assert.Equal(1, count);
    }

    [Fact]
    public void Delegate_EnumerateInvocationList_Null()
    {
        Action? action = null;
        var count = 0;
        foreach (var _ in Delegate.EnumerateInvocationList(action))
        {
            count++;
        }
        Assert.Equal(0, count);
    }

}
