using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Single
{
    extension(float)
    {
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out float result) => float.TryParse(Encoding.UTF8.GetString(utf8Text), style, provider, out result);
    }
}