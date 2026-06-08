using System;
using System.Security.Cryptography;

static partial class PolyfillExtensions
{
    public static void AppendData(this IncrementalHash target, ReadOnlySpan<byte> data)
    {
        target.AppendData(data.ToArray());
    }
}