using System;

static partial class PolyfillExtensions
{
    extension(System.Net.HttpVersion)
    {
        public static Version Version20 => HttpVersionVersion20.Value;
    }
}

file static class HttpVersionVersion20
{
    public static readonly Version Value = new(2, 0);
}
