using System;

static partial class PolyfillExtensions
{
    extension(TimeSpan)
    {
        public static TimeSpan operator *(TimeSpan timeSpan, double factor)
        {
            return new TimeSpan((long)(timeSpan.Ticks * factor));
        }
    }
}
