using System;
using System.Text;

static partial class PolyfillExtensions
{
    public static bool TryNormalize(this ReadOnlySpan<char> source, Span<char> destination, out int charsWritten, NormalizationForm normalizationForm)
    {
        var normalized = source.ToString().Normalize(normalizationForm);
        if (normalized.Length > destination.Length)
        {
            charsWritten = 0;
            return false;
        }

        normalized.AsSpan().CopyTo(destination);
        charsWritten = normalized.Length;
        return true;
    }
}
