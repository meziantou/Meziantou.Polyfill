using System;

static partial class PolyfillExtensions_Double
{
    extension(double)
    {
        public static bool TryParse(ReadOnlySpan<char> s, out double result) => double.TryParse(s.ToString(), out result);
    }
}