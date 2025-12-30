using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Byte
{
    extension(byte)
    {
        public static byte Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider) => byte.Parse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider);
    }
}