using System;

static partial class PolyfillExtensions
{
    public static bool TryFormat(this bool target, Span<char> destination, out int charsWritten)
    {
        var value = target ? bool.TrueString : bool.FalseString;
        if (value.Length > destination.Length)
        {
            charsWritten = 0;
            return false;
        }

        value.AsSpan().CopyTo(destination);
        charsWritten = value.Length;
        return true;
    }
}
