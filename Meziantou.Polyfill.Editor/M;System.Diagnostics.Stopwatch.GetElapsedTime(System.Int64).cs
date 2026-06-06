using System;
using System.Diagnostics;
static partial class PolyfillExtensions_Stopwatch
{
    extension(Stopwatch)
    {
        public static TimeSpan GetElapsedTime(long startingTimestamp) => Stopwatch.GetElapsedTime(startingTimestamp, Stopwatch.GetTimestamp());
    }
}
