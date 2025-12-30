using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_UInt64
{
    extension(ulong)
    {
        public static ulong Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider) => ulong.Parse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider);
    }
}