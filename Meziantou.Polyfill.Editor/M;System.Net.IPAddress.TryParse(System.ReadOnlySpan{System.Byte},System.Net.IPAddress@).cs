using System;
using System.Net;
using System.Text;
static partial class PolyfillExtensions_IPAddress
{
    extension(IPAddress)
    {
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, out IPAddress? address) => IPAddress.TryParse(Encoding.UTF8.GetString(utf8Text), out address);
    }
}
