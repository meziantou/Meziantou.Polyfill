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

}
