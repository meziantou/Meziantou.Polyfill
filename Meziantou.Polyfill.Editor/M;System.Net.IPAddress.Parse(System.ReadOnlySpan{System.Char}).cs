using System;
using System.Net;
static partial class PolyfillExtensions_IPAddress
{
    extension(IPAddress)
    {
        public static IPAddress Parse(ReadOnlySpan<char> ipString) => IPAddress.Parse(ipString.ToString());
    }
}
