using System;
static partial class PolyfillExtensions
{
    public static string GetString(this Random target, ReadOnlySpan<char> choices, int length)
    {
        if (choices.IsEmpty)
            throw new ArgumentException("Choices cannot be empty.", nameof(choices));

        if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length));

        var result = new char[length];
        for (var i = 0; i < result.Length; i++)
        {
            result[i] = choices[target.Next(choices.Length)];
        }

        return new string(result);
    }
}
