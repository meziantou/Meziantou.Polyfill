﻿static partial class PolyfillExtensions
{
    public static bool EndsWith(this string target, char value)
    {
        return target.Length > 0 && target[target.Length - 1] == value;
    }
}