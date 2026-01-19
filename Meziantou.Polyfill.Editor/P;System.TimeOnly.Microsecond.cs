using System;

static partial class PolyfillExtensions
{
    extension(TimeOnly target)
    {
        public int Microsecond
        {
            get => (int)(target.Ticks / 10L % 1000L);
        }
    }
}
