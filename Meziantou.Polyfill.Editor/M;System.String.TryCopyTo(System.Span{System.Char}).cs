using System;

static partial class PolyfillExtensions
{
    public static bool TryCopyTo(this string target, Span<char> destination)
    {
        return target.AsSpan().TryCopyTo(destination);
    }
}