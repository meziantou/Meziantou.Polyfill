static partial class PolyfillExtensions
{
    public static bool TryGetRuneAt(this System.Text.StringBuilder target, int index, out System.Text.Rune value)
    {
        if ((uint)index >= (uint)target.Length)
            throw new System.ArgumentOutOfRangeException(nameof(index));

        if (index + 1 < target.Length && System.Text.Rune.TryCreate(target[index], target[index + 1], out value))
            return true;

        return System.Text.Rune.TryCreate(target[index], out value);
    }
}
