using System;
static partial class PolyfillExtensions
{
    public static string GetHexString(this Random target, int stringLength, bool lowercase = false)
    {
        if (stringLength < 0) throw new ArgumentOutOfRangeException(nameof(stringLength));
        var chars = lowercase ? "0123456789abcdef" : "0123456789ABCDEF";
        var result = new char[stringLength];
        for (var i = 0; i < result.Length; i++)
        {
            result[i] = chars[target.Next(16)];
        }

        return new string(result);
    }
}
