using System;

static partial class PolyfillExtensions_Byte
{
    extension(byte)
    {
        public static bool TryParse(ReadOnlySpan<char> s, out byte result) => byte.TryParse(s.ToString(), out result);
    }
}