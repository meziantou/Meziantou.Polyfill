#pragma warning disable CA1307
#pragma warning disable CA1837
#pragma warning disable CA1849
#pragma warning disable CA2000
#pragma warning disable CA2264
#pragma warning disable CA5351
#pragma warning disable MA0001
#pragma warning disable MA0002
#pragma warning disable MA0015
#pragma warning disable MA0021
#pragma warning disable MA0074
#pragma warning disable MA0131
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public class SystemBuffersTests
{
        [Fact]
        public void System_Buffers_SearchValues_Create_Byte()
        {
            ReadOnlySpan<byte> span = new byte[] { 1, 2, 3, 4, 5 };
            var values = System.Buffers.SearchValues.Create(span);
            Assert.NotNull(values);
        }

        [Fact]
        public void System_Buffers_SearchValues_Create_Char()
        {
            ReadOnlySpan<char> span = "abcde".AsSpan();
            var values = System.Buffers.SearchValues.Create(span);
            Assert.NotNull(values);
        }

    #if NET9_0_OR_GREATER
        [Fact]
        public void System_Buffers_SearchValues_Create_String_Ordinal()
        {
            ReadOnlySpan<string> span = new[] { "hello", "world", "test" };
            var values = System.Buffers.SearchValues.Create(span, StringComparison.Ordinal);
            Assert.NotNull(values);
        }

        [Fact]
        public void System_Buffers_SearchValues_Create_String_OrdinalIgnoreCase()
        {
            ReadOnlySpan<string> span = new[] { "hello", "world", "test" };
            var values = System.Buffers.SearchValues.Create(span, StringComparison.OrdinalIgnoreCase);
            Assert.NotNull(values);
        }
    #endif

}