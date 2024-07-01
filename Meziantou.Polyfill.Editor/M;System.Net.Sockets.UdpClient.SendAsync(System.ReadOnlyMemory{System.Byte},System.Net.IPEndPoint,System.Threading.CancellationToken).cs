using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
	public async static ValueTask<int> SendAsync(this System.Net.Sockets.UdpClient client, ReadOnlyMemory<byte> datagram, IPEndPoint? endPoint, CancellationToken cancellationToken = default)
	{
		return await client.SendAsync(datagram.ToArray(), datagram.Length, endPoint).WaitAsync(cancellationToken);
	}
}
