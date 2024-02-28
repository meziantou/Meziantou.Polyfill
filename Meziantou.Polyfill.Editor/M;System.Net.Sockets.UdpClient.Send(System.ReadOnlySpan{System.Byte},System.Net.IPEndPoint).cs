using System;
using System.Net;

static partial class PolyfillExtensions
{
	public static int Send(this System.Net.Sockets.UdpClient client, ReadOnlySpan<byte> datagram, IPEndPoint? endPoint)
	{
		return client.Send(datagram.ToArray(), datagram.Length, endPoint);
	}
}
