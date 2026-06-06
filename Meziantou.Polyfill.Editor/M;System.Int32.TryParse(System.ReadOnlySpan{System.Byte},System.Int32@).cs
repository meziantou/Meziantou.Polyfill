using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Int32
{
    extension(int)
    {
        public static bool TryParse(ReadOnlySpan<byte> value, out int result) => int.TryParse(Encoding.UTF8.GetString(value), NumberStyles.Integer, CultureInfo.CurrentCulture, out result);
    }
}
