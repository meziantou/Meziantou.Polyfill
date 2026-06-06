static partial class PolyfillExtensions
{
    extension(char)
    {
        public static bool IsAsciiHexDigitLower(char c) => c is (>= '0' and <= '9') or (>= 'a' and <= 'f');
    }
}
