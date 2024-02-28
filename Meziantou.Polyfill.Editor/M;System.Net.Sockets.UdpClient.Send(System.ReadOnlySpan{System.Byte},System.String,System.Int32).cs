using System;

static partial class PolyfillExtensions
{
    public static int Send(this System.Net.Sockets.UdpClient client, ReadOnlySpan<byte> datagram, string? hostname, int port)
    {
        return client.Send(datagram.ToArray(), datagram.Length, hostname, port);
    }
}
