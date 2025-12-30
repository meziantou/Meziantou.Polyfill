using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Double
{
    extension(double)
    {
        public static double Parse(ReadOnlySpan<byte> utf8Text, NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands, IFormatProvider? provider = null) => double.Parse(Encoding.UTF8.GetString(utf8Text), style, provider);
    }
}