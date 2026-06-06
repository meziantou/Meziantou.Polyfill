using System;

static partial class PolyfillExtensions
{
    public static void Deconstruct(this DateTimeOffset target, out DateOnly date, out TimeOnly time, out TimeSpan offset)
    {
        date = DateOnly.FromDateTime(target.DateTime);
        time = TimeOnly.FromDateTime(target.DateTime);
        offset = target.Offset;
    }
}
