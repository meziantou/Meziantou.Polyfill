using System;

partial class PolyfillExtensions
{
    public static T[] GetItems<T>(this Random random, ReadOnlySpan<T> choices, int length)
    {
        if (random is null)
        {
            throw new ArgumentNullException(nameof(random));
        }

        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), length, "must not be negative.");
        }

        if (choices.IsEmpty)
        {
            throw new ArgumentException("Choices cannot be empty.", nameof(choices));
        }

        T[] items = new T[length];
        GetItems(random, choices, items.AsSpan());
        return items;
    }
}
