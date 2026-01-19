using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

static partial class PolyfillExtensions
{
    public static bool TryPop<T>(this Stack<T> target, [MaybeNullWhen(false)] out T result)
    {
        if (target.Count == 0)
        {
            result = default;
            return false;
        }

        result = target.Pop();
        return true;
    }
}
