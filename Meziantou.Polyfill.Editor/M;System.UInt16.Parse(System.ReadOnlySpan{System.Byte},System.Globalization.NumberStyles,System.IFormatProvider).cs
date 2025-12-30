using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_UInt16
{
    extension(ushort)
    {
        public static ushort Parse(ReadOnlySpan<byte> utf8Text, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null) => ushort.Parse(Encoding.UTF8.GetString(utf8Text), style, provider);
    }
}