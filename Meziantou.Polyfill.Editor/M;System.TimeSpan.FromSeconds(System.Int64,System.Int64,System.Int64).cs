using System;
static partial class PolyfillExtensions_TimeSpan
{
    extension(TimeSpan)
    {
        public static TimeSpan FromSeconds(long seconds, long milliseconds, long microseconds)
        {
            try
            {
                return TimeSpan.FromTicks(checked(seconds * TimeSpan.TicksPerSecond + milliseconds * TimeSpan.TicksPerMillisecond + microseconds * 10));
            }
            catch (OverflowException)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
