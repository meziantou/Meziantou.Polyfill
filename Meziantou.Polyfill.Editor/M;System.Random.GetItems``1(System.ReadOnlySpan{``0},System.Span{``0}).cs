using System;

partial class PolyfillExtensions
{
    public static void GetItems<T>(this Random random, ReadOnlySpan<T> choices, Span<T> destination)
    {
        ArgumentNullException.ThrowIfNull(random);

        if (choices.IsEmpty)
        {
            throw new ArgumentException("Choices cannot be empty.", nameof(choices));
        }

        for (int i = 0; i < destination.Length; i++)
        {
            destination[i] = choices[random.Next(choices.Length)];
        }
    }
}
