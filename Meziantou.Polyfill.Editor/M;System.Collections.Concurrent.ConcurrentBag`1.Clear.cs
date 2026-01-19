using System.Collections.Concurrent;

static partial class PolyfillExtensions
{
    public static void Clear<T>(this ConcurrentBag<T> bag)
    {
        while (bag.TryTake(out _))
        {
        }
    }
}
