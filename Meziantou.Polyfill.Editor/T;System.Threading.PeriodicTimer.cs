using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace System.Threading
{
    internal sealed class PeriodicTimer : IDisposable
    {
        private const long MaxSupportedTimeoutMilliseconds = uint.MaxValue - 1L;

        private readonly ITimer _timer;
        private readonly State _state;
        private TimeSpan _period;

        public PeriodicTimer(TimeSpan period)
            : this(period, TimeProvider.System)
        {
        }

        public PeriodicTimer(TimeSpan period, TimeProvider timeProvider)
        {
            if (!IsValidPeriod(period))
            {
                GC.SuppressFinalize(this);
                throw new ArgumentOutOfRangeException(nameof(period));
            }

            if (timeProvider is null)
            {
                GC.SuppressFinalize(this);
                throw new ArgumentNullException(nameof(timeProvider));
            }

            _period = period;
            _state = new State();
            using (ExecutionContext.SuppressFlow())
            {
                _timer = timeProvider.CreateTimer(static state => ((State)state!).Signal(), _state, period, period);
            }
        }

        public TimeSpan Period
        {
            get => _period;
            set
            {
                if (!IsValidPeriod(value))
                    throw new ArgumentOutOfRangeException(nameof(value));

                _period = value;
                if (!_timer.Change(value, value))
                    throw new ObjectDisposedException(GetType().FullName);
            }
        }

        public ValueTask<bool> WaitForNextTickAsync(CancellationToken cancellationToken = default)
            => _state.WaitForNextTickAsync(this, cancellationToken);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _timer.Dispose();
            _state.Signal(stopping: true);
        }

        ~PeriodicTimer() => Dispose();

        private static bool IsValidPeriod(TimeSpan value)
        {
            if (value == Timeout.InfiniteTimeSpan)
                return true;

            var milliseconds = (long)value.TotalMilliseconds;
            return milliseconds >= 1 && milliseconds <= MaxSupportedTimeoutMilliseconds;
        }

        private sealed class State : IValueTaskSource<bool>
        {
            private readonly object _lock = new object();
            private PeriodicTimer? _owner;
            private TaskCompletionSource<bool>? _waiter;
            private CancellationTokenRegistration _cancellationRegistration;
            private CancellationToken _cancellationToken;
            private short _version;
            private bool _signaled;
            private bool _stopped;
            private bool _activeWait;
            private bool _canceled;

            public ValueTask<bool> WaitForNextTickAsync(PeriodicTimer owner, CancellationToken cancellationToken)
            {
                lock (_lock)
                {
                    if (_activeWait)
                        throw new InvalidOperationException();

                    if (cancellationToken.IsCancellationRequested)
                        return new ValueTask<bool>(Task.FromCanceled<bool>(cancellationToken));

                    if (_signaled)
                    {
                        if (!_stopped)
                        {
                            _signaled = false;
                        }

                        return new ValueTask<bool>(!_stopped);
                    }

                    _owner = owner;
                    _activeWait = true;
                    _waiter = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                    _cancellationToken = cancellationToken;
                    _version++;

                    if (cancellationToken.CanBeCanceled)
                    {
                        _cancellationRegistration = cancellationToken.Register(static state => ((State)state!).SignalCancellation(), this);
                    }
                    else
                    {
                        _cancellationRegistration = default;
                    }

                    return new ValueTask<bool>(this, _version);
                }
            }

            public void Signal(bool stopping = false)
            {
                TaskCompletionSource<bool>? waiter = null;

                lock (_lock)
                {
                    _stopped |= stopping;

                    if (!_signaled)
                    {
                        _signaled = true;
                        if (_activeWait)
                        {
                            waiter = _waiter;
                        }
                    }
                }

                waiter?.TrySetResult(true);
            }

            private void SignalCancellation()
            {
                TaskCompletionSource<bool>? waiter = null;

                lock (_lock)
                {
                    if (!_signaled)
                    {
                        _signaled = true;
                        if (_activeWait)
                        {
                            waiter = _waiter;
                            _canceled = true;
                        }
                    }
                }

                if (waiter != null)
                {
                    waiter.TrySetResult(true);
                }
            }

            bool IValueTaskSource<bool>.GetResult(short token)
            {
                ValidateToken(token);

                var waiter = _waiter!;
                _cancellationRegistration.Dispose();

                lock (_lock)
                {
                    try
                    {
                        _ = waiter.Task.GetAwaiter().GetResult();
                        if (_canceled)
                        {
                            throw new OperationCanceledException(_cancellationToken);
                        }
                    }
                    finally
                    {
                        GC.KeepAlive(_owner);
                        _activeWait = false;
                        _waiter = null;
                        _owner = null;
                        _cancellationToken = default;
                        _cancellationRegistration = default;
                        _canceled = false;

                        if (!_stopped)
                        {
                            _signaled = false;
                        }
                    }

                    return !_stopped;
                }
            }

            ValueTaskSourceStatus IValueTaskSource<bool>.GetStatus(short token)
            {
                ValidateToken(token);

                return _waiter!.Task.Status switch
                {
                    TaskStatus.RanToCompletion => _canceled ? ValueTaskSourceStatus.Faulted : ValueTaskSourceStatus.Succeeded,
                    TaskStatus.Canceled => ValueTaskSourceStatus.Canceled,
                    TaskStatus.Faulted => ValueTaskSourceStatus.Faulted,
                    _ => ValueTaskSourceStatus.Pending,
                };
            }

            void IValueTaskSource<bool>.OnCompleted(Action<object?> continuation, object? state, short token, ValueTaskSourceOnCompletedFlags flags)
            {
                ValidateToken(token);

                _waiter!.Task.GetAwaiter().OnCompleted(() => continuation(state));
            }

            private void ValidateToken(short token)
            {
                if (token != _version)
                    throw new InvalidOperationException();
            }
        }
    }
}
