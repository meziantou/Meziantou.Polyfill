using System;

static partial class PolyfillExtensions
{
    extension(TimeSpan)
    {
        public static TimeSpan operator /(TimeSpan timeSpan, double divisor)
        {
            return new TimeSpan((long)(timeSpan.Ticks / divisor));
        }
    }
}
