using System;

static partial class PolyfillExtensions_Int16
{
    extension(short)
    {
        public static bool TryParse(ReadOnlySpan<char> s, out short result) => short.TryParse(s.ToString(), out result);
    }
}