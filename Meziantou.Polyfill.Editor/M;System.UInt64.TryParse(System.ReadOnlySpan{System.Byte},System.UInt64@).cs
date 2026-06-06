using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_UInt64
{
    extension(ulong)
    {
        public static bool TryParse(ReadOnlySpan<byte> value, out ulong result) => ulong.TryParse(Encoding.UTF8.GetString(value), NumberStyles.Integer, CultureInfo.CurrentCulture, out result);
    }
}
