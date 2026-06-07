using System.Text.RegularExpressions;

static partial class PolyfillExtensions_Regex
{
    extension(Regex)
    {
        public static int Count(string input, string pattern) => new Regex(pattern).Matches(input).Count;
    }
}
