using System;

static partial class PolyfillExtensions
{
    public static void CopyTo(this string target, Span<char> destination)
    {
        target.AsSpan().CopyTo(destination);
    }
}