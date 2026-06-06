static partial class PolyfillExtensions
{
    extension(char)
    {
        public static bool IsAsciiLetter(char c) => c is (>= 'A' and <= 'Z') or (>= 'a' and <= 'z');
    }
}
