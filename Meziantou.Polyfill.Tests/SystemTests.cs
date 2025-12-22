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

    [Fact]
    public void ArgumentOutOfRangeException_ThrowIfNegative_Int32()
    {
#if NET7_0_OR_GREATER
        var sample = -1;
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentOutOfRangeException.ThrowIfNegative(sample));
        Assert.Equal("sample", ex.ParamName);

        var value = 0;
        ArgumentOutOfRangeException.ThrowIfNegative(value);
        value = 5;
        ArgumentOutOfRangeException.ThrowIfNegative(value);
#endif
    }

    [Fact]
    public void ArgumentOutOfRangeException_ThrowIfNegative_Int64()
    {
#if NET7_0_OR_GREATER
        var sample = -1L;
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentOutOfRangeException.ThrowIfNegative(sample));
        Assert.Equal("sample", ex.ParamName);

        var value = 0L;
        ArgumentOutOfRangeException.ThrowIfNegative(value);
        value = 5L;
        ArgumentOutOfRangeException.ThrowIfNegative(value);
#endif
    }

    [Fact]
    public void ArgumentOutOfRangeException_ThrowIfNegative_Double()
    {
#if NET7_0_OR_GREATER
        var sample = -1.0;
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentOutOfRangeException.ThrowIfNegative(sample));
        Assert.Equal("sample", ex.ParamName);

        var value = 0.0;
        ArgumentOutOfRangeException.ThrowIfNegative(value);
        value = 5.0;
        ArgumentOutOfRangeException.ThrowIfNegative(value);
#endif
    }

    [Fact]
    public void ArgumentOutOfRangeException_ThrowIfNegative_Decimal()
    {
#if NET7_0_OR_GREATER
        var sample = -1m;
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentOutOfRangeException.ThrowIfNegative(sample));
        Assert.Equal("sample", ex.ParamName);

        var value = 0m;
        ArgumentOutOfRangeException.ThrowIfNegative(value);
        value = 5m;
        ArgumentOutOfRangeException.ThrowIfNegative(value);
#endif
    }

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
    public void String_Create()
    {
        var actual = string.Create(CultureInfo.InvariantCulture, $"a{1}b");
        Assert.Equal("a1b", actual);
    }

}
