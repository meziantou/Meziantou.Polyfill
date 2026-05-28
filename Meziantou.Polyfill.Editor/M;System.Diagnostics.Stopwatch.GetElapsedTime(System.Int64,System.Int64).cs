using System;
using System.Diagnostics;

static partial class PolyfillExtensions
{
    extension(Stopwatch)
    {
        public static TimeSpan GetElapsedTime(long startingTimestamp, long endingTimestamp)
        {
            return new TimeSpan((long)((endingTimestamp - startingTimestamp) * ((double)TimeSpan.TicksPerSecond / Stopwatch.Frequency)));
        }
    }
}
