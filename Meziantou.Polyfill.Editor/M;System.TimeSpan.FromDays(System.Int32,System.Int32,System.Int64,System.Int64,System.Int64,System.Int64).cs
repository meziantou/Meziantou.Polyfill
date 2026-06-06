using System;
static partial class PolyfillExtensions_TimeSpan
{
    extension(TimeSpan)
    {
        public static TimeSpan FromDays(int days, int hours, long minutes, long seconds, long milliseconds, long microseconds)
        {
            try
            {
                return TimeSpan.FromTicks(checked(days * TimeSpan.TicksPerDay + hours * TimeSpan.TicksPerHour + minutes * TimeSpan.TicksPerMinute + seconds * TimeSpan.TicksPerSecond + milliseconds * TimeSpan.TicksPerMillisecond + microseconds * 10));
            }
            catch (OverflowException)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
