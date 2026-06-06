using System;
static partial class PolyfillExtensions_TimeSpan
{
    extension(TimeSpan)
    {
        public static TimeSpan FromMicroseconds(long microseconds)
        {
            try
            {
                return TimeSpan.FromTicks(checked(microseconds * 10));
            }
            catch (OverflowException)
            {
                throw new ArgumentOutOfRangeException(nameof(microseconds));
            }
        }
    }
}
