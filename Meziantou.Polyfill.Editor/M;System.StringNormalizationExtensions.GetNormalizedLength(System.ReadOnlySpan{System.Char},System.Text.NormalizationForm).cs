using System;
using System.Text;

static partial class PolyfillExtensions
{
    public static int GetNormalizedLength(this ReadOnlySpan<char> source, NormalizationForm normalizationForm) => source.ToString().Normalize(normalizationForm).Length;
}
