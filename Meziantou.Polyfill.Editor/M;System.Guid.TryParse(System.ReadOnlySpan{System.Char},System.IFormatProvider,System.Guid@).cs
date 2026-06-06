using System;

static partial class PolyfillExtensions_Guid
{
    extension(Guid)
    {
        public static bool TryParse(ReadOnlySpan<char> input, IFormatProvider? provider, out Guid result) => Guid.TryParse(input.ToString(), out result);
    }
}
