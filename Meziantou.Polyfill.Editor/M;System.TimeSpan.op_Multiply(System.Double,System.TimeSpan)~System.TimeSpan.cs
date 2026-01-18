using System;

static partial class PolyfillExtensions
{
    extension(TimeSpan)
    {
        public static TimeSpan operator *(double factor, TimeSpan timeSpan)
        {
            return new TimeSpan((long)(timeSpan.Ticks * factor));
        }
    }
}
