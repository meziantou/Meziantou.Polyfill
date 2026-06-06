using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Int64
{
    extension(long)
    {
        public static bool TryParse(ReadOnlySpan<byte> value, out long result) => long.TryParse(Encoding.UTF8.GetString(value), NumberStyles.Integer, CultureInfo.CurrentCulture, out result);
    }
}
