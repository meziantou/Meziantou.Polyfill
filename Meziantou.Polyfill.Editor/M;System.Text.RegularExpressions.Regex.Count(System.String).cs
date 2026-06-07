using System.Text.RegularExpressions;

static partial class PolyfillExtensions
{
    public static int Count(this Regex regex, string input) => regex.Matches(input).Count;
}
