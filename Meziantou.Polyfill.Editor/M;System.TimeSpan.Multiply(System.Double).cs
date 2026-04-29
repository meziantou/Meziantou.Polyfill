using System;

static partial class PolyfillExtensions
{
    public static TimeSpan Multiply(this TimeSpan timeSpan, double factor)
    {
        if (double.IsNaN(factor))
        {
            throw new ArgumentException("TimeSpan does not accept floating point Not-a-Number values.", nameof(factor));
        }

        return IntervalFromDoubleTicks(timeSpan.Ticks * factor);

        static TimeSpan IntervalFromDoubleTicks(double ticks)
        {
            if ((ticks > long.MaxValue) || (ticks < long.MinValue) || double.IsNaN(ticks))
            {
                throw new OverflowException();
            }

            if (ticks == long.MaxValue)
            {
                return TimeSpan.MaxValue;
            }

            return new TimeSpan((long)ticks);
        }
    }
}
