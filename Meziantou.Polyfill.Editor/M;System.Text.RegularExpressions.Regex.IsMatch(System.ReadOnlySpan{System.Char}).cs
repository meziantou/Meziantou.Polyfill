using System;
using System.Text.RegularExpressions;

static partial class PolyfillExtensions
{
    public static bool IsMatch(this Regex regex, ReadOnlySpan<char> input) => regex.IsMatch(input.ToString());
}
