using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_SByte
{
    extension(sbyte)
    {
        public static sbyte Parse(ReadOnlySpan<byte> utf8Text, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null) => sbyte.Parse(Encoding.UTF8.GetString(utf8Text), style, provider);
    }
}