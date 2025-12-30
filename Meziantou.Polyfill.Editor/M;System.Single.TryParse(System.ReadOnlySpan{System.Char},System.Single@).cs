using System;

static partial class PolyfillExtensions_Single
{
    extension(float)
    {
        public static bool TryParse(ReadOnlySpan<char> s, out float result) => float.TryParse(s.ToString(), out result);
    }
}