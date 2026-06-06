static partial class PolyfillExtensions
{
    extension(char)
    {
        public static bool IsAsciiLetterLower(char c) => c is >= 'a' and <= 'z';
    }
}
