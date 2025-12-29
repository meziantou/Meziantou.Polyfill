using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Byte
{
    extension(byte)
    {
        public static byte Parse(ReadOnlySpan<byte> utf8Text, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null) => byte.Parse(Encoding.UTF8.GetString(utf8Text), style, provider);
    }
}