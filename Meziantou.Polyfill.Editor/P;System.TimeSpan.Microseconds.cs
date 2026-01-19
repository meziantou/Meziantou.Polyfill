using System;

static partial class PolyfillExtensions
{
    extension(TimeSpan target)
    {
        public int Microseconds
        {
            get => (int)(target.Ticks / 10L % 1000L);
        }
    }
}
