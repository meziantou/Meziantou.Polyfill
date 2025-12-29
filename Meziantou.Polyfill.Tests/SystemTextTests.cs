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
