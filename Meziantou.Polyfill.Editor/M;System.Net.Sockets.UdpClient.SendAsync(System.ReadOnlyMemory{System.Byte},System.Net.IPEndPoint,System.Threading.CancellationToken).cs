using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<int> SendAsync(this System.Net.Sockets.UdpClient client, ReadOnlyMemory<byte> datagram, IPEndPoint? endPoint, CancellationToken cancellationToken = default)
    {
        return new(client.SendAsync(datagram.ToArray(), datagram.Length, endPoint));
    }
}
