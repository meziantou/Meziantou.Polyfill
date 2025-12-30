using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Int16
{
    extension(short)
    {
        public static short Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider) => short.Parse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider);
    }
}