using System;

static partial class PolyfillExtensions
{
    extension(Delegate)
    {
        public static PolyfillInvocationListEnumerator<TDelegate> EnumerateInvocationList<TDelegate>(TDelegate? d) where TDelegate : Delegate
        {
            return new PolyfillInvocationListEnumerator<TDelegate>(d);
        }
    }
}

internal struct PolyfillInvocationListEnumerator<TDelegate> where TDelegate : Delegate
{
    private readonly TDelegate? _delegate;
    private int _index;
    private TDelegate? _current;

    internal PolyfillInvocationListEnumerator(TDelegate? d)
    {
        _delegate = d;
        _index = -1;
        _current = null;
    }

    public TDelegate Current => _current!;

    public bool MoveNext()
    {
        if (_delegate is null)
        {
            return false;
        }

        var invocationList = _delegate.GetInvocationList();
        _index++;
        if (_index < invocationList.Length)
        {
            _current = (TDelegate)invocationList[_index];
            return true;
        }

        _current = null;
        return false;
    }

    public PolyfillInvocationListEnumerator<TDelegate> GetEnumerator() => this;
}
