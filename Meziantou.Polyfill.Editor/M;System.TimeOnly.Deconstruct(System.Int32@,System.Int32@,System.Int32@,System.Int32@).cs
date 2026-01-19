using System;

static partial class PolyfillExtensions
{
    public static void Deconstruct(this TimeOnly timeOnly, out int hour, out int minute, out int second, out int millisecond)
    {
        hour = timeOnly.Hour;
        minute = timeOnly.Minute;
        second = timeOnly.Second;
        millisecond = timeOnly.Millisecond;
    }
}
