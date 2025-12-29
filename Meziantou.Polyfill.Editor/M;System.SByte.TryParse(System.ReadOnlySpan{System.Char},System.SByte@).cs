using System;

static partial class PolyfillExtensions_SByte
{
    extension(sbyte)
    {
        public static bool TryParse(ReadOnlySpan<char> s, out sbyte result) => sbyte.TryParse(s.ToString(), out result);
    }
}