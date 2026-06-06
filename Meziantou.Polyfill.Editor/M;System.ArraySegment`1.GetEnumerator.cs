partial class PolyfillExtensions
{
    extension<T>(System.ArraySegment<T> segment)
    {
        public ArraySegmentEnumerator<T> GetEnumerator()
        {
            if (segment.Array is null)
            {
                throw new System.InvalidOperationException("The underlying array is null.");
            }

            return new ArraySegmentEnumerator<T>(segment);
        }
    }
}

internal struct ArraySegmentEnumerator<T> : System.Collections.Generic.IEnumerator<T>
{
    private readonly T[] _array;
    private readonly int _start;
    private readonly int _end;
    private int _current;

    public ArraySegmentEnumerator(System.ArraySegment<T> segment)
    {
        _array = segment.Array!;
        _start = segment.Offset;
        _end = segment.Offset + segment.Count;
        _current = segment.Offset - 1;
    }

    public readonly T Current
    {
        get
        {
            if (_current < _start)
            {
                throw new System.InvalidOperationException("Enumeration has not started.");
            }

            if (_current >= _end)
            {
                throw new System.InvalidOperationException("Enumeration already finished.");
            }

            return _array[_current];
        }
    }

    readonly object? System.Collections.IEnumerator.Current => Current;

    public bool MoveNext()
    {
        if (_current < _end)
        {
            _current++;
            return _current < _end;
        }

        return false;
    }

    public void Reset()
    {
        _current = _start - 1;
    }

    public readonly void Dispose()
    {
    }
}
