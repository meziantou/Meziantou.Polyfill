using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Int64
{
    extension(long)
    {
        public static long Parse(ReadOnlySpan<byte> utf8Text, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null) => long.Parse(Encoding.UTF8.GetString(utf8Text), style, provider);
    }
}