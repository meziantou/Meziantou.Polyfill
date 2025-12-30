using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_UInt64
{
    extension(ulong)
    {
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out ulong result) => ulong.TryParse(Encoding.UTF8.GetString(utf8Text), style, provider, out result);
    }
}