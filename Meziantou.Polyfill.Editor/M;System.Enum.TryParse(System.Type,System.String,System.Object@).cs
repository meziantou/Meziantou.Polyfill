using System;

static partial class PolyfillExtensions
{
    extension(Enum)
    {
        public static bool TryParse(Type enumType, string? value, out object? result)
        {
            return Enum.TryParse(enumType, value, ignoreCase: false, out result);
        }
    }
}
