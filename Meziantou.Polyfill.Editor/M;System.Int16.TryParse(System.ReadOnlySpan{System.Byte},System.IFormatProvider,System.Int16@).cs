using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Int16
{
    extension(short)
    {
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out short result) => short.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider, out result);
    }
}