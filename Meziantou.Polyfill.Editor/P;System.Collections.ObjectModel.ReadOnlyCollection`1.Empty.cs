using System.Collections.Generic;
using System.Collections.ObjectModel;

static partial class PolyfillExtensions
{
    extension<T>(System.Collections.ObjectModel.ReadOnlyCollection<T> target)
    {
        public static ReadOnlyCollection<T> Empty
        {
            get => EmptyReadOnlyCollection<T>.Instance;
        }
    }
}

file static class EmptyReadOnlyCollection<T>
{
    public static readonly ReadOnlyCollection<T> Instance = new(System.Array.Empty<T>());
}
