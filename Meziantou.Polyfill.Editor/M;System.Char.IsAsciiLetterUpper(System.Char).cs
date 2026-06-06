static partial class PolyfillExtensions
{
    extension(char)
    {
        public static bool IsAsciiLetterUpper(char c) => c is >= 'A' and <= 'Z';
    }
}
