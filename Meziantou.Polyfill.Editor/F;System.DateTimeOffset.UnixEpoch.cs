using System;

static partial class PolyfillExtensions
{
    extension(DateTimeOffset)
    {
        public static DateTimeOffset UnixEpoch => DateTimeOffsetHelpers.UnixEpoch;
    }
}

file static class DateTimeOffsetHelpers
{
    // 621355968000000000L = ticks for January 1, 1970, 00:00:00 UTC
    public static readonly DateTimeOffset UnixEpoch = new DateTimeOffset(621355968000000000L, TimeSpan.Zero);
}