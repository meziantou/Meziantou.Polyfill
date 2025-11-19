using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
