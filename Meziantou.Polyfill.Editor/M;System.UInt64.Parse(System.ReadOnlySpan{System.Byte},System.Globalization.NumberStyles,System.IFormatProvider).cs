using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_UInt64
{
    extension(ulong)
    {
        public static ulong Parse(ReadOnlySpan<byte> utf8Text, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null) => ulong.Parse(Encoding.UTF8.GetString(utf8Text), style, provider);
    }
}