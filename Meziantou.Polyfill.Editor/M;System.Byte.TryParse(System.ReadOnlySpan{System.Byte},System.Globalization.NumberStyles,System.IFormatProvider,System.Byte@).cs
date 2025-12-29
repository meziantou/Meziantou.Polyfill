using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Byte
{
    extension(byte)
    {
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out byte result) => byte.TryParse(Encoding.UTF8.GetString(utf8Text), style, provider, out result);
    }
}