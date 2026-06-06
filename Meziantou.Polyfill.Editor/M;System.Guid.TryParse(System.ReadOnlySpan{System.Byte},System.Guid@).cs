using System;
using System.Text;

static partial class PolyfillExtensions_Guid
{
    extension(Guid)
    {
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, out Guid result) => Guid.TryParse(Encoding.UTF8.GetString(utf8Text), out result);
    }
}
