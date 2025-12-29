using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Single
{
    extension(float)
    {
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out float result) => float.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Float | NumberStyles.AllowThousands, provider, out result);
    }
}