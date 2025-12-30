using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Single
{
    extension(float)
    {
        public static float Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider) => float.Parse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Float | NumberStyles.AllowThousands, provider);
    }
}