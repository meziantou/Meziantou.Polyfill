using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Int64
{
    extension(long)
    {
        public static long Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider) => long.Parse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider);
    }
}