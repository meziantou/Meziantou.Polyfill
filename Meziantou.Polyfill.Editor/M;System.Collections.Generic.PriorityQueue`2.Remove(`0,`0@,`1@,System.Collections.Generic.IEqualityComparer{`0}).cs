using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static bool Remove<TElement, TPriority>(this PriorityQueue<TElement, TPriority> queue, TElement element, out TElement removedElement, out TPriority priority, IEqualityComparer<TElement>? equalityComparer = null)
    {
        equalityComparer ??= EqualityComparer<TElement>.Default;
        var entries = new List<(TElement Element, TPriority Priority)>();
        var found = false;
        removedElement = default!;
        priority = default!;

        while (queue.TryDequeue(out var currentElement, out var currentPriority))
        {
            if (!found && equalityComparer.Equals(currentElement, element))
            {
                found = true;
                removedElement = currentElement;
                priority = currentPriority;
            }
            else
            {
                entries.Add((currentElement, currentPriority));
            }
        }

        foreach (var entry in entries)
            queue.Enqueue(entry.Element, entry.Priority);

        return found;
    }
}
