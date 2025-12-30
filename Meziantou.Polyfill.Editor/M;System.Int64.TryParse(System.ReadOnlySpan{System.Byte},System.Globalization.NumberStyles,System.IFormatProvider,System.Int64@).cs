using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Int64
{
    extension(long)
    {
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out long result) => long.TryParse(Encoding.UTF8.GetString(utf8Text), style, provider, out result);
    }
}