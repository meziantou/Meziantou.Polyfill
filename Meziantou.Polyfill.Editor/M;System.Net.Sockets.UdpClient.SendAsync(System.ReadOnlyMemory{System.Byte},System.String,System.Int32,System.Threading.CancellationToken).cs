using System;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<int> SendAsync(this System.Net.Sockets.UdpClient client, ReadOnlyMemory<byte> datagram, string? hostname, int port, CancellationToken cancellationToken = default)
    {
        return new(client.SendAsync(datagram.ToArray(), datagram.Length, hostname, port));
    }
}
