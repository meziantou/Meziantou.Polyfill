﻿static partial class PolyfillExtensions
{
    public static bool StartsWith(this string target, char value)
    {
        return target.Length > 0 && target[0] == value;
    }
}