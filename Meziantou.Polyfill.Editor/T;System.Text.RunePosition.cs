namespace System.Text
{
    internal readonly struct RunePosition : System.IEquatable<RunePosition>
    {
        public RunePosition(Rune rune, int startIndex, int length, bool wasReplaced)
        {
            Rune = rune;
            StartIndex = startIndex;
            Length = length;
            WasReplaced = wasReplaced;
        }

        public Rune Rune { get; }
        public int StartIndex { get; }
        public int Length { get; }
        public bool WasReplaced { get; }

        public void Deconstruct(out Rune rune, out int startIndex, out int length)
        {
            rune = Rune;
            startIndex = StartIndex;
            length = Length;
        }

        public void Deconstruct(out Rune rune, out int startIndex)
        {
            rune = Rune;
            startIndex = StartIndex;
        }

        public static Utf16Enumerator EnumerateUtf16(System.ReadOnlySpan<char> span) => new(span);
        public static Utf8Enumerator EnumerateUtf8(System.ReadOnlySpan<byte> span) => new(span);

        public override bool Equals(object? obj) => obj is RunePosition other && Equals(other);
        public bool Equals(RunePosition other) => Rune == other.Rune && StartIndex == other.StartIndex && Length == other.Length && WasReplaced == other.WasReplaced;
        public override int GetHashCode() => HashCode.Combine(Rune, StartIndex, Length, WasReplaced);
        public static bool operator ==(RunePosition left, RunePosition right) => left.Equals(right);
        public static bool operator !=(RunePosition left, RunePosition right) => !left.Equals(right);

        public ref struct Utf16Enumerator : System.Collections.Generic.IEnumerator<RunePosition>, System.Collections.IEnumerator, System.IDisposable
        {
            private readonly System.ReadOnlySpan<char> _span;
            private int _index;
            private RunePosition _current;

            internal Utf16Enumerator(System.ReadOnlySpan<char> span)
            {
                _span = span;
                _index = 0;
                _current = default;
            }

            public readonly Utf16Enumerator GetEnumerator() => this;
            public RunePosition Current => _current;
            readonly object System.Collections.IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_index >= _span.Length)
                    return false;

                var status = Rune.DecodeFromUtf16(_span.Slice(_index), out var rune, out var consumed);
                if (consumed == 0)
                    consumed = 1;

                _current = new RunePosition(rune, _index, consumed, status != System.Buffers.OperationStatus.Done);
                _index += consumed;
                return true;
            }

            public void Reset() => throw new System.NotSupportedException();
            void System.IDisposable.Dispose() { }
        }

        public ref struct Utf8Enumerator : System.Collections.Generic.IEnumerator<RunePosition>, System.Collections.IEnumerator, System.IDisposable
        {
            private readonly System.ReadOnlySpan<byte> _span;
            private int _index;
            private RunePosition _current;

            internal Utf8Enumerator(System.ReadOnlySpan<byte> span)
            {
                _span = span;
                _index = 0;
                _current = default;
            }

            public readonly Utf8Enumerator GetEnumerator() => this;
            public RunePosition Current => _current;
            readonly object System.Collections.IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_index >= _span.Length)
                    return false;

                var status = Rune.DecodeFromUtf8(_span.Slice(_index), out var rune, out var consumed);
                if (consumed == 0)
                    consumed = 1;

                _current = new RunePosition(rune, _index, consumed, status != System.Buffers.OperationStatus.Done);
                _index += consumed;
                return true;
            }

            public void Reset() => throw new System.NotSupportedException();
            void System.IDisposable.Dispose() { }
        }
    }
}
