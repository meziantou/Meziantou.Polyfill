using System;

static partial class PolyfillExtensions
{
    public static void Deconstruct(this DateOnly dateOnly, out int year, out int month, out int day)
    {
        year = dateOnly.Year;
        month = dateOnly.Month;
        day = dateOnly.Day;
    }
}
