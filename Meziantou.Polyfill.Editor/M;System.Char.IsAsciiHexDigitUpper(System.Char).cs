static partial class PolyfillExtensions
{
    extension(char)
    {
        public static bool IsAsciiHexDigitUpper(char c) => c is (>= '0' and <= '9') or (>= 'A' and <= 'F');
    }
}
