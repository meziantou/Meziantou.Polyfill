using System;

static partial class PolyfillExtensions_Guid
{
    extension(Guid)
    {
        public static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, out Guid result) => Guid.TryParseExact(input.ToString(), format.ToString(), out result);
    }
}
