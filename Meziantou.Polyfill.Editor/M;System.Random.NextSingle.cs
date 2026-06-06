using System;
static partial class PolyfillExtensions
{
    public static float NextSingle(this Random target) => target.Next(1 << 24) / (float)(1 << 24);
}
