using System;
using System.Diagnostics;

static partial class PolyfillExtensions
{
    extension(System.Environment)
    {
        public static long TickCount64 => (Stopwatch.GetTimestamp() * 1000) / Stopwatch.Frequency;
    }
}
