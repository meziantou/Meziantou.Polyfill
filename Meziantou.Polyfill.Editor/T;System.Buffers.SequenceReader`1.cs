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

        public long Length
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

        public readonly ReadOnlySequence<T> Sequence => _sequence;

        public readonly bool End => Remaining == 0;

        public readonly ReadOnlySpan<T> CurrentSpan => _currentMemory.Span;

        public readonly int CurrentSpanIndex => _currentIndex;

        public readonly ReadOnlySequence<T> UnreadSequence => _sequence.Slice(_consumed);

        public readonly ReadOnlySpan<T> UnreadSpan => _currentMemory.Span.Slice(_currentIndex);

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

        public void Advance(long count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (count > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            Advance((int)count);
        }

        public void Rewind(long count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (count > _consumed)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            _consumed -= count;

            if (_consumed == 0)
            {
                _currentPosition = _sequence.Start;
                _currentIndex = 0;
                if (!_sequence.IsEmpty)
                {
                    _sequence.TryGet(ref _currentPosition, out _currentMemory);
                }
            }
            else
            {
                _currentPosition = _sequence.GetPosition(_consumed);
                _sequence.TryGet(ref _currentPosition, out _currentMemory);
                _currentIndex = 0;
            }
        }

        public readonly bool TryPeek(out T value)
        {
            if (End)
            {
                value = default;
                return false;
            }

            value = _currentMemory.Span[_currentIndex];
            return true;
        }

        public readonly bool TryPeek(long offset, out T value)
        {
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            if (offset >= Remaining)
            {
                value = default;
                return false;
            }

            var position = _sequence.GetPosition(_consumed + offset);
            if (_sequence.TryGet(ref position, out var memory))
            {
                value = memory.Span[0];
                return true;
            }

            value = default;
            return false;
        }

        public bool TryRead(out T value)
        {
            if (End)
            {
                value = default;
                return false;
            }

            value = _currentMemory.Span[_currentIndex];
            _currentIndex++;
            _consumed++;

            if (_currentIndex >= _currentMemory.Length)
            {
                if (_consumed < Length)
                {
                    var nextPosition = _currentPosition;
                    while (_sequence.TryGet(ref nextPosition, out var nextMemory, advance: true))
                    {
                        _currentPosition = nextPosition;
                        _currentMemory = nextMemory;
                        _currentIndex = 0;
                        if (nextMemory.Length > 0)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    _currentPosition = _sequence.End;
                    _currentMemory = default;
                    _currentIndex = 0;
                }
            }

            return true;
        }
    }
}
