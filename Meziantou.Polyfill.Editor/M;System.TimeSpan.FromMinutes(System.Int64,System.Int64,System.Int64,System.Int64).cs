using System;
static partial class PolyfillExtensions_TimeSpan
{
    extension(TimeSpan)
    {
        public static TimeSpan FromMinutes(long minutes, long seconds, long milliseconds, long microseconds)
        {
            try
            {
                return TimeSpan.FromTicks(checked(minutes * TimeSpan.TicksPerMinute + seconds * TimeSpan.TicksPerSecond + milliseconds * TimeSpan.TicksPerMillisecond + microseconds * 10));
            }
            catch (OverflowException)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
