using System;

static partial class PolyfillExtensions
{
    public static void Deconstruct(this TimeOnly timeOnly, out int hour, out int minute)
    {
        hour = timeOnly.Hour;
        minute = timeOnly.Minute;
    }
}
