namespace System.Text
{
    internal struct StringBuilderRuneEnumerator : System.Collections.Generic.IEnumerable<Rune>, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator<Rune>, System.Collections.IEnumerator, System.IDisposable
    {
        private readonly StringBuilder _builder;
        private int _index;
        private Rune _current;

        internal StringBuilderRuneEnumerator(StringBuilder builder)
        {
            _builder = builder;
            _index = 0;
            _current = default;
        }

        public readonly StringBuilderRuneEnumerator GetEnumerator() => this;
        public Rune Current => _current;
        readonly object System.Collections.IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_index >= _builder.Length)
                return false;

            if (_index + 1 < _builder.Length && Rune.TryCreate(_builder[_index], _builder[_index + 1], out _current))
            {
                _index += 2;
                return true;
            }

            if (!Rune.TryCreate(_builder[_index], out _current))
                _current = Rune.ReplacementChar;

            _index++;
            return true;
        }

        void System.Collections.IEnumerator.Reset() => throw new System.NotSupportedException();
        void System.IDisposable.Dispose() { }

        readonly System.Collections.Generic.IEnumerator<Rune> System.Collections.Generic.IEnumerable<Rune>.GetEnumerator() => this;
        readonly System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => this;
    }
}
