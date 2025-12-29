using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_UInt32
{
    extension(uint)
    {
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out uint result) => uint.TryParse(Encoding.UTF8.GetString(utf8Text), style, provider, out result);
    }
}