using System.Threading.Tasks;
using System.Threading;
using System;

static partial class PolyfillExtensions
{
    public static ValueTask<int> SendAsync(this System.Net.Sockets.UdpClient client, ReadOnlyMemory<byte> datagram, CancellationToken cancellationToken = default)
    {
        return new(client.SendAsync(datagram.ToArray(), datagram.Length));
    }
}
