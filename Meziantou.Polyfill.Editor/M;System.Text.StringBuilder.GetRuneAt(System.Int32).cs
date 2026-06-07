static partial class PolyfillExtensions
{
    public static System.Text.Rune GetRuneAt(this System.Text.StringBuilder target, int index)
    {
        if ((uint)index >= (uint)target.Length)
            throw new System.ArgumentOutOfRangeException(nameof(index));

        if (index + 1 < target.Length && System.Text.Rune.TryCreate(target[index], target[index + 1], out var rune))
            return rune;

        if (System.Text.Rune.TryCreate(target[index], out rune))
            return rune;

        throw new System.ArgumentException("The specified index does not contain a valid Unicode scalar value.", nameof(index));
    }
}
