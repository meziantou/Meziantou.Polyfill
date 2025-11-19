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

public class SystemNetHttpTests
{
        [Fact]
        public async Task ReadOnlyMemoryContent()
        {
            using var content = new ReadOnlyMemoryContent(new byte[] { 1, 2 });
            var ms = new MemoryStream();
            await content.CopyToAsync(ms);
    
            Assert.Equal([1, 2], ms.ToArray());
        }

        [Fact]
        public void HttpContent_ReadAsStream()
        {
            using var content = new ByteArrayContent([1, 2]);
            var stream = content.ReadAsStream();
    
            var streamContent = new MemoryStream();
            stream.CopyTo(streamContent);
    
            Assert.Equal([1, 2], streamContent.ToArray());
        }

        [Fact]
        public async Task HttpContent_ReadAsStringAsync()
        {
            var content = new StringContent("dummy");
            Assert.Equal("dummy", await content.ReadAsStringAsync(CancellationToken.None));
        }

        [Fact]
        public void HttpMethod_Query()
        {
            var queryMethod = HttpMethod.Query;
            Assert.NotNull(queryMethod);
            Assert.Equal("QUERY", queryMethod.Method);
        }

}