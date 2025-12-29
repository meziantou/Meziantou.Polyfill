using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_SByte
{
    extension(sbyte)
    {
        public static sbyte Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider) => sbyte.Parse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider);
    }
}