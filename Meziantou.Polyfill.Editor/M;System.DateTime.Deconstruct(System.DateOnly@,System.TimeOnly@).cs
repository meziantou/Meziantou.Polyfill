using System;

static partial class PolyfillExtensions
{
    public static void Deconstruct(this DateTime target, out DateOnly date, out TimeOnly time)
    {
        date = DateOnly.FromDateTime(target);
        time = TimeOnly.FromDateTime(target);
    }
}
