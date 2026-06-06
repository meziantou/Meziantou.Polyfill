static partial class PolyfillExtensions
{
    extension(char)
    {
        public static bool IsAsciiLetterOrDigit(char c) => c is (>= '0' and <= '9') or (>= 'A' and <= 'Z') or (>= 'a' and <= 'z');
    }
}
