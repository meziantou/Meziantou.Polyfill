using System;
using System.Net;
static partial class PolyfillExtensions_IPAddress
{
    extension(IPAddress)
    {
        public static bool TryParse(ReadOnlySpan<char> ipString, out IPAddress? address) => IPAddress.TryParse(ipString.ToString(), out address);
    }
}
