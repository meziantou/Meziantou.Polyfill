using System;

static partial class PolyfillExtensions_Int32
{
    extension(int)
    {
        public static bool TryParse(ReadOnlySpan<char> s, out int result) => int.TryParse(s.ToString(), out result);
    }
}