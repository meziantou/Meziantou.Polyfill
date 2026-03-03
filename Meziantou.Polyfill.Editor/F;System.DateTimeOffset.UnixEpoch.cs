static partial class PolyfillExtensions
{
    extension(System.DateTimeOffset)
    {
        // 621355968000000000L = ticks for January 1, 1970, 00:00:00 UTC
        public static System.DateTimeOffset UnixEpoch => new System.DateTimeOffset(621355968000000000L, System.TimeSpan.Zero);
    }
}
