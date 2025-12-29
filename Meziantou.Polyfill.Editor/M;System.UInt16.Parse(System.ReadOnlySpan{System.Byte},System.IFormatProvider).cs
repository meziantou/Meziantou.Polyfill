using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_UInt16
{
    extension(ushort)
    {
        public static ushort Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider) => ushort.Parse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider);
    }
}