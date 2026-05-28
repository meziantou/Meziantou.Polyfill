using System;

static partial class PolyfillExtensions
{
    extension(Enum)
    {
        public static bool TryParse(Type enumType, ReadOnlySpan<char> value, out object? result)
        {
            return Enum.TryParse(enumType, value.ToString(), ignoreCase: false, out result);
        }
    }
}
