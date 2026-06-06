using System;
static partial class PolyfillExtensions
{
    public static long NextInt64(this Random target) => target.NextInt64(long.MaxValue);
}
