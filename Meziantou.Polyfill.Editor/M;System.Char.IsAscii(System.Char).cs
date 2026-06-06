static partial class PolyfillExtensions
{
    extension(char)
    {
        public static bool IsAscii(char c) => c <= '\x7F';
    }
}
