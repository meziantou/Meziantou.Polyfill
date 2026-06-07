using System;
using System.Text.RegularExpressions;

static partial class PolyfillExtensions
{
    public static ValueMatchEnumerator EnumerateMatches(this Regex regex, ReadOnlySpan<char> input, int startat) => new(regex, input.ToString(), startat);
}
