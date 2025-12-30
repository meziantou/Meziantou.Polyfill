using System;

static partial class PolyfillExtensions_Int64
{
    extension(long)
    {
        public static bool TryParse(ReadOnlySpan<char> s, out long result) => long.TryParse(s.ToString(), out result);
    }
}