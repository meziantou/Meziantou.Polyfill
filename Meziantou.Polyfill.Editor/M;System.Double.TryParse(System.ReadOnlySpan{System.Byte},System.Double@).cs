using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Double
{
    extension(double)
    {
        public static bool TryParse(ReadOnlySpan<byte> value, out double result) => double.TryParse(Encoding.UTF8.GetString(value), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out result);
    }
}
