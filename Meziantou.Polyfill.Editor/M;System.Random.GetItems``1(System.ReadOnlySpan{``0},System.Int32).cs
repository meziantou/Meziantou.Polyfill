using System;

partial class PolyfillExtensions
{
    public static T[] GetItems<T>(this Random random, ReadOnlySpan<T> choices, int length)
    {
        ArgumentNullException.ThrowIfNull(random);
        ArgumentOutOfRangeException.ThrowIfNegative(length);

        if (choices.IsEmpty)
        {
            throw new ArgumentException("Choices cannot be empty.", nameof(choices));
        }

        T[] items = new T[length];
        GetItems(random, choices, items.AsSpan());
        return items;
    }
}
