static partial class PolyfillExtensions
{
    extension(char)
    {
        public static bool IsAsciiDigit(char c) => c is >= '0' and <= '9';
    }
}
