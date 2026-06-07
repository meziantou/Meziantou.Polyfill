using System;
using System.Text.RegularExpressions;

static partial class PolyfillExtensions
{
    public static ValueSplitEnumerator EnumerateSplits(this Regex regex, ReadOnlySpan<char> input, int count, int startat) => new(regex, input.ToString(), count, startat);
}
