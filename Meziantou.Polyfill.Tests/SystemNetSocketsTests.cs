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

public class SystemNetSocketsTests
{
        [Fact]
        public void UdpClient()
        {
            int port = 1024;
    
            UdpClient CreateUdpClient()
            {
                while (true)
                {
                    try
                    {
                        return new UdpClient(port);
                    }
                    catch
                    {
                        port++;
                        if (port >= ushort.MaxValue)
                            throw;
                    }
                }
            }
    
            using UdpClient client = CreateUdpClient();
            using UdpClient server = new();
    
            ReadOnlySpan<byte> data = [1, 2, 3];
            server.Send(data, "localhost", port);
            IPEndPoint endpoint = new(IPAddress.Any, 0);
            var result = client.Receive(ref endpoint);
            Assert.Equal(data.ToArray(), result);
        }

        [Fact]
        public async Task UdpClientAsync()
        {
            int port = 1024;
    
            UdpClient CreateUdpClient()
            {
                while (true)
                {
                    try
                    {
                        return new UdpClient(port);
                    }
                    catch
                    {
                        port++;
                        if (port >= ushort.MaxValue)
                            throw;
                    }
                }
            }
    
            using UdpClient client = CreateUdpClient();
            using UdpClient server = new();
    
            ReadOnlyMemory<byte> data = new([1, 2, 3]);
            await server.SendAsync(data, "localhost", port);
            IPEndPoint endpoint = new(IPAddress.Any, 0);
            var result = client.Receive(ref endpoint);
            Assert.Equal(data.ToArray(), result);
        }

}