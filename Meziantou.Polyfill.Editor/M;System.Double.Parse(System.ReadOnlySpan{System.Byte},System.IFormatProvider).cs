using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Double
{
    extension(double)
    {
        public static double Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider) => double.Parse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Float | NumberStyles.AllowThousands, provider);
    }
}