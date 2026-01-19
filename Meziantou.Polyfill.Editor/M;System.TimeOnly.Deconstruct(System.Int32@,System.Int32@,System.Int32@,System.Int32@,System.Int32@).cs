using System;

static partial class PolyfillExtensions
{
    public static void Deconstruct(this TimeOnly timeOnly, out int hour, out int minute, out int second, out int millisecond, out int microsecond)
    {
        hour = timeOnly.Hour;
        minute = timeOnly.Minute;
        second = timeOnly.Second;
        millisecond = timeOnly.Millisecond;
        microsecond = (int)(timeOnly.Ticks / 10L % 1000L);
    }
}
