using System.Collections;

static partial class PolyfillExtensions
{
    public static void Deconstruct(this DictionaryEntry entry, out object key, out object? value)
    {
        key = entry.Key;
        value = entry.Value;
    }
}
