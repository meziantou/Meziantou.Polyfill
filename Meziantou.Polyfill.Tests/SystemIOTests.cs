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

public class SystemIOTests
{
    #if NET461_OR_GREATER || NETCOREAPP
        [Fact]
        public async Task StreamWriter_WriteAsync()
        {
            using var sr = new System.IO.StringWriter();
            await sr.WriteAsync("test".AsMemory(), CancellationToken.None);
            Assert.Equal("test", sr.ToString());
        }
    #endif

    #if NET461_OR_GREATER || NETCOREAPP
        [Fact]
        public void StreamWriter_Write()
        {
            using var ms = new MemoryStream();
            ms.Write([1, 2]);
            Assert.Equal([1, 2], ms.ToArray());
        }
    #endif
    
        [Fact]
        public async Task StreamReader_ReadToEndAsync()
        {
            using var sr = new StringReader("test");
            var result = await sr.ReadToEndAsync(CancellationToken.None);
            Assert.Equal("test", result);
        }

    #if NET461_OR_GREATER || NETCOREAPP
        [Fact]
        public async Task StreamReader_ReadAsync()
        {
            using var sr = new StringReader("test");
            var buffer = new char[2];
            var result = await sr.ReadAsync(buffer.AsMemory(), CancellationToken.None);
            Assert.Equal(2, result);
            Assert.Equal("te", new string(buffer));
        }

        [Fact]
        public async Task StreamReader_ReadAsync2()
        {
            using var sr = new MemoryStream([3, 4, 5]);
            var buffer = new byte[3];
            buffer[0] = 1;
            var result = await sr.ReadAsync(buffer.AsMemory()[1..], CancellationToken.None);
            Assert.Equal(2, result);
            Assert.Equal([1, 3, 4], buffer);
        }

        [Fact]
        public async Task StreamReader_ReadLineAsync()
        {
            using var ms = new MemoryStream(Encoding.UTF8.GetBytes("ab\ncd"));
            using var reader = new StreamReader(ms, Encoding.UTF8);
    
            Assert.Equal("ab", await reader.ReadLineAsync());
            Assert.Equal("cd", await reader.ReadLineAsync(CancellationToken.None));
            Assert.Null(await reader.ReadLineAsync(CancellationToken.None));
        }

        [Fact]
        public void Stream_ReadAtLeast()
        {
            using var ms = new MemoryStream([1, 2, 3, 4]);
    
            var buffer = new byte[4];
            Assert.Equal(4, ms.ReadAtLeast(buffer.AsSpan(), 4));
            Assert.Equal([1, 2, 3, 4], buffer);
        }

        [Fact]
        public async Task Stream_ReadAtLeastAsync()
        {
            using var ms = new MemoryStream([1, 2, 3, 4]);
    
            var buffer = new byte[4];
            Assert.Equal(4, await ms.ReadAtLeastAsync(buffer.AsMemory(), 4));
            Assert.Equal([1, 2, 3, 4], buffer);
        }

        [Fact]
        public async Task File_WriteAllTextAsync_ReadOnlyMemory()
        {
            var tempFile = Path.GetTempFileName();
            try
            {
                // Write with default encoding (UTF8 without BOM)
                ReadOnlyMemory<char> content = "Hello, World!".AsMemory();
                await File.WriteAllTextAsync(tempFile, content);
    
                var result = File.ReadAllText(tempFile);
                Assert.Equal("Hello, World!", result);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }
    #endif

}