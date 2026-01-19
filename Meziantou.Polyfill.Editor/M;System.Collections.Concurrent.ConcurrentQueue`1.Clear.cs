using System.Collections.Concurrent;

static partial class PolyfillExtensions
{
    public static void Clear<T>(this ConcurrentQueue<T> queue)
    {
        while (queue.TryDequeue(out _))
        {
        }
    }
}
