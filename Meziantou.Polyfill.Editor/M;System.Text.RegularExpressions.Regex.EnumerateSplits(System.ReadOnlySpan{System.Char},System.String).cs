using System;
using System.Text.RegularExpressions;

static partial class PolyfillExtensions_Regex
{
    extension(Regex)
    {
        public static ValueSplitEnumerator EnumerateSplits(ReadOnlySpan<char> input, string pattern) => new(new Regex(pattern), input.ToString(), 0, 0);
    }
}
