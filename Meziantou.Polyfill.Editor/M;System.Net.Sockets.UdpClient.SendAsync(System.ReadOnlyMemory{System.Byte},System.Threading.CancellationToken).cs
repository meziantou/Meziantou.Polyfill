using System.Threading.Tasks;
using System.Threading;
using System;

static partial class PolyfillExtensions
{
    public static async ValueTask<int> SendAsync(this System.Net.Sockets.UdpClient client, ReadOnlyMemory<byte> datagram, CancellationToken cancellationToken = default)
    {
        return await client.SendAsync(datagram.ToArray(), datagram.Length).WaitAsync(cancellationToken);
    }
}
