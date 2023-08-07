using System;
using System.Text;

static partial class PolyfillExtensions
{
    public static string GetString(this Encoding target, ReadOnlySpan<byte> bytes)
    {
        return target.GetString(bytes.ToArray());
    }
}