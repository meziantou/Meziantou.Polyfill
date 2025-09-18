using System;

static partial class PolyfillExtensions
{
    public static TimeSpan Multiply(this TimeSpan timeSpan, double factor)
    {
        return new TimeSpan((long)(timeSpan.Ticks * factor));
    }
}