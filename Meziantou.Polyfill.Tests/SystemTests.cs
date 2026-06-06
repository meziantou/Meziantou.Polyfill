using System;
using System.Buffers;
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
using System.Text.RegularExpressions;
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
    public void String_EndsWith_Char_StringComparison()
    {
        Assert.True("test".EndsWith('t', StringComparison.Ordinal));
        Assert.True("test".EndsWith('T', StringComparison.OrdinalIgnoreCase));
        Assert.False("test".EndsWith('T', StringComparison.Ordinal));
        Assert.False("".EndsWith('t', StringComparison.Ordinal));
    }

    [Fact]
    public void String_GetHashCode()
    {
        Assert.Equal("test".GetHashCode(), "test".GetHashCode(StringComparison.Ordinal));
        Assert.Equal(StringComparer.OrdinalIgnoreCase.GetHashCode("Test"), "test".GetHashCode(StringComparison.OrdinalIgnoreCase));
        Assert.Equal("test".GetHashCode(), string.GetHashCode("test".AsSpan()));
        Assert.Equal(StringComparer.OrdinalIgnoreCase.GetHashCode("Test"), string.GetHashCode("test".AsSpan(), StringComparison.OrdinalIgnoreCase));

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
    public void String_StartsWith_Char_StringComparison()
    {
        Assert.True("test".StartsWith('t', StringComparison.Ordinal));
        Assert.True("test".StartsWith('T', StringComparison.OrdinalIgnoreCase));
        Assert.False("test".StartsWith('T', StringComparison.Ordinal));
        Assert.False("".StartsWith('t', StringComparison.Ordinal));
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
    public void String_LastIndexOf_Char_StringComparison()
    {
        Assert.Equal(4, "tEstT".LastIndexOf('t', StringComparison.OrdinalIgnoreCase));
        Assert.Equal(3, "tEstT".LastIndexOf('t', StringComparison.Ordinal));
        Assert.Equal(-1, "".LastIndexOf('t', StringComparison.Ordinal));
    }

    [Fact]
    public void String_IndexOf_Char_StartIndex_StringComparison()
    {
        Assert.Equal(2, "test".IndexOf('S', 1, StringComparison.OrdinalIgnoreCase));
        Assert.Equal(-1, "test".IndexOf('T', 1, StringComparison.Ordinal));
    }

    [Fact]
    public void String_IndexOf_Char_StartIndex_Count_StringComparison()
    {
        Assert.Equal(2, "test".IndexOf('S', 1, 2, StringComparison.OrdinalIgnoreCase));
        Assert.Equal(-1, "test".IndexOf('t', 1, 2, StringComparison.Ordinal));
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
    public void TimeSpan_Multiply_Double_TimeSpan_EdgeCases()
    {
        Assert.Equal(TimeSpan.MaxValue, 1.0 * TimeSpan.MaxValue);
        Assert.Throws<ArgumentException>(() => double.NaN * TimeSpan.FromSeconds(1));
        Assert.Throws<OverflowException>(() => 2.0 * TimeSpan.MaxValue);
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
    public void TimeSpan_Multiply_TimeSpan_Double_EdgeCases()
    {
        Assert.Equal(TimeSpan.MaxValue, TimeSpan.MaxValue * 1.0);
        Assert.Throws<ArgumentException>(() => TimeSpan.FromSeconds(1) * double.NaN);
        Assert.Throws<OverflowException>(() => TimeSpan.MaxValue * 2.0);
    }

    [Fact]
    public void TimeSpan_Multiply_Method_EdgeCases()
    {
        Assert.Equal(TimeSpan.MaxValue, TimeSpan.MaxValue.Multiply(1.0));
        Assert.Throws<ArgumentException>(() => TimeSpan.FromSeconds(1).Multiply(double.NaN));
        Assert.Throws<OverflowException>(() => TimeSpan.MaxValue.Multiply(2.0));
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
    public void TimeSpan_Division_Double_EdgeCases()
    {
        Assert.Equal(TimeSpan.MaxValue, TimeSpan.MaxValue / 1.0);
        Assert.Throws<OverflowException>(() => TimeSpan.FromSeconds(1) / 0.0);
        Assert.Throws<OverflowException>(() => TimeSpan.Zero / 0.0);
        Assert.Throws<ArgumentException>(() => TimeSpan.FromSeconds(1) / double.NaN);
    }

    [Fact]
    public void Environment_ProcessId()
    {
        Assert.Equal(Process.GetCurrentProcess().Id, Environment.ProcessId);
    }

    [Fact]
    public void Environment_TickCount64()
    {
        var first = Environment.TickCount64;
        Assert.True(first > 0, $"Expected positive value, got {first}");

        Thread.Sleep(15);
        var second = Environment.TickCount64;
        Assert.True(second > first, $"Expected {second} > {first}");
    }

    [Fact]
    public void Task_IsCompletedSuccessfully()
    {
        Assert.True(Task.CompletedTask.IsCompletedSuccessfully);
        Assert.False(Task.FromException(new InvalidOperationException()).IsCompletedSuccessfully);

        var tcs = new TaskCompletionSource<int>();
        tcs.SetCanceled();
        Assert.False(tcs.Task.IsCompletedSuccessfully);
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
    public void Enum_TryParse_Type()
    {
        Assert.True(Enum.TryParse(typeof(DayOfWeek), "Monday", out var result1));
        Assert.Equal(DayOfWeek.Monday, result1);

        Assert.False(Enum.TryParse(typeof(DayOfWeek), "InvalidDay", out var result2));
        Assert.Null(result2);

        Assert.False(Enum.TryParse(typeof(DayOfWeek), (string?)null, out var result3));
        Assert.Null(result3);
    }

    [Fact]
    public void Enum_TryParse_Type_IgnoreCase()
    {
        Assert.True(Enum.TryParse(typeof(DayOfWeek), "monday", ignoreCase: true, out var result1));
        Assert.Equal(DayOfWeek.Monday, result1);

        Assert.False(Enum.TryParse(typeof(DayOfWeek), "monday", ignoreCase: false, out var result2));
        Assert.Null(result2);
    }

    [Fact]
    public void Enum_TryParse_Type_ReadOnlySpan()
    {
        Assert.True(Enum.TryParse(typeof(DayOfWeek), "Monday".AsSpan(), out var result1));
        Assert.Equal(DayOfWeek.Monday, result1);

        Assert.False(Enum.TryParse(typeof(DayOfWeek), "InvalidDay".AsSpan(), out var result2));
        Assert.Null(result2);
    }

    [Fact]
    public void Enum_TryParse_Type_ReadOnlySpan_IgnoreCase()
    {
        Assert.True(Enum.TryParse(typeof(DayOfWeek), "monday".AsSpan(), ignoreCase: true, out var result1));
        Assert.Equal(DayOfWeek.Monday, result1);

        Assert.False(Enum.TryParse(typeof(DayOfWeek), "monday".AsSpan(), ignoreCase: false, out var result2));
        Assert.Null(result2);
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
    public void Convert_FromHexString()
    {
        Assert.Equal<byte[]>([0x01, 0xAB, 0xFF], Convert.FromHexString("01abFF"));
        Assert.Equal<byte[]>([0x01, 0xAB, 0xFF], Convert.FromHexString("01abFF".AsSpan()));
        Assert.Equal<byte[]>([0x01, 0xAB, 0xFF], Convert.FromHexString("01abFF"u8));
        Assert.Empty(Convert.FromHexString(""));

        Assert.Throws<ArgumentNullException>(() => Convert.FromHexString((string)null!));
        Assert.Throws<FormatException>(() => Convert.FromHexString("0"));
        Assert.Throws<FormatException>(() => Convert.FromHexString("0G"));
    }

    [Fact]
    public void Convert_FromHexString_Status()
    {
        Span<byte> destination = stackalloc byte[3];

        Assert.Equal(OperationStatus.Done, Convert.FromHexString("01abFF", destination, out var consumed, out var written));
        Assert.Equal(6, consumed);
        Assert.Equal(3, written);
        Assert.Equal<byte[]>([0x01, 0xAB, 0xFF], destination.ToArray());

        destination.Clear();
        Assert.Equal(OperationStatus.NeedMoreData, Convert.FromHexString("01a", destination, out consumed, out written));
        Assert.Equal(2, consumed);
        Assert.Equal(1, written);
        Assert.Equal(0x01, destination[0]);

        destination.Clear();
        Assert.Equal(OperationStatus.InvalidData, Convert.FromHexString("01G2", destination, out consumed, out written));
        Assert.Equal(2, consumed);
        Assert.Equal(1, written);
        Assert.Equal(0x01, destination[0]);

        destination.Clear();
        Assert.Equal(OperationStatus.DestinationTooSmall, Convert.FromHexString("010203", destination[..2], out consumed, out written));
        Assert.Equal(4, consumed);
        Assert.Equal(2, written);
        Assert.Equal<byte[]>([0x01, 0x02], destination[..2].ToArray());

        destination.Clear();
        Assert.Equal(OperationStatus.Done, Convert.FromHexString("01abFF".AsSpan(), destination, out consumed, out written));
        Assert.Equal(6, consumed);
        Assert.Equal(3, written);
        Assert.Equal<byte[]>([0x01, 0xAB, 0xFF], destination.ToArray());

        destination.Clear();
        Assert.Equal(OperationStatus.Done, Convert.FromHexString("01abFF"u8, destination, out consumed, out written));
        Assert.Equal(6, consumed);
        Assert.Equal(3, written);
        Assert.Equal<byte[]>([0x01, 0xAB, 0xFF], destination.ToArray());

        Assert.Throws<ArgumentNullException>(() => Convert.FromHexString((string)null!, new byte[1], out _, out _));
    }

    [Fact]
    public void Convert_TryBase64()
    {
        Span<byte> bytes = stackalloc byte[4];
        Assert.True(Convert.TryFromBase64String("AQIDBA==", bytes, out var bytesWritten));
        Assert.Equal(4, bytesWritten);
        Assert.Equal<byte[]>([1, 2, 3, 4], bytes.ToArray());

        bytes.Clear();
        Assert.True(Convert.TryFromBase64Chars("AQID\r\nBA==".AsSpan(), bytes, out bytesWritten));
        Assert.Equal(4, bytesWritten);
        Assert.Equal<byte[]>([1, 2, 3, 4], bytes.ToArray());

        Assert.False(Convert.TryFromBase64String("invalid", bytes, out bytesWritten));
        Assert.Equal(0, bytesWritten);
        Assert.False(Convert.TryFromBase64String("AQIDBA==", bytes[..3], out bytesWritten));
        Assert.Equal(0, bytesWritten);
        Assert.Throws<ArgumentNullException>(() => Convert.TryFromBase64String(null!, new byte[1], out _));

        Span<char> chars = stackalloc char[8];
        Assert.True(Convert.TryToBase64Chars([1, 2, 3, 4], chars, out var charsWritten));
        Assert.Equal(8, charsWritten);
        Assert.Equal("AQIDBA==", chars.ToString());
        Assert.False(Convert.TryToBase64Chars([1, 2, 3, 4], chars[..7], out charsWritten));
        Assert.Equal(0, charsWritten);
    }

    [Fact]
    public void Convert_TryToHexString()
    {
        ReadOnlySpan<byte> bytes = [0x01, 0xAB, 0xFF];
        Span<char> chars = stackalloc char[6];
        Span<byte> utf8 = stackalloc byte[6];

        Assert.True(Convert.TryToHexString(bytes, chars, out var written));
        Assert.Equal(6, written);
        Assert.Equal("01ABFF", chars.ToString());
        Assert.True(Convert.TryToHexString(bytes, utf8, out written));
        Assert.Equal(6, written);
        Assert.Equal("01ABFF", Encoding.ASCII.GetString(utf8));

        Assert.True(Convert.TryToHexStringLower(bytes, chars, out written));
        Assert.Equal(6, written);
        Assert.Equal("01abff", chars.ToString());
        Assert.True(Convert.TryToHexStringLower(bytes, utf8, out written));
        Assert.Equal(6, written);
        Assert.Equal("01abff", Encoding.ASCII.GetString(utf8));

        chars.Fill('x');
        Assert.False(Convert.TryToHexString(bytes, chars[..5], out written));
        Assert.Equal(0, written);
        Assert.Equal("xxxxx", chars[..5].ToString());
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
    public void BitConverter_ToUInt16_ReadOnlySpan()
    {
        // Test with little-endian bytes representing 42
        ReadOnlySpan<byte> data1 = [0x2A, 0x00];
        var result1 = BitConverter.ToUInt16(data1);
        Assert.Equal(42u, result1);

        // Test with little-endian bytes representing 256
        ReadOnlySpan<byte> data3 = [0x00, 0x01];
        var result2 = BitConverter.ToUInt16(data3);
        Assert.Equal(256u, result2);

        // Test with little-endian bytes representing ushort.MaxValue (65535)
        ReadOnlySpan<byte> data4 = [0xFF, 0xFF];
        var result3 = BitConverter.ToUInt16(data4);
        Assert.Equal(ushort.MaxValue, result3);

        // Test with little-endian bytes representing ushort.MinValue (0)
        ReadOnlySpan<byte> data5 = [0x00, 0x00];
        var result4 = BitConverter.ToUInt16(data5);
        Assert.Equal(ushort.MinValue, result4);
    }

    [Fact]
    public void BitConverter_ToUInt32_ReadOnlySpan()
    {
        // Test with little-endian bytes representing 42
        ReadOnlySpan<byte> data1 = [0x2A, 0x00, 0x00, 0x00];
        var result1 = BitConverter.ToUInt32(data1);
        Assert.Equal(42u, result1);

        // Test with little-endian bytes representing 256
        ReadOnlySpan<byte> data3 = [0x00, 0x01, 0x00, 0x00];
        var result2 = BitConverter.ToUInt32(data3);
        Assert.Equal(256u, result2);

        // Test with little-endian bytes representing uint.MaxValue (4294967295)
        ReadOnlySpan<byte> data4 = [0xFF, 0xFF, 0xFF, 0xFF];
        var result3 = BitConverter.ToUInt32(data4);
        Assert.Equal(uint.MaxValue, result3);

        // Test with little-endian bytes representing uint.MinValue (0)
        ReadOnlySpan<byte> data5 = [0x00, 0x00, 0x00, 0x00];
        var result4 = BitConverter.ToUInt32(data5);
        Assert.Equal(uint.MinValue, result4);
    }

    [Fact]
    public void String_Create()
    {
        var actual = string.Create(CultureInfo.InvariantCulture, $"a{1}b");
        Assert.Equal("a1b", actual);
    }

    [Fact]
    public void String_Create_TState()
    {
        var actual = string.Create(3, "abc", static (span, state) => state.AsSpan().CopyTo(span));
        Assert.Equal("abc", actual);

        var called = false;
        var empty = string.Create(0, 0, (span, state) => called = true);
        Assert.Empty(empty);
        Assert.False(called);

        Assert.Throws<ArgumentNullException>(() => string.Create(1, 0, null!));
        Assert.Throws<ArgumentOutOfRangeException>(() => string.Create(-1, 0, static (span, state) => { }));
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
    public void Double_TryParse_ReadOnlySpan_Char_NumberStyles_IFormatProvider()
    {
        Assert.True(double.TryParse("123.456".AsSpan(), NumberStyles.Float, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(123.456d, result, 3);

        Assert.False(double.TryParse("ABC".AsSpan(), NumberStyles.Float, CultureInfo.InvariantCulture, out result));
        Assert.Equal(0d, result);

        Assert.True(double.TryParse("42.5".AsSpan(), NumberStyles.Float, null, out result));
        Assert.Equal(42.5d, result, 1);
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
    public void Decimal_TryFormat_TryParse()
    {
        const decimal Value = 12345.67m;

        Span<char> chars = stackalloc char[16];
        Assert.True(Value.TryFormat(chars, out var charsWritten, "F2", CultureInfo.InvariantCulture));
        Assert.Equal("12345.67", chars[..charsWritten].ToString());
        Assert.False(Value.TryFormat(chars[..1], out charsWritten, "F2", CultureInfo.InvariantCulture));
        Assert.Equal(0, charsWritten);

        Span<byte> bytes = stackalloc byte[16];
        Assert.True(Value.TryFormat(bytes, out var bytesWritten, "F2", CultureInfo.InvariantCulture));
        Assert.Equal("12345.67", Encoding.UTF8.GetString(bytes[..bytesWritten]));
        Assert.False(Value.TryFormat(bytes[..1], out bytesWritten, "F2", CultureInfo.InvariantCulture));
        Assert.Equal(0, bytesWritten);

        ReadOnlySpan<byte> utf8Text = "12345.67"u8;
#pragma warning disable MA0011 // Test the providerless overload
        Assert.True(decimal.TryParse(utf8Text, out var parsed));
#pragma warning restore MA0011
        Assert.Equal(Value, parsed);
        Assert.True(decimal.TryParse("12345.67", CultureInfo.InvariantCulture, out parsed));
        Assert.Equal(Value, parsed);
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
    public void Int64_TryParse_ReadOnlySpan_Char_NumberStyles_IFormatProvider()
    {
        Assert.True(long.TryParse("7FFFFFFFFFFFFFFF".AsSpan(), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result));
        Assert.Equal(9223372036854775807L, result);

        Assert.True(long.TryParse("-42".AsSpan(), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out result));
        Assert.Equal(-42L, result);

        Assert.False(long.TryParse("ABC".AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out result));
        Assert.Equal(0L, result);

        Assert.True(long.TryParse("17".AsSpan(), NumberStyles.Integer, null, out result));
        Assert.Equal(17L, result);
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
    public void DateTime_Microsecond_AddMicroseconds_Deconstruct()
    {
        var value = new DateTime(2024, 5, 15, 12, 30, 45, 123, DateTimeKind.Utc).AddTicks(4_567);

        Assert.Equal(456, value.Microsecond);
        Assert.Equal(value.AddTicks(45), value.AddMicroseconds(4.5));
        Assert.Equal(value.AddTicks(-45), value.AddMicroseconds(-4.5));
        Assert.Equal(DateTimeKind.Utc, value.AddMicroseconds(1).Kind);

        var (year, month, day) = value;
        Assert.Equal(2024, year);
        Assert.Equal(5, month);
        Assert.Equal(15, day);
    }

    [Fact]
    public void DateTime_TryFormat_TryParse_TryParseExact()
    {
        var value = new DateTime(2024, 5, 15, 12, 30, 45, DateTimeKind.Utc);
        Span<char> chars = stackalloc char[32];
        Assert.True(value.TryFormat(chars, out var charsWritten, "O", CultureInfo.InvariantCulture));
        Assert.Equal(value.ToString("O", CultureInfo.InvariantCulture), chars[..charsWritten].ToString());
        Assert.False(value.TryFormat(chars[..1], out charsWritten, "O", CultureInfo.InvariantCulture));
        Assert.Equal(0, charsWritten);

        Span<byte> bytes = stackalloc byte[32];
        Assert.True(value.TryFormat(bytes, out var bytesWritten, "O", CultureInfo.InvariantCulture));
        Assert.Equal(value.ToString("O", CultureInfo.InvariantCulture), Encoding.UTF8.GetString(bytes[..bytesWritten]));
        Assert.False(value.TryFormat(bytes[..1], out bytesWritten, "O", CultureInfo.InvariantCulture));
        Assert.Equal(0, bytesWritten);

        ReadOnlySpan<char> input = "2024-05-15";
#pragma warning disable MA0011 // Test the providerless overload
        Assert.True(DateTime.TryParse(input, out _));
#pragma warning restore MA0011
        Assert.True(DateTime.TryParse(input, CultureInfo.InvariantCulture, out _));
        Assert.True(DateTime.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.None, out _));
        Assert.True(DateTime.TryParse("2024-05-15", CultureInfo.InvariantCulture, out _));
        Assert.True(DateTime.TryParseExact(input, "yyyy-MM-dd".AsSpan(), CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed));
        Assert.Equal(new DateTime(2024, 5, 15), parsed);
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

    [Fact]
    public void DateTimeOffset_Microsecond_AddMicroseconds()
    {
        var value = new DateTimeOffset(2024, 5, 15, 12, 30, 45, 123, TimeSpan.FromHours(5)).AddTicks(4_567);

        Assert.Equal(456, value.Microsecond);
        Assert.Equal(value.AddTicks(45), value.AddMicroseconds(4.5));
        Assert.Equal(value.AddTicks(-45), value.AddMicroseconds(-4.5));
        Assert.Equal(value.Offset, value.AddMicroseconds(1).Offset);
    }

    [Fact]
    public void DateTimeOffset_TryFormat_TryParse_TryParseExact()
    {
        var value = new DateTimeOffset(2024, 5, 15, 12, 30, 45, TimeSpan.FromHours(5));
        Span<char> chars = stackalloc char[40];
        Assert.True(value.TryFormat(chars, out var charsWritten, "O", CultureInfo.InvariantCulture));
        Assert.Equal(value.ToString("O", CultureInfo.InvariantCulture), chars[..charsWritten].ToString());
        Assert.False(value.TryFormat(chars[..1], out charsWritten, "O", CultureInfo.InvariantCulture));
        Assert.Equal(0, charsWritten);

        Span<byte> bytes = stackalloc byte[40];
        Assert.True(value.TryFormat(bytes, out var bytesWritten, "O", CultureInfo.InvariantCulture));
        Assert.Equal(value.ToString("O", CultureInfo.InvariantCulture), Encoding.UTF8.GetString(bytes[..bytesWritten]));
        Assert.False(value.TryFormat(bytes[..1], out bytesWritten, "O", CultureInfo.InvariantCulture));
        Assert.Equal(0, bytesWritten);

        ReadOnlySpan<char> input = "2024-05-15 +05:00";
#pragma warning disable MA0011 // Test the providerless overload
        Assert.True(DateTimeOffset.TryParse(input, out _));
#pragma warning restore MA0011
        Assert.True(DateTimeOffset.TryParse(input, CultureInfo.InvariantCulture, out _));
        Assert.True(DateTimeOffset.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.None, out _));
        Assert.True(DateTimeOffset.TryParse("2024-05-15 +05:00", CultureInfo.InvariantCulture, out _));
        Assert.True(DateTimeOffset.TryParseExact(input, "yyyy-MM-dd zzz".AsSpan(), CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed));
        Assert.Equal(new DateTimeOffset(2024, 5, 15, 0, 0, 0, TimeSpan.FromHours(5)), parsed);
    }

    [Fact]
    public void DateTimeOffset_UnixEpoch()
    {
        Assert.Equal(new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero), DateTimeOffset.UnixEpoch);
        Assert.Equal(TimeSpan.Zero, DateTimeOffset.UnixEpoch.Offset);
    }

#if NET6_0_OR_GREATER
    [Fact]
    public void DateTime_DateTimeOffset_Deconstruct_DateOnly_TimeOnly()
    {
        var dateTime = new DateTime(2024, 5, 15, 12, 30, 45);
        var (date, time) = dateTime;
        Assert.Equal(new DateOnly(2024, 5, 15), date);
        Assert.Equal(new TimeOnly(12, 30, 45), time);

        var dateTimeOffset = new DateTimeOffset(dateTime, TimeSpan.FromHours(5));
        var (offsetDate, offsetTime, offset) = dateTimeOffset;
        Assert.Equal(date, offsetDate);
        Assert.Equal(time, offsetTime);
        Assert.Equal(TimeSpan.FromHours(5), offset);
    }

    [Fact]
    public void DateOnly_TryFormat()
    {
        var date = new DateOnly(2024, 5, 15);

        Span<char> chars = stackalloc char[10];
        Assert.True(date.TryFormat(chars, out var charsWritten, "yyyy-MM-dd", CultureInfo.InvariantCulture));
        Assert.Equal(10, charsWritten);
        Assert.Equal("2024-05-15", chars.ToString());
        Assert.False(date.TryFormat(chars[..9], out charsWritten, "yyyy-MM-dd", CultureInfo.InvariantCulture));
        Assert.Equal(0, charsWritten);

        Span<byte> utf8 = stackalloc byte[10];
        Assert.True(date.TryFormat(utf8, out var bytesWritten, "yyyy-MM-dd", CultureInfo.InvariantCulture));
        Assert.Equal(10, bytesWritten);
        Assert.Equal("2024-05-15", Encoding.UTF8.GetString(utf8));
        Assert.False(date.TryFormat(utf8[..9], out bytesWritten, "yyyy-MM-dd", CultureInfo.InvariantCulture));
        Assert.Equal(0, bytesWritten);
    }

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

    [Fact]
    public void Guid_CreateVersion7_ReturnsVersion7Guid()
    {
        var guid = Guid.CreateVersion7();
        var str = guid.ToString("D");

        // Version should be 7 (character at position 14 in "D" format: xxxxxxxx-xxxx-Vxxx-...)
        Assert.Equal('7', str[14]);

        // Variant should be 0b10xx (8, 9, a, or b) at position 19
        Assert.Contains(str[19], new[] { '8', '9', 'a', 'b' });
    }

    [Fact]
    public void Guid_CreateVersion7_IsSortable()
    {
        // UUID v7 GUIDs with different (1ms apart) timestamps should sort by timestamp
        var timestamp1 = new DateTimeOffset(2024, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
        var timestamp2 = new DateTimeOffset(2024, 1, 1, 0, 0, 0, 1, TimeSpan.Zero);

        var guid1 = Guid.CreateVersion7(timestamp1);
        var guid2 = Guid.CreateVersion7(timestamp2);

        Assert.True(string.Compare(guid1.ToString("D"), guid2.ToString("D"), StringComparison.Ordinal) < 0);
    }

    [Fact]
    public void Guid_CreateVersion7_WithTimestamp_ReturnsVersion7Guid()
    {
        var timestamp = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var guid = Guid.CreateVersion7(timestamp);
        var str = guid.ToString("D");

        // Version should be 7
        Assert.Equal('7', str[14]);

        // Variant should be 0b10xx (8, 9, a, or b)
        Assert.Contains(str[19], new[] { '8', '9', 'a', 'b' });
    }

    [Fact]
    public void Guid_CreateVersion7_WithTimestamp_EmbedsTimestamp()
    {
        var timestamp = new DateTimeOffset(2024, 6, 15, 12, 30, 45, 123, TimeSpan.Zero);
        var guid1 = Guid.CreateVersion7(timestamp);
        var guid2 = Guid.CreateVersion7(timestamp);

        // Both GUIDs created with the same timestamp should have identical timestamp portions
        // (first 12 hex chars in "N" format = 48-bit timestamp)
        var str1 = guid1.ToString("N");
        var str2 = guid2.ToString("N");
        Assert.True(str1.AsSpan(0, 12).SequenceEqual(str2.AsSpan(0, 12)));
    }

    [Fact]
    public void Guid_CreateVersion7_WithTimestamp_TimestampOrderPreserved()
    {
        var timestamp1 = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var timestamp2 = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);

        var guid1 = Guid.CreateVersion7(timestamp1);
        var guid2 = Guid.CreateVersion7(timestamp2);

        // GUIDs with earlier timestamps should sort before GUIDs with later timestamps
        Assert.True(string.Compare(guid1.ToString("D"), guid2.ToString("D"), StringComparison.Ordinal) < 0);
    }

    [Fact]
    public void Guid_CreateVersion7_WithTimestamp_OutOfRange_ThrowsArgumentOutOfRangeException()
    {
        // Timestamp before Unix epoch
        var beforeEpoch = new DateTimeOffset(1969, 12, 31, 23, 59, 59, TimeSpan.Zero);
        Assert.Throws<ArgumentOutOfRangeException>(() => Guid.CreateVersion7(beforeEpoch));
    }

    [Fact]
    public void Uri_EscapeDataString_ReadOnlySpan()
    {
        Assert.Equal("hello", Uri.EscapeDataString("hello".AsSpan()));
        Assert.Equal("hello%20world", Uri.EscapeDataString("hello world".AsSpan()));
        Assert.Equal("", Uri.EscapeDataString(ReadOnlySpan<char>.Empty));
        Assert.Equal("%26%3D%3F", Uri.EscapeDataString("&=?".AsSpan()));
    }

    [Fact]
    public void Uri_UnescapeDataString_ReadOnlySpan()
    {
        Assert.Equal("hello", Uri.UnescapeDataString("hello".AsSpan()));
        Assert.Equal("hello world", Uri.UnescapeDataString("hello%20world".AsSpan()));
        Assert.Equal("", Uri.UnescapeDataString(ReadOnlySpan<char>.Empty));
        Assert.Equal("&=?", Uri.UnescapeDataString("%26%3D%3F".AsSpan()));
    }

    [Fact]
    public void Uri_TryEscapeDataString()
    {
        Span<char> buffer = stackalloc char[128];

        Assert.True(Uri.TryEscapeDataString("hello world".AsSpan(), buffer, out var written));
        Assert.Equal("hello%20world", buffer[..written].ToString());

        Assert.False(Uri.TryEscapeDataString("hello world".AsSpan(), buffer[..1], out written));
        Assert.Equal(0, written);
    }

    [Fact]
    public void Uri_TryUnescapeDataString()
    {
        Span<char> buffer = stackalloc char[128];

        Assert.True(Uri.TryUnescapeDataString("hello%20world".AsSpan(), buffer, out var written));
        Assert.Equal("hello world", buffer[..written].ToString());

        Assert.False(Uri.TryUnescapeDataString("%20suffix".AsSpan(), buffer[..1], out written));
        Assert.Equal(0, written);
    }

    [Fact]
    public void Boolean_TryFormat()
    {
        Span<char> buffer = stackalloc char[5];

        Assert.True(true.TryFormat(buffer, out var written));
        Assert.Equal(4, written);
        Assert.Equal("True", buffer[..written].ToString());

        Assert.False(false.TryFormat(buffer[..4], out written));
        Assert.Equal(0, written);
    }

    [Fact]
    public void Byte_TryFormat_Span_Byte()
    {
        Span<byte> buffer = stackalloc byte[2];

        Assert.True(((byte)15).TryFormat(buffer, out var written, "X2", CultureInfo.InvariantCulture));
        Assert.Equal(2, written);
        Assert.Equal("0F", Encoding.UTF8.GetString(buffer[..written]));

        Assert.False(((byte)255).TryFormat(buffer[..1], out written, "D3", CultureInfo.InvariantCulture));
        Assert.Equal(0, written);
    }

    [Fact]
    public void Byte_TryFormat_Span_Char()
    {
        Span<char> buffer = stackalloc char[2];

        Assert.True(((byte)15).TryFormat(buffer, out var written, "X2", CultureInfo.InvariantCulture));
        Assert.Equal(2, written);
        Assert.Equal("0F", buffer[..written].ToString());

        Assert.False(((byte)255).TryFormat(buffer[..1], out written, "D3", CultureInfo.InvariantCulture));
        Assert.Equal(0, written);
    }

    [Fact]
    public void Byte_TryParse_WithoutNumberStyles()
    {
        Assert.True(byte.TryParse("123"u8, out var utf8Result));
        Assert.Equal(123, utf8Result);
        Assert.False(byte.TryParse("256"u8, out utf8Result));
        Assert.Equal(0, utf8Result);

        Assert.True(byte.TryParse("42".AsSpan(), out var charResult));
        Assert.Equal(42, charResult);
        Assert.False(byte.TryParse("invalid".AsSpan(), out charResult));
        Assert.Equal(0, charResult);

        Assert.True(byte.TryParse("17", CultureInfo.InvariantCulture, out var stringResult));
        Assert.Equal(17, stringResult);
        Assert.False(byte.TryParse((string?)null, CultureInfo.InvariantCulture, out stringResult));
        Assert.Equal(0, stringResult);
    }

    [Fact]
    public void Capture_ValueSpan()
    {
        var capture = Regex.Match("abc123", @"\d+", RegexOptions.None, TimeSpan.FromSeconds(1));

        Assert.Equal("123", capture.ValueSpan.ToString());
    }

    [Fact]
    public void Char_Equals_StringComparison()
    {
        Assert.True('a'.Equals('A', StringComparison.OrdinalIgnoreCase));
        Assert.False('a'.Equals('A', StringComparison.Ordinal));
        Assert.False('a'.Equals('b', StringComparison.OrdinalIgnoreCase));
        Assert.Throws<ArgumentException>(() => 'a'.Equals('a', (StringComparison)(-1)));
    }

    [Fact]
    public void Char_IsAscii()
    {
        Assert.True(char.IsAscii('\0'));
        Assert.True(char.IsAscii('\x7F'));
        Assert.False(char.IsAscii('\x80'));
    }

    [Fact]
    public void Char_IsAsciiDigit()
    {
        Assert.True(char.IsAsciiDigit('0'));
        Assert.True(char.IsAsciiDigit('9'));
        Assert.False(char.IsAsciiDigit('/'));
        Assert.False(char.IsAsciiDigit(':'));
        Assert.False(char.IsAsciiDigit('\u0660'));
    }

    [Fact]
    public void Char_IsAsciiHexDigit()
    {
        Assert.True(char.IsAsciiHexDigit('0'));
        Assert.True(char.IsAsciiHexDigit('9'));
        Assert.True(char.IsAsciiHexDigit('A'));
        Assert.True(char.IsAsciiHexDigit('F'));
        Assert.True(char.IsAsciiHexDigit('a'));
        Assert.True(char.IsAsciiHexDigit('f'));
        Assert.False(char.IsAsciiHexDigit('G'));
        Assert.False(char.IsAsciiHexDigit('g'));
    }

    [Fact]
    public void Char_IsAsciiHexDigitLower()
    {
        Assert.True(char.IsAsciiHexDigitLower('0'));
        Assert.True(char.IsAsciiHexDigitLower('a'));
        Assert.True(char.IsAsciiHexDigitLower('f'));
        Assert.False(char.IsAsciiHexDigitLower('A'));
        Assert.False(char.IsAsciiHexDigitLower('g'));
    }

    [Fact]
    public void Char_IsAsciiHexDigitUpper()
    {
        Assert.True(char.IsAsciiHexDigitUpper('0'));
        Assert.True(char.IsAsciiHexDigitUpper('A'));
        Assert.True(char.IsAsciiHexDigitUpper('F'));
        Assert.False(char.IsAsciiHexDigitUpper('a'));
        Assert.False(char.IsAsciiHexDigitUpper('G'));
    }

    [Fact]
    public void Char_IsAsciiLetter()
    {
        Assert.True(char.IsAsciiLetter('A'));
        Assert.True(char.IsAsciiLetter('Z'));
        Assert.True(char.IsAsciiLetter('a'));
        Assert.True(char.IsAsciiLetter('z'));
        Assert.False(char.IsAsciiLetter('@'));
        Assert.False(char.IsAsciiLetter('['));
        Assert.False(char.IsAsciiLetter('\u00E9'));
    }

    [Fact]
    public void Char_IsAsciiLetterLower()
    {
        Assert.True(char.IsAsciiLetterLower('a'));
        Assert.True(char.IsAsciiLetterLower('z'));
        Assert.False(char.IsAsciiLetterLower('A'));
        Assert.False(char.IsAsciiLetterLower('{'));
    }

    [Fact]
    public void Char_IsAsciiLetterOrDigit()
    {
        Assert.True(char.IsAsciiLetterOrDigit('0'));
        Assert.True(char.IsAsciiLetterOrDigit('A'));
        Assert.True(char.IsAsciiLetterOrDigit('z'));
        Assert.False(char.IsAsciiLetterOrDigit('_'));
        Assert.False(char.IsAsciiLetterOrDigit('\u00E9'));
    }

    [Fact]
    public void Char_IsAsciiLetterUpper()
    {
        Assert.True(char.IsAsciiLetterUpper('A'));
        Assert.True(char.IsAsciiLetterUpper('Z'));
        Assert.False(char.IsAsciiLetterUpper('a'));
        Assert.False(char.IsAsciiLetterUpper('@'));
    }

    [Fact]
    public void Char_IsBetween()
    {
        Assert.True(char.IsBetween('a', 'a', 'z'));
        Assert.True(char.IsBetween('z', 'a', 'z'));
        Assert.True(char.IsBetween('m', 'a', 'z'));
        Assert.False(char.IsBetween('A', 'a', 'z'));
        Assert.False(char.IsBetween('m', 'z', 'a'));
    }

    [Fact]
    public void Console_OpenStandardHandles_DoNotOwnUnderlyingHandles()
    {
        AssertDoesNotOwnUnderlyingHandle(Console.OpenStandardInputHandle, expectedUnixHandle: 0);
        AssertDoesNotOwnUnderlyingHandle(Console.OpenStandardOutputHandle, expectedUnixHandle: 1);
        AssertDoesNotOwnUnderlyingHandle(Console.OpenStandardErrorHandle, expectedUnixHandle: 2);

        static void AssertDoesNotOwnUnderlyingHandle(Func<Microsoft.Win32.SafeHandles.SafeFileHandle> openHandle, int expectedUnixHandle)
        {
            var handle = openHandle();
            var rawHandle = handle.DangerousGetHandle();

            if (!OperatingSystem.IsWindows())
                Assert.Equal(new IntPtr(expectedUnixHandle), rawHandle);

            Assert.False(handle.IsClosed);
            handle.Dispose();
            Assert.True(handle.IsClosed);

            using var reopenedHandle = openHandle();
            Assert.Equal(rawHandle, reopenedHandle.DangerousGetHandle());
        }
    }

    [Fact]
    public void GC_AllocateUninitializedArray()
    {
        Assert.Equal(4, GC.AllocateUninitializedArray<int>(4).Length);
        Assert.Equal(4, GC.AllocateUninitializedArray<string>(4, pinned: true).Length);
        Assert.Throws<OverflowException>(() => GC.AllocateUninitializedArray<int>(-1));
    }

    [Fact]
    public void Numeric_TryFormat_And_ProviderlessUtf8TryParse()
    {
        Span<byte> bytes = stackalloc byte[32];
        Span<char> chars = stackalloc char[32];

        Assert.True(123.5d.TryFormat(bytes, out var written, "F1", CultureInfo.InvariantCulture));
        Assert.Equal("123.5", Encoding.UTF8.GetString(bytes[..written]));
        Assert.True(123.5f.TryFormat(chars, out written, "F1", CultureInfo.InvariantCulture));
        Assert.Equal("123.5", chars[..written].ToString());
        Assert.True(((sbyte)-12).TryFormat(bytes, out _, default, CultureInfo.InvariantCulture));
        Assert.True(123u.TryFormat(chars, out _, default, CultureInfo.InvariantCulture));
        Assert.True(123ul.TryFormat(bytes, out _, default, CultureInfo.InvariantCulture));
        Assert.True(123.TryFormat(chars, out _, default, CultureInfo.InvariantCulture));
        Assert.True(123L.TryFormat(bytes, out _, default, CultureInfo.InvariantCulture));

#pragma warning disable MA0011 // Test providerless overloads
        Assert.True(double.TryParse("123.5"u8, out _));
        Assert.True(float.TryParse("123.5"u8, out _));
        Assert.True(sbyte.TryParse("-12"u8, out _));
        Assert.True(uint.TryParse("123"u8, out _));
        Assert.True(ulong.TryParse("123"u8, out _));
        Assert.True(int.TryParse("123"u8, out _));
        Assert.True(long.TryParse("123"u8, out _));
#pragma warning restore MA0011
    }

    [Fact]
    public void Guid_NewMembers()
    {
        var guid = Guid.NewGuid();
        Span<byte> bytes = stackalloc byte[36];
        Span<char> chars = stackalloc char[36];
        Assert.True(guid.TryFormat(bytes, out var written, "D"));
        Assert.Equal(guid.ToString("D"), Encoding.UTF8.GetString(bytes[..written]));
        Assert.True(guid.TryFormat(chars, out written, "D"));
        Assert.Equal(guid.ToString("D"), chars[..written].ToString());
        Assert.Equal(guid, Guid.Parse(bytes));
        Assert.True(Guid.TryParse(bytes, out _));
        Assert.True(Guid.TryParse(chars, out _));
        Assert.True(Guid.TryParse(chars, CultureInfo.InvariantCulture, out _));
        Assert.True(Guid.TryParse(guid.ToString(), CultureInfo.InvariantCulture, out _));
        Assert.True(Guid.TryParseExact(chars, "D", out _));
        Assert.Equal("ffffffff-ffff-ffff-ffff-ffffffffffff", Guid.AllBitsSet.ToString());
    }

    [Fact]
    public void TimeSpan_NewFactories_And_TryFormat()
    {
        Assert.Equal(TimeSpan.FromTicks(10), TimeSpan.FromMicroseconds(1));
        Assert.Equal(TimeSpan.FromTicks(10_010), TimeSpan.FromMilliseconds(1, 1));
        Assert.Equal(TimeSpan.FromTicks(TimeSpan.TicksPerSecond + 10_010), TimeSpan.FromSeconds(1, 1, 1));
        Assert.Equal(TimeSpan.FromTicks(TimeSpan.TicksPerMinute + TimeSpan.TicksPerSecond + 10_010), TimeSpan.FromMinutes(1, 1, 1, 1));
        Assert.Equal(TimeSpan.FromTicks(TimeSpan.TicksPerHour + TimeSpan.TicksPerMinute + TimeSpan.TicksPerSecond + 10_010), TimeSpan.FromHours(1, 1, 1, 1, 1));
        Assert.Equal(TimeSpan.FromTicks(TimeSpan.TicksPerDay + TimeSpan.TicksPerHour + TimeSpan.TicksPerMinute + TimeSpan.TicksPerSecond + 10_010), TimeSpan.FromDays(1, 1, 1, 1, 1, 1));

        Span<char> chars = stackalloc char[32];
        Span<byte> bytes = stackalloc byte[32];
        Assert.True(TimeSpan.FromSeconds(1).TryFormat(chars, out _, "c", CultureInfo.InvariantCulture));
        Assert.True(TimeSpan.FromSeconds(1).TryFormat(bytes, out _, "c", CultureInfo.InvariantCulture));
    }

    [Fact]
    public void BitConverter_UnsignedBitConversions()
    {
        const double DoubleValue = -123.5;
        Assert.Equal(DoubleValue, BitConverter.UInt64BitsToDouble(BitConverter.DoubleToUInt64Bits(DoubleValue)));
        Assert.Equal(123.5f, BitConverter.UInt32BitsToSingle(BitConverter.ToUInt32(BitConverter.GetBytes(123.5f), 0)));
    }

    [Fact]
    public void Pointer_And_IPAddress_TryParse()
    {
#pragma warning disable MA0011 // Test providerless overloads
        Assert.True(UIntPtr.TryParse("123"u8, out var unsignedValue));
#pragma warning restore MA0011
        Assert.Equal(new UIntPtr(123), unsignedValue);
        Assert.True(UIntPtr.TryParse("7B".AsSpan(), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out unsignedValue));
        Assert.Equal(new UIntPtr(123), unsignedValue);
#pragma warning disable MA0011 // Test providerless overloads
        Assert.True(IntPtr.TryParse("-123"u8, out var signedValue));
#pragma warning restore MA0011
        Assert.Equal(new IntPtr(-123), signedValue);
        Assert.True(IntPtr.TryParse("-123", CultureInfo.InvariantCulture, out _));

        Assert.Equal(System.Net.IPAddress.Loopback, System.Net.IPAddress.Parse("127.0.0.1".AsSpan()));
        Assert.True(System.Net.IPAddress.TryParse("127.0.0.1"u8, out _));
        Assert.True(System.Net.IPAddress.TryParse("127.0.0.1".AsSpan(), out _));
    }

    [Fact]
    public void DefaultInterpolatedStringHandler_Clear()
    {
        var handler = new DefaultInterpolatedStringHandler(3, 0);
        handler.AppendLiteral("abc");
        handler.Clear();
        Assert.Equal("", handler.ToString());
    }

    [Fact]
    public void Int16_UInt16_TryFormat_And_Utf8TryParse()
    {
        Span<byte> bytes = stackalloc byte[16];
        Span<char> chars = stackalloc char[16];
        Assert.True(((short)-123).TryFormat(bytes, out var written, default, CultureInfo.InvariantCulture));
        Assert.Equal("-123", Encoding.UTF8.GetString(bytes[..written]));
        Assert.True(((ushort)123).TryFormat(chars, out written, default, CultureInfo.InvariantCulture));
        Assert.Equal("123", chars[..written].ToString());

#pragma warning disable MA0011 // Test providerless overloads
        Assert.True(short.TryParse("-123"u8, out var signed));
        Assert.True(ushort.TryParse("123"u8, out var unsigned));
#pragma warning restore MA0011
        Assert.Equal(-123, signed);
        Assert.Equal(123, unsigned);
    }

    [Fact]
    public void Random_NewMembers()
    {
#pragma warning disable CA5394 // Test System.Random polyfills
        var random = new Random(42);
        Assert.Equal(16, random.GetHexString(16).Length);
        Assert.All(random.GetHexString(16, lowercase: true), value => Assert.Contains(value, "0123456789abcdef"));
        Span<char> hex = stackalloc char[16];
        random.GetHexString(hex, lowercase: true);
        Assert.All(hex.ToArray(), value => Assert.Contains(value, "0123456789abcdef"));
        Assert.Equal(20, random.GetString("abc", 20).Length);
        Assert.InRange(random.NextInt64(), 0, long.MaxValue - 1);
        Assert.InRange(random.NextInt64(10), 0, 9);
        Assert.InRange(random.NextInt64(-10, 10), -10, 9);
        var single = random.NextSingle();
        Assert.True(single >= 0f);
        Assert.True(single < 1f);
#pragma warning restore CA5394
    }

}
