using System;

static partial class PolyfillExtensions
{
    extension(TimeOnly target)
    {
        public int Nanosecond
        {
            get => (int)(target.Ticks % 10L * 100L);
        }
    }
}
