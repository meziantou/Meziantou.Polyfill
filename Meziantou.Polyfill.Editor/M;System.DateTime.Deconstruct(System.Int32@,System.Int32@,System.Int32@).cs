using System;

static partial class PolyfillExtensions
{
    public static void Deconstruct(this DateTime target, out int year, out int month, out int day)
    {
        year = target.Year;
        month = target.Month;
        day = target.Day;
    }
}
