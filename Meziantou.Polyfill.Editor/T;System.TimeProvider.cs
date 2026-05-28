using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace System
{
    internal abstract class TimeProvider
    {
        public static TimeProvider System { get; } = new SystemTimeProvider();

        protected TimeProvider()
        {
        }

        public virtual DateTimeOffset GetUtcNow() => DateTimeOffset.UtcNow;

        private static readonly long s_minDateTicks = DateTime.MinValue.Ticks;
        private static readonly long s_maxDateTicks = DateTime.MaxValue.Ticks;

        public DateTimeOffset GetLocalNow()
        {
            DateTimeOffset utcDateTime = GetUtcNow();
            TimeZoneInfo zoneInfo = LocalTimeZone;
            if (zoneInfo is null)
                throw new InvalidOperationException("A null TimeZoneInfo was returned by LocalTimeZone.");

            TimeSpan offset = zoneInfo.GetUtcOffset(utcDateTime);
            if (offset.Ticks is 0)
                return utcDateTime;

            long localTicks = utcDateTime.Ticks + offset.Ticks;
            if ((ulong)localTicks > (ulong)s_maxDateTicks)
                localTicks = localTicks < s_minDateTicks ? s_minDateTicks : s_maxDateTicks;

            return new DateTimeOffset(localTicks, offset);
        }

        public virtual TimeZoneInfo LocalTimeZone => TimeZoneInfo.Local;

        public virtual long TimestampFrequency => Stopwatch.Frequency;

        public virtual long GetTimestamp() => Stopwatch.GetTimestamp();

        public TimeSpan GetElapsedTime(long startingTimestamp, long endingTimestamp)
        {
            long timestampFrequency = TimestampFrequency;
            if (timestampFrequency <= 0)
                throw new InvalidOperationException("TimestampFrequency must be a positive value.");

            return new TimeSpan((long)((endingTimestamp - startingTimestamp) * ((double)TimeSpan.TicksPerSecond / timestampFrequency)));
        }

        public TimeSpan GetElapsedTime(long startingTimestamp) => GetElapsedTime(startingTimestamp, GetTimestamp());

        public virtual ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period)
        {
            if (callback is null)
                throw new ArgumentNullException(nameof(callback));

            return new SystemTimeProviderTimer(dueTime, period, callback, state);
        }

        private sealed class SystemTimeProviderTimer : ITimer
        {
            private readonly Timer _timer;

            public SystemTimeProviderTimer(TimeSpan dueTime, TimeSpan period, TimerCallback callback, object? state)
            {
                var timerState = new TimerState(callback, state);
                timerState.Timer = _timer = new Timer(static s =>
                {
                    var ts = (TimerState)s!;
                    ts.Callback(ts.State);
                }, timerState, dueTime, period);
            }

            private sealed class TimerState(TimerCallback callback, object? state)
            {
                public TimerCallback Callback { get; } = callback;
                public object? State { get; } = state;
                public Timer? Timer { get; set; }
            }

            public bool Change(TimeSpan dueTime, TimeSpan period)
            {
                try
                {
                    return _timer.Change(dueTime, period);
                }
                catch (ObjectDisposedException)
                {
                    return false;
                }
            }

            public void Dispose() => _timer.Dispose();

            public ValueTask DisposeAsync()
            {
                _timer.Dispose();
                return default;
            }
        }

        private sealed class SystemTimeProvider : TimeProvider;
    }
}
