using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_SByte
{
    extension(sbyte)
    {
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out sbyte result) => sbyte.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider, out result);
    }
}