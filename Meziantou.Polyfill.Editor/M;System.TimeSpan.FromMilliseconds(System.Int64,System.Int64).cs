using System;
static partial class PolyfillExtensions_TimeSpan
{
    extension(TimeSpan)
    {
        public static TimeSpan FromMilliseconds(long milliseconds, long microseconds)
        {
            try
            {
                return TimeSpan.FromTicks(checked(milliseconds * TimeSpan.TicksPerMillisecond + microseconds * 10));
            }
            catch (OverflowException)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
