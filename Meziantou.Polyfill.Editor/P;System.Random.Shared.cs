using System;

file sealed class SharedRandom
{
    [ThreadStatic]
    private static Random? t_random;

    public static Random Instance => t_random ??= new Random();
}

partial class PolyfillExtensions
{
    extension(Random)
    {
        public static Random Shared => SharedRandom.Instance;
    }
}
