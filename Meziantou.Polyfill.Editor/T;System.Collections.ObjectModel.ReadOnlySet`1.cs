using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Collections.ObjectModel;

[DebuggerDisplay("Count = {Count}")]
internal sealed class ReadOnlySet<T> : ISet<T>, IReadOnlyCollection<T>
{
    private readonly ISet<T> _set;

    public static ReadOnlySet<T> Empty => EmptyReadOnlySet<T>.Instance;

    public ReadOnlySet(ISet<T> set)
    {
        _set = set ?? throw new ArgumentNullException(nameof(set));
    }

    public int Count => _set.Count;

    public bool Contains(T item) => _set.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => _set.CopyTo(array, arrayIndex);

    public IEnumerator<T> GetEnumerator() => _set.GetEnumerator();

    public bool IsProperSubsetOf(IEnumerable<T> other) => _set.IsProperSubsetOf(other);

    public bool IsProperSupersetOf(IEnumerable<T> other) => _set.IsProperSupersetOf(other);

    public bool IsSubsetOf(IEnumerable<T> other) => _set.IsSubsetOf(other);

    public bool IsSupersetOf(IEnumerable<T> other) => _set.IsSupersetOf(other);

    public bool Overlaps(IEnumerable<T> other) => _set.Overlaps(other);

    public bool SetEquals(IEnumerable<T> other) => _set.SetEquals(other);

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_set).GetEnumerator();

    bool ISet<T>.Add(T item) => throw new NotSupportedException("Set is read-only.");

    void ISet<T>.ExceptWith(IEnumerable<T> other) => throw new NotSupportedException("Set is read-only.");

    void ISet<T>.IntersectWith(IEnumerable<T> other) => throw new NotSupportedException("Set is read-only.");

    void ISet<T>.SymmetricExceptWith(IEnumerable<T> other) => throw new NotSupportedException("Set is read-only.");

    void ISet<T>.UnionWith(IEnumerable<T> other) => throw new NotSupportedException("Set is read-only.");

    void ICollection<T>.Add(T item) => throw new NotSupportedException("Set is read-only.");

    void ICollection<T>.Clear() => throw new NotSupportedException("Set is read-only.");

    bool ICollection<T>.Remove(T item) => throw new NotSupportedException("Set is read-only.");

    bool ICollection<T>.IsReadOnly => true;
}

file static class EmptyReadOnlySet<T>
{
    public static readonly ReadOnlySet<T> Instance = new(new HashSet<T>());
}
