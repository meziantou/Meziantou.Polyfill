using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Int16
{
    extension(short)
    {
        public static short Parse(ReadOnlySpan<byte> utf8Text, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null) => short.Parse(Encoding.UTF8.GetString(utf8Text), style, provider);
    }
}