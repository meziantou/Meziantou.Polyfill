using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Int32
{
    extension(int)
    {
        public static int Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider) => int.Parse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider);
    }
}