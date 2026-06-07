using System;
using System.Text;

static partial class PolyfillExtensions
{
    public static bool IsNormalized(this ReadOnlySpan<char> source, NormalizationForm normalizationForm) => source.ToString().IsNormalized(normalizationForm);
}
