using System.Collections.Generic;
using System.Collections.ObjectModel;

static partial class PolyfillExtensions
{
    public static ReadOnlyCollection<T> AsReadOnly<T>(this IList<T> list)
    {
        return new ReadOnlyCollection<T>(list);
    }
}
