using System;
static partial class PolyfillExtensions
{
    public static void GetHexString(this Random target, Span<char> destination, bool lowercase = false)
    {
        var chars = lowercase ? "0123456789abcdef" : "0123456789ABCDEF";
        for (var i = 0; i < destination.Length; i++)
        {
            destination[i] = chars[target.Next(16)];
        }
    }
}
