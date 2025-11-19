using System;

namespace System.Buffers;

internal abstract class SearchValues<T>
    where T : IEquatable<T>?
{
    internal SearchValues()
    {
    }

    internal abstract bool Contains(T value);
}
