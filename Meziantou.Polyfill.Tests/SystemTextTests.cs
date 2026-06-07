using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

public class SystemTextTests
{
#if NET8_0_OR_GREATER
    [Fact]
    public void Rune_StringMembers()
    {
        var rune = new Rune(0x1F600);
        const string Value = "a😀b😀";

        Assert.True(Value.Contains(rune));
        Assert.True(Value.Contains(new Rune('A'), StringComparison.OrdinalIgnoreCase));
        Assert.True(Value.StartsWith(new Rune('a')));
        Assert.True(Value.EndsWith(rune));
        Assert.Equal(1, Value.IndexOf(rune));
        Assert.Equal(4, Value.IndexOf(rune, 2));
        Assert.Equal(4, Value.LastIndexOf(rune));
        Assert.Equal(1, Value.LastIndexOf(rune, 3));
        Assert.Equal("aXbX", Value.Replace(rune, new Rune('X')));
        Assert.Equal(["a", "b", ""], Value.Split(rune));
        Assert.Equal(["a", "b😀"], Value.Split(rune, 2));
        Assert.Equal("x", "😀😀x😀".Trim(rune));
        Assert.Equal("x😀", "😀😀x😀".TrimStart(rune));
        Assert.Equal("😀😀x", "😀😀x😀".TrimEnd(rune));
    }

    [Fact]
    public void Rune_ComparisonAndTextInfo()
    {
        Assert.True(new Rune('a').Equals(new Rune('A'), StringComparison.OrdinalIgnoreCase));
        Assert.Equal(new Rune('i'), System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToLower(new Rune('I')));
        Assert.Equal(new Rune('I'), System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToUpper(new Rune('i')));
    }

    [Fact]
    public void Rune_StringBuilderMembers()
    {
        var smile = new Rune(0x1F600);
        var builder = new StringBuilder("a");
        builder.Append(smile).Insert(0, smile);
        Assert.Equal("😀a😀", builder.ToString());
        Assert.Equal(smile, builder.GetRuneAt(0));
        Assert.True(builder.TryGetRuneAt(3, out var value));
        Assert.Equal(smile, value);
        Assert.Throws<ArgumentOutOfRangeException>(() => builder.TryGetRuneAt(-1, out _));
        Assert.False(new StringBuilder("\uD800").TryGetRuneAt(0, out _));

        builder.Replace(smile, new Rune('X'));
        Assert.Equal("XaX", builder.ToString());

        var runes = new List<Rune>();
        foreach (var rune in new StringBuilder("a😀\uD800").EnumerateRunes())
            runes.Add(rune);
        Assert.Equal([new Rune('a'), smile, Rune.ReplacementChar], runes);
    }

    [Fact]
    public void RunePosition_EnumerateUtf16()
    {
        var items = new List<RunePosition>();
        foreach (var item in RunePosition.EnumerateUtf16("a😀\uD800".AsSpan()))
            items.Add(item);

        Assert.Equal(3, items.Count);
        Assert.Equal(new RunePosition(new Rune('a'), 0, 1, false), items[0]);
        Assert.Equal(new RunePosition(new Rune(0x1F600), 1, 2, false), items[1]);
        Assert.Equal(new RunePosition(Rune.ReplacementChar, 3, 1, true), items[2]);
        var (rune, startIndex, length) = items[1];
        Assert.Equal(new Rune(0x1F600), rune);
        Assert.Equal(1, startIndex);
        Assert.Equal(2, length);
    }

    [Fact]
    public void RunePosition_EnumerateUtf8()
    {
        var items = new List<RunePosition>();
        foreach (var item in RunePosition.EnumerateUtf8(new byte[] { 0x61, 0xF0, 0x9F, 0x98, 0x80, 0xFF }))
            items.Add(item);

        Assert.Equal(3, items.Count);
        Assert.Equal(new RunePosition(new Rune('a'), 0, 1, false), items[0]);
        Assert.Equal(new RunePosition(new Rune(0x1F600), 1, 4, false), items[1]);
        Assert.Equal(new RunePosition(Rune.ReplacementChar, 5, 1, true), items[2]);
    }
#endif

    [Fact]
    public void Encoding_Latin1()
    {
        Assert.Equal(28591, Encoding.Latin1.CodePage);
        Assert.Equal([0x41, 0xE9], Encoding.Latin1.GetBytes("A\u00E9"));
        Assert.Equal("A\u00E9", Encoding.Latin1.GetString([0x41, 0xE9]));
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
    public void Encoding_GetString()
    {
        var str = Encoding.UTF8.GetString((ReadOnlySpan<byte>)Encoding.UTF8.GetBytes("sample").AsSpan());
        Assert.Equal("sample", str);
    }

    [Fact]
    public void Encoding_GetString_Empty()
    {
        var str = Encoding.UTF8.GetString(ReadOnlySpan<byte>.Empty);
        Assert.Equal(string.Empty, str);
    }

    [Fact]
    public void StringBuilder_AppendJoin_String_ObjectArray()
    {
        var sb = new StringBuilder();
        sb.AppendJoin(", ", 1, 2, 3);
        Assert.Equal("1, 2, 3", sb.ToString());
    }

    [Fact]
    public void StringBuilder_AppendJoin_String_StringArray()
    {
        var sb = new StringBuilder();
        sb.AppendJoin(", ", "1", "2", "3");
        Assert.Equal("1, 2, 3", sb.ToString());
    }

    [Fact]
    public void StringBuilder_AppendJoin_String_IEnumerable()
    {
        var sb = new StringBuilder();
        sb.AppendJoin(", ", Enumerable());
        Assert.Equal("1, 2, 3", sb.ToString());

        static IEnumerable<string> Enumerable()
        {
            yield return "1";
            yield return "2";
            yield return "3";
        }
    }

    [Fact]
    public void StringBuilder_AppendJoin_Char_ObjectArray()
    {
        var sb = new StringBuilder();
        sb.AppendJoin(',', 1, 2, 3);
        Assert.Equal("1,2,3", sb.ToString());
    }

    [Fact]
    public void StringBuilder_AppendJoin_Char_StringArray()
    {
        var sb = new StringBuilder();
        sb.AppendJoin(',', "1", "2", "3");
        Assert.Equal("1,2,3", sb.ToString());
    }

    [Fact]
    public void StringBuilder_AppendJoin_Char_IEnumerable()
    {
        var sb = new StringBuilder();
        sb.AppendJoin(',', Enumerable());
        Assert.Equal("1,2,3", sb.ToString());

        static IEnumerable<string> Enumerable()
        {
            yield return "1";
            yield return "2";
            yield return "3";
        }
    }

    [Fact]
    public void StringBuilder_Replace_ReadOnlySpanChar_ReadOnlySpanChar()
    {
        var sb = new StringBuilder("abcd");
        sb.Replace("bc".AsSpan(), "zy".AsSpan());
        Assert.Equal("azyd", sb.ToString());
    }

    [Fact]
    public void StringBuilder_Replace_ReadOnlySpanChar_ReadOnlySpanChar_Int32_Int32()
    {
        var sb = new StringBuilder("abcdbcbc");
        sb.Replace("bc".AsSpan(), "zy".AsSpan(), 2, 5);
        Assert.Equal("abcdzybc", sb.ToString());
    }

    [Fact]
    public void Encoding_GetByteCount_ReadOnlySpan()
    {
        var encoding = Encoding.UTF8;
        var text = "Hello, 世界!";
        var expected = encoding.GetByteCount(text);
        var actual = encoding.GetByteCount(text.AsSpan());
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Encoding_GetBytes_ReadOnlySpan_Span()
    {
        var encoding = Encoding.UTF8;
        var text = "Hello, 世界!";
        var expectedBytes = encoding.GetBytes(text);

        var bytes = new byte[expectedBytes.Length];
        var count = encoding.GetBytes(text.AsSpan(), bytes);

        Assert.Equal(expectedBytes.Length, count);
        for (var i = 0; i < count; i++)
        {
            Assert.Equal(expectedBytes[i], bytes[i]);
        }
    }

    [Fact]
    public void Encoding_GetCharCount_ReadOnlySpan()
    {
        var encoding = Encoding.UTF8;
        var text = "Hello, 世界!";
        var bytes = encoding.GetBytes(text);
        
        var expected = encoding.GetCharCount(bytes);
        var actual = encoding.GetCharCount(bytes.AsSpan());
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Encoding_GetCharCount_Empty()
    {
        var actual = Encoding.UTF8.GetCharCount(ReadOnlySpan<byte>.Empty);
        Assert.Equal(0, actual);
    }

    [Fact]
    public void Encoding_GetChars_ReadOnlySpan_Span()
    {
        var encoding = Encoding.UTF8;
        var text = "Hello, 世界!";
        var bytes = encoding.GetBytes(text);
        var chars = new char[text.Length];
        
        var count = encoding.GetChars(bytes.AsSpan(), chars);
        var result = new string(chars, 0, count);
        
        Assert.Equal(text, result);
    }

    [Fact]
    public void Encoding_TryGetBytes_Success()
    {
        var encoding = Encoding.UTF8;
        var text = "Hello";
        var expectedBytes = encoding.GetBytes(text);
        var bytes = new byte[100];
        
        var success = encoding.TryGetBytes(text.AsSpan(), bytes, out var bytesWritten);
        
        Assert.True(success);
        Assert.Equal(expectedBytes.Length, bytesWritten);
        for (int i = 0; i < bytesWritten; i++)
        {
            Assert.Equal(expectedBytes[i], bytes[i]);
        }
    }

    [Fact]
    public void Encoding_TryGetBytes_DestinationTooSmall()
    {
        var encoding = Encoding.UTF8;
        var text = "Hello, 世界!";
        var bytes = new byte[2];
        
        var success = encoding.TryGetBytes(text.AsSpan(), bytes, out var bytesWritten);
        
        Assert.False(success);
        Assert.Equal(0, bytesWritten);
    }

    [Fact]
    public void Encoding_TryGetChars_Success()
    {
        var encoding = Encoding.UTF8;
        var text = "Hello";
        var bytes = encoding.GetBytes(text);
        var chars = new char[100];
        
        var success = encoding.TryGetChars(bytes.AsSpan(), chars, out var charsWritten);
        
        Assert.True(success);
        Assert.Equal(text.Length, charsWritten);
        Assert.Equal(text, new string(chars, 0, charsWritten));
    }

    [Fact]
    public void Encoding_TryGetChars_DestinationTooSmall()
    {
        var encoding = Encoding.UTF8;
        var text = "Hello, 世界!";
        var bytes = encoding.GetBytes(text);
        var chars = new char[2];
        
        var success = encoding.TryGetChars(bytes.AsSpan(), chars, out var charsWritten);
        
        Assert.False(success);
        Assert.Equal(0, charsWritten);
    }
}
