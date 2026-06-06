using System;
static partial class PolyfillExtensions_TimeSpan
{
    extension(TimeSpan)
    {
        public static TimeSpan FromHours(int hours, long minutes, long seconds, long milliseconds, long microseconds)
        {
            try
            {
                return TimeSpan.FromTicks(checked(hours * TimeSpan.TicksPerHour + minutes * TimeSpan.TicksPerMinute + seconds * TimeSpan.TicksPerSecond + milliseconds * TimeSpan.TicksPerMillisecond + microseconds * 10));
            }
            catch (OverflowException)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
