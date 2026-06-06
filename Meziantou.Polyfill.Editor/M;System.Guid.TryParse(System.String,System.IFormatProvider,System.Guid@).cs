using System;

static partial class PolyfillExtensions_Guid
{
    extension(Guid)
    {
        public static bool TryParse(string? input, IFormatProvider? provider, out Guid result) => Guid.TryParse(input, out result);
    }
}
