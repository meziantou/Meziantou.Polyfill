using System;

static partial class PolyfillExtensions
{
    public static DateTimeOffset AddMicroseconds(this DateTimeOffset target, double microseconds)
    {
        const long TicksPerMicrosecond = 10;
        var maxMicroseconds = DateTime.MaxValue.Ticks / TicksPerMicrosecond;

        if (Math.Abs(microseconds) > maxMicroseconds)
            throw new ArgumentOutOfRangeException(nameof(microseconds));

        var integralPart = Math.Truncate(microseconds);
        var fractionalPart = microseconds - integralPart;
        var ticks = (long)integralPart * TicksPerMicrosecond;
        ticks += (long)(fractionalPart * TicksPerMicrosecond);
        return target.AddTicks(ticks);
    }
}
