using System;

static partial class PolyfillExtensions
{
    extension(TimeSpan target)
    {
        public int Nanoseconds
        {
            get => (int)(target.Ticks % 10L * 100L);
        }
    }
}
