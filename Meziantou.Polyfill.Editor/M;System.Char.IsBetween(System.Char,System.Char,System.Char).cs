static partial class PolyfillExtensions
{
    extension(char)
    {
        public static bool IsBetween(char c, char minInclusive, char maxInclusive) => c >= minInclusive && c <= maxInclusive;
    }
}
