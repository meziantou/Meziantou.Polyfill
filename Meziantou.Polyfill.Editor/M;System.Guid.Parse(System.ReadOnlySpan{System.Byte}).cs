using System;
using System.Text;

static partial class PolyfillExtensions_Guid
{
    extension(Guid)
    {
        public static Guid Parse(ReadOnlySpan<byte> utf8Text) => Guid.Parse(Encoding.UTF8.GetString(utf8Text));
    }
}
