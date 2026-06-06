using System;

static partial class PolyfillExtensions
{
    public static DateTime AddMicroseconds(this DateTime target, double value)
    {
        const long TicksPerMicrosecond = 10;
        var maxMicroseconds = DateTime.MaxValue.Ticks / TicksPerMicrosecond;

        if (Math.Abs(value) > maxMicroseconds)
            throw new ArgumentOutOfRangeException(nameof(value));

        var integralPart = Math.Truncate(value);
        var fractionalPart = value - integralPart;
        var ticks = (long)integralPart * TicksPerMicrosecond;
        ticks += (long)(fractionalPart * TicksPerMicrosecond);
        return target.AddTicks(ticks);
    }
}
