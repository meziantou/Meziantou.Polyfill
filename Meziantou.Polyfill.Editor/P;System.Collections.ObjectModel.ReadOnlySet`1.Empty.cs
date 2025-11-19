using System.Collections.Generic;
using System.Collections.ObjectModel;

static partial class PolyfillExtensions
{
    extension<T>(System.Collections.ObjectModel.ReadOnlySet<T> target)
    {
        public static ReadOnlySet<T> Empty
        {
            get => EmptyReadOnlySet<T>.Instance;
        }
    }
}

file static class EmptyReadOnlySet<T>
{
    public static readonly ReadOnlySet<T> Instance = new(new HashSet<T>());
}
