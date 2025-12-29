using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Single
{
    extension(float)
    {
        public static float Parse(ReadOnlySpan<byte> utf8Text, NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands, IFormatProvider? provider = null) => float.Parse(Encoding.UTF8.GetString(utf8Text), style, provider);
    }
}