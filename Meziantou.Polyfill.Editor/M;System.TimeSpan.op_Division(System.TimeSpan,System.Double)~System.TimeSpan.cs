using System;

static partial class PolyfillExtensions
{
    extension(TimeSpan)
    {
        public static TimeSpan operator /(TimeSpan timeSpan, double divisor)
        {
            if (double.IsNaN(divisor))
            {
                throw new ArgumentException("TimeSpan does not accept floating point Not-a-Number values.", nameof(divisor));
            }

            return IntervalFromDoubleTicks(timeSpan.Ticks / divisor);

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
}
