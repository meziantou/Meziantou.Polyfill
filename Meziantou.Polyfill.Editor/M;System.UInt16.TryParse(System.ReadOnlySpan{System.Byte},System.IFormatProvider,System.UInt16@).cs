using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_UInt16
{
    extension(ushort)
    {
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out ushort result) => ushort.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider, out result);
    }
}