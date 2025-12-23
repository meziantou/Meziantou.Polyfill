namespace System.Buffers
{
    internal ref partial struct SequenceReader<T> where T : unmanaged, IEquatable<T>
    {
        private readonly ReadOnlySequence<T> _sequence;
        private SequencePosition _currentPosition;
        private ReadOnlyMemory<T> _currentMemory;
        private int _currentIndex;
        private long _consumed;
        private long _length;

        public SequenceReader(ReadOnlySequence<T> sequence)
        {
            _sequence = sequence;
            _currentPosition = sequence.Start;
            _currentMemory = default;
            _currentIndex = 0;
            _consumed = 0;
            _length = -1;

            if (!sequence.IsEmpty)
            {
                if (sequence.TryGet(ref _currentPosition, out _currentMemory))
                {
                    _currentIndex = 0;
                }
            }
        }

        private long Length
        {
            get
            {
                if (_length < 0)
                {
                    _length = _sequence.Length;
                }
                return _length;
            }
        }

        public readonly long Remaining => Length - _consumed;

        public readonly long Consumed => _consumed;

        public readonly SequencePosition Position => _sequence.GetPosition(_consumed);

        public readonly bool TryCopyTo(Span<T> destination)
        {
            if (Remaining < destination.Length)
            {
                return false;
            }

            if (destination.Length == 0)
            {
                return true;
            }

            var sourceSlice = _sequence.Slice(_consumed, destination.Length);

            CopySequenceToSpan(sourceSlice, destination);

            return true;
        }

        private static void CopySequenceToSpan(ReadOnlySequence<T> source, Span<T> destination)
        {
            if (source.IsSingleSegment)
            {
                source.First.Span.CopyTo(destination);
            }
            else
            {
                var destinationIndex = 0;
                foreach (var segment in source)
                {
                    var segmentSpan = segment.Span;
                    segmentSpan.CopyTo(destination.Slice(destinationIndex));
                    destinationIndex += segmentSpan.Length;
                }
            }
        }

        public void Advance(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (count == 0)
            {
                return;
            }

            var maxAdvance = Length - _consumed;

            if (count > maxAdvance)
            {
                throw new ArgumentOutOfRangeException(nameof(count), $"Cannot advance {count} elements when only {maxAdvance} elements remain in sequence.");
            }

            _consumed += count;

            if (_consumed >= Length)
            {
                _currentPosition = _sequence.End;
                _currentMemory = default;
                _currentIndex = 0;
            }
            else
            {
                _currentPosition = _sequence.GetPosition(_consumed);
                if (!_sequence.TryGet(ref _currentPosition, out _currentMemory))
                {
                    _currentMemory = default;
                    _currentIndex = 0;
                }
                else
                {
                    _currentIndex = 0;
                }
            }
        }
    }
}
