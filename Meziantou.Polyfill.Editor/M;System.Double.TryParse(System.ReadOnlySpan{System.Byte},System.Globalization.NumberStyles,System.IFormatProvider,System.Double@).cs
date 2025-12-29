using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Double
{
    extension(double)
    {
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out double result) => double.TryParse(Encoding.UTF8.GetString(utf8Text), style, provider, out result);
    }
}